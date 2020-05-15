using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net;

namespace System.ToolKit
{
    public static partial class Extensions2
    {
        //=============================泛型============================
        #region 判断是否在范围内:Between
        ///// <summary>
        /// 泛型版本 Between 扩展
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="lowerBound"></param>
        /// <param name="upperBound"></param>
        /// <param name="includeLowerBound"></param>
        /// <param name="includeUpperBound"></param>
        /// <returns></returns>
        public static bool Between<T>(this T t, T lowerBound, T upperBound,
        bool includeLowerBound = false, bool includeUpperBound = false)
        where T : IComparable<T>
        {
            if (t == null) throw new ArgumentNullException("t");

            var lowerCompareResult = t.CompareTo(lowerBound);
            var upperCompareResult = t.CompareTo(upperBound);

            return (includeLowerBound && lowerCompareResult == 0) ||
                (includeUpperBound && upperCompareResult == 0) ||
                (lowerCompareResult > 0 && upperCompareResult < 0);
        }
        ////举例:
        ////int
        //bool b0 = 3.Between(1, 5);
        //bool b1 = 3.Between(1, 3, includeUpperBound: true);
        //bool b2 = 3.Between(3, 5, includeLowerBound: true);
        ////double
        //bool b3 = 3.14.Between(3.0, 4.0);
        ////string
        //bool b4 = "ND".Between("NA", "NC");
        ////DateTime
        //bool b5 = new DateTime(2011, 2, 17).Between(new DateTime(2011, 1, 1), new DateTime(2011, 3, 1));

