namespace Server_database
{
    partial class Home
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
            this.launchingButton = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // launchingButton
            // 
            this.launchingButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.launchingButton.Location = new System.Drawing.Point(47, 224);
            this.launchingButton.Name = "launchingButton";
            this.launchingButton.Size = new System.Drawing.Size(193, 55);
            this.launchingButton.TabIndex = 1;
            this.launchingButton.Text = "Launch server";
            this.launchingButton.UseVisualStyleBackColor = true;
            this.launchingButton.Click += new System.EventHandler(this.launchingButton_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(301, 69);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(683, 380);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1025, 501);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.launchingButton);
            this.Name = "Home";
            this.Text = "Home";
            this.Load += new System.EventHandler(this.loadForm);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button launchingButton;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}