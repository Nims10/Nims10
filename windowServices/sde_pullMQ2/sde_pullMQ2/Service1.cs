﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using sde_pullMQ.WCFsde;

namespace sde_pullMQ
{
    public partial class Service1 : ServiceBase
    {
        Thread thr = new Thread(new ThreadStart(run));
        //WCFsde.Service1 obj = new WCFsde.Service1();
        public Service1()
        {
            InitializeComponent();
        }

        static void run()
        {
            WCFsde.Service1 obj = new WCFsde.Service1();
            string path = @"C:\inetpub\wwwroot\sde_publish\windowServices\logger\start" + "-pullMQ2.log";
            string path2 = @"C:\inetpub\wwwroot\sde_publish\windowServices\logger\" + DateTime.Now.ToString("yyyyMMdd") + "-pullMQ2.txt";

            while (1 == 1) 
            {
                try
                {
                    //TimeSpan ts = DateTime.Now - Convert.ToDateTime(File.GetLastWriteTime(path));
                    //if (ts.Minutes > 2)
                    //{
                    //    if (File.Exists(path))
                    //    {
                    //        File.Delete(path);
                    //    }

                    //    StreamWriter str = new StreamWriter(path, true);
                        try
                        {
                            String pullMQ = obj.PullMQ2();
                            Thread.Sleep(30000); //sleep 30 seconds
                        }
                        catch (Exception e)
                        {
                            StreamWriter str2 = new StreamWriter(path2, true);
                            str2.WriteLine(DateTime.Now + " " + e.ToString());
                            str2.Close();
                        }
                        finally
                        {
                            //int ii = 0;
                            //// 0.5 minutes
                            //while (ii < 22)
                            //{
                            //    int i = 0;
                            //    while (i < 500000000)
                            //    {
                            //        i++;
                            //    }

                            //    ii++;
                            //}
                            //str.Close();
                        }
                    //}
                }
                catch (Exception e)
                {
                    StreamWriter str2 = new StreamWriter(path2, true);
                    str2.WriteLine(DateTime.Now + " " + e.ToString());
                    str2.Close();
                }
            }
        }

        protected override void OnStart(string[] args)
        {
            string path = @"C:\inetpub\wwwroot\sde_publish\windowServices\logger\" + DateTime.Now.ToString("yyyyMMdd") + "-pullMQ2.txt";
            //if (!File.Exists(path))
            //{
            //    File.Create(path);
            //}

            //if (File.Exists(path))
            //{
                StreamWriter str = new StreamWriter(path, true);
                str.WriteLine("Service started on : " + DateTime.Now.ToString());
                str.Close();

                thr.Start();
            //}
        }

        protected override void OnStop()
        {
            string path = @"C:\inetpub\wwwroot\sde_publish\windowServices\logger\" + DateTime.Now.ToString("yyyyMMdd") + "-pullMQ2.txt";
            StreamWriter str = new StreamWriter(path, true);
            str.WriteLine("Service stoped on : " + DateTime.Now.ToString());
            str.Close();
            thr.Abort();
        }
    }
}
