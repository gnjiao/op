using CMotion.Interfaces.IO;
using CMotion.Interfaces.Axis;
using CMotion.Interfaces.Configuration;
namespace CMotion.Interfaces
{
    /// <summary>
    ///     元器件（部件依赖项）
    /// </summary>
    public abstract class Automatic : IAutomatic
    {
        #region Implementation of IAutomatic
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }


        #endregion
    }

    /// <summary>
    /// IO卡基类
    /// </summary>
    public abstract class IdaskControl : Automatic
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public abstract void Initialize();
        /// <summary>
        /// 读
        /// </summary>
        /// <param name="ioPoint"></param>
        /// <returns></returns>
        public abstract bool Read(IoPoint ioPoint);
        /// <summary>
        /// 写
        /// </summary>
        /// <param name="ioPoint"></param>
        /// <param name="value"></param>
        public abstract void Write(IoPoint ioPoint, bool value);
        /// <summary>
        /// 型号
        /// </summary>
        public ushort Type { get; set; }

    }

    /// <summary>
    /// 轴基类
    /// </summary>
    public abstract class IAxisControl : Automatic
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public abstract bool Initialize();
        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <param name="xmlfilename"></param>
        /// <returns></returns>
        public abstract bool LoadParamFromFile(string xmlfilename);
        /// <summary>
        /// 相对定位
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="position"></param>
        /// <param name="velocityCurveParams"></param>
        public abstract void RelativeMove(short axisNo, double position, VelocityCurve velocityCurveParams);
        /// <summary>
        /// 绝对定位
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="position"></param>
        /// <param name="velocityCurveParams"></param>
        public abstract void AbsoluteMove(short axisNo, double position, VelocityCurve velocityCurveParams);
        /// <summary>
        /// 连续移动
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="moveDirection"></param>
        /// <param name="velocityCurveParams"></param>
        public abstract void ContinuousMove(short axisNo, MoveDirection moveDirection, VelocityCurve velocityCurveParams);
        /// <summary>
        /// 立即停止
        /// </summary>
        /// <param name="axisNo"></param>
        public abstract void ImmediateStop(short axisNo);
        /// <summary>
        /// 减速停止
        /// </summary>
        /// <param name="axisNo"></param>
        public abstract void DecelStop(short axisNo);
        /// <summary>
        /// 回原点
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="HomeMode">回原点模式</param>
        /// <param name="Org"></param>
        /// <param name="Ez"></param>
        /// <param name="EZcount"></param>
        /// <param name="erc"></param>
        /// <param name="velocityCurveParams">回原点速度</param>
        /// <param name="ORGOffset">原点偏移位置</param>
        /// <param name="DirMode">回原点方向</param>
        public abstract void BackHome(short axisNo, short HomeMode, short Org, short Ez, short EZcount, short erc
          , VelocityCurve velocityCurveParams, double ORGOffset, uint DirMode);
        /// <summary>
        /// 回原点完成
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="timeoutLimit"></param>
        /// <returns></returns>
        public abstract int CheckHomeDone(short axisNo, double timeoutLimit);
        /// <summary>
        /// 是否准备好
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        public abstract bool IsReady(short axisNo);
        /// <summary>
        /// 是否报警
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        public abstract bool IsAlm(short axisNo);
        /// <summary>
        /// 是否正限位
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        public abstract bool IsPel(short axisNo);
        /// <summary>
        /// 是否负限位
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        public abstract bool IsMel(short axisNo);
        /// <summary>
        /// 是否原点
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        public abstract bool IsOrg(short axisNo);
        /// <summary>
        /// 是否急停
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        public abstract bool IsEMG(short axisNo);
        /// <summary>
        /// 伺服Z相
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        public abstract bool IsEZ(short axisNo);
        /// <summary>
        /// 定位完成信号
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        public abstract bool IsInp(short axisNo);       
        /// <summary>
        /// 轴上电
        /// </summary>
        /// <param name="axisNo"></param>
        public abstract void ServoOn(short axisNo);
        /// <summary>
        /// 轴上电
        /// </summary>
        /// <param name="axisNo"></param>
        public abstract bool GetServo(short axisNo);
        /// <summary>
        /// 轴掉电
        /// </summary>
        /// <param name="axisNo"></param>
        public abstract void ServoOff(short axisNo);
        /// <summary>
        /// 获取命令当前位置
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        public abstract double GetCurrentCommandPosition(short axisNo);
        /// <summary>
        /// 获取编码器当前位置
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        public abstract double GetCurrentFeedbackPosition(short axisNo);
        /// <summary>
        /// 获取当前速度
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        public abstract double GetCurrentCommandSpeed(short axisNo);
        /// <summary>
        /// 设置命令位置
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="position"></param>
        public abstract void SetCommandPosition(short axisNo, int position);
        /// <summary>
        /// 设置编码器位置
        /// </summary>
        /// <param name="axisNo"></param>
#pragma warning disable CS1573 // 参数“pos”在“IAxisControl.SetFeedbackPosition(short, int)”的 XML 注释中没有匹配的 param 标记(但其他参数有)
        public abstract void SetFeedbackPosition(short axisNo, int pos);
#pragma warning restore CS1573 // 参数“pos”在“IAxisControl.SetFeedbackPosition(short, int)”的 XML 注释中没有匹配的 param 标记(但其他参数有)
        /// <summary>
        /// 读板卡地址
        /// </summary>
        /// <param name="ioPoint"></param>
        /// <returns></returns>
        public abstract bool Read(IoPoint ioPoint);
        /// <summary>
        /// 写板卡地址
        /// </summary>
        /// <param name="ioPoint"></param>
        /// <param name="value"></param>
        public abstract void Write(IoPoint ioPoint, bool value);

    }
}