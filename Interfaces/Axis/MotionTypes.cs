using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMotion.Interfaces.Axis
{
    /// <summary>
    /// 轴运动类型
    /// </summary>
    public enum  MotionTypes :byte
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“MotionTypes.直线插补”的 XML 注释
        直线插补,
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“MotionTypes.直线插补”的 XML 注释
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“MotionTypes.圆弧插补”的 XML 注释
        圆弧插补
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“MotionTypes.圆弧插补”的 XML 注释
    }
}
