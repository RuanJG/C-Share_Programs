using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;//用于启用线程类；
using System.IO.Ports;//用于调用串口类函数


/*
public string iPort = "com1"; //默认为串口1
public int iRate = 9600; //波特率1200,2400,4800,9600
public byte bSize = 8; //8 bits
public int iTimeout = 1000; //延时时长
public SerialPort serialPort1 = new SerialPort();//定义一个串口类的串口变量
string serialReadString; //用于串口接收数据
public bool IsCirlce;//判断是否选用循环发送数据
public Thread Thd_Send; //开辟一个专用于发送数据的线程        
public byte[] recb;  //用于存放接收数据的数组
*/



namespace WindowsFormsApplication1
{
    public partial class mainForm : Form
    {

        int count=0;
        SerialPort serial = new SerialPort();
        Thread com_read_thread ;

        private delegate void DelegateComRead();

        public mainForm()
        {
            InitializeComponent();
            comRate_Init();
            com_Init();
        }

        private void comRate_Init()
        {
            rateComboBox.Items.Add("115200");
            rateComboBox.Items.Add("100000");
            rateComboBox.Items.Add("57600");
            rateComboBox.Items.Add("9600");
            rateComboBox.SelectedIndex = 0;
        }
        private void com_Init()
        {
            //comSelectComboBox.Items.Add("NONE");
            comSelectComboBox.Items.Clear();
            com_Load();
            comSelectComboBox.SelectedIndex = 0;

            serial.DataBits = 8;
            serial.StopBits = StopBits.One;
            serial.Parity = Parity.None;
            serial.ReadTimeout = 1000;//500ms
       
        }
        private void com_Load()
        {
            string[] ports = SerialPort.GetPortNames();
            foreach (string s in ports)
            {
                //cmb_port.Items.Add(s);
                comSelectComboBox.Items.Add(s);
            }
        }
        private void log(string str)
        {
            logTextBox.AppendText(str);
        }
        private void ComRead()
        {
            ////第三种发送接收方式
            //byte[] SendBuf = new byte[256];
            //SendBuf = System.Text.Encoding.UTF8.GetBytes(txtSend.Text);
            //int len = txtSend.Text.Length;
            //sp.Write(SendBuf, 0, len);

            //byte[] RecieveBuf = new byte[256];
            //sp.Read(RecieveBuf, 0, 256);
            //string strRecieve3 = System.Text.Encoding.UTF8.GetString(RecieveBuf);
            //txtRecieve.Text = strRecieve3;
            while (true)
            {
                Thread.Sleep(500);
                logTextBox.AppendText("sdf");
            }

        }
        private  void thread_com_read()
        {
            byte[] RecieveBuf = new byte[256];
            int len;

            while (true)
            {
                Thread.Sleep(10);
                Array.Clear(RecieveBuf,0,RecieveBuf.Length);
                try
                {
                    len = serial.Read(RecieveBuf, 0, RecieveBuf.Length-1);
                    logTextBox.BeginInvoke(new MethodInvoker(delegate { log(System.Text.Encoding.UTF8.GetString(RecieveBuf)); }));
                }
                catch
                {
                    logTextBox.BeginInvoke(new MethodInvoker(delegate { log("get a catch\n"); }));
                }
            }
        }
        
        //##############################################################  UI fucntion

        private void startButton_Click(object sender, EventArgs e)
        {
            if (serial.IsOpen){
                serial.Close();
                com_read_thread.Abort();
                startButton.Text = "Start";
                log("serial Close\n");
            }else{
                serial.Open();
                com_read_thread = new Thread(new ThreadStart(thread_com_read));
                com_read_thread.IsBackground = true;
                com_read_thread.Start();
                startButton.Text = "Stop";
                log("serial Starting\n");
            }
            
        }
        private void comSelectComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            serial.PortName = comSelectComboBox.Items[comSelectComboBox.SelectedIndex].ToString();
            log("com select "+serial.PortName+","+serial.BaudRate+"\n");
           
        }
        private void rateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            serial.BaudRate = int.Parse(rateComboBox.Items[rateComboBox.SelectedIndex].ToString());
            log("com select " + serial.PortName + "," + serial.BaudRate + "\n");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void logTextBox_TextChanged(object sender, EventArgs e)
        {
            //log("log text changed\n");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void sendTextBox_TextChanged(object sender, EventArgs e)
        {
            string cmd;
            if (sendTextBox.TextLength > 1 && sendTextBox.Text[sendTextBox.TextLength - 2].Equals('\\') && sendTextBox.Text[sendTextBox.TextLength - 1].Equals('n'))
            {
                cmd = sendTextBox.Text;
                sendTextBox.Clear();
                cmd = cmd.Remove(cmd.Length - 2);
                if (serial.IsOpen)
                {
                    serial.Write(cmd);
                    log("send cmd: " + cmd);
                }
            }
        }









    }
}
