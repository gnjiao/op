using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.ToolKit
{
    public static partial class Extensions1
    {
        #region 设置窗体位置在桌面右下角 （类似桌面右下角广告弹窗的效果）
        /// <summary>
        /// 设置窗体位置在桌面右下角 （类似桌面右下角广告弹窗的效果）
        /// </summary>
        /// <param name="frm"></param>
        public static void SetLocationAdPopup(this Form frm)
        {
            //设置窗体位置在桌面右下角 （类似桌面右下角广告弹窗的效果）
            frm.Location = new Point(Screen.PrimaryScreen.Bounds.Width - frm.Width, Screen.PrimaryScreen.Bounds.Height - frm.Height - 25);
        }
        #endregion
        #region 窗体全屏显示:ToFullScreen
        /// <summary>
        ///     A Form extension method that set the window form to full screen mode to the specified screen.
        /// </summary>
        /// <param name="form">The form to act on.</param>
        /// <param name="screen">(Optional) the screen to act on.</param>
        public static void ToFullScreen(this Form form, int screen = 0)
        {
            form.WindowState = FormWindowState.Maximized;
            form.StartPosition = FormStartPosition.Manual;
            form.Bounds = Screen.AllScreens[screen].Bounds;
        }
        #endregion
        #region 窗体最大化不挡住任务栏
        /// <summary>
        /// 窗体最大化不挡住任务栏
        /// </summary>
        /// <param name="form"></param>
        public static void Maximized(this Form form)
        {
            form.FormBorderStyle = FormBorderStyle.Sizable;
            form.Top = 0;
            form.Left = 0;
            form.Width = Screen.PrimaryScreen.WorkingArea.Width;
            form.Height = Screen.PrimaryScreen.WorkingArea.Height;
            form.WindowState = FormWindowState.Maximized;
        }
        #endregion
        #region 图像双缓冲技术:http://blog.csdn.net/fujie724/article/details/5767064
        //        以前做用户控件的时候喜欢拿已有的基础控件来拼。
        //发现这样做用户控件比较方便。
        //但是在控件投入大量使用之后，发现这种做法对控件的速度影响非常大。
        //如果一个控件是由1个Label，一个TextBox复合而成的。
        //那么创建一个这样的控件就相当于要生成2个控件。在设计界面和程序启动的时候速度明显感觉到变慢了。

        //于是全部重新修改，尽量把能不用控件的地方全部改成绘制。
        //比如Label用画出来的文字去替代。
        //比如一个日历控件上的31天的日期，以前用的Label，或者Button。
        //现在全部换成画出来的。创建速度从20毫秒加快到了0毫秒。


        //随之而来带来的一个问题就是。绘画的操作太多。导致界面闪烁比较严重。
        //问了一下公司的前辈，得到了下面的一个方法。使用之后确实完全没有闪烁了。。十分吃惊。赶紧记下。
        //public static void DoubleBuffer(this Form form)
        //{
        //    SetStyle(
        //             ControlStyles.OptimizedDoubleBuffer
        //             | ControlStyles.ResizeRedraw
        //             | ControlStyles.Selectable
        //             | ControlStyles.AllPaintingInWmPaint
        //             | ControlStyles.UserPaint
        //             | ControlStyles.SupportsTransparentBackColor,
        //             true);
        //}
        //        将这段代码加到用户控件的构造函数中即可生效。
        //这段代码的主要功能是开启了双缓冲。
        //平时我以为开双缓冲只需要设置ControlStyles.OptimizedDoubleBuffer为true而已。
        //但是经过实践才发现，起关键作用的是OptimizedDoubleBuffer和AllPaintingInWmPaint两个。
        //当这两个都为true的时候。闪烁几乎消失。效果非常好。
        #endregion
        #region 获取当前拥有焦点的控件
        ////API声明：获取当前焦点控件句柄      
        //[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        //internal static extern IntPtr GetFocus();

        /////获取 当前拥有焦点的控件
        //private static Control GetFocusedControl()
        //{
        //    Control focusedControl = null;
        //    // To get hold of the focused control:
        //    IntPtr focusedHandle = GetFocus();
        //    if (focusedHandle != IntPtr.Zero)
        //        //focusedControl = Control.FromHandle(focusedHandle);
        //        focusedControl = Control.FromChildHandle(focusedHandle);
        //    return focusedControl;
        //}
        #endregion
    }
}

