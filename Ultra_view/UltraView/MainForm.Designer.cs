namespace UltraView
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabTypeConnect = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMyIP = new System.Windows.Forms.TextBox();
            this.txtMyPort = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.btnStatusTab2 = new System.Windows.Forms.Button();
            this.txtPort2 = new System.Windows.Forms.TextBox();
            this.txtIP2 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lbStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.btnOpenConnect = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnConnect2 = new System.Windows.Forms.Button();
            this.tabTypeConnect.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabTypeConnect
            // 
            this.tabTypeConnect.Controls.Add(this.tabPage1);
            this.tabTypeConnect.Controls.Add(this.tabPage2);
            this.tabTypeConnect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabTypeConnect.Location = new System.Drawing.Point(0, 0);
            this.tabTypeConnect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabTypeConnect.Name = "tabTypeConnect";
            this.tabTypeConnect.SelectedIndex = 0;
            this.tabTypeConnect.Size = new System.Drawing.Size(700, 412);
            this.tabTypeConnect.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.BackColor = System.Drawing.Color.OldLace;
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.btnOpenConnect);
            this.tabPage1.Controls.Add(this.txtMyIP);
            this.tabPage1.Controls.Add(this.txtMyPort);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Size = new System.Drawing.Size(692, 383);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Open Connect";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label5.Location = new System.Drawing.Point(211, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(212, 36);
            this.label5.TabIndex = 39;
            this.label5.Text = "Open connect";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.OldLace;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(212, 207);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 20);
            this.label3.TabIndex = 38;
            this.label3.Text = "Port:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.OldLace;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(124, 152);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 20);
            this.label4.TabIndex = 37;
            this.label4.Text = "My IP address:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtMyIP
            // 
            this.txtMyIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMyIP.Location = new System.Drawing.Point(280, 151);
            this.txtMyIP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMyIP.Name = "txtMyIP";
            this.txtMyIP.Size = new System.Drawing.Size(183, 26);
            this.txtMyIP.TabIndex = 1;
            this.txtMyIP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMyIP_KeyPress);
            // 
            // txtMyPort
            // 
            this.txtMyPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMyPort.Location = new System.Drawing.Point(280, 206);
            this.txtMyPort.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMyPort.Name = "txtMyPort";
            this.txtMyPort.Size = new System.Drawing.Size(183, 26);
            this.txtMyPort.TabIndex = 4;
            this.txtMyPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMyPort_KeyPress);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.OldLace;
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.btnStatusTab2);
            this.tabPage2.Controls.Add(this.txtPort2);
            this.tabPage2.Controls.Add(this.txtIP2);
            this.tabPage2.Controls.Add(this.label14);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.btnConnect2);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Size = new System.Drawing.Size(692, 383);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Connect to other";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.OldLace;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(179, 218);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 20);
            this.label7.TabIndex = 36;
            this.label7.Text = "Port:";
            // 
            // btnStatusTab2
            // 
            this.btnStatusTab2.BackColor = System.Drawing.Color.LawnGreen;
            this.btnStatusTab2.ForeColor = System.Drawing.Color.Lime;
            this.btnStatusTab2.Location = new System.Drawing.Point(443, 275);
            this.btnStatusTab2.Name = "btnStatusTab2";
            this.btnStatusTab2.Size = new System.Drawing.Size(25, 25);
            this.btnStatusTab2.TabIndex = 35;
            this.btnStatusTab2.UseVisualStyleBackColor = false;
            // 
            // txtPort2
            // 
            this.txtPort2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPort2.Location = new System.Drawing.Point(249, 216);
            this.txtPort2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPort2.Name = "txtPort2";
            this.txtPort2.Size = new System.Drawing.Size(237, 26);
            this.txtPort2.TabIndex = 29;
            // 
            // txtIP2
            // 
            this.txtIP2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIP2.Location = new System.Drawing.Point(249, 159);
            this.txtIP2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtIP2.Name = "txtIP2";
            this.txtIP2.Size = new System.Drawing.Size(237, 26);
            this.txtIP2.TabIndex = 27;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(157, 111);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(353, 20);
            this.label14.TabIndex = 30;
            this.label14.Text = "Please enter your partner\'s IP address an port";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.OldLace;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(128, 163);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 20);
            this.label6.TabIndex = 26;
            this.label6.Text = "Partner IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label2.Location = new System.Drawing.Point(156, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(351, 36);
            this.label2.TabIndex = 25;
            this.label2.Text = "Connect to your partner";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 386);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 14, 0);
            this.statusStrip1.Size = new System.Drawing.Size(700, 26);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lbStatus
            // 
            this.lbStatus.BackColor = System.Drawing.Color.AliceBlue;
            this.lbStatus.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.lbStatus.Name = "lbStatus";
            this.lbStatus.Size = new System.Drawing.Size(49, 20);
            this.lbStatus.Text = "Status";
            // 
            // timer1
            // 
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button2
            // 
            this.button2.AutoSize = true;
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.OldLace;
            this.button2.Location = new System.Drawing.Point(460, 244);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(205, 52);
            this.button2.TabIndex = 40;
            this.button2.Text = "Open connect";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnOpenConnect
            // 
            this.btnOpenConnect.AutoSize = true;
            this.btnOpenConnect.BackColor = System.Drawing.Color.Transparent;
            this.btnOpenConnect.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnOpenConnect.BackgroundImage")));
            this.btnOpenConnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOpenConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenConnect.ForeColor = System.Drawing.Color.OldLace;
            this.btnOpenConnect.Location = new System.Drawing.Point(6, 244);
            this.btnOpenConnect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOpenConnect.Name = "btnOpenConnect";
            this.btnOpenConnect.Size = new System.Drawing.Size(205, 52);
            this.btnOpenConnect.TabIndex = 25;
            this.btnOpenConnect.Text = "Open connect";
            this.btnOpenConnect.UseVisualStyleBackColor = false;
            this.btnOpenConnect.Click += new System.EventHandler(this.btnOpenConnect_Click);
            // 
            // button1
            // 
            this.button1.AutoSize = true;
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.OldLace;
            this.button1.Location = new System.Drawing.Point(485, 258);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(148, 52);
            this.button1.TabIndex = 37;
            this.button1.Text = "CONNECT";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnConnect2
            // 
            this.btnConnect2.AutoSize = true;
            this.btnConnect2.BackColor = System.Drawing.Color.Transparent;
            this.btnConnect2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnConnect2.BackgroundImage")));
            this.btnConnect2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnConnect2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect2.ForeColor = System.Drawing.Color.OldLace;
            this.btnConnect2.Location = new System.Drawing.Point(81, 258);
            this.btnConnect2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnConnect2.Name = "btnConnect2";
            this.btnConnect2.Size = new System.Drawing.Size(148, 52);
            this.btnConnect2.TabIndex = 24;
            this.btnConnect2.Text = "CONNECT";
            this.btnConnect2.UseVisualStyleBackColor = false;
            this.btnConnect2.Click += new System.EventHandler(this.btnConnect2_Click_1);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(700, 412);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabTypeConnect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = " UltraView";
            this.tabTypeConnect.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TabControl tabTypeConnect;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lbStatus;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtMyIP;
        private System.Windows.Forms.TextBox txtMyPort;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnStatusTab2;
        private System.Windows.Forms.TextBox txtPort2;
        private System.Windows.Forms.TextBox txtIP2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnConnect2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOpenConnect;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}