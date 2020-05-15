using System.Diagnostics;

namespace desaySV
{
    /// <summary>
    /// 定义机台流程状态
    /// </summary>
    public class Marking
    {


        /// <summary>
        /// 读码器信息接收完成标志
        /// </summary>
        public static bool isCodeReaderCompleted;

        public static int PassNum;
        public static int FailNum;

        /// <summary>
        /// 运行时间
        /// </summary>
        public static string CycleRunTime;
        /// <summary>
        /// 运行时间
        /// </summary>
        public static string StartRunTime;
        /// <summary>
        /// 启动允许
        /// </summary>
        public static bool StartAllow;
        /// <summary>
        /// 旋转动作完成
        /// </summary>
        public static bool RotaryMotorFinish;
        /// <summary>
        /// 工站2完成
        /// </summary>
        public static bool station2Finish;
        /// <summary>
        /// 工站3完成
        /// </summary>
        public static bool station3Finish;
        /// <summary>
        /// 工站4完成
        /// </summary>
        public static bool station4Finish;
        /// <summary>
        /// 一模计时器
        /// </summary>
        public static Stopwatch watchModel = new Stopwatch();
        /// <summary>
        /// 一模生产周期时间
        /// </summary>
        public static double watchModelValue;
       
        /// <summary>
        /// 条码当前值
        /// </summary>
        public static string ScanComValue;
       
        /// <summary>
        /// 强制旋转
        /// </summary>
        public static bool SwitchProduct;
      

    }
}
