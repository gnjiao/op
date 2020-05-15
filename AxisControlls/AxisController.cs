using System;
using System.Threading;
using System.Diagnostics;
using CMotion.AdlinkAps_8164;
using CMotion.Interfaces;
using CMotion.Interfaces.Configuration;
using CMotion.Interfaces.IO;
using CMotion.Interfaces.Axis;
using CMotion.AdlinkAps_AMP204;
using CMotion.AdvantechAps;


namespace AxisControlls
{
    /// <summary>
    ///     运动控制卡控制器....  To 2020.01.04 aiwen
    /// </summary>   
    public class AxisController : Automatic, ISwitchController, IDisposable
    {


        private readonly object obj = new object();
        private bool _disposed;
        private IAxisControl ApsController;
        /// <summary>
        /// 用于切换板卡型号
        /// </summary>
        public CardAxisName M_cardAxisName { get; set; }
        public AxisController(CardAxisName cardAxisName)
        {
            M_cardAxisName = cardAxisName;
            switch (M_cardAxisName)
            {
                case CardAxisName.凌华科技PCI_8164:
                    ApsController = new CMotion.AdlinkAps_8164.ApsController();
                    break;
                case CardAxisName.凌华科技AMP_204Cor208C:
                    ApsController = new CMotion.AdlinkAps_AMP204.ApsController();
                    break;
                case CardAxisName.研华科技PCI_1245orPCI_1285:
                    ApsController = new CMotion.AdvantechAps.ApsController();
                    break;
                default:
                    break;
            }

        }


        #region 初始化
        public bool Initialize()
        {
            return ApsController.Initialize();
        }

        public bool LoadParamFromFile(string xmlfilename)
        {
            return ApsController.LoadParamFromFile(xmlfilename);

        }
        #endregion

        #region 参数设置



        #endregion

        #region 梯形运动/S行运动 必须是绝对位置
        public void RelativeMove(short axisNo, double position, VelocityCurve velocityCurveParams)
        {
            ApsController.RelativeMove(axisNo, position, velocityCurveParams);
        }

        public void AbsoluteMove(short axisNo, double position, VelocityCurve velocityCurveParams)
        {
            ApsController.AbsoluteMove(axisNo, position, velocityCurveParams);
        }
        #endregion

        #region 多轴插补
        /// <summary>
        /// 直线插补/插补
        /// </summary>
        public void Line2Interpolation(short CardNo, short axisNoX, double posX, double posY, VelocityCurve velocityCurveParams)
        {

        }

        /// <summary>
        /// 直线插补/插补
        /// </summary>
        public void Line3Interpolation(short CardNo, double posX, double posY, double posZ, VelocityCurve velocityCurveParams)
        {

        }

        /// <summary>
        /// 直线插补/插补
        /// </summary>
        public void Line4Interpolation(short CardNo, double posX, double posY, double posZ, double posU, VelocityCurve velocityCurveParams)
        {

        }

        /// <summary>
        /// 圆弧插补
        /// </summary>
        /// <param name="axisNoX">轴</param>
        /// <param name="CenterposX">中心位置X</param>
        /// <param name="CenterposY">中心位置Y</param>
        /// <param name="EndposX">终点位置X</param>
        /// <param name="EndposY">终点位置Y</param>
        /// 
        /// <param name="velocityCurveParams"></param>
        public void CirculInterpolation(short CardNo, short axisNoX, double CenterposX, double CenterposY, double EndposX, double EndposY, short Dir, VelocityCurve velocityCurveParams)
        {

        }

        #endregion

        #region 连续运动
        /// <summary>
        ///     连续运动
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="moveDirection"></param>
        /// <param name="velocityCurveParams"></param>
        /// <returns></returns>
        public void ContinuousMove(short axisNo, MoveDirection moveDirection, VelocityCurve velocityCurveParams)
        {
            ApsController.ContinuousMove(axisNo, moveDirection, velocityCurveParams);
        }
        /// <summary>
        ///     立即停止
        /// </summary>
        /// <param name="axisNo"></param>
        public void ImmediateStop(short axisNo)
        {
            ApsController.ImmediateStop(axisNo);
        }

