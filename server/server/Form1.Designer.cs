namespace server
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.buttonListen = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBoxServer = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.richTextBoxLobby = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.logs = new System.Windows.Forms.RichTextBox();
            this.button_debug = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(61, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port: ";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(127, 42);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(304, 22);
            this.textBoxPort.TabIndex = 1;
            // 
            // buttonListen
            // 
            this.buttonListen.Location = new System.Drawing.Point(459, 37);
            this.buttonListen.Name = "buttonListen";
            this.buttonListen.Size = new System.Drawing.Size(142, 33);
            this.buttonListen.TabIndex = 2;
            this.buttonListen.Text = "Listen";
            this.buttonListen.UseVisualStyleBackColor = true;
            this.buttonListen.Click += new System.EventHandler(this.buttonListen_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(64, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(146, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Players in the server: ";
            // 
            // richTextBoxServer
            // 
            this.richTextBoxServer.Location = new System.Drawing.Point(67, 111);
            this.richTextBoxServer.Name = "richTextBoxServer";
            this.richTextBoxServer.ReadOnly = true;
            this.richTextBoxServer.Size = new System.Drawing.Size(254, 188);
            this.richTextBoxServer.TabIndex = 4;
            this.richTextBoxServer.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(436, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Players in the lobby: ";
            // 
            // richTextBoxLobby
            // 
            this.richTextBoxLobby.Location = new System.Drawing.Point(439, 111);
            this.richTextBoxLobby.Name = "richTextBoxLobby";
            this.richTextBoxLobby.ReadOnly = true;
            this.richTextBoxLobby.Size = new System.Drawing.Size(254, 188);
            this.richTextBoxLobby.TabIndex = 6;
            this.richTextBoxLobby.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(64, 311);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Logs: ";
            // 
            // logs
            // 
            this.logs.Location = new System.Drawing.Point(67, 341);
            this.logs.Name = "logs";
            this.logs.ReadOnly = true;
            this.logs.Size = new System.Drawing.Size(626, 256);
            this.logs.TabIndex = 8;
            this.logs.Text = "";
            // 
            // button_debug
            // 
            this.button_debug.Location = new System.Drawing.Point(664, 40);
            this.button_debug.Name = "button_debug";
            this.button_debug.Size = new System.Drawing.Size(133, 42);
            this.button_debug.TabIndex = 9;
            this.button_debug.Text = "DEBUGLA";
            this.button_debug.UseVisualStyleBackColor = true;
            this.button_debug.Click += new System.EventHandler(this.button_debug_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 664);
            this.Controls.Add(this.button_debug);
            this.Controls.Add(this.logs);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.richTextBoxLobby);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.richTextBoxServer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonListen);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Button buttonListen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox richTextBoxServer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox richTextBoxLobby;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox logs;
        private System.Windows.Forms.Button button_debug;
    }
}

