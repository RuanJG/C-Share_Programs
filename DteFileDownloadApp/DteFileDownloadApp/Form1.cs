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

namespace DteFileDownloadApp
{
    public partial class DteFileDownloadApp : Form
    {
        string reqURL = "http://30.24.6.196:8080/";
        string fileName = "gps_dte.bin";


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
            syncMainLoop();
        }

        private void delButton_Click(object sender, EventArgs e)
        {
            log("准备删除远程文件....");

            WebClient client = new WebClient();
            client.Headers.Clear();
            client.Headers.Add("START", "" + -1);
            byte[] res = client.DownloadData(reqURL);
            if (res.Length > 0 && res[0] == 1)
            {
                log("远程文件已删除");
            }
            else
            {
                log("远程文件删除失败");
            }
        }



        private void log(string str)
        {
            consoleTextBox.AppendText(str + "\r\n");
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

        private void syncMainLoop()
        {
            int retry = 10;
            //del the old file
            if (File.Exists(fileName))
            {
                File.WriteAllBytes(fileName, new byte[0]);
            }

            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);
            long fileLength = fs.Length;
            fs.Seek(0, SeekOrigin.End);

            while (0 < retry--)
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
                        log("Sync " + data.Length + " bytes! DTE " + CheckDTE(data) + " bytes!");
                        log("已保存到 " + fileName);
                    }
                    else
                    {
                        //Thread.Sleep(500);
                        log("retry "+retry);
                    }
                }
                catch (Exception e)
                {
                    log("Download error:" + e.ToString());
                    //Thread.Sleep(500);
                }

            }
            fs.Close();

            log("完成！！");
        }

    }
}
