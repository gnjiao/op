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
        public static string[] GetItemsArray(this ListBox lst)
        {
            string[] arr = new string[lst.Items.Count];
            for (int i = 0; i < lst.Items.Count; i++)
            {
                arr[i] = lst.Items[i].ToString();
            }
            return arr;
        }
        /// <summary>
        /// 选择上一个项目,默认不循环选择(即第1个项目的上一个项目不会跑到最后一个项目)
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="isLoop"></param>
        /// <returns></returns>
        public static void SelectPrev(this ListBox lst,bool isLoop)
        {
            if (lst.SelectedIndex == -1)
            {
                lst.SelectedIndex = 0;
            }
            if (isLoop)
            {
                lst.SelectedIndex = lst.SelectedIndex > 0 ? lst.SelectedIndex - 1 : lst.Items.Count - 1;
            }
            else
            {
                lst.SelectedIndex = lst.SelectedIndex > 0 ? lst.SelectedIndex - 1 : 0;
            }
        }
        /// <summary>
        /// 选择下一个项目,默认不循环选择
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="isLoop"></param>
        /// <returns></returns>
        public static void SelectNext(this ListBox lst, bool isLoop = false)
        {
            if (lst.SelectedIndex == -1)
            {
                lst.SelectedIndex = 0;
            }
            if (isLoop)
            {
                lst.SelectedIndex = lst.SelectedIndex < lst.Items.Count - 1 ? lst.SelectedIndex + 1 : 0;
            }
            else
            {
                lst.SelectedIndex = lst.SelectedIndex < lst.Items.Count - 1 ? lst.SelectedIndex + 1 : lst.Items.Count - 1;
            }
        }
        public static void RemoveSelectItems(this ListBox lst)
        {
            int count = lst.SelectedItems.Count;
            for (int i = 0; i < count; i++)
            {
                lst.Items.RemoveAt(lst.SelectedIndex);
            }
        }
    }
}
