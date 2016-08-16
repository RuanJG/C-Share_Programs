namespace WindowsFormsApplication1
{
    partial class mainForm
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
            this.startButton = new System.Windows.Forms.Button();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.comSelectComboBox = new System.Windows.Forms.ComboBox();
            this.rateComboBox = new System.Windows.Forms.ComboBox();
            this.sendTextBox = new System.Windows.Forms.TextBox();
            this.chooseFileButton = new System.Windows.Forms.Button();
            this.binFilePathTextBox = new System.Windows.Forms.TextBox();
            this.ProgramButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tcpConnectButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tcpIpAdressTextBox = new System.Windows.Forms.TextBox();
            this.tcpPortTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(329, 28);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(85, 23);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Open";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // logTextBox
            // 
            this.logTextBox.Location = new System.Drawing.Point(438, 41);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.logTextBox.Size = new System.Drawing.Size(463, 439);
            this.logTextBox.TabIndex = 2;
            this.logTextBox.TextChanged += new System.EventHandler(this.logTextBox_TextChanged);
            // 
            // comSelectComboBox
            // 
            this.comSelectComboBox.FormattingEnabled = true;
            this.comSelectComboBox.Location = new System.Drawing.Point(49, 28);
            this.comSelectComboBox.Name = "comSelectComboBox";
            this.comSelectComboBox.Size = new System.Drawing.Size(78, 20);
            this.comSelectComboBox.TabIndex = 3;
            this.comSelectComboBox.SelectedIndexChanged += new System.EventHandler(this.comSelectComboBox_SelectedIndexChanged);
            // 
            // rateComboBox
            // 
            this.rateComboBox.FormattingEnabled = true;
            this.rateComboBox.Location = new System.Drawing.Point(206, 28);
            this.rateComboBox.Name = "rateComboBox";
            this.rateComboBox.Size = new System.Drawing.Size(106, 20);
            this.rateComboBox.TabIndex = 4;
            this.rateComboBox.SelectedIndexChanged += new System.EventHandler(this.rateComboBox_SelectedIndexChanged);
            // 
            // sendTextBox
            // 
            this.sendTextBox.Location = new System.Drawing.Point(16, 30);
            this.sendTextBox.Name = "sendTextBox";
            this.sendTextBox.Size = new System.Drawing.Size(318, 21);
            this.sendTextBox.TabIndex = 5;
            this.sendTextBox.TextChanged += new System.EventHandler(this.sendTextBox_TextChanged);
            // 
            // chooseFileButton
            // 
            this.chooseFileButton.Location = new System.Drawing.Point(329, 24);
            this.chooseFileButton.Name = "chooseFileButton";
            this.chooseFileButton.Size = new System.Drawing.Size(85, 23);
            this.chooseFileButton.TabIndex = 7;
            this.chooseFileButton.Text = "选择bin文件";
            this.chooseFileButton.UseVisualStyleBackColor = true;
            this.chooseFileButton.Click += new System.EventHandler(this.chooseFileButton_Click);
            // 
            // binFilePathTextBox
            // 
            this.binFilePathTextBox.Location = new System.Drawing.Point(16, 44);
            this.binFilePathTextBox.Name = "binFilePathTextBox";
            this.binFilePathTextBox.Size = new System.Drawing.Size(296, 21);
            this.binFilePathTextBox.TabIndex = 8;
            // 
            // ProgramButton
            // 
            this.ProgramButton.Location = new System.Drawing.Point(329, 60);
            this.ProgramButton.Name = "ProgramButton";
            this.ProgramButton.Size = new System.Drawing.Size(85, 23);
            this.ProgramButton.TabIndex = 9;
            this.ProgramButton.Text = "开始烧写";
            this.ProgramButton.UseVisualStyleBackColor = true;
            this.ProgramButton.Click += new System.EventHandler(this.ProgramButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "串口";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(159, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "波特率";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comSelectComboBox);
            this.groupBox1.Controls.Add(this.rateComboBox);
            this.groupBox1.Controls.Add(this.startButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(420, 69);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "uart";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.chooseFileButton);
            this.groupBox2.Controls.Add(this.binFilePathTextBox);
            this.groupBox2.Controls.Add(this.ProgramButton);
            this.groupBox2.Location = new System.Drawing.Point(12, 233);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(420, 93);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "烧写";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "文件路径";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(834, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "clean";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(345, 30);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "send";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.sendTextBox);
            this.groupBox3.Location = new System.Drawing.Point(12, 367);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(420, 78);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "发送";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tcpConnectButton);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.tcpIpAdressTextBox);
            this.groupBox4.Controls.Add(this.tcpPortTextBox);
            this.groupBox4.Location = new System.Drawing.Point(12, 113);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(420, 92);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Tcp";
            // 
            // tcpConnectButton
            // 
            this.tcpConnectButton.Location = new System.Drawing.Point(329, 48);
            this.tcpConnectButton.Name = "tcpConnectButton";
            this.tcpConnectButton.Size = new System.Drawing.Size(85, 23);
            this.tcpConnectButton.TabIndex = 12;
            this.tcpConnectButton.Text = "Connect";
            this.tcpConnectButton.UseVisualStyleBackColor = true;
            this.tcpConnectButton.Click += new System.EventHandler(this.tcpConnectButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(219, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "Port";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "IP Address";
            // 
            // tcpIpAdressTextBox
            // 
            this.tcpIpAdressTextBox.Location = new System.Drawing.Point(8, 48);
            this.tcpIpAdressTextBox.Name = "tcpIpAdressTextBox";
            this.tcpIpAdressTextBox.Size = new System.Drawing.Size(173, 21);
            this.tcpIpAdressTextBox.TabIndex = 1;
            this.tcpIpAdressTextBox.Text = "192.168.127.254";
            // 
            // tcpPortTextBox
            // 
            this.tcpPortTextBox.Location = new System.Drawing.Point(221, 48);
            this.tcpPortTextBox.Name = "tcpPortTextBox";
            this.tcpPortTextBox.Size = new System.Drawing.Size(100, 21);
            this.tcpPortTextBox.TabIndex = 0;
            this.tcpPortTextBox.Text = "4001";
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 480);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.logTextBox);
            this.Name = "mainForm";
            this.Text = "IAP烧写程序";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.TextBox logTextBox;
        private System.Windows.Forms.ComboBox comSelectComboBox;
        private System.Windows.Forms.ComboBox rateComboBox;
        private System.Windows.Forms.TextBox sendTextBox;
        private System.Windows.Forms.Button chooseFileButton;
        private System.Windows.Forms.TextBox binFilePathTextBox;
        private System.Windows.Forms.Button ProgramButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button tcpConnectButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tcpIpAdressTextBox;
        private System.Windows.Forms.TextBox tcpPortTextBox;
    }
}

