using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Motion.Tray
{
    public partial class uButton : UserControl
    {
        #region Private Member

        private Color color_lantern_background = Color.LimeGreen;                   // 按钮的背景颜色，包括边线颜色
        private Brush brush_lantern_background = null;                              // 按钮的背景画刷
        private Pen pen_lantern_background = null;                                  // 按钮的背景画笔
        private string _text = "";                                                  // 按钮的背景颜色，包括边线颜色
        private StringFormat centerFormat = null;                                   // 居中显示的格式化文本
        #endregion
        public uButton()
        {
            InitializeComponent();

            DoubleBuffered = true;
            brush_lantern_background = new SolidBrush(color_lantern_background);
            pen_lantern_background = new Pen(color_lantern_background, 2f);

            centerFormat = new StringFormat();
            centerFormat.Alignment = StringAlignment.Center;
            centerFormat.LineAlignment = StringAlignment.Center;
        }
        Point center;
        Rectangle rectangle_larger;
        Rectangle rectangle;
        Rectangle rect_text;
        private void useTray_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            center = GetCenterPoint();
            e.Graphics.TranslateTransform(center.X, center.Y);
            int radius = (center.X - 2);
            if (radius < 5) return;
            rectangle_larger = new Rectangle(-radius - 2, -radius - 2, 2 * radius + 4, 2 * radius + 4);
            rectangle = new Rectangle(-radius, -radius, 2 * radius, 2 * radius);
            rect_text = new Rectangle(-center.X, -center.X, 2 * center.X, 2 * center.X);
            e.Graphics.DrawEllipse(pen_lantern_background, rectangle_larger);
            e.Graphics.FillEllipse(brush_lantern_background, rectangle);
            e.Graphics.DrawString(_text, Font, Brushes.Black, rect_text, centerFormat);
        }
        #region Public Member
        /// <summary>
        /// 获取或设置开关按钮的背景色
        /// </summary>
        [Browsable(true)]
        [Description("获取或设置信号灯的背景色")]
        [Category("外观")]
        [DefaultValue(typeof(Color), "LimeGreen")]
        public Color LanternBackground
        {
            get
            {
                return color_lantern_background;
            }
            set
            {
                color_lantern_background = value;
                brush_lantern_background?.Dispose();
                pen_lantern_background?.Dispose();
                brush_lantern_background = new SolidBrush(color_lantern_background);
                pen_lantern_background = new Pen(color_lantern_background, 2f);
                Invalidate();
            }
        }
        /// <summary>
        /// 获取或设置开关按钮的文本
        /// </summary>
        [Browsable(true)]
        [Description("获取或设置开关按钮的文本")]
        [Category("外观")]
        [DefaultValue(typeof(string), "888")]
#pragma warning disable CS0114 // '“uButton.Text”隐藏继承的成员“UserControl.Text”。若要使当前成员重写该实现，请添加关键字 override。否则，添加关键字 new。
        public string Text
#pragma warning restore CS0114 // '“uButton.Text”隐藏继承的成员“UserControl.Text”。若要使当前成员重写该实现，请添加关键字 override。否则，添加关键字 new。
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                Invalidate();
            }
        }
        #endregion
        #region Private Method
        private Point GetCenterPoint()
        {
            if (Height > Width) Width = Height;
            if (Height < Width) Height = Width;
            return new Point((Width - 1) / 2, (Height - 1) / 2);
        }
        #endregion
        private void useTray_MouseEnter(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            g.Clear(BackColor);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.TranslateTransform(center.X, center.Y);
            g.DrawEllipse(new Pen(Color.White, 2f), rectangle_larger);
            g.FillEllipse(brush_lantern_background, rectangle);
            g.DrawString(_text, Font, Brushes.Black, rect_text, centerFormat);
            g.Dispose();
        }
        private void useTray_MouseLeave(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            g.Clear(BackColor);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.TranslateTransform(center.X, center.Y);
            g.DrawEllipse(pen_lantern_background, rectangle_larger);
            g.FillEllipse(brush_lantern_background, rectangle);
            g.DrawString(_text, Font, Brushes.Black, rect_text, centerFormat);
            g.Dispose();
        }
    }
}
