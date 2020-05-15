﻿namespace CMotion.Interfaces.Configuration
{
    /// <summary>
    ///     回零配置参数
    /// </summary>
    public class HomeParams
    {
        #region 属性

        /// <summary>
        ///     回零模式 0：ORG,1：EL,2：EZ
        /// </summary>
        public int Mode { get; set; }

        /// <summary>
        ///     回零方向 0：Positive, 1:Negative
        /// </summary>
        public int Dir { get; set; }

        /// <summary>
        ///     寻找Z向信号 0：NO，1：YES
        /// </summary>
        public int EZ { get; set; }

        /// <summary>
        ///     回零最大速度
        /// </summary>
        public int MaxVel { get; set; }

        /// <summary>
        ///     找原点速度
        /// </summary>
        public int OrgVel { get; set; }

        /// <summary>
        ///     回零偏移
        /// </summary>
        public int ZeroOffset { get; set; }

        /// <summary>
        ///     回零超时时间
        /// </summary>
        public double Timeout { get; set; }

        #endregion

        #region 构造器

#pragma warning disable CS1591 // 缺少对公共可见类型或成员“HomeParams.HomeParams()”的 XML 注释
        public HomeParams()
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“HomeParams.HomeParams()”的 XML 注释
        {
        }

#pragma warning disable CS1591 // 缺少对公共可见类型或成员“HomeParams.HomeParams(int, int, int, int, int, int)”的 XML 注释
        public HomeParams(int mode, int dir, int ez, int maxVel, int orgVel, int shift)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“HomeParams.HomeParams(int, int, int, int, int, int)”的 XML 注释
        {
            Mode = mode;
            Dir = dir;
            EZ = ez;
            MaxVel = maxVel;
            OrgVel = orgVel;
            ZeroOffset = shift;
        }
        //"0,0,100,0,10,0,1000"
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“HomeParams.ToString()”的 XML 注释
        public override string ToString()
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“HomeParams.ToString()”的 XML 注释
        {
            return Dir.ToString() + ","+ EZ.ToString() + ","+ MaxVel.ToString() + ","+ Mode.ToString() + ","
                + OrgVel.ToString() + ","+ ZeroOffset.ToString() + ","+ Timeout.ToString(); 
        }
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“HomeParams.Parse(string)”的 XML 注释
        public static  HomeParams Parse(string str)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“HomeParams.Parse(string)”的 XML 注释
        {
            string[] strValue = str.Split(',');
            var homeparams = new HomeParams();
            homeparams.Dir = int.Parse(strValue[0]);
            homeparams.EZ = int.Parse(strValue[1]);
            homeparams.MaxVel = int.Parse(strValue[2]);
            homeparams.Mode = int.Parse(strValue[3]);
            homeparams.OrgVel = int.Parse(strValue[4]);
            homeparams.ZeroOffset = int.Parse(strValue[5]);
            homeparams.Timeout = int.Parse(strValue[6]);
            return homeparams;
        }
        #endregion
    }
}