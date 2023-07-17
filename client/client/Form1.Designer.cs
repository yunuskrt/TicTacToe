namespace client
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
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonJoin = new System.Windows.Forms.Button();
            this.buttonLeave = new System.Windows.Forms.Button();
            this.logs = new System.Windows.Forms.RichTextBox();
            this.buttonMove = new System.Windows.Forms.Button();
            this.textBoxMove = new System.Windows.Forms.TextBox();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.Game = new System.Windows.Forms.RichTextBox();
            this.labelWin = new System.Windows.Forms.Label();
            this.labelLoss = new System.Windows.Forms.Label();
            this.labelDraw = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP Address:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(183, 23);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port:";
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(69, 20);
            this.textBoxIP.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(110, 20);
            this.textBoxIP.TabIndex = 2;
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(210, 20);
            this.textBoxPort.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(110, 20);
            this.textBoxPort.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(324, 23);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Username:";
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(386, 20);
            this.textBoxUsername.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(110, 20);
            this.textBoxUsername.TabIndex = 5;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(14, 57);
            this.buttonConnect.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(94, 29);
            this.buttonConnect.TabIndex = 6;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonJoin
            // 
            this.buttonJoin.Enabled = false;
            this.buttonJoin.Location = new System.Drawing.Point(364, 244);
            this.buttonJoin.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonJoin.Name = "buttonJoin";
            this.buttonJoin.Size = new System.Drawing.Size(94, 29);
            this.buttonJoin.TabIndex = 7;
            this.buttonJoin.Text = "Join Game";
            this.buttonJoin.UseVisualStyleBackColor = true;
            this.buttonJoin.Click += new System.EventHandler(this.buttonJoin_Click);
            // 
            // buttonLeave
            // 
            this.buttonLeave.Enabled = false;
            this.buttonLeave.Location = new System.Drawing.Point(364, 288);
            this.buttonLeave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonLeave.Name = "buttonLeave";
            this.buttonLeave.Size = new System.Drawing.Size(94, 29);
            this.buttonLeave.TabIndex = 8;
            this.buttonLeave.Text = "Leave Lobby";
            this.buttonLeave.UseVisualStyleBackColor = true;
            this.buttonLeave.Click += new System.EventHandler(this.buttonLeave_Click);
            // 
            // logs
            // 
            this.logs.Location = new System.Drawing.Point(78, 98);
            this.logs.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.logs.Name = "logs";
            this.logs.ReadOnly = true;
            this.logs.Size = new System.Drawing.Size(382, 141);
            this.logs.TabIndex = 9;
            this.logs.Text = "";
            // 
            // buttonMove
            // 
            this.buttonMove.Enabled = false;
            this.buttonMove.Location = new System.Drawing.Point(364, 414);
            this.buttonMove.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonMove.Name = "buttonMove";
            this.buttonMove.Size = new System.Drawing.Size(94, 28);
            this.buttonMove.TabIndex = 10;
            this.buttonMove.Text = "Make Move";
            this.buttonMove.UseVisualStyleBackColor = true;
            this.buttonMove.Click += new System.EventHandler(this.buttonMove_Click);
            // 
            // textBoxMove
            // 
            this.textBoxMove.Enabled = false;
            this.textBoxMove.Location = new System.Drawing.Point(78, 419);
            this.textBoxMove.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxMove.Name = "textBoxMove";
            this.textBoxMove.Size = new System.Drawing.Size(283, 20);
            this.textBoxMove.TabIndex = 11;
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Enabled = false;
            this.buttonDisconnect.Location = new System.Drawing.Point(117, 57);
            this.buttonDisconnect.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(94, 29);
            this.buttonDisconnect.TabIndex = 12;
            this.buttonDisconnect.Text = "Disconnect";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.buttonDisconnect_Click);
            // 
            // Game
            // 
            this.Game.Location = new System.Drawing.Point(78, 244);
            this.Game.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Game.Name = "Game";
            this.Game.ReadOnly = true;
            this.Game.Size = new System.Drawing.Size(283, 161);
            this.Game.TabIndex = 13;
            this.Game.Text = "";
            // 
            // labelWin
            // 
            this.labelWin.AutoSize = true;
            this.labelWin.Location = new System.Drawing.Point(364, 331);
            this.labelWin.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelWin.Name = "labelWin";
            this.labelWin.Size = new System.Drawing.Size(38, 13);
            this.labelWin.TabIndex = 14;
            this.labelWin.Text = "Win: 0";
            // 
            // labelLoss
            // 
            this.labelLoss.AutoSize = true;
            this.labelLoss.Location = new System.Drawing.Point(364, 358);
            this.labelLoss.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelLoss.Name = "labelLoss";
            this.labelLoss.Size = new System.Drawing.Size(41, 13);
            this.labelLoss.TabIndex = 15;
            this.labelLoss.Text = "Loss: 0";
            // 
            // labelDraw
            // 
            this.labelDraw.AutoSize = true;
            this.labelDraw.Location = new System.Drawing.Point(364, 390);
            this.labelDraw.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelDraw.Name = "labelDraw";
            this.labelDraw.Size = new System.Drawing.Size(44, 13);
            this.labelDraw.TabIndex = 16;
            this.labelDraw.Text = "Draw: 0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 472);
            this.Controls.Add(this.labelDraw);
            this.Controls.Add(this.labelLoss);
            this.Controls.Add(this.labelWin);
            this.Controls.Add(this.Game);
            this.Controls.Add(this.buttonDisconnect);
            this.Controls.Add(this.textBoxMove);
            this.Controls.Add(this.buttonMove);
            this.Controls.Add(this.logs);
            this.Controls.Add(this.buttonLeave);
            this.Controls.Add(this.buttonJoin);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.textBoxUsername);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonJoin;
        private System.Windows.Forms.Button buttonLeave;
        private System.Windows.Forms.RichTextBox logs;
        private System.Windows.Forms.Button buttonMove;
        private System.Windows.Forms.TextBox textBoxMove;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.RichTextBox Game;
        private System.Windows.Forms.Label labelWin;
        private System.Windows.Forms.Label labelLoss;
        private System.Windows.Forms.Label labelDraw;
    }
}

