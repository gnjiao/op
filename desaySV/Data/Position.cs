using System;
using System.ToolKit;
namespace desaySV
{
    [Serializable]
    public class Position
    {
        /// <summary>
        /// 单例模式
        /// </summary>
        [NonSerialized]
        public static Position Instance = new Position();

        #region 参数设置  
        /// <summary>
        /// Z轴位置
        /// </summary>
        public axisParam[] ZaxisPostion = new axisParam[10];
        /// <summary>
        /// Y轴位置
        /// </summary>
        public axisParam[] YaxisPostion = new axisParam[6];
       
        /// <summary>
        /// 移动间距(用于标定相机移动距离)
        /// </summary>
        public double MoveXInterval = 100;
        /// <summary>
        /// 待机坐标
        /// </summary>
        public RootParam[] RootPostion = new RootParam[15];
        /// <summary>
        /// 左相机位置
        /// </summary>
        public PosPhoto LPhotoOriPostion = new PosPhoto();
        /// <summary>
        /// 右相机位置
        /// </summary>
        public PosPhoto RPhotoOriPostion = new PosPhoto();
        #endregion
    }
}
