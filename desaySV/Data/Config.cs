using System;

namespace desaySV
{
    [Serializable]
    public class Config
    {
        public static Config Instance = new Config();



        #region  通信参数
        /// <summary>
        /// 扫描枪通信参数
        /// </summary>
        public string ReadCodePortConnectParam = "COM1,19200,None,8,One,1500,1500";
        /// <summary>
        /// 左前光源控制器
        /// </summary>
        public string LFLightController = "COM2,19200,None,8,One,1500,1500";
        /// <summary>
        /// 左后光源控制器
        /// </summary>
        public string LRLightController = "COM3,19200,None,8,One,1500,1500";
        /// <summary>
        /// 右前光源控制器
        /// </summary>
        public string RFLightController = "COM4,19200,None,8,One,1500,1500";
        /// <summary>
        /// 右后光源控制器
        /// </summary>
        public string RRLightController = "COM5,19200,None,8,One,1500,1500";
        /// <summary>
        /// 机器人IP
        /// </summary>
        public string YamahaIP = "127.0.0.1";
        /// <summary>
        /// 机器人端口
        /// </summary>
        public int Port = 8000;
        #endregion

        public string ModelSerial = "";

        public string StationID = "CA5";

        #region 相机参数
        /// <summary>
        /// 相机与产品组装的关系
        /// </summary>
        public PosXY[] OfficePhoto = new PosXY[2];
        /// <summary>
        /// 尺寸换算
        /// </summary>
        public ConversionMM[] PhotoConverMM = new ConversionMM[4];
        /// <summary>
        /// 相机关系标定基准位置(角度)
        /// </summary>
        public RootParam PhotoCalibPostion;
        ///// <summary>
        ///// 相机与机械坐标标定移动间距(角度)
        ///// </summary>
        public PosXY PhotoCalibEndPostion;
        ///// <summary>
        ///// 相机与机械的旋转角度偏差
        ///// </summary>
        public double[] PhotoAngleOffice = new double[4];

        #endregion





    }
}
