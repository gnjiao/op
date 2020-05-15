using System;

namespace desaySV
{
    /// <summary>
    /// 用于保存产品的其他参数
    /// </summary>
    [Serializable]    
   public class ProductConfig
    {
        public static ProductConfig Instance = new ProductConfig();


        public SwitchCom switchCom = SwitchCom.MES;

        #region  生产参数
        /// <summary>
        ///     OK产品总数
        /// </summary>
        public int ProductOkTotal;
        /// <summary>
        ///     NG产品总数
        /// </summary>
        public int ProductNgTotal;
        /// <summary>
        /// 产品总数
        /// </summary>
        public int ProductTotal
        {
            get
            {
                return ProductOkTotal + ProductNgTotal;
            }
        }

        #endregion





    }


}
