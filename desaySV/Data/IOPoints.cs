
using CMotion.Interfaces.IO;
using AxisControlls;

namespace desaySV
{
    /// <summary>
    ///     设备 IO 项
    /// </summary>
    public class IoPoints
    {
        //public static DaskAllController DaskController = new DaskAllController(CardDaskName.凌华科技7432);
        //public static AxisController ApsController = new AxisController(CardAxisName.凌华科技AMP_204Cor208C);
        public static AxisController ApsController = new AxisController(CardAxisName.研华科技PCI_1245orPCI_1285) { Name = "" };

        #region  IO list

        /// <summary>
        ///   总启动按钮
        /// </summary>
        public static IoPoint S1 = new IoPoint(ApsController, 0, 0, IoModes.Senser)
        {
            Name = "S1",
            Description = "总启动按钮"
        };

        /// <summary>
        ///   暂停按钮
        /// </summary>
        public static IoPoint S2 = new IoPoint(ApsController, 0, 1, IoModes.Senser)
        {
            Name = "S2",
            Description = "暂停按钮"
        };

        /// <summary>
        ///   复位按钮
        /// </summary>
        public static IoPoint S3 = new IoPoint(ApsController, 0, 2, IoModes.Senser)
        {
            Name = "S3",
            Description = "复位按钮"
        };

        /// <summary>
        ///   左启动按钮
        /// </summary>
        public static IoPoint S4 = new IoPoint(ApsController, 0, 3, IoModes.Senser)
        {
            Name = "S4",
            Description = "左启动按钮"
        };

        /// <summary>
        ///   右启动按钮
        /// </summary>
        public static IoPoint S5 = new IoPoint(ApsController, 0, 4, IoModes.Senser)
        {
            Name = "S5",
            Description = "右启动按钮"
        };

        /// <summary>
        ///   总门开关
        /// </summary>
        public static IoPoint S6 = new IoPoint(ApsController, 0, 5, IoModes.Senser)
        {
            Name = "S6",
            Description = "总门开关"
        };

        /// <summary>
        ///   竖直光栅
        /// </summary>
        public static IoPoint S7 = new IoPoint(ApsController, 0, 6, IoModes.Senser)
        {
            Name = "S7",
            Description = "竖直光栅"
        };

        /// <summary>
        ///   顶部左1相机X轴基准点
        /// </summary>
        public static IoPoint S8 = new IoPoint(ApsController, 0, 7, IoModes.Senser)
        {
            Name = "S8",
            Description = "顶部左1相机X轴基准点"
        };
        /// <summary>
        ///  顶部左1相机Y轴基准点
        /// </summary>
        public static IoPoint S9 = new IoPoint(ApsController, 0, 8, IoModes.Senser)
        {
            Name = "S9",
            Description = "顶部左1相机Y轴基准点"
        };
        /// <summary>
        ///   顶部左2相机X轴基准点
        /// </summary>
        public static IoPoint S10 = new IoPoint(ApsController, 0, 9, IoModes.Senser)
        {
            Name = "S10",
            Description = "顶部左2相机X轴基准点"
        };
        /// <summary>
        ///   顶部左2相机Y轴基准点
        /// </summary>
        public static IoPoint S11 = new IoPoint(ApsController, 0, 10, IoModes.Senser)
        {
            Name = "S11",
            Description = "顶部左2相机Y轴基准点"
        };
        /// <summary>
        ///  顶部右1相机X轴基准点
        /// </summary>
        public static IoPoint S12 = new IoPoint(ApsController, 0, 11, IoModes.Senser)
        {
            Name = "S12",
            Description = "顶部右1相机X轴基准点"
        };
        /// <summary>
        ///  顶部右1相机Y轴基准点
        /// </summary>
        public static IoPoint S13 = new IoPoint(ApsController, 0, 12, IoModes.Senser)
        {
            Name = "S13",
            Description = "顶部右1相机Y轴基准点"
        };
        /// <summary>
        ///   顶部右2相机X轴基准点
        /// </summary>
        public static IoPoint S14 = new IoPoint(ApsController, 0, 13, IoModes.Senser)
        {
            Name = "S14",
            Description = "顶部右2相机X轴基准点"
        };
        /// <summary>
        ///   顶部右2相机Y轴基准点
        /// </summary>
        public static IoPoint S15 = new IoPoint(ApsController, 0, 14, IoModes.Senser)
        {
            Name = "S15",
            Description = "顶部右2相机Y轴基准点"
        };
        /// <summary>
        ///   底部左相机X轴基准点
        /// </summary>
        public static IoPoint S16 = new IoPoint(ApsController, 0, 15, IoModes.Senser)
        {
            Name = "S16",
            Description = "底部左相机X轴基准点"
        };
        /// <summary>
        ///   底部左相机Y轴基准点
        /// </summary>
        public static IoPoint S17 = new IoPoint(ApsController, 1, 0, IoModes.Senser)
        {
            Name = "S17",
            Description = "底部左相机Y轴基准点"
        };

