using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace System.ToolKit
{
    //含有布尔类型的方法都放在这里,包括数组/数值与布尔的相互转换
    public static partial class Extensions
    {
        #region 布尔与整数间转换:ToInt,ToBool
        /// <summary>
        /// 布尔值转换为0和1
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static int ToInt(this bool @this)
        {
            return @this ? 1 : 0;
        }
        /// <summary>
        /// 数值大于0为True,否则为False
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool ToBool(this int @this)
        {
            return @this>0 ? true : false;
        }
        #endregion
        #region 数组与,数组或:ArrayAnd,ArrayOr
        /// <summary>
        /// 数组与
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool ArrayAnd(this bool[] @this)
        {
            return !(@this.Contains(false));
        }
        /// <summary>
        /// 数组或
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool ArrayOr(this bool[] @this)
        {
            return @this.Contains(true);
        }
        #endregion
        #region 布尔为真或假时,执行动作:IfFalse,IfTrue
        /// <summary>
        /// 布尔为False时执行Action动作
        /// </summary>
        /// <param name="this"></param>
        /// <param name="action"></param>
        public static void IfFalse(this bool @this, Action action)
        {
            if (!@this)
            {
                action();
            }
        }
        /// <summary>
        /// 布尔为True时执行Action动作
        /// </summary>
        /// <param name="this"></param>
        /// <param name="action"></param>
        public static void IfTrue(this bool @this, Action action)
        {
            if (@this)
            {
                action();
            }
        }
        #endregion
        #region 布尔转换为字节数组:ToByte
        /// <summary>
        /// 布尔转换为字节
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static byte ToByte(this bool @this)
        {
            return Convert.ToByte(@this);
        }
        #endregion
    }
}
