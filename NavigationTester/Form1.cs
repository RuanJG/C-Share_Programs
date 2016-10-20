using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;//用于调用串口类函数


namespace NavigationTester
{
    public partial class Form1 : Form
    {
        private SerialPort mSerialPort;
        private cmdCoder mDecoder;
        private cmdCoder mEncoder;
        System.Timers.Timer mTimer = new System.Timers.Timer(500); //设置时间间隔为0.5秒
        SerialDataReceivedEventHandler serialReciver;


        public Form1()
        {
            InitializeComponent();
            myInit();
        }
        
        private void myInit()
        {
            comRate_Init();
            com_Init();
            mDecoder = new cmdCoder(4, null);
            mEncoder = new cmdCoder(0, cmdcoderSendCallback);
            serialReciver = new SerialDataReceivedEventHandler(serialPort_DataReceived);
            TimerIint();
        }
        private void comRate_Init()
        {
            baudrateComboBox.Items.Add("115200");
            baudrateComboBox.Items.Add("100000");
            baudrateComboBox.Items.Add("57600");
            baudrateComboBox.Items.Add("9600");
            if (baudrateComboBox.Items.Count > 0)
                baudrateComboBox.SelectedIndex = 0;
        }
        private void com_Init()
        {
            //comSelectComboBox.Items.Add("NONE");
            comComboBox.Items.Clear();
            string[] ports = SerialPort.GetPortNames();
            foreach (string s in ports)
            {
                //cmb_port.Items.Add(s);
                comComboBox.Items.Add(s);
            }
            if( comComboBox.Items.Count > 0 )
                comComboBox.SelectedIndex = 0;
        }
        private void TimerStart()
        {
            mTimer.Enabled = true; //是否触发Elapsed事件
            mTimer.Start();
        }
        private void TimerStop()
        {
            mTimer.Stop();
        }
        private void TimerIint()
        {
            mTimer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_TimesUp);
            mTimer.AutoReset = true; //每到指定时间Elapsed事件是触发一次（false），还是一直触发（true）
        }
        private void Timer_TimesUp(object sender, System.Timers.ElapsedEventArgs e)
        {

            Invoke((MethodInvoker)delegate
            {
                decodeErrorCountLable.Text = mDecoder.DecodeErrorCount.ToString();
                decodeIgnoreCountLable.Text = mDecoder.DecodeIgnoreByteCount.ToString();
            });
            if (heartPackgetEnalebox.Checked)
            {
                sendHeartPackget();
            }

        }
        private void sendHeartPackget()
        {
            byte[] data = new byte[3];
            //data[0] = decimal.ToByte(modeDataNumber.Value);
            //data[1] = decimal.ToByte(statusNumber.Value);
            //data[2] = decimal.ToByte(powerNumber.Value);
            data[0] = decimal.ToByte(powerNumber.Value);
            mEncoder.cmdcoder_send_bytes(data,1);
        }
        private void log(String str)
        {
            Invoke((MethodInvoker)delegate
            {
                winConsole.AppendText(str);
            });
        }
        private int cmdcoderSendCallback(byte c)
        {
            byte[] data = new byte[1];
            data[0] = c;
            if (mSerialPort != null && mSerialPort.IsOpen)
            {
                mSerialPort.Write(data,0,1);
            }
            return 1;
        }
        void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int size = mSerialPort.BytesToRead;
            if (size < 1)
            {
                return;
            }

