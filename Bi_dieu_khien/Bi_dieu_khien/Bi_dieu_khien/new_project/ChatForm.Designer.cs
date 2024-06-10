namespace new_project
{
    partial class ChatForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatForm));
            this.txtcontent = new System.Windows.Forms.RichTextBox();
            this.txtsendmess = new System.Windows.Forms.RichTextBox();
            this.btnsend = new System.Windows.Forms.Button();
            this.btnsendfile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statussendfile = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtcontent
            // 
            this.txtcontent.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcontent.Location = new System.Drawing.Point(137, 12);
            this.txtcontent.Name = "txtcontent";
            this.txtcontent.Size = new System.Drawing.Size(603, 461);
            this.txtcontent.TabIndex = 0;
            this.txtcontent.Text = "";
            // 
            // txtsendmess
            // 
            this.txtsendmess.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsendmess.Location = new System.Drawing.Point(137, 511);
            this.txtsendmess.Name = "txtsendmess";
            this.txtsendmess.Size = new System.Drawing.Size(603, 93);
            this.txtsendmess.TabIndex = 1;
            this.txtsendmess.Text = "";
            this.txtsendmess.TextChanged += new System.EventHandler(this.txtsendmess_TextChanged);
            this.txtsendmess.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyEnter);
            // 
            // btnsend
            // 
            this.btnsend.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsend.Location = new System.Drawing.Point(564, 628);
            this.btnsend.Name = "btnsend";
            this.btnsend.Size = new System.Drawing.Size(176, 58);
            this.btnsend.TabIndex = 2;
            this.btnsend.Text = "Send";
            this.btnsend.UseVisualStyleBackColor = true;
            this.btnsend.Click += new System.EventHandler(this.btnsend_Click);
            // 
            // btnsendfile
            // 
            this.btnsendfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsendfile.Location = new System.Drawing.Point(137, 628);
            this.btnsendfile.Name = "btnsendfile";
            this.btnsendfile.Size = new System.Drawing.Size(176, 58);
            this.btnsendfile.TabIndex = 3;
            this.btnsendfile.Text = "Send file";
            this.btnsendfile.UseVisualStyleBackColor = true;
            this.btnsendfile.Click += new System.EventHandler(this.btnsendfile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 29);
            this.label1.TabIndex = 4;
            this.label1.Text = "Content";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 504);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 29);
            this.label2.TabIndex = 5;
            this.label2.Text = "Message";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statussendfile});
            this.statusStrip1.Location = new System.Drawing.Point(0, 698);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(755, 26);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statussendfile
            // 
            this.statussendfile.Name = "statussendfile";
            this.statussendfile.Size = new System.Drawing.Size(49, 20);
            this.statussendfile.Text = "Status";
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 724);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnsendfile);
            this.Controls.Add(this.btnsend);
            this.Controls.Add(this.txtsendmess);
            this.Controls.Add(this.txtcontent);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChatForm";
            this.Text = "ChatForm";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtcontent;
        private System.Windows.Forms.RichTextBox txtsendmess;
        private System.Windows.Forms.Button btnsend;
        private System.Windows.Forms.Button btnsendfile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statussendfile;
    }
}