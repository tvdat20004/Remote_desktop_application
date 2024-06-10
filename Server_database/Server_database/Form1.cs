using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using MongoDB.Bson.IO;
using Newtonsoft.Json;
using System.IO;

namespace Server_database
{
    public partial class Form1 : Form
    {
        int port;
        public Form1(int _port)
        {
            InitializeComponent();
            connectDB();
            port = _port;
        }
        MongoClient mongoClient;
        class User
        {
            public ObjectId Id { get; set; }
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

        private void connectDB()
        {

            string url = "mongodb+srv://tvdat20004:tvdat20004@ultraview .ammewhw.mongodb.net/?retryWrites=true&w=majority&appName=Ultraview";
            var pack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("elementNameConvention", pack, x => true);
            mongoClient = new MongoClient(url);
            // Get the user collection
            var database = mongoClient.GetDatabase("ultraview");
            var userCollection = database.GetCollection<User>("user");

            // Create a unique index on Username
            var options = new CreateIndexOptions { Unique = true };
            var field = new StringFieldDefinition<User>("Username");
            var indexDefinition = new IndexKeysDefinitionBuilder<User>().Ascending(field);
            userCollection.Indexes.CreateOne(indexDefinition, options);
        }
        public string GetIP()
        {
            // Get all network interfaces
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (var networkInterface in networkInterfaces)
            {
                // Check if the network interface is the Wi-Fi adapter
                if (networkInterface.Name == "Wi-Fi" && networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    var properties = networkInterface.GetIPProperties();

                    // Get the first IPv4 address that is not a loopback address
                    var address = properties.UnicastAddresses
                        .FirstOrDefault(a => a.Address.AddressFamily == AddressFamily.InterNetwork &&
                                             !IPAddress.IsLoopback(a.Address));

                    if (address != null)
                    {
                        return address.Address.ToString();
                    }
                }
            }
            return string.Empty;
        }
        Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        async void StartUnsafeThread()
        {
            try
            {
                string myIP = GetIP();
                IPEndPoint ipepServer1 = new IPEndPoint(IPAddress.Parse(myIP), port);
                listenerSocket.Bind(ipepServer1);
                listenerSocket.Listen(-1);
                richTextBox1.Text += $"Server is listening on {myIP}:{port}.\n";
            }
            catch
            {
                MessageBox.Show("Server is already running!!!");
                return;
            }

            while (true)
            {
                Socket clientSocket = await listenerSocket.AcceptAsync();
                richTextBox1.Text += $"Load balancing server connected\n";
                Thread listenMsg = new Thread(() => handleRequest(clientSocket));
                listenMsg.Start();
            }
        }
        private void sendRespone(Socket client, string msg)
        {
            client.Send(Encoding.UTF8.GetBytes(msg));
        }
        private string listenMsgFromClient(Socket clientSocket)
        {
            try
            {
                NetworkStream mainStream = new NetworkStream(clientSocket);
                byte[] buffer = new byte[1024];
                int rec = mainStream.Read(buffer, 0, buffer.Length);
                string message = Encoding.UTF8.GetString(buffer, 0, rec);
                return message;
            }
            catch
            {
                return null;
            }   
        }
        private void handleRequest(Socket clientSocket)
        {

            string message = listenMsgFromClient(clientSocket);
            var json = Newtonsoft.Json.JsonConvert.DeserializeObject<request>(message);
            if (json.type == "Register")
            {
                try
                {
                    var user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(json.data);
                    var database = mongoClient.GetDatabase("ultraview");
                    var data = database.GetCollection<User>("user");
                    var newUsers = new User
                    {
                        Name = user.Name,
                        Email = user.Email,
                        Password = user.Password,
                        Username = user.Username
                    };
                    data.InsertOne(newUsers);
                    Thread sendSuccessMess = new Thread(() => sendRespone(clientSocket, "Register successfully!!!"));
                    sendSuccessMess.Start();
                    richTextBox1.Text += $"{clientSocket.RemoteEndPoint} register successfully!!!\n";
                }
                catch
                {
                    Thread sendFailMess = new Thread(() => sendRespone(clientSocket, "Register fail :("));
                    sendFailMess.Start();
                    richTextBox1.Text += $"{clientSocket.RemoteEndPoint} register fail :(\n";
                }
            }
            else if (json.type == "Login")
            {
                var user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(json.data);
                var database = mongoClient.GetDatabase("ultraview");
                var data = database.GetCollection<User>("user");
                var filter = Builders<User>.Filter.Eq("Username", user.Username);
                var _user = data.Find(filter).FirstOrDefault();
                if (_user == null)
                {
                    richTextBox1.Text += $"Client {clientSocket.RemoteEndPoint} login fail!!!\n";
                    Thread sendFailMess = new Thread(() => sendRespone(clientSocket, "Login fail :("));
                    sendFailMess.Start();
                }
                else
                {
                    if (_user.Password != user.Password)
                    {
                        richTextBox1.Text += $"Client {clientSocket.RemoteEndPoint} login fail!!!\n";
                        Thread sendFailMess = new Thread(() => sendRespone(clientSocket, "Login fail :("));
                        sendFailMess.Start();
                    }
                    else
                    {
                        richTextBox1.Text += $"Client {clientSocket.RemoteEndPoint} login successfully!!!\n";
                        Thread sendSuccessMess = new Thread(() => sendRespone(clientSocket, "Login successfully"));
                        sendSuccessMess.Start();
                    }   
                }
            }
        }

        private void listeningButton_Click(object sender, EventArgs e)
        {
            
        }

        private void closeServer(object sender, FormClosingEventArgs e)
        {
            string filename = DateTime.Now.ToString("HH_mm_ss");
            string saveFolderPath = @"C:\Ultraview\log";
            string fileName = $"log_db_{filename}.txt";
            string fullFilePath = Path.Combine(saveFolderPath, fileName);
            Directory.CreateDirectory(saveFolderPath);
            File.WriteAllText(fullFilePath, richTextBox1.Text);
        }

        private void loadForm(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            Thread serverThread = new Thread(new ThreadStart(StartUnsafeThread));
            serverThread.Start();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