        /// <summary>
        ///  底部中相机X轴基准点
        /// </summary>
        public static IoPoint S18 = new IoPoint(ApsController, 1, 1, IoModes.Senser)
        {
            Name = "S18",
            Description = "底部中相机X轴基准点"
        };

        /// <summary>
        ///   底部中相机Y轴基准点
        /// </summary>
        public static IoPoint S19 = new IoPoint(ApsController, 1, 2, IoModes.Senser)
        {
            Name = "S19",
            Description = "底部中相机Y轴基准点"
        };

        /// <summary>
        ///   底部右相机X轴基准点
        /// </summary>
        public static IoPoint S20 = new IoPoint(ApsController, 1, 3, IoModes.Senser)
        {
            Name = "S20",
            Description = "底部右相机X轴基准点"
        };

        /// <summary>
        ///   底部右相机Y轴基准点
        /// </summary>
        public static IoPoint S21 = new IoPoint(ApsController, 1, 4, IoModes.Senser)
        {
            Name = "S21",
            Description = "底部右相机Y轴基准点"
        };

        /// <summary>
        ///   Robot光纤物料感应
        /// </summary>
        public static IoPoint S22 = new IoPoint(ApsController, 1, 5, IoModes.Senser)
        {
            Name = "S22",
            Description = "Robot光纤物料感应"
        };

        /// <summary>
        ///   手自动切换开关
        /// </summary>
        public static IoPoint S23 = new IoPoint(ApsController, 1, 6, IoModes.Senser)
        {
            Name = "S23",
            Description = "手自动切换开关"
        };

        /// <summary>
        ///   提升机送料成功
        /// </summary>
        public static IoPoint S24 = new IoPoint(ApsController, 1, 7, IoModes.Senser)
        {
            Name = "S24",
            Description = "提升机送料成功"
        };
        /// <summary>
        ///   提升机接料成功
        /// </summary>
        public static IoPoint S25 = new IoPoint(ApsController, 1, 8, IoModes.Senser)
        {
            Name = "S25",
            Description = "提升机接料成功"
        };
        /// <summary>
        ///   下料机接料成功
        /// </summary>
        public static IoPoint S26 = new IoPoint(ApsController, 1, 9, IoModes.Senser)
        {
            Name = "S26",
            Description = "下料机接料成功"
        };
        /// <summary>
        ///   下料机送料成功
        /// </summary>
        public static IoPoint S27 = new IoPoint(ApsController, 1, 10, IoModes.Senser)
        {
            Name = "S27",
            Description = "下料机送料成功"
        };
        /// <summary>
        ///  机器人使能中
        /// </summary>
        public static IoPoint S28 = new IoPoint(ApsController, 1, 11, IoModes.Senser)
        {
            Name = "S28",
            Description = "机器人使能中"
        };
        /// <summary>
        ///   机器人报警中
        /// </summary>
        public static IoPoint S29 = new IoPoint(ApsController, 1, 12, IoModes.Senser)
        {
            Name = "S29",
            Description = "机器人报警中"
        };

