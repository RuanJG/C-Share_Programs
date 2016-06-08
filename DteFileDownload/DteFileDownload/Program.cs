using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DteFileDownload
{
    class Program
    {
        static void Main(string[] args)
        {
            string reqURL = "http://30.24.6.196:8080/";
            string fileName = "gps_dte.bin";
            long maxFileSize = 1024 * 1024 * 500; //50MB

            if (args.Length > 0 && args.Length != 2)
            {
                Console.WriteLine("Usage: Program url file");
                Console.WriteLine("url: HTTP url to sync data, default is http://30.24.6.196:8080/");
                Console.WriteLine("file: sync data to this file, default is " + fileName + ".");
                return;
            }

            if (args.Length >= 2)
            {
                reqURL = args[0];
                fileName = args[1];
            }
            if (args.Length >= 3)
            {
                int mb = int.Parse(args[2]);
                if (mb >= 0)
                {
                    maxFileSize = mb * 1024 * 1024;
                }
            }

            //del the old file
            if (File.Exists(fileName))
            {
                File.WriteAllBytes(fileName, new byte[0]);
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
                        Console.WriteLine("已保存到 "+fileName);
                        Thread.Sleep(500);
                    }
                    else
                    {
                        if ( fileLength >= maxFileSize )
                        {
                            if( 0 != maxFileSize ){
                                Console.WriteLine("远程文件大于 "+""+maxFileSize+" Byte，将执行删除操作 ！！");
                            }
                            client.Headers.Clear();
                            client.Headers.Add("START", "" + -1);
                            byte[] res = client.DownloadData(reqURL);
                            if (res.Length > 0 && res[0] == 1)
                            {
                                Console.WriteLine("远程文件已删除");
                            }
                            else
                            {
                                Console.WriteLine("远程文件删除失败");
                            }
                        }
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Download error:" + e.ToString());
                    Thread.Sleep(500);
                }
                
            }
            fs.Close();

            Console.WriteLine("完成！！");
            while(true)
                Thread.Sleep(1000);
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
