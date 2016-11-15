using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;//用于调用串口类函数
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace NavigationTester
{
    public partial class Form1 : Form
    {
        private SerialPort mSerialPort;
        private cmdCoder mDecoder;
        private PieDrawerManager yawDrawer;
        private PieDrawerManager pitchDrawer;
        private PieDrawerManager rollDrawer;
        System.Timers.Timer mTimer = new System.Timers.Timer(1000); //设置时间间隔为1秒
        System.Timers.Timer updateTimer = new System.Timers.Timer(100); //设置时间间隔为1秒
        SerialDataReceivedEventHandler serialReciver;

        private int MAX_GPS_ERROR_COUNT = 5;
        private int MAX_COMPASS_ERROR_COUNT = 10;

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
            yawDrawer = new PieDrawerManager(YawPanel.CreateGraphics(), YawPanel.Width, YawPanel.Height, YawPanel.BackColor);
            rollDrawer = new PieDrawerManager(rollPictureBox.CreateGraphics(), rollPictureBox.Width, rollPictureBox.Height, rollPictureBox.BackColor);
            pitchDrawer = new PieDrawerManager(PitchPanel.CreateGraphics(), PitchPanel.Width, PitchPanel.Height, PitchPanel.BackColor);

            if (!loadLocalGPSFromFile())
            {
                localLatitude = double.Parse(localLatTextBox.Text);
                localLongitude = double.Parse(locallongTextBox.Text);
            }
            
            serialReciver = new SerialDataReceivedEventHandler(serialPort_DataReceived);


            compassChartInit();

            TimerIint();



        }

        private void compassChartInit()
        {

            Random random = new Random();
            compassChart.Series.Clear();

            Series yawSeries = new Series("yaw");
            Series pitchSeries = new Series("pitch");
            Series rollSeries = new Series("roll");
            Series zeroSeries = new Series("0");


            yawSeries.BorderColor = Color.Blue;
            pitchSeries.BorderColor = Color.Green;
            rollSeries.BorderColor = Color.Red;
            yawSeries.BorderWidth = 3;
            pitchSeries.BorderWidth = 3;
            rollSeries.BorderWidth = 3;

            zeroSeries.BorderColor = Color.Black;
            zeroSeries.BorderWidth = 2;



            yawSeries.ChartType = SeriesChartType.Spline;
            pitchSeries.ChartType = SeriesChartType.Spline;
            rollSeries.ChartType = SeriesChartType.Spline;
            zeroSeries.ChartType = SeriesChartType.Line;



            compassChart.Series.Add(yawSeries);
            compassChart.Series.Add(pitchSeries);
            compassChart.Series.Add(rollSeries);
            compassChart.Series.Add(zeroSeries);




            //yaw 0-360
            /*
            compassChart.ChartAreas[0].AxisX.Maximum = 50;// 1s 20hz compass data 
            compassChart.ChartAreas[0].AxisY.Maximum = 360;
            compassChart.ChartAreas[0].AxisY.Interval = 5;
            compassChart.ChartAreas[0].AxisX.Interval = 1;
            */

            // pitch -90->0<-90  
            /*
            compassChart.ChartAreas[0].AxisX.Maximum = 50;// 1s 20hz compass data 
            compassChart.ChartAreas[0].AxisY.Maximum = 95;
            compassChart.ChartAreas[0].AxisY.Minimum = -90;
            compassChart.ChartAreas[0].AxisY.Interval = 5;
            compassChart.ChartAreas[0].AxisX.Interval = 1;
             * */

            // roll  0->180 180-> -180 
            compassChart.ChartAreas[0].AxisX.Maximum = 50;// 1s 20hz compass data 
            compassChart.ChartAreas[0].AxisY.Maximum = 185;
            compassChart.ChartAreas[0].AxisY.Minimum = -185;
            compassChart.ChartAreas[0].AxisY.Interval = 5;
            compassChart.ChartAreas[0].AxisX.Interval = 1;
           

            //compassChart.ChartAreas[0].AxisX.ArrowStyle = AxisArrowStyle.None;

            for (int i = 0; i < compassChart.ChartAreas[0].AxisX.Maximum; i++)
            {
                compassChart.Series["0"].Points.AddY(0);
            }
        }

        void updateCompassChartValue(float yaw, float pitch, float roll)
        {
            if (chartStopDisplay) return;

            Invoke((MethodInvoker)delegate
            {
                //compassChart.Series["0"].Points.AddY(0);

                if (compassChart.Series["yaw"].Points.Count > compassChart.ChartAreas[0].AxisX.Maximum)
                    compassChart.Series["yaw"].Points.RemoveAt(0);
                if (compassChart.Series["roll"].Points.Count > compassChart.ChartAreas[0].AxisX.Maximum)
                    compassChart.Series["roll"].Points.RemoveAt(0);
                if (compassChart.Series["pitch"].Points.Count > compassChart.ChartAreas[0].AxisX.Maximum)
                    compassChart.Series["pitch"].Points.RemoveAt(0);


                if (rollChatCheckBox.Checked)
                {
                    compassChart.Series["roll"].Points.AddY(roll);
                }
                else
                {
                    if (compassChart.Series["roll"].Points.Count > 0)
                        compassChart.Series["roll"].Points.Clear();
                }

                if (pitchChartCheckBox.Checked)
                {
                    compassChart.Series["pitch"].Points.AddY(pitch);
                }
                else
                {
                    if (compassChart.Series["pitch"].Points.Count > 0)
                        compassChart.Series["pitch"].Points.Clear();
                }

                if (yawChartCheckBox.Checked)
                {
                    compassChart.Series["yaw"].Points.AddY(yaw);
                }
                else
                {
                    if (compassChart.Series["yaw"].Points.Count > 0)
                        compassChart.Series["yaw"].Points.Clear();
                }
                //compassChart.Series["yaw"].Points.AddY(NaviYaw);
                //compassChart.Series["roll"].Points.AddY(NaviRoll);
                //compassChart.Series["pitch"].Points.AddY(NaviPitch);


            });
        }

        private void comRate_Init()
        {
            baudrateComboBox.Items.Add("9600");
            baudrateComboBox.Items.Add("115200");
            baudrateComboBox.Items.Add("100000");
            baudrateComboBox.Items.Add("57600");
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
            return;
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

                    handle_navi_can_message(mDecoder.data, mDecoder.len);

                }
        
                //data[idx] = (byte)mSerialPort.ReadByte();
            }
            
        }


        private string saveFilePath = "localGPS.ini";
        private bool loadLocalGPSFromFile()
        {
            if (!File.Exists(saveFilePath))
            {
                return false;
            }
            string latStr, lonStr;
            string[] filelines,splitstr={"="};

            filelines = File.ReadAllLines(saveFilePath);
            latStr = filelines[0];
            lonStr = filelines[1];

            string[] sArray ;
            sArray = latStr.Split(new char[1] { '=' });
            localLatTextBox.Text = sArray[1];
            sArray = lonStr.Split(new char[1] { '=' });
            locallongTextBox.Text = sArray[1];

            localLatitude = double.Parse(localLatTextBox.Text);
            localLongitude = double.Parse(locallongTextBox.Text);

            return true;
        }
        private bool saveLocalGpsToFile()
        {
            string latStr, lonStr;
            latStr = "Latitude="+localLatitude.ToString();
            lonStr = "Longitude="+localLongitude.ToString();
            if (File.Exists(saveFilePath))
            {
                //clear
                File.WriteAllBytes(saveFilePath, new byte[0]);
            }

            FileStream fs = new FileStream(saveFilePath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            sw.WriteLine(latStr);
            sw.WriteLine(lonStr);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
            return true;
        }



        int can_data_updating = 0;
        byte[] GPSLatitudeArray = new byte[8];
        byte[] GPSLongitudeArray = new byte[8];
        double GpsLatitude, GpsLongitude;
        double newGpsLatitude, newGpsLongitude;
        double localLatitude = 0.0;
        double localLongitude = 0.0;
        float GPSSpeed ;
        byte GPSTimeHour;
        byte GPSTimeMin ;
        byte GPSTimeSec ;
        byte GPSDateYear;
        byte GPSDateMonth;
        byte GPSDateDay;
        float NaviPitch, NaviRoll, NaviYaw;

        byte gpsDataCompile = 0;
        void checkLonLatData( byte mask)
        {
            can_data_updating |= mask;
            
            if( 0x03 == (can_data_updating & 0x03) ){
                //latitude
                newGpsLatitude = BitConverter.ToDouble(GPSLatitudeArray, 0);
                can_data_updating &= ~0x03;
                gpsDataCompile |= 0x1;
            }
            if( 0x30 == (can_data_updating & 0x30) ){
                //Longitude
                newGpsLongitude = BitConverter.ToDouble(GPSLongitudeArray, 0);
                can_data_updating &= ~0x30;
                gpsDataCompile |= 0x2;
            }
            if (gpsDataCompile == 0x3)
            {
                updateGpsLatitudeLongtitude(newGpsLatitude, newGpsLongitude);
                gpsDataCompile = 0;
            }
        }
        bool handle_navi_can_message(byte[] data,UInt32 len)
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
                        GPSSpeed = (float)((int)data[6] * 256 + (int)data[7] / 10.0f);

                        checkLonLatData(0x20);
                        
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
                        float NYaw = (float)(((Int16)(data[2] * 256 + (int)data[3])) / 10.0f);
                        float NPitch = (float)(((Int16)(data[4] * 256 + (int)data[5])) / 10.0f);
                        float NRoll = (float)(((Int16)(data[6] * 256 + (int)data[7])) / 10.0f);
                        updateCompassData(NYaw,NRoll,NPitch);
                        
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

        private void updateYawPic(float angle)
        {
            Invoke((MethodInvoker)delegate
            {
                yawDrawer.updateValue(angle);
                int persen = yawDrawer.getPersen();
                YawPersenLabel.Text = persen.ToString() + "%";
                if (yawMinValue == 0 && yawMaxValue == 0)
                {
                    yawMaxValue = angle;
                    yawMinValue = angle;
                }
                if (yawMinValue > angle) yawMinValue = angle;
                if (yawMaxValue < angle) yawMaxValue = angle;
                YawmaxLabel.Text = yawMaxValue.ToString();
                yawminlable.Text = yawMinValue.ToString();

                yawAngleLabel.Text = angle.ToString();
            });
            
        }
        private void updateRollPic(float angle)
        {
            Invoke((MethodInvoker)delegate
            {
            if( angle < 0 )
                rollDrawer.updateValue(360 + angle); //-180~180
            else
                rollDrawer.updateValue(angle); //-180~180
            int persen = rollDrawer.getPersen();
            rolllabel.Text = persen.ToString() + "%";
            if (rollMin == 0 && rollMax == 0)
            {
                rollMin = angle;
                rollMax = angle;
            }
            if (rollMin > angle) rollMin = angle;
            if (rollMax < angle) rollMax = angle;
            rollmaxlabel.Text = rollMax.ToString();
            rollMinlable.Text = rollMin.ToString();

            rollAngleLabel.Text = angle.ToString();
            });
        }
        private void updatePitchPic(float angle)
        {
            Invoke((MethodInvoker)delegate
            {

                if (angle < 0)
                    pitchDrawer.updateValue(angle+360); //-90~90
                else
                    pitchDrawer.updateValue(angle); //-90~90
            int persen = pitchDrawer.getPersen();
            pitchlable.Text = persen.ToString() + "%";
            if (pitchMin == 0 && pitchMax == 0)
            {
                pitchMin = angle;
                pitchMax = angle;
            }
            if (pitchMin > angle) pitchMin = angle;
            if (pitchMax < angle) pitchMax = angle;
            pitchmaxlable.Text = pitchMax.ToString();
            pitchMinLable.Text = pitchMin.ToString();

            pitchAngleLabel.Text = angle.ToString();
            });
        }
        private void updateCompassData(float yaw, float roll, float pitch)
        {
            if (NaviYaw == yaw && NaviRoll == roll && NaviPitch == pitch)
            {
                tmpCompassDataErrorCount++;
            }
            else
            {
                NaviYaw = yaw;
                NaviRoll = roll;
                NaviPitch = pitch;
                //updateYawPic(NaviYaw);
                //updatePitchPic(NaviPitch);
                //updateRollPic(NaviRoll);
                updateCompassChartValue(NaviYaw, NaviPitch, NaviRoll);

                if (tmpCompassDataErrorCount >= MAX_COMPASS_ERROR_COUNT)
                {
                    compassDataErrorCount++;
                    Invoke((MethodInvoker)delegate
                    {
                        compassDataErrorCountLabel.Text = compassDataErrorCount.ToString();
                    });

                }
                tmpCompassDataErrorCount = 0;
                
            }
            compassDataUpdataCount++;
            log("Yaw: " + NaviYaw.ToString());
            log("Pitch: " + NaviPitch.ToString());
            log("Roll: " + NaviRoll.ToString());
            log("\r\n");
        }

        // *********************** GPS cali
        private const double EARTH_RADIUS = 6378.137;//地球半径
        
        private static double rad(double d)
        {
            return d * Math.PI / 180.0;
        }

        private  double caliGpsDistance(double lat1, double lng1,double lat2, double lng2)
        {
            double radLat1 = rad(lat1);
            double radLat2 = rad(lat2);
            double a = radLat1 - radLat2;
            double b = rad(lng1) - rad(lng2);

            double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
             Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
            s = s * EARTH_RADIUS;
            //*** km
            //s = Math.Round(s * 10000) / 10000;
            //*** m
            s = Math.Round(s * 10000) / 10;
            return s;
        }

        double gpsSpeed = 0;
        double gpsSpeedLat0 = 0;
        double gpsSpeedLon0 = 0;
        int gpsSpeedTimeMS = 0 ;
        private void updateGpsSpeed()
        {
            int interval = 1000; //1000 = 1s
            int ms;

            if (gpsSpeedLat0 == 0 && gpsSpeedLon0 == 0)
            {
                gpsSpeedLon0 = newGpsLongitude;
                gpsSpeedLat0 = newGpsLatitude;
                gpsSpeed = 0;
                gpsSpeedTimeMS = DateTime.Now.Millisecond;
                return;
            }
            ms = DateTime.Now.Millisecond - gpsSpeedTimeMS;
            if (ms > interval)
            {
                double distance = caliGpsDistance(newGpsLatitude, newGpsLongitude, gpsSpeedLat0, gpsSpeedLon0);
                gpsSpeed = distance / ms;
                
                Invoke((MethodInvoker)delegate
                {
                    gpsSpeedlabel.Text = gpsSpeed.ToString();// ("f3");
                    sensorGpsSpeedlabel.Text = GPSSpeed.ToString();
                });
                gpsSpeedLon0 = newGpsLongitude;
                gpsSpeedLat0 = newGpsLatitude;
                gpsSpeedTimeMS = DateTime.Now.Millisecond;
            }
        }

        double maxDistance = -1.0;
        double minDistance = -1.0;
        private void updateGpsLatitudeLongtitude( double lat,double lon)
        {
            if (GpsLatitude == lat && GpsLongitude == lon)
            {
                tmpGpsDataErrorCount++;
            }
            else
            {
                GpsLatitude = lat ;
                GpsLongitude = lon;

                double distance = caliGpsDistance(lat, lon, localLatitude, localLongitude);
                if (minDistance < 0 || distance < minDistance)
                    minDistance = distance;
                if (maxDistance < 0 || distance > maxDistance)
                    maxDistance = distance;

                Invoke((MethodInvoker)delegate
                {
                    MaxDistanceTextBox.Text = maxDistance.ToString();
                    MinDistanceTextBox.Text = minDistance.ToString();
                    currentDistanceTextBox.Text = distance.ToString();
                    gpsLongtitudeTextBox.Text = lon.ToString();
                    GpsLattitudeTextBox.Text = lat.ToString();
                });

                updateGpsSpeed();

                if (tmpGpsDataErrorCount >= MAX_GPS_ERROR_COUNT)
                {
                    gpsDataErrorCount++;
                    Invoke((MethodInvoker)delegate
                    {
                        gpsDataErrorCountLabel.Text = tmpGpsDataErrorCount.ToString();
                    });
                }
                tmpGpsDataErrorCount = 0;
            }
            

            gpsDataUpdataCount++;

        }




        private int gpsDataUpdataCount = 0;
        private int compassDataUpdataCount = 0;
        private int compassDataErrorCount=0;
        private int gpsDataErrorCount = 0;
        private int tmpCompassDataErrorCount = 0;
        private int tmpGpsDataErrorCount = 0;
        private void Timer_TimesUp(object sender, System.Timers.ElapsedEventArgs e)
        {
            Invoke((MethodInvoker)delegate
            {
                compassReflashLabel.Text = compassDataUpdataCount.ToString();
                gpsReflashLabel.Text = gpsDataUpdataCount.ToString();
            });
            
            gpsDataUpdataCount = 0;
            compassDataUpdataCount = 0;
        }

        private void updateTimerHandler(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (compassDataUpdataCount <= 0) return;

            updateYawPic(NaviYaw);
            updatePitchPic(NaviPitch);
            updateRollPic(NaviRoll);
        }
        private void TimerStart()
        {
            mTimer.Enabled = true; //是否触发Elapsed事件
            mTimer.Start();

            updateTimerStart();
        }
        private void TimerStop()
        {
            mTimer.Stop();

            updateTimerStop();
        }
        private void updateTimerStart()
        {
            updateTimer.Enabled = true;
            updateTimer.Start();
        }
        private void updateTimerStop()
        {
            updateTimer.Stop();
        }
        private void TimerIint()
        {
            mTimer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_TimesUp);
            mTimer.AutoReset = true; //每到指定时间Elapsed事件是触发一次（false），还是一直触发（true）

            updateTimer.Elapsed += new System.Timers.ElapsedEventHandler(updateTimerHandler);
            updateTimer.AutoReset = true;
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
                    serialConnectButton.Text = "关闭串口";
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

                    serialConnectButton.Text = "打开串口";
                    TimerStop();
                    isConnectBtnClick = false;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        float yawMinValue = 0;
        float yawMaxValue = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            yawDrawer.clear();
           // int persen = yawDrawer.getPersen();
            YawmaxLabel.Text = "0";
            yawminlable.Text = "0";
            yawMinValue = 0;
            yawMaxValue = 0;


        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        float pitchMin = 0;
        float pitchMax = 0;
        private void button1_Click_1(object sender, EventArgs e)
        {
            pitchDrawer.clear();
            //int persen = pitchDrawer.getPersen();
            pitchmaxlable.Text = "0";
            pitchMinLable.Text = "0";
            pitchMin = 0;
            pitchMax = 0;
        }

        float rollMin = 0;
        float rollMax = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            rollDrawer.clear();
            //int persen = rollDrawer.getPersen();
            rollmaxlabel.Text = "0";
            rollMinlable.Text = "0";
            rollMin = 0;
            rollMax = 0;
        }

        private void YawPersenLabel_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void LatTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void changeLocalGpsButton_Click(object sender, EventArgs e)
        {
            localLatitude = double.Parse(localLatTextBox.Text);
            localLongitude = double.Parse(locallongTextBox.Text);

            if (saveLocalGpsToFile())
                MessageBox.Show("成功修改与保存坐标！");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            maxDistance = -1.0;
            minDistance = -1.0;
            MaxDistanceTextBox.Text = "0.0";
            MinDistanceTextBox.Text = "0.0";
            currentDistanceTextBox.Text = "0.0";
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //罗盘校准
            bool res = false;
            if (mSerialPort != null && mSerialPort.IsOpen)
            {
                try
                {
                    mSerialPort.Write("cali\n");
                    MessageBox.Show("请进行校准操作，当有数据更新时，校准完成");
                    res = true;
                }
                catch
                {
                    MessageBox.Show("发送命令失败");
                }

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            locallongTextBox.Text = gpsLongtitudeTextBox.Text;
            localLatTextBox.Text = GpsLattitudeTextBox.Text;
            changeLocalGpsButton_Click(sender, e);
        }

        private bool chartStopDisplay = false;
        private void ChartStopbutton_Click(object sender, EventArgs e)
        {
            if (chartStopDisplay)
            {
                chartStopDisplay = false;
                ChartStopbutton.Text = "暂停";
            }
            else
            {
                //stop display
                ChartStopbutton.Text = "继续";
                chartStopDisplay = true;
            }
        }

        private void yawChartCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            

            if (yawChartCheckBox.Checked)
            {
                rollChatCheckBox.Checked = false;
                pitchChartCheckBox.Checked = false;
                rollChatCheckBox.Enabled = false;
                pitchChartCheckBox.Enabled = false;
                compassChart.ChartAreas[0].AxisY.Maximum = 360;
                compassChart.ChartAreas[0].AxisY.Minimum = 0;
            }
            else
            {
                rollChatCheckBox.Enabled = true;
                pitchChartCheckBox.Enabled = true;
                compassChart.ChartAreas[0].AxisY.Maximum = 185;
                compassChart.ChartAreas[0].AxisY.Minimum = -185;
            }
            
        }









    }
}
