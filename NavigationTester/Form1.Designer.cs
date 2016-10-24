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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.baudrateComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comComboBox = new System.Windows.Forms.ComboBox();
            this.serialConnectButton = new System.Windows.Forms.Button();
            this.winConsole = new System.Windows.Forms.RichTextBox();
            this.channelLable1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.channelLable4 = new System.Windows.Forms.Label();
            this.channelLable3 = new System.Windows.Forms.Label();
            this.channelLable2 = new System.Windows.Forms.Label();
            this.channelLable5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.alarmBtnLable = new System.Windows.Forms.Label();
            this.sampleBtnLable = new System.Windows.Forms.Label();
            this.modeBtnLable = new System.Windows.Forms.Label();
            this.decodeIgnoreCountLable = new System.Windows.Forms.Label();
            this.decodeErrorCountLable = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.heartPackgetEnalebox = new System.Windows.Forms.CheckBox();
            this.rNumber = new System.Windows.Forms.Label();
            this.powerNumber = new System.Windows.Forms.NumericUpDown();
            this.menulable = new System.Windows.Forms.Label();
            this.okbtnLable = new System.Windows.Forms.Label();
            this.cancelbtnlable = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.sendSettingbutton = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.idPicNumber = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.channlePicNumber = new System.Windows.Forms.NumericUpDown();
            this.setIdbutton = new System.Windows.Forms.Button();
            this.startAtbutton = new System.Windows.Forms.Button();
            this.getParamButton = new System.Windows.Forms.Button();
            this.exitAtbutton = new System.Windows.Forms.Button();
            this.clearLogbutton = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerNumber)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idPicNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.channlePicNumber)).BeginInit();
            this.groupBox4.SuspendLayout();
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
            this.winConsole.Location = new System.Drawing.Point(10, 396);
            this.winConsole.Name = "winConsole";
            this.winConsole.ReadOnly = true;
            this.winConsole.Size = new System.Drawing.Size(591, 209);
            this.winConsole.TabIndex = 1;
            this.winConsole.Text = "";
            // 
            // channelLable1
            // 
            this.channelLable1.AutoSize = true;
            this.channelLable1.Location = new System.Drawing.Point(10, 123);
            this.channelLable1.Name = "channelLable1";
            this.channelLable1.Size = new System.Drawing.Size(59, 12);
            this.channelLable1.TabIndex = 2;
            this.channelLable1.Text = "Channel1:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(91, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "sample:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 156);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "mode:";
            // 
            // channelLable4
            // 
            this.channelLable4.AutoSize = true;
            this.channelLable4.Location = new System.Drawing.Point(386, 123);
            this.channelLable4.Name = "channelLable4";
            this.channelLable4.Size = new System.Drawing.Size(59, 12);
            this.channelLable4.TabIndex = 5;
            this.channelLable4.Text = "Channel1:";
            // 
            // channelLable3
            // 
            this.channelLable3.AutoSize = true;
            this.channelLable3.Location = new System.Drawing.Point(258, 123);
            this.channelLable3.Name = "channelLable3";
            this.channelLable3.Size = new System.Drawing.Size(59, 12);
            this.channelLable3.TabIndex = 6;
            this.channelLable3.Text = "Channel1:";
            // 
            // channelLable2
            // 
            this.channelLable2.AutoSize = true;
            this.channelLable2.Location = new System.Drawing.Point(142, 123);
            this.channelLable2.Name = "channelLable2";
            this.channelLable2.Size = new System.Drawing.Size(59, 12);
            this.channelLable2.TabIndex = 7;
            this.channelLable2.Text = "Channel1:";
            // 
            // channelLable5
            // 
            this.channelLable5.AutoSize = true;
            this.channelLable5.Location = new System.Drawing.Point(498, 123);
            this.channelLable5.Name = "channelLable5";
            this.channelLable5.Size = new System.Drawing.Size(59, 12);
            this.channelLable5.TabIndex = 8;
            this.channelLable5.Text = "Channel1:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(188, 156);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 9;
            this.label10.Text = "alarm:";
            // 
            // alarmBtnLable
            // 
            this.alarmBtnLable.AutoSize = true;
            this.alarmBtnLable.Location = new System.Drawing.Point(249, 156);
            this.alarmBtnLable.Name = "alarmBtnLable";
            this.alarmBtnLable.Size = new System.Drawing.Size(11, 12);
            this.alarmBtnLable.TabIndex = 10;
            this.alarmBtnLable.Text = "0";
            // 
            // sampleBtnLable
            // 
            this.sampleBtnLable.AutoSize = true;
            this.sampleBtnLable.Location = new System.Drawing.Point(149, 156);
            this.sampleBtnLable.Name = "sampleBtnLable";
            this.sampleBtnLable.Size = new System.Drawing.Size(11, 12);
            this.sampleBtnLable.TabIndex = 11;
            this.sampleBtnLable.Text = "1";
            // 
            // modeBtnLable
            // 
            this.modeBtnLable.AutoSize = true;
            this.modeBtnLable.Location = new System.Drawing.Point(58, 156);
            this.modeBtnLable.Name = "modeBtnLable";
            this.modeBtnLable.Size = new System.Drawing.Size(11, 12);
            this.modeBtnLable.TabIndex = 12;
            this.modeBtnLable.Text = "1";
            // 
            // decodeIgnoreCountLable
            // 
            this.decodeIgnoreCountLable.AutoSize = true;
            this.decodeIgnoreCountLable.Location = new System.Drawing.Point(280, 358);
            this.decodeIgnoreCountLable.Name = "decodeIgnoreCountLable";
            this.decodeIgnoreCountLable.Size = new System.Drawing.Size(11, 12);
            this.decodeIgnoreCountLable.TabIndex = 13;
            this.decodeIgnoreCountLable.Text = "0";
            // 
            // decodeErrorCountLable
            // 
            this.decodeErrorCountLable.AutoSize = true;
            this.decodeErrorCountLable.Location = new System.Drawing.Point(107, 358);
            this.decodeErrorCountLable.Name = "decodeErrorCountLable";
            this.decodeErrorCountLable.Size = new System.Drawing.Size(11, 12);
            this.decodeErrorCountLable.TabIndex = 14;
            this.decodeErrorCountLable.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 358);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "解码出错次数：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(161, 358);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "解码无用数据个数：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.heartPackgetEnalebox);
            this.groupBox2.Controls.Add(this.rNumber);
            this.groupBox2.Controls.Add(this.powerNumber);
            this.groupBox2.Location = new System.Drawing.Point(14, 185);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(225, 59);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "发送心跳包：";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // heartPackgetEnalebox
            // 
            this.heartPackgetEnalebox.AutoSize = true;
            this.heartPackgetEnalebox.Location = new System.Drawing.Point(7, 26);
            this.heartPackgetEnalebox.Name = "heartPackgetEnalebox";
            this.heartPackgetEnalebox.Size = new System.Drawing.Size(48, 16);
            this.heartPackgetEnalebox.TabIndex = 23;
            this.heartPackgetEnalebox.Text = "发送";
            this.heartPackgetEnalebox.UseVisualStyleBackColor = true;
            // 
            // rNumber
            // 
            this.rNumber.AutoSize = true;
            this.rNumber.Location = new System.Drawing.Point(77, 30);
            this.rNumber.Name = "rNumber";
            this.rNumber.Size = new System.Drawing.Size(41, 12);
            this.rNumber.TabIndex = 22;
            this.rNumber.Text = "船电量";
            // 
            // powerNumber
            // 
            this.powerNumber.Location = new System.Drawing.Point(118, 24);
            this.powerNumber.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.powerNumber.Name = "powerNumber";
            this.powerNumber.Size = new System.Drawing.Size(59, 21);
            this.powerNumber.TabIndex = 21;
            // 
            // menulable
            // 
            this.menulable.AutoSize = true;
            this.menulable.Location = new System.Drawing.Point(375, 156);
            this.menulable.Name = "menulable";
            this.menulable.Size = new System.Drawing.Size(11, 12);
            this.menulable.TabIndex = 25;
            this.menulable.Text = "1";
            // 
            // okbtnLable
            // 
            this.okbtnLable.AutoSize = true;
            this.okbtnLable.Location = new System.Drawing.Point(472, 156);
            this.okbtnLable.Name = "okbtnLable";
            this.okbtnLable.Size = new System.Drawing.Size(11, 12);
            this.okbtnLable.TabIndex = 24;
            this.okbtnLable.Text = "1";
            // 
            // cancelbtnlable
            // 
            this.cancelbtnlable.AutoSize = true;
            this.cancelbtnlable.Location = new System.Drawing.Point(580, 156);
            this.cancelbtnlable.Name = "cancelbtnlable";
            this.cancelbtnlable.Size = new System.Drawing.Size(11, 12);
            this.cancelbtnlable.TabIndex = 23;
            this.cancelbtnlable.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(507, 156);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(71, 12);
            this.label13.TabIndex = 22;
            this.label13.Text = "cancel_btn:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(314, 156);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 12);
            this.label14.TabIndex = 21;
            this.label14.Text = "menu_btn";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(421, 156);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(41, 12);
            this.label15.TabIndex = 20;
            this.label15.Text = "ok_btn";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.sendSettingbutton);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.idPicNumber);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.channlePicNumber);
            this.groupBox3.Location = new System.Drawing.Point(14, 250);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(587, 91);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "设置包：";
            // 
            // sendSettingbutton
            // 
            this.sendSettingbutton.Location = new System.Drawing.Point(167, 37);
            this.sendSettingbutton.Name = "sendSettingbutton";
            this.sendSettingbutton.Size = new System.Drawing.Size(120, 23);
            this.sendSettingbutton.TabIndex = 5;
            this.sendSettingbutton.Text = "向遥控器发送设置";
            this.sendSettingbutton.UseVisualStyleBackColor = true;
            this.sendSettingbutton.Click += new System.EventHandler(this.sendSettingbutton_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 61);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 25;
            this.label8.Text = "ID";
            // 
            // idPicNumber
            // 
            this.idPicNumber.Location = new System.Drawing.Point(57, 58);
            this.idPicNumber.Maximum = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.idPicNumber.Minimum = new decimal(new int[] {
            17,
            0,
            0,
            0});
            this.idPicNumber.Name = "idPicNumber";
            this.idPicNumber.Size = new System.Drawing.Size(59, 21);
            this.idPicNumber.TabIndex = 24;
            this.idPicNumber.Value = new decimal(new int[] {
            13106,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label3.Location = new System.Drawing.Point(7, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 22;
            this.label3.Text = "Channel";
            // 
            // channlePicNumber
            // 
            this.channlePicNumber.Location = new System.Drawing.Point(57, 27);
            this.channlePicNumber.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.channlePicNumber.Name = "channlePicNumber";
            this.channlePicNumber.Size = new System.Drawing.Size(41, 21);
            this.channlePicNumber.TabIndex = 21;
            this.channlePicNumber.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // setIdbutton
            // 
            this.setIdbutton.Location = new System.Drawing.Point(79, 47);
            this.setIdbutton.Name = "setIdbutton";
            this.setIdbutton.Size = new System.Drawing.Size(56, 23);
            this.setIdbutton.TabIndex = 26;
            this.setIdbutton.Text = "Set";
            this.setIdbutton.UseVisualStyleBackColor = true;
            this.setIdbutton.Click += new System.EventHandler(this.setIdbutton_Click);
            // 
            // startAtbutton
            // 
            this.startAtbutton.Location = new System.Drawing.Point(8, 26);
            this.startAtbutton.Name = "startAtbutton";
            this.startAtbutton.Size = new System.Drawing.Size(56, 23);
            this.startAtbutton.TabIndex = 27;
            this.startAtbutton.Text = "StartAT";
            this.startAtbutton.UseVisualStyleBackColor = true;
            this.startAtbutton.Click += new System.EventHandler(this.startAtbutton_Click);
            // 
            // getParamButton
            // 
            this.getParamButton.Location = new System.Drawing.Point(79, 9);
            this.getParamButton.Name = "getParamButton";
            this.getParamButton.Size = new System.Drawing.Size(56, 23);
            this.getParamButton.TabIndex = 28;
            this.getParamButton.Text = "Get";
            this.getParamButton.UseVisualStyleBackColor = true;
            this.getParamButton.Click += new System.EventHandler(this.getParamButton_Click);
            // 
            // exitAtbutton
            // 
            this.exitAtbutton.Location = new System.Drawing.Point(157, 27);
            this.exitAtbutton.Name = "exitAtbutton";
            this.exitAtbutton.Size = new System.Drawing.Size(56, 23);
            this.exitAtbutton.TabIndex = 29;
            this.exitAtbutton.Text = "ExitAt";
            this.exitAtbutton.UseVisualStyleBackColor = true;
            this.exitAtbutton.Click += new System.EventHandler(this.exitAtbutton_Click);
            // 
            // clearLogbutton
            // 
            this.clearLogbutton.Location = new System.Drawing.Point(522, 367);
            this.clearLogbutton.Name = "clearLogbutton";
            this.clearLogbutton.Size = new System.Drawing.Size(56, 23);
            this.clearLogbutton.TabIndex = 30;
            this.clearLogbutton.Text = "clear";
            this.clearLogbutton.UseVisualStyleBackColor = true;
            this.clearLogbutton.Click += new System.EventHandler(this.clearLogbutton_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.startAtbutton);
            this.groupBox4.Controls.Add(this.exitAtbutton);
            this.groupBox4.Controls.Add(this.getParamButton);
            this.groupBox4.Controls.Add(this.setIdbutton);
            this.groupBox4.Location = new System.Drawing.Point(333, 11);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(231, 74);
            this.groupBox4.TabIndex = 30;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "设置本机数值";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 623);
            this.Controls.Add(this.clearLogbutton);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.menulable);
            this.Controls.Add(this.okbtnLable);
            this.Controls.Add(this.cancelbtnlable);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.decodeErrorCountLable);
            this.Controls.Add(this.decodeIgnoreCountLable);
            this.Controls.Add(this.modeBtnLable);
            this.Controls.Add(this.sampleBtnLable);
            this.Controls.Add(this.alarmBtnLable);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.channelLable5);
            this.Controls.Add(this.channelLable2);
            this.Controls.Add(this.channelLable3);
            this.Controls.Add(this.channelLable4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.channelLable1);
            this.Controls.Add(this.winConsole);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.powerNumber)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.idPicNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.channlePicNumber)).EndInit();
            this.groupBox4.ResumeLayout(false);
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
        private System.Windows.Forms.Label channelLable1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label channelLable4;
        private System.Windows.Forms.Label channelLable3;
        private System.Windows.Forms.Label channelLable2;
        private System.Windows.Forms.Label channelLable5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label alarmBtnLable;
        private System.Windows.Forms.Label sampleBtnLable;
        private System.Windows.Forms.Label modeBtnLable;
        private System.Windows.Forms.Label decodeIgnoreCountLable;
        private System.Windows.Forms.Label decodeErrorCountLable;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox heartPackgetEnalebox;
        private System.Windows.Forms.Label rNumber;
        private System.Windows.Forms.NumericUpDown powerNumber;
        private System.Windows.Forms.Label menulable;
        private System.Windows.Forms.Label okbtnLable;
        private System.Windows.Forms.Label cancelbtnlable;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button sendSettingbutton;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown idPicNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown channlePicNumber;
        private System.Windows.Forms.Button setIdbutton;
        private System.Windows.Forms.Button exitAtbutton;
        private System.Windows.Forms.Button getParamButton;
        private System.Windows.Forms.Button startAtbutton;
        private System.Windows.Forms.Button clearLogbutton;
        private System.Windows.Forms.GroupBox groupBox4;
    }
}

