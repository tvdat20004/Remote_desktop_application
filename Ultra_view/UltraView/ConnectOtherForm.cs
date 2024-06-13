using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltraView
{
    public partial class ConnectOtherForm : Form
    {
        private TcpClient client;

        TcpClient newClient;
        string newID;
        public ConnectOtherForm(string NewID, TcpClient NewClient)
        {
            newID = NewID;
            newClient = NewClient;
            InitializeComponent();
            btnStatusTab2.BackColor = Color.Yellow;
        }
        void Test(string partnerIP, int randomPort)
        {
            string newPort = randomPort.ToString();
            string newString = partnerIP + ":" + newPort;
            byte[] messageBytes = Encoding.UTF8.GetBytes(newString);
            NetworkStream networkStream = newClient.GetStream();
            {
                networkStream.Write(messageBytes, 0, messageBytes.Length);
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            
            string parnerIP = txtIP2.Text;
            Random random = new Random();
            int randomNumber = random.Next(8000, 9001);
            Test(parnerIP, randomNumber);
            byte[] buffer = new byte[1024];
            bool ok = true;
            while (newClient.Connected)
            {
                NetworkStream _Streamrecv = newClient.GetStream();
                if (_Streamrecv.DataAvailable)
                {
                    _Streamrecv.Read(buffer, 0, buffer.Length);
                    string receivedData = Encoding.UTF8.GetString(buffer);
                    if (receivedData.Contains("not"))
                    {
                        ok = false; 
                    }
                    break;

                }  
            }
            if(!ok)
            {
                MessageBox.Show("ID không hợp lệ");
                this.Close();
                return;
            }
            RemoteScreenForm rmtScrForm = new RemoteScreenForm(randomNumber);
            rmtScrForm.Show();
            ChatBox chatBoxForm = new ChatBox(randomNumber + 1);
            chatBoxForm.Show();
        }
    }
}
