using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMotion.Interfaces.IO
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“ICylinderStatusJugger”的 XML 注释
    public interface ICylinderStatusJugger
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“ICylinderStatusJugger”的 XML 注释
    {
        /// <summary>
        /// 状态判读
        /// </summary>
        void StatusJugger();
    }
}
