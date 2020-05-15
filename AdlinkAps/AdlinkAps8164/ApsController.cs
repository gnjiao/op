using System;
using System.Collections.Generic;
using CMotion.Interfaces;
using CMotion.Interfaces.Configuration;
using CMotion.Interfaces.IO;
using CMotion.Interfaces.Axis;
using System.ComponentModel.Composition;
using System.Diagnostics;

namespace CMotion.AdlinkAps_8164
{
    /// <summary>
    ///     凌华科技PCI-8164运动控制卡控制器....  To 2020.01.04 aiwen
    /// </summary>
    [Export(typeof(ApsController))]
    public class ApsController : IAxisControl, ISwitchController, IDisposable
    {
        private readonly List<ushort> m_Axises;
        private readonly List<ushort> m_Devices;
        private readonly object obj = new object();
        private bool _disposed;

        public ApsController()
        {
            m_Axises = new List<ushort>();
            m_Devices = new List<ushort>();
        }


        #region 初始化
        public override bool Initialize()
        {
            short card = 0;
            var ret = Motion._8164_initial(ref card);
            for (ushort i = 0; i < card; i++)
            {
                m_Devices.Add(i);
                for (ushort j = 0; j < 4; j++)
                {
                    m_Axises.Add(j);
                }
            }
            if (ret == 0) ThrowIfResultError(ret);
            return ret == 0;
        }

