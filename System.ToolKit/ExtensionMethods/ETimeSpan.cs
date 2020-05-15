using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ToolKit
{
    public static partial class Extensions
    {
        #region Weeks
        public static TimeSpan Weeks(this Int32 @this)
        {
            return TimeSpan.FromDays(@this*7);
        }
        #endregion
        #region Days
        public static TimeSpan Days(this Int32 @this)
        {
            return TimeSpan.FromDays(@this);
        }
        #endregion
        #region Hour
        public static TimeSpan Hours(this Int32 @this)
        {
            return TimeSpan.FromHours(@this);
        }
        #endregion
        #region Minutes
        public static TimeSpan Minutes(this Int32 @this)
        {
            return TimeSpan.FromMinutes(@this);
        }
        #endregion
        #region Seconds
        public static TimeSpan Seconds(this Int32 @this)
        {
            return TimeSpan.FromSeconds(@this);
        }
        #endregion
        #region Milliseconds
        public static TimeSpan Milliseconds(this Int32 @this)
        {
            return TimeSpan.FromMilliseconds(@this);
        }
        #endregion
    }
}
