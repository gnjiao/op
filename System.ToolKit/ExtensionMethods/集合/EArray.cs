using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Reflection;
using System.Data;
using System.Collections.ObjectModel;

namespace System.ToolKit
{
    public static partial class Extensions
    {
        #region 使用范例:数组索引
        //int[,] data = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 },{ 10,11,12} };
        //    int[] row = data.IndexRow(1);
        //    int[] col = data.IndexColumn(1);
        //    int[,] rows = data.IndexRow(0, 2);
        //    int[,] cols = data.IndexColumn(0, 2);
        #endregion
        #region 数组创建
        /// <summary>
        /// 两个1D数组组合成一个2D数组(如果1D数组的长度不相等,则舍去多余的长度,按最短长度的数组来组合)(默认行组合,isTranspose=true时为列组合)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array1"></param>
        /// <param name="array2"></param>
        /// <param name="isTranspose"></param>
        /// <returns></returns>
        public static T[,] Build2DArray<T>(this T[] array1, T[] array2, bool isTranspose = false)
        {
            T[,] data;
            int num = Math.Min(array1.Length, array2.Length);
            if (isTranspose)
            {
                //列组合
                data = new T[num, 2];
                for (int i = 0; i < num; i++)
                {
                    data[i, 0] = array1[i];
                    data[i, 1] = array2[i];
                }
            }
            else
            {
                //行组合
                data = new T[2, num];
                for (int i = 0; i < num; i++)
                {
                    data[0, i] = array1[i];
                    data[1, i] = array2[i];
                }
            }

            return data;
        }
        /// <summary>
        /// 三个1D数组组合成一个2D数组(如果1D数组的长度不相等,则舍去多余的长度,按最短长度的数组来组合)(默认行组合,isTranspose=true时为列组合)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array1"></param>
        /// <param name="array2"></param>
        /// <param name="array3"></param>
        /// <param name="isTranspose"></param>
        /// <returns></returns>
        public static T[,] Build2DArray<T>(this T[] array1, T[] array2, T[] array3, bool isTranspose = false)
        {
            T[,] data;
            int num = array1.Length.Min(array2.Length, array3.Length);
            if (isTranspose)
            {
                //列组合
                data = new T[num, 3];
                for (int i = 0; i < num; i++)
                {
                    data[i, 0] = array1[i];
                    data[i, 1] = array2[i];
                    data[i, 2] = array3[i];
                }
            }
            else
            {
                //行组合
                data = new T[3, num];
                for (int i = 0; i < num; i++)
                {
                    data[0, i] = array1[i];
                    data[1, i] = array2[i];
                    data[2, i] = array3[i];
                }
            }

            return data;
        }
        #endregion

        #region 数组清除,数组清除全部:ClearAll,ClearAt,Clear
        public static void ClearAll(this Array @this)
        {
            Array.Clear(@this, 0, @this.Length);
        }
        public static void Clear(this Array array, Int32 index, Int32 length=1)
        {
            Array.Clear(array, index, length);
        }
        #endregion
        #region 数组复制
        #region 数组强制复制:ConstrainedCopy
        /// <summary>
        ///     Copies a range of elements from an  starting at the specified source index and pastes them to another
        ///     starting at the specified destination index.  Guarantees that all changes are undone if the copy does not
        ///     succeed completely.
        /// </summary>
        /// <param name="sourceArray">The  that contains the data to copy.</param>
        /// <param name="sourceIndex">A 32-bit integer that represents the index in the  at which copying begins.</param>
        /// <param name="destinationArray">The  that receives the data.</param>
        /// <param name="destinationIndex">A 32-bit integer that represents the index in the  at which storing begins.</param>
        /// <param name="length">A 32-bit integer that represents the number of elements to copy.</param>
        public static void ConstrainedCopy(this Array sourceArray, Int32 sourceIndex, Array destinationArray, Int32 destinationIndex, Int32 length)
        {
            Array.ConstrainedCopy(sourceArray, sourceIndex, destinationArray, destinationIndex, length);
        }
        #endregion
        #region 数组复制:Copy
        
        public static void Copy(this Array sourceArray, Array destinationArray, Int32 length)
        {
            Array.Copy(sourceArray, destinationArray, length);
        }
        public static void Copy(this Array sourceArray, Int32 sourceIndex, Array destinationArray, Int32 destinationIndex, Int32 length)
        {
            Array.Copy(sourceArray, sourceIndex, destinationArray, destinationIndex, length);
        }
       