        /// <summary>
        ///     减速停止指定机构轴脉冲输出
        /// </summary>
        /// <param name="axisNo"></param>
        public void DecelStop(short axisNo)
        {
            ApsController.DecelStop(axisNo);
        }

        #endregion

        #region 手动模式

        public void BackHome(short axisNo, short HomeMode, VelocityCurve velocityCurveParams)
        {
            ApsController.BackHome(axisNo, HomeMode, 0, 0, 0, 0, velocityCurveParams, 0, 1);
        }

        public int CheckHomeDone(short axisNo, double timeoutLimit)
        {
            return ApsController.CheckHomeDone(axisNo, timeoutLimit);
        }
        #endregion

        #region 同步模式

        #endregion

        #region 轴IO状态
        public bool IsReady(short axisNo)
        {
            return ApsController.IsReady(axisNo);
        }
        public bool IsAlm(short axisNo)
        {
            return ApsController.IsAlm(axisNo);
        }
        /// <summary>
        /// zheng
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        public bool IsPel(short axisNo)
        {
            return ApsController.IsPel(axisNo);
        }
        /// <summary>
        /// fu 
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        public bool IsMel(short axisNo)
        {
            return ApsController.IsMel(axisNo);
        }
        public bool IsOrg(short axisNo)
        {
            return ApsController.IsOrg(axisNo);
        }

        public bool IsEMG(short axisNo)
        {
            return ApsController.IsEMG(axisNo);
        }
        public bool IsEZ(short axisNo)
        {
            return ApsController.IsEZ(axisNo);
        }
        public bool IsInp(short axisNo)
        {
            return ApsController.IsInp(axisNo);
        }

        /// <summary>
        ///   获取轴使能
        /// </summary>
        /// <param name="noId"></param>
        public bool GetServo(short axisNo)
        {
           return ApsController.GetServo(axisNo);
        }


        /// <summary>
        ///     轴上电
        /// </summary>
        /// <param name="noId"></param>
        public void ServoOn(short axisNo)
        {
            ApsController.ServoOn(axisNo);
        }

        /// <summary>
        ///     轴掉电
        /// </summary>
        /// <param name="noId"></param>
        public void ServoOff(short axisNo)
        {
            ApsController.ServoOff(axisNo);
        }


        #endregion


        #region 编码器状态
        /// <summary>
        ///     获取轴当前位置
        /// </summary>
        /// <param name="axisNo">轴标识</param>
        /// <returns>当前位置</returns>
        public double GetCurrentCommandPosition(short axisNo)
        {
            return ApsController.GetCurrentCommandPosition(axisNo);
        }
        public double GetCurrentFeedbackPosition(short axisNo)
        {
            return ApsController.GetCurrentFeedbackPosition(axisNo);
        }
        public double GetCurrentCommandSpeed(short axisNo)
        {
            return ApsController.GetCurrentCommandSpeed(axisNo);
        }

        public void SetCommandPosition(short axisNo, int position)
        {
            ApsController.SetCommandPosition(axisNo, position);
        }
        /// <summary>
        /// 当没有编码器时执行此命令
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="position"></param>
        public void SetFeedbackPosition(short axisNo, int position)
        {
            ApsController.SetFeedbackPosition(axisNo, position);
        }


        #endregion


        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }



        ~AxisController()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing, short Card = 0)
        {
            if (_disposed)
                return;
            if (disposing)
            {
            }
            try
            {
                //Motion._8164_int_disable(Card);
            }
            catch (Exception)
            {
                //ignorl
            }
            _disposed = true;
        }

        #endregion


        public bool Read(IoPoint ioPoint)
        {
            return ApsController.Read(ioPoint);
        }

        public void Write(IoPoint ioPoint, bool value)
        {
            ApsController.Write(ioPoint, value);
        }

    }



    public enum CardAxisName
    {
        凌华科技PCI_8164,
        凌华科技AMP_204Cor208C,
        研华科技PCI_1245orPCI_1285

    }
}