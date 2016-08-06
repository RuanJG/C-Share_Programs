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
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.comSelectComboBox = new System.Windows.Forms.ComboBox();
            this.rateComboBox = new System.Windows.Forms.ComboBox();
            this.sendTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chooseFileButton = new System.Windows.Forms.Button();
            this.binFilePathTextBox = new System.Windows.Forms.TextBox();
            this.ProgramButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(334, 57);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(40, 24);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(78, 16);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "checkBox1";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // logTextBox
            // 
            this.logTextBox.Location = new System.Drawing.Point(40, 216);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.logTextBox.Size = new System.Drawing.Size(385, 107);
            this.logTextBox.TabIndex = 2;
            this.logTextBox.TextChanged += new System.EventHandler(this.logTextBox_TextChanged);
            // 
            // comSelectComboBox
            // 
            this.comSelectComboBox.FormattingEnabled = true;
            this.comSelectComboBox.Location = new System.Drawing.Point(40, 60);
            this.comSelectComboBox.Name = "comSelectComboBox";
            this.comSelectComboBox.Size = new System.Drawing.Size(78, 20);
            this.comSelectComboBox.TabIndex = 3;
            this.comSelectComboBox.SelectedIndexChanged += new System.EventHandler(this.comSelectComboBox_SelectedIndexChanged);
            // 
            // rateComboBox
            // 
            this.rateComboBox.FormattingEnabled = true;
            this.rateComboBox.Location = new System.Drawing.Point(141, 60);
            this.rateComboBox.Name = "rateComboBox";
            this.rateComboBox.Size = new System.Drawing.Size(121, 20);
            this.rateComboBox.TabIndex = 4;
            this.rateComboBox.SelectedIndexChanged += new System.EventHandler(this.rateComboBox_SelectedIndexChanged);
            // 
            // sendTextBox
            // 
            this.sendTextBox.Location = new System.Drawing.Point(40, 352);
            this.sendTextBox.Name = "sendTextBox";
            this.sendTextBox.Size = new System.Drawing.Size(385, 21);
            this.sendTextBox.TabIndex = 5;
            this.sendTextBox.TextChanged += new System.EventHandler(this.sendTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 330);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "send text :";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // chooseFileButton
            // 
            this.chooseFileButton.Location = new System.Drawing.Point(40, 121);
            this.chooseFileButton.Name = "chooseFileButton";
            this.chooseFileButton.Size = new System.Drawing.Size(85, 23);
            this.chooseFileButton.TabIndex = 7;
            this.chooseFileButton.Text = "选择bin文件";
            this.chooseFileButton.UseVisualStyleBackColor = true;
            this.chooseFileButton.Click += new System.EventHandler(this.chooseFileButton_Click);
            // 
            // binFilePathTextBox
            // 
            this.binFilePathTextBox.Location = new System.Drawing.Point(141, 121);
            this.binFilePathTextBox.Name = "binFilePathTextBox";
            this.binFilePathTextBox.Size = new System.Drawing.Size(268, 21);
            this.binFilePathTextBox.TabIndex = 8;
            // 
            // ProgramButton
            // 
            this.ProgramButton.Location = new System.Drawing.Point(42, 151);
            this.ProgramButton.Name = "ProgramButton";
            this.ProgramButton.Size = new System.Drawing.Size(75, 23);
            this.ProgramButton.TabIndex = 9;
            this.ProgramButton.Text = "开始烧写";
            this.ProgramButton.UseVisualStyleBackColor = true;
            this.ProgramButton.Click += new System.EventHandler(this.ProgramButton_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 410);
            this.Controls.Add(this.ProgramButton);
            this.Controls.Add(this.binFilePathTextBox);
            this.Controls.Add(this.chooseFileButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sendTextBox);
            this.Controls.Add(this.rateComboBox);
            this.Controls.Add(this.comSelectComboBox);
            this.Controls.Add(this.logTextBox);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.startButton);
            this.Name = "mainForm";
            this.Text = "mainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox logTextBox;
        private System.Windows.Forms.ComboBox comSelectComboBox;
        private System.Windows.Forms.ComboBox rateComboBox;
        private System.Windows.Forms.TextBox sendTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button chooseFileButton;
        private System.Windows.Forms.TextBox binFilePathTextBox;
        private System.Windows.Forms.Button ProgramButton;
    }
}

