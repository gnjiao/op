namespace CMotion.Interfaces.Configuration
{
    /// <summary>
    ///     软限位参数
    /// </summary>
    public class SoftLimitParams
    {
        #region 属性

        /// <summary>
        ///     使能软件位
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        ///     正限位位置
        /// </summary>
        public int SPelPosition { get; set; }

        /// <summary>
        ///     负限位位置
        /// </summary>
        public int SMelPosition { get; set; }

        #endregion

        #region 构造器

#pragma warning disable CS1591 // 缺少对公共可见类型或成员“SoftLimitParams.SoftLimitParams()”的 XML 注释
        public SoftLimitParams()
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“SoftLimitParams.SoftLimitParams()”的 XML 注释
        {
        }

#pragma warning disable CS1591 // 缺少对公共可见类型或成员“SoftLimitParams.SoftLimitParams(bool, int, int)”的 XML 注释
        public SoftLimitParams(bool enable, int pPosition, int mPosition)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“SoftLimitParams.SoftLimitParams(bool, int, int)”的 XML 注释
        {
            Enable = enable;
            SPelPosition = pPosition;
            SMelPosition = mPosition;
        }
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“SoftLimitParams.ToString()”的 XML 注释
        public override string ToString()
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“SoftLimitParams.ToString()”的 XML 注释
        {
            return (Enable ? "1" : "0") + ","+ SMelPosition.ToString() + ","+ SPelPosition.ToString();
        }
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“SoftLimitParams.Parse(string)”的 XML 注释
        public static SoftLimitParams Parse(string str)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“SoftLimitParams.Parse(string)”的 XML 注释
        {
            string[] strValue = str.Split(',');
            var softLimitParams = new SoftLimitParams();
            softLimitParams.Enable = strValue[0] == "1" ? true : false;
            softLimitParams.SMelPosition = int.Parse(strValue[1]);
            softLimitParams.SPelPosition = int.Parse(strValue[2]);
            return softLimitParams;
        }
        #endregion
    }
}