using System.ToolKit;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System;

namespace Software.Device
{
    /// <summary>
    /// 通用类,包含常用方法
    /// </summary>
    public class Power_IT6821
    {
        private SerialPort sp;

        #region 通讯指令
        //十六进制数据
        //Init:
        //AA00 2001 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 00CB

        //Voltage:
        //AA00 2388 1300 0000 0000 0000 0000 0000 0000 0000 0000 0000 0068 

        //Current:
        //AA00 24B8 0B00 0000 0000 0000 0000 0000 0000 0000 0000 0000 0091 

        //PowerOn:
        //AA00 2101 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 00CC

        //PowerOff:
        //AA00 2100 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 00CB

        //Measurement:
        //AA00 2600 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 00??
        //反馈电压电流值
        //AA00 266E 007E 1300 0085 B80B 384A 0000 8813 0000 0100 0000 0035 
        #endregion

        #region 私有函数
        private void PowerOnOrOff(bool isPowerOn, int addressSet = 0)
        {
            byte[] arr = new byte[26];
            arr[0] = 0xAA;
            arr[1] = (byte)addressSet;
            arr[2] = 0x21;
            arr[3] = (byte)(isPowerOn ? 1 : 0);
            arr[25] = GetCheckSumValue(arr);
            WriteData(arr);
        }
        private static byte GetCheckSumValue(byte[] arr)
        {
            //最后一位校验值:所有字节的和,取后两个长度字符
            string sumStr = arr.Take(25).Select(x => (int)x).Sum().ToString(16).Right(2);
            return (byte)(sumStr.ToInt(16));
        }

        private void WriteData(byte[] arr)
        {
            try
            {
                sp.Write(arr, 0, arr.Length);
                Thread.Sleep(200);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Init
        public void Init(string port, int addressSet=0)
        {
            InitSerialPort(port);
            InitPower(addressSet);
        }
        private void InitSerialPort(string port)
        {
            sp = new SerialPort(port, 9600, Parity.None, 8);
            sp.ReadTimeout = 10000;

            //在开发中有些串口设备需要串口供电，使用C#中的SerialPort类默认情况下不会出发 DataReceived函数，但使用超级终端却可以接收到数据，这是因为 SerialPort 类的DtrEnable 和RtsEnable 两个属性默认是false，设为true即可接收数据了,如下：
            sp.DtrEnable = true; //启用控制终端就续信号
            sp.RtsEnable = true; //启用请求发送信号
            sp.Open();
        }
        private void InitPower(int addressSet=0)
        {
            byte[] arr = new byte[26];
            arr[0] = 0xAA;
            arr[1] = (byte)addressSet;
            arr[2] = 0x20;
            arr[3] = 0x01;
            arr[25] = GetCheckSumValue(arr);
            WriteData(arr);
        }
        #endregion

        #region PowerOn PowerOff
        public void PowerOn(int addressSet = 0)
        {
            PowerOnOrOff(true, addressSet);
        }
        public void PowerOff(int addressSet = 0)
        {
            PowerOnOrOff(false, addressSet);
        }
        #endregion

        #region SetVoltage  SetCurrent
        public void SetVoltage(double voltage, int addressSet = 0)
        {
            byte[] arr = new byte[26];
            arr[0] = 0xAA;
            arr[1] = (byte)addressSet;
            arr[2] = 0x23;
            string voltageStr = Convert.ToString((voltage * 1000).ToInt(), 16).PadLeft(8, '0');
            arr[3] = (byte)(voltageStr.Substring(6, 2).ToInt(16));
            arr[4] = (byte)(voltageStr.Substring(4, 2).ToInt(16));
            arr[5] = (byte)(voltageStr.Substring(2, 2).ToInt(16));
            arr[6] = (byte)(voltageStr.Substring(0, 2).ToInt(16));
            arr[25] = GetCheckSumValue(arr);
            WriteData(arr);
        }
        public void SetCurrent(double current, int addressSet = 0)
        {
            byte[] arr = new byte[26];
            arr[0] = 0xAA;
            arr[1] = (byte)addressSet;
            arr[2] = 0x24;
            string voltageStr = Convert.ToString((current * 1000).ToInt(), 16).PadLeft(4, '0');
            arr[3] = (byte)(voltageStr.Substring(2, 2).ToInt(16));
            arr[4] = (byte)(voltageStr.Substring(0, 2).ToInt(16));
            arr[25] = GetCheckSumValue(arr);
            WriteData(arr);
        }
        #endregion

        #region GetVoltageAndCurrent
        public void GetVoltageAndCurrent(out double voltage, out double current, int addressSet = 0)
        {
            byte[] arr = new byte[26];
            arr[0] = 0xAA;
            arr[1] = (byte)addressSet;
            arr[2] = 0x26;
            arr[25] = GetCheckSumValue(arr);

            try
            {
                sp.DiscardInBuffer();
                Thread.Sleep(200);
                sp.Write(arr, 0, arr.Length);
                Thread.Sleep(200);
                string response = sp.ReadExisting();
                byte[] arrNew = Encoding.Default.GetBytes(response);
                voltage = (arrNew[8].ToHex() + arrNew[7].ToHex() + arrNew[6].ToHex() + arrNew[5].ToHex()).ToInt(16).ToDouble() / 1000;
                current = (arrNew[4].ToHex() + arrNew[3].ToHex()).ToInt(16).ToDouble() / 1000;
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



    }
}