        public override bool LoadParamFromFile(string xmlfilename)
        {
            for (int i = 0; i < m_Devices.Count; i++)
            {
                //Set all configurations for the device according to the loaded file
                var ret = Motion._8164_config_from_file(xmlfilename);
                ThrowIfResultError(ret);
                if (ret != 0)
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region 参数设置



        #endregion

        #region 梯形运动/S行运动 必须是绝对位置
        public override void RelativeMove(short axisNo, double position, VelocityCurve velocityCurveParams)
        {
            bool sStart = CurveTypes.S == velocityCurveParams.VelocityCurveType;
            if (sStart)
            {
                var ret = Motion._8164_start_sr_move(axisNo, position, velocityCurveParams.Strvel, velocityCurveParams.Maxvel, velocityCurveParams.Tacc, velocityCurveParams.Tdec,
                 velocityCurveParams.Svacc, velocityCurveParams.Svdec);
                ThrowIfResultError(ret);
            }
            else
            {
                var ret = Motion._8164_start_tr_move(axisNo, position, velocityCurveParams.Strvel, velocityCurveParams.Maxvel, velocityCurveParams.Tacc, velocityCurveParams.Tdec);
                ThrowIfResultError(ret);
            }

        }

        public override void AbsoluteMove(short axisNo, double position, VelocityCurve velocityCurveParams)
        {
            bool sStart = CurveTypes.S == velocityCurveParams.VelocityCurveType;
            if (sStart)
            {
                var ret = Motion._8164_start_ta_move(axisNo, position, velocityCurveParams.Strvel, velocityCurveParams.Maxvel, velocityCurveParams.Tacc, velocityCurveParams.Tdec);
                ThrowIfResultError(ret);
            }
            else
            {
                var ret = Motion._8164_start_sa_move(axisNo, position, velocityCurveParams.Strvel, velocityCurveParams.Maxvel, velocityCurveParams.Tacc, velocityCurveParams.Tdec,
                  velocityCurveParams.Svacc, velocityCurveParams.Svdec);
                ThrowIfResultError(ret);
            }
        }
        #endregion

        #region 多轴插补
        /// <summary>
        /// 直线插补/插补
        /// </summary>
        public void Line2Interpolation(short CardNo, short axisNoX, double posX, double posY, VelocityCurve velocityCurveParams)
        {
            bool sStart = CurveTypes.S == velocityCurveParams.VelocityCurveType;
            if (sStart)
            {
                if (axisNoX < 2)
                {
                    var ret = Motion._8164_start_ta_move_xy(CardNo, posX, posY, velocityCurveParams.Strvel, velocityCurveParams.Maxvel, velocityCurveParams.Tacc, velocityCurveParams.Tdec);
                    ThrowIfResultError(ret);
                }
                else
                {
                    var ret = Motion._8164_start_ta_move_zu(CardNo, posX, posY, velocityCurveParams.Strvel, velocityCurveParams.Maxvel, velocityCurveParams.Tacc, velocityCurveParams.Tdec);
                    ThrowIfResultError(ret);
                }
            }
            else
            {
                if (axisNoX < 2)
                {
                    var ret = Motion._8164_start_sa_move_xy(CardNo, posX, posY, velocityCurveParams.Strvel, velocityCurveParams.Maxvel, velocityCurveParams.Tacc, velocityCurveParams.Tdec,
                          velocityCurveParams.Svacc, velocityCurveParams.Svdec);
                    ThrowIfResultError(ret);
                }
                else
                {
                    var ret = Motion._8164_start_sa_move_zu(CardNo, posX, posY, velocityCurveParams.Strvel, velocityCurveParams.Maxvel, velocityCurveParams.Tacc, velocityCurveParams.Tdec,
                          velocityCurveParams.Svacc, velocityCurveParams.Svdec);
                    ThrowIfResultError(ret);
                }
            }
        }

        /// <summary>
        /// 直线插补/插补
        /// </summary>
        public void Line3Interpolation(short CardNo, double posX, double posY, double posZ, VelocityCurve velocityCurveParams)
        {
            bool sStart = CurveTypes.S == velocityCurveParams.VelocityCurveType;
            if (sStart)
            {
                short axisNo = 0;
                var ret = Motion._8164_start_ta_line3(CardNo, ref axisNo, posX, posY, posZ, velocityCurveParams.Strvel, velocityCurveParams.Maxvel, velocityCurveParams.Tacc, velocityCurveParams.Tdec);
                ThrowIfResultError(ret);
            }
            else
            {
                short axisNo = 0;
                var ret = Motion._8164_start_sa_line3(CardNo, ref axisNo, posX, posY, posZ, velocityCurveParams.Strvel, velocityCurveParams.Maxvel, velocityCurveParams.Tacc, velocityCurveParams.Tdec,
                velocityCurveParams.Svacc, velocityCurveParams.Svdec);
                ThrowIfResultError(ret);
            }
        }

        /// <summary>
        /// 直线插补/插补
        /// </summary>
        public void Line4Interpolation(short CardNo, double posX, double posY, double posZ, double posU, VelocityCurve velocityCurveParams)
        {
            bool sStart = CurveTypes.S == velocityCurveParams.VelocityCurveType;
            if (sStart)
            {
                var ret = Motion._8164_start_ta_line4(CardNo, posX, posY, posZ, posU, velocityCurveParams.Strvel, velocityCurveParams.Maxvel, velocityCurveParams.Tacc, velocityCurveParams.Tdec);
                ThrowIfResultError(ret);
            }
            else
            {
                var ret = Motion._8164_start_sa_line4(CardNo, posX, posY, posZ, posU, velocityCurveParams.Strvel, velocityCurveParams.Maxvel, velocityCurveParams.Tacc, velocityCurveParams.Tdec,
                velocityCurveParams.Svacc, velocityCurveParams.Svdec);
                ThrowIfResultError(ret);
            }
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
            if (axisNoX < 2)
            {
                var ret = Motion._8164_start_a_arc_xy(CardNo, CenterposX, CenterposY, EndposX, EndposY, Dir, velocityCurveParams.Maxvel);
                ThrowIfResultError(ret);
            }
            else
            {
                var ret = Motion._8164_start_a_arc_zu(CardNo, CenterposX, CenterposY, EndposX, EndposY, Dir, velocityCurveParams.Maxvel);
                ThrowIfResultError(ret);
            }
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
        public override void ContinuousMove(short axisNo, MoveDirection moveDirection, VelocityCurve velocityCurveParams)
        {
            bool sStart = CurveTypes.S == velocityCurveParams.VelocityCurveType;
            double dir = MoveDirection.Postive == moveDirection ? 1 : -1;
            if (sStart)
            {
                Motion._8164_tv_move(axisNo, velocityCurveParams.Strvel * 1000 * dir, velocityCurveParams.Maxvel * dir, velocityCurveParams.Tacc * 0.01);
            }
            else
            {
                Motion._8164_sv_move(axisNo, velocityCurveParams.Strvel * 1000 * dir, velocityCurveParams.Maxvel * dir, velocityCurveParams.Tacc, velocityCurveParams.Svacc);

            }
        }
        /// <summary>
        ///     立即停止
        /// </summary>
        /// <param name="axisNo"></param>
        public override void ImmediateStop(short axisNo)
        {
            Motion._8164_emg_stop(axisNo);
        }

        /// <summary>
        ///     减速停止指定机构轴脉冲输出
        /// </summary>
        /// <param name="axisNo"></param>
        public override void DecelStop(short axisNo)
        {
            Motion._8164_sd_stop(axisNo, 0.5);
            //ThrowIfResultError(reult);
        }

        #endregion

        #region 手动模式

        public override void BackHome(short axisNo, short HomeMode, short Org, short Ez, short EZcount, short erc
            , VelocityCurve velocityCurveParams, double ORGOffset,uint DirMode)
        {
            var ret = Motion._8164_set_home_config(axisNo, HomeMode, Org, Ez, EZcount, erc);
            ThrowIfResultError(ret);
            ret = Motion._8164_home_search(axisNo, velocityCurveParams.Strvel, velocityCurveParams.Maxvel,
                velocityCurveParams.Tacc, ORGOffset);
            ThrowIfResultError(ret);
        }
       
        public override int CheckHomeDone(short axisNo, double timeoutLimit)
        {
#pragma warning disable CS0219 // 变量“status”已被赋值，但从未使用过它的值
            ushort status = 0;
#pragma warning restore CS0219 // 变量“status”已被赋值，但从未使用过它的值
            var strtime = new Stopwatch();
            strtime.Start();

            do
            {
                break;
                ////判断是否正常停止
                //Motion.mAcm_AxGetState(m_Axises[axisNo], ref status);
                //if (status == (int)APS_StateStatus.STA_AxErrorStop)
                //    return -1;
                //if (status == (int)APS_StateStatus.STA_Stopping)
                //    break;
                ////判断INP鑫海
                //if (hasExtEncode)
                //{
                //    uint status1 = 0;
                //    Motion.mAcm_AxGetMotionIO(m_Axises[axisNo], ref status1);
                //    if (((status >> (int)APS_Define.INP) & 1) == 1)
                //        return -2;
                //}
                ////检查是否超时
                //strtime.Stop();
                //if (strtime.ElapsedMilliseconds / 1000.0 > timeoutLimit)
                //{
                //    Motion.mAcm_AxStopEmg(m_Axises[axisNo]);
                //    return -3;
                //}
                //strtime.Start();
                ////延时
                //Thread.Sleep(20);
            } while (true);
            return 0;
        }

        #endregion

        #region 同步模式

        #endregion

        #region 轴IO状态
        public override bool IsReady(short axisNo)
        {
            return ((getIOstasus(axisNo) >> 0) & 1) == 1;
        }
        public override bool IsAlm(short axisNo)
        {
            return ((getIOstasus(axisNo) >> 1) & 1) == 1;
        }
        /// <summary>
        /// zheng
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        public override bool IsPel(short axisNo)
        {
            return ((getIOstasus(axisNo) >> 2) & 1) == 1;
        }
        /// <summary>
        /// fu 
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        public override bool IsMel(short axisNo)
        {
            return ((getIOstasus(axisNo) >> 3) & 1) == 1;
        }
        public override bool IsOrg(short axisNo)
        {
            return ((getIOstasus(axisNo) >> 4) & 1) == 1;
        }
        public bool IsDir(short axisNo)
        {
            return ((getIOstasus(axisNo) >> 5) & 1) == 1;
        }
        public override bool IsEMG(short axisNo)
        {
            return ((getIOstasus(axisNo) >> 6) & 1) == 1;
        }
        public override bool IsEZ(short axisNo)
        {
            return ((getIOstasus(axisNo) >> 9) & 1) == 1;
        }
        public override bool IsInp(short axisNo)
        {
            return ((getIOstasus(axisNo) >> 13) & 1) == 1;
        }
        public override bool GetServo(short axisNo)
        {
            return ((getIOstasus(axisNo) >> 14) & 1) == 1;
        }
      

        /// <summary>
        ///     轴上电
        /// </summary>
        /// <param name="noId"></param>
        public override void ServoOn(short axisNo)
        {
            Motion._8164_set_servo(axisNo, 1);
        }

        /// <summary>
        ///     轴掉电
        /// </summary>
        /// <param name="noId"></param>
        public override void ServoOff(short axisNo)
        {
            Motion._8164_set_servo(axisNo, 0);
        }


        #endregion


        #region 编码器状态
        /// <summary>
        ///     获取轴当前位置
        /// </summary>
        /// <param name="axisNo">轴标识</param>
        /// <returns>当前位置</returns>
        public override double GetCurrentCommandPosition(short axisNo)
        {
            int position = 0;
            var ret = Motion._8164_get_command(axisNo, ref position);
            ThrowIfResultError(ret);
            return position;
        }
        public override double GetCurrentFeedbackPosition(short axisNo)
        {
            double position = 0;
            var ret = Motion._8164_get_position(axisNo, ref position);
            ThrowIfResultError(ret);
            return position;
        }
        public override double GetCurrentCommandSpeed(short axisNo)
        {
            double speed = 0;
            var ret = Motion._8164_get_current_speed(axisNo, ref speed);
            ThrowIfResultError(ret);
            return speed;
        }

        public override void SetCommandPosition(short axisNo, int position)
        {
            int ret = Motion._8164_set_command(axisNo, position);
            ThrowIfResultError(ret);
        }
        /// <summary>
        /// 当没有编码器时执行此命令
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="position"></param>
        public void SetFeedback(short axisNo)
        {
            int ret = Motion._8164_set_feedback_src(axisNo, 1);
            ThrowIfResultError(ret);
        }
        /// <summary>
        /// 当没有编码器时执行此命令
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="position"></param>
        public override void SetFeedbackPosition(short axisNo, int pos)
        {
            int ret = Motion._8164_set_position(axisNo, pos);
            ThrowIfResultError(ret);
        }
        #endregion

        #region 位置比较输出

        #endregion


        #region 软件限位
        /// <summary>
        ///     是否使用软限位
        /// </summary>
        /// <param name="axisNo"></param>
        public void EnbleSoftConfig(short axisNo, bool isEnble)
        {
            if (isEnble)
            {
                Motion._8164_enable_soft_limit(axisNo, 1);
            }
            else
            {
                Motion._8164_disable_soft_limit(axisNo);
            }
        }

        /// <summary>
        ///     限位配置
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="softLimitParams"></param>
        public void SetSoftELConfig(short axisNo, SoftLimitParams softLimitParams)
        {
            if (softLimitParams.Enable)
            {
                Motion._8164_set_soft_limit(axisNo, softLimitParams.SMelPosition, softLimitParams.SPelPosition);
            }
        }
        #endregion



        #region 板卡IO PCI-8164 无外部IO所以此功能无效
        /// <summary>
        /// 读取DIO的信号
        /// </summary>
        /// <param name="ioPoint"></param>
        /// <returns></returns>
        public override bool Read(IoPoint ioPoint)
        {

            return false;
        }
        /// <summary>
        /// 输出DO的信号
        /// </summary>
        /// <param name="ioPoint"></param>
        /// <param name="value"></param>
        public override void Write(IoPoint ioPoint, bool value)
        {

        }
        #endregion

        private void ThrowIfResultError(int errorCode, string function = null)
        {

            //throw new ApsException(string.Format("凌华运动控制卡功能 [{0}] 错误:{1}", function, errorCode));
        }

        private int getIOstasus(short axisNo)
        {
            ushort value = 0;
            var ret = Motion._8164_get_io_status(axisNo, ref value);
            ThrowIfResultError(ret);
            return value;
        }



        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }



        ~ApsController()
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
                Motion._8164_int_disable(Card);
            }
            catch (Exception)
            {
                //ignorl
            }
            _disposed = true;
        }

        #endregion


























    }
}