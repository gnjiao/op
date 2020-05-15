using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Software.Device.SimensPLC
{
    public delegate int PLCReadDeviceBlock(string szDevice, int dwSize, out byte[]  lpdwData);
    public delegate int PLCWriteDeviceBlock(string szDevice, int dwSize, ref byte[] lpdwData);            

    public class IOBuffer
    {
        #region 变量

        public PLCReadDeviceBlock ReadDevice;
        public PLCWriteDeviceBlock WriteDevice;

        public string ReadDeviceName;
        public string WriteDeviceName;
        private int ReadSize;
        private int WriteSize;
        private byte[] Buffer;

        private static object locker = new object();

        #endregion

        #region 构造函数
        public IOBuffer(string rdDeviceName,int rdDeviceSize,string wrDeviceName,int wrDeviceSize)
        {
            Buffer = new byte[rdDeviceSize];
            ReadSize = rdDeviceSize;
            WriteSize = wrDeviceSize;
            Buffer = new byte[wrDeviceSize];
            ReadDeviceName = rdDeviceName;
            WriteDeviceName = wrDeviceName;
        }
        #endregion

        /// <summary>
        /// 读取缓冲区
        /// </summary>
        /// <param name="num">目标地址</param>
        /// <returns>值</returns>
        public bool ReadM(int num)
        {
            //int index = (Adr - ReadStartAddress) % 16;
            int index = num % 8;
            int val = Buffer[num / 8];
            int mask = 1 << index;
            return (val & mask) > 0;
        }

        /// <summary>
        ///写入缓冲区
        /// </summary>
        /// <param name="num">目标地址</param>
        /// <param name="value">值</param>
        public void WriteM(int num, bool value)
        {
            lock (locker)
            {
                int index = num % 8;
                int newVal = Buffer[num / 8];
                int mask = 0;
                if (value)
                {
                    mask = 1 << index;
                    newVal = newVal | mask;
                }
                else
                {
                    mask = 0xffff ^ 1 << index;
                    newVal = newVal & mask;
                }
                Buffer[num / 8] = (byte)newVal;
            }
        }

        /// <summary>
        /// 刷新IO缓冲区
        /// </summary>
        //public void Update()
        //{
        //    ReadDevice(ReadDeviceName, ReadSize, out ReadBuffer);
        //    WriteDevice(WriteDeviceName, WriteSize, ref WriteBuffer);
        //}
        public void ReadDeviceBlock()
        {
            ReadDevice(ReadDeviceName, ReadSize, out Buffer);
        }
        public void WriteDeviceBlock()
        {
            WriteDevice(WriteDeviceName, WriteSize, ref Buffer);
        }
        /// <summary>
        /// IO缓冲区复位
        /// </summary>
        public void Reset()
        {
            //Array.Clear(Buffer, 0, Buffer.Length);
            Array.Clear(Buffer, 0, Buffer.Length);
        }
    }
}
