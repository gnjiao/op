using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMotion.Interfaces.Base
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IDevice”的 XML 注释
    public interface IDevice
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IDevice”的 XML 注释
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IDevice.Name”的 XML 注释
        string Name { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IDevice.Name”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IDevice.ConnectionParam”的 XML 注释
        string ConnectionParam { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IDevice.ConnectionParam”的 XML 注释

#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IDevice.DeviceOpen()”的 XML 注释
        void DeviceOpen();
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IDevice.DeviceOpen()”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IDevice.DeviceClose()”的 XML 注释
        void DeviceClose();
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IDevice.DeviceClose()”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IDevice.SetConnectionParam(string)”的 XML 注释
        void SetConnectionParam(string param);
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IDevice.SetConnectionParam(string)”的 XML 注释
    }
}