        /// <summary>
        ///   机器人输出信号1
        /// </summary>
        public static IoPoint S30 = new IoPoint(ApsController, 1, 13, IoModes.Senser)
        {
            Name = "S30",
            Description = "机器人输出信号1"
        };
        /// <summary>
        ///   机器人输出信号2
        /// </summary>
        public static IoPoint S31 = new IoPoint(ApsController, 1, 14, IoModes.Senser)
        {
            Name = "S31",
            Description = "机器人输出信号2"
        };
        /// <summary>
        ///   机器人输出信号3
        /// </summary>
        public static IoPoint S32 = new IoPoint(ApsController, 1, 15, IoModes.Senser)
        {
            Name = "S32",
            Description = "机器人输出信号3"
        };


        /// <summary>
        /// 三色灯红灯
        /// </summary>
        public static IoPoint Y1 = new IoPoint(ApsController, 0, 0, IoModes.Signal)
        {
            Name = "Y1",
            Description = "三色灯红灯"
        };

        /// <summary>
        ///   三色灯黄灯
        /// </summary>
        public static IoPoint Y2 = new IoPoint(ApsController, 0, 1, IoModes.Signal)
        {
            Name = "Y2",
            Description = "三色灯黄灯"
        };

        /// <summary>
        ///  三色灯绿灯
        /// </summary>
        public static IoPoint Y3 = new IoPoint(ApsController, 0, 2, IoModes.Signal)
        {
            Name = "Y3",
            Description = "三色灯绿灯"
        };

        /// <summary>
        ///   蜂铃器
        /// </summary>
        public static IoPoint Y4 = new IoPoint(ApsController, 0, 3, IoModes.Signal)
        {
            Name = "Y4",
            Description = "蜂铃器"
        };

        /// <summary>
        ///  内饰灯开/关
        /// </summary>
        public static IoPoint Y5 = new IoPoint(ApsController, 0, 4, IoModes.Signal)
        {
            Name = "Y5",
            Description = "内饰灯开/关"
        };

        /// <summary>
        /// 备用
        /// </summary>
        public static IoPoint Y6 = new IoPoint(ApsController, 0, 5, IoModes.Signal)
        {
            Name = "Y6",
            Description = "备用"
        };

        /// <summary>
        ///   紧急停止
        /// </summary>
        public static IoPoint Y7 = new IoPoint(ApsController, 0, 6, IoModes.Signal)
        {
            Name = "Y7",
            Description = "紧急停止"
        };

        /// <summary>
        ///    机器人使能
        /// </summary>
        public static IoPoint Y8 = new IoPoint(ApsController, 0, 7, IoModes.Signal)
        {
            Name = "Y8",
            Description = "机器人使能"
        };

        /// <summary>
        ///  机器人程序复位
        /// </summary>
        public static IoPoint Y9 = new IoPoint(ApsController, 0, 8, IoModes.Signal)
        {
            Name = "Y9",
            Description = "机器人程序复位"
        };

        /// <summary>
        ///  机器人报警复位
        /// </summary>
        public static IoPoint Y10 = new IoPoint(ApsController, 0, 9, IoModes.Signal)
        {
            Name = "Y10",
            Description = "机器人报警复位"
        };

        /// <summary>
        ///  机器人停止
        /// </summary>
        public static IoPoint Y11 = new IoPoint(ApsController, 0, 10, IoModes.Signal)
        {
            Name = "Y11",
            Description = "机器人停止"
        };

        /// <summary>
        ///  机器人自动运行
        /// </summary>
        public static IoPoint Y12 = new IoPoint(ApsController, 0, 11, IoModes.Signal)
        {
            Name = "Y12",
            Description = "机器人自动运行"
        };

        /// <summary>
        ///   绿灯（双色灯）
        /// </summary>
        public static IoPoint Y13 = new IoPoint(ApsController, 0, 12, IoModes.Signal)
        {
            Name = "Y13",
            Description = " 绿灯（双色灯）"
        };

        /// <summary>
        ///   红灯（双色灯）
        /// </summary>
        public static IoPoint Y14 = new IoPoint(ApsController, 0, 13, IoModes.Signal)
        {
            Name = "Y14",
            Description = "红灯（双色灯）"
        };

        /// <summary>
        ///  光源开启
        /// </summary>
        public static IoPoint Y15 = new IoPoint(ApsController, 0, 14, IoModes.Signal)
        {
            Name = "Y15",
            Description = "光源开启"
        };

