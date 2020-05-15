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
        public static string[] GetItemsArray(this ComboBox cbo)
        {
            string[] arr = new string[cbo.Items.Count];
            for (int i = 0; i < cbo.Items.Count; i++)
            {
                arr[i] = cbo.Items[i].ToString();
            }
            return arr;
        }
        /// <summary>
        /// 组合框绑定一个枚举类型,并设置当前选中项(注意:这个方法最好放在窗口的Form1_Shown中,不要放在Form1_Load中,因为有时无法正常显示下拉项)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="selectedIndex"></param>
        public static void Bangding<T>(this ComboBox @this, int selectedIndex)
        {
            string[] items = Enum.GetNames(typeof(T));
            @this.Items.Clear();
            @this.Items.AddRange(items);
            @this.SelectedIndex = selectedIndex;
        }
    }
}
