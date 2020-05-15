using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Drawing;

namespace System.ToolKit
{
    public static partial class Extensions
    {
        #region 多个点的加减、平均:Add,Subtract,Average
        /// <summary>
        /// 获取多个点相加的位置
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static Point Add(this Point @this, params Point[] points)
        {
            return new Point(points.Sum(p => p.X)+@this.X, points.Sum(p => p.Y)+@this.Y);
        }
        /// <summary>
        /// 获取多个点相减的位置
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static Point Subtract(this Point @this,params Point[] points)
        {
            int px = @this.X;
            int py = @this.Y;
            for (int i = 0; i < points.Length; i++)
            {
                px -= points[i].X;
                py -= points[i].Y;
            }
            return new Point(px, py);
        }
        /// <summary>
        /// 获取多个点的平均位置
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static PointF Average(this Point @this,params Point[] points)
        {
            Point[] newPoints = new Point[points.Length+1] ;
            for (int i = 0; i < points.Length; i++)
            {
                newPoints[i] = points[i];
            }
            newPoints[points.Length] = @this;
            return new PointF(newPoints.Average(p => p.X).ToFloat(), newPoints.Average(p => p.Y).ToFloat());
        }
        #endregion
        #region 多个点的加减、平均 PointF:Add,Subtract,Average
        /// <summary>
        /// 获取多个点相加的位置
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static PointF Add(this PointF @this, params PointF[] points)
        {
            return new PointF(points.Sum(p => p.X) + @this.X, points.Sum(p => p.Y) + @this.Y);
        }
        /// <summary>
        /// 获取多个点相减的位置
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static PointF Subtract(this PointF @this, params PointF[] points)
        {
            float px = @this.X;
            float py = @this.Y;
            for (int i = 0; i < points.Length; i++)
            {
                px -= points[i].X;
                py -= points[i].Y;
            }
            return new PointF(px, py);
        }
        /// <summary>
        /// 获取多个点的平均位置
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static PointF Average(this PointF @this, params PointF[] points)
        {
            PointF[] newPoints = new PointF[points.Length + 1];
            for (int i = 0; i < points.Length; i++)
            {
                newPoints[i] = points[i];
            }
            newPoints[points.Length] = @this;
            return new PointF(newPoints.Average(p => p.X), newPoints.Average(p => p.Y));
        }
        #endregion
        #region 点与数值的加减乘除:Add,Subtract,Mul,Dev
        public static Point Add(this Point @this,int value)
        {
            return new Point(@this.X+value,@this.Y+value);
        }
        public static Point Subtract(this Point @this,int value)
        {
            return new Point(@this.X-value,@this.Y-value);
        }
        public static Point Mul(this Point @this,int value)
        {
            return new Point(@this.X*value,@this.Y*value);
        }
        public static Point Dev(this Point @this,int value)
        {
            return new Point(@this.X/value,@this.Y/value);
        }

       public static PointF Add(this PointF @this,float value)
        {
            return new PointF(@this.X+value,@this.Y+value);
        }
        public static PointF Subtract(this PointF @this,float value)
        {
            return new PointF(@this.X-value,@this.Y-value);
        }
        public static PointF Mul(this PointF @this,float value)
        {
            return new PointF(@this.X*value,@this.Y*value);
        }
        public static PointF Dev(this PointF @this,float value)
        {
            return new PointF(@this.X/value,@this.Y/value);
        }
        #endregion

        #region PointF与Point的转换:ToPointF,ToPoint
        public static PointF ToPointF(this Point point)
        {
            return new PointF(point.X.ToFloat(), point.Y.ToFloat());
        }
        public static Point ToPoint(this PointF pointF)
        {
            return new Point(pointF.X.ToInt(), pointF.Y.ToInt());
        }
        #endregion
        #region between
        public static bool Between(this Point point, Point lowerBound, Point upperBound, bool includeLowerBound = false, bool includeUpperBound = false)
        {
            return point.X.Between(lowerBound.X, upperBound.X, includeLowerBound, includeUpperBound) && point.Y.Between(lowerBound.Y, upperBound.Y, includeLowerBound, includeUpperBound);
        }
        #endregion


    }
}
