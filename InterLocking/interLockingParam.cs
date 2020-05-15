using System;
using System.Collections.Generic;

namespace InterLocking
{
    [Serializable]
    public class interLockingParam
    {
        /// <summary>
        /// 单例模式
        /// </summary>
        [NonSerialized]
        public static interLockingParam Instance = new interLockingParam();

        public List<MesData> InterLockingListParam;

        public string mesTxtPath = @"D:\Report\";
        public string csvFilePath = @"D:\MesCsv\";

        public EV_MSTR EvData = new EV_MSTR();
        public int getValue()
        {
            return InterLockingListParam.Count;
        }
    }
}
