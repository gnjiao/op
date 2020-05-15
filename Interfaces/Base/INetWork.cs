using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace CMotion.Interfaces.Base
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“INetWork”的 XML 注释
    public interface INetWork
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“INetWork”的 XML 注释
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“INetWork.AddressFamily”的 XML 注释
        AddressFamily AddressFamily { get; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“INetWork.AddressFamily”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“INetWork.Connected”的 XML 注释
        bool Connected { get; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“INetWork.Connected”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“INetWork.IPAddr”的 XML 注释
        IPAddress IPAddr{get;set;}       
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“INetWork.IPAddr”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“INetWork.IsBound”的 XML 注释
        bool IsBound { get; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“INetWork.IsBound”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“INetWork.LocalEndPoint”的 XML 注释
        EndPoint LocalEndPoint { get; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“INetWork.LocalEndPoint”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“INetWork.Port”的 XML 注释
        int Port { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“INetWork.Port”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“INetWork.ProtocolType”的 XML 注释
        ProtocolType ProtocolType { get; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“INetWork.ProtocolType”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“INetWork.ReceiveTimeout”的 XML 注释
        int ReceiveTimeout { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“INetWork.ReceiveTimeout”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“INetWork.RemoteEndPoint”的 XML 注释
        EndPoint RemoteEndPoint { get; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“INetWork.RemoteEndPoint”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“INetWork.SendTimeout”的 XML 注释
        int SendTimeout { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“INetWork.SendTimeout”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“INetWork.SocketType”的 XML 注释
        SocketType SocketType { get; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“INetWork.SocketType”的 XML 注释
    }
}
