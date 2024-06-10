using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Net.Sockets;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;
using System.Runtime.InteropServices;

namespace new_project
{
    public partial class Formdkidnhap : Form
    {
        public Formdkidnhap()
        {
            InitializeComponent();
        }

        public TcpClient client;
        class User
        {
            public string Username { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
        class request
        {
            public string type { get; set; }
            public string data { get; set; }
        }


        public string HashPassword(string password)
        {
            using (SHA512 sha512Hash = SHA512.Create())
            {
                byte[] data = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }

        NetworkStream stream = null;
        string messFromserver;
        string id;
        private void btndangnhap_Click(object sender, EventArgs e)
        {
            client = new TcpClient();
            client.Connect(IPAddress.Parse("192.168.102.32"), 8888);
            stream = client.GetStream();

            var newUsers = new User
            {
                Password = HashPassword(txtmatkhaudangnhap.Text),
                Username = txttendangnhap.Text,
            };

            string jsonUser = JsonConvert.SerializeObject(newUsers);
            var req = new request
            {
                type = "Login",
                data = jsonUser,
            };
            string jsonString = JsonConvert.SerializeObject(req);
            byte[] messageBytes = Encoding.UTF8.GetBytes(jsonString);
            //MessageBox.Show(jsonString);
            // Send the serialized JSON byte array over the TCP network
            stream.Write(messageBytes, 0, messageBytes.Length);
            
            getMessageLogin();
        }
        
        private bool connected()
        {
            while (client.Connected)
            {
                try
                {
                    if (stream.DataAvailable)
                    {
                        byte[] buffer = new byte[client.ReceiveBufferSize];
                        if (buffer.Length > 0)
                        {
                            stream.ReadAsync(buffer, 0, (int)client.ReceiveBufferSize);
                            messFromserver = Encoding.UTF8.GetString(buffer);
                            if (messFromserver.Contains("fail"))
                            {
                                MessageBox.Show(messFromserver);
                                return false;
                            }
                            else
                            {
                                MessageBox.Show(messFromserver);
                                client.Close();
                                return true;
                            }
                        }
                    }
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
        private void getMessageLogin()
        {
           if (connected() == false)
            {
                MessageBox.Show("Please enter correct account!!!");
                return;
            }


            TcpClient tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect(IPAddress.Parse("192.168.102.32"), 8080);
            }
            catch
            {
                MessageBox.Show("Can't connect to server");
            }
            
            NetworkStream ns = tcpClient.GetStream();
            while (tcpClient.Connected)
            {
                try
                {
                    if (ns.DataAvailable)
                    {
                        byte[] buffer = new byte[tcpClient.ReceiveBufferSize];
                        if (buffer.Length > 0)
                        {
                            ns.ReadAsync(buffer, 0, (int)tcpClient.ReceiveBufferSize);
                            id = Encoding.UTF8.GetString(buffer);
                            //MessageBox.Show(id);
                            Form1 form1 = new Form1(id, tcpClient);
                            form1.ShowDialog();
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }


        private void btndangky_Click(object sender, EventArgs e)
        {
            if (txtpassword.Text != txtpassagain.Text)
            {
                MessageBox.Show("Passwords are not the same");
                return;
            }

                client = new TcpClient();
                client.Connect(IPAddress.Parse("192.168.102.32"), 8888);



            var newUsers = new User
            {
                Name = txtname.Text,
                Email = txtmail.Text,
                Password = HashPassword(txtpassword.Text),
                Username = txtusername.Text,
            };

            string jsonUser = JsonConvert.SerializeObject(newUsers);
            var req = new request
            {
                type = "Register",
                data = jsonUser,
            };
            string jsonString = JsonConvert.SerializeObject(req);
            byte[] messageBytes = Encoding.UTF8.GetBytes(jsonString);
            stream = client.GetStream();

            //MessageBox.Show(jsonString);
                // Send the serialized JSON byte array over the TCP network
            stream.Write(messageBytes, 0, messageBytes.Length);
            if(connected() == false)
            {
                return;
            }
        }
    }
}