        #endregion
        #region 数组块复制:BlockCopy
        /// <summary>
        ///     Copies a specified number of bytes from a source array starting at a particular offset to a destination array
        ///     starting at a particular offset.
        /// </summary>
        /// <param name="src">The source buffer.</param>
        /// <param name="srcOffset">The zero-based byte offset into .</param>
        /// <param name="dst">The destination buffer.</param>
        /// <param name="dstOffset">The zero-based byte offset into .</param>
        /// <param name="count">The number of bytes to copy.</param>
        public static void BlockCopy(this Array src, Int32 srcOffset, Array dst, Int32 dstOffset, Int32 count)
        {
            Buffer.BlockCopy(src, srcOffset, dst, dstOffset, count);
        }
        #endregion
        #region 数组复制:CopyArray
        /// <summary>
        /// 复制1D数组(length为0时代表全部复制)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static T[] CopyArray<T>(this T[] arr, int length = 0)
        {
            T[] array = new T[arr.Length];
            Array.Copy(arr, array, arr.Length);
            return array;
        }
        public static T[,] CopyArray<T>(this T[,] arr)
        {
            T[,] array = new T[arr.GetLength(0), arr.GetLength(1)];
            Array.Copy(arr, array, arr.Length);
            return array;
        }
        #endregion
        #endregion

        #region 数组二进制搜索:BinarySearch
        public static Int32 BinarySearch(this Array array, System.Object value)
        {
            return Array.BinarySearch(array, value);
        }
        public static Int32 BinarySearch(this Array array, Int32 index, Int32 length, System.Object value)
        {
            return Array.BinarySearch(array, index, length, value);
        }
        public static Int32 BinarySearch(this Array array, System.Object value, IComparer comparer)
        {
            return Array.BinarySearch(array, value, comparer);
        }
        public static Int32 BinarySearch(this Array array, Int32 index, Int32 length, System.Object value, IComparer comparer)
        {
            return Array.BinarySearch(array, index, length, value, comparer);
        }
        #endregion
        #region 数组搜索:IndexOf,LastIndexOf  IndicesOf(批量搜索)
        public static Int32 IndexOf(this Array array, Object value, Int32 startIndex=0)
        {
            return Array.IndexOf(array, value, startIndex);
        }
        public static Int32 IndexOf(this Array array, Object value, Int32 startIndex, Int32 count)
        {
            return Array.IndexOf(array, value, startIndex, count);
        }
        public static Int32 LastIndexOf(this Array array, Object value, Int32 startIndex=0)
        {
            return Array.LastIndexOf(array, value, startIndex);
        }
        public static Int32 LastIndexOf(this Array array, Object value, Int32 startIndex, Int32 count)
        {
            return Array.LastIndexOf(array, value, startIndex, count);
        }

