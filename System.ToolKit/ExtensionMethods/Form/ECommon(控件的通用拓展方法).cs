using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data;
using System.IO;

namespace System.ToolKit
{
    public static partial class Extensions
    {
        #region 取反:CheckReverse,EnabledReverse,VisibleReverse
        public static void CheckReverse(this CheckBox @this)
        {
            @this.Checked = !@this.Checked;
        }
        public static void CheckReverse(this ToolStripMenuItem @this)
        {
            @this.Checked = !@this.Checked;
        }
        public static void CheckReverse(this ToolStripButton @this)
        {
            @this.Checked = !@this.Checked;
        }

        public static void EnabledReverse(this Control @this)
        {
            @this.Enabled = !@this.Enabled;
        }
        public static void EnabledReverse(this ToolStripMenuItem @this)
        {
            @this.Enabled = !@this.Enabled;
        }
        public static void EnabledReverse(this ToolStripButton @this)
        {
            @this.Enabled = !@this.Enabled;
        }

        public static void VisibleReverse(this Control @this)
        {
            @this.Visible = !@this.Visible;
        }
        public static void VisibleReverse(this ToolStripMenuItem @this)
        {
            @this.Visible = !@this.Visible;
        }
        public static void VisibleReverse(this ToolStripButton @this)
        {
            @this.Visible = !@this.Visible;
        }
        #endregion

        #region ClearThenAddRange
        public static void ClearThenAddRange(this ListBox.ObjectCollection @this,string[] data)
        {
            @this.Clear();
            @this.AddRange(data);
        }
        public static void ClearThenAddRange(this ComboBox.ObjectCollection @this, string[] data)
        {
            @this.Clear();
            @this.AddRange(data);
        }
        #endregion

        #region DisableAndGray
        /// <summary>
        /// 禁用控件及控件字体变灰色,isDisable为false时启用控件及控件字体颜色变黑色
        /// </summary>
        /// <param name="this"></param>
        /// <param name="isDisable"></param>
        public static void DisableAndGray(this Button @this, bool isDisable = true)
        {
            @this.Enabled = !isDisable;
            @this.ForeColor = (!isDisable) ? Color.Black : Color.Gray;
        }
        /// <summary>
        /// 禁用控件及控件字体变灰色,isDisable为false时启用控件及控件字体颜色变黑色
        /// </summary>
        /// <param name="this"></param>
        /// <param name="isDisable"></param>
        public static void DisableAndGray(this ToolStripMenuItem @this, bool isDisable = true)
        {
            @this.Enabled = !isDisable;
            @this.ForeColor = (!isDisable) ? Color.Black : Color.Gray;
        }
        /// <summary>
        /// 禁用控件及控件字体变灰色,isDisable为false时启用控件及控件字体颜色变黑色
        /// </summary>
        /// <param name="this"></param>
        /// <param name="isDisable"></param>
        public static void DisableAndGray(this ToolStripButton @this, bool isDisable = true)
        {
            @this.Enabled = !isDisable;
            @this.ForeColor = (!isDisable) ? Color.Black : Color.Gray;
        }
        #endregion
    }
}
