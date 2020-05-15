using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMotion.Interfaces.Base
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>”的 XML 注释
    public interface IPLC<PLCDeviceType,T> :IAutomatic,IDisposable
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>”的 XML 注释
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>.ConnectionParam”的 XML 注释
        string ConnectionParam { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>.ConnectionParam”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>.Open()”的 XML 注释
        int Open();
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>.Open()”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>.Close()”的 XML 注释
        int Close();
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>.Close()”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>.SetConnectionParam(string)”的 XML 注释
        void SetConnectionParam(string param);
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>.SetConnectionParam(string)”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>.SetBitDevice(string, int, byte[])”的 XML 注释
        int SetBitDevice(string iDeviceName, int iSize, byte[] onOffBits);
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>.SetBitDevice(string, int, byte[])”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>.SetBitDevice(PLCDeviceType, int, int, byte[])”的 XML 注释
        int SetBitDevice(PLCDeviceType iType, int iAddress, int iSize, byte[] onOffBits);
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>.SetBitDevice(PLCDeviceType, int, int, byte[])”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>.GetBitDevice(string, int, byte[])”的 XML 注释
        int GetBitDevice(string iDeviceName, int iSize, byte[] outOnOffBits);
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>.GetBitDevice(string, int, byte[])”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>.GetBitDevice(PLCDeviceType, int, int, byte[])”的 XML 注释
        int GetBitDevice(PLCDeviceType iType, int iAddress, int iSize, byte[] outOnOffBits);
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>.GetBitDevice(PLCDeviceType, int, int, byte[])”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>.WriteDeviceBlock(string, int, ref T[])”的 XML 注释
        int WriteDeviceBlock(string iDeviceName, int iSize, ref T[] iData);
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>.WriteDeviceBlock(string, int, ref T[])”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>.WriteDeviceBlock(PLCDeviceType, int, int, ref T[])”的 XML 注释
        int WriteDeviceBlock(PLCDeviceType iType, int iAddress, int iSize, ref T[] iData);
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>.WriteDeviceBlock(PLCDeviceType, int, int, ref T[])”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>.ReadDeviceBlock(string, int, out T[])”的 XML 注释
        int ReadDeviceBlock(string iDeviceName, int iSize,out T[] oData);
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>.ReadDeviceBlock(string, int, out T[])”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>.ReadDeviceBlock(PLCDeviceType, int, int, out T[])”的 XML 注释
        int ReadDeviceBlock(PLCDeviceType iType, int iAddress, int iSize,out T[] oData);
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>.ReadDeviceBlock(PLCDeviceType, int, int, out T[])”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>.SetDevice(string, T)”的 XML 注释
        int SetDevice(string iDeviceName, T iData);
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>.SetDevice(string, T)”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>.SetDevice(PLCDeviceType, int, T)”的 XML 注释
        int SetDevice(PLCDeviceType iType, int iAddress, T iData);
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>.SetDevice(PLCDeviceType, int, T)”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>.GetDevice(string, out T)”的 XML 注释
        int GetDevice(string iDeviceName, out T oData);
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>.GetDevice(string, out T)”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>.GetDevice(PLCDeviceType, int, out T)”的 XML 注释
        int GetDevice(PLCDeviceType iType, int iAddress, out T oData);
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IPLC<PLCDeviceType, T>.GetDevice(PLCDeviceType, int, out T)”的 XML 注释
    }
}