        public static List<int> IndicesOf<T>(this T[] arr, T value)
        {
            List<int> list = new List<int>();
            int startIndex = 0;
            while (arr.IndexOf(value, startIndex) != -1)
            {
                startIndex = arr.IndexOf(value, startIndex);
                list.Add(startIndex++);//注意:运行后startIndex才加1,这样可以省略下一行代码
                //startIndex++;
            }
            return list;
        }
        public static List<int[]> IndicesOf<T>(this T[,] arr, T value)
        {
            List<int[]> list = new List<int[]>();
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j].Equals(value))
                    {
                        list.Add(new int[] { i, j });
                    }
                }
            }
            return list;
        }
        #endregion
        #region 替换数组元素:Replace,ReplaceAll
        public static T[] Replace<T>(this T[] arr, int index, T newValue)
        {
            T[] array = arr.CopyArray();
            array[index] = newValue;
            return array;
        }
        public static T[,] Replace<T>(this T[,] arr, int rowIndex, int colIndex, T newValue)
        {
            T[,] array = arr.CopyArray();
            array[rowIndex, colIndex] = newValue;
            return array;
        }

        public static T[] ReplaceAll<T>(this T[] arr, T value, T newValue)
        {
            T[] array = arr.CopyArray();
            foreach (var item in arr.IndicesOf(value))
            {
                array[item] = newValue;
            }
            return array;
        }
        public static T[,] ReplaceAll<T>(this T[,] arr, T value, T newValue)
        {
            T[,] array = arr.CopyArray();
            foreach (var item in arr.IndicesOf(value))
            {
                array[item[0], item[1]] = newValue;
            }
            return array;
        }
        #endregion
        #region 数组的排序与反转:Sort,SortForLink,Reverse,ReverseForLink
        public static void Sort(this Array array)
        {
            Array.Sort(array);
        }
        public static void Sort(this Array array, Array items)
        {
            Array.Sort(array, items);
        }
        public static void Sort(this Array array, Int32 index, Int32 length)
        {
            Array.Sort(array, index, length);
        }
        public static void Sort(this Array array, Array items, Int32 index, Int32 length)
        {
            Array.Sort(array, items, index, length);
        }
        public static void Sort(this Array array, IComparer comparer)
        {
            Array.Sort(array, comparer);
        }
        public static void Sort(this Array array, Array items, IComparer comparer)
        {
            Array.Sort(array, items, comparer);
        }
        public static void Sort(this Array array, Int32 index, Int32 length, IComparer comparer)
        {
            Array.Sort(array, index, length, comparer);
        }
        public static void Sort(this Array array, Array items, Int32 index, Int32 length, IComparer comparer)
        {
            Array.Sort(array, items, index, length, comparer);
        }
        public static T[] SortForLink<T>(this T[] array)
        {
            Array.Sort(array);
            return array;
        }

        public static void Reverse(this Array array)
        {
            Array.Reverse(array);
        }
        public static void Reverse(this Array array, Int32 index, Int32 length)
        {
            Array.Reverse(array, index, length);
        }
        public static T[] ReverseForLink<T>(this T[] array)
        {
            Array.Reverse(array);
            return array;
        }
        #endregion
        #region 数组索引:数组是否含有指定索引,行索引,列索引
        public static bool WithinIndex(this Array @this, int index)
        {
            return index >= 0 && index < @this.Length;
        }
        /// <summary>
        ///     An Array extension method that check if the array is lower then the specified index.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="index">Zero-based index of the.</param>
        /// <param name="dimension">(Optional) the dimension.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public static bool WithinIndex(this Array @this, int index, int dimension = 0)
        {
            return index >= @this.GetLowerBound(dimension) && index <= @this.GetUpperBound(dimension);
        }
        /// <summary>
        /// 索引1D数组指定长度,输出1D数组子集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="index"></param>
        /// <param name="length">长度为0时输入剩余数组元素</param>
        /// <returns></returns>
        public static T[] SubArray<T>(this T[] @this, int index, int length = 0)
        {
            if (length == 0)
            {
                return @this.ToList().Skip(index).ToArray();
            }
            else
            {
                return @this.ToList().Skip(index).Take(length).ToArray();
            }
        }

        /// <summary>
        /// 索引1D数组,输出数组元素,index支持负数,例如-1则输出最后一个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T Index<T>(this T[] @this, int index)
        {
            index = index < 0 ? index + @this.Length : index;
            return @this[index];
        }
        /// <summary>
        /// 索引1D数组,支持多个索引值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static T[] Indices<T>(this T[] @this, params int[] indices)
        {
            T[] arr = new T[indices.Length];
            for (int i = 0; i < indices.Length; i++)
            {
                arr[i] = @this[indices[i]];
            }
            return arr;
        }

        /// <summary>
        /// 索引2D数组,输出2D数组子集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="rowIndex"></param>
        /// <param name="rowLength"></param>
        /// <param name="colIndex"></param>
        /// <param name="colLength"></param>
        /// <returns></returns>
        public static T[,] SubArray<T>(this T[,] @this, int rowIndex, int rowLength, int colIndex, int colLength)
        {
            T[,] arr = new T[rowLength, colLength];
            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    arr[i, j] = @this[rowIndex + i, colIndex + j];
                }
            }
            return arr;
        }
        /// <summary>
        /// 索引2D数组某行的内容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T[] IndexRow<T>(this T[,] @this, int index)
        {
            T[] arr = new T[@this.GetLength(1)];
            for (int i = 0; i < @this.GetLength(1); i++)
            {
                arr[i] = @this[index, i];
            }
            return arr;
        }
        /// <summary>
        /// 索引2D数组多行的内容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="indices"></param>
        /// <returns></returns>
        public static T[,] IndexRow<T>(this T[,] @this, params int[] indices)
        {
            T[,] arr = new T[@this.GetLength(1), indices.Length];
            for (int i = 0; i < @this.GetLength(1); i++)
            {
                for (int j = 0; j < indices.Length; j++)
                {
                    arr[i, j] = @this[indices[j], i];
                }
            }
            return arr;
        }
        /// <summary>
        /// 索引2D数组某列的内容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T[] IndexColumn<T>(this T[,] @this, int index)
        {
            T[] arr = new T[@this.GetLength(0)];
            for (int i = 0; i < @this.GetLength(0); i++)
            {
                arr[i] = @this[i, index];
            }
            return arr;
        }
        /// <summary>
        /// 索引2D数组多列的内容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="indexs"></param>
        /// <returns></returns>
        public static T[,] IndexColumn<T>(this T[,] @this, params int[] indexs)
        {
            T[,] arr = new T[@this.GetLength(0), indexs.Length];
            for (int i = 0; i < @this.GetLength(0); i++)
            {
                for (int j = 0; j < indexs.Length; j++)
                {
                    arr[i, j] = @this[i, indexs[j]];
                }
            }
            return arr;
        }
        #endregion
        #region 数组插入与删除:AddElement(尾部追加元素或数组),RemoveElement(移除元素),InsertElement  InsertRow  InsertColumn
        public static T[] AddElement<T>(this T[] @this, params T[] newElements)
        {
            List<T> list = @this.ToList();
            list.AddRange(newElements);
            return list.ToArray();
        }
        /// <summary>
        /// 从数组中移除一个或多个元素,索引支持负数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="index">索引支持负数,例如:-1则删除最后一个元素</param>
        /// <returns></returns>
        public static T[] RemoveElement<T>(this T[] @this, params int[] index)
        {
            List<T> list = @this.ToList();
            index.Sort();
            index.Reverse();
            for (int i = 0; i < index.Length; i++)
            {
                //如果索引是负数,则加上数组的长度
                if (index[i] < 0)
                {
                    index[i] += @this.Length;
                }
                list.RemoveAt(index[i]);
            }
            return list.ToArray();
        }
        /// <summary>
        /// 移除数组中指定的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static T[] RemoveElementContains<T>(this T[] @this, params T[] element)
        {
            return @this.Where(x=> element.Contains(x)).ToArray();
        }
        /// <summary>
        /// 移除数组中未指定的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static T[] RemoveElementNotContains<T>(this T[] @this, params T[] element)
        {
            return @this.Where(x => !element.Contains(x)).ToArray();
        }

        /// <summary>
        /// 数组插入元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_src"></param>
        /// <param name="_insertElement"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T[] InsertElement<T>(this T[] _src, T _insertElement, int index)
        {
            T[] _dst = new T[_src.Length + 1];
            Array.Copy(_src, 0, _dst, 0, index);
            Array.Copy(_src, index, _dst, index + 1, _src.Length - index);
            _dst[index] = _insertElement;
            return _dst;
        }
        public static T[,] InsertRow<T>(this T[,] _src, T[] _insertArray, int _rowIndex)
        {
            T[,] _dst = new T[_src.GetLength(0) + 1, _src.GetLength(1)];
            for (int i = 0; i < _src.GetLength(0) + 1; i++)
            {
                for (int j = 0; j < _src.GetLength(1); j++)
                {
                    if (i < _rowIndex)
                    {
                        _dst[i, j] = _src[i, j];
                    }
                    else if (i == _rowIndex)
                    {
                        _dst[_rowIndex, j] = _insertArray[j];
                    }
                    else
                    {
                        _dst[i, j] = _src[i - 1, j];
                    }
                }
            }
            return _dst;
        }
        public static T[,] InsertColumn<T>(this T[,] _src, T[] _insertArray, int _columnIndex)
        {
            T[,] _dst = new T[_src.GetLength(0), _src.GetLength(1) + 1];

            for (int j = 0; j < _columnIndex; j++)
            {
                for (int i = 0; i < _dst.GetLength(0); i++)
                {
                    _dst[i, j] = _src[i, j];
                }
            };

            for (int i = 0; i < _src.GetLength(0); i++)
            {
                _dst[i, _columnIndex] = _insertArray[i];
            }

            for (int j = _columnIndex + 1; j < _src.GetLength(1) + 1; j++)
            {
                for (int i = 0; i < _src.GetLength(0); i++)
                {
                    _dst[i, j] = _src[i, j - 1];
                }
            };
            return _dst;
        }
        #endregion
        #region 数组相等判断:AreEqual(集合判断),EqualItems(元素判断),获取两个1D数组相同位置相等的元素的索引:GetEqualIndices
        /// <summary>
        /// 比较两个一维数组中的元素是否依次相等
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool AreEqual<T>(this T[] a, T[] b)
        {
            if (a.Length != b.Length) return false;

            // compare elements
            for (int i = 0; i < a.Length; i++) { if (!a[i].Equals(b[i])) return false; }

            return true;
        }
        /// <summary>
        /// 判断两个数组的所有元素是否对应相等,返回布尔数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool[] EqualItems<T>(this T[] a, T[] b)
        {
            List<bool> list = new List<bool>();
            for (int i = 0; i < a.Length; i++)
            {
                list.Add(a[i].Equals(b[i]));
            }
            return list.ToArray();
        }
        /// <summary>
        /// 判断两个字符串的所有字符是否对应相等,返回布尔数组
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool[] EqualItems(this string a, string b)
        {
            return a.ToCharArray().EqualItems(b.ToCharArray());
        }

        /// <summary>
        /// 获取两个1D数组相同位置相等的元素的索引(两个数组的长度可以不相等)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr1"></param>
        /// <param name="arr2"></param>
        /// <returns></returns>
        public static List<int> GetEqualIndices<T>(this T[] arr1, T[] arr2)
        {
            List<int> list = new List<int>();
            int length = arr1.Length.Min(arr2.Length);
            for (int i = 0; i < length; i++)
            {
                if (arr1[i].Equals(arr2[i]))
                {
                    list.Add(i);
                }
            }
            return list;
        }
        /// <summary>
        /// 获取两个1D数组相同位置不相等的元素的索引(两个数组的长度可以不相等)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr1"></param>
        /// <param name="arr2"></param>
        /// <returns></returns>
        public static List<int> GetNotEqualIndices<T>(this T[] arr1, T[] arr2)
        {
            List<int> list = new List<int>();
            int length = arr1.Length.Min(arr2.Length);
            //比较两个数组相同长度部分的元素,将不相等的元素的索引加入列表
            for (int i = 0; i < length; i++)
            {
                if (!arr1[i].Equals(arr2[i]))
                {
                    list.Add(i);
                }
            }

            //将较长的数组的剩余部分元素的索引加入列表
            int last = arr1.Length.Max(arr2.Length) - arr1.Length.Min(arr2.Length);
            for (int i = 0; i < last; i++)
            {
                list.Add(i + length);
            }
            return list;
        }
        #endregion
        #region 数组替换行或列内容:ReplaceRow,ReplaceColumn
        public static T[,] ReplaceRow<T>(this T[,] _src, T[] _replaceArray, int _rowIndex)
        {
            for (int i = 0; i < _replaceArray.Length; i++)
            {
                _src[_rowIndex, i] = _replaceArray[i];
            }
            return _src;
        }
        public static T[,] ReplaceColumn<T>(this T[,] _src, T[] _replaceArray, int _columnIndex)
        {
            for (int i = 0; i < _replaceArray.Length; i++)
            {
                _src[i, _columnIndex] = _replaceArray[i];
            }
            return _src;
        }
        /// <summary>
        /// 从指定行列索引位置开始，替换数组中的子数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_src"></param>
        /// <param name="_replaceArray"></param>
        /// <param name="_rowIndex"></param>
        /// <param name="_columnIndex"></param>
        /// <returns></returns>
        public static T[,] ReplaceArraySubset<T>(this T[,] _src, T[,] _replaceArray, int _rowIndex = 0, int _columnIndex = 0)
        {
            for (int i = 0; i < _replaceArray.GetLength(0); i++)
            {
                for (int j = 0; j < _replaceArray.GetLength(1); j++)
                {
                    _src[i + _rowIndex, _columnIndex + j] = _replaceArray[i, j];
                }
            }
            return _src;
        }
        #endregion


        #region Exists
        /// <summary>
        ///     A T[] extension method that exists.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="array">The array to act on.</param>
        /// <param name="match">Specifies the match.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public static Boolean Exists<T>(this T[] array, Predicate<T> match)
        {
            return Array.Exists(array, match);
        }
        #endregion
        #region Find
        /// <summary>
        ///     A T[] extension method that searches for the first match.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="array">The array to act on.</param>
        /// <param name="match">Specifies the match.</param>
        /// <returns>A T.</returns>
        public static T Find<T>(this T[] array, Predicate<T> match)
        {
            return Array.Find(array, match);
        }
        #endregion
        #region FindAll
        /// <summary>
        ///     A T[] extension method that searches for the first all.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="array">The array to act on.</param>
        /// <param name="match">Specifies the match.</param>
        /// <returns>The found all.</returns>
        public static T[] FindAll<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindAll(array, match);
        }
        #endregion
        #region FindIndex
        /// <summary>
        ///     A T[] extension method that searches for the first index.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="array">The array to act on.</param>
        /// <param name="match">Specifies the match.</param>
        /// <returns>The found index.</returns>
        public static Int32 FindIndex<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindIndex(array, match);
        }

        /// <summary>
        ///     A T[] extension method that searches for the first index.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="array">The array to act on.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="match">Specifies the match.</param>
        /// <returns>The found index.</returns>
        public static Int32 FindIndex<T>(this T[] array, Int32 startIndex, Predicate<T> match)
        {
            return Array.FindIndex(array, startIndex, match);
        }

        /// <summary>
        ///     A T[] extension method that searches for the first index.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="array">The array to act on.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">Number of.</param>
        /// <param name="match">Specifies the match.</param>
        /// <returns>The found index.</returns>
        public static Int32 FindIndex<T>(this T[] array, Int32 startIndex, Int32 count, Predicate<T> match)
        {
            return Array.FindIndex(array, startIndex, count, match);
        }
        #endregion
        #region FindLast
        /// <summary>
        ///     A T[] extension method that searches for the first last.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="array">The array to act on.</param>
        /// <param name="match">Specifies the match.</param>
        /// <returns>The found last.</returns>
        public static T FindLast<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindLast(array, match);
        }
        #endregion
        #region FindLastIndex
        /// <summary>
        ///     A T[] extension method that searches for the last index.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="array">The array to act on.</param>
        /// <param name="match">Specifies the match.</param>
        /// <returns>The found index.</returns>
        public static Int32 FindLastIndex<T>(this T[] array, Predicate<T> match)
        {
            return Array.FindLastIndex(array, match);
        }

        /// <summary>
        ///     A T[] extension method that searches for the last index.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="array">The array to act on.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="match">Specifies the match.</param>
        /// <returns>The found index.</returns>
        public static Int32 FindLastIndex<T>(this T[] array, Int32 startIndex, Predicate<T> match)
        {
            return Array.FindLastIndex(array, startIndex, match);
        }

        /// <summary>
        ///     A T[] extension method that searches for the last index.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="array">The array to act on.</param>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">Number of.</param>
        /// <param name="match">Specifies the match.</param>
        /// <returns>The found index.</returns>
        public static Int32 FindLastIndex<T>(this T[] array, Int32 startIndex, Int32 count, Predicate<T> match)
        {
            return Array.FindLastIndex(array, startIndex, count, match);
        }
        #endregion
        #region TrueForAll
        /// <summary>
        ///     A T[] extension method that true for all.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="array">The array to act on.</param>
        /// <param name="match">Specifies the match.</param>
        /// <returns>true if it succeeds, false if it fails.</returns>
        public static Boolean TrueForAll<T>(this T[] array, Predicate<T> match)
        {
            return Array.TrueForAll(array, match);
        }
        #endregion



        #region 数组运算
        #region MaxSubstractMin
        //public static T MaxSubstractMin<T>(this T[] @this) where T : IComparable<T>
        //{
        //    Array.Sort(@this);
        //    dynamic v1 = @this[@this.Length - 1];
        //    dynamic v2 = @this[0];
        //    //return (T)(v1-v2);
        //    return (T)Convert.ChangeType(v1 - v2, typeof(T));
        //}
        #endregion
        #region 两个1D或2D数组相加,相减,相乘:Add,Subtract,Multiply
        public static int[] Add(this int[] a, int[] b, bool isAdd = true)
        {
            if (a.Length != b.Length)
                throw new ArgumentException("数组长度不匹配");
            return a.Zip(b, (x, y) => isAdd ? (x + y) : x).ToArray();
        }
        public static int[] Subtract(this int[] a, int[] b, bool isSubtract = true)
        {
            if (a.Length != b.Length)
                throw new ArgumentException("数组长度不匹配");
            return a.Zip(b, (x, y) => isSubtract ? (x - y) : x).ToArray();
        }
        public static int[] Multiply(this int[] a, int[] b, bool isMultiply = true)
        {
            if (a.Length != b.Length)
                throw new ArgumentException("数组长度不匹配");
            return a.Zip(b, (x, y) => isMultiply ? (x * y) : x).ToArray();
        }

        public static int[,] Add(this int[,] a, int[,] b, bool isAdd = true)
        {
            int[,] c = new int[a.GetLength(0), a.GetLength(1)];
            if (!isAdd) { return a; }
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    c[i, j] = a[i, j] + b[i, j];
                }
            }
            return c;
        }
        public static int[,] Subtract(this int[,] a, int[,] b, bool isSubtract = true)
        {
            int[,] c = new int[a.GetLength(0), a.GetLength(1)];
            if (!isSubtract) { return a; }
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    c[i, j] = a[i, j] - b[i, j];
                }
            }
            return c;
        }
        public static int[,] Multiply(this int[,] a, int[,] b, bool isMultiply = true)
        {
            int[,] c = new int[a.GetLength(0), a.GetLength(1)];
            if (!isMultiply) { return a; }
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    c[i, j] = a[i, j] * b[i, j];
                }
            }
            return c;
        }
        #endregion
        #region 1D或2D数组与数值相加,相减,相乘:Add,Subtract,Multiply
        public static int[] Add(this int[] a, int b, bool isAdd = true)
        {
            return a.Select(x => isAdd ? (x + b) : b).ToArray();
        }
        public static int[] Subtract(this int[] a, int b, bool isSubtract = true)
        {
            return a.Select(x => isSubtract ? (x - b) : b).ToArray();
        }
        public static int[] Multiply(this int[] a, int b, bool isMultiply = true)
        {
            return a.Select(x => isMultiply ? (x * b) : b).ToArray();
        }

        public static int[,] Add(this int[,] a, int b, bool isAdd = true)
        {
            return a.To1DArray().Add(b, isAdd).To2DArray(a.GetLength(0), a.GetLength(1));
        }
        public static int[,] Subtract(this int[,] a, int b, bool isSubtract = true)
        {
            return a.To1DArray().Add(b, isSubtract).To2DArray(a.GetLength(0), a.GetLength(1));
        }
        public static int[,] Multiply(this int[,] a, int b, bool isMultiply = true)
        {
            return a.To1DArray().Add(b, isMultiply).To2DArray(a.GetLength(0), a.GetLength(1));
        }
        #endregion
        #region 数组均方根值(RMS)
        /// <summary>
        /// 数组均方根值(RMS)
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static double RMS(this double[] a)
        {
            double sum = 0;
            for (int i = 0; i < a.Length; i++) { sum += a[i] * a[i]; }
            return Math.Sqrt(sum / a.Length);
        }
        #endregion
        #region 数组求绝对值:Abs
        public static double[] Abs(this double[] a)
        {
            double[] b = new double[a.Length];
            for (int i = 0; i < a.Length; i++) { b[i] = Math.Abs(a[i]); }
            return b;
        }
        #endregion
        #endregion
        #region 字符串数组:AddOrRemoveString
        /// <summary>
        /// 字符串数组增删前后缀,先删(如不包含则不删)再增
        /// </summary>
        /// <param name="this"></param>
        /// <param name="addStart"></param>
        /// <param name="addEnd"></param>
        /// <param name="removeStart"></param>
        /// <param name="removeEnd"></param>
        /// <returns></returns>
        public static string[] AddOrRemoveString(this string[] @this, string addStart, string addEnd = "", string removeStart = "", string removeEnd = "")
        {
            string[] newArr = new string[@this.Length];
            for (int i = 0; i < @this.Length; i++)
            {
                if (removeStart != "" && @this[i].Contains(removeStart))
                {
                    @this[i] = @this[i].Right(removeStart);
                }
                if (removeEnd != "" && @this[i].Contains(removeEnd))
                {
                    @this[i] = @this[i].LastLeft(removeEnd);
                }
                newArr[i] = addStart + @this[i] + addEnd;
            }
            return newArr;
        }

        public static string[] FindMatches(this string[] @this,string matchStr)
        {
            return @this.Where(x => x.IsMatch(matchStr)).ToArray();
        }
        public static int[] FindMatchIndices(this string[] @this, string matchStr)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < @this.Length; i++)
            {
                if (@this[i].IsMatch(matchStr))
                {
                    list.Add(i);
                }
            }
            return list.ToArray();
        }
        #endregion

        #region 数组转换:翻转/移位,类型转换,1D/2D转换
        #region 2D数组镜像/转置/旋转:MirrorVertically,Horizontal,Transpose,Rotate90,Rotate180,Rotate270
        /// <summary>
        /// 2D数组垂直镜像
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static T[,] MirrorVertically<T>(this T[,] arr)
        {
            T[,] array = new T[arr.GetLength(0), arr.GetLength(0)];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = arr[array.GetLength(1) - 1 - i, j];
                }
            }
            return array;
        }
        /// <summary>
        /// 2D数组水平镜像
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static T[,] Horizontal<T>(this T[,] arr)
        {
            T[,] array = new T[arr.GetLength(0), arr.GetLength(0)];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = arr[i, array.GetLength(1) - 1 - j];
                }
            }
            return array;
        }
        /// <summary>
        /// 2D数组转置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static T[,] Transpose<T>(this T[,] arr)
        {
            T[,] array = new T[arr.GetLength(1), arr.GetLength(0)];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[j, i] = arr[i, j];
                }
            }
            return array;
        }
        /// <summary>
        /// 2D数组顺时针旋转90度
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static T[,] Rotate90<T>(this T[,] arr)
        {
            return arr.MirrorVertically().Transpose();
        }
        /// <summary>
        /// 2D数组顺时针旋转180度
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static T[,] Rotate180<T>(this T[,] arr)
        {
            return arr.Rotate90().Rotate90();
        }
        /// <summary>
        /// 2D数组顺时针旋转270度
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static T[,] Rotate270<T>(this T[,] arr)
        {
            return arr.Transpose().MirrorVertically();
        }
        #endregion
        #region 1D 2D数组按行或列移位:Shift  ShiftRows  ShiftColumns
        /// <summary>
        /// 1D数组移位(负数向左移动,整数向右移动),举例:arr.Shift(-1)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static T[] Shift<T>(this T[] arr, int n)
        {
            T[] array = new T[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                array[i] = arr[(i + n + arr.Length) % arr.Length];
            }
            return array;
        }
        public static T[,] ShiftRows<T>(this T[,] arr, int n)
        {
            T[,] array = new T[arr.GetLength(0), arr.GetLength(1)];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = arr[(i - n + array.GetLength(0)) % array.GetLength(0), j];
                }
            }
            return array;
        }
        public static T[,] ShiftColumns<T>(this T[,] arr, int n)
        {
            T[,] array = new T[arr.GetLength(0), arr.GetLength(1)];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = arr[i, (j - n + array.GetLength(1)) % array.GetLength(1)];
                }
            }
            return array;
        }
        #endregion

        #region 转换为字符串数组:ToStringArray
        /// <summary>
        /// 转换1D数组为1D字符串数组(默认无format,可以填写"f2"等格式保留2位小数)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_src"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string[] ToStringArray<T>(this T[] _src, string format = "")
        {
            string[] _dst = new string[_src.Length];
            for (int i = 0; i < _src.Length; i++)
            {
                if (format != "")
                {
                    //先转换为double,再填写字符串格式

                    //保留2位小数，.tostring("f2")

                    //0 代表占位的 如果ToString("0.00") 这样就是保留两位小数，无论小数有多少位或者无小数，结果都是两位小数 例如  1.1234 那么结果是1.12 如果是1.2 那么结果会补零 为1.20
                    //# 代表后面的不是零就被保留，如果是0就去掉 例如ToString("0.##")
                    //如果两位小数就保留两个数字,大于两位的非零数字也要保留的话,可以下面这样写
                    //double dd = 1.2530;
                    //string ret = dd.ToString("0.00####");
                    _dst[i] = Convert.ToDouble(_src[i]).ToString(format);
                }
                else
                {
                    _dst[i] = _src[i].ToString();
                }
            }
            return _dst;
        }
        /// <summary>
        /// 转换2D数组为2D字符串数组(默认无format,可以填写"f2"等格式保留2位小数)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_src"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string[,] ToStringArray<T>(this T[,] _src, string format = "")
        {
            string[,] _dst = new string[_src.GetLength(0), _src.GetLength(1)];
            for (int i = 0; i < _src.GetLength(0); i++)
            {
                for (int j = 0; j < _src.GetLength(1); j++)
                {
                    if (format != "")
                    {
                        _dst[i, j] = Convert.ToDouble(_src[i, j]).ToString(format);
                    }
                    else
                    {
                        _dst[i, j] = _src[i, j].ToString();
                    }

                }
            }
            return _dst;
        }
        #endregion

        #region 1D/2D数组相互转换:To1DArray,To2DArray
        public static T[] To1DArray<T>(this T[,] sourceArray)
        {
            int row = sourceArray.GetLength(0);
            int col = sourceArray.GetLength(1);
            T[] destinationArray = new T[row * col];
            for (int i = 0; i < sourceArray.Length; i++)
                destinationArray[i] = sourceArray[i / col, i % col];
            return destinationArray;
        }
        public static T[,] To2DArray<T>(this T[] sourceArray, int row, int col)
        {
            T[,] destinationArray = new T[row, col];
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    destinationArray[i, j] = sourceArray[i * col + j];
                }
            }
            return destinationArray;
        }
        public static T[,] To2DArray<T>(this T[][] sourceArray)
        {
            T[,] destinationArray = new T[sourceArray.GetLength(0), sourceArray[0].Length];
            for (int i = 0; i < sourceArray.GetLength(0); i++)
            {
                for (int j = 0; j < sourceArray[0].Length; j++)
                {
                    destinationArray[i, j] = sourceArray[i][j];
                }
            }
            return destinationArray;
        }
        #endregion
        #endregion
        #region List和数组的转换:ToList,To2DArray
        public static List<T> ToList<T>(this T[] @this)
        {
            List<T> list = new List<T> { };
            list.AddRange(@this);
            return list;
        }
        public static List<T[]> ToList<T>(this T[,] @this)
        {
            List<T[]> list = new List<T[]> { };
            for (int i = 0; i < @this.GetLength(0); i++)
            {
                List<T> lstRow = new List<T>();
                for (int j = 0; j < @this.GetLength(1); j++)
                {
                    lstRow.Add(@this[i, j]);
                }
                list.Add(lstRow.ToArray());
            }
            return list;
        }
        public static T[,] To2DArray<T>(this List<T[]> @this)
        {
            int col = (@this.Count == 0) ? 0 : @this[0].Length;
            T[,] arr = new T[@this.Count, col];
            for (int i = 0; i < @this.Count; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    arr[i, j] = @this[i][j];
                }
            }
            return arr;
        }
        #endregion

    }
}
