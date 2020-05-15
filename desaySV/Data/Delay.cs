using System;
using System.Enginee;

namespace desaySV
{
    [Serializable]
    public class Delay
    {
        [NonSerialized]
        public static Delay Instance = new Delay();


       

        /// <summary>
        /// 左负压
        /// </summary>
        public CylinderDelay LeftCylinderDelay { get; set; } = new CylinderDelay() { OriginTime = 0, MoveTime = 0, AlarmTime = 2000 };
        /// <summary>
        /// 右负压
        /// </summary>
        public CylinderDelay RightCylinderDelay { get; set; } = new CylinderDelay() { OriginTime = 0, MoveTime = 0, AlarmTime = 2000 };
        /// <summary>
        /// 切换气缸
        /// </summary>
        public CylinderDelay SwitchCylinderDelay { get; set; } = new CylinderDelay() { OriginTime = 0, MoveTime = 0, AlarmTime = 2000 };
       

    }
}
