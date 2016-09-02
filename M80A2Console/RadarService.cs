using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace M80A2Console
{
    public class RadarService
    {
        private static int LeftRightMargin = 8;
        private static string TcpHost = "127.0.0.1";
        private static int TcpPort = 6789;
        private static int UdpPort = 6669;

        private static Thread m_thread1;
        private static Thread m_thread2;
        private static volatile bool m_quit = false;

        public static void start()
        {
            stop();

            m_quit = false;
            m_thread1 = new Thread(new ThreadStart(run1));
            m_thread1.Start();

            m_thread2 = new Thread(new ThreadStart(run2));
            m_thread2.Start();
        }

        public static void stop()
        {
            m_quit = true;
            if (m_thread1 != null)
            {
                if (!m_thread1.Join(500))
                {
                    m_thread1.Abort();
                    Thread.Sleep(100);
                }
                m_thread1 = null;
            }

            if (m_thread2 != null)
            {
                if (!m_thread2.Join(500))
                {
                    m_thread2.Abort();
                    Thread.Sleep(100);
                }
                m_thread2 = null;
            }
        }

        private static void run1()
        {
            while (!m_quit)
            {
                TcpClient client = new TcpClient();
                try
                {
                    client.Connect(TcpHost, TcpPort);
                    run1(client);
                }
                catch (Exception e)
                {
                    Console.WriteLine("tcp connect exception: " + e.Message);
                }
                finally
                {
                    if (client.Connected)
                    {
                        client.Close();
                    }
                }

                Thread.Sleep(100);
            }
        }

        private static double readYaw()
        {
            return 0; // TODO read yaw
        }

        private static byte[] getHDT(double yaw)
        {
            return null; // TODO
        }

        private static void run1(TcpClient client)
        {
            byte[] hbtBytes = Encoding.Default.GetBytes("$18F,HBT,1852,-1852*18\r\n"); // TODO Checksum
            double lastYaw = 0;
            DateTime lastHbtTime = DateTime.Now;

            client.SendTimeout = 500;

            while (!m_quit)
            {
                DateTime now = DateTime.Now;
                if (now.Subtract(lastHbtTime).TotalSeconds > 2)
                {
                    client.Client.Send(hbtBytes);
                    lastHbtTime = now;
                }

                double yaw = readYaw();
                if (yaw != lastYaw)
                {
                    client.Client.Send(getHDT(yaw));
                    lastYaw = yaw;
                }

                Thread.Sleep(100);
            }
        }

        private static void run2()
        {
            while (!m_quit)
            {
                try
                {
                    UdpClient client = new UdpClient();
                    client.EnableBroadcast = true;

                    while (!m_quit)
                    {
                        IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, UdpPort);
                        byte[] data = client.Receive(ref endpoint);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("udp client exception: " + e.Message);
                }

                Thread.Sleep(100);
            }
        }

        private static void parseRadar(byte[] data)
        {
            if (data.Length < 6)
            {
                Console.WriteLine("radar data error");
                return;
            }

            double leftDistance = 200.0;
            double rightDistance = 200.0;

            int startIndex = 0;
            int lineSize = data[5]; // always 32
            startIndex += 8;

            for (int index = 0; index < lineSize; ++index)
            {
                int largeRange = ((data[startIndex + 7] << 8) & 0xFF00) + data[startIndex + 6];
                int angle = ((data[startIndex + 9] << 8) & 0xFF00) + data[startIndex + 8];
                int smallRange = ((data[startIndex + 13] << 8) & 0xFF00) + data[startIndex + 12];

                if (angle > 180)
                {
                    angle -= 360;
                }

                startIndex += 24;
                // 512 byte following
                for (int pointIdx = 0; pointIdx < 512; ++pointIdx)
                {
                    byte gray = data[startIndex + pointIdx];
                    if (gray > 190)
                    {
                        double distance = smallRange + (largeRange - smallRange)  * pointIdx / 512;
                        if (angle > double.Epsilon)
                        {
                            if (angle < 90)
                            {
                                double angleRadius = angle * Math.PI / 180.0;
                                if (distance * Math.Sin(angleRadius) < LeftRightMargin)
                                {
                                    distance = distance * Math.Cos(angleRadius);
                                    if (rightDistance > distance)
                                    {
                                        rightDistance = distance;
                                    }
                                }
                            }
                        }
                        else if (angle < -double.Epsilon)
                        {
                            if (angle > -90)
                            {
                                double angleRadius = -angle * Math.PI / 180.0;
                                if (distance * Math.Sin(angleRadius) < LeftRightMargin)
                                {
                                    distance = distance * Math.Cos(angleRadius);
                                    if (leftDistance > distance)
                                    {
                                        leftDistance = distance;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (leftDistance > distance)
                            {
                                leftDistance = distance;
                            }

                            if (rightDistance > distance)
                            {
                                rightDistance = distance;
                            }
                        }
                        break;
                    }
                }
                startIndex += 512;
            }

            // TODO update left right obstacle distance.
        }
    }
}
