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
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.button19 = new System.Windows.Forms.Button();
            this.button20 = new System.Windows.Forms.Button();
            this.button21 = new System.Windows.Forms.Button();
            this.button22 = new System.Windows.Forms.Button();
            this.button23 = new System.Windows.Forms.Button();
            this.button24 = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button18 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.filtTtextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.a485AddrTextBox = new System.Windows.Forms.TextBox();
            this.a485NumberTextBox = new System.Windows.Forms.TextBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.damDelayMsTextBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.button25 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.damCloseButton = new System.Windows.Forms.Button();
            this.DamOpenButton = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox10.SuspendLayout();
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
            this.logTextBox.Size = new System.Drawing.Size(463, 447);
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
            this.sendTextBox.Location = new System.Drawing.Point(8, 20);
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
            this.button2.Location = new System.Drawing.Point(339, 20);
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
            this.groupBox3.Location = new System.Drawing.Point(12, 342);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(420, 57);
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
            this.tcpIpAdressTextBox.Text = "192.168.1.254";
            // 
            // tcpPortTextBox
            // 
            this.tcpPortTextBox.Location = new System.Drawing.Point(221, 48);
            this.tcpPortTextBox.Name = "tcpPortTextBox";
            this.tcpPortTextBox.Size = new System.Drawing.Size(100, 21);
            this.tcpPortTextBox.TabIndex = 0;
            this.tcpPortTextBox.Text = "4001";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(8, 20);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(35, 23);
            this.button3.TabIndex = 16;
            this.button3.Text = "开";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.button5);
            this.groupBox6.Controls.Add(this.button4);
            this.groupBox6.Controls.Add(this.button3);
            this.groupBox6.Location = new System.Drawing.Point(907, 37);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(181, 44);
            this.groupBox6.TabIndex = 19;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "发动机";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(60, 20);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(35, 23);
            this.button5.TabIndex = 18;
            this.button5.Text = "关";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(111, 20);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(43, 23);
            this.button4.TabIndex = 17;
            this.button4.Text = "打火";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.button6);
            this.groupBox7.Controls.Add(this.button8);
            this.groupBox7.Location = new System.Drawing.Point(907, 87);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(181, 44);
            this.groupBox7.TabIndex = 20;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "发电机";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(60, 20);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(35, 23);
            this.button6.TabIndex = 18;
            this.button6.Text = "关";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(8, 20);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(35, 23);
            this.button8.TabIndex = 16;
            this.button8.Text = "开";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.groupBox5);
            this.groupBox8.Controls.Add(this.groupBox9);
            this.groupBox8.Controls.Add(this.button10);
            this.groupBox8.Controls.Add(this.button12);
            this.groupBox8.Controls.Add(this.button9);
            this.groupBox8.Controls.Add(this.button11);
            this.groupBox8.Location = new System.Drawing.Point(907, 137);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(207, 351);
            this.groupBox8.TabIndex = 21;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "侧推";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.button19);
            this.groupBox5.Controls.Add(this.button20);
            this.groupBox5.Controls.Add(this.button21);
            this.groupBox5.Controls.Add(this.button22);
            this.groupBox5.Controls.Add(this.button23);
            this.groupBox5.Controls.Add(this.button24);
            this.groupBox5.Location = new System.Drawing.Point(8, 205);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(186, 106);
            this.groupBox5.TabIndex = 29;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "右控制";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 80);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 28;
            this.label8.Text = "后退";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 27;
            this.label9.Text = "前进";
            // 
            // button19
            // 
            this.button19.Location = new System.Drawing.Point(123, 78);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(55, 23);
            this.button19.TabIndex = 26;
            this.button19.Text = "后开";
            this.button19.UseVisualStyleBackColor = true;
            this.button19.Click += new System.EventHandler(this.button19_Click);
            // 
            // button20
            // 
            this.button20.Location = new System.Drawing.Point(123, 49);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(55, 23);
            this.button20.TabIndex = 25;
            this.button20.Text = "前开";
            this.button20.UseVisualStyleBackColor = true;
            this.button20.Click += new System.EventHandler(this.button20_Click);
            // 
            // button21
            // 
            this.button21.Location = new System.Drawing.Point(53, 78);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(55, 23);
            this.button21.TabIndex = 24;
            this.button21.Text = "前关";
            this.button21.UseVisualStyleBackColor = true;
            this.button21.Click += new System.EventHandler(this.button21_Click);
            // 
            // button22
            // 
            this.button22.Location = new System.Drawing.Point(53, 49);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(55, 23);
            this.button22.TabIndex = 23;
            this.button22.Text = "后关";
            this.button22.UseVisualStyleBackColor = true;
            this.button22.Click += new System.EventHandler(this.button22_Click);
            // 
            // button23
            // 
            this.button23.Location = new System.Drawing.Point(53, 20);
            this.button23.Name = "button23";
            this.button23.Size = new System.Drawing.Size(55, 23);
            this.button23.TabIndex = 22;
            this.button23.Text = "开";
            this.button23.UseVisualStyleBackColor = true;
            this.button23.Click += new System.EventHandler(this.button23_Click);
            // 
            // button24
            // 
            this.button24.Location = new System.Drawing.Point(123, 20);
            this.button24.Name = "button24";
            this.button24.Size = new System.Drawing.Size(55, 23);
            this.button24.TabIndex = 21;
            this.button24.Text = "关";
            this.button24.UseVisualStyleBackColor = true;
            this.button24.Click += new System.EventHandler(this.button24_Click);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.label7);
            this.groupBox9.Controls.Add(this.label6);
            this.groupBox9.Controls.Add(this.button18);
            this.groupBox9.Controls.Add(this.button17);
            this.groupBox9.Controls.Add(this.button16);
            this.groupBox9.Controls.Add(this.button15);
            this.groupBox9.Controls.Add(this.button14);
            this.groupBox9.Controls.Add(this.button13);
            this.groupBox9.Location = new System.Drawing.Point(8, 83);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(186, 106);
            this.groupBox9.TabIndex = 23;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "左控制";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 28;
            this.label7.Text = "后退";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 27;
            this.label6.Text = "前进";
            // 
            // button18
            // 
            this.button18.Location = new System.Drawing.Point(123, 78);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(55, 23);
            this.button18.TabIndex = 26;
            this.button18.Text = "后开";
            this.button18.UseVisualStyleBackColor = true;
            this.button18.Click += new System.EventHandler(this.button18_Click);
            // 
            // button17
            // 
            this.button17.Location = new System.Drawing.Point(123, 49);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(55, 23);
            this.button17.TabIndex = 25;
            this.button17.Text = "前开";
            this.button17.UseVisualStyleBackColor = true;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // button16
            // 
            this.button16.Location = new System.Drawing.Point(53, 78);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(55, 23);
            this.button16.TabIndex = 24;
            this.button16.Text = "前关";
            this.button16.UseVisualStyleBackColor = true;
            this.button16.Click += new System.EventHandler(this.button16_Click);
            // 
            // button15
            // 
            this.button15.Location = new System.Drawing.Point(53, 49);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(55, 23);
            this.button15.TabIndex = 23;
            this.button15.Text = "后关";
            this.button15.UseVisualStyleBackColor = true;
            this.button15.Click += new System.EventHandler(this.button15_Click);
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(53, 20);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(55, 23);
            this.button14.TabIndex = 22;
            this.button14.Text = "开";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(123, 20);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(55, 23);
            this.button13.TabIndex = 21;
            this.button13.Text = "关";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(8, 49);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(53, 23);
            this.button10.TabIndex = 20;
            this.button10.Text = "关充电";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(6, 20);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(55, 23);
            this.button12.TabIndex = 19;
            this.button12.Text = "充电";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(69, 49);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(58, 23);
            this.button9.TabIndex = 18;
            this.button9.Text = "关电";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(68, 20);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(59, 23);
            this.button11.TabIndex = 16;
            this.button11.Text = "上电";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // filtTtextBox
            // 
            this.filtTtextBox.Location = new System.Drawing.Point(509, 12);
            this.filtTtextBox.Name = "filtTtextBox";
            this.filtTtextBox.Size = new System.Drawing.Size(143, 21);
            this.filtTtextBox.TabIndex = 22;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(438, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 12;
            this.label10.Text = "匹配字符串";
            // 
            // a485AddrTextBox
            // 
            this.a485AddrTextBox.Location = new System.Drawing.Point(91, 23);
            this.a485AddrTextBox.Name = "a485AddrTextBox";
            this.a485AddrTextBox.Size = new System.Drawing.Size(21, 21);
            this.a485AddrTextBox.TabIndex = 13;
            this.a485AddrTextBox.Text = "5";
            // 
            // a485NumberTextBox
            // 
            this.a485NumberTextBox.Location = new System.Drawing.Point(146, 23);
            this.a485NumberTextBox.Name = "a485NumberTextBox";
            this.a485NumberTextBox.Size = new System.Drawing.Size(35, 21);
            this.a485NumberTextBox.TabIndex = 23;
            this.a485NumberTextBox.Text = "3";
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.label15);
            this.groupBox10.Controls.Add(this.damDelayMsTextBox);
            this.groupBox10.Controls.Add(this.label14);
            this.groupBox10.Controls.Add(this.button25);
            this.groupBox10.Controls.Add(this.button7);
            this.groupBox10.Controls.Add(this.damCloseButton);
            this.groupBox10.Controls.Add(this.DamOpenButton);
            this.groupBox10.Controls.Add(this.label13);
            this.groupBox10.Controls.Add(this.label12);
            this.groupBox10.Controls.Add(this.label11);
            this.groupBox10.Controls.Add(this.a485AddrTextBox);
            this.groupBox10.Controls.Add(this.a485NumberTextBox);
            this.groupBox10.Location = new System.Drawing.Point(12, 405);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(420, 83);
            this.groupBox10.TabIndex = 25;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "继电器开关";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(183, 59);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(17, 12);
            this.label15.TabIndex = 34;
            this.label15.Text = "ms";
            // 
            // damDelayMsTextBox
            // 
            this.damDelayMsTextBox.Location = new System.Drawing.Point(146, 54);
            this.damDelayMsTextBox.Name = "damDelayMsTextBox";
            this.damDelayMsTextBox.Size = new System.Drawing.Size(35, 21);
            this.damDelayMsTextBox.TabIndex = 33;
            this.damDelayMsTextBox.Text = "1000";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(14, 57);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(113, 12);
            this.label14.TabIndex = 32;
            this.label14.Text = "闪开/闪闭     间隔";
            // 
            // button25
            // 
            this.button25.Location = new System.Drawing.Point(329, 54);
            this.button25.Name = "button25";
            this.button25.Size = new System.Drawing.Size(59, 23);
            this.button25.TabIndex = 31;
            this.button25.Text = "闪开";
            this.button25.UseVisualStyleBackColor = true;
            this.button25.Click += new System.EventHandler(this.button25_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(221, 54);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(61, 23);
            this.button7.TabIndex = 30;
            this.button7.Text = "闪闭";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // damCloseButton
            // 
            this.damCloseButton.Location = new System.Drawing.Point(329, 21);
            this.damCloseButton.Name = "damCloseButton";
            this.damCloseButton.Size = new System.Drawing.Size(59, 23);
            this.damCloseButton.TabIndex = 29;
            this.damCloseButton.Text = "断开";
            this.damCloseButton.UseVisualStyleBackColor = true;
            this.damCloseButton.Click += new System.EventHandler(this.damCloseButton_Click);
            // 
            // DamOpenButton
            // 
            this.DamOpenButton.Location = new System.Drawing.Point(221, 21);
            this.DamOpenButton.Name = "DamOpenButton";
            this.DamOpenButton.Size = new System.Drawing.Size(61, 23);
            this.DamOpenButton.TabIndex = 28;
            this.DamOpenButton.Text = "连接";
            this.DamOpenButton.UseVisualStyleBackColor = true;
            this.DamOpenButton.Click += new System.EventHandler(this.DamOpenButton_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(187, 26);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(17, 12);
            this.label13.TabIndex = 27;
            this.label13.Text = "路";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(126, 26);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 12);
            this.label12.TabIndex = 26;
            this.label12.Text = "第";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(14, 26);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 12);
            this.label11.TabIndex = 25;
            this.label11.Text = "485地址：0x0";
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1133, 492);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.filtTtextBox);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
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
            this.groupBox6.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
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
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button button16;
        private System.Windows.Forms.Button button15;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button18;
        private System.Windows.Forms.Button button17;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button19;
        private System.Windows.Forms.Button button20;
        private System.Windows.Forms.Button button21;
        private System.Windows.Forms.Button button22;
        private System.Windows.Forms.Button button23;
        private System.Windows.Forms.Button button24;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox filtTtextBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox a485AddrTextBox;
        private System.Windows.Forms.TextBox a485NumberTextBox;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Button damCloseButton;
        private System.Windows.Forms.Button DamOpenButton;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button25;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox damDelayMsTextBox;
        private System.Windows.Forms.Label label14;
    }
}

