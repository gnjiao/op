using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace System.ToolKit
{
    public static partial class Extensions
    {
        #region 字节或字节数组转换为十六进制字符串
        /// <summary>
        /// 转换为十六进制字符串
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string ToHex(this byte b)
        {
            return b.ToString("X2");
        }
        /// <summary>
        /// 转换为十六进制字符串(返回的十六进制字符串是连着的，为了阅读方便会用一个空格分开)
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToHex(this IEnumerable<byte> bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
                sb.Append(b.ToString("X2"));
            return sb.ToString();
        }
        #endregion
        #region 转换为Base64字符串
        /// <summary>
        /// 转换为Base64字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToBase64String(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }
        #endregion
        #region 转换为Int32,Int64
        public static int ToInt32(this byte[] value, int startIndex)
        {
            return BitConverter.ToInt32(value, startIndex);
        }
        public static long ToInt64(this byte[] value, int startIndex)
        {
            return BitConverter.ToInt64(value, startIndex);
        }
        #endregion
        #region 转换为指定编码的字符串
        /// <summary>
        /// 转换为指定编码的字符串
        /// </summary>
        /// <param name="data"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Decode(this byte[] data, Encoding encoding)
        {
            return encoding.GetString(data);
        }
        #endregion
        #region 使用指定或默认算法Hash
        //使用指定算法Hash
        public static byte[] Hash(this byte[] data, string hashName)
        {
            HashAlgorithm algorithm;
            if (string.IsNullOrEmpty(hashName)) algorithm = HashAlgorithm.Create();
            else algorithm = HashAlgorithm.Create(hashName);
            return algorithm.ComputeHash(data);
        }
        //使用默认算法Hash
        public static byte[] Hash(this byte[] data)
        {
            return Hash(data, null);
        }
        #endregion
        #region 获取和设置Bit
        //index从0开始
        //获取取第index是否为1
        public static bool GetBit(this byte b, int index)
        {
            return (b & (1 << index)) > 0;
        }
        //将第index位设为1
        public static byte SetBit(this byte b, int index)
        {
            b |= (byte)(1 << index);
            return b;
        }
        #endregion
        #region 指定位置清除Bit,位取反
        //将第index位设为0
        public static byte ClearBit(this byte b, int index)
        {
            b &= (byte)((1 << 8) - 1 - (1 << index));
            return b;
        }
        //将第index位取反
        public static byte ReverseBit(this byte b, int index)
        {
            b ^= (byte)(1 << index);
            return b;
        }
        #endregion
        #region 保存为文件
        /// <summary>
        /// 保存为文件
        /// </summary>
        /// <param name="data"></param>
        /// <param name="path"></param>
        public static void Save(this byte[] data, string path)
        {
            File.WriteAllBytes(path, data);
        }
        #endregion
        #region 转换为内存流
        /// <summary>
        /// 转换为内存流
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static MemoryStream ToMemoryStream(this byte[] data)
        {
            return new MemoryStream(data);
        }
        #endregion
        #region 求最大值,最小值
        /// <summary>
        ///     Returns the larger of two 8-bit unsigned integers.
        /// </summary>
        /// <param name="val1">The first of two 8-bit unsigned integers to compare.</param>
        /// <param name="val2">The second of two 8-bit unsigned integers to compare.</param>
        /// <returns>Parameter  or , whichever is larger.</returns>
        public static Byte Max(this Byte val1, Byte val2)
        {
            return Math.Max(val1, val2);
        }
        /// <summary>
        ///     Returns the smaller of two 8-bit unsigned integers.
        /// </summary>
        /// <param name="val1">The first of two 8-bit unsigned integers to compare.</param>
        /// <param name="val2">The second of two 8-bit unsigned integers to compare.</param>
        /// <returns>Parameter  or , whichever is smaller.</returns>
        public static Byte Min(this Byte val1, Byte val2)
        {
            return Math.Min(val1, val2);
        }
        #endregion



    }
}
