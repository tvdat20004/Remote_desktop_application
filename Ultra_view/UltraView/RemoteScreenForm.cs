using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UltraView
{
    public partial class RemoteScreenForm : Form
    {
        #region Connect and ReceiveImage
        int port;
        public TcpClient client;
        public TcpListener server;
        public NetworkStream mainStream, ns;
        public readonly Thread Listening;

        public readonly Thread GetImage;
        public Size receivedImageSize;
        public bool gotten = false;
        bool ok = false;

        public RemoteScreenForm(int Port)
        {
            port = Port;   
            /*client = new TcpClient();
            client.Connect(IPAddress.Parse("192.168.102.45"), 8080);
            ns = client.GetStream();
            //Thread serverThread = new Thread(new ThreadStart(startThread));
            //serverThread.Start();
            
           
            Thread thread = new Thread(() => getMessage());
            thread.Start();*/
            
            client = new TcpClient();
            Listening = new Thread(StartListening);
            GetImage = new Thread(ReceiveImage);
           
            InitializeComponent();
            this.ActiveControl = picShowScreen;
            /*//listenToClient();
                   // break;
             *//*   }
            }*/
        }
        Socket listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Socket clientSocket;
        public void StartListening()
        {
            try
            {
                while (!client.Connected)
                {
                    server.Start();

                    client = server.AcceptTcpClient();
                }
                GetImage.Start();
            }
            catch
            {
                //MessageBox.Show("Listening failed!");
                StopListening();
            }
        }
        private void ReceiveImage()
        {
            BinaryFormatter binFormatter = new BinaryFormatter();
            try
            {
                while (client.Connected)
                {
                    mainStream = client.GetStream();
                    if (gotten == true)
                    {
                        picShowScreen.Image = (Image)binFormatter.Deserialize(mainStream);
                    }
                    else
                    {
                        Image receivedImage = (Image)binFormatter.Deserialize(mainStream);
                        picShowScreen.Image = receivedImage;
                        receivedImageSize.Height = receivedImage.Height;
                        receivedImageSize.Width = receivedImage.Width;
                        gotten = true;
                    }
                }
            }
            catch
            {
                //Thêm để thoong báo khi bên client out
                MessageBox.Show("Connection has been lost!");
                this.Close();
            }
        }
        

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            server = new TcpListener(IPAddress.Any, port);
            Listening.Start();
         
        }
       

       

   
        public void StopListening()
        {
            try
            {
                server.Stop();
                client.Close();
                client = null;
                if (Listening.IsAlive) Listening.Abort();
                if (GetImage.IsAlive) GetImage.Abort();
                MessageBox.Show("Disconnect success!");
            }
            catch { }
        }

       
        void BroadcastImage(Image img)
        {
            picShowScreen.Image = img;
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            StopListening();
        }
        private void RemoteScreenForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm.RemoteScreenFormCount--; // giảm biến đếm số lượng form remote
        }
        #endregion


        #region SendClick
        
        private NetworkStream ostream;
        private void sendText(string str)
        {
            if(client.Connected)
            {
                
                BinaryFormatter binFormatter = new BinaryFormatter();
                ostream = client.GetStream();
                binFormatter.Serialize(ostream, str);
                
            }
        }
        private void picShowScreen_MouseMove(object sender, MouseEventArgs e)
        {    
            int posX = this.PointToClient(Cursor.Position).X;
            int posY = this.PointToClient(Cursor.Position).Y;
            lbMouseMove.Text = "\tPoint: " + posX + ":" + posY;
            sendText("MM:" + posX + ":" + posY + ":" + picShowScreen.Width + ":" + picShowScreen.Height);
        }

        private void picShowScreen_MouseClick(object sender, MouseEventArgs e)
        {
            int posX = this.PointToClient(Cursor.Position).X;
            int posY = this.PointToClient(Cursor.Position).Y;
            if (e.Button == MouseButtons.Right)
            {
                lbStatus.Text = "Right click " + posX + " : " + posY;
                sendText("RC:" + posX + ":" + posY + ":" + picShowScreen.Width + ":" + picShowScreen.Height);
            }
            else if(e.Button==MouseButtons.Middle)
            {
                lbStatus.Text = "Middle click " + posX + " : " + posY;
                sendText("MC:" + posX + ":" + posY + ":" + picShowScreen.Width + ":" + picShowScreen.Height);
            }

        }
        private void picShowScreen_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int posX = this.PointToClient(Cursor.Position).X;
            int posY = this.PointToClient(Cursor.Position).Y;
            if (e.Button == MouseButtons.Right)
            {
                lbStatus.Text = "Double right click " + posX + " : " + posY;
                sendText("DR:" + posX + ":" + posY + ":" + picShowScreen.Width + ":" + picShowScreen.Height);
            } 
        }

        //Giu chuot
        private void picShowScreen_MouseDown(object sender, MouseEventArgs e)
        {
            int posX = this.PointToClient(Cursor.Position).X;
            int posY = this.PointToClient(Cursor.Position).Y;
            if (e.Button == MouseButtons.Left)
            {
                lbStatus.Text = "Mouse Left Down " + posX + " : " + posY;
                sendText("LD:" + posX + ":" + posY + ":" + picShowScreen.Width + ":" + picShowScreen.Height);
            }
        }

        private void picShowScreen_MouseUp(object sender, MouseEventArgs e)
        {
            int posX = this.PointToClient(Cursor.Position).X;
            int posY = this.PointToClient(Cursor.Position).Y;
            if (e.Button == MouseButtons.Left)
            {
                lbStatus.Text = "Mouse Left Up " + posX + " : " + posY;
                sendText("LU:" + posX + ":" + posY + ":" + picShowScreen.Width + ":" + picShowScreen.Height);
            }
        }

        #endregion

        #region SendKey
      
        private void RemoteScreenForm_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                string keystr = "KD:" + e.KeyValue.ToString();
                sendText(keystr);
            }
            catch { }
            
        }

        private void RemoteScreenForm_KeyUp(object sender, KeyEventArgs e)
        {

            try
            {
                string keystr = "KU:" + e.KeyValue.ToString();
                sendText(keystr);
            }
            catch { }
            
        }
        #endregion
        
        #region Status Strip
        //label lay picture box size => set gia tri trong OnLoad va SizeChanged
        private void RemoteScreenForm_SizeChanged(object sender, EventArgs e)
        {
            //Show size on statusbar
            lbSize.Text = "\tSize: " + picShowScreen.Width + "x" + (picShowScreen.Height);
        }
        #endregion

        private void picShowScreen_Click(object sender, EventArgs e)
        {

        }
    }
}
