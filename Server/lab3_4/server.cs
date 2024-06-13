using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net; 
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Net.NetworkInformation;

namespace lab3_4
{

    public partial class server : Form
    {
        public server()
        {
            InitializeComponent();
        }
        // Dictionary to store the mapping between ID and client's socket
        Dictionary<Int64, Socket> mapping = new Dictionary<Int64, Socket>();
        // Get IP address of the server
        public string GetIP()
        {
            var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (var networkInterface in networkInterfaces)
            {
                if (networkInterface.Name == "Wi-Fi" && networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    var properties = networkInterface.GetIPProperties();
                    var address = properties.UnicastAddresses
                        .FirstOrDefault(a => a.Address.AddressFamily == AddressFamily.InterNetwork && !IPAddress.IsLoopback(a.Address));
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
                // Bind the server to the IP address of the server and port 8080
                string myIP = GetIP();
                IPEndPoint ipepServer1 = new IPEndPoint(IPAddress.Parse(myIP), 8080);
                listenerSocket.Bind(ipepServer1);
                listenerSocket.Listen(-1);     
                richTextBox1.Text += $"Server is listening on {myIP}:8080.\n";
            }
            catch
            {
                MessageBox.Show("Server is already running!!!");
                return;
            }
            // Accept client connection
            while (true)
            {
                Socket clientSocket = await listenerSocket.AcceptAsync();
                richTextBox1.Text += $"Client {clientSocket.RemoteEndPoint} connected\n";
                Thread thr = new Thread(() => HandleClient(clientSocket));
                thr.Start();
            }
        }        
        // Handle each client connection
        void HandleClient(Socket clientSocket)
        {
            mapping.Add(clientSocket.GetHashCode(), clientSocket);
            // Send the client's ID to the client
            byte[] data = Encoding.UTF8.GetBytes(clientSocket.GetHashCode().ToString());
            clientSocket.Send(data);
            // Listen for response from the client
            string msg = listenMsgFromClient(clientSocket);
            if (msg == null)
            {
                richTextBox1.Text += $"{clientSocket.RemoteEndPoint} disconnected\n";
                return;
            }
            if (msg != "" )
            {
                string[] m = msg.Split(':');
                Int64 id = Int64.Parse(m[0]);
                try
                {
                    Socket otherSocket = mapping[id];
                    richTextBox1.Text += $"{clientSocket.RemoteEndPoint} is mapping with {otherSocket.RemoteEndPoint}\n";
                    sendMessage2Client(otherSocket, clientSocket.RemoteEndPoint.ToString() + ":" + m[1]);
                    sendMessage2Client(clientSocket, "ID is available");
                    mapping.Remove(clientSocket.GetHashCode());
                    mapping.Remove(otherSocket.GetHashCode());
                }
                catch
                {   
                    sendMessage2Client(clientSocket, "ID is not available");
                }
            }
            
        }
        // Send message to client
        private void sendMessage2Client(Socket client, string msg)
        {
            client.Send(Encoding.UTF8.GetBytes(msg));
        }
        // Listen for message from client
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
        // Save log file when closing the form
        private void closeForm(object sender, FormClosingEventArgs e)
        {
            string filename = DateTime.Now.ToString("HH_mm_ss");
            string saveFolderPath = @"C:\Ultraview\log";
            string fileName = $"log_server_{filename}.txt";
            string fullFilePath = Path.Combine(saveFolderPath, fileName);
            Directory.CreateDirectory(saveFolderPath); 
            File.WriteAllText(fullFilePath, richTextBox1.Text);
        }

        private void listenButton_click(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            Thread serverThread = new Thread(new ThreadStart(StartUnsafeThread));
            serverThread.Start();
        }
    }
}
