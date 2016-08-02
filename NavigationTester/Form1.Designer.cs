namespace NavigationTester
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.baudrateComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comComboBox = new System.Windows.Forms.ComboBox();
            this.serialConnectButton = new System.Windows.Forms.Button();
            this.winConsole = new System.Windows.Forms.RichTextBox();
            this.PitchPanel = new System.Windows.Forms.Panel();
            this.rollPictureBox = new System.Windows.Forms.PictureBox();
            this.YawClearBtn = new System.Windows.Forms.Button();
            this.YawPersenLabel = new System.Windows.Forms.Label();
            this.YawPanel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.pitchlable = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.rolllabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.changeLocalGpsButton = new System.Windows.Forms.Button();
            this.MinDistanceTextBox = new System.Windows.Forms.TextBox();
            this.currentDistanceTextBox = new System.Windows.Forms.TextBox();
            this.MaxDistanceTextBox = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.GpsLattitudeTextBox = new System.Windows.Forms.TextBox();
            this.gpsLongtitudeTextBox = new System.Windows.Forms.TextBox();
            this.localLatTextBox = new System.Windows.Forms.TextBox();
            this.locallongTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.compassReflashLabel = new System.Windows.Forms.Label();
            this.gpsRefLabel = new System.Windows.Forms.Label();
            this.gpsReflashLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rollPictureBox)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.baudrateComboBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.comComboBox);
            this.groupBox1.Controls.Add(this.serialConnectButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(591, 67);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "串口";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "COM";
            // 
            // baudrateComboBox
            // 
            this.baudrateComboBox.FormattingEnabled = true;
            this.baudrateComboBox.Location = new System.Drawing.Point(248, 26);
            this.baudrateComboBox.Name = "baudrateComboBox";
            this.baudrateComboBox.Size = new System.Drawing.Size(121, 20);
            this.baudrateComboBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(201, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "波特率";
            // 
            // comComboBox
            // 
            this.comComboBox.FormattingEnabled = true;
            this.comComboBox.Location = new System.Drawing.Point(47, 26);
            this.comComboBox.Name = "comComboBox";
            this.comComboBox.Size = new System.Drawing.Size(121, 20);
            this.comComboBox.TabIndex = 1;
            // 
            // serialConnectButton
            // 
            this.serialConnectButton.Location = new System.Drawing.Point(463, 24);
            this.serialConnectButton.Name = "serialConnectButton";
            this.serialConnectButton.Size = new System.Drawing.Size(75, 23);
            this.serialConnectButton.TabIndex = 0;
            this.serialConnectButton.Text = "Connect";
            this.serialConnectButton.UseVisualStyleBackColor = true;
            this.serialConnectButton.Click += new System.EventHandler(this.serialConnectButton_Click);
            // 
            // winConsole
            // 
            this.winConsole.Location = new System.Drawing.Point(10, 549);
            this.winConsole.Name = "winConsole";
            this.winConsole.ReadOnly = true;
            this.winConsole.Size = new System.Drawing.Size(591, 24);
            this.winConsole.TabIndex = 1;
            this.winConsole.Text = "";
            // 
            // PitchPanel
            // 
            this.PitchPanel.Location = new System.Drawing.Point(203, 40);
            this.PitchPanel.Name = "PitchPanel";
            this.PitchPanel.Size = new System.Drawing.Size(160, 160);
            this.PitchPanel.TabIndex = 3;
            // 
            // rollPictureBox
            // 
            this.rollPictureBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("rollPictureBox.InitialImage")));
            this.rollPictureBox.Location = new System.Drawing.Point(401, 40);
            this.rollPictureBox.Name = "rollPictureBox";
            this.rollPictureBox.Size = new System.Drawing.Size(160, 160);
            this.rollPictureBox.TabIndex = 4;
            this.rollPictureBox.TabStop = false;
            this.rollPictureBox.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // YawClearBtn
            // 
            this.YawClearBtn.Location = new System.Drawing.Point(43, 214);
            this.YawClearBtn.Name = "YawClearBtn";
            this.YawClearBtn.Size = new System.Drawing.Size(75, 23);
            this.YawClearBtn.TabIndex = 5;
            this.YawClearBtn.Text = "Clear";
            this.YawClearBtn.UseVisualStyleBackColor = true;
            this.YawClearBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // YawPersenLabel
            // 
            this.YawPersenLabel.AutoSize = true;
            this.YawPersenLabel.Location = new System.Drawing.Point(133, 214);
            this.YawPersenLabel.Name = "YawPersenLabel";
            this.YawPersenLabel.Size = new System.Drawing.Size(17, 12);
            this.YawPersenLabel.TabIndex = 6;
            this.YawPersenLabel.Text = "0%";
            this.YawPersenLabel.Click += new System.EventHandler(this.YawPersenLabel_Click);
            // 
            // YawPanel
            // 
            this.YawPanel.Location = new System.Drawing.Point(8, 40);
            this.YawPanel.Name = "YawPanel";
            this.YawPanel.Size = new System.Drawing.Size(160, 160);
            this.YawPanel.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(237, 214);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // pitchlable
            // 
            this.pitchlable.AutoSize = true;
            this.pitchlable.Location = new System.Drawing.Point(318, 214);
            this.pitchlable.Name = "pitchlable";
            this.pitchlable.Size = new System.Drawing.Size(17, 12);
            this.pitchlable.TabIndex = 8;
            this.pitchlable.Text = "0%";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(429, 214);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Clear";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // rolllabel
            // 
            this.rolllabel.AutoSize = true;
            this.rolllabel.Location = new System.Drawing.Point(521, 214);
            this.rolllabel.Name = "rolllabel";
            this.rolllabel.Size = new System.Drawing.Size(17, 12);
            this.rolllabel.TabIndex = 10;
            this.rolllabel.Text = "0%";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(81, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 12);
            this.label5.TabIndex = 11;
            this.label5.Text = "Yaw";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(270, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "Pitch";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(475, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 13;
            this.label7.Text = "Roll";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rollPictureBox);
            this.groupBox2.Controls.Add(this.rolllabel);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.pitchlable);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.PitchPanel);
            this.groupBox2.Controls.Add(this.YawPersenLabel);
            this.groupBox2.Controls.Add(this.YawPanel);
            this.groupBox2.Controls.Add(this.YawClearBtn);
            this.groupBox2.Location = new System.Drawing.Point(12, 95);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(591, 250);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "罗盘";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.changeLocalGpsButton);
            this.groupBox3.Controls.Add(this.MinDistanceTextBox);
            this.groupBox3.Controls.Add(this.currentDistanceTextBox);
            this.groupBox3.Controls.Add(this.MaxDistanceTextBox);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.GpsLattitudeTextBox);
            this.groupBox3.Controls.Add(this.gpsLongtitudeTextBox);
            this.groupBox3.Controls.Add(this.localLatTextBox);
            this.groupBox3.Controls.Add(this.locallongTextBox);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(12, 351);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(591, 143);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Gps";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(477, 103);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 23);
            this.button3.TabIndex = 16;
            this.button3.Text = "重新计算距离";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // changeLocalGpsButton
            // 
            this.changeLocalGpsButton.Location = new System.Drawing.Point(75, 100);
            this.changeLocalGpsButton.Name = "changeLocalGpsButton";
            this.changeLocalGpsButton.Size = new System.Drawing.Size(93, 23);
            this.changeLocalGpsButton.TabIndex = 15;
            this.changeLocalGpsButton.Text = "更改当前位置";
            this.changeLocalGpsButton.UseVisualStyleBackColor = true;
            this.changeLocalGpsButton.Click += new System.EventHandler(this.changeLocalGpsButton_Click);
            // 
            // MinDistanceTextBox
            // 
            this.MinDistanceTextBox.Location = new System.Drawing.Point(458, 75);
            this.MinDistanceTextBox.Name = "MinDistanceTextBox";
            this.MinDistanceTextBox.ReadOnly = true;
            this.MinDistanceTextBox.Size = new System.Drawing.Size(131, 21);
            this.MinDistanceTextBox.TabIndex = 14;
            this.MinDistanceTextBox.Text = "0.0";
            // 
            // currentDistanceTextBox
            // 
            this.currentDistanceTextBox.Location = new System.Drawing.Point(458, 48);
            this.currentDistanceTextBox.Name = "currentDistanceTextBox";
            this.currentDistanceTextBox.ReadOnly = true;
            this.currentDistanceTextBox.Size = new System.Drawing.Size(131, 21);
            this.currentDistanceTextBox.TabIndex = 13;
            this.currentDistanceTextBox.Text = "0.0";
            // 
            // MaxDistanceTextBox
            // 
            this.MaxDistanceTextBox.Location = new System.Drawing.Point(458, 23);
            this.MaxDistanceTextBox.Name = "MaxDistanceTextBox";
            this.MaxDistanceTextBox.ReadOnly = true;
            this.MaxDistanceTextBox.Size = new System.Drawing.Size(131, 21);
            this.MaxDistanceTextBox.TabIndex = 12;
            this.MaxDistanceTextBox.Text = "0.0";
            this.MaxDistanceTextBox.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(411, 78);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 10;
            this.label12.Text = "最小值";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(411, 51);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 9;
            this.label11.Text = "当前值";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(411, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 8;
            this.label10.Text = "最大值";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(282, 105);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 7;
            this.label9.Text = "GPS位置";
            // 
            // GpsLattitudeTextBox
            // 
            this.GpsLattitudeTextBox.Location = new System.Drawing.Point(248, 68);
            this.GpsLattitudeTextBox.Name = "GpsLattitudeTextBox";
            this.GpsLattitudeTextBox.Size = new System.Drawing.Size(131, 21);
            this.GpsLattitudeTextBox.TabIndex = 5;
            this.GpsLattitudeTextBox.Text = "22.289478673938376";
            // 
            // gpsLongtitudeTextBox
            // 
            this.gpsLongtitudeTextBox.Location = new System.Drawing.Point(248, 26);
            this.gpsLongtitudeTextBox.Name = "gpsLongtitudeTextBox";
            this.gpsLongtitudeTextBox.Size = new System.Drawing.Size(131, 21);
            this.gpsLongtitudeTextBox.TabIndex = 4;
            this.gpsLongtitudeTextBox.Text = "113.57701148948458";
            // 
            // localLatTextBox
            // 
            this.localLatTextBox.Location = new System.Drawing.Point(58, 69);
            this.localLatTextBox.Name = "localLatTextBox";
            this.localLatTextBox.Size = new System.Drawing.Size(131, 21);
            this.localLatTextBox.TabIndex = 3;
            this.localLatTextBox.Text = "22.289478673938376";
            this.localLatTextBox.TextChanged += new System.EventHandler(this.LatTextBox_TextChanged);
            // 
            // locallongTextBox
            // 
            this.locallongTextBox.Location = new System.Drawing.Point(58, 29);
            this.locallongTextBox.Name = "locallongTextBox";
            this.locallongTextBox.Size = new System.Drawing.Size(131, 21);
            this.locallongTextBox.TabIndex = 2;
            this.locallongTextBox.Text = "113.57701148948458";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "北纬";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "东经";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(10, 507);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(89, 12);
            this.label14.TabIndex = 16;
            this.label14.Text = "罗盘更新频率：";
            // 
            // compassReflashLabel
            // 
            this.compassReflashLabel.AutoSize = true;
            this.compassReflashLabel.Location = new System.Drawing.Point(120, 507);
            this.compassReflashLabel.Name = "compassReflashLabel";
            this.compassReflashLabel.Size = new System.Drawing.Size(23, 12);
            this.compassReflashLabel.TabIndex = 17;
            this.compassReflashLabel.Text = "0Hz";
            // 
            // gpsRefLabel
            // 
            this.gpsRefLabel.AutoSize = true;
            this.gpsRefLabel.Location = new System.Drawing.Point(258, 507);
            this.gpsRefLabel.Name = "gpsRefLabel";
            this.gpsRefLabel.Size = new System.Drawing.Size(83, 12);
            this.gpsRefLabel.TabIndex = 18;
            this.gpsRefLabel.Text = "GPS更新频率：";
            // 
            // gpsReflashLabel
            // 
            this.gpsReflashLabel.AutoSize = true;
            this.gpsReflashLabel.Location = new System.Drawing.Point(368, 507);
            this.gpsReflashLabel.Name = "gpsReflashLabel";
            this.gpsReflashLabel.Size = new System.Drawing.Size(23, 12);
            this.gpsReflashLabel.TabIndex = 19;
            this.gpsReflashLabel.Text = "0Hz";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 585);
            this.Controls.Add(this.gpsReflashLabel);
            this.Controls.Add(this.gpsRefLabel);
            this.Controls.Add(this.compassReflashLabel);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.winConsole);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rollPictureBox)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox baudrateComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comComboBox;
        private System.Windows.Forms.Button serialConnectButton;
        private System.Windows.Forms.RichTextBox winConsole;
        private System.Windows.Forms.Panel PitchPanel;
        private System.Windows.Forms.PictureBox rollPictureBox;
        private System.Windows.Forms.Button YawClearBtn;
        private System.Windows.Forms.Label YawPersenLabel;
        private System.Windows.Forms.Panel YawPanel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label pitchlable;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label rolllabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox MinDistanceTextBox;
        private System.Windows.Forms.TextBox currentDistanceTextBox;
        private System.Windows.Forms.TextBox MaxDistanceTextBox;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox GpsLattitudeTextBox;
        private System.Windows.Forms.TextBox gpsLongtitudeTextBox;
        private System.Windows.Forms.TextBox localLatTextBox;
        private System.Windows.Forms.TextBox locallongTextBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label compassReflashLabel;
        private System.Windows.Forms.Label gpsRefLabel;
        private System.Windows.Forms.Label gpsReflashLabel;
        private System.Windows.Forms.Button changeLocalGpsButton;
        private System.Windows.Forms.Button button3;
    }
}

