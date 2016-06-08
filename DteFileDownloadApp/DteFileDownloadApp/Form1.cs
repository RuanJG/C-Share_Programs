using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Threading;

namespace DteFileDownloadApp
{
    public partial class DteFileDownloadApp : Form
    {
        string reqURL = "http://30.24.6.196:8080/";
        string fileName = "gps_dte.bin";
        Thread syncThread;
        Thread delThread;

        public DteFileDownloadApp()
        {
            InitializeComponent();
            urlTextBox.Text = reqURL;
            log("startup ok");
        }

        private void urlTextBox_TextChanged(object sender, EventArgs e)
        {
            reqURL = urlTextBox.Text;
        }

        private void downloadButton_Click(object sender, EventArgs e)
        {
            //syncMainLoop();
            if (syncThread != null)
            {
                //syncThread.Join();
                syncThread.Abort();
                syncThread = null;
            }
            syncThread = new Thread(new ThreadStart(syncMainLoop));
            syncThread.Start();
            downloadButton.Enabled = false;
        }

        private void delButton_Click(object sender, EventArgs e)
        {
            if (delThread != null)
            {
                //syncThread.Join();
                delThread.Abort();
                delThread = null;
            }
            delThread = new Thread(new ThreadStart(delFileLoop));
            delThread.Start();
            delButton.Enabled = false;
        }


        private void stopButton_Click(object sender, EventArgs e)
        {
            if (syncThread != null)
            {
                //syncThread.Join();
                syncThread.Abort();
                syncThread = null;
                downloadButton.Enabled = true;
            }
            if (delThread != null)
            {
                //syncThread.Join();
                delThread.Abort();
                delThread = null;
                delButton.Enabled = true;
            }
        }


        private void log(string str)
        {
            consoleTextBox.AppendText(str + "\r\n");
        }
        private void threadLog(string str)
        {
            consoleTextBox.BeginInvoke(new MethodInvoker(delegate { log(str); }));
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

        private void delFileLoop()
        {
            try
            {
                threadLog("准备删除远程文件....");

                WebClient client = new WebClient();
                client.Headers.Clear();
                client.Headers.Add("START", "" + -1);
                byte[] res = client.DownloadData(reqURL);
                if (res.Length > 0 && res[0] == 1)
                {
                    threadLog("远程文件已删除");
                }
                else
                {
                    threadLog("远程文件删除失败");
                }
            }
            catch (Exception e)
            {
                threadLog("connect remote error:" + e.ToString());
                Thread.Sleep(100);
            }
            delButton.BeginInvoke(new MethodInvoker(delegate { delButton.Enabled = true; }));
        }
        private void syncMainLoop()
        {
            int retryTimes = 10; //10 * 100 ms
            int retry = retryTimes;
            long fileLength = 0;
            FileStream fs;

            try
            {
                //del the old file
                if (File.Exists(fileName))
                {
                    File.WriteAllBytes(fileName, new byte[0]);
                    fileLength = 0;
                }
            }
            catch (Exception e)
            {
                threadLog("Download error:" + e.ToString());
                downloadButton.BeginInvoke(new MethodInvoker(delegate { downloadButton.Enabled = true; }));
                return;
            }

            while (0 < retry--)
            {
                try
                {
                    WebClient client = new WebClient();
                    client.Headers.Add("START", "" + fileLength);
                    byte[] data = client.DownloadData(reqURL);
                    if (data.Length > 0)
                    {
                        fs = new FileStream(fileName, FileMode.OpenOrCreate);
                        fileLength = fs.Length;
                        fs.Seek(0, SeekOrigin.End);
                        fs.Write(data, 0, data.Length);
                        fs.Flush();
                        fs.Close();
                        fileLength += data.Length;
                        threadLog("Sync " + data.Length + " bytes! DTE " + CheckDTE(data) + " bytes!");
                        threadLog("已保存到 " + fileName);
                        retry = retryTimes;
                    }
                    else
                    {
                        Thread.Sleep(100);
                        threadLog("retry "+retry);
                    }
                }
                catch (Exception e)
                {
                    threadLog("Download error:" + e.ToString());
                    Thread.Sleep(500);
                }

            }
           

            threadLog("完成！！");
            downloadButton.BeginInvoke(new MethodInvoker(delegate { downloadButton.Enabled = true; }));
        }


    }
}
