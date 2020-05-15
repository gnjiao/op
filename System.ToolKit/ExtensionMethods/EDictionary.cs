using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace System.ToolKit
{
    public static partial class Extensions4
    {
        #region 向字典中添加键和值
        /// <summary>
        /// 尝试将键和值添加到字典中：如果不存在，才添加；存在，不添加也不抛导常
        /// </summary>
        public static Dictionary<TKey, TValue> TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key) == false) dict.Add(key, value);
            return dict;
        }
        /// <summary>
        /// 将键和值添加或替换到字典中：如果不存在，则添加；存在，则替换
        /// </summary>
        public static Dictionary<TKey, TValue> AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            dict[key] = value;
            return dict;
        }

        ////范例:
        //dict.TryAdd(2, "Banana");
        //dict.AddOrReplace(3, "Orange");

        //dict.TryAdd(1, "A")
        //    .TryAdd(2, "B")
        //    .AddOrReplace(3, "C")
        //    .AddOrReplace(4, "D")
        //    .TryAdd(5, "E");
        #endregion

        #region 获取值
        /// <summary>
        /// 获取与指定的键相关联的值，如果没有则返回输入的默认值
        /// 举例:var v1 = dict.GetValue(2);         //不存在则返回 null
        ///      var v2 = dict.GetValue(2, "abc");  //不存在返回 ”abc“
        /// </summary>
        public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue defaultValue = default(TValue))
        {
            return dict.ContainsKey(key) ? dict[key] : defaultValue;
        }
        #endregion

        #region 批量添加 
        /// <summary>
        /// 向字典中批量添加键值对
        /// </summary>
        /// <param name="replaceExisted">如果已存在，是否替换</param>
        public static Dictionary<TKey, TValue> AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dict, IEnumerable<KeyValuePair<TKey, TValue>> values, bool replaceExisted)
        {
            foreach (var item in values)
            {
                if (dict.ContainsKey(item.Key) == false || replaceExisted)
                    dict[item.Key] = item.Value;
            }
            return dict;
        }

        ////举例:
        //var dict1 = new Dictionary<int, int>()
        //     .AddOrReplace(2, 2)
        //     .AddOrReplace(3, 3);
        //var dict2 = new Dictionary<int, int>()
        //    .AddOrReplace(1, 1)
        //    .AddOrReplace(2, 1)
        //    .AddRange(dict1, false);
        #endregion
    }
}
