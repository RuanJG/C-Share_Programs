using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace M80A2Console
{
    public partial class Form1 : Form, CommandHadler
    {
        private static IPAddress m_host = IPAddress.Parse("192.168.1.230");
        private static int m_port = 10417;

        private TcpClient m_client;
        private bool m_reconnect;
        private byte[] m_revbuffer;
        private CommandParser m_parser;
        private Timer m_connectTimer;
        private Timer m_statusTimer;
        private Timer m_lightTimer;
        private bool m_showLigth;
        private bool m_listhStatus;
        private SwitchStatus m_status;

        public Form1()
        {
            InitializeComponent();
            m_client = null;
            m_reconnect = true;
            m_revbuffer = new byte[1];
            m_parser = new CommandParser();
            m_connectTimer = new Timer();
            m_statusTimer = new Timer();
            m_lightTimer = new Timer();
            m_status = new SwitchStatus();
            m_showLigth = false;
            m_listhStatus = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            m_connectTimer.Interval = 1000;
            m_connectTimer.Tick += new EventHandler(reconnectCheck);
            m_connectTimer.Start();

            m_statusTimer.Interval = 1000;
            m_statusTimer.Tick += new EventHandler(statusUpdate);
            m_statusTimer.Start();

            m_lightTimer.Interval = 500;
            m_lightTimer.Tick += new EventHandler(lightUpdate);
            m_lightTimer.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // TODO
        }

        private void reconnectCheck(object sender, EventArgs e)
        {
            if (m_reconnect)
            {
                beginConnect();
            }
        }

        private void statusUpdate(object sender, EventArgs e)
        {
            byte[] data = new byte[1];
            data[0] = 0;
            sendCommand(m_parser.packResponse(data));
        }

        private void lightUpdate(object sender, EventArgs e)
        {
            if (m_showLigth)
            {
                m_listhStatus = !m_listhStatus;
                pictureBox1.Visible = m_listhStatus;
            }
        }

        public void writeLog(string log)
        {
            Invoke((MethodInvoker)delegate
            {
                textBox1.Text += log;
                if (textBox1.Text.Length > 1024 * 64)
                {
                    textBox1.Text = textBox1.Text.Substring(32 * 1024);
                }

                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.ScrollToCaret();
            });
        }

        private void beginConnect()
        {
            m_reconnect = false;
            m_client = new TcpClient();
            m_client.BeginConnect(m_host, m_port, connectCallback, m_client);
        }

        private void connectCallback(IAsyncResult ar)
        {
            TcpClient client = (TcpClient)ar.AsyncState;

            try
            {
                client.EndConnect(ar);

                if (!client.Connected)
                {
                    m_reconnect = true;
                    return;
                }

                client.Client.BeginReceive(m_revbuffer, 0, 1, SocketFlags.None, receivedCallback, client);
                writeLog("connect ok\r\n");
            }
            catch (Exception e)
            {
                writeLog("connect callback exception: " + e.Message + "\r\n");
                m_reconnect = true;
            }
        }

        private void receivedCallback(IAsyncResult ar)
        {
            TcpClient client = (TcpClient)ar.AsyncState;
            try
            {
                int len = client.Client.EndReceive(ar);
                if (len < 1)
                {
                    m_reconnect = true;
                    return;
                }

                List<byte[]> commands = m_parser.parse(m_revbuffer, 0, len);
                client.Client.BeginReceive(m_revbuffer, 0, 1, SocketFlags.None, receivedCallback, client);

                foreach (byte[] cmd in commands)
                {
                    byte[] resp = handle(cmd);
                    if (resp != null && resp.Length > 0)
                    {
                        sendCommand(resp);
                    }
                }
            }
            catch (Exception e)
            {
                writeLog("receive callback exception: " + e.Message + "\r\n");
                if (client.Connected)
                {
                    client.Close();
                }
                m_reconnect = true;
            }
        }

        private void sendCallback(IAsyncResult ar)
        {
            TcpClient client = (TcpClient)ar.AsyncState;

            try
            {
                int len = client.Client.EndSend(ar);
                if (len < 1)
                {
                    m_reconnect = true;
                    return;
                }

                writeLog("Send " + len + " Bytes!!!" + "\r\n");
            }
            catch (Exception e)
            {
                writeLog("send callback exception: " + e.Message + "\r\n");
                if (client.Connected)
                {
                    client.Close();
                }
                m_reconnect = true;
            }
        }

        public void sendCommand(byte[] command)
        {
            if (m_client == null || !m_client.Connected)
            {
                writeLog("Client Disconnected!!!" + "\r\n");
                return;
            }

            try
            {
                m_client.Client.BeginSend(command, 0, command.Length, SocketFlags.None, sendCallback, m_client);
            }
            catch (Exception e)
            {
                writeLog("socket send exception: " + e.Message + "\r\n");
                m_reconnect = true;
            }
        }

        public byte[] handle(byte[] command)
        {
            int index = 0;
            int di = 0;
            while (index < command.Length)
            {
                int cmd = command[index]; //command[0];
                index += 1;
                di = index;
                switch (cmd)
                {
                    case 0x0A: //前仓湿度温度
                        index += 4;
                        if( index > command.Length )
                        {
                            writeLog("Invalid 0x"+cmd.ToString("X2")+" command data from master control!\r\n");
                            return null;
                        }
                        m_status.FrontHumidity = 0.1f * (command[di] | (command[di + 1] << 8));
                        m_status.FrontTemperature = 0.1f * (command[di+2] | (command[di + 3] << 8));
                        Invoke((MethodInvoker)delegate
                        {
                            aGauge10.Value = m_status.FrontHumidity;
                            aGauge9.Value = m_status.FrontTemperature;
                        });
                        
                        break;

                    case 0x0B: //后仓湿度温度
                        index += 4;
                        if (index > command.Length)
                        {
                            writeLog("Invalid 0x" + cmd.ToString("X2") + " command data from master control!\r\n");
                            return null;
                        }
                        m_status.BackHumidity = 0.1f * (command[di] | (command[di + 1] << 8));
                        m_status.BackTemperature = 0.1f * (command[di+2] | (command[di + 3] << 8));
                        Invoke((MethodInvoker)delegate
                        {
                            aGauge19.Value = m_status.BackHumidity;
                            aGauge18.Value = m_status.BackTemperature;
                        });
                        break;

                    case 0x02: //发电机和发动机
                        index += 6;
                        if (index > command.Length)
                        {
                            writeLog("Invalid 0x" + cmd.ToString("X2") + " command data from master control!\r\n");
                            return null;
                        }
                        int eng_status = (command[di] | (command[di+1]<<8));
                        m_status.EnginSwitchOn = ((eng_status & 0x8) == 0) ? false : true;
                        int oil_low_warning = command[di + 3]*100;
                        int water_high_tempture_warning = command[di + 4]*100;
                        Invoke((MethodInvoker)delegate
                        {
                            aGauge14.Value = oil_low_warning;
                            aGauge13.Value = water_high_tempture_warning;
                            groupBox11.Text = "发电机控制" + (m_status.EnginSwitchOn ? "(开)" : "(关)");
                        });
                        break;

                    case 0x04: //侧推上电控制
                        index += 2;
                        break;

                    case 0x05: //侧推方向控制
                        index += 2;
                        break;

                    case 0x08: // DC配电箱
                        index += 2;
                        if (index > command.Length)
                        {
                            writeLog("Invalid 0x" + cmd.ToString("X2") + " command data from master control!\r\n");
                            return null;
                        }
                        int dc_status = (command[di] | (command[di + 1] << 8));
                        
                        m_status.AntenaUpOn = ((dc_status & 0x1) == 0) ? false : true;
                        m_status.AntenaDownOn = ((dc_status & 0x2) == 0) ? false : true;
                        m_status.SpeekerSwitchOn = ((dc_status & (0x1<<2)) == 0) ? false : true;// 蜂鸣器电源控制
                        m_status.SerialServerSwitchOn = ((dc_status & (0x1 << 3)) == 0) ? false : true;
                        m_status.WifiSwitchOn = ((dc_status & (0x1 << 4)) == 0) ? false : true;
                        m_status.RaserSwitchOn = ((dc_status & (0x1<<5)) == 0) ? false : true;
                        m_status.RudderSwitchOn = ((dc_status & (0x1<<6)) == 0) ? false : true;
                        m_status.KE4SwitchOn = ((dc_status & (0x1<<7)) == 0) ? false : true;
                        m_status.BreakSwitchOn = ((dc_status & (0x1<<8)) == 0) ? false : true;
                        m_status.FrontFanOn = ((dc_status & (0x1<<9)) == 0) ? false : true;
                        m_status.BackFanOn = ((dc_status & (0x1<<10)) == 0) ? false : true;
                        m_status.PumpSwitchOn = ((dc_status & (0x1 << 11)) == 0) ? false : true;
                        m_status.RadarSwitchOn = ((dc_status & (0x1 << 12)) == 0) ? false : true;
                        m_status.CameraSwitchOn = ((dc_status & (0x1 << 13)) == 0) ? false : true;
                        m_status.SwitchSwitchOn = ((dc_status & (0x1 << 14)) == 0) ? false : true;
                        m_status.GPSSwitchOn = ((dc_status & (0x1 << 15)) == 0) ? false : true;
                        Invoke((MethodInvoker)delegate
                        {
                            groupBox33.Text = "蜂鸣器控制" + (m_status.SpeekerSwitchOn ? "(开)" : "(关)");
                            groupBox34.Text = "串口服务电源" + (m_status.SerialServerSwitchOn ? "(开)" : "(关)");
                            groupBox35.Text = "WIFI电源控制" + (m_status.WifiSwitchOn ? "(开)" : "(关)");
                            groupBox16.Text = "激光雷达电源控制" + (m_status.RaserSwitchOn ? "(开)" : "(关)");
                            groupBox17.Text = "舵机电源控制" + (m_status.RudderSwitchOn ? "(开)" : "(关)");
                            groupBox18.Text = "KE4电源控制" + (m_status.KE4SwitchOn ? "(开)" : "(关)");
                            groupBox19.Text = "翻斗电源控制" + (m_status.BreakSwitchOn ? "(开)" : "(关)");
                            groupBox20.Text = "前仓风机电源控制" + (m_status.FrontFanOn ? "(开)" : "(关)");
                            groupBox21.Text = "后仓风机电源控制" + (m_status.BackFanOn ? "(开)" : "(关)");
                            groupBox22.Text = "仓底泵电源控制" + (m_status.PumpSwitchOn ? "(开)" : "(关)");
                            groupBox23.Text = "4G雷达电源控制" + (m_status.RadarSwitchOn ? "(开)" : "(关)");
                            groupBox25.Text = "摄像头电源控制" + (m_status.CameraSwitchOn ? "(开)" : "(关)");
                            groupBox26.Text = "交换机电源控制" + (m_status.SwitchSwitchOn ? "(开)" : "(关)");
                            groupBox24.Text = "主控/GPS电源控制" + (m_status.GPSSwitchOn ? "(开)" : "(关)");
                        });
                        break;

                    case 0x09: // 声纳，LTE等
                        index += 2;
                        if (index > command.Length)
                        {
                            writeLog("Invalid 0x" + cmd.ToString("X2") + " command data from master control!\r\n");
                            return null;
                        }
                        int dam_status = (command[di] | (command[di + 1] << 8));
                        m_status.ForwardSonarOn = ((dam_status & 0x1) == 0) ? false : true;
                        m_status.MultiWaveOn = ((dam_status & 0x2) == 0) ? false : true;
                        m_status.LteSwitchOn = ((dam_status & 0x4) == 0) ? false : true;
                        m_status.ComputerSwitchOn = ((dam_status & 0x8) == 0) ? false : true;
                        Invoke((MethodInvoker)delegate
                        {
                            groupBox27.Text = "前视声纳电源控制" + (m_status.ForwardSonarOn ? "(开)" : "(关)");
                            groupBox29.Text = "LTE电源控制" + (m_status.LteSwitchOn ? "(开)" : "(关)");
                            groupBox30.Text = "工控机电源控制" + (m_status.ComputerSwitchOn ? "(开)" : "(关)");
                            groupBox28.Text = "多波速电源控制" + (m_status.MultiWaveOn ? "(开)" : "(关)");
                        });
                        break;

                    case 0x10:
                        index += 6;
                        if (index > command.Length)
                        {
                            writeLog("Invalid 0x" + cmd.ToString("X2") + " command data from master control!\r\n");
                            return null;
                        }
                        //发电机转出的12V电压
                        float DC_12V = (float)(command[di] | (command[di + 1] << 8)); // 0-30v map to 0-30000
                        DC_12V /= 1000;
                        break;

                    case 0x11:
                        index += 6;
                        if (index > command.Length)
                        {
                            writeLog("Invalid 0x" + cmd.ToString("X2") + " command data from master control!\r\n");
                            return null;
                        }
                        //发电机输出的电流电压
                        float AC_I_VOUT = (float)(command[di+2] | (command[di + 3] << 8)); // 0-50A map to 0-30000
                        float AC_V_VOUT = (float)(command[di+4] | (command[di + 5] << 8)); // 0-5v map to 0-30000
                        AC_I_VOUT = (AC_I_VOUT*50)/30000;
                        AC_V_VOUT = (AC_V_VOUT * 5*44) / 30000; // 0-30000 map to 0-5 --> 0-5 map to 0-220

                        Invoke((MethodInvoker)delegate
                        {
                            aGauge3.Value = AC_I_VOUT;
                            aGauge1.Value = AC_V_VOUT;
                        });
                        break;

                    case 0x60:
                        index += 6;
                        break;

                    case 0x61:
                        index += 6;
                        break;

                    case 0x70:
                        index += 6;
                        if (index > command.Length)
                        {
                            writeLog("Invalid 0x" + cmd.ToString("X2") + " command data from master control!\r\n");
                            return null;
                        }
                        //电池电压
                        float BAT_12V = (float)(command[di] | (command[di + 1] << 8)); // 0-30v map to 0-30000
                        BAT_12V = BAT_12V / 1000;
                        //现在的12V电压
                        float a12V_EXT = (float)(command[di+2] | (command[di + 3] << 8)); // 0-30v map to 0-30000
                        a12V_EXT = a12V_EXT  / 1000;
                        //现在输出的24V电压
                        float a24V_EXT = (float)(command[di+4] | (command[di + 5] << 8)); //0-30v map to 0-30000
                        a24V_EXT = a24V_EXT / 1000;

                        Invoke((MethodInvoker)delegate
                        {
                            aGauge16.Value = BAT_12V;
                            aGauge16.CapText = "电压" + BAT_12V.ToString()+"V";
                            aGauge7.Value = a12V_EXT;
                            aGauge7.CapText = "电压" + a12V_EXT.ToString() + "V";
                            aGauge11.Value = a24V_EXT;
                            aGauge11.CapText = "电压" + a24V_EXT.ToString() + "V";
                        });
                        break;

                    case 0x71:
                        index += 6;
                        if (index > command.Length)
                        {
                            writeLog("Invalid 0x" + cmd.ToString("X2") + " command data from master control!\r\n");
                            return null;
                        }
                        //电池输出的24v电压
                        float a24V_BAT = (float)(command[di] | (command[di + 1] << 8));
                        a24V_BAT = a24V_BAT * 30 / 30000;
                        //总线上的电流
                        float I_12V = (float)(command[di + 2] | (command[di + 3] << 8)); // 1-5 map to 0-30000; 1=10A
                        I_12V = 10* (I_12V * 5 / 30000);
                        float I_24V = (float)(command[di + 4] | (command[di + 5] << 8));
                        I_24V = 10 * (I_24V * 5 / 30000);

                        Invoke((MethodInvoker)delegate
                        {
                            //显示在电池电流上
                            aGauge15.Value = I_12V;
                            aGauge15.CapText = "电流" + I_12V.ToString() + "A";
                            //显示在总线回路的电流
                            aGauge12.Value = I_24V;
                            aGauge12.CapText = "电流" + I_24V.ToString() + "A";
                        });
                        break;

                    case 0x03: //转速表
                        index += 4;
                        if (index > command.Length)
                        {
                            writeLog("Invalid 0x" + cmd.ToString("X2") + " command data from master control!\r\n");
                            return null;
                        }
                        //speed = (adc-6586.3)/8.955
                        Int32 speed = (command[di] | (command[di + 1]<<8) | (command[di + 2]<<16) | (command[di + 3] << 24)   );
                        speed = (speed - 6586) / 9;

                        Invoke((MethodInvoker)delegate
                        {
                            aGauge2.Value = speed/10; //speed
                        });
                        break;

                    case 0x1A: // 转向电流，舵角，油量百分比
                        index += 6;
                        if (index > command.Length)
                        {
                            writeLog("Invalid 0x" + cmd.ToString("X2") + " command data from master control!\r\n");
                            return null;
                        }
                        int current = command[di] | (command[di + 1] << 8);
                        int angle = command[di+2] | (command[di + 3] << 8); // 1000 - 2000 map to 0-100
                        angle = (angle - 1000) / 10;
                        int oil_mass = command[di + 4] | (command[di + 5] << 8);

                        Invoke((MethodInvoker)delegate
                        {
                            aGauge4.Value = current;
                            aGauge5.Value = angle;
                            aGauge17.Value = oil_mass;
                        });
                        break;
                    
                    case 0xA1:
                        index += 3;
                        if (index > command.Length)
                        {
                            writeLog("Invalid 0x" + cmd.ToString("X2") + " command data from master control!\r\n");
                            return null;
                        }
                        int front = command[di];
                        int middle = command[di+1];
                        int back = command[di + 2];
                        Invoke((MethodInvoker)delegate
                        {
                            frontLeakLabel.Text = front == 1 ? "前：有" : "前：无";
                            middleLeakLabel.Text = middle == 1 ? "中：有" : "中：无";
                            backLeakLabel.Text = back == 1 ? "后：有" : "后：无";
                        });
                        break;

                    default:
                        writeLog("Invalid command from master control!\r\n");
                        return null;
                }
            }

            return null;
        }

        public static string dumpBytes(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("X02"));
                sb.Append(" ");
            }
            return sb.ToString();
        }

        public void updateUI()
        {
            Invoke((MethodInvoker)delegate
            {
                // TODO
            });
        }

        private void btn_engin_open_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x02, 0x04, 0x01, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_engin_start_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x02, 0x03, 0x03, 0xF4, 0x01 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_engine_stop_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x02, 0x04, 0x00, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_generator_start_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x02, 0x02, 0x03, 0xB8, 0x0B };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_generator_stop_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x02, 0x02, 0x00, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_motor_power_open_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x04, 0x01, 0x01, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_motor_power_close_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x04, 0x01, 0x00, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_motor_charge_open_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x04, 0x02, 0x01, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_motor_charge_close_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x04, 0x02, 0x00, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_left_motor_open_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x04, 0x03, 0x01, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_left_motor_close_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x04, 0x03, 0x00, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_right_motor_open_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x04, 0x04, 0x01, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_right_motor_close_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x04, 0x04, 0x00, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_left_motor_forward_open_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x05, 0x01, 0x01, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_left_motor_forward_close_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x05, 0x01, 0x00, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_left_motor_back_open_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x05, 0x02, 0x01, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_left_motor_back_close_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x05, 0x02, 0x00, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_right_motor_forward_open_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x05, 0x03, 0x01, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_right_motor_forward_close_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x05, 0x03, 0x00, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_right_motor_back_open_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x05, 0x04, 0x01, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_right_motor_back_close_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x05, 0x04, 0x00, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_multi_wave_open_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x09, 2, 1, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_antenna_start_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x08, 0x01, 0x01, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_antenna_stop_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x08, 0x01, 0x00, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_laser_open_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x08, 0x06, 0x01, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_laser_close_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x08, 0x06, 0x00, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_rudder_open_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x08, 0x07, 0x01, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_rudder_close_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x08, 0x07, 0x00, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_ke4_open_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x08, 0x08, 0x01, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_ke4_close_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x08, 0x08, 0x00, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_break_open_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x08, 0x09, 0x01, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_break_close_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x08, 0x09, 0x00, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_front_fan_open_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x08, 10, 1, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_front_fan_close_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x08, 10, 0, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_back_fan_open_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x08, 11, 1, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_back_fan_close_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x08, 11, 0, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_pump_open_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x08, 12, 1, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_pump_close_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x08, 12, 0, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_radar_open_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x08, 13, 1, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_radar_close_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x08, 13, 0, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_camera_open_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x08, 14, 1, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_camera_close_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x08, 14, 0, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_gps_open_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x08, 16, 1, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_gps_close_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x08, 16, 0, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_switch_open_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x08, 15, 1, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_switch_close_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x08, 15, 0, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_sonar_open_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x09, 1, 1, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_sonar_close_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x09, 1, 0, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_multi_wave_close_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x09, 2, 0, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_lte_open_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x09, 3, 1, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_lte_close_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x09, 3, 0, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_computer_open_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x09, 4, 1, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void btn_computer_close_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x09, 4, 0, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }


        private void updateDamSwitchStatus()
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x08, 0x03, 0x01, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] cmd = new byte[] { 0x01, 0x12, 0x01, 0x08, 0x03, 0x00, 0x00, 0x00 };
            sendCommand(m_parser.packResponse(cmd));
        }
    }

    public class SwitchStatus
    {
        public float FrontHumidity = 0.0f;
        public float FrontTemperature = 0.0f;

        public float BackHumidity = 0.0f;
        public float BackTemperature = 0.0f;

        //发电机，发动机
        public bool EnginSwitchOn = false;
        public bool GeneratorSwitchOn = false;

        //侧推
        public bool MotorPowerOn = false;
        public bool MotorChargeOn = false;

        public bool LeftMotorOn = false;
        public bool RightMotorOn = false;

        public bool LeftMotorForwardOn = false;
        public bool LeftMotorBackOn = false;

        public bool RightMotorForwardOn = false;
        public bool RightMotorBackOn = false;
        //DC配电箱
        public bool SpeekerSwitchOn = false;
        public bool SerialServerSwitchOn = false;
        public bool WifiSwitchOn= false;
        public bool AntenaUpOn = false;
        public bool AntenaDownOn = false;

        public bool RaserSwitchOn = false;
        public bool RudderSwitchOn = false;
        public bool KE4SwitchOn = false;
        public bool BreakSwitchOn = false;
        public bool FrontFanOn = false;
        public bool BackFanOn = false;
        public bool PumpSwitchOn = false;
        public bool RadarSwitchOn = false;
        public bool CameraSwitchOn = false;
        public bool SwitchSwitchOn = false;
        public bool GPSSwitchOn = false;

        //DAM 09
        public bool ForwardSonarOn = false;
        public bool MultiWaveOn = false;
        public bool LteSwitchOn = false;
        public bool ComputerSwitchOn = false;
    }

}
