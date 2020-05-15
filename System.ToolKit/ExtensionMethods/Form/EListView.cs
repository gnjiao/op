using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace System.ToolKit
{
    public static partial class Extensions
    {
        /// <summary>
        /// 获取选中行的指定列的内容,返回1D字符串数组
        /// </summary>
        /// <param name="lvw"></param>
        /// <param name="colIndex"></param>
        /// <returns></returns>
        public static string[] SelectedItemsColumnValues(this ListView lvw, int colIndex)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < lvw.SelectedItems.Count; i++)
            {
                list.Add(lvw.SelectedItems[i].SubItems[colIndex].Text);
            }
            return list.ToArray().PrintEx();
        }
        public static bool IsSelectAll(this ListView lvw)
        {
            for (int i = 0; i < lvw.Items.Count; i++)
            {
                if (lvw.Items[i].Selected == false)
                {
                    return false;
                }
            }
            return true;
        }
        public static void SelectAll(this ListView @this)
        {
            for (int i = 0; i < @this.Items.Count; i++)
            {
                @this.Items[i].Selected = true;
            }
        }
        public static void SelectNone(this ListView @this)
        {
            for (int i = 0; i < @this.Items.Count; i++)
            {
                @this.Items[i].Selected = false;
            }
        }
        public static void SelectReverse(this ListView @this)
        {
            for (int i = 0; i < @this.Items.Count; i++)
            {
                @this.Items[i].Selected = !@this.Items[i].Selected;
            }
        }

        public static string[] GetColumnItems(this ListView @this, int columnIndex)
        {
            string[] arr = new string[@this.Items.Count];
            for (int i = 0; i < @this.Items.Count; i++)
            {
                arr[i] = @this.Items[i].SubItems[columnIndex].Text;
            }
            return arr;
        }
        public static string ItemNames(this ListView @this, int itemIndex, int subIndex)
        {
            return @this.Items[itemIndex].SubItems[subIndex].Text;
        }
        public static void ItemNames(this ListView @this, int itemIndex, int subIndex, string newItemName)
        {
            @this.Items[itemIndex].SubItems[subIndex].Text = newItemName;
        }
        #region ListView与2D数组的转换
        public static void From2DArray(this ListView lsv, string[,] data, bool isClearBefore = false)
        {
            if (isClearBefore)
            {
                lsv.Items.Clear();
            }
            for (int i = 0; i < data.GetLength(0); i++)
            {
                ListViewItem item = new ListViewItem(data.IndexRow(i));
                lsv.Items.Add(item);
            }
        }
        public static string[,] To2DArray(this ListView lst)
        {
            string[,] arr = new string[lst.Items.Count, lst.Columns.Count];
            for (int i = 0; i < lst.Items.Count; i++)
            {
                for (int j = 0; j < lst.Columns.Count; j++)
                {
                    arr[i, j] = lst.Items[i].SubItems[j].Text;
                }
            }
            return arr;
        }
        #endregion
        #region ListView替换列的内容
        public static ListView ReplaceColumn<T>(this ListView lsv, T[] data, int Index)
        {
            for (int i = 0; i < data.Length; i++)
            {
                lsv.Items[i].SubItems[Index].Text=data[i].ToString();
            }
            return lsv;
        }
        /// <summary>
        /// 从指定行列索引位置开始，替换数组中的子数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lsv"></param>
        /// <param name="_replaceArray"></param>
        /// <param name="_rowIndex"></param>
        /// <param name="_columnIndex"></param>
        /// <returns></returns>
        public static ListView ReplaceArraySubset<T>(this ListView lsv, T[,] _replaceArray, int _rowIndex = 0, int _columnIndex = 0)
        {
            for (int i = 0; i < _replaceArray.GetLength(0); i++)
            {
                for (int j = 0; j < _replaceArray.GetLength(1); j++)
                {
                    lsv.Items[i+ _rowIndex].SubItems[j+_columnIndex].Text= _replaceArray[i, j].ToString();
                }
            }
            return lsv;
        }
        #endregion
        #region 点击排序方法
        /// <summary>
        /// /若原来是无序，则按升序排列；若果原来是升序，则按降序排列；若原来是降序，则按升序排列 
        /// </summary>
        /// <param name="lst"></param>
        public static void ColumnClickAndSort(this ListView lst)
        {
            if (lst.Sorting == SortOrder.None)
            {
                lst.Sorting = SortOrder.Ascending;
            }
            else if (lst.Sorting == SortOrder.Ascending)
            {
                lst.Sorting = SortOrder.Descending;
            }
            else
            {
                lst.Sorting = SortOrder.Ascending;
            }
        }
        #endregion
        #region 自动调整列宽
        public static ListView AutoAdjustColumnWidth(this ListView lsv)
        {
            //获取一共有多少列 然后每列内容自适应-2   另外标题自适应是-1
            for (int i = 0; i < lsv.Columns.Count; i++)
            {
                lsv.Columns[i].Width = -2;
            }
            return lsv;
        }
        public static ListView TextAlignCenter(this ListView lsv)
        {
            for (int i = 0; i < lsv.Columns.Count; i++)
            {
                lsv.Columns[i].TextAlign= HorizontalAlignment.Center;
            }
            return lsv;
        }
        #endregion
    }
}
