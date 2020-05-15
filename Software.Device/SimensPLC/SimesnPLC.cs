using System;
using CMotion.Interfaces.Base;
using LogHeper;
namespace Software.Device.SimensPLC
{
    public enum PLCDeviceType
    {
        I,
        Q,
        M,
        DB,
        Max
    }
    public class SimensPLC : IPLC<PLCDeviceType, byte>
    {
        private libnodave.daveOSserialType fds;
        private libnodave.daveInterface di;
        private libnodave.daveConnection dc;
        private byte[] memoryBuffer = new byte[10];
        private int _rack, _slot, _port;
        private string _address;
        #region 属性
        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///     说明
        /// </summary>
        public string Description { get; set; }
        public string ConnectionParam { get; set; }
        public bool IsConnect { get; private set; }
        #endregion
        public SimensPLC() : this(0, 1, "192.168.0,2", 102) { }
        public SimensPLC(int rack, int slot, string address, int port)
        {
            _rack = rack;
            _slot = slot;
            _address = address;
            _port = port;
            ConnectionParam = _address + "," + _port.ToString() + "," + _rack.ToString() + "," + _slot.ToString();
        }

        public void SetConnectionParam(string param)
        {
            ConnectionParam = param;
            string[] str = ConnectionParam.Split(',');
            _address = str[0];
            _port = int.Parse(str[1]);
            _rack = int.Parse(str[2]);
            _slot = int.Parse(str[3]);
        }
        public int Open()
        {
            fds.rfd = libnodave.openSocket(_port, _address);
            fds.wfd = fds.rfd;
            if (fds.rfd > 0)
            {
                di = new libnodave.daveInterface(fds, "IF1", 0, libnodave.daveProtoISOTCP, libnodave.daveSpeed187k);
                di.setTimeout(1000000);
                dc = new libnodave.daveConnection(di, 0, _rack, _slot);
                if (0 == dc.connectPLC()) IsConnect = true;
                return 0;
            }
            else
            {
                IsConnect = false;
                return -1;
            }
        }
        public int Close()
        {
            if (di == null) return 0;
            if (dc == null) return 0;
            dc.disconnectPLC();
            di.disconnectAdapter();
            return 0;
        }
        #region "写入PLC"
        /// <summary>
        /// 写入字节数据
        /// </summary>
        /// <param name="szAddress"></param>
        /// <param name="lpdwData"></param>
        /// <returns></returns>
        public int WriteByteDevice(string szAddress, ref byte lpdwData)
        {
            int iRet = 0;
            byte[] byteValue = new byte[1];
            try
            {
                byteValue[0] = lpdwData;
                iRet = WriteDeviceBlock(szAddress, 1, ref byteValue);
                if (iRet != 0)
                {
                    LogHelper.Error("Error code = 0x" + iRet.ToString("X"));
                    return -1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error code:" + ex);
                return -1;
            }
        }
        /// <summary>
        /// 写入字数据
        /// </summary>
        /// <param name="szAddress"></param>
        /// <param name="lpdwData"></param>
        /// <returns></returns>
        public int WriteInt16Device(string szAddress, ref Int16 lpdwData)
        {
            int iRet = 0;
            byte[] byteValue = new byte[2];
            try
            {
                byte[] byteTemp = BitConverter.GetBytes(lpdwData);
                byteValue[0] = byteTemp[1];
                byteValue[1] = byteTemp[0];
                iRet = WriteDeviceBlock(szAddress, 2, ref byteValue);
                if (iRet != 0)
                {
                    LogHelper.Error("Error code = 0x" + iRet.ToString("X"));
                    return -1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error code:" + ex);
                return -1;
            }
        }
        /// <summary>
        /// 写入双字数据
        /// </summary>
        /// <param name="szAddress"></param>
        /// <param name="lpdwData"></param>
        /// <returns></returns>
        public int WriteInt32Device(string szAddress, ref Int32 lpdwData)
        {
            int iRet = 0;
            byte[] byteValue = new byte[4];
            try
            {
                byte[] byteTemp = BitConverter.GetBytes(lpdwData);
                byteValue[0] = byteTemp[3];
                byteValue[1] = byteTemp[2];
                byteValue[2] = byteTemp[1];
                byteValue[3] = byteTemp[0];
                iRet = WriteDeviceBlock(szAddress, 4, ref byteValue);
                if (iRet != 0)
                {
                    LogHelper.Error("Error code = 0x" + iRet.ToString("X"));
                    return -1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error code:" + ex);
                return -1;
            }
        }
        /// <summary>
        /// 写入连续字节数据
        /// </summary>
        /// <param name="szAddress"></param>
        /// <param name="Size"></param>
        /// <param name="lpdwData"></param>
        /// <returns></returns>
        public int WriteByteBlock(string szAddress, int Size, ref byte[] lpdwData)
        {
            int iRet = 0;
            byte[] byteValue = new byte[Size];
            try
            {
                byteValue = lpdwData;
                iRet = WriteDeviceBlock(szAddress, Size, ref byteValue);
                if (iRet != 0)
                {
                    LogHelper.Error("Error code = 0x" + iRet.ToString("X"));
                    return -1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error code:" + ex);
                return -1;
            }
        }
        /// <summary>
        /// 写入连续字数据
        /// </summary>
        /// <param name="szAddress"></param>
        /// <param name="Size"></param>
        /// <param name="lpdwData"></param>
        /// <returns></returns>
        public int WriteInt16Block(string szAddress, int Size, ref Int16[] lpdwData)
        {
            int iRet = 0;
            byte[] byteValue = new byte[2 * Size];
            try
            {
                for (int i = 0; i < Size; i++)
                {
                    int j = 2 * i;
                    byte[] byteTemp = BitConverter.GetBytes(lpdwData[i]);
                    byteValue[j + 0] = byteTemp[1];
                    byteValue[j + 1] = byteTemp[0];
                }
                iRet = WriteDeviceBlock(szAddress, 2 * Size, ref byteValue);
                if (iRet != 0)
                {
                    LogHelper.Error("Error code = 0x" + iRet.ToString("X"));
                    return -1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error code:" + ex);
                return -1;
            }
        }
        /// <summary>
        /// 写入连续M或DB区双字数据
        /// </summary>
        /// <param name="DB">为0，M区；大于0，DB区</param>
        /// <param name="Address">起始地址</param>
        /// <param name="values">数据区</param>
        public int WriteInt32Block(string szAddress, int Size, ref Int32[] lpdwData)
        {
            int iRet = 0;
            byte[] byteValue = new byte[4 * Size];
            try
            {
                for (int i = 0; i < Size; i++)
                {
                    int j = 4 * i;
                    byte[] byteTemp = BitConverter.GetBytes(lpdwData[i]);
                    byteValue[j + 0] = byteTemp[3];
                    byteValue[j + 1] = byteTemp[2];
                    byteValue[j + 2] = byteTemp[1];
                    byteValue[j + 3] = byteTemp[0];
                }
                iRet = WriteDeviceBlock(szAddress, 4 * Size, ref byteValue);
                if (iRet != 0)
                {
                    LogHelper.Error("Error code = 0x" + iRet.ToString("X"));
                    return -1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error code:" + ex);
                return -1;
            }
        }
        #endregion
        #region "读取PLC"
        /// <summary>
        /// 读取字节数据
        /// </summary>
        /// <param name="szAddress"></param>
        /// <param name="lpdwData"></param>
        /// <returns></returns>
        public int ReadByteDevice(string szAddress, out byte lpdwData)
        {
            int res = 0;
            byte[] byteValue = new byte[1];
            try
            {
                res = ReadDeviceBlock(szAddress, 1, out byteValue);
                if (res != 0)
                {
                    LogHelper.Error("Error code = 0x" + res.ToString("X"));
                    lpdwData = 0;
                    return -1;
                }
                lpdwData = byteValue[0];
                return 0;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error code:" + ex);
                lpdwData = 0;
                return -1;
            }
        }
        /// <summary>
        /// 读取字数据
        /// </summary>
        /// <param name="szAddress"></param>
        /// <param name="lpdwData"></param>
        /// <returns></returns>
        public int ReadInt16Device(string szAddress, out Int16 lpdwData)
        {
            int res = 0;
            byte[] byteValue = new byte[2];
            byte[] byteTemp = new byte[2];
            try
            {
                res = ReadDeviceBlock(szAddress, 2, out byteValue);
                if (res != 0)
                {
                    LogHelper.Error("Error code = 0x" + res.ToString("X"));
                    lpdwData = 0;
                    return -1;
                }
                byteTemp[0] = byteValue[1];
                byteTemp[1] = byteValue[0];
                lpdwData = BitConverter.ToInt16(byteTemp, 0);
                return 0;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error code:" + ex);
                lpdwData = 0;
                return -1;
            }
        }
        /// <summary>
        /// 读取双字数据
        /// </summary>
        /// <param name="szAddress"></param>
        /// <param name="lpdwData"></param>
        /// <returns></returns>
        public int ReadInt32Device(string szAddress, out Int32 lpdwData)
        {
            int res = 0;
            byte[] byteValue = new byte[4];
            byte[] byteTemp = new byte[4];
            try
            {
                res = ReadDeviceBlock(szAddress, 4, out byteValue);
                if (res != 0)
                {
                    LogHelper.Error("Error code = 0x" + res.ToString("X"));
                    lpdwData = 0;
                    return -1;
                }
                byteTemp[0] = byteValue[3];
                byteTemp[1] = byteValue[2];
                byteTemp[2] = byteValue[1];
                byteTemp[3] = byteValue[0];
                lpdwData = BitConverter.ToInt32(byteTemp, 0);
                return 0;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error code:" + ex);
                lpdwData = 0;
                return -1;
            }
        }
        /// <summary>
        /// 读取连续字节数据
        /// </summary>
        /// <param name="DB"></param>
        /// <param name="Address"></param>
        /// <param name="lpdwData"></param>
        /// <returns></returns>
        public int ReadByteBlock(string szAddress, int Size, out byte[] lpdwData)
        {
            int res = 0;
            byte[] byteValue = new byte[Size];
            try
            {
                res = ReadDeviceBlock(szAddress, Size, out byteValue);
                if (res != 0)
                {
                    LogHelper.Error("Error code = 0x" + res.ToString("X"));
                    lpdwData = null;
                    return -1;
                }
                lpdwData = byteValue;
                return 0;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error code:" + ex);
                lpdwData = null;
                return -1;
            }
        }
        /// <summary>
        /// 读取连续字数据
        /// </summary>
        /// <param name="szAddress"></param>
        /// <param name="Size"></param>
        /// <param name="lpdwData"></param>
        /// <returns></returns>
        public int ReadInt16Block(string szAddress, int Size, out Int16[] lpdwData)
        {
            int res = 0;
            byte[] byteValue = new byte[2 * Size];
            Int16[] tempData = new Int16[Size];
            byte[] byteTemp = new byte[2];
            try
            {
                res = ReadDeviceBlock(szAddress, 2 * Size, out byteValue);
                if (res != 0)
                {
                    LogHelper.Error("Error code = 0x" + res.ToString("X"));
                    lpdwData = null;
                    return -1;
                }
                for (int i = 0; i < Size; i++)
                {
                    int j = 2 * i;
                    byteTemp[0] = byteValue[j + 1];
                    byteTemp[1] = byteValue[j + 0];
                    tempData[i] = BitConverter.ToInt16(byteTemp, 0);
                }
                lpdwData = tempData;
                return 0;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error code:" + ex);
                lpdwData = null;
                return -1;
            }
        }
        /// <summary>
        /// 读取连续双字数据
        /// </summary>
        /// <param name="szAddress"></param>
        /// <param name="Size"></param>
        /// <param name="lpdwData"></param>
        /// <returns></returns>
        public int ReadInt32Block(string szAddress, int Size, out Int32[] lpdwData)
        {
            int res = 0;
            byte[] byteValue = new byte[4 * Size];
            Int32[] tempData = new Int32[Size];
            byte[] byteTemp = new byte[4];
            try
            {
                res = ReadDeviceBlock(szAddress, 4 * Size, out byteValue);
                if (res != 0)
                {
                    LogHelper.Error("Error code = 0x" + res.ToString("X"));
                    lpdwData = null;
                    return -1;
                }
                for (int i = 0; i < Size; i++)
                {
                    int j = 4 * i;
                    byteTemp[0] = byteValue[j + 3];
                    byteTemp[1] = byteValue[j + 2];
                    byteTemp[2] = byteValue[j + 1];
                    byteTemp[3] = byteValue[j + 0];
                    tempData[i] = BitConverter.ToInt32(byteTemp, 0);
                }
                lpdwData = tempData;
                return 0;
            }
            catch (Exception ex)
            {
                LogHelper.Error("Error code:" + ex);
                lpdwData = null;
                return -1;
            }
        }
        #endregion
        public int WriteDeviceBlock(string iDeviceName, int iSize, ref byte[] iData)
        {
            PLCDeviceType type;
            if ("DB" == iDeviceName.Substring(0, 2))
            {
                int iRet, addr;
                int DBnum;
                GetDeviceCode(iDeviceName, out type, out DBnum, out addr);
                iRet = dc.writeBytes(libnodave.daveDB, DBnum, addr, iSize, iData);
                if (iRet != 0)
                {
                    LogHelper.Error("Error code = 0x" + iRet.ToString("X"));
                    return -1;
                }
                return 0;
            }
            else
            {
                int addr;
                GetDeviceCode(iDeviceName, out type, out addr);
                return WriteDeviceBlock(type, addr, iSize, ref iData);
            }
        }
        public int WriteDeviceBlock(PLCDeviceType iType, int iAddress, int iSize, ref byte[] iData)
        {
            int iRet = 0;
            byte[] byteValue = new byte[iSize];
            try
            {
                switch (iType)
                {
                    case PLCDeviceType.Q:
                        iRet = dc.writeBytes(libnodave.daveOutputs, 0, iAddress, iSize, iData);
                        break;
                    case PLCDeviceType.M:
                        iRet = dc.writeBytes(libnodave.daveFlags, 0, iAddress, iSize, iData);
                        break;
                    default:
                        break;
                }
                if (iRet != 0)
                {
                    LogHelper.Error("Error code = 0x" + iRet.ToString("X"));
                    return -1;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int ReadDeviceBlock(string iDeviceName, int iSize, out byte[] oData)
        {
            PLCDeviceType type;
            if ("DB" == iDeviceName.Substring(0, 2))
            {
                int res, addr;
                int DBnum;
                byte[] byteValue = new byte[iSize];
                GetDeviceCode(iDeviceName, out type, out DBnum, out addr);
                res = dc.readBytes(libnodave.daveDB, DBnum, addr, iSize, byteValue);
                if (res == 0) oData = byteValue;
                else oData = null;
                return 0;
            }
            else
            {
                int addr;
                GetDeviceCode(iDeviceName, out type, out addr);
                return ReadDeviceBlock(type, addr, iSize, out oData);
            }
        }
        public int ReadDeviceBlock(PLCDeviceType iType, int iAddress, int iSize, out byte[] oData)
        {
            int res = 0;
            byte[] byteValue = new byte[iSize];
            try
            {
                switch (iType)
                {
                    case PLCDeviceType.I:
                        res = dc.readBytes(libnodave.daveInputs, 0, iAddress, iSize, byteValue);
                        break;
                    case PLCDeviceType.M:
                        res = dc.readBytes(libnodave.daveFlags, 0, iAddress, iSize, byteValue);
                        break;
                    default:
                        break;
                }
                if (res == 0) oData = byteValue;
                else oData = null;
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void GetDeviceCode(string iDeviceName, out PLCDeviceType oType, out int oAddress)
        {
            string s = iDeviceName.ToUpper();
            string strType = s.Substring(0, 1);
            oType = GetDeviceType(strType);
            oAddress = int.Parse(s.Substring(2, s.Length - 2));
        }
        private void GetDeviceCode(string iDeviceName, out PLCDeviceType oType, out int DBnum, out int oAddress)
        {
            string s = iDeviceName.ToUpper();
            string strType = s.Substring(0, 2);
            string[] str = iDeviceName.Split('.');
            oType = GetDeviceType(str[1].Substring(0, 3));
            DBnum = int.Parse(str[0].Substring(2, str[0].Length - 2));
            oAddress = int.Parse(str[1].Substring(3, str[1].Length - 3));
        }
        private PLCDeviceType GetDeviceType(string s)
        {
            return (s == "I") ? PLCDeviceType.I :
                   (s == "Q") ? PLCDeviceType.Q :
                   (s == "M") ? PLCDeviceType.M :
                   (s == "DB") ? PLCDeviceType.DB :
                                  PLCDeviceType.Max;
        }
        public void Dispose()
        {
            Close();
        }
        #region 暂时不用
        public int SetBitDevice(string iDeviceName, int iSize, byte[] onOffBits)
        {
            throw new NotImplementedException();
        }

        public int SetBitDevice(PLCDeviceType iType, int iAddress, int iSize, byte[] onOffBits)
        {
            throw new NotImplementedException();
        }

        public int GetBitDevice(string iDeviceName, int iSize, byte[] outOnOffBits)
        {
            throw new NotImplementedException();
        }

        public int GetBitDevice(PLCDeviceType iType, int iAddress, int iSize, byte[] outOnOffBits)
        {
            throw new NotImplementedException();
        }

        public int SetDevice(string iDeviceName, byte iData)
        {
            throw new NotImplementedException();
        }

        public int SetDevice(PLCDeviceType iType, int iAddress, byte iData)
        {
            throw new NotImplementedException();
        }

        public int GetDevice(string iDeviceName, out byte oData)
        {
            throw new NotImplementedException();
        }

        public int GetDevice(PLCDeviceType iType, int iAddress, out byte oData)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
