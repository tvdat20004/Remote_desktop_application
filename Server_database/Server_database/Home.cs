using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server_database
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }
        // Get the IP address of the Wi-Fi adapter
        public string GetIP()
        {
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (var networkInterface in networkInterfaces)
            {
                if (networkInterface.Name == "Wi-Fi" && networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    var properties = networkInterface.GetIPProperties();
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
        // Start load balancing server while loading form
        private void loadForm(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            Thread clientThread = new Thread(new ThreadStart(StartClientThread));
            clientThread.Start();
        }
        async void StartClientThread()
        {
            string myIP = GetIP();
            IPEndPoint ipepServer1 = new IPEndPoint(IPAddress.Parse(myIP), 8888);
            listenerSocket.Bind(ipepServer1);
            listenerSocket.Listen(-1);

            while (true)
            {
                Socket clientSocket = await listenerSocket.AcceptAsync();
                Thread listenMsg = new Thread(() => listenMsgFromClient(clientSocket));
                listenMsg.Start();
            }
        }
        // Handle request from client
        private void listenMsgFromClient(Socket clientSocket)
        {
            try
            {
                NetworkStream mainStream = new NetworkStream(clientSocket);
                byte[] buffer = new byte[1024];
                int rec = mainStream.Read(buffer, 0, buffer.Length);
                // Load balancing
                roundRobinLoadBalancing(buffer, clientSocket);
            }
            catch
            {
            }
        }
        int index = 0;
        // Round robin load balancing
        private void roundRobinLoadBalancing(byte[] request, Socket clientSocket)
        {
            int n = dbServers.Count;
            TcpClient db = dbServers[index % n];
            NetworkStream stream = db.GetStream();
            stream.Write(request, 0, request.Length);
            index = (index + 1) % n;
            Thread recvMessFromDBServer = new Thread(() => recv(db, clientSocket));
            recvMessFromDBServer.Start();
        }
        // Receive response from database server
        private void recv(TcpClient db, Socket clientSocket)
        {
            NetworkStream stream = db.GetStream();
            while (true)
            {
                byte[] buffer = new byte[1024];
                int rec = stream.Read(buffer, 0, buffer.Length);
                if (rec > 0)
                {
                    sendRespone(clientSocket, buffer);
                    break;
                }
            }
        }
        // Send response to client
        private void sendRespone(Socket client, byte[] buffer)
        {
            client.Send(buffer);
        }

        int countDBServer = 0;
        List<TcpClient> dbServers = new List<TcpClient>();
        // Connect to database server
        void connectToDBServer(int port)
        {
            TcpClient db = new TcpClient();
            string myIP = GetIP();
            IPEndPoint ipepServer1 = new IPEndPoint(IPAddress.Parse(myIP), port);
            db.Connect(ipepServer1);
            dbServers.Add(db);
            richTextBox1.Text += $"Connected to server {countDBServer}\n";
        }

        private void launchingButton_Click(object sender, EventArgs e)
        {
            int port = 8000 + countDBServer;

            Form1 form1 = new Form1(port);
            countDBServer++;
            Task.Run(() => form1.ShowDialog());
            richTextBox1.Text += $"Server {countDBServer} is launching\n";
            Thread.Sleep(2000);
            Thread connect = new Thread(() => connectToDBServer(port));
            connect.Start();
        }
    }
}
