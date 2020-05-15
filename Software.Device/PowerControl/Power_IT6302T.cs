using System.IO.Ports;
using System.ToolKit;
using System.Threading;
using System;
namespace Software.Device
{
    /// <summary>
    /// 通用类,支持IT6302和IT6322
    /// </summary>
    public class Power_IT6302T
    {
        //参考资料: 
        private SerialPort sp;
        private object obj = new object();

        #region 私有函数
        private void InitSerialPort(string port)
        {
            sp = new SerialPort(port, 9600, Parity.None, 8);
            sp.ReadTimeout = 10000;

            //在开发中有些串口设备需要串口供电，使用C#中的SerialPort类默认情况下不会出发 DataReceived函数，但使用超级终端却可以接收到数据，这是因为 SerialPort 类的DtrEnable 和RtsEnable 两个属性默认是false，设为true即可接收数据了,如下：
            sp.DtrEnable = true; //启用控制终端就续信号
            sp.RtsEnable = true; //启用请求发送信号
            sp.Open();
        }
        private void InitPower(int addressSet = 0)
        {
            string[] channelString = { "FIRst", "SECOnd", "THIrd" };
            sp.WriteLine($@"INST {channelString[addressSet]}");
        }
        /// <summary>
        /// 设定电压电流，并开启通道
        /// </summary>
        /// <param name="vol">电压</param>
        /// <param name="curr">电流</param>
        /// <param name="addressSet">设定通道</param>
        public void SetChanleVOLTandCurr(double vol, double curr, int addressSet = 0)
        {
            try
            {
                lock (this)
                {
                    if (addressSet > 2) return;
                    sp.WriteLine($@"INST CH" + addressSet.ToString());
                    Thread.Sleep(3);
                    sp.WriteLine($@"VOLT " + vol.ToString());
                    Thread.Sleep(3);
                    sp.WriteLine($@"CURR " + curr.ToString());
                    Thread.Sleep(3);
                    sp.WriteLine($@" CHAN: OUTP 1");
                }

            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// 获取通道电流
        /// </summary>
        /// <param name="addressSet"></param>
        /// <returns></returns>
        public double GetChanleCurr(int addressSet = 0)
        {
            try
            {
                lock (this)
                {
                    if (addressSet > 2) return 0;
                    sp.WriteLine($@"INST CH" + addressSet.ToString());
                    Thread.Sleep(3);
                    sp.WriteLine($@"MEAS:CURR?");
                    Thread.Sleep(300);
                    return sp.ReadExisting().ToDouble();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                throw;
            }
        }
        /// <summary>
        /// 关闭电源
        /// </summary>
        /// <param name="addressSet"></param>
        /// <returns></returns>
        public void CloseVolt(int addressSet = 0)
        {
            try
            {
                if (addressSet > 2) return;
                sp.WriteLine($@"INST CH" + addressSet.ToString());
                Thread.Sleep(3);
                sp.WriteLine($@"VOLT 0");
                Thread.Sleep(3);
                sp.WriteLine($@"CURR 0");
                Thread.Sleep(3);
                sp.WriteLine($@" CHAN: OUTP 1");
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 获取通道电压
        /// </summary>
        /// <param name="addressSet"></param>
        /// <returns></returns>
        public double GetChanleVOLT(int addressSet = 0)
        {
            try
            {
                if (addressSet > 2) return 0;
                sp.WriteLine($@"INST CH" + addressSet.ToString());
                Thread.Sleep(3);
                sp.WriteLine($@"MEAS:VOLT?");
                Thread.Sleep(200);
                return sp.ReadExisting().ToDouble();
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 电源模式
        /// </summary>
        private void InitSeedPowerMode()
        {
            sp.WriteLine($@"SYST:REM");
        }
        #endregion

        #region Init
        /// <summary>
        /// 多端口时请不要定义通道
        /// </summary>
        /// <param name="port"></param>
        /// <param name="addressSet"></param>
        public void Init(string port)
        {
            InitSerialPort(port);
            InitSeedPowerMode();
        }

        #endregion

        #region PowerOn PowerOff
        public void PowerOn()
        {
            sp.WriteLine("OUTP 1");
        }
        public void PowerOff()
        {
            sp.WriteLine("OUTP 0");
        }
        #endregion

        #region SetVoltage  SetCurrent
        public void SetVoltage(double voltage)
        {
            sp.WriteLine($"VOLT {voltage}");
        }
        public void SetCurrent(double current)
        {
            sp.WriteLine($"CURR {current}");
        }
        #endregion

        #region GetVoltageAndCurrent
        public double GetVoltage()
        {
            try
            {
                sp.DiscardInBuffer();
                Thread.Sleep(200);
                sp.Write("MEAS: VOLT?");
                Thread.Sleep(200);
                return sp.ReadExisting().ToDouble();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public double GetCurrent()
        {
            try
            {
                sp.DiscardInBuffer();
                Thread.Sleep(200);
                sp.Write("MEAS: CURR?");
                Thread.Sleep(200);
                return sp.ReadExisting().ToDouble();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Close
        public void Close()
        {
            sp.Close();
        }
        #endregion

        #region 简易方法
        ///// <summary>
        ///// 一步设置电压电流并上电
        ///// </summary>
        ///// <param name="port"></param>
        ///// <param name="addressSet"></param>
        ///// <param name="voltage"></param>
        ///// <param name="current"></param>
        //public void EasyPowerOn(string port, int addressSet, double voltage, double current)
        //{
        //    Init(port, addressSet);
        //    SetVoltage(voltage);
        //    SetCurrent(current);
        //    PowerOn();
        //}
        #endregion

    }
}
