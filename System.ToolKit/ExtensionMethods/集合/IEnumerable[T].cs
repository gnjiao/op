using System.ToolKit;
using System;
using System.Collections.Generic;
using System.Linq;

public static partial class Extensions
{
    #region 调用范例
    //int[] data = { 11, 12, 13, 14, 15, 17, 19, 20, 21 };
    //data.ForEach((x) => Console.WriteLine(x)).PrintEx();
    //data.ForEach((x, i) => Console.WriteLine(x+"  "+i)).PrintEx();
    //data.ForEach((x) => x* 100 + 1).PrintEx();
    //data.ForEach((x, i) => x*100 + i).PrintEx();

    //int[] a = { 11, 12, 13, 14, 15, 16, 17, 16, 15 };
    //List<int> b = new List<int> { 11, 12, 13, 14, 15, 16, 17, 16, 15 };
    //a.GetOddIndexItem().PrintEx();
    //b.GetOddIndexItem().PrintEx();
    //a.RemoveDuplates().PrintEx();

    //b.AddIfNotContains(11).PrintEx();
    #endregion

    #region 包含与判断
    /// <summary>
    ///     An IEnumerable&lt;T&gt; extension method that query if '@this' contains all.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="values">A variable-length parameters list containing values.</param>
    /// <returns>true if it succeeds, false if it fails.</returns>
    public static bool ContainsAll<T>(this IEnumerable<T> @this, params T[] values)
    {
        T[] list = @this.ToArray();
        foreach (T value in values)
        {
            if (!list.Contains(value))
            {
                return false;
            }
        }

        return true;
    }
    /// <summary>
    ///     An IEnumerable&lt;T&gt; extension method that query if '@this' contains any.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="values">A variable-length parameters list containing values.</param>
    /// <returns>true if it succeeds, false if it fails.</returns>
    public static bool ContainsAny<T>(this IEnumerable<T> @this, params T[] values)
    {
        T[] list = @this.ToArray();
        foreach (T value in values)
        {
            if (list.Contains(value))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///     An IEnumerable&lt;T&gt; extension method that query if 'collection' is empty.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The collection to act on.</param>
    /// <returns>true if empty, false if not.</returns>
    public static bool IsEmpty<T>(this IEnumerable<T> @this)
    {
        return !@this.Any();
    }
    /// <summary>
    ///     An IEnumerable&lt;T&gt; extension method that queries if a not is empty.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The collection to act on.</param>
    /// <returns>true if a not is t>, false if not.</returns>
    public static bool IsNotEmpty<T>(this IEnumerable<T> @this)
    {
        return @this.Any();
    }
    /// <summary>
    ///     An IEnumerable&lt;T&gt; extension method that queries if a not null or is empty.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The collection to act on.</param>
    /// <returns>true if a not null or is t>, false if not.</returns>
    public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> @this)
    {
        return @this != null && @this.Any();
    }
    /// <summary>
    ///     An IEnumerable&lt;T&gt; extension method that queries if a null or is empty.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The collection to act on.</param>
    /// <returns>true if a null or is t>, false if not.</returns>
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> @this)
    {
        return @this == null || !@this.Any();
    }
    #endregion
    #region ForEach
    /// <summary>
    /// 对集合中所有元素进行操作(仅读取)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> @this, Action<T> action)
    {
        foreach (T t in @this.ToArray())
        {
            action(t);
        }
        return @this;
    }
    /// <summary>
    /// 对集合中所有元素和元素索引进行操作(仅读取)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> @this, Action<T, int> action)
    {
        T[] temp = @this.ToArray();
        for (int i = 0; i < temp.Length; i++)
        {
            action(temp[i], i);
        }
        return temp;
    }
    /// <summary>
    /// 对集合中所有元素进行操作(读写)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> @this, Func<T, T> func)
    {
        T[] temp = @this.ToArray();
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] = func(temp[i]);
        }
        return temp;
    }
    /// <summary>
    /// 对集合中所有元素和元素索引进行操作(读写)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> @this, Func<T, int, T> func)
    {
        T[] temp = @this.ToArray();
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] = func(temp[i], i);
        }
        return temp;
    }
    #endregion

    #region StringJoin
    /// <summary>
    ///     Concatenates all the elements of a IEnumerable, using the specified separator between each element.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">An IEnumerable that contains the elements to concatenate.</param>
    /// <param name="separator">
    ///     The string to use as a separator. separator is included in the returned string only if
    ///     value has more than one element.
    /// </param>
    /// <returns>
    ///     A string that consists of the elements in value delimited by the separator string. If value is an empty array,
    ///     the method returns String.Empty.
    /// </returns>
    public static string Join<T>(this IEnumerable<T> @this, string separator)
    {
        return string.Join(separator, @this);
    }
    /// <summary>
    ///     Concatenates all the elements of a IEnumerable, using the specified separator between
    ///     each element.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="this">The @this to act on.</param>
    /// <param name="separator">
    ///     The string to use as a separator. separator is included in the
    ///     returned string only if value has more than one element.
    /// </param>
    /// <returns>
    ///     A string that consists of the elements in value delimited by the separator string. If
    ///     value is an empty array, the method returns String.Empty.
    /// </returns>
    public static string Join<T>(this IEnumerable<T> @this, char separator)
    {
        return string.Join(separator.ToString(), @this);
    }
    #endregion


    //新增方法
    #region 获取奇数或偶数索引的元素
    public static T[] GetOddIndexItem<T>(this IEnumerable<T> @this)
    {
        return @this.Where((x, i) => i % 2 == 1).ToArray();
    }
    public static T[] GetEvenIndexItem<T>(this IEnumerable<T> @this)
    {
        return @this.Where((x, i) => i % 2 == 0).ToArray();
    }
    #endregion
    #region 去除重复项:RemoveDuplates
    public static T[] RemoveDuplates<T>(this IEnumerable<T> list)
    {
        List<T> newList = new List<T>();
        foreach (var element in list)
        {
            newList.AddIfNotContains(element);
        }
        return newList.ToArray();
    }
    #endregion
    #region 移位:Shift
    public static T[] Shift<T>(this IEnumerable<T> @this, int n)
    {
        T[] array = new T[@this.Count()];
        T[] temp = @this.ToArray();
        for (int i = 0; i < temp.Length; i++)
        {
            array[i] = temp[(i + n + @this.Count()) % @this.Count()];
        }
        return array;
    }
    #endregion
    #region 数组元素交换:Swap
    public static IEnumerable<T> Swap<T>(this IEnumerable<T> @this, int index1, int index2, bool isSwap = true)
    {
        T[] tempArray = @this.ToArray();
        if (isSwap)
        {
            T temp = tempArray[index1];
            tempArray[index1] = tempArray[index2];
            tempArray[index2] = temp;
        }
        @this = (IEnumerable<T>)tempArray;
        return @this;
    }
    #endregion
    #region 集合:IsIntersected,IsExpected,DifArrays

    /// <summary>
    /// 判斷A和B是否有交集(A和B共同的元素)
    /// </summary>
    /// <param name="list1"></param>
    /// <param name="list2"></param>
    /// <returns></returns>
    public static bool IsIntersected<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
    {
        return list1.Intersect(list2).Count() > 0;
    }
    /// <summary>
    /// 判斷A和B是否有差集 (A有，B沒有的元素)
    /// </summary>
    /// <param name="list1"></param>
    /// <param name="list2"></param>
    /// <returns></returns>
    public static bool IsExpected<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
    {
        return list1.Except(list2).Count() > 0;
    }
    /// <summary>
    /// 取集合一和集合二不相同部分
    /// </summary>
    /// <param name="PartOne"></param>
    /// <param name="PartTwo"></param>
    /// <returns></returns>
    public static List<T> DifArrays<T>(this IEnumerable<T> list1, IEnumerable<T> list2)
    {
        List<T> result = new List<T>();
        bool havesame = false;
        foreach (T a in list2)
        {
            havesame = false;
            foreach (T b in list1)
            {
                if (a.Equals(b))
                {
                    havesame = true;
                    break;
                }
            }
            if (!havesame)
            {
                result.Add(a);
            }
        }
        foreach (T a in list1)
        {
            havesame = false;
            foreach (T b in list2)
            {
                if (a.Equals(b))
                {
                    havesame = true;
                    break;
                }
            }
            if (!havesame)
            {
                result.Add(a);
            }
        }
        return result;
    }

    #endregion

}