using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Numerics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UltraView
{
    public partial class ChatBox : Form
    {
        

        TcpClient tcpClient;
        TcpListener server;
        private readonly Thread Listening;
        private readonly Thread GetText;
        private int port; //đem cái port gửi hình +1 để ra cái port khác sài cho gọn đường

        public ChatBox(int Port)
        {
            
            port = Port;
            tcpClient = new TcpClient();
            Listening = new Thread(StartListening);
            GetText = new Thread(ReceiveData);

            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {

            base.OnLoad(e);
            try
            {
                server = new TcpListener(IPAddress.Any, port);
                Listening.Start();

            }
            catch
            {

            }
        }
        public void StopListening()
        {
            try
            {
                tcpClient.Close();
                tcpClient = null;
                server.Stop();
            }
            catch { }
            try
            {
                if (GetText.IsAlive) GetText.Abort();
                if (Listening.IsAlive) Listening.Abort();
            }
            catch { }
            // MessageBox.Show("Disconnect success!");
        }
        private void StartListening()
        {
            try
            {
                while (!tcpClient.Connected)
                {
                    server.Start();
                    tcpClient = server.AcceptTcpClient();

                }
                MessageBox.Show("Client Connected");
                DHKeyExchange();
                GetText.Start();

            }
            catch { }
        }

        private NetworkStream istream;
      
        private NetworkStream stream;
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
                            
                            decrypted = Encoding.UTF8.GetBytes(srDecrypt.ReadToEnd());
                        }
                    }
                }
            }

            // Return the decrypted bytes.
            return decrypted;
        }
        private void sendData(byte[] data)
        {
            NetworkStream newStream = tcpClient.GetStream();
            newStream.Write(data, 0, data.Length);
        }
        private void DHKeyExchange()
        {
            BigInteger s = GetRandomBigInteger(p - 1);
            BigInteger S = BigInteger.ModPow(g, s, p);

            byte[] data = long_to_bytes(S);
            sendData(data);
            int bytesRead;
            NetworkStream NewStream = tcpClient.GetStream();
            BigInteger C = 0;
            byte[] buffer = new byte[192];
            while ((bytesRead = NewStream.Read(buffer, 0, 192)) > 0)
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
        #endregion
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
        private void sendText(string str)
        {
            //dang la server //server se lay stream cua client va truyen vo do
            //dang la client// client se su dung stream cua minh de truyen cho server
            if (tcpClient.Connected)
            {
                stream = tcpClient.GetStream();
                //Byte[] data = Encoding.UTF8.GetBytes(str);
                byte[] data = Encoding.UTF8.GetBytes(str);
                byte[] sendData = encryptAES(data, key, iv);
                byte[] extensionHeader = new byte[] { 0x01 };
                stream.Write(extensionHeader, 0, extensionHeader.Length);
                stream.Write(sendData, 0, sendData.Length);
                Status.Text = "Text Send Success";

            }

        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            StopListening();
        }
        private void ChatBox_Load(object sender, EventArgs e)
        {
            PrivateText.Focus();
            AllText.SelectionColor = Color.Blue;


        }
        private void Send_Click(object sender, EventArgs e)
        {
            if (PrivateText.Text != "")
            {
                try
                {
                    sendText("MS:" + PrivateText.Text);
                }
                catch
                {
                    PrivateText.Text += "\nTin nhắn không gửi được!";
                    return;
                }

                AllText.Text += "\nMe: " + PrivateText.Text;
                PrivateText.Text = "";
            }
        }
        NetworkStream _Streamrecv;
        private void ReceiveData()
        {
            _Streamrecv = tcpClient.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            try
            {
                while (tcpClient.Connected)
                {
                    byte[] headerBuffer = new byte[1];
                    _Streamrecv.Read(headerBuffer, 0, headerBuffer.Length);
                    if (headerBuffer[0] == 0x01)
                    {
                        bytesRead = _Streamrecv.Read(buffer, 1, buffer.Length - 1);
                        if (bytesRead > 0)
                        {
                            byte[] tmpbuffer = buffer.Skip(1).Take(bytesRead).ToArray();
                            tmpbuffer = DecryptData(tmpbuffer, key, iv);
                            string receivedData = Encoding.UTF8.GetString(tmpbuffer);
                            AllText.Text += "\nMS:" + receivedData;
                        }
                        Status.Text = "Text Receive Success";
                        continue;
                        
                    }
                    else
                    {
                        byte[] numfiles = new byte[1];
                        _Streamrecv.Read(numfiles, 0, numfiles.Length);
                        string _numfile = Encoding.UTF8.GetString(numfiles);
                        int _tmpnumfile = int.Parse(_numfile);
                        int cnt = 0;
                        while (cnt < _tmpnumfile)
                        {
                            ReceiveFile(cnt);
                            cnt++;
                        }
                        Status.Text = "File Receive Success";
                        continue;
                    }

                }
            } catch
            {
                MessageBox.Show("This message box close");
                this.Close();
            }
        }

        private void ReceiveFile(int cnt)
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

            // Save the file data to the D:\Save folder
            string saveFolderPath = @"C:\Ultraview_hehe";
            string fileName = $"received_file{cnt}{fileExtension}";
            string fullFilePath = Path.Combine(saveFolderPath, fileName);
            Directory.CreateDirectory(saveFolderPath); // Create the Save folder if it doesn't exist
            File.WriteAllBytes(fullFilePath, fileData);
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            Thread t = new Thread((ThreadStart)(() =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    int number = openFileDialog.FileNames.Length;
                    stream = tcpClient.GetStream();
                    Byte[] data = Encoding.UTF8.GetBytes(number.ToString());
                    byte[] extensionHeader = new byte[] { 0x02 };
                    NetworkStream _Streamsend = tcpClient.GetStream();
                    _Streamsend.Write(extensionHeader, 0, extensionHeader.Length);
                    _Streamsend.Write(data, 0, data.Length);
                    foreach (string file in openFileDialog.FileNames)
                    {
                        try
                        {
                            string filePath = file;


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
                        }
                    }
                    

                }
            }));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
            Status.Text = "File Send Success";

        }
    }
}