        /// <summary>
        /// 带 IComparer<T> 参数的 Between 扩展
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="lowerBound"></param>
        /// <param name="upperBound"></param>
        /// <param name="comparer"></param>
        /// <param name="includeLowerBound"></param>
        /// <param name="includeUpperBound"></param>
        /// <returns></returns>
        public static bool Between<T>(this T t, T lowerBound, T upperBound, IComparer<T> comparer,
        bool includeLowerBound = false, bool includeUpperBound = false)
        {
            if (comparer == null) throw new ArgumentNullException("comparer");

            var lowerCompareResult = comparer.Compare(t, lowerBound);
            var upperCompareResult = comparer.Compare(t, upperBound);

            return (includeLowerBound && lowerCompareResult == 0) ||
                (includeUpperBound && upperCompareResult == 0) ||
                (lowerCompareResult > 0 && upperCompareResult < 0);
        }
        /// <summary>
        /// 针对 IComparable<T> 接口的 IsBetween 扩展
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="lowerBound"></param>
        /// <param name="upperBound"></param>
        /// <param name="includeLowerBound"></param>
        /// <param name="includeUpperBound"></param>
        /// <returns></returns>
        public static bool Between<T>(this IComparable<T> t, T lowerBound, T upperBound,
        bool includeLowerBound = false, bool includeUpperBound = false)
        {
            if (t == null) throw new ArgumentNullException("t");

            var lowerCompareResult = t.CompareTo(lowerBound);
            var upperCompareResult = t.CompareTo(upperBound);

            return (includeLowerBound && lowerCompareResult == 0) ||
                (includeUpperBound && upperCompareResult == 0) ||
                (lowerCompareResult > 0 && upperCompareResult < 0);
        }
        #endregion
        #region 判断大于,小于或等于:LessThan,GreatThan,LessOrEquals,GreatOrEquals
        public static bool IsLessThan<T>(this T t, T bound) where T : IComparable<T>
        {
            if (t == null) throw new ArgumentNullException("t");

            var compareResult = t.CompareTo(bound);
            return compareResult < 0;
        }
        public static bool IsGreatThan<T>(this T t, T bound) where T : IComparable<T>
        {
            if (t == null) throw new ArgumentNullException("t");

            var compareResult = t.CompareTo(bound);
            return compareResult > 0;
        }
        public static bool IsLessOrEquals<T>(this T t, T bound) where T : IComparable<T>
        {
            if (t == null) throw new ArgumentNullException("t");

            var compareResult = t.CompareTo(bound);
            return (compareResult < 0 || compareResult == 0);
        }
        public static bool IsGreatOrEquals<T>(this T t, T bound) where T : IComparable<T>
        {
            if (t == null) throw new ArgumentNullException("t");

            var compareResult = t.CompareTo(bound);
            return (compareResult > 0 || compareResult == 0);
        }
        #endregion
        #region 判断是否在数组内:In,NotIn
        public static bool In<T>(this T @this, params T[] values)
        {
            return Array.IndexOf(values, @this) != -1;
        }
        public static bool NotIn<T>(this T @this, params T[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }
        #endregion
        #region 泛型对比
        public static T Max<T>(this T val1, T val2) where T : IComparable
        {
            return val1.CompareTo(val2) > 0 ? val1 : val2;
        }
        public static T Min<T>(this T val1, T val2) where T : IComparable
        {
            return val1.CompareTo(val2) < 0 ? val1 : val2;
        }
        #endregion

        //============================非泛型===========================

        #region 判断奇偶性:IsOdd,IsEven
        /// <summary>
        /// 判断是否奇数
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsOdd(this int number)
        {
            return number % 2 == 1;
        }
        /// <summary>
        /// 判断是否偶数
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsEven(this int number)
        {
            return number % 2 == 0;
        }
        #endregion
        #region 判断质数:IsPrime
        public static bool IsPrime(this Int32 @this)
        {
            if (@this == 1) { return false; }
            if (@this == 2) { return true; }
            if (@this % 2 == 0) { return false; }
            var sqrt = (Int32)Math.Sqrt(@this);
            for (Int64 t = 3; t <= sqrt; t = t + 2)
            {
                if (@this % t == 0)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion
        #region 因子与倍数:FactorOf,IsMultipleOf,Factors
        /// <summary>
        ///     An Int32 extension method that factor of.举例:3.FactorOf(12),3是12的因子
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="factorNumer">The factor numer.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public static bool FactorOf(this Int32 @this, Int32 factorNumer)
        {
            return factorNumer % @this == 0;
        }
        /// <summary>
        ///     An Int32 extension method that query if '@this' is multiple of.举例:12.IsMultipleOf(3),12是3的倍数
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="factor">The factor.</param>
        /// <returns>true if multiple of, false if not.</returns>
        public static bool IsMultipleOf(this Int32 @this, Int32 factor)
        {
            return @this % factor == 0;
        }
        /// <summary>
        /// 获取整数的所有因数
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static int[] Factors(this int @this)
        {
            List<int> list = new List<int>();
            while (@this > 1)
            {
                for (int i = 2; i <= @this; i++)
                {
                    if (@this % i == 0)
                    {
                        list.Add(i);
                        @this /= i;
                    }
                }
            }
            list.Sort();
            return list.ToArray();
        }
        #endregion



        #region 无穷大IsInfinity,非法IsNaN,负无穷IsNegativeInfinity,正无穷IsPositiveInfinity
        public static Boolean IsInfinity(this Double d)
        {
            return Double.IsInfinity(d);
        }
        public static Boolean IsNaN(this Double d)
        {
            return Double.IsNaN(d);
        }
        public static Boolean IsNegativeInfinity(this Double d)
        {
            return Double.IsNegativeInfinity(d);
        }
        public static Boolean IsPositiveInfinity(this Double d)
        {
            return Double.IsPositiveInfinity(d);
        }
        #endregion

        #region 最大最小值,绝对值,阶乘等:Max,Min,Abs,BigMul,DivRem,Factorial
        public static Int32 Max(this Int32 val1, params Int32[] val2)
        {
            return Math.Max(val1, val2.Max());
        }
        public static double Average(this Int32 val1, params Int32[] val2)
        {
            return (val1 + val2.Sum()) / (1 + val2.Length);
        }
        public static Int32 Min(this Int32 val1, params Int32[] val2)
        {
            return Math.Min(val1, val2.Min());
        }
        public static Int32 Abs(this Int32 value)
        {
            return Math.Abs(value);
        }
        public static Int64 BigMul(this Int32 a, Int32 b)
        {
            return Math.BigMul(a, b);
        }
        public static Int32 DivRem(this Int32 a, Int32 b, out Int32 result)
        {
            return Math.DivRem(a, b, out result);
        }

        public static long Factorial(this long x)
        {
            return ((x <= 1) ? 1 : x * (Factorial(x - 1)));
        }
        public static long Factorial(this int x)
        {
            return Factorial(Convert.ToInt64(x));
        }
        #endregion

        #region Math(double):ToMoney,Acos,Asin,
        public static Double ToMoney(this Double @this)
        {
            return Math.Round(@this, 2);
        }
        public static Double Acos(this Double d)
        {
            return Math.Acos(d);
        }
        public static Double Asin(this Double d)
        {
            return Math.Asin(d);
        }
        public static Double Atan(this Double d)
        {
            return Math.Atan(d);
        }
        public static Double Atan2(this Double y, Double x)
        {
            return Math.Atan2(y, x);
        }
        public static Double Ceiling(this Double a)
        {
            return Math.Ceiling(a);
        }
        public static Double Cos(this Double d)
        {
            return Math.Cos(d);
        }
        public static Double Cosh(this Double value)
        {
            return Math.Cosh(value);
        }
        public static Double Exp(this Double d)
        {
            return Math.Exp(d);
        }
        public static Double Floor(this Double d)
        {
            return Math.Floor(d);
        }
        public static Double IEEERemainder(this Double x, Double y)
        {
            return Math.IEEERemainder(x, y);
        }
        public static Double Log(this Double d)
        {
            return Math.Log(d);
        }
        public static Double Log(this Double d, Double newBase)
        {
            return Math.Log(d, newBase);
        }
        public static Double Log10(this Double d)
        {
            return Math.Log10(d);
        }
        public static Double Pow(this Double x, Double y)
        {
            return Math.Pow(x, y);
        }
        public static double Pow(this int a, double b)
        {
            return Math.Pow(a, b);
        }
        public static Double Round(this Double a)
        {
            return Math.Round(a);
        }
        public static Double Round(this Double a, Int32 digits)
        {
            return Math.Round(a, digits);
        }
        public static Double Round(this Double a, MidpointRounding mode)
        {
            return Math.Round(a, mode);
        }
        public static Double Round(this Double a, Int32 digits, MidpointRounding mode)
        {
            return Math.Round(a, digits, mode);
        }
        public static Int32 Sign(this Int32 value)
        {
            return Math.Sign(value);
        }
        public static Double Sin(this Double a)
        {
            return Math.Sin(a);
        }
        public static Double Sinh(this Double value)
        {
            return Math.Sinh(value);
        }
        public static Double Sqrt(this Double d)
        {
            return Math.Sqrt(d);
        }
        public static Double Tan(this Double a)
        {
            return Math.Tan(a);
        }
        public static Double Tanh(this Double value)
        {
            return Math.Tanh(value);
        }
        public static Double Truncate(this Double d)
        {
            return Math.Truncate(d);
        }
        #endregion
        #region 强制转换:Coerce
        /// <summary>
        /// 强制转换为范围内的值,-1.Coerce(0,4)的值为0
        /// </summary>
        /// <param name="val"></param>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <returns></returns>
        public static int Coerce(this int val, int lower, int upper)
        {
            if (val >= upper) { return upper; }
            else if (val <= lower) { return lower; }
            else { return val; }
        }
        #endregion
        #region BitConverter:GetBytes,Int64BitsToDouble
        /// <summary>
        ///     Returns the specified 32-bit signed integer value as an array of bytes.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>An array of bytes with length 4.</returns>
        public static Byte[] GetBytes(this Int32 value)
        {
            return BitConverter.GetBytes(value);
        }
        /// <summary>
        ///     Converts the specified 64-bit signed integer to a double-precision floating point number.
        /// </summary>
        /// <param name="value">The number to convert.</param>
        /// <returns>A double-precision floating point number whose value is equivalent to .</returns>
        public static Double Int64BitsToDouble(this Int64 value)
        {
            return BitConverter.Int64BitsToDouble(value);
        }
        #endregion

        #region DateTime:DaysInMonth,IsLeapYear,Int64.FromBinary,Int64.FromFileTime,Int64.FromFileTimeUtc
        /// <summary>
        ///     Returns the number of days in the specified month and year.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month (a number ranging from 1 to 12).</param>
        /// <returns>
        ///     The number of days in  for the specified .For example, if  equals 2 for February, the return value is 28 or
        ///     29 depending upon whether  is a leap year.
        /// </returns>
        public static Int32 DaysInMonth(this Int32 year, Int32 month)
        {
            return DateTime.DaysInMonth(year, month);
        }
        public static bool IsLeapYear(this int year)
        {
            return (year % 400 == 0) || (year % 4 == 0 && year % 100 != 0) ? true : false;
        }
        /// <summary>
        ///     Deserializes a 64-bit binary value and recreates an original serialized  object.
        /// </summary>
        /// <param name="dateData">
        ///     A 64-bit signed integer that encodes the  property in a 2-bit field and the  property in
        ///     a 62-bit field.
        /// </param>
        /// <returns>An object that is equivalent to the  object that was serialized by the  method.</returns>
        public static DateTime FromBinary(this Int64 dateData)
        {
            return DateTime.FromBinary(dateData);
        }
        /// <summary>
        ///     Converts the specified Windows file time to an equivalent local time.
        /// </summary>
        /// <param name="fileTime">A Windows file time expressed in ticks.</param>
        /// <returns>
        ///     An object that represents the local time equivalent of the date and time represented by the  parameter.
        /// </returns>
        public static DateTime FromFileTime(this Int64 fileTime)
        {
            return DateTime.FromFileTime(fileTime);
        }
        /// <summary>
        ///     Converts the specified Windows file time to an equivalent UTC time.
        /// </summary>
        /// <param name="fileTime">A Windows file time expressed in ticks.</param>
        /// <returns>
        ///     An object that represents the UTC time equivalent of the date and time represented by the  parameter.
        /// </returns>
        public static DateTime FromFileTimeUtc(this Int64 fileTime)
        {
            return DateTime.FromFileTimeUtc(fileTime);
        }
        #endregion
        #region Drawing.Color.FromArgb
        /// <summary>
        ///     Creates a  structure from a 32-bit ARGB value.
        /// </summary>
        /// <param name="argb">A value specifying the 32-bit ARGB value.</param>
        /// <returns>The  structure that this method creates.</returns>
        public static Color FromArgb(this Int32 argb)
        {
            return Color.FromArgb(argb);
        }

        /// <summary>
        ///     Creates a  structure from the four ARGB component (alpha, red, green, and blue) values. Although this method
        ///     allows a 32-bit value to be passed for each component, the value of each component is limited to 8 bits.
        /// </summary>
        /// <param name="argb">A value specifying the 32-bit ARGB value.</param>
        /// <param name="red">The red component. Valid values are 0 through 255.</param>
        /// <param name="green">The green component. Valid values are 0 through 255.</param>
        /// <param name="blue">The blue component. Valid values are 0 through 255.</param>
        /// <returns>The  that this method creates.</returns>
        /// ###
        /// <param name="alpha">The alpha component. Valid values are 0 through 255.</param>
        public static Color FromArgb(this Int32 argb, Int32 red, Int32 green, Int32 blue)
        {
            return Color.FromArgb(argb, red, green, blue);
        }

        /// <summary>
        ///     Creates a  structure from the specified  structure, but with the new specified alpha value. Although this
        ///     method allows a 32-bit value to be passed for the alpha value, the value is limited to 8 bits.
        /// </summary>
        /// <param name="argb">A value specifying the 32-bit ARGB value.</param>
        /// <param name="baseColor">The  from which to create the new .</param>
        /// <returns>The  that this method creates.</returns>
        /// ###
        /// <param name="alpha">The alpha value for the new . Valid values are 0 through 255.</param>
        public static Color FromArgb(this Int32 argb, Color baseColor)
        {
            return Color.FromArgb(argb, baseColor);
        }

        /// <summary>
        ///     Creates a  structure from the specified 8-bit color values (red, green, and blue). The alpha value is
        ///     implicitly 255 (fully opaque). Although this method allows a 32-bit value to be passed for each color
        ///     component, the value of each component is limited to 8 bits.
        /// </summary>
        /// <param name="argb">A value specifying the 32-bit ARGB value.</param>
        /// <param name="green">The green component value for the new . Valid values are 0 through 255.</param>
        /// <param name="blue">The blue component value for the new . Valid values are 0 through 255.</param>
        /// <returns>The  that this method creates.</returns>
        /// ###
        /// <param name="red">The red component value for the new . Valid values are 0 through 255.</param>
        public static Color FromArgb(this Int32 argb, Int32 green, Int32 blue)
        {
            return Color.FromArgb(argb, green, blue);
        }
        #endregion
        #region Drawing.ColorTranslator
        /// <summary>
        ///     Translates an OLE color value to a GDI+  structure.
        /// </summary>
        /// <param name="oleColor">The OLE color to translate.</param>
        /// <returns>The  structure that represents the translated OLE color.</returns>
        public static Color FromOle(this Int32 oleColor)
        {
            return ColorTranslator.FromOle(oleColor);
        }
        /// <summary>
        ///     Translates a Windows color value to a GDI+  structure.
        /// </summary>
        /// <param name="win32Color">The Windows color to translate.</param>
        /// <returns>The  structure that represents the translated Windows color.</returns>
        public static Color FromWin32(this Int32 win32Color)
        {
            return ColorTranslator.FromWin32(win32Color);
        }
        #endregion
        #region Net.IPAddress:HostToNetworkOrder,NetworkToHostOrder
        /// <summary>
        ///     Converts an integer value from host byte order to network byte order.
        /// </summary>
        /// <param name="host">The number to convert, expressed in host byte order.</param>
        /// <returns>An integer value, expressed in network byte order.</returns>
        public static Int32 HostToNetworkOrder(this Int32 host)
        {
            return IPAddress.HostToNetworkOrder(host);
        }
        /// <summary>
        ///     Converts an integer value from network byte order to host byte order.
        /// </summary>
        /// <param name="network">The number to convert, expressed in network byte order.</param>
        /// <returns>An integer value, expressed in host byte order.</returns>
        public static Int32 NetworkToHostOrder(this Int32 network)
        {
            return IPAddress.NetworkToHostOrder(network);
        }
        #endregion

        #region 类型转换(与数值相关的):ToDouble,ToFloat,ToUInt16,ToInt16,ToInt/IsInt,IsBool/ToBool,ToEnum,ToString

        #region ToDouble
        public static double ToDouble(this int @this)
        {
            return Convert.ToDouble(@this);
        }
        public static double[] ToDouble(this int[] @this)
        {
            return @this.Select(x => x.ToDouble()).ToArray();
        }

        public static double ToDouble(this long @this)
        {
            return Convert.ToDouble(@this);
        }
        public static double ToDouble(this decimal @this)
        {
            return Convert.ToDouble(@this);
        }

        public static double ToDouble(this string t)
        {
            double id;
            double.TryParse(t, out id);//这里当转换失败时返回的id为0
            return id;
        }
        public static double[] ToDouble(this string[] t)
        {
            return t.Select(x => x.ToDouble()).ToArray();
        }
        #endregion
        #region ToFloat
        public static float ToFloat(this int @this)
        {
            return Convert.ToSingle(@this);
        }
        public static float ToFloat(this double @this)
        {
            return Convert.ToSingle(@this);
        }
        public static float ToFloat(this long @this)
        {
            return Convert.ToSingle(@this);
        }
        public static float ToFloat(this string t)
        {
            float id;
            float.TryParse(t, out id);//这里当转换失败时返回的id为0
            return id;
        }
        #endregion
        #region ToDecimal
        public static decimal ToDecimal(this int @this)
        {
            return Convert.ToDecimal(@this);
        }
        public static decimal ToDecimal(this double @this)
        {
            return Convert.ToDecimal(@this);
        }
        public static decimal ToDecimal(this long @this)
        {
            return Convert.ToDecimal(@this);
        }
        public static decimal ToDecimal(this string t)
        {
            decimal id;
            decimal.TryParse(t, out id);//这里当转换失败时返回的id为0
            return id;
        }
        #endregion
        #region ToUInt16
        public static ushort ToUInt16(this short @this)
        {
            return Convert.ToUInt16(@this);
        }
        public static ushort ToUInt16(this int @this)
        {
            return Convert.ToUInt16(@this);
        }
        public static ushort ToUInt16(this double @this)
        {
            return Convert.ToUInt16(@this);
        }
        public static ushort ToUInt16(this long @this)
        {
            return Convert.ToUInt16(@this);
        }
        public static ushort ToUInt16(this string t)
        {
            ushort id;
            ushort.TryParse(t, out id);//这里当转换失败时返回的id为0
            return id;
        }
        #endregion
        #region ToInt16
        public static short ToInt16(this short @this)
        {
            return Convert.ToInt16(@this);
        }
        public static short ToInt16(this int @this)
        {
            return Convert.ToInt16(@this);
        }
        public static short ToInt16(this double @this)
        {
            return Convert.ToInt16(@this);
        }
        public static short ToInt16(this long @this)
        {
            return Convert.ToInt16(@this);
        }
        public static short ToInt16(this string t)
        {
            short id;
            short.TryParse(t, out id);//这里当转换失败时返回的id为0
            return id;
        }
        #endregion
        #region ToInt/IsInt
        public static int ToInt<T>(this T @this)
        {
            return Convert.ToInt32(@this);
        }

        public static bool IsInt(this string s)
        {
            int i;
            return int.TryParse(s, out i);
        }
        public static int[] ToInt<T>(this T[] _src)
        {
            int[] _dst = new int[_src.Length];
            for (int i = 0; i < _src.Length; i++)
            {
                _dst[i] = Convert.ToInt32(_src[i]);
            }
            return _dst;
        }
        public static int[,] ToInt<T>(this T[,] _src)
        {
            int[,] _dst = new int[_src.GetLength(0), _src.GetLength(1)];
            for (int i = 0; i < _src.GetLength(0); i++)
            {
                for (int j = 0; j < _src.GetLength(1); j++)
                {
                    _dst[i, j] = Convert.ToInt32(_src[i, j]);
                }
            }
            return _dst;
        }
        /// <summary>
        /// 其他进制字符串转换为十进制数值
        /// </summary>
        /// <param name="s"></param>
        /// <param name="fromBase"></param>
        /// <returns></returns>
        public static int ToInt(this string s, int fromBase)
        {
            return Convert.ToInt32(s, fromBase);
        }
        public static int[] ToInt(this string[] @this)
        {
            return @this.Select(x => x.ToInt()).ToArray();
        }

        public static int ToInt(this char @this)
        {
            return int.Parse(@this.ToString());
        }

        public static int ToInt(this decimal @this)
        {
            return Convert.ToInt32(@this);
        }
        #endregion
        #region ToByte
        public static byte ToByte<T>(this T @this)
        {
            return Convert.ToByte(@this);
        }
        #endregion
        #region IsBoolean/ToBoolean
        /// <summary> 
        /// 将布尔的字符串表示形式转换为它的等效布尔值。一个指示转换是否成功的返回值 
        /// </summary> 
        /// <param name="input">包含要转换的数字的字符串</param> 
        /// <returns>如果 input 转换成功，则为返回转换后的布尔值；否则为 false</returns> 
        public static bool IsBool(this string input)
        {
            if (input.Equals("true", StringComparison.CurrentCultureIgnoreCase)) return true;
            else if (input.Equals("false", StringComparison.InvariantCultureIgnoreCase)) return false;
            else return false;  // 考虑是否抛出异常? 
        }
        /// <summary> 
        /// 将布尔的字符串表示形式转换为它的等效布尔值 
        /// </summary> 
        /// <param name="input">包含要转换的数字的字符串</param> 
        /// <returns>如果 input 转换成功，则为返回转换后的布尔值；否则为 false</returns> 
        public static bool ToBool(this string input)
        {
            if (input.Equals("true", StringComparison.CurrentCultureIgnoreCase)) return true;
            else if (input.Equals("false", StringComparison.InvariantCultureIgnoreCase)) return false;
            else return false; // 考虑是否抛出异常? 
        }
        #endregion
        #region ToEnum
        public static T ToEnum<T>(this string @this)
        {
            Type enumType = typeof(T);
            return (T)Enum.Parse(enumType, @this);
        }
        #endregion
        #region ToString
        /// <summary>
        /// 十进制数值转换为其他进制字符串
        /// </summary>
        /// <param name="this"></param>
        /// <param name="toBase"></param>
        /// <returns></returns>
        public static string ToString(this int @this, int toBase)
        {
            return Convert.ToString(@this, toBase);
        }
        #endregion

        #endregion
        #region 数与位数转换:NumToBits,Length,BitsToNum
        /// <summary>
        /// 数组转换为位数,例如:123.NumToBits()=>{1,2,3}
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int[] NumToBits(this int value)
        {
            List<int> lst = new List<int>();
            foreach (var item in value.ToString())
            {
                lst.Add(Convert.ToInt32(item.ToString()));
            }
            return lst.ToArray();
        }
        /// <summary>
        /// 获取整数的位数,例如:1234.Length=>4
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int Length(this int value)
        {
            return value.ToString().Length;
        }
        /// <summary>
        /// 位数转换成数值,例如:new int[]{1,2,3}.BitsToNum()=>123
        /// </summary>
        /// <param name="bits"></param>
        /// <returns></returns>
        public static int BitsToNum(this int[] bits)
        {
            string s = "";
            foreach (var item in bits)
            {
                s += item;
            }
            return Convert.ToInt32(s);
        }
        #endregion
        #region 单位转换:ToPercent,ToPermille,ToPPB,ToPPM,FromPercent,FromPermille,FromPPB,FromPPM,PercentChange,PercentOf,PercentTotal
        public static double ToPercent(this double @this)
        {
            return @this * 100;
        }
        public static double ToPermille(this double @this)
        {
            return @this * 1000;
        }
        public static double ToPPB(this double @this)
        {
            return @this * 1000 * 1000 * 1000;
        }
        public static double ToPPM(this double @this)
        {
            return @this * 1000 * 1000;
        }

        public static double FromPercent(this double @this)
        {
            return @this / 100;
        }
        public static double FromPermille(this double @this)
        {
            return @this / 1000;
        }
        public static double FromPPB(this double @this)
        {
            return @this / 1000 / 1000 / 1000;
        }
        public static double FromPPM(this double @this)
        {
            return @this / 1000 / 1000;
        }
        /// <summary>
        /// 12.PercentChange(9)=-25,即9比12降低百分之25
        /// </summary>
        /// <param name="this"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public static double PercentChange(this double @this, double newValue)
        {
            return (@this / newValue - 1) / 100;
        }
        /// <summary>
        /// a的百分之b,举例:10.PercentOf(50)=2
        /// </summary>
        /// <param name="this"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public static double PercentOf(this double @this, double newValue)
        {
            return @this / 100 * newValue;
        }
        /// <summary>
        /// b是a的100比值,举例:10.PercentTotal(2)=20,即2是10的百分之20
        /// </summary>
        /// <param name="this"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        public static double PercentTotal(this double @this, double newValue)
        {
            return newValue / @this * 100;
        }
        #endregion
        #region 温度转换:Temp_C_To_F,Temp_C_To_K,Temp_F_To_C,Temp_F_To_K,Temp_K_To_C,Temp_K_To_F
        public static double Temp_C_To_F(this double @this)
        {
            return @this * 1.8 + 32;
        }
        public static double Temp_C_To_K(this double @this)
        {
            return @this + 273.15;
        }
        public static double Temp_F_To_C(this double @this)
        {
            return (@this - 32) * 5 / 9;
        }
        public static double Temp_F_To_K(this double @this)
        {
            return @this.Temp_F_To_C().Temp_C_To_K();
        }
        public static double Temp_K_To_C(this double @this)
        {
            return @this - 273.15;
        }
        public static double Temp_K_To_F(this double @this)
        {
            return @this.Temp_K_To_C().Temp_C_To_F();
        }
        #endregion
    }
}
