using System;
using System.IO;
using System.IO.Ports;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;

namespace DataBuffer
{
    class Program
    {
        static void Main(string[] args)
        {
            object locker = new object();
            string fileName = "gps_dte.bin";
            string gpsPort = "COM1";
            int gpsBitrate = 19200;
            string dtePort = "COM4";
            int dteBitrate = 9600;
            short httpPort = 8080;

            if (args.Length > 0 && args.Length != 6)
            {
                Console.WriteLine("Usage: Program file GPS_COM bitrate DTE_COM bitrate port");
                Console.WriteLine("file: save gps and dte data to this file. default is " + fileName + ".");
                Console.WriteLine("GPS_COM: serial port for reading GPS data, default is COM1.");
                Console.WriteLine("bitrate: GPS serial port bitrate, default is 19200.");
                Console.WriteLine("DTE_COM: serial port for read DTE data, default is COM4.");
                Console.WriteLine("bitrate: DTE serial port bitrate, default is 9600.");
                Console.WriteLine("port: HTTP server port to sync data, default is 8080.");
                return;
            }

            if (args.Length == 6)
            {
                fileName = args[0];
                gpsPort = args[1];
                gpsBitrate = int.Parse(args[2]);
                dtePort = args[3];
                dteBitrate = int.Parse(args[4]);
                httpPort = short.Parse(args[5]);
            }

            FileWriter fileWriter = new FileWriter(fileName, locker);
            FileReader fileReader = new FileReader(fileName, locker);
            UartReader gpsReader = new UartReader(gpsPort, gpsBitrate, fileWriter, DataType.TYPE_GPS);
            UartReader dteReader = new UartReader(dtePort, dteBitrate, fileWriter, DataType.TYPE_DTE);
            HttpServer httpServer = new HttpServer(httpPort, fileReader);
            httpServer.Start();

            //ThreadTest test = new ThreadTest(gpsReader, dteReader);
            //test.StartTest();

            while (true)
            {
                string cmd = Console.ReadLine();
                cmd = cmd.ToLower();
                if (cmd == "quit" || cmd == "exit")
                {
                    break;
                }
            }

            //test.StopTest();
            httpServer.Stop();
            gpsReader.Close();
            dteReader.Close();
        }
    }

    /*class ThreadTest
    {
        private UartReader _uart1;
        private UartReader _uart2;
        private Thread _thread;
        private volatile bool _quit;

        public ThreadTest(UartReader uart1, UartReader uart2)
        {
            _uart1 = uart1;
            _uart2 = uart2;
        }

        public void StartTest()
        {
            _quit = false;
            _thread = new Thread(new ThreadStart(Run));
            _thread.Start();
        }

        private void Run()
        {
            Random rand = new Random((int)DateTime.Now.ToBinary());
            while (!_quit)
            {
                int size = rand.Next(1, 400);
                byte val = (byte)((size % 2 == 0) ? 97 : 98);
                UartReader uart = (size % 2 == 0) ? _uart1 : _uart2;

                byte[] data = new byte[size];
                for (int idx = 0; idx < size; ++idx)
                {
                    data[idx] = val;
                }

                uart.Send(data);
            }
        }

        public void StopTest()
        {
            if (_thread == null)
            {
                return;
            }

            _quit = true;
            _thread.Join();
            _thread = null;
        }
    }*/

    class HttpServer
    {
        private string _prefix;
        private HttpListener _httpListener;
        private FileReader _fileReader;

        public HttpServer(short port, FileReader reader)
        {
            _httpListener = new HttpListener();
            _prefix = "http://+:" + port + "/";
            _httpListener.Prefixes.Add(_prefix);
            _fileReader = reader;
        }

        public void Start()
        {
            _httpListener.Start();
            Console.WriteLine("Http server start listening...");

            _httpListener.BeginGetContext(new AsyncCallback(ListenerCallback), _httpListener);
        }

        public void Stop()
        {
            _httpListener.Stop();
            Console.WriteLine("Http server stop listening...");
        }