            byte[] data = new byte[size];
            mSerialPort.Read(data, 0, size);
            for (int idx = 0; idx < size; idx++)
            {
                //log(data[idx].ToString("X02"));
                if (1 == mDecoder.cmdcoder_Parse_byte(data[idx]))
                {
                    /*
                    log("\r\n");
                    for (int i = 0; i < mDecoder.len; i++)
                    {
                        log(mDecoder.data[i].ToString("X02"));
                    }
                    log("\r\n");
                    */
                    handle_remoter_channel_packget(mDecoder.id,mDecoder.data,mDecoder.len);

                }
                //winConsole.AppendText("\r\n");
            }
            
        }




        /*

cmdcoder Frame : 
{
	[0xff, 0xff, id , len....(可变长0-4Byte) , data....(可变长), crc ]
	len: 是data的实际长度
	crc: id和len和data 区段编码后的字节相加的和
	id : JOSTICK_PACKGET_ID =1 ，data数据是遥控器的通道与按键值； HEART_PACKGET_ID =0 心跳包id
	
	data:
	{
		1，通道包
		cmdcoder id = 1 , data[]={channel1-16bit,channel2,channle3,channle4,channel5,modebtn-8bit,sampleB-8bit,alarmBtn-8bit}
		channel1 left-jostick-x; [Byte0][Byte1] 小端
		channel2 left-jostick-y;
		channel3 right-jostick-x;
		channel4 right-jostick-y;
		channel5 middle-knob 
        modebtn:0-1-2
        sampleBtn: 1-0  1:press key
        alarmbtn: 1-0   1:press key
	
		2，心跳包, 监听主控发过来的心跳包数据，用来获取状态
		cmdcoder id = 0 , data[]={电量-8bit}
		byte2 : 0-100 , 电量
	
	}
}


*/

        byte JOSTICK_PACKGET_ID= 1;
        byte HEART_PACKGET_ID =0;

        void handle_remoter_channel_packget(byte id, byte[] data,UInt32 len)
        {
            byte[] channleArray = new byte[2];
            
            switch (id)
            {
                case 1:
                    if (len < 16) { MessageBox.Show("数据包长度有问题"); break; }

                    UInt16[] channel = new UInt16[5];
                    for( int i=0; i< 10; i++)
                    {
                        channleArray[0]=data[i];
                        channleArray[1]=data[i+1];
                        channel[i/2] = BitConverter.ToUInt16(channleArray,0);
                        i++;
                    }

                    Invoke((MethodInvoker)delegate
                    {

                        channelLable1.Text = channel[0].ToString();
                        channelLable2.Text = channel[1].ToString();
                        channelLable3.Text = channel[2].ToString();
                        channelLable4.Text = channel[3].ToString();
                        channelLable5.Text = channel[4].ToString();

                        modeBtnLable.Text = data[10].ToString();
                        sampleBtnLable.Text = data[11].ToString();
                        alarmBtnLable.Text = data[12].ToString();

                        menulable.Text = data[13].ToString();
                        okbtnLable.Text = data[14].ToString();
                        cancelbtnlable.Text = data[15].ToString();

                    });
 
                    break;
                case 0:
                    //log("mode: 0x" + data[0].ToString("X2") + "    status: 0x"+data[1].ToString("X2")+ "    Power: "+data[2].ToString()+"%\r\n");
                    log("Boat Power: " + data[0].ToString() + "%\r\n");
                    break;

                default:
                    break;
            }
        }






        int angle = 0;
        bool test = true;
        bool isConnectBtnClick = false;
        
        private void serialConnectButton_Click(object sender, EventArgs e)
        {
            /*
            if( true){
                updatePitchPic(angle);
                updateRollPic(angle);
                updateYawPic(angle);
                angle += 10;
                angle %= 360;

                updateGpsLatitudeLongtitude(22.289478673938376, 113.57801148948458);

                if (test)
                {
                    TimerStart();
                    test = false;
                }
                else
                {
                    TimerStop();
                    test = true;
                }
                return;
            }
             */

            if (!isConnectBtnClick )//serialConnectButton.Text.Equals("Connect"))
            {
                String port = comComboBox.Items[comComboBox.SelectedIndex].ToString();
                String baud = baudrateComboBox.Items[baudrateComboBox.SelectedIndex].ToString();
                if (port.Equals("") || baud.Equals(""))
                {
                    MessageBox.Show("No Select Com or Baudrate !!!");
                    return;
                }
                try
                {
                    if( mSerialPort != null && mSerialPort.IsOpen ) mSerialPort.Close();
                    mSerialPort = new SerialPort(port, int.Parse(baud), Parity.None, 8, StopBits.One);
                    mSerialPort.Open();
                    mSerialPort.DataReceived += serialReciver;
                    serialConnectButton.Text = "DisConnect";
                    isConnectBtnClick = true;

                    TimerStart();
                }
                catch 
                {
                    MessageBox.Show("Open Serial Error !!!");
                    return;
                }

            }else{
                if( mSerialPort != null && mSerialPort.IsOpen ){
                    mSerialPort.DataReceived -= serialReciver;
                    try
                    {
                        //mSerialPort.Close();
                    }
                    catch
                    {
                        MessageBox.Show("close Serial Error !!!");
                    }
                    
                    serialConnectButton.Text = "Connect";
                    isConnectBtnClick = false;

                    TimerStop();
                }
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }






    }
}
