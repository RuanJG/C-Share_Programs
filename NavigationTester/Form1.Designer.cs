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
            this.modeDataNumber = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.statusNumber = new System.Windows.Forms.NumericUpDown();
            this.rNumber = new System.Windows.Forms.Label();
            this.powerNumber = new System.Windows.Forms.NumericUpDown();
            this.heartPackgetEnalebox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.modeDataNumber)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.powerNumber)).BeginInit();
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
            this.winConsole.Location = new System.Drawing.Point(10, 290);
            this.winConsole.Name = "winConsole";
            this.winConsole.ReadOnly = true;
            this.winConsole.Size = new System.Drawing.Size(591, 315);
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
            this.label4.Location = new System.Drawing.Point(107, 156);
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
            this.label10.Location = new System.Drawing.Point(250, 156);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 9;
            this.label10.Text = "alarm:";
            // 
            // alarmBtnLable
            // 
            this.alarmBtnLable.AutoSize = true;
            this.alarmBtnLable.Location = new System.Drawing.Point(322, 156);
            this.alarmBtnLable.Name = "alarmBtnLable";
            this.alarmBtnLable.Size = new System.Drawing.Size(11, 12);
            this.alarmBtnLable.TabIndex = 10;
            this.alarmBtnLable.Text = "0";
            // 
            // sampleBtnLable
            // 
            this.sampleBtnLable.AutoSize = true;
            this.sampleBtnLable.Location = new System.Drawing.Point(178, 156);
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
            this.decodeIgnoreCountLable.Location = new System.Drawing.Point(280, 248);
            this.decodeIgnoreCountLable.Name = "decodeIgnoreCountLable";
            this.decodeIgnoreCountLable.Size = new System.Drawing.Size(11, 12);
            this.decodeIgnoreCountLable.TabIndex = 13;
            this.decodeIgnoreCountLable.Text = "0";
            // 
            // decodeErrorCountLable
            // 
            this.decodeErrorCountLable.AutoSize = true;
            this.decodeErrorCountLable.Location = new System.Drawing.Point(107, 248);
            this.decodeErrorCountLable.Name = "decodeErrorCountLable";
            this.decodeErrorCountLable.Size = new System.Drawing.Size(11, 12);
            this.decodeErrorCountLable.TabIndex = 14;
            this.decodeErrorCountLable.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 248);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 15;
            this.label7.Text = "解码出错次数：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(161, 248);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "解码无用数据个数：";
            // 
            // modeDataNumber
            // 
            this.modeDataNumber.Location = new System.Drawing.Point(47, 20);
            this.modeDataNumber.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.modeDataNumber.Name = "modeDataNumber";
            this.modeDataNumber.Size = new System.Drawing.Size(59, 21);
            this.modeDataNumber.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 18;
            this.label3.Text = "mode:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.heartPackgetEnalebox);
            this.groupBox2.Controls.Add(this.rNumber);
            this.groupBox2.Controls.Add(this.powerNumber);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.statusNumber);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.modeDataNumber);
            this.groupBox2.Location = new System.Drawing.Point(14, 185);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(587, 50);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "发送心跳包：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(160, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 20;
            this.label8.Text = "状态";
            // 
            // statusNumber
            // 
            this.statusNumber.Location = new System.Drawing.Point(201, 20);
            this.statusNumber.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.statusNumber.Name = "statusNumber";
            this.statusNumber.Size = new System.Drawing.Size(59, 21);
            this.statusNumber.TabIndex = 19;
            // 
            // rNumber
            // 
            this.rNumber.AutoSize = true;
            this.rNumber.Location = new System.Drawing.Point(311, 26);
            this.rNumber.Name = "rNumber";
            this.rNumber.Size = new System.Drawing.Size(41, 12);
            this.rNumber.TabIndex = 22;
            this.rNumber.Text = "船电量";
            // 
            // powerNumber
            // 
            this.powerNumber.Location = new System.Drawing.Point(352, 20);
            this.powerNumber.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.powerNumber.Name = "powerNumber";
            this.powerNumber.Size = new System.Drawing.Size(59, 21);
            this.powerNumber.TabIndex = 21;
            // 
            // heartPackgetEnalebox
            // 
            this.heartPackgetEnalebox.AutoSize = true;
            this.heartPackgetEnalebox.Location = new System.Drawing.Point(486, 20);
            this.heartPackgetEnalebox.Name = "heartPackgetEnalebox";
            this.heartPackgetEnalebox.Size = new System.Drawing.Size(48, 16);
            this.heartPackgetEnalebox.TabIndex = 23;
            this.heartPackgetEnalebox.Text = "发送";
            this.heartPackgetEnalebox.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(618, 623);
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.modeDataNumber)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.powerNumber)).EndInit();
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
        private System.Windows.Forms.NumericUpDown modeDataNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox heartPackgetEnalebox;
        private System.Windows.Forms.Label rNumber;
        private System.Windows.Forms.NumericUpDown powerNumber;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown statusNumber;
    }
}

