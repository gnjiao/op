using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace JobNumber
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool isRunning;
            Mutex mutex = new Mutex(true, "RunOneInstanceOnly", out isRunning);

            if (isRunning)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                Application.ThreadException += new ThreadExceptionEventHandler(UI_ThreadException);//处理UI线程异常
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);//处理非UI线程异常               

                //Application.Run(new JobNumber());
            }
            else
            {
                MessageBox.Show("程序已经启动！");
            }
        }

        /// <summary>
        /// 处理UI线程异常
        /// </summary>
        static void UI_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            //Global.isErrorExit = true;
            //SerializerManager<Config>.Instance.Save(AppConfig.ConfigFileName, Config.Instance);          
            //SerializerManager<Position>.Instance.Save(AppConfig.ConfigPositionName, Position.Instance);
            //SerializerManager<Delay>.Instance.Save(AppConfig.ConfigPositionName, Delay.Instance);
            //LogHelper.Fatal(e.Exception.Message);
            Application.Exit();
        }
        /// <summary>
        /// 处理非UI线程异常
        /// </summary>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //Global.isErrorExit = true;
            //SerializerManager<Config>.Instance.Save(AppConfig.ConfigFileName, Config.Instance);
            //SerializerManager<Position>.Instance.Save(AppConfig.ConfigPositionName, Position.Instance);
            //SerializerManager<Delay>.Instance.Save(AppConfig.ConfigPositionName, Delay.Instance);
            //LogHelper.Fatal(e.ExceptionObject.ToString());
            Application.Exit();
        }
    }
    
}
