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

        public Form1()
        {
            InitializeComponent();
            comRate_Init();
            com_Init();
            mDecoder = new cmdCoder(4,null);
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
        private void log(String str)
        {
            Invoke((MethodInvoker)delegate
            {
                winConsole.AppendText(str);
            });
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

                    handle_navi_message(mDecoder.data, mDecoder.len);

                }
                //winConsole.AppendText("\r\n");
                //data[idx] = (byte)mSerialPort.ReadByte();
            }
            
        }



        int can_data_updating = 0;
        byte[] GPSLatitudeArray = new byte[8];
        byte[] GPSLongitudeArray = new byte[8];
        double GpsLatitude, GpsLongitude;
        float GPSSpeed ;
        byte GPSTimeHour;
        byte GPSTimeMin ;
        byte GPSTimeSec ;
        byte GPSDateYear;
        byte GPSDateMonth;
        byte GPSDateDay;
        float NaviPitch, NaviRoll, NaviYaw;
        void checkLonLatData( byte mask)
        {
            can_data_updating |= mask;
            
            if( 0x03 == (can_data_updating & 0x03) ){
                //latitude
                GpsLatitude = BitConverter.ToDouble(GPSLatitudeArray,0);
                can_data_updating &= ~0x03;
            }
            if( 0x30 == (can_data_updating & 0x30) ){
                //latitude
                GpsLongitude = BitConverter.ToDouble(GPSLongitudeArray,0);
                can_data_updating &= ~0x30;
            }
        }
        bool handle_navi_message(byte[] data,UInt32 len)
        {
            int i;
            if (len < 8) return false;
            if (data[0] == 0x11)
            {
                switch (data[1])
                {
                    case 0x00:
                        for (i = 0; i < 6; i++)
                        {
                            GPSLatitudeArray[i] = data[2 + i];
                        }
                        checkLonLatData(0x01);
                        break;
                    case 0x01:
                        for (i = 0; i < 2; i++)
                        {
                            GPSLatitudeArray[i + 6] = data[2 + i];
                        }
                        checkLonLatData(0x02);
                        for (i = 0; i < 4; i++)
                        {
                            GPSLongitudeArray[i] = data[4 + i];
                        }
                        checkLonLatData(0x10);
                        break;
                    case 0x02:
                        for (i = 0; i < 4; i++)
                        {
                            GPSLongitudeArray[i + 4] = data[2 + i];
                        }
                        checkLonLatData(0x20);
                        //GPSSpeed[0] = data[6];
                        //GPSSpeed[1] = data[7];
                        GPSSpeed = (float)((int)data[6] * 256 + (int)data[7] / 10.0f);
                        break;
                    case 0x03:
                        /*
                        Compass.heading[0] = data[2];
                        Compass.heading[1] = data[3];
                        Compass.pitch[0]=data[4];	
                        Compass.pitch[1]=data[5];
                        Compass.roll[0]=data[6];	
                        Compass.roll[1]=data[7];
                         * */
                        NaviYaw = (float)(((Int16)(data[2] * 256 + (int)data[3])) / 10.0f);
                        NaviPitch = (float)(((Int16)(data[4] * 256 + (int)data[5])) / 10.0f);
                        NaviRoll = (float)(((Int16)(data[6] * 256 + (int)data[7])) / 10.0f);
                        log("Yaw: " + NaviYaw.ToString() );
                        log("Pitch: " + NaviPitch.ToString());
                        log("Roll: " + NaviRoll.ToString());
                        log("\r\n");
                        break;
                    case 0x04:
                        GPSTimeHour = data[2];
                        GPSTimeMin = data[3];
                        GPSTimeSec = data[4];
                        GPSDateYear = data[5];
                        GPSDateMonth = data[6];
                        GPSDateDay = data[7];
                        break;
                    default:
                        break;
                }// end of case
            }// end of data from 0x11
            return false;
        }


        private void serialConnectButton_Click(object sender, EventArgs e)
        {

            if( serialConnectButton.Text.Equals("Connect")){
                String port = comComboBox.Items[comComboBox.SelectedIndex].ToString();
                String baud = baudrateComboBox.Items[baudrateComboBox.SelectedIndex].ToString();
                if (port.Equals("") || baud.Equals(""))
                {
                    MessageBox.Show("No Select Com or Baudrate !!!");
                    return;
                }
                try
                {
                    mSerialPort = new SerialPort(port, int.Parse(baud), Parity.None, 8, StopBits.One);
                    mSerialPort.Open();
                    mSerialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
                    serialConnectButton.Text = "DisConnect";
                }
                catch 
                {
                    MessageBox.Show("Open Serial Error !!!");
                    return;
                }

            }else{
                if( mSerialPort != null && mSerialPort.IsOpen ){
                    mSerialPort.Close();
                    serialConnectButton.Text = "Connect";
                }
            }
        }









    }
}
