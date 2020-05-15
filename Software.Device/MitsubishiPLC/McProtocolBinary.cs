using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.IO;
using CMotion.Interfaces.Base;
namespace Software.Device.MitsubishiPLC
{
    /// <summary>
    /// 二进制3E格式
    /// </summary>
    public class McProtocolBinary : IPLC<PLCDeviceType, int>
    {
        System.Diagnostics.Stopwatch _watch = new System.Diagnostics.Stopwatch();
        private TcpClient Client { get; set; }
        private NetworkStream Stream { get; set; }
        // ====================================================================================
        public McFrame CommandFrame { get; set; }      // 使用框架
        private string _address { get; set; }        // 主机名或IP地址
        private int _port { get; set; }              // 端口号
        private McCommand Command { get; set; }
        public string ConnectionParam { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        // ====================================================================================
        // 构造函数
        public McProtocolBinary()
        {
            CommandFrame = McFrame.MC3E;
            Client = new TcpClient();
        }
        public void SetConnectionParam(string param)
        {
            ConnectionParam = param;
            string[] str = ConnectionParam.Split(',');
            _address = str[0];
            _port = int.Parse(str[1]);
        }
        // ====================================================================================
        // 後処理
        public void Dispose()
        {
            Close();
        }

        // ====================================================================================
        public int Open()
        {
            TcpClient c = Client;
            if (!c.Connected)
            {
                // Keep Alive機能の実装
                var ka = new List<byte>(sizeof(uint) * 3);
                ka.AddRange(BitConverter.GetBytes(1u));
                ka.AddRange(BitConverter.GetBytes(45000u));
                ka.AddRange(BitConverter.GetBytes(5000u));
                c.Client.IOControl(IOControlCode.KeepAliveValues, ka.ToArray(), null);
                try
                {
                    c.Connect(_address, _port);
                    Stream = c.GetStream();
                }
                catch (SocketException e1)
                {
                    throw new Exception("连接到PLC（" + _address + ":" + _port + "）失败！", e1);
                }
            }
            Command = new McCommand(CommandFrame);
            return 0;
        }
        // ====================================================================================
        public int Close()
        {
            TcpClient c = Client;
            if (c.Connected) c.Close();
            return 0;
        }

        // ====================================================================================
        public int SetBitDevice(string iDeviceName, int iSize, byte[] onOffBits)
        {
            PLCDeviceType type;
            int addr;
            GetDeviceCode(iDeviceName, out type, out addr);
            return SetBitDevice(type, addr, iSize, onOffBits);
        }

        // ====================================================================================
        public int SetBitDevice(PLCDeviceType iType, int iAddress, int iSize, byte[] onOffBits)
        {
            var type = iType;
            var addr = iAddress;
            var data = new List<byte>(6) {
                        (byte) addr ,
                        (byte) (addr >> 8), 
                        (byte) (addr >> 16),
                        (byte) type , 
                        (byte) iSize ,
                        (byte) (iSize >> 8) };
            var d = (byte)onOffBits[0];
            var i = 0;
            while (i < onOffBits.Length) {
                if (i % 2 == 0) {
                    d = (byte)onOffBits[i];
                    d <<= 4;
                } else {
                    d |= (byte)(onOffBits[i] & 0x01);
                    data.Add(d);
                }
                ++i;
            }
            if (i % 2 != 0) {
                data.Add(d);
            }
            byte[] sdCommand = Command.SetCommand(0x1401, 0x0001, data.ToArray());
            byte[] rtResponse = TryExecution(sdCommand);
			int rtCode = Command.SetResponse(rtResponse, CommandFrame);
			return rtCode;
        }

        // ====================================================================================
        public int GetBitDevice(string iDeviceName, int iSize, byte[] outOnOffBits)
        {
            PLCDeviceType type;
            int addr;
            GetDeviceCode(iDeviceName, out type, out addr);
            return GetBitDevice(type, addr, iSize, outOnOffBits);
        }

        // ====================================================================================
        public int GetBitDevice(PLCDeviceType iType, int iAddress, int iSize, byte[] outOnOffBits)
        {
            PLCDeviceType type = iType;
            int addr = iAddress;
            var data = new List<byte>(6)
                    {
                        (byte) addr
                      , (byte) (addr >> 8)
                      , (byte) (addr >> 16)
                      , (byte) type
                      , (byte) iSize
                      , (byte) (iSize >> 8)
                    };
            byte[] sdCommand = Command.SetCommand(0x0401, 0x0001, data.ToArray());
            byte[] rtResponse = TryExecution(sdCommand);
			int rtCode = Command.SetResponse(rtResponse, CommandFrame);
			byte[] rtData = Command.Response;
            for (int i = 0; i < iSize; ++i) {
                if (i % 2 == 0) {
                    outOnOffBits[i] = (byte)((rtCode == 0) ? ((rtData[i / 2] >> 4) & 0x01) : 0);
                } else {
                    outOnOffBits[i] = (byte)((rtCode == 0) ? (rtData[i / 2] & 0x01) : 0);
                }
            }
            return rtCode;
        }
        // ====================================================================================
        public int WriteDeviceBlock(string iDeviceName, int iSize, ref int[] iData)
        {
            PLCDeviceType type;
            int addr;
            GetDeviceCode(iDeviceName, out type, out addr);
            return WriteDeviceBlock(type, addr, iSize, ref iData);
        }

        // ====================================================================================
        public int WriteDeviceBlock(PLCDeviceType iType, int iAddress, int iSize, ref int[] iData)
        {
            PLCDeviceType type = iType;
            int addr = iAddress;
            var data = new List<byte>(6)
                    {
                        (byte) addr
                      , (byte) (addr >> 8)
                      , (byte) (addr >> 16)
                      , (byte) type
                      , (byte) iSize
                      , (byte) (iSize >> 8)
                    };
            foreach (int t in iData) {
                data.Add((byte)t);
                data.Add((byte)(t >> 8));
            }
            byte[] sdCommand = Command.SetCommand(0x1401, 0x0000, data.ToArray());
            byte[] rtResponse = TryExecution(sdCommand);
			int rtCode = Command.SetResponse(rtResponse, CommandFrame);
			return rtCode;
        }

        // ====================================================================================
        public int ReadDeviceBlock(string iDeviceName, int iSize, out int[] oData)
        {
            PLCDeviceType type;
            int addr;
            GetDeviceCode(iDeviceName, out type, out addr);
            return ReadDeviceBlock(type, addr, iSize, out oData);
        }

        // ====================================================================================
        public int ReadDeviceBlock(PLCDeviceType iType, int iAddress, int iSize, out int[] oData)
        {
            PLCDeviceType type = iType;
            int addr = iAddress;
            var data = new List<byte>(6)
                    {
                        (byte) addr
                      , (byte) (addr >> 8)
                      , (byte) (addr >> 16)
                      , (byte) type
                      , (byte) iSize
                      , (byte) (iSize >> 8)
                    };
            byte[] sdCommand = Command.SetCommand(0x0401, 0x0000, data.ToArray());
            byte[] rtResponse = TryExecution(sdCommand);
			int rtCode = Command.SetResponse(rtResponse, CommandFrame);
			byte[] rtData = Command.Response;
            oData = null;
            for (int i = 0; i < iSize; ++i) 
                oData[i] = (rtCode == 0) ? BitConverter.ToInt16(rtData, i * 2) : 0;
            return rtCode;
        }
        // ====================================================================================
        public int SetDevice(string iDeviceName, int iData)
        {
            PLCDeviceType type;
            int addr;
            GetDeviceCode(iDeviceName, out type, out addr);
            return SetDevice(type, addr, iData);
        }
        // ====================================================================================
        public int SetDevice(PLCDeviceType iType, int iAddress, int iData)
        {
            PLCDeviceType type = iType;
            int addr = iAddress;
            var data = new List<byte>(6)
                    {
                        (byte) addr
                      , (byte) (addr >> 8)
                      , (byte) (addr >> 16)
                      , (byte) type
                      , 0x01
                      , 0x00
                      , (byte) iData
                      , (byte) (iData >> 8)
                    };
            byte[] sdCommand = Command.SetCommand(0x1401, 0x0000, data.ToArray());
            byte[] rtResponse = TryExecution(sdCommand);
			int rtCode = Command.SetResponse(rtResponse, CommandFrame);
			return rtCode;
        }
        // ====================================================================================
        public int GetDevice(string iDeviceName, out int oData)
        {
            PLCDeviceType type;
            int addr;
            GetDeviceCode(iDeviceName, out type, out addr);
            return GetDevice(type, addr, out oData);
        }
        // ====================================================================================
        public int GetDevice(PLCDeviceType iType, int iAddress, out int oData)
        {
            PLCDeviceType type = iType;
            int addr = iAddress;
            var data = new List<byte>(6)
                    {
                        (byte) addr
                      , (byte) (addr >> 8)
                      , (byte) (addr >> 16)
                      , (byte) type
                      , 0x01
                      , 0x00
                    };
            byte[] sdCommand = Command.SetCommand(0x0401, 0x0000, data.ToArray());
            byte[] rtResponse = TryExecution(sdCommand);
			int rtCode = Command.SetResponse(rtResponse, CommandFrame);
			if (0 < rtCode) {
                oData = 0;
            } else {
                byte[] rtData = Command.Response;
                oData = BitConverter.ToInt16(rtData, 0);
            }
            return rtCode;
        }

        public static PLCDeviceType GetDeviceType(string s)
        {
            return (s == "M") ? PLCDeviceType.M :
                   (s == "SM") ? PLCDeviceType.SM :
                   (s == "L") ? PLCDeviceType.L :
                   (s == "F") ? PLCDeviceType.F :
                   (s == "V") ? PLCDeviceType.V :
                   (s == "S") ? PLCDeviceType.S :
                   (s == "X") ? PLCDeviceType.X :
                   (s == "Y") ? PLCDeviceType.Y :
                   (s == "B") ? PLCDeviceType.B :
                   (s == "SB") ? PLCDeviceType.SB :
                   (s == "DX") ? PLCDeviceType.DX :
                   (s == "DY") ? PLCDeviceType.DY :
                   (s == "D") ? PLCDeviceType.D :
                   (s == "SD") ? PLCDeviceType.SD :
                   (s == "R") ? PLCDeviceType.R :
                   (s == "ZR") ? PLCDeviceType.ZR :
                   (s == "W") ? PLCDeviceType.W :
                   (s == "SW") ? PLCDeviceType.SW :
                   (s == "TC") ? PLCDeviceType.TC :
                   (s == "TS") ? PLCDeviceType.TS :
                   (s == "TN") ? PLCDeviceType.TN :
                   (s == "CC") ? PLCDeviceType.CC :
                   (s == "CS") ? PLCDeviceType.CS :
                   (s == "CN") ? PLCDeviceType.CN :
                   (s == "SC") ? PLCDeviceType.SC :
                   (s == "SS") ? PLCDeviceType.SS :
                   (s == "SN") ? PLCDeviceType.SN :
                   (s == "Z") ? PLCDeviceType.Z :
                   (s == "TT") ? PLCDeviceType.TT :
                   (s == "TM") ? PLCDeviceType.TM :
                   (s == "CT") ? PLCDeviceType.CT :
                   (s == "CM") ? PLCDeviceType.CM :
                   (s == "A") ? PLCDeviceType.A :
                                 PLCDeviceType.Max;
        }

        // ====================================================================================
        public static bool IsBitDevice(PLCDeviceType type)
        {
            return !((type == PLCDeviceType.D)
                  || (type == PLCDeviceType.SD)
                  || (type == PLCDeviceType.Z)
                  || (type == PLCDeviceType.ZR)
                  || (type == PLCDeviceType.R)
                  || (type == PLCDeviceType.W));
        }

        // ====================================================================================
        public static bool IsHexDevice(PLCDeviceType type)
        {
            return (type == PLCDeviceType.X)
                || (type == PLCDeviceType.Y)
                || (type == PLCDeviceType.B)
                || (type == PLCDeviceType.W);
        }

        // ====================================================================================
        private void GetDeviceCode(string iDeviceName, out PLCDeviceType oType, out int oAddress)
        {
            string s = iDeviceName.ToUpper();
            string strAddress;

            // 1文字取り出す
            string strType = s.Substring(0, 1);
            switch (strType) {
            case "A":
            case "B":
            case "D":
            case "F":
            case "L":
            case "M":
            case "R":
            case "V":
            case "W":
            case "X":
            case "Y":
                // 2文字目以降は数値のはずなので変換する
                strAddress = s.Substring(1);
                break;
            case "Z":
                // もう1文字取り出す
                strType = s.Substring(0, 2);
                // ファイルレジスタの場合     : 2
                // インデックスレジスタの場合 : 1
                strAddress = s.Substring(strType.Equals("ZR") ? 2 : 1);
                break;
            case "C":
                // もう1文字取り出す
                strType = s.Substring(0, 2);
                switch (strType) {
                case "CC":
                case "CM":
                case "CN":
                case "CS":
                case "CT":
                    strAddress = s.Substring(2);
                    break;
                default:
                    throw new Exception("Invalid format.");
                }
                break;
            case "S":
                // もう1文字取り出す
                strType = s.Substring(0, 2);
                switch (strType) {
                case "SD":
                case "SM":
                    strAddress = s.Substring(2);
                    break;
                default:
                    throw new Exception("Invalid format.");
                }
                break;
            case "T":
                // もう1文字取り出す
                strType = s.Substring(0, 2);
                switch (strType) {
                case "TC":
                case "TM":
                case "TN":
                case "TS":
                case "TT":
                    strAddress = s.Substring(2);
                    break;
                default:
                    throw new Exception("Invalid format.");
                }
                break;
            default:
                throw new Exception("Invalid format.");
            }

            oType = GetDeviceType(strType);
            oAddress = IsHexDevice(oType) ? Convert.ToInt32(strAddress, BlockSize) :
                                            Convert.ToInt32(strAddress);
        }

        private byte[] Execute(byte[] iCommand)
        {
            NetworkStream ns = Stream;
            ns.Write(iCommand, 0, iCommand.Length);
            ns.Flush();

            _watch.Restart();

            using (var ms = new MemoryStream())
            {
                var buff = new byte[256];
                do
                {
                    int sz = ns.Read(buff, 0, buff.Length);
                    if (sz == 0)
                    {
                        Debug.WriteLine("读TCP流失败，网络已断");
                    }
                    else
                    {
                        ms.Write(buff, 0, sz);
                    }
                } while (ns.DataAvailable && _watch.ElapsedMilliseconds > 10);

                return ms.ToArray();
            }
        }
        // &&&&& private &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        private const int BlockSize = 0x0010;
        // ================================================================================
        private byte[] TryExecution(byte[] iCommand)
        {
            byte[] rtResponse;
            int tCount = 10;
            do {
                rtResponse = Execute(iCommand);
                --tCount;
                if (tCount < 0)
                {
                    throw new Exception("不能从PLC取得正确的值.");
                }
            } while (Command.IsIncorrectResponse(rtResponse));
            return rtResponse;
        }
    }
}
