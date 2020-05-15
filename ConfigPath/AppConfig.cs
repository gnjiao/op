using System.IO;
using System;

namespace ConfigPath
{
    public class AppConfig
    {
        #region 固定参数
        /// <summary>
        /// 机台固定参数（如通信，机台参数）
        /// </summary>
        public static string ConfigIntrinsicProductPathdName => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\Config.xml");

        /// <summary>
        /// 板卡，机构固定参数（如回原点参数，传动比）
        /// </summary>
        public static string ConfigIntrinsicParamAxisCardName => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\paramAxis.xml");
        /// <summary>
        /// 产品参数
        /// </summary>
        public static string ProductIntrinsicConfigModel => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\Model.ini");
        /// <summary>
        /// 固定参数其他未定义的参数
        /// </summary>
        public static string ConfigIntrinsicOtherPathName => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\Other.xml");
        /// <summary>
        /// 托盘配置文件路径(带型号)
        /// </summary>
        public static string ConfigIntrinsicTrayName => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\Tray.ini");
        #endregion

        #region 不固定参数
        /// <summary>
        /// 保存延时的路径（带型号）
        /// </summary>
        public static string ConfigDelayName => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\" + ProductPath + "\\Delay.xml");

        /// <summary>
        /// 位置路径（带型号）
        /// </summary>
        public static string ConfigPositionName => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\" + ProductPath + "\\Position.xml");
        /// <summary>
        /// 运行速度（带型号）
        /// </summary>
        public static string ConfigOtherParamName => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\" + ProductPath + "\\other.xml");

        /// <summary>
        /// 运行速度（带型号）
        /// </summary>
        public static string ConfigParamAxisCardName => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\" + ProductPath + "\\paramAxis.xml");

        /// <summary>
        /// MesConfig（带型号）
        /// </summary>
        public static string MesConfigPathName => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\" + ProductPath + "\\MesConfig.xml");
   

        #endregion

        #region 轴卡配置参数
        /// <summary>
        /// 板卡配置文件路径（根据板卡而定）
        /// </summary>
        public static string ConfigAxisCardName(string path)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config\\BoardConfig\\" + path);
        }

        #endregion

        /// <summary>
        /// 产品型号
        /// </summary>
        public static string ProductPath;
       

    }
}
