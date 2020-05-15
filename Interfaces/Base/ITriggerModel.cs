using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMotion.Interfaces.Base
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“DataReceiveCompleteEventHandler”的 XML 注释
    public delegate void DataReceiveCompleteEventHandler(object sender, string result);
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“DataReceiveCompleteEventHandler”的 XML 注释

#pragma warning disable CS1591 // 缺少对公共可见类型或成员“TriggerArgs”的 XML 注释
    public struct TriggerArgs
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“TriggerArgs”的 XML 注释
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“TriggerArgs.sender”的 XML 注释
        public object sender;
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“TriggerArgs.sender”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“TriggerArgs.tryTimes”的 XML 注释
        public byte tryTimes;
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“TriggerArgs.tryTimes”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“TriggerArgs.message”的 XML 注释
        public string message;
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“TriggerArgs.message”的 XML 注释
    }

#pragma warning disable CS1591 // 缺少对公共可见类型或成员“ITriggerModel”的 XML 注释
    public interface ITriggerModel : IDevice
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“ITriggerModel”的 XML 注释
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“ITriggerModel.DeviceDataReceiveCompelete”的 XML 注释
        event DataReceiveCompleteEventHandler DeviceDataReceiveCompelete;
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“ITriggerModel.DeviceDataReceiveCompelete”的 XML 注释

#pragma warning disable CS1591 // 缺少对公共可见类型或成员“ITriggerModel.BeginTrigger(TriggerArgs)”的 XML 注释
        IAsyncResult BeginTrigger(TriggerArgs args);
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“ITriggerModel.BeginTrigger(TriggerArgs)”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“ITriggerModel.Execute(string)”的 XML 注释
        string Execute(string cmd);
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“ITriggerModel.Execute(string)”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“ITriggerModel.Trigger(TriggerArgs)”的 XML 注释
        void Trigger(TriggerArgs args);
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“ITriggerModel.Trigger(TriggerArgs)”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“ITriggerModel.StopTrigger()”的 XML 注释
        string StopTrigger();
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“ITriggerModel.StopTrigger()”的 XML 注释
    }

#pragma warning disable CS1591 // 缺少对公共可见类型或成员“ISerialPortTriggerModel”的 XML 注释
    public interface ISerialPortTriggerModel : ISerialPort, ITriggerModel { }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“ISerialPortTriggerModel”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“INetworkTriggerModel”的 XML 注释
    public interface INetworkTriggerModel : INetWork, ITriggerModel { }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“INetworkTriggerModel”的 XML 注释

}
