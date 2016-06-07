using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            string reqURL = "http://30.24.6.196:8080/";
            string fileName = "gps_dte.bin";

            if (args.Length > 0 && args.Length != 2)
            {
                Console.WriteLine("Usage: Program url file");
                Console.WriteLine("url: HTTP url to sync data, default is http://30.24.6.196:8080/");
                Console.WriteLine("file: sync data to this file, default is " + fileName + ".");
                return;
            }

            if (args.Length == 2)
            {
                reqURL = args[0];
                fileName = args[1];
            }

            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);
            long fileLength = fs.Length;
            fs.Seek(0, SeekOrigin.End);

            while (true)
            {
                try
                {
                    WebClient client = new WebClient();
                    client.Headers.Add("START", "" + fileLength);
                    byte[] data = client.DownloadData(reqURL);
                    if (data.Length > 0)
                    {
                        fs.Write(data, 0, data.Length);
                        fs.Flush();
                        fileLength += data.Length;
                        Console.WriteLine("Sync " + data.Length + " bytes! DTE " + CheckDTE(data) + " bytes!");
                    }

                    if (data.Length < 1)
                    {
                        Thread.Sleep(1000);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Download error:" + e.ToString());
                    Thread.Sleep(500);
                }
            }

            fs.Close();
        }

        static int CheckDTE(byte[] data)
        {
            int dteSize = 0;
            for (int idx = 0; idx < data.Length; ++idx)
            {
                if (data[idx] > 127)
                {
                    dteSize += (data[idx] & 127);
                }
            }
            return dteSize;
        }
    }
}
