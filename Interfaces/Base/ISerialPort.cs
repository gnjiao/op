using System.IO.Ports;

namespace CMotion.Interfaces.Base
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“ISerialPort”的 XML 注释
    public interface ISerialPort
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“ISerialPort”的 XML 注释
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“ISerialPort.BaudRate”的 XML 注释
        int BaudRate { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“ISerialPort.BaudRate”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“ISerialPort.DataBits”的 XML 注释
        int DataBits { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“ISerialPort.DataBits”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“ISerialPort.DtrEnable”的 XML 注释
        bool DtrEnable { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“ISerialPort.DtrEnable”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“ISerialPort.IsOpen”的 XML 注释
        bool IsOpen { get; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“ISerialPort.IsOpen”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“ISerialPort.Parity”的 XML 注释
        Parity Parity { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“ISerialPort.Parity”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“ISerialPort.PortName”的 XML 注释
        string PortName { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“ISerialPort.PortName”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“ISerialPort.ReadTimeout”的 XML 注释
        int ReadTimeout { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“ISerialPort.ReadTimeout”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“ISerialPort.RtsEnable”的 XML 注释
        bool RtsEnable { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“ISerialPort.RtsEnable”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“ISerialPort.StopBits”的 XML 注释
        StopBits StopBits { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“ISerialPort.StopBits”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“ISerialPort.WriteTimeout”的 XML 注释
        int WriteTimeout { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“ISerialPort.WriteTimeout”的 XML 注释
    }
}
