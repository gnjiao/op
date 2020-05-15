
using AxisControlls;
using CMotion.Interfaces;
using CMotion.Interfaces.Axis;
using CMotion.Interfaces.Configuration;
using System.Collections.Generic;
namespace System.Enginee
{
    /// <summary>
    ///     通用  2020-01-17 to aiwen
    /// </summary>
    public class ApsAxis : Axis, INeedClean
    {
        protected readonly AxisController ApsController;

        public Func<bool> _condition;
        public ApsAxis(AxisController apsController)
        {
            ApsController = apsController;
        }
        /// <summary>
        /// 位置（用于辨别转盘位置）
        /// </summary>
        public int intPos { get; set; }
        public bool isCondition
        {
            get
            {
                try
                {
                    return _condition();
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        #region Overrides of Axis

        /// <summary>
        ///     当前 Absolute 位置。
        /// </summary>
        public override double CurrentPos { get; }
        public override double BackPos { get; }

        public override double CurrentSpeed
        {
            get
            {
                return Convert.ToDouble(ApsController.GetCurrentCommandSpeed(NoId)) / Transmission.EquivalentPulse;
            }
        }

        /// <summary>
        /// 轴传动参数
        /// </summary>
        public CMotion.Interfaces.Configuration.TransmissionParams Transmission { get; set; }
       
        #region 获取当前轴的 IO 信号
        /// <summary>
        ///     是否已励磁。
        /// </summary>
        public bool IsServon
        {
            get { return ApsController.GetServo(NoId); }
            set
            {
                if (value)
                {
                    ApsController.ServoOn(NoId);
                }
                else
                {
                    ApsController.ServoOff(NoId);
                }
            }
        }
        public short HomeMode { get; set; }
        public uint HomeDir { get; set; }
        /// <summary>
        ///     是否到达正限位
        /// </summary>
        /// <returns></returns>
        public bool IsPEL
        {
            get { return ApsController.IsPel(NoId); }
        }
        /// <summary>
        ///     是否到达正负位
        /// </summary>
        /// <returns></returns>
        public bool IsMEL
        {
            get { return ApsController.IsMel(NoId); }
        }

        /// <summary>
        ///     是否在轴原点
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        public bool IsOrign
        {
            get { return ApsController.IsOrg(NoId); }
        }
        /// <summary>
        /// 是否在轴Z相
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        public bool IsSZ
        {
            get { return ApsController.IsEZ(NoId); }
        }
        /// <summary>
        /// 是否在轴Z相
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        public bool IsINP
        {
            get { return ApsController.IsInp(NoId); }
        }

        /// <summary>
        ///     是否报警
        /// </summary>
        public override bool IsAlarmed
        {
            get { return ApsController.IsAlm(NoId); }
        }
        /// <summary>
        ///     是否原点
        /// </summary>
        public bool IsOrigin
        {
            get { return ApsController.IsOrg(NoId); }
        }

        public override bool IsEmg { get; }
        #endregion
        /// <summary>
        ///     是否已完成最后运动指令。
        /// </summary>
        /// <code>? + var isReach = Math.Abs(commandPosition - currentPosition) &lt; Precision;</code>
        public override bool IsDone
        {
            get { return ApsController.IsInp(NoId); }
        }

        /// <summary>
        /// 运动轴轴移动到指定的位置。
        /// </summary>
        /// <param name="value">将要移动到的位置。</param>
        /// <param name="velocityCurve">移动时的运行参数。</param>
        public override void MoveTo(double value, VelocityCurve velocityCurve = null)
        {
            if (!isCondition) { return; }
            var Data = value * Transmission.EquivalentPulse;
            var velocity = velocityCurve;
            velocity.Strvel = VelocityCurveRun.Strvel;
            if (VelocityCurveRun.Tacc >= 50000000)
            {
                velocity.Tacc = 50000000;
                velocity.Tdec = 50000000;
            }
            else
            {
                velocity.Tacc = VelocityCurveRun.Tacc;
                velocity.Tdec = VelocityCurveRun.Tacc;
            }
            velocity.Maxvel = velocityCurve.Maxvel * Transmission.EquivalentPulse;
            ApsController.AbsoluteMove(NoId, (int)Data, velocity);
        }

        /// <summary>
        ///     运动轴相对移动到指定位置。
        /// </summary>
        /// <param name="value">要移动到的距离。</param>
        /// <param name="velocityCurve"></param>
        public override void MoveDelta(double value, VelocityCurve velocityCurve = null)
        {
            if (!isCondition) { return; }      
            ApsController.RelativeMove(NoId, (int)value, velocityCurve);
        }

        /// <summary>
        ///     正向移动。
        /// </summary>
        public override void Postive()
        {
            if (!isCondition) { return; }
            var velocityCurve = new VelocityCurve { Strvel = VelocityCurveRun.Strvel, Maxvel = (Speed ?? 0) * 10000, Tacc = VelocityCurveRun.Tacc };
            ApsController.ContinuousMove(NoId, MoveDirection.Postive, velocityCurve);
        }

        /// <summary>
        ///     反向移动。
        /// </summary>
        public override void Negative()
        {
            if (!isCondition) { return; }
            var velocityCurve = new VelocityCurve { Strvel = VelocityCurveRun.Strvel, Maxvel = (Speed ?? 0) * 10000, Tacc = VelocityCurveRun.Tacc };
            ApsController.ContinuousMove(NoId, MoveDirection.Negative, velocityCurve);
        }

        /// <summary>
        ///     轴停止运动。
        /// </summary>
        /// <param name="velocityCurve"></param>
        public override void Stop(VelocityCurve velocityCurve = null)
        {
            ApsController.ImmediateStop(NoId);
        }

        public override void Initialize()
        {
            //ApsController.MoveOrigin(NoId);
        }

        #endregion

        #region Implementation of INeedInitialization

        #endregion

        #region Implementation of INeedClean

        /// <summary>
        ///      清除
        /// </summary>
        public void Clean()
        {
            //Stop();
            //ApsController.CleanError(NoId);
        }

        public override bool IsInPosition(double pos)
        {
            throw new NotImplementedException();
        }

        public override void BackHome()
        {
            if (!isCondition) { return; }
            ApsController.BackHome(NoId, HomeMode, VelocityCurveHome);
        }

        public override int CheckHomeDone(double timeoutLimit) => ApsController.CheckHomeDone(NoId, timeoutLimit);

        public override void SetCurrentPos(double pos)
        {           
        }


        /// <summary>
        /// 轴报警集合
        /// </summary>
        public IList<Alarm> Alarms
        {
            get
            {
                var list = new List<Alarm>();
                list.Add(new Alarm(() => ApsController.IsAlm(NoId))
                {
                    AlarmKey= Name + "故障报警",
                    AlarmLevel = AlarmLevels.Error, 
                    Name = Name + "故障报警" 
                }
                    //AlarmKey=""
                );            

                return list;
            }
        }
        #endregion
    }
}