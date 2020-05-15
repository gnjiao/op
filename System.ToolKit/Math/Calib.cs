using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.ToolKit
{

    /// <summary>
    /// 坐标转换类--From 20200318 to aiwen
    /// </summary>
    public class Calib
    {

        /// <summary>
        /// 将四个相机坐标转换成统一坐标系
        /// </summary>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <param name="point3"></param>
        /// <param name="point4"></param>
        /// <param name="angle1"></param>
        /// <param name="angle2"></param>
        /// <param name="angle3"></param>
        /// <param name="angle4"></param>
        /// <returns></returns>
        public static Point<double> ConventToPos(Point<double>point1, Point<double> point2, Point<double> point3, Point<double> point4
            ,double angle1, double angle2, double angle3, double angle4)
        {
            Point<double> p1 = new Point<double>();
            p1.X = 0;
            p1.Y = 0;
            Point<double> Pos = RotationOffsetCal(p1, point1, angle1);
            Point<double> Pos1 = RotationOffsetCal(p1, point2, angle2);
            Point<double> Pos2 = RotationOffsetCal(p1, point3, angle3);
            Point<double> Pos3 = RotationOffsetCal(p1, point4, angle4);
            Point<double> p2 = new Point<double>();
            p2.X = (Pos.X + Pos1.X + Pos2.X + Pos3.X) / 4;
            p2.Y = (Pos.Y + Pos1.Y + Pos2.Y + Pos3.Y) / 4;
            return p2;
        }


            /// <summary>
            /// 计算相机坐标与机械坐标的角度
            /// </summary>
            /// <param name="point">当前位置（机械）</param>
            /// <param name="point1">检测位置（相机）</param>
            /// <returns></returns>
            public static double RotationAngle(Point<double> point, Point<double> point1, out bool Result)
        {
            double air1 = 0;
            double air2 = 0;
            if (point.X * point.X + point.Y * point.Y == point1.X * point1.X + point1.Y * point1.Y)
            {
                air1 = Angle(point.X, point.Y);
                air2 = Angle(point1.X, point1.Y);
                Result = true;
            }
            else
            {
                air1 = Angle(point.X, point.Y);
                air2 = Angle(point1.X, point1.Y);
                Result = false;
            }
            if ((air1 - air2) > 180) { return (air1 - air2) - 360; }
            if ((air1 - air2) > 360 || (air1 - air2) < -360) { Result = false; }
            return air1 - air2;

        }

        public static double Angle(double x, double y)
        {
            double R = 0;
            if (x < 0 && y > 0) R = (180 + 180 / (Math.PI / Math.Atan(y / x)));
            if (x < 0 && y < 0) R = (180 - 180 / (Math.PI / Math.Atan(y / x))) * -1;
            if (y == 0 && x < 0) R = 180;
            if (x >= 0) R = 180 / (Math.PI / Math.Atan(y / x));
            return R;
        }

        /// <summary>
        /// 旋转后的偏差值计算
        /// </summary>
        /// <param name="rpoint">旋转中心偏差坐标</param>        
        /// <param name="lpoint">0°镜片中心偏差坐标</param>
        /// <param name="angle">旋转角度（单位°)</param>
        /// <returns>旋转后的镜片与吸笔偏差值</returns>
        public static Point<double> RotationOffsetCal(Point<double> rpoint,
            Point<double> lpoint, double angle)
        {
            var point = new Point<double>();
            var lrx = lpoint.X - rpoint.X;
            var lry = lpoint.Y - rpoint.Y;
            var lrx1 = Math.Abs(lrx);
            var lry1 = Math.Abs(lry);
            var R = Math.Sqrt(lrx1 * lrx1 + lry1 * lry1);
            double angle1 = 0;
            if (lrx == 0) { R = lry1; }
            if (lry == 0) { R = lrx1; }
            if (lry < 0) { angle1 = 180 / (Math.PI / Math.Acos(lrx / R)) * -1; }
            if (lry > 0) { angle1 = 180 / (Math.PI / Math.Acos(lrx / R)); }
            var angle2 = angle1 + angle;
            if (angle2 >= 360) { angle2 = angle2 - 360; }
            if (angle2 <= 360) { angle2 = angle2 + 360; }
            var X = Math.Cos(Math.PI / (180 / angle2)) * R;
            point.X = Math.Cos(Math.PI / (180 / angle2)) * R + rpoint.X;
            point.Y = Math.Sin(Math.PI / (180 / angle2)) * R + rpoint.Y;
            return point;
        }


    }
  
}
