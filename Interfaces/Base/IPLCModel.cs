using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMotion.Interfaces.Base
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IPLCModel”的 XML 注释
    public interface IPLCModel : IDevice
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IPLCModel”的 XML 注释
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IPLCModel.ReadDeviceBlock(string, int, out int)”的 XML 注释
        int ReadDeviceBlock(string szDevice, int dwSize, out int lpdwData);
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IPLCModel.ReadDeviceBlock(string, int, out int)”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IPLCModel.ReadInt16Array(string, int)”的 XML 注释
        Int16[] ReadInt16Array(string str, int n);
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IPLCModel.ReadInt16Array(string, int)”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IPLCModel.ReadInt32Array(string, int)”的 XML 注释
        Int32[] ReadInt32Array(string str, int n);
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IPLCModel.ReadInt32Array(string, int)”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IPLCModel.WriteDeviceBlock(string, int, ref int)”的 XML 注释
        int WriteDeviceBlock(string szDevice, int dwSize, ref int lpdwData);
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IPLCModel.WriteDeviceBlock(string, int, ref int)”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IPLCModel.WriteInt16Array(string, short[])”的 XML 注释
        void WriteInt16Array(string str, Int16[] values);
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IPLCModel.WriteInt16Array(string, short[])”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IPLCModel.WriteInt32Array(string, int[])”的 XML 注释
        void WriteInt32Array(string str, Int32[] values);
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IPLCModel.WriteInt32Array(string, int[])”的 XML 注释

        //int SetBitDevice(string iDeviceName, int iSize, byte[] onOffBits);
        //int SetBitDevice(PlcDeviceType iType, int iAddress, int iSize, byte[] onOffBits);
        //int GetBitDevice(string iDeviceName, int iSize, byte[] outOnOffBits);
        //int GetBitDevice(PlcDeviceType iType, int iAddress, int iSize, byte[] outOnOffBits);
        //int WriteDeviceBlock(string iDeviceName, int iSize, int[] iData);
        //int WriteDeviceBlock(PlcDeviceType iType, int iAddress, int iSize, int[] iData);
        //int ReadDeviceBlock(string iDeviceName, int iSize, int[] oData);
        //int ReadDeviceBlock(PlcDeviceType iType, int iAddress, int iSize, int[] oData);
        //int SetDevice(string iDeviceName, int iData);
        //int SetDevice(PlcDeviceType iType, int iAddress, int iData);
        //int GetDevice(string iDeviceName, out int oData);
        //int GetDevice(PlcDeviceType iType, int iAddress, out int oData);
    }
}
