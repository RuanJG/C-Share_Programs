using System;
using System.Threading;
using System.IO;
using System.IO.Ports;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReplayGPSDTM
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "gps_dte.bin";
            string gpsPort = "COM4";
            int gpsBitrate = 19200;
            string dtePort = "COM5";
            int dteBitrate = 9600;
            int delay = 15;

            if (args.Length > 0 && args.Length != 6)
            {
                Console.WriteLine("Usage: Program file GPS_COM bitrate DTE_COM bitrate delay");
                Console.WriteLine("file: save gps and dte data to this file. default is " + fileName + ".");
                Console.WriteLine("GPS_COM: serial port for reading GPS data, default is COM2.");
                Console.WriteLine("bitrate: GPS serial port bitrate, default is 19200.");
                Console.WriteLine("DTE_COM: serial port for read DTE data, default is COM3.");
                Console.WriteLine("bitrate: DTE serial port bitrate, default is 9600.");
                Console.WriteLine("delay: replay speed, smaller for faster, min value is 0, default is 10.");
                return;
            }

            if (args.Length == 6)
            {
                fileName = args[0];
                gpsPort = args[1];
                gpsBitrate = int.Parse(args[2]);
                dtePort = args[3];
                dteBitrate = int.Parse(args[4]);
                delay = int.Parse(args[5]);
            }

            SerialPort _gpsPort = null;
            SerialPort _dtePort = null ;
            FileStream _fileStream = null;

            try
            {
                _gpsPort = new SerialPort(gpsPort, gpsBitrate, Parity.None, 8, StopBits.One);
                _dtePort = new SerialPort(dtePort, dteBitrate, Parity.None, 8, StopBits.One);
                
                _gpsPort.Open();
                _dtePort.Open();

                byte[] buffer = new byte[8192];
                int startPos = 0;
                int dataSize = 0;
                int readLen = 0;

                _fileStream = new FileStream(fileName, FileMode.Open);
                while ((readLen = _fileStream.Read(buffer, startPos + dataSize, buffer.Length - dataSize - startPos)) > 0)
                {
                    dataSize += readLen;
                    while (true)
                    {
                        if (dataSize < 1)
                        {
                            break;
                        }

                        SerialPort _serialPort = _gpsPort;
                        byte head = buffer[startPos];
                        if ((head & 128) == 128)
                        {
                            _serialPort = _dtePort;
                        }

                        byte[] rawData = new byte[head & 127];
                        if (dataSize < rawData.Length + 1)
                        {
                            break;
                        }

                        Array.Copy(buffer, startPos + 1, rawData, 0, rawData.Length);
                        _serialPort.Write(rawData, 0, rawData.Length);

                        startPos += (rawData.Length + 1);
                        dataSize -= (rawData.Length + 1);

                        Thread.Sleep(delay);
                    }

                    if (dataSize > 0)
                    {
                        byte[] bakbuf = new byte[dataSize];
                        Array.Copy(buffer, startPos, bakbuf, 0, bakbuf.Length);
                        Array.Copy(bakbuf, 0, buffer, 0, bakbuf.Length);
                    }
                    startPos = 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.ToString());
                return;
            }
            finally
            {
                if (_gpsPort != null && _gpsPort.IsOpen)
                {
                    _gpsPort.Close();
                }

                if (_dtePort != null && _dtePort.IsOpen)
                {
                    _dtePort.Close();
                }

                if (_fileStream != null)
                {
                    _fileStream.Close();
                }
            }

            Console.WriteLine("Done!");
        }
    }   
}
