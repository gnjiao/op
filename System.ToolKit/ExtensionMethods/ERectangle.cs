using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Net;

namespace System.ToolKit
{
    public static partial class Extensions
    {
        #region 矩形9个点(左上/左中/坐下/...):LeftTop,LeftMiddle,LeftBottom,MiddleTop,Center,MiddleBottom,RightTop,RightMiddle,RightBottom
        public static Point LeftTop(this Rectangle rc)
        {
            return rc.Location;
        }
        public static Point LeftMiddle(this Rectangle rc)
        {
            return new Point(rc.Left,(rc.Top+rc.Bottom)/2);
        }
        public static Point LeftBottom(this Rectangle rc)
        {
            return new Point(rc.Left,rc.Bottom);
        }

        public static Point MiddleTop(this Rectangle rc)
        {
            return new Point((rc.Left+rc.Right)/2,rc.Top);
        }
        public static Point Center(this Rectangle rc)
        {
            return new Point((rc.Left+rc.Right)/2,(rc.Top+rc.Bottom)/2);
        }
        public static Point MiddleBottom(this Rectangle rc)
        {
            return new Point((rc.Left+rc.Right)/2,rc.Bottom);
        }

        public static Point RightTop(this Rectangle rc)
        {
            return new Point(rc.Right,rc.Top);
        }
        public static Point RightMiddle(this Rectangle rc)
        {
            return new Point(rc.Right,(rc.Top+rc.Bottom)/2);
        }
        public static Point RightBottom(this Rectangle rc)
        {
            return new Point(rc.Right,rc.Bottom);
        }
        #endregion
        #region 与RectangleF的转换:ToRectangle
        public static Rectangle ToRectangle(this RectangleF rc)
        {
            return new Rectangle(rc.Location.ToPoint(), new Size(rc.Size.Width.ToInt(), rc.Size.Height.ToInt()));
        }
        #endregion



    }
}
