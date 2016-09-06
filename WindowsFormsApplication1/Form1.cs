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

using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Collections;

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
        Thread iap_thread ;
      

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
            if(comSelectComboBox.Items.Count > 0)
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
            if( !consoleStopTag )
                logTextBox.AppendText(str);
        }
        private void  thread_log(string str)
        {
            if (!consoleStopTag)
            {
                Invoke((MethodInvoker)delegate
                {
                    logTextBox.AppendText(str);
                });
            }
            
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




        int myUartEncoderSendCallback(byte c)
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

        int myTcpEncoderSendCallback(byte c)
        {
            try
            {
                if (iapTcpClient!= null && iapTcpClient.Connected)
                {
                    NetworkStream sendStream = iapTcpClient.GetStream();
                    sendStream.WriteByte(c);
                    sendStream.Flush();
                }
                
            }
            catch
            {
                MessageBox.Show("tcp send error");
            }
            return 0;
        }


        void cleanIapBufferData()
        {
            try
            {
                if (serial.IsOpen)
                {
                    int len = serial.BytesToRead;
                    byte[] RecieveBuf = new byte[len];
                    serial.Read(RecieveBuf, 0, len);
                }
                
            }
            catch
            {

            }

            try
            {
                if (iapTcpClient != null && iapTcpClient.Connected)
                {
                    NetworkStream stream = iapTcpClient.GetStream();
                    if (stream.DataAvailable)
                    {
                        int len;
                        byte[] buffer = new byte[1024];
                        len = stream.Read(buffer, 0, buffer.Length);
                    }
                }

            }
            catch
            {

            }
        }
        void sendPackget(byte p_id, byte[] data, int len)
        {
            cmdCoder encoder;
            if (serial.IsOpen)
            {
                encoder = new cmdCoder(p_id, myUartEncoderSendCallback);
            }
            else if (iapTcpClient != null && iapTcpClient.Connected)
            {
                encoder = new cmdCoder(p_id, myTcpEncoderSendCallback);
            }
            else
            {
                MessageBox.Show("send packget set path error");
                return;
            }
            encoder.cmdcoder_send_bytes(data, len);
        }
        byte[] getBytesFromUart()
        {
            try
            {
                if (serial.BytesToRead > 0)
                {
                    int len = serial.BytesToRead;
                    byte[] RecieveBuf = new byte[len];
                    serial.Read(RecieveBuf, 0, len);
                    return RecieveBuf;
                }
            }
            catch
            {
                thread_log("read serial error");
            }

            return null;
        }
        byte[] getBytesFromTcp()
        {
            try
            {
                if (iapTcpClient != null && iapTcpClient.Connected)
                {
                    NetworkStream stream = iapTcpClient.GetStream();
                    if (stream.DataAvailable)
                    {
                        int len;
                        byte[] buffer = new byte[1024];
                        len = stream.Read(buffer, 0, buffer.Length);
                        if( len > 0){
                            byte[] res = new byte[len];
                            Array.Copy(buffer, res, len);
                            return res;
                        }
                    }
                }
            }
            catch
            {
                thread_log("read tcp  error");
            }
            return null;

        }
        byte[] getBytesFromRemote()
        {
            if (serial.IsOpen)
            {
                return getBytesFromUart();
            }
            if (iapTcpClient != null && iapTcpClient.Connected)
            {
                return getBytesFromTcp();
            }
            return null;
        }

        bool sendEndStopPackget()
        {
            byte[] data = new byte[2];
            data[0] = PACKGET_END_STOP;
            sendPackget(PACKGET_END_ID, data, 1);
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
            byte[] data = new byte[2];
            data[0] = PACKGET_END_JUMP;
            sendPackget(PACKGET_END_ID, data, 1);

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

                    byte[] RecieveBuf = getBytesFromRemote();
                    if (RecieveBuf != null)
                    {
                        for (int i = 0; i < RecieveBuf.Length; i++)
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

            cleanIapBufferData();
            while (!iap_thread_need_quit)
            {
                sendPackget(PACKGET_START_ID, data, 1);
                byte res = getAckResult(100);
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
                cleanIapBufferData();
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







        bool iap_thread_need_quit = false;
        private  void thread_iap()
        {

            while (!iap_thread_need_quit)
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
            start_log_thread();

        }
        void start_iap_thread()
        {
            iap_thread_need_quit = false;
            iap_thread = new Thread(new ThreadStart(thread_iap));
            iap_thread.IsBackground = true;
            iap_thread.Start();
        }
        void stop_iap_thread()
        {
            iap_thread_need_quit = true;
            if (iap_thread != null)
            {
                iap_thread.Abort();
                iap_thread = null;
            }
        }







        bool log_thread_need_quit = false;
        Thread log_read_thread;
        string last_str="";
        private void thread_recive_log()
        {
            while ( ! log_thread_need_quit)
            {
                try
                {
                    byte[] RecieveBuf = getBytesFromRemote();
                    string str="";

                    if (RecieveBuf != null)
                        str = System.Text.Encoding.UTF8.GetString(RecieveBuf);
                    str = last_str + str;
                    last_str = "";

                    if( str.Length > 0)
                    {
                        writeLogFile(str);

                        if (filtTtextBox.Text != null)
                        {
                            string filt = filtTtextBox.Text;
                            string spiltstr = "\r\n";
                            int filtindex = str.IndexOf(spiltstr);
                            if( filtindex >= 0 )
                            {
                                string alinestr = str.Substring(0,filtindex+2);
                                if (0 <= alinestr.IndexOf(filt))
                                        thread_log(alinestr);
                                if( str.Length > (filtindex+2) )
                                    last_str = str.Substring(filtindex+2);
                                
                            }else{
                                last_str = str;
                            }

                        }
                        else
                        {
                            thread_log(str);
                            /*
                            if (0 <= str.IndexOf("speed"))
                            {
                                Invoke((MethodInvoker)delegate
                                {
                                    speepTtextBox.Text = str;
                                });
                            }*/
                        }    
                    }
                }
                catch
                {
                    thread_log("serial read error in log thread\r\n");
                    //break;
                }
                Thread.Sleep(10);
            }
        }

        FileStream logFileStream=null;
        string logfilename;
        private void openLogFile()
        {
            logfilename = ".\\iapwriterLog_";
            DateTime date=System.DateTime.Now;

            logfilename += date.ToString("yyyyMMddHHmmss");
            logfilename += ".txt";

            try
            {
                if (logFileStream != null)
                {
                    logFileStream.Close();
                    logFileStream = null;
                }
                logFileStream = new FileStream(logfilename, FileMode.OpenOrCreate);
                if (logFileStream != null)
                {
                    log("open log file " + logfilename);
                }
            }
            catch
            {

            }
            
        }
        private void closeLogFile()
        {
            if (logFileStream != null)
            {
                logFileStream.Close();
                logFileStream = null;
            }
            log("close log file " + logfilename);
        }
        private void writeLogFile(string str)
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(str);
            if (logFileStream != null)
            {
                try
                {
                    logFileStream.Seek(0, SeekOrigin.End);
                    logFileStream.Write(data, 0, data.Length);
                    //logFileStream.Close();
                }
                catch
                {

                }
                
            }
            
        }
        private void start_log_thread()
        {
            openLogFile();
            log_thread_need_quit = false;
            log_read_thread = new Thread(new ThreadStart(thread_recive_log));
            log_read_thread.IsBackground = true;
            log_read_thread.Start();


        }
        private void stop_log_thread()
        {
            log_thread_need_quit = true;
            Thread.Sleep(100);
            if (null != log_read_thread)
            {
                log_read_thread.Abort();
                log_read_thread = null;
            }
            closeLogFile();
        }






        //#######################  tcp 


        private TcpClient iapTcpClient = null;
        private bool iapTcpThreadNeedQuit = false;


        bool isIapTcpConnected()
        {
            if (iapTcpClient != null && iapTcpClient.Connected)
                return true;
            return false;
        }
        public void startConnectIapTcp()
        {
            string ip;
            int port;

            if (tcpIpAdressTextBox.Text == null || tcpPortTextBox.Text == null)
            {
                MessageBox.Show("Set Right ip and port first");
                return;
            }
            ip = tcpIpAdressTextBox.Text;
            port = int.Parse(tcpPortTextBox.Text);
            
            try
            {
                iapTcpClient = new TcpClient(ip, port); // maybe block
                start_log_thread();
                tcpConnectButton.Text = "Close";
                log("TCP 连接成功\r\n");
            }
            catch
            {
                MessageBox.Show("Tcp connect Error\r\n");
            }
        }
        void stopConnectIapTcp()
        {
            if (iapTcpClient != null && iapTcpClient.Connected)
            {
                stop_iap_thread();
                stop_log_thread();
                iapTcpClient.Close();

                ProgramButton.Enabled = true;
                tcpConnectButton.Text = "Connect";
                log("Tcp Close\r\n");
            }
        }
        
        //##############################################################  UI fucntion

        private void startButton_Click(object sender, EventArgs e)
        {
            if (serial.IsOpen){
                stop_iap_thread();
                stop_log_thread();
                serial.Close();

                ProgramButton.Enabled = true;
                startButton.Text = "Open";
                log("serial Close\n");
            }else{
                serial.Open();
                start_log_thread();

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
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void sendTextBox_TextChanged(object sender, EventArgs e)
        {
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
            if (!serial.IsOpen && iapTcpClient != null && !iapTcpClient.Connected)
            {
                MessageBox.Show("先打开串口 或者 TCP 连接 !!!");
                return;
            }
            ProgramButton.Enabled = false;

            stop_log_thread();

            start_iap_thread();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            logTextBox.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //send text to remote 
            string cmd = sendTextBox.Text;
            byte[] data = System.Text.Encoding.UTF8.GetBytes(cmd);

           // for (int i = 0; i < data.Length; i++) logTextBox.AppendText("0x"+Convert.ToString(data[i], 16)+" ");
            //sendTextBox.Clear();
            if (serial.IsOpen)
            {
                try
                {
                    serial.Write(cmd);
                    log("send >>" + cmd+"\r\n");
                }
                catch
                {

                }
                
            }
            if (iapTcpClient != null && iapTcpClient.Connected)
            {
                try
                {
                    if (iapTcpClient != null && iapTcpClient.Connected)
                    {
                        NetworkStream sendStream = iapTcpClient.GetStream();
                        sendStream.Write(data, 0, data.Length);
                        sendStream.Flush();
                    }
                    log("send >>" + cmd + "\r\n");
                }
                catch
                {

                }
            }
        }

        private void tcpConnectButton_Click(object sender, EventArgs e)
        {
            try{
                if (iapTcpClient != null && iapTcpClient.Connected)
                    stopConnectIapTcp();
                else
                    startConnectIapTcp();
            }catch{

            }
            
        }

        private void sendCmdToRemote(string cmd)
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(cmd);
            log("send >>" + cmd + "\r\n");
            if (serial.IsOpen)
            {
                try
                {
                    serial.Write(cmd);
                    log("uart send >>" + cmd + "\r\n");
                    
                }
                catch
                {

                }

            }
            if (iapTcpClient != null && iapTcpClient.Connected)
            {
                try
                {
                    if (iapTcpClient != null && iapTcpClient.Connected)
                    {
                        NetworkStream sendStream = iapTcpClient.GetStream();
                        sendStream.Write(data, 0, data.Length);
                        sendStream.Flush();
                        log("tcp send >>" + cmd + "\r\n");
                    }
                }
                catch
                {

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //发动机开
            sendCmdToRemote("#5#241");

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //发动机关
            sendCmdToRemote("#5#240");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //发动机打火
            sendCmdToRemote("#5#23305");//500ms
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //发电机开
            sendCmdToRemote("#5#22330");//3s
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //发电机关
            sendCmdToRemote("#5#21310");//1s
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //侧推充电
            sendCmdToRemote("#5#421");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //侧推上电
            sendCmdToRemote("#5#411");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //侧推关充电
            sendCmdToRemote("#5#420");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //侧推关电
            sendCmdToRemote("#5#410");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            //侧推左控制开
            sendCmdToRemote("#5#431");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            //侧推左控制关
            sendCmdToRemote("#5#430");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            //侧推左控制前开
            sendCmdToRemote("#5#511");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //侧推左控制后关
            sendCmdToRemote("#5#520");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            //侧推左控制前关
            sendCmdToRemote("#5#510");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            //侧推左控制后开
            sendCmdToRemote("#5#521");
        }

        private void button23_Click(object sender, EventArgs e)
        {
            //侧推右控制开
            sendCmdToRemote("#5#441");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            //侧推右控制关
            sendCmdToRemote("#5#440");
        }

        private void button22_Click(object sender, EventArgs e)
        {
            //侧推右控制后关
            sendCmdToRemote("#5#540");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            //侧推右控制前开
            sendCmdToRemote("#5#531");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            //侧推右控制前关
            sendCmdToRemote("#5#530");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            //侧推右控制后开
            sendCmdToRemote("#5#541");
        }

        private void DamOpenButton_Click(object sender, EventArgs e)
        {
            string addr = a485AddrTextBox.Text;
            string num = a485NumberTextBox.Text;
            if (addr == null || num == null)
            {
                MessageBox.Show("先设置好继电进地址与开关号");
                return;
            }


            sendCmdToRemote("#5#"+addr+num+"1");

        }

        private void damCloseButton_Click(object sender, EventArgs e)
        {
            string addr = a485AddrTextBox.Text;
            string num = a485NumberTextBox.Text;
            if (addr == null || num == null)
            {
                MessageBox.Show("先设置好继电进地址与开关号");
                return;
            }

            sendCmdToRemote("#5#" + addr + num + "0");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string addr = a485AddrTextBox.Text;
            string num = a485NumberTextBox.Text;
            if (addr == null || num == null)
            {
                MessageBox.Show("先设置好继电进地址与开关号");
                return;
            }
            string ms = damDelayMsTextBox.Text;
            if (ms == null)
            {
                MessageBox.Show("设置闪开闪闭的时间间隔");
                return;
            }

            int ims = int.Parse(ms);
            if (ims < 100 || (ims % 100) != 0)
            {
                MessageBox.Show("时间间隔应大于100ms，并且为100ms整倍数");
                return;
            }
            string flash_ms = (ims / 100).ToString("D2") ;
            //string flash_ms = (ims / 1000).ToString("D1");
            sendCmdToRemote("#5#" + addr + num + "3"+flash_ms);

        }

        private void button25_Click(object sender, EventArgs e)
        {
            string addr = a485AddrTextBox.Text;
            string num = a485NumberTextBox.Text;
            if (addr == null || num == null)
            {
                MessageBox.Show("先设置好继电进地址与开关号");
                return;
            }
            string ms = damDelayMsTextBox.Text;
            if (ms == null)
            {
                MessageBox.Show("设置闪开闪闭的时间间隔");
                return;
            }

            int ims = int.Parse(ms);
            if (ims < 100 || (ims % 100) != 0)
            {
                MessageBox.Show("时间间隔应大于100ms，并且为100ms整倍数");
                return;
            }
            string flash_ms = (ims / 100).ToString("D2");
            //string flash_ms = (ims / 1000).ToString("D1");
            sendCmdToRemote("#5#" + addr + num + "2" + flash_ms);
        }

        private void a485AddrTextBox_TextChanged(object sender, EventArgs e)
        {

        }


        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pitchUpButton_Click(object sender, EventArgs e)
        {
            sendCmdToRemote("#8#f");
        }

        private void pitchMiddleButton_Click(object sender, EventArgs e)
        {
            sendCmdToRemote("#8#m");
        }

        private void pitchDownButton_Click(object sender, EventArgs e)
        {
            sendCmdToRemote("#8#b");
        }

        private void yawManualModecheckBox_CheckedChanged_1(object sender, EventArgs e)
        {
            //进入手动模式
            if (yawManualModecheckBox.Checked){
                sendCmdToRemote("#10#1");
                yawTurnLeftbutton.Enabled = true;
                yawTurnRightButton.Enabled = true;
                yawTurnStopButton.Enabled = true;
            }else{
                sendCmdToRemote("#10#0");
                yawTurnLeftbutton.Enabled = false;
                yawTurnRightButton.Enabled = false;
                yawTurnStopButton.Enabled = false;
            }
        }

        private void yawLogcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            //打开yaw log
            if (yawLogcheckBox.Checked)
                sendCmdToRemote("#11#1");
            else
                sendCmdToRemote("#11#0");
        }

        private void yawTurnLeftbutton_Click_1(object sender, EventArgs e)
        {
            if (yawManualModecheckBox.Checked)
            {
                sendCmdToRemote("#3#l");
            }
        }

        private void yawTurnStopButton_Click_1(object sender, EventArgs e)
        {
            if (yawManualModecheckBox.Checked)
            {
                sendCmdToRemote("#3#s");
            }
        }

        private void yawTurnRightButton_Click(object sender, EventArgs e)
        {
            if (yawManualModecheckBox.Checked)
            {
                sendCmdToRemote("#3#r");
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {//打开激光雷达
            sendCmdToRemote("#5#861");
        }

        private void button28_Click(object sender, EventArgs e)
        {
            sendCmdToRemote("#5#860");
        }

        private void button26_Click(object sender, EventArgs e)
        {
            //转向助力泵
            sendCmdToRemote("#5#871");
        }

        private void button29_Click(object sender, EventArgs e)
        {
            sendCmdToRemote("#5#870");
        }

        private void button30_Click(object sender, EventArgs e)
        {
            //ke4
            sendCmdToRemote("#5#881");
        }

        private void button31_Click(object sender, EventArgs e)
        {
            sendCmdToRemote("#5#880");
        }

        private void button32_Click(object sender, EventArgs e)
        {
            //翻斗
            sendCmdToRemote("#5#891");
        }

        private void button33_Click(object sender, EventArgs e)
        {
            sendCmdToRemote("#5#890");
        }

        private void button34_Click(object sender, EventArgs e)
        {
            //前舱风机
            sendCmdToRemote("#5#8a1");
        }

        private void button35_Click(object sender, EventArgs e)
        {
            sendCmdToRemote("#5#8a0");
        }

        private void button36_Click(object sender, EventArgs e)
        {
            //后舱风机
            sendCmdToRemote("#5#8b1");
        }

        private void button37_Click(object sender, EventArgs e)
        {
            sendCmdToRemote("#5#8b0");
        }

        private void button38_Click(object sender, EventArgs e)
        {
            //舱底泵
            sendCmdToRemote("#5#8c1");
        }

        private void button39_Click(object sender, EventArgs e)
        {
            sendCmdToRemote("#5#8c0");
        }

        private void button40_Click(object sender, EventArgs e)
        {
            //4G雷达
            sendCmdToRemote("#5#8d1");
        }

        private void button41_Click(object sender, EventArgs e)
        {
            sendCmdToRemote("#5#8d0");
        }

        private void button46_Click(object sender, EventArgs e)
        {
            //camera
            sendCmdToRemote("#5#8e1");
        }

        private void button47_Click(object sender, EventArgs e)
        {
            sendCmdToRemote("#5#8e0");
        }

        private void button42_Click(object sender, EventArgs e)
        {
            //switch
            sendCmdToRemote("#5#8f1");
        }

        private void button43_Click(object sender, EventArgs e)
        {
            sendCmdToRemote("#5#8f0");
        }

        private void button44_Click(object sender, EventArgs e)
        {
            //gps
            sendCmdToRemote("#5#8g1");
        }

        private void button45_Click(object sender, EventArgs e)
        {
            sendCmdToRemote("#5#8g0");
        }

        bool consoleStopTag = false;
        private void button48_Click(object sender, EventArgs e)
        {
            if (consoleStopTag)
            {
                button48.Text = "Stop";
                consoleStopTag = false;
            }
            else
            {
                button48.Text = "Start";
                consoleStopTag = true;
            }
            
        }



    }
}