        private void ListenerCallback(IAsyncResult result)
        {
            HttpListener listener = (HttpListener)result.AsyncState;
            HttpListenerContext context = listener.EndGetContext(result);

            AsyncCallback callback = new AsyncCallback(ListenerCallback);
            listener.BeginGetContext(callback, listener);

            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            int startFrom = 0;
            string startString = request.Headers.Get("START");
            if (startString != null && startString.Length > 0)
            {
                startFrom = int.Parse(startString);
            }

            byte[] responseData = _fileReader.Read(startFrom, 1024 * 16);
            response.ContentLength64 = responseData.Length;
            response.OutputStream.Write(responseData, 0, responseData.Length);
            response.OutputStream.Close();
        }
    }

    enum DataType
    {
        TYPE_GPS,
        TYPE_DTE
    }

    class UartReader
    {
        private SerialPort _serialPort;
        private FileWriter _fileWriter;
        private DataType _dataType;

        public UartReader(string port, int bitrate, FileWriter writer, DataType type)
        {
            _fileWriter = writer;
            _dataType = type;

            try
            {
                _serialPort = new SerialPort(port, bitrate, Parity.None, 8, StopBits.One);
                _serialPort.Open();
                _serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open " + port + ":" + e.ToString());
            }
        }

        public void Close()
        {
            if (_serialPort != null && _serialPort.IsOpen)
            {
                _serialPort.Close();
            }
        }

        public void Send(byte[] data)
        {
            if (_serialPort != null && _serialPort.IsOpen)
            {
                _serialPort.Write(data, 0, data.Length);
            }
        }

        void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int size = _serialPort.BytesToRead;
            if (size < 1)
            {
                return;
            }

            byte[] data = new byte[size];
            for (int idx = 0; idx < size; idx++)
            {
                data[idx] = (byte)_serialPort.ReadByte();
            }
            _fileWriter.Write(data, _dataType);
        }
    }

    class FileReader
    {
        private string _fileName;
        private object _fileLock;

        public FileReader(string file, object lockobj)
        {
            _fileName = file;
            _fileLock = lockobj;
        }

        public byte[] Read(int start, long len)
        {
            FileStream fs = null;
            byte[] result = new byte[0];

            lock (_fileLock)
            {
                try
                {
                    fs = new FileStream(_fileName, FileMode.Open);
                    long total = fs.Length;
                    if (start + len > total)
                    {
                        len = total - start;
                    }

                    if (start >= total | len < 1)
                    {
                        return new byte[0];
                    }

                    fs.Seek(start, SeekOrigin.Begin);
                    result = new byte[len];                    
                    int rdLen = fs.Read(result, 0, result.Length);
                    if (rdLen != len)
                    {
                        Console.WriteLine("Error! Read data size small than required size!!!");
                        Console.WriteLine("Read return " + rdLen + ", required " + len);
                        return new byte[0];
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Cannot read file:" + e.ToString());
                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Close();
                    }
                }

                return result;
            }
        }
    }

    class FileWriter
    {
        private string _fileName;
        private object _fileLock;

        public FileWriter(string file, object lockobj)
        {
            _fileName = file;
            _fileLock = lockobj;
        }

        public void Write(byte[] raw, DataType type)
        {
            byte typeInt = 0;
            if (type == DataType.TYPE_DTE)
            {
                typeInt = 128;
            }

            if (raw.Length < 1)
            {
                return;
            }

            string fname = "gps.bin";
            if (type == DataType.TYPE_DTE)
            {
                fname = "dte.bin";
            }

            FileStream outf = new FileStream(fname, FileMode.OpenOrCreate);
            outf.Seek(0, SeekOrigin.End);
            outf.Write(raw, 0, raw.Length);
            outf.Close();

            FileStream fs = null;
            lock (_fileLock)
            {
                try
                {
                    int start = 0;
                    int len = 0;

                    fs = new FileStream(_fileName, FileMode.OpenOrCreate);
                    fs.Seek(0, SeekOrigin.End);

                    while (start < raw.Length)
                    {
                        len = 127;
                        if (start + len > raw.Length)
                        {
                            len = raw.Length - start;
                        }

                        fs.WriteByte((byte)(typeInt + len));
                        fs.Write(raw, start, len);
                        start += len;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Save file error:" + e.ToString());
                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Close();
                    }
                }
            }
        }
    }
}
