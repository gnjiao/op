using System.ToolKit;

namespace desaySV
{
    /// <summary>
    /// 所有未定义的公共变量
    /// </summary>
    public class Global
    {
        /// <summary>
        /// 运动轴动作中
        /// </summary>
        public static bool IsLocating;
        /// <summary>
        /// 机器人动作中
        /// </summary>
        public static bool RootIsLocating;

        public static string[] RootName = new string[15] { "待机位置","左前安全位置", "右前安全位置", "左上安全位置",
            "右上安全位置", "左拍照位置", "右拍照位置", "左组装位置", "右组装位置","左标定位置","右标定位置",
            "左X方向移动距离","左Y方向移动距离", "右X方向移动距离","右Y方向移动距离" };
        public static string[] YaxisName = new string[6] { "取屏位置","放屏位置" ,"备用", "备用", "备用", "备用" };

        public static string[] ZaxisName = new string[10] { "进出料位置", "左拍照位置", "右拍照位置", "左组装位置", 
            "右组装位置", "安全位置", "备用", "备用", "备用", "备用" };

        public static UserLevel userLevel = UserLevel.None;

        public static string ProductSn = "";

        public static double  Beattime ;
    }



}
