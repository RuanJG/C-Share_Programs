using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
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
        private void  thread_log(string str)
        {
            Invoke((MethodInvoker)delegate
            {
                logTextBox.AppendText(str);
            });
        }



        //id
        byte PACKGET_START_ID = 1; //[id start]data{[0]}
        byte PACKGET_ACK_ID = 2 ;//[id ack]data{[ACK_OK/ACK_FALSE/ACK/RESTART][error code]}
        byte PACKGET_DATA_ID= 3 ;//[id data] data{[seq][]...[]}
        byte PACKGET_END_ID =4; //[id end] data{[stop/jump]}
        //data
        byte PACKGET_ACK_NONE = 0;
        byte PACKGET_ACK_OK =1;
        byte PACKGET_ACK_FALSE =2;
        byte PACKGET_ACK_RESTART= 3;
        byte PACKGET_END_JUMP= 1;
        byte PACKGET_END_STOP =2;
        byte PACKGET_MAX_DATA_SEQ= 200 ; // new_seq = (last_seq+1)% PACKGET_MAX_DATA_SEQ
        // ack error code
        byte PACKGET_ACK_FALSE_PROGRAM_ERROR = 21;// the send data process stop
        byte PACKGET_ACK_FALSE_ERASE_ERROR =22; // the send data process stop
        byte PACKGET_ACK_FALSE_SEQ_FALSE = 23; // the send data process will restart
        // if has ack false , the flash process shuld be restart


        void cleanSerialData()
        {
            try
            {
                int len = serial.BytesToRead;
                byte[] RecieveBuf = new byte[len];
                serial.Read(RecieveBuf, 0, len);
            }
            catch
            {

            }
        }

        int myEncoderSendCallback(byte c)
        {
            try
            {
                byte[] data = new byte[1];
                data[0] = c;
                serial.Write(data, 0, 1);
                return 1;
            }
            catch
            {
                MessageBox.Show("serial send error");
            }
            return 0;
        }

        void sendPackget(byte p_id, byte[] data, int len)
        {
            cmdCoder encoder = new cmdCoder(p_id, myEncoderSendCallback);
            encoder.cmdcoder_send_bytes(data, len);
        }

        bool sendEndStopPackget()
        {
            cmdCoder encoder = new cmdCoder(PACKGET_END_ID, myEncoderSendCallback);
            byte[] data = new byte[2];
            data[0] = PACKGET_END_STOP;
            encoder.cmdcoder_send_bytes(data, 1);
            if (getAckResult(1000) == PACKGET_ACK_OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        bool sendEndJumpPackget()
        {
            cmdCoder encoder = new cmdCoder(PACKGET_END_ID, myEncoderSendCallback);
            byte[] data = new byte[2];
            data[0] = PACKGET_END_JUMP;
            encoder.cmdcoder_send_bytes(data, 1);

            if (getAckResult(1000) == PACKGET_ACK_OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        cmdCoder mDecoder = new cmdCoder(0, null);
        byte[] getAckPackget(int timeoutMs)
        {
            byte[] res = new byte[2];
            try{
                int retry = 0;
                while (retry++ < timeoutMs)
                {
                    if (serial.BytesToRead > 0)
                    {
                        int len = serial.BytesToRead;
                        byte[] RecieveBuf = new byte[len];
                        serial.Read(RecieveBuf, 0, len);
                        for (int i = 0; i < len; i++)
                        {
                            if (mDecoder.cmdcoder_Parse_byte(RecieveBuf[i]) == 1)
                            {
                                if (mDecoder.id == PACKGET_ACK_ID)
                                {
                                    res[0] = mDecoder.data[0];
                                    res[1] = mDecoder.data[1];
                                    return res;
                                }
                            }
                        }
                    }
                    Thread.Sleep(1);
                }
                
                return null;
            }catch
            {

            }
            return null;
        }
        byte getAckResult(int timeoutMs)
        {
            //return this value
             /*    byte PACKGET_ACK_OK =1;
                    byte PACKGET_ACK_FALSE =2;
                    byte PACKGET_ACK_RESTART= 3;
             *     byte PACKGET_ACK_FALSE_PROGRAM_ERROR =1 ;// the send data process stop
                byte PACKGET_ACK_FALSE_ERASE_ERROR =2; // the send data process stop
                    byte PACKGET_ACK_FALSE_SEQ_FALSE = 3; // the send data process will restart
             */
            byte[] res;
            res = getAckPackget(timeoutMs);
            if (res != null)
            {
                if (res[0] == PACKGET_ACK_FALSE)
                {
                    thread_log("false ack: ");
                    if( res[1] == PACKGET_ACK_FALSE_PROGRAM_ERROR)
                        thread_log("PACKGET_ACK_FALSE_PROGRAM_ERROR\r\n");
                    else if (res[1] == PACKGET_ACK_FALSE_ERASE_ERROR)
                        thread_log("PACKGET_ACK_FALSE_ERASE_ERROR\r\n");
                    else if (res[1] == PACKGET_ACK_FALSE_SEQ_FALSE)
                        thread_log("PACKGET_ACK_FALSE_SEQ_FALSE\r\n");
                    else
                        thread_log("UNknow error \r\n");
                    return res[1];
                }
                else
                {
                    return res[0];
                }
            }
            return PACKGET_ACK_NONE;
        }

        
        bool sendStartAndWaitAck()
        {
            thread_log("send Start packget and wait ack...\r\n");
            //cmdCoder encoder = new cmdCoder(PACKGET_START_ID, myEncoderSendCallback);
            byte[] data = new byte[2];

            cleanSerialData();
            while (!thread_need_quit)
            {
                sendPackget(PACKGET_START_ID, data, 1);
                byte res = getAckResult(500);
                if (res == PACKGET_ACK_OK)
                {
                    thread_log("get ack ok\r\n");
                    return true;
                }else if(res == PACKGET_ACK_FALSE_ERASE_ERROR || res == PACKGET_ACK_FALSE_PROGRAM_ERROR ){
                    thread_log("may be main program set tag error\r\n");
                }
                thread_log(".");
            }
            return false;
        }

        bool sendDataPackget(byte[]sendData, int len)
        {
            byte ack;
            int retry = 5;
            bool res = true;

            while (retry-- > 0)
            {
                cleanSerialData();
                sendPackget(PACKGET_DATA_ID, sendData, len);
                ack = getAckResult(1000);
                if (ack == PACKGET_ACK_OK)
                {
                    res = true;
                    break;
                }
                else if (ack == PACKGET_ACK_NONE)
                {// may be decode error or timeout, so resend
                    thread_log("send data packget timeout\r\n");
                }
                else
                {
                    // false or restart
                    thread_log("send data packget error\r\n");
                    res = false;
                    break;
                }
            }
            return res;
        }
        bool sendDataToDataPackget(byte[] data, int len)
        {
            //each 100byte a packget ;  [seq][data0]...[data99]
            byte seq = 0;
            int PackgetDataSize = 100; 
            int PackgetSize = 100 +1 ;//1 is seq; 
            byte[] sendData = new byte[PackgetSize];

            int packgetCount = len/PackgetDataSize;
            int packgetLess = len%PackgetDataSize;

            float sendSize = 0;

            int i;
            for ( i= 0; i < packgetCount; i++)
            {
                Array.Copy(data, i * PackgetDataSize, sendData, 1, PackgetDataSize);
                sendData[0] = (byte)((++seq) % PACKGET_MAX_DATA_SEQ);

                if (sendDataPackget(sendData, PackgetSize))
                {
                    sendSize += PackgetDataSize;
                    thread_log("send " + sendSize.ToString() + "/" + len.ToString() + "\r\n");
                }
                else
                {
                    thread_log("send data packget failed\r\n");
                    return false;
                }
            }
            if (packgetLess > 0)
            {
                Array.Copy(data, packgetCount * PackgetDataSize, sendData, 1, packgetLess);
                sendData[0] = (byte)((++seq) % PACKGET_MAX_DATA_SEQ);

                if (sendDataPackget(sendData, packgetLess + 1))
                {
                    sendSize += (packgetLess + 1);
                    thread_log("send " + sendSize.ToString() + "/" + len.ToString() + "\r\n");
                }
                else
                {
                    thread_log("send data packget failed\r\n");
                    return false;
                }
            }
            
            return true;
        }

        bool sendBinFile()
        {

            string binfileName = binFilePathTextBox.Text;
            FileStream binFile = null;
            byte[] data;

            binFile = new FileStream(binfileName, FileMode.Open);
            if (binFile == null || binFile.Length <= 0) return false;

            data = new byte[binFile.Length];
            //fs.Seek(0, SeekOrigin.Begin);
            int len = binFile.Read(data, 0, data.Length);
            binFile.Close();

            if (len != data.Length)
            {
                thread_log("read file data false\r\n");
                return false;
            }

            bool res = true;
            if ( ! sendDataToDataPackget(data, data.Length))
            {
                thread_log("send file data false\r\n");
                res =  false;
            }

            //import to send end packget even false send data
            int retry = 0;
            while (!sendEndStopPackget())
            {
                if (retry++ > 5)
                {
                    thread_log("stm32 no response...\r\n");
                    break;
                }
            }

            //need to jump app if send ok
            if (res)
            {
                while (!sendEndJumpPackget())
                {
                    if (retry++ > 5)
                    {
                        thread_log("stm32 no response to jump \r\n");
                        break;
                    }
                }
            }

            return res;
        }


        bool thread_need_quit = false;
        private  void thread_com_read()
        {

            while (!thread_need_quit)
            {
                if (!sendStartAndWaitAck())
                    break;

                thread_log("start send bin file \r\n");
                if (sendBinFile())
                {
                    thread_log("send bin file ok \r\n");
                    break;
                }
                break;
                //start program bin
                
                //Thread.Sleep(1000);
            }
            Invoke((MethodInvoker)delegate
            {
                ProgramButton.Enabled = true;
            });
            

        }
        
        //##############################################################  UI fucntion

        private void startButton_Click(object sender, EventArgs e)
        {
            if (serial.IsOpen){
                thread_need_quit = true;
                if( com_read_thread != null )
                    com_read_thread.Abort();
                ProgramButton.Enabled = true;
                serial.Close();
                startButton.Text = "Open";
                log("serial Close\n");
            }else{
                serial.Open();
                startButton.Text = "Close";
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

        private void chooseFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.InitialDirectory = "C://";

            fileDialog.Filter = "bin files (*.bin)|*.bin|All files (*.*)|*.*";

            fileDialog.FilterIndex = 1;

            fileDialog.RestoreDirectory = true;

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                binFilePathTextBox.Text = fileDialog.FileName;
            }

        }

        private void ProgramButton_Click(object sender, EventArgs e)
        {
            string binfileName = binFilePathTextBox.Text;
            if (binfileName == null || !File.Exists(binfileName))
            {
                MessageBox.Show("先选择可用的Bin文件 !!!");
                return;
            }
            if (!serial.IsOpen)
            {
                MessageBox.Show("先打开串口!!!");
                return;
            }
            ProgramButton.Enabled = false;

            thread_need_quit = false;
            com_read_thread = new Thread(new ThreadStart(thread_com_read));
            com_read_thread.IsBackground = true;
            com_read_thread.Start();
           
        }










    }
}