        /// <summary>
        ///   扫描触发
        /// </summary>
        public static IoPoint Y16 = new IoPoint(ApsController, 0, 15, IoModes.Signal)
        {
            Name = "Y16",
            Description = "扫描触发"
        };

        /// <summary>
        /// 备用
        /// </summary>
        public static IoPoint Y17 = new IoPoint(ApsController, 0, 16, IoModes.Signal)
        {
            Name = "Y17",
            Description = "备用"
        };

        /// <summary>
        ///   工站2上顶电磁阀
        /// </summary>
        public static IoPoint Y18 = new IoPoint(ApsController, 0, 17, IoModes.Signal)
        {
            Name = "Y18",
            Description = "工站2上顶电磁阀"
        };

        /// <summary>
        ///  工站3上顶电磁阀
        /// </summary>
        public static IoPoint Y19 = new IoPoint(ApsController, 0, 18, IoModes.Signal)
        {
            Name = "Y19",
            Description = "工站3上顶电磁阀"
        };

        /// <summary>
        ///   工站4上顶电磁阀
        /// </summary>
        public static IoPoint Y20 = new IoPoint(ApsController, 0, 19, IoModes.Signal)
        {
            Name = "Y20",
            Description = "机器人"
        };

        /// <summary>
        ///  白板上电磁阀
        /// </summary>
        public static IoPoint Y21 = new IoPoint(ApsController, 0, 20, IoModes.Signal)
        {
            Name = "Y21",
            Description = "白板上电磁阀"
        };

        /// <summary>
        /// 白板下电磁阀
        /// </summary>
        public static IoPoint Y22 = new IoPoint(ApsController, 0, 21, IoModes.Signal)
        {
            Name = "Y22",
            Description = "白板下电磁阀"
        };

        /// <summary>
        ///   黑板上电磁阀
        /// </summary>
        public static IoPoint Y23 = new IoPoint(ApsController, 0, 22, IoModes.Signal)
        {
            Name = "Y23",
            Description = "黑板上电磁阀"
        };

        /// <summary>
        ///    黑板下电磁阀
        /// </summary>
        public static IoPoint Y24 = new IoPoint(ApsController, 0, 23, IoModes.Signal)
        {
            Name = "Y24",
            Description = "黑板下电磁阀"
        };

        /// <summary>
        ///  红灯
        /// </summary>
        public static IoPoint Y25 = new IoPoint(ApsController, 0, 24, IoModes.Signal)
        {
            Name = "Y25",
            Description = "红灯"
        };

        /// <summary>
        ///  黄灯
        /// </summary>
        public static IoPoint Y26 = new IoPoint(ApsController, 0, 25, IoModes.Signal)
        {
            Name = "Y26",
            Description = "黄灯"
        };

        /// <summary>
        ///  绿灯
        /// </summary>
        public static IoPoint Y27 = new IoPoint(ApsController, 0, 26, IoModes.Signal)
        {
            Name = "Y27",
            Description = "绿灯"
        };

        /// <summary>
        ///  蜂铃器
        /// </summary>
        public static IoPoint Y28 = new IoPoint(ApsController, 0, 27, IoModes.Signal)
        {
            Name = "Y28",
            Description = "蜂铃器"
        };

        /// <summary>
        ///   绿灯（双色灯）
        /// </summary>
        public static IoPoint Y29 = new IoPoint(ApsController, 0, 28, IoModes.Signal)
        {
            Name = "Y29",
            Description = " 绿灯（双色灯）"
        };

        /// <summary>
        ///   红灯（双色灯）
        /// </summary>
        public static IoPoint Y30 = new IoPoint(ApsController, 0, 29, IoModes.Signal)
        {
            Name = "Y30",
            Description = "红灯（双色灯）"
        };

        /// <summary>
        ///  光源开启
        /// </summary>
        public static IoPoint Y31 = new IoPoint(ApsController, 0, 30, IoModes.Signal)
        {
            Name = "Y31",
            Description = "光源开启"
        };

        /// <summary>
        ///   扫描触发
        /// </summary>
        public static IoPoint Y32 = new IoPoint(ApsController, 0, 31, IoModes.Signal)
        {
            Name = "Y32",
            Description = "扫描触发"
        };
        #endregion


    }
}