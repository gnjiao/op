using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMotion.Interfaces
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IAlarmsResult”的 XML 注释
    public  interface IAlarmsResult
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IAlarmsResult”的 XML 注释
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IAlarmsResult.IsAlarms”的 XML 注释
         bool IsAlarms { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IAlarmsResult.IsAlarms”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IAlarmsResult.IsPrompt”的 XML 注释
         bool IsPrompt { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IAlarmsResult.IsPrompt”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IAlarmsResult.IsWarning”的 XML 注释
         bool IsWarning { get; set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IAlarmsResult.IsWarning”的 XML 注释
    }
}
