using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Numerics;

namespace new_project
{
    public partial class ChatForm : Form
    {
        private string _IP;
        private int _Port;
        private TcpClient _Client;
        private NetworkStream _Streamsend;
        private NetworkStream _Streamrecv;
        private Thread GetText;
        public ChatForm(string IP, int Port)
        {
            _IP = IP;
            _Port = Port;
            _Client = new TcpClient();
            GetText = new Thread(ReceiveData);
            InitializeComponent();
        }
        public void stoprecvData()
        {
            GetText.Abort();
            _Client.Close();
            this.Invoke(new MethodInvoker(delegate
            {
                this.Close();
            }));
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {
                //Perform key exchange as soon as the form loads
                _Client.Connect(_IP, _Port);
                DHKeyExchange();
                GetText.Start();
                
            }
            catch
            {
                MessageBox.Show("Cant connect to server chat");
                return;
            }
        }

        private void sendData(byte[] data)
        {
            _Streamsend = _Client.GetStream();
            _Streamsend.Write(data, 0, data.Length);
        }

        #region cryptography
        BigInteger g = BigInteger.Parse("2");
        BigInteger p = BigInteger.Parse("2410312426921032588552076022197566074856950548502459942654116941958108831682612228890093858261341614673227141477904012196503648957050582631942730706805009223062734745341073406696246014589361659774041027169249453200378729434170325843778659198143763193776859869524088940195577346119843545301547043747207749969763750084308926339295559968882457872412993810129130294592999947926365264059284647209730384947211681434464714438488520940127459844288859336526896320919633919");
        BigInteger shared_secret;
        byte[] key = null;
        byte[] iv = null;
        public BigInteger bytes_to_long(byte[] text)
        {
            BigInteger res = 0;
            for (int i = 0; i < text.Length; i++)
            {
                res = res * 256 + text[i];
            }
            return res;
        }
        public void getKeyAndIV()
        {
            byte[] shareByte = long_to_bytes(shared_secret);
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] data = sha256Hash.ComputeHash(shareByte);
                key = data.Take(16).ToArray();
                iv = data.Skip(16).Take(16).ToArray();
            }
        }
        public byte[] DecryptData(byte[] dataToDecrypt, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (dataToDecrypt == null || dataToDecrypt.Length <= 0)
                throw new ArgumentNullException("dataToDecrypt");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            byte[] decrypted;

            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(dataToDecrypt))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            decrypted = Encoding.UTF8.GetBytes(srDecrypt.ReadToEnd());
                        }
                    }
                }
            }

            // Return the decrypted bytes.
            return decrypted;
        }
        private void DHKeyExchange()
        {
            BigInteger s = GetRandomBigInteger(p - 1);
            BigInteger S = BigInteger.ModPow(g, s, p);

            byte[] data = long_to_bytes(S);
            sendData(data);
            int bytesRead;
            _Streamrecv = _Client.GetStream();
            BigInteger C = 0;
            byte[] buffer = new byte[192];
            while ((bytesRead = _Streamrecv.Read(buffer, 0, 192)) > 0)
            {
                C = bytes_to_long(buffer);
                break;
            }
            if (C != 0)
            {
                shared_secret = BigInteger.ModPow(C, s, p);
                getKeyAndIV();

            }
        }
        private static BigInteger GetRandomBigInteger(BigInteger max)
        {
            BigInteger result;
            do
            {
                byte[] bytes = max.ToByteArray();
                using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(bytes);
                }

                bytes[bytes.Length - 1] &= 0x7F; // Ensure the number is non-negative
                result = new BigInteger(bytes);
            } while (result >= max);

            return result;
        }
        public byte[] long_to_bytes(BigInteger num)
        {
            List<byte> res = new List<byte>();
            while (num > 0)
            {
                res.Add((byte)(num % 256));
                num /= 256;
            }
            res.Reverse();
            return res.ToArray();
        }

        public byte[] encryptAES(byte[] dataToEncrypt, byte[] Key, byte[] IV)
        {
            if (dataToEncrypt == null || dataToEncrypt.Length <= 0)
                throw new ArgumentNullException("dataToEncrypt");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            byte[] encrypted;

            // Create an AesCryptoServiceProvider object
            // with the specified key and IV.
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                        csEncrypt.FlushFinalBlock();
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;
        }

        #endregion



        private void btnsend_Click(object sender, EventArgs e)
        {
            SendMess(txtsendmess.Text);
        }

        //chat with your partner
        //Each time you send a chat message, the message will be encrypted,
        //and sent including the header (0x01) and the message content.

        //header to identify sending messages or files
        private void SendMess(string mess)
        {
            if(_Client.Connected)
            {
                try
                {
                    _Streamsend = _Client.GetStream();
                    byte[] data = Encoding.UTF8.GetBytes(mess);
                    byte[] sendData = encryptAES(data, key, iv);
                    byte[] extensionHeader = new byte[] { 0x01 };
                    _Streamsend.Write(extensionHeader, 0, extensionHeader.Length);
                    _Streamsend.Write(sendData, 0, sendData.Length);
                    txtcontent.Text += "Me: " + mess + '\n';
                    txtsendmess.Clear();
                    statussendfile.Text = "Send message successfully";

                }
                catch
                {
                    MessageBox.Show("Cant send text");
                }

            }
        }

        //Similarly, for receiving data, the header needs to be separated and displayed
        private void ReceiveData()
        {
            _Streamrecv = _Client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            try
            {


                while (_Client.Connected)
                {
                    byte[] headerBuffer = new byte[1];
                    _Streamrecv.Read(headerBuffer, 0, headerBuffer.Length);

                    //if message
                    if (headerBuffer[0] == 0x01)
                    {
                        bytesRead = _Streamrecv.Read(buffer, 1, buffer.Length - 1);

                        if (bytesRead > 0)
                        {
                            byte[] tmpbuffer = buffer.Skip(1).Take(bytesRead).ToArray();
                            tmpbuffer = DecryptData(tmpbuffer, key, iv);
                            string receivedData = Encoding.UTF8.GetString(tmpbuffer);
                            AppendTextToChatContent(receivedData + "\n");
                        }
                    }
                    //if file
                    else
                    {
                        byte[] numfiles = new byte[1024];
                        _Streamrecv.Read(numfiles, 0, 1);

                        string _numfile = Encoding.UTF8.GetString(numfiles);

                        int _tmpnumfile = 0;
                        try
                        {
                            _tmpnumfile = int.Parse(_numfile);
                        }
                        catch
                        {
                            this.Invoke(new MethodInvoker(delegate
                            {
                                this.Close();
                            }));
                            //stoprecvData();
                            return;
                        }
                        int cnt = 0;
                        while (cnt < _tmpnumfile)
                        {
                            ReceiveFile(cnt);
                            cnt++;
                        }
                        statussendfile.Text = $"{cnt} files received successfully";
                    }
                }

            }
            catch
            { }
        }

        private void ReceiveFile(int _cnt)
        {
            // Read the file extension length
            byte[] extensionLengthBytes = new byte[4];
            int bytesRead = _Streamrecv.Read(extensionLengthBytes, 0, extensionLengthBytes.Length);
            if (bytesRead == 0)
                return; // No data received, try again

            int extensionLength = BitConverter.ToInt32(extensionLengthBytes, 0);
            if (bytesRead == 0)
                return; // No data received, try again

            // Read the file extension
            byte[] extensionBytes = new byte[extensionLength];
            bytesRead = _Streamrecv.Read(extensionBytes, 0, extensionBytes.Length);
            string fileExtension = Encoding.UTF8.GetString(extensionBytes);

            // Read the file size
            byte[] fileSizeBuffer = new byte[4];
            bytesRead = _Streamrecv.Read(fileSizeBuffer, 0, fileSizeBuffer.Length);
            int fileSize = BitConverter.ToInt32(fileSizeBuffer, 0);

            // Read the file data
            byte[] fileData = new byte[fileSize];
            int offset = 0;
            while (offset < fileSize)
            {
                int bytesReceived = _Streamrecv.Read(fileData, offset, fileSize - offset);
                if (bytesReceived == 0)
                    break; // Connection closed by the client
                offset += bytesReceived;
            }

            // Save the file data to the "C:\Ultraview_hehe"
            string saveFolderPath = @"C:\Ultraview_hehe";
            string dateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string fileName = $"received_file{_cnt}_{dateTime}{fileExtension}";
            string fullFilePath = Path.Combine(saveFolderPath, fileName);
            Directory.CreateDirectory(saveFolderPath); // Create the Save folder if it doesn't exist
            File.WriteAllBytes(fullFilePath, fileData);
            //MessageBox.Show($"You have received file {_cnt} saved at {saveFolderPath}");
        }



        private void AppendTextToChatContent(string text)
        {
            if (txtcontent.InvokeRequired)
            {
                txtcontent.Invoke(new Action<string>(AppendTextToChatContent), text);
            }
            else
            {
                txtcontent.Text += text;
            }
        }

        //send file
        private void btnsendfile_Click(object sender, EventArgs e)
        {
            Thread t = new Thread((ThreadStart)(() =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    int number = openFileDialog.FileNames.Length;
                    Byte[] data = Encoding.UTF8.GetBytes(number.ToString());
                    byte[] extensionHeader = new byte[] { 0x02 };
                    _Streamsend = _Client.GetStream();
                    _Streamsend.Write(extensionHeader, 0, extensionHeader.Length);
                    _Streamsend.Write(data, 0, data.Length);
                    foreach (string file in openFileDialog.FileNames)
                    {
                        try
                        {
                            string filePath = file;
                            //MessageBox.Show(filePath);

                            // Read the compressed file into a byte array
                            byte[] fileBytes = File.ReadAllBytes(filePath);

                            // Send the file extension first (e.g., ".rar")
                            string fileExtension = Path.GetExtension(filePath);
                            byte[] extensionBytes = Encoding.UTF8.GetBytes(fileExtension);

                            _Streamsend.Write(BitConverter.GetBytes(extensionBytes.Length), 0, sizeof(int));
                            _Streamsend.Write(extensionBytes, 0, extensionBytes.Length);

                            // Send the file size
                            byte[] fileSizeBytes = BitConverter.GetBytes(fileBytes.Length);
                            _Streamsend.Write(fileSizeBytes, 0, fileSizeBytes.Length);

                            // Send the file data
                            _Streamsend.Write(fileBytes, 0, fileBytes.Length);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error sending file: {ex.Message}");
                            return;
                        }
                    }
                }
            }));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
            statussendfile.Text = "Files sent successfully";
        }

        private void txtsendmess_TextChanged(object sender, EventArgs e)
        {

        }

        //Enter to send mess
        private void keyEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Prevent the beep that occurs by default
                e.SuppressKeyPress = true;
                // Load the URL
                btnsend_Click(sender, e);
            }
        }
    }
}
