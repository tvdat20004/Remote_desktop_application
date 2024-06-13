using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltraView
{
    public partial class Login : Form
    {
        public Login()
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
        // Hash mật khẩu
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
        //Tiến hành đăng kí
        private void Register_Click(object sender, EventArgs e)
        {
            if(Password.Text != PasswordAgain.Text)
            {
                MessageBox.Show("Mật khẩu không giống nhau");
                return;
            }
            client = new TcpClient();
            client.Connect(IPAddress.Parse("192.168.102.133"), 8888);
            var newUsers = new User
            {
                Name = Name.Text,
                Email = email.Text,
                Password = HashPassword(Password.Text),
                Username = Username.Text,
            };

            string jsonUser = JsonConvert.SerializeObject(newUsers);
            var req = new request
            {
                type = "Register",
                data = jsonUser,
            };
            string jsonString = JsonConvert.SerializeObject(req);
            byte[] messageBytes = Encoding.UTF8.GetBytes(jsonString);
            using (NetworkStream stream = client.GetStream())
            {
                
                stream.Write(messageBytes, 0, messageBytes.Length);
                ConnectServer(stream, 1);
            }
            
            

        }
        //Tiến hành đăng nhập 
        private async void LoginButton_Click(object sender, EventArgs e)
        {
            client = new TcpClient();
            client.Connect(IPAddress.Parse("192.168.102.32"), 8888);
            var newUsers = new User
            {
                Password = HashPassword(PassLogin.Text),
                Username = NameLogin.Text,
            };

            string jsonUser = JsonConvert.SerializeObject(newUsers);
            var req = new request
            {
                type = "Login",
                data = jsonUser,
            };
            string jsonString = JsonConvert.SerializeObject(req);
            byte[] messageBytes = Encoding.UTF8.GetBytes(jsonString);
            NetworkStream ns = client.GetStream();
            {
              
                ns.Write(messageBytes, 0, messageBytes.Length);
                ConnectServer(ns, 2);
                
            }
        }
        //Sau khi nhận được phản hồi từ server1 thì tiến hành kết nối server2
        private async void ConnectServer(NetworkStream ns, int type)
        {
            
            bool ok = false;
            while (client.Connected)
            {
                try
                {
                    if (ns.DataAvailable)
                    {
                        byte[] buffer = new byte[client.ReceiveBufferSize];
                        if(buffer.Length > 0)
                        {
                            await ns.ReadAsync(buffer, 0, (int)client.ReceiveBufferSize);
                            string incomingMessage = Encoding.UTF8.GetString(buffer);
                            if (incomingMessage.Contains("fail"))
                            {
                                MessageBox.Show($"{incomingMessage}");
                            }
                            else
                            {
                                MessageBox.Show($"{incomingMessage}");
                                ok = true;
                            }
                            break;
                        }
                    }
                }
                catch
                {
                    return;
                }
            }
            if(!ok || type == 1)
            {
                return;
            }
            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect(IPAddress.Parse("192.168.102.32"), 8080);
            NetworkStream networkStream = tcpClient.GetStream();
            while (tcpClient.Connected)
            {
                try
                {
                    if (networkStream.DataAvailable)
                    {
                        byte[] buffer = new byte[tcpClient.ReceiveBufferSize];
                        if (buffer.Length > 0)
                        {
                            await networkStream.ReadAsync(buffer, 0, (int)tcpClient.ReceiveBufferSize);
                            string id = Encoding.UTF8.GetString(buffer);
                            ConnectOtherForm form = new ConnectOtherForm(id, tcpClient);

                            form.Show();
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
    }
}
