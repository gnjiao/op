
namespace desaySV
{
    /// <summary>
    /// 故障代码索引
    /// </summary>
    public static class AlarmKeys
    {
        public static string ESTOP = "1000";
        public static string DOOR = "1001";
        public static string DOOR1 = "1002";
        public static string DOOR2 = "1003";
        public static string PRESSURE = "1004";
        public static string ConnectAlarm1 = "1005";
        public static string RobotRCX340Alarm = "1006";
        public static string 未选择停机原因 = "0001";
        public static string Alarm_2 = "002";
        public static string Alarm_3 = "003";
        public static string Alarm_004 = "004";
        public static string Alarm_005 = "005";
        public static string Alarm_006 = "006";
        public static string Alarm_7 = "007";
        public static string Alarm_1 = "1";
        public static string Alarm_20 = "2";
        public static string Alarm_30 = "3";
        public static string Alarm_4 = "4";
        public static string Alarm_5 = "5";
        public static string Alarm_6 = "6";
        public static string Alarm_70 = "7";
        public static string Alarm_8 = "8";
        public static string Alarm_9 = "9";
        public static string Alarm_10 = "10";
        public static string Alarm_11 = "11";
        public static string Alarm_12 = "12";
        public static string Alarm_13 = "13";

    }

    public enum RootPositionName
    { 
        待料位置=0,
        左前安全位置= 1,
        右前安全位置 = 2,
        左上安全位置 = 3,
        右上安全位置 = 4,
        左上拍照位置 = 5,
        右上拍照位置 = 6,
        左上组装位置 = 7,
        右上组装位置 = 8,
        左相机标定位置 = 9,
        右相机标定位置 = 10,
        左备用位置 = 11,
        右备用位置 = 12,
        左备用位置1 = 13,
        右备用位置2 = 14
    }

    public enum SwitchCom
    {
        None = 0,
        MES = 1,
        Interlocking = 2
    }
    /// <summary>
    /// 轴参数
    /// </summary>
    public struct axisParam
    {
        public string Name;
        public double pos;      
    }
    /// <summary>
    /// 机器人位置
    /// </summary>
    public struct RootParam
    {
       
        public double X;
        public double Y;
        public double Z;
        public double R;
    }
    /// <summary>
    /// 相机与机器人的关系
    /// </summary>
    public struct PhotoOffice
    {
        public string Name;
        public double X;
        public double Y;       
        public double R;
    }

    /// <summary>
    /// 相机与MM转换
    /// </summary>
    public struct ConversionMM
    {      
        public double XX;
        public double YY;
    }

    /// <summary>
    /// XY值
    /// </summary>
    public struct PosXY
    {
        public double X;
        public double Y;

    }
    /// <summary>
    /// 相机参数
    /// </summary>
    public struct PosPhoto
    {
        /// <summary>
        /// 相机左前X轴
        /// </summary>
        public double LFXPhoto;
        /// <summary>
        /// 相机左前X轴
        /// </summary>
        public double LFYPhoto;
        /// <summary>
        /// 相机左后X轴
        /// </summary>
        public double LRXPhoto;
        /// <summary>
        /// 相机左后Y轴
        /// </summary>
        public double LRYPhoto;
        /// <summary>
        /// 相机右Y轴
        /// </summary>
        public double RYPhoto;
        /// <summary>
        /// 相机X轴
        /// </summary>
        public double XPhoto;
    }

    public struct AxisSpeed
    {
        /// <summary>
        /// 最大速度
        /// </summary>
        public double velocityMax;
        /// <summary>
        /// 速度比例
        /// </summary>
        public double velocityRate;
        /// <summary>
        /// 启动速度
        /// </summary>
        public int startSpeed;
        /// <summary>
        /// 运行速度
        /// </summary>
        public int RunSpeed;
        /// <summary>
        /// 加减速
        /// </summary>
        public int AddSpeed;
        /// <summary>
        /// 回原点启动速度
        /// </summary>
        public int HomestartSpeed;
        /// <summary>
        /// 回原点运行速度
        /// </summary>
        public int HomeRunSpeed;
        /// <summary>
        /// 回原点加减速
        /// </summary>
        public int HomeAddSpeed;


    }

}
