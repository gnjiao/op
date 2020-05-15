using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using CMotion.Interfaces;
using CMotion.Interfaces.Configuration;
using CMotion.Interfaces.IO;
using CMotion.Interfaces.Axis;
using System.Text;
using System.Windows.Forms;
using Advantech.Motion;
namespace CMotion.AdvantechAps
{
    /// <summary>
    ///     研华科技PCI-1245/PCI-1285运动控制卡控制器。修改于2018.12.10 Finley Jiang
    /// </summary>
    public class ApsController : IAxisControl, ISwitchController, IMotionController, IDisposable
    {
        private readonly List<IntPtr> m_Axises;
        private readonly List<IntPtr> m_Devices;
        private bool _disposed;
        private bool _isInitialized;     
        DEV_LIST[] CurAvailableDevs = new DEV_LIST[Motion.MAX_DEVICES];
        uint deviceCount = 0;
        public bool IsLoadXmlFile { get; private set; }
        private readonly object obj = new object();
        public ApsController()
        {
            //_cardNos = cardNos;
            m_Axises = new List<IntPtr>();
            m_Devices = new List<IntPtr>();
        }

        #region Implementation of INeedInitialization

        public override bool Initialize()
        {
            if (!_isInitialized)
            {
                InitializeCard();               
            }
            return true;
        }
        public override bool LoadParamFromFile(string xmlfilename)
        {
            UInt32 Result;
            string strTemp;
            //if (_isInitialized != true)  return false;
            for (int i = 0; i < m_Devices.Count; i++)
            {
                //Set all configurations for the device according to the loaded file
                Result = Motion.mAcm_DevLoadConfig(m_Devices[i], xmlfilename);
                if (Result != (uint)ErrorCode.SUCCESS)
                {
                    strTemp = "Load Config Failed With Error Code: [0x" + Convert.ToString(Result, 16) + "]";
                    ShowMessages(strTemp, Result);
                    return false;
                }
            }
            return true;
        }
        private void InitializeCard()
        {
            uint ret;
            string strTemp;
            IntPtr m_AxisHandle;
            IntPtr m_DeviceHandle;
            uint DeviceNum = 0;
            //if (VersionIsOk == false) return;
            ret = (uint)Motion.mAcm_GetAvailableDevs(CurAvailableDevs, Motion.MAX_DEVICES, ref deviceCount);
            if (ret != (int)ErrorCode.SUCCESS)
            {
                strTemp = "Get Device Numbers Failed With Error Code: [0x" + Convert.ToString(ret, 16) + "]";
                ShowMessages(strTemp, (uint)ret);
                return;
            }
            m_Axises.Clear();
            m_Devices.Clear();
            for (int i = 0; i < deviceCount; i++)
            {
                var AxesPerDev = new uint();
                m_DeviceHandle = IntPtr.Zero;
                DeviceNum = CurAvailableDevs[i].DeviceNum;
                ret = Motion.mAcm_DevOpen(DeviceNum, ref m_DeviceHandle);
                if (ret != (uint)ErrorCode.SUCCESS)
                {
                    strTemp = "Open Device Failed With Error Code: [0x" + Convert.ToString(ret, 16) + "]";
                    ShowMessages(strTemp, ret);
                    continue;
                }
                m_Devices.Add(m_DeviceHandle);
                ret = Motion.mAcm_GetU32Property(m_DeviceHandle, (uint)PropertyID.FT_DevAxesCount, ref AxesPerDev);
                if (ret != (uint)ErrorCode.SUCCESS)
                {
                    strTemp = "Get Axis Number Failed With Error Code: [0x" + Convert.ToString(ret, 16) + "]";
                    ShowMessages(strTemp, ret);
                    continue;
                }
                for (i = 0; i < AxesPerDev; i++)
                {
                    //Open every Axis and get the each Axis Handle
                    //And Initial property for each Axis
                    //Open Axis
                    m_AxisHandle = IntPtr.Zero;
                    ret = Motion.mAcm_AxOpen(m_DeviceHandle, (UInt16)i, ref m_AxisHandle);
                    if (ret != (uint)ErrorCode.SUCCESS)
                    {
                        strTemp = "Open Axis Failed With Error Code: [0x" + Convert.ToString(ret, 16) + "]";
                        ShowMessages(strTemp, ret);
                        continue;
                    }
                    m_Axises.Add(m_AxisHandle);
                }
            }
        }
        public void Dispose()
        {
            IntPtr Axis = IntPtr.Zero;
            IntPtr Device = IntPtr.Zero;
            for (int i=0;i< m_Devices.Count;i++)
            {
                for(var j=0;j<m_Axises.Count;j++)
                {
                    ushort usAxisState = 0;
                    Motion.mAcm_AxGetState(m_Axises[j], ref usAxisState);
                    if (usAxisState == (uint)AxisState.STA_AX_ERROR_STOP)
                    {
                        // Reset the axis' state. If the axis is in ErrorStop state, the state will be changed to Ready after calling this function
                        Motion.mAcm_AxResetError(m_Axises[j]);
                    }
                    //To command axis to decelerate to stop.
                    Motion.mAcm_AxStopDec(m_Axises[j]);
                    Axis = m_Axises[j];
                    Motion.mAcm_AxClose(ref Axis);
                }
                Device = m_Devices[i];
                Motion.mAcm_DevClose(ref Device);
                Device = IntPtr.Zero;
            }
            m_Devices.Clear();
            m_Axises.Clear();
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
        /// <summary>
        /// 设置比较事件
        /// </summary>
        /// <param name="axisNo">轴号</param>
        /// <param name="AxCmpSrc">比较源，0:理论位置，1:实际位置</param>
        /// <param name="AxCmpMethod">比较方法，0:>=,1:小于=,2:=</param>
        /// <param name="AxCmpPulseLogic">高低电平，0:低,1:高</param>
        /// <param name="AxCmpEnable">禁用启用比较功能，0:禁用，1:启用</param>
        public void SetCompareEvent(int axisNo,uint AxCmpSrc,uint AxCmpMethod, uint AxCmpPulseLogic ,uint AxCmpEnable)
        {
            UInt32 Result;
            String strTemp;
            UInt32[] AxEnableEvtArray = new UInt32[8];
            UInt32[] GpEnableEvt = new UInt32[8];
            AxEnableEvtArray[axisNo] |= (UInt32)EventType.EVT_AX_COMPARED;
            //Enable motion event
            Result = Motion.mAcm_EnableMotionEvent(m_Devices[0], AxEnableEvtArray, GpEnableEvt, 8, 3);
            if (Result != (uint)ErrorCode.SUCCESS)
            {
                strTemp = "EnableMotionEvent Filed With Error Code[0x" + Convert.ToString(Result, 16) + "]";
                ShowMessages(strTemp, Result);
                return;
            }
            Result = Motion.mAcm_SetU32Property(m_Axises[axisNo], (uint)PropertyID.CFG_AxCmpSrc, AxCmpSrc);
            if (Result != (uint)ErrorCode.SUCCESS)
            {
                strTemp = "Set Property-CFG_AxCmpSrc Failed With Error Code[0x" + Convert.ToString(Result, 16) + "]";
                ShowMessages(strTemp, Result);
                return;
            }
            Result = Motion.mAcm_SetU32Property(m_Axises[axisNo], (uint)PropertyID.CFG_AxCmpMethod, AxCmpMethod);
            if (Result != (uint)ErrorCode.SUCCESS)
            {
                strTemp = "Set Property-AxCmpMethod Failed With Error Code[0x" + Convert.ToString(Result, 16) + "]";
                ShowMessages(strTemp, Result);
                return;
            }
            Result = Motion.mAcm_SetU32Property(m_Axises[axisNo], (uint)PropertyID.CFG_AxCmpPulseLogic, AxCmpPulseLogic);
            if (Result != (uint)ErrorCode.SUCCESS)
            {
                strTemp = "Set Property-AxCmpMethod Failed With Error Code[0x" + Convert.ToString(Result, 16) + "]";
                ShowMessages(strTemp, Result);
                return;
            }
            Result = Motion.mAcm_SetU32Property(m_Axises[axisNo], (uint)PropertyID.CFG_AxCmpPulseWidth, 4);
            if (Result != (uint)ErrorCode.SUCCESS)
            {
                strTemp = "Set Property-AxCmpMethod Failed With Error Code[0x" + Convert.ToString(Result, 16) + "]";
                ShowMessages(strTemp, Result);
                return;
            }
            Result = Motion.mAcm_SetU32Property(m_Axises[axisNo], (uint)PropertyID.CFG_AxCmpEnable, AxCmpEnable);
            if (Result != (uint)ErrorCode.SUCCESS)
            {
                strTemp = "Set Property-AxCmpEnable Failed With Error Code[0x" + Convert.ToString(Result, 16) + "]";
                ShowMessages(strTemp, Result);
                return;
            }
        }

        public void SetComparePosition(int axisNo,List<double> CmpData)
        {
            UInt32 Result;
            int ArrayCount = 0;
            string strTemp;
            var i = 0;
            ArrayCount = CmpData.Count;
            double[] TableArray = new double[ArrayCount];
            foreach (var list in CmpData)
            {
                TableArray[i] = list;
                i++;
            }
            SetCompareEvent(axisNo,1,1,1,1);
            //Set compare data list for the specified axis
            Result = Motion.mAcm_AxSetCmpTable(m_Axises[axisNo], TableArray, ArrayCount);
            if (Result != (uint)ErrorCode.SUCCESS)
            {
                strTemp = "Set Compare Table Failed With Error Code[0x" + Convert.ToString(Result, 16) + "]";
                ShowMessages(strTemp, Result);
            }
        }
        #region 获取当前轴的 IO 信号

        public override bool IsReady(short axisNo)
        {
            return false;
        }
        /// <summary>
        ///     是否报警
        /// </summary>
        /// <returns></returns>
        public override bool IsAlm(short axisNo)
        {
            uint status=0;
            var ret = Motion.mAcm_AxGetMotionIO(m_Axises[axisNo], ref status);
            return ret == (uint)ErrorCode.SUCCESS ? ((status >> (int)APS_Define.ALM) & 1) == 1:false;
        }
        /// <summary>
        ///     是否到达正限位
        /// </summary>
        /// <returns></returns>
        public override bool IsPel(short axisNo)
        {
            uint status = 0;
            var ret = Motion.mAcm_AxGetMotionIO(m_Axises[axisNo], ref status);
            return ret == (uint)ErrorCode.SUCCESS ? ((status >> (int)APS_Define.PLMT) & 1) == 1 : false;
        }
        /// <summary>
        ///     是否到达正负位
        /// </summary>
        /// <returns></returns>
        public override bool IsMel(short axisNo)
        {
            uint status = 0;
            var ret = Motion.mAcm_AxGetMotionIO(m_Axises[axisNo], ref status);
            return ret == (uint)ErrorCode.SUCCESS ? ((status >> (int)APS_Define.NLMT) & 1) == 1 : false;
        }

        /// <summary>
        ///     是否在轴原点
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        public override bool IsOrg(short axisNo)
        {
            uint status = 0;
            var ret = Motion.mAcm_AxGetMotionIO(m_Axises[axisNo], ref status);
            return ret == (uint)ErrorCode.SUCCESS ? ((status >> (int)APS_Define.ORG) & 1) == 1 : false;
        }
        /// <summary>
        ///     是否急停
        /// </summary>
        /// <returns></returns>
        public override bool IsEMG(short axisNo)
        {
            uint status = 0;
            var ret = Motion.mAcm_AxGetMotionIO(m_Axises[axisNo], ref status);
            return ret == (uint)ErrorCode.SUCCESS ? ((status >> (int)APS_Define.EMG) & 1) == 1 : false;
        }
        /// <summary>
        /// 是否在轴Z相
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        public override bool IsEZ(short axisNo)
        {
            uint status = 0;
            var ret = Motion.mAcm_AxGetMotionIO(m_Axises[axisNo], ref status);
            return ret == (uint)ErrorCode.SUCCESS ? ((status >> (int)APS_Define.EZ) & 1) == 1 : false;
        }
        /// <summary>
        /// 是否在轴Z相
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        public override bool IsInp(short axisNo)
        {
            uint status = 0;
            var ret = Motion.mAcm_AxGetMotionIO(m_Axises[axisNo], ref status);
            return ret == (uint)ErrorCode.SUCCESS ? ((status >> (int)APS_Define.INP) & 1) == 1 : false;
        }
        /// <summary>
        ///     获取电机励磁状态。
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        public override bool GetServo(short axisNo)
        {
            uint status = 0;
            var ret = Motion.mAcm_AxGetMotionIO(m_Axises[axisNo], ref status);
            return ret == (uint)ErrorCode.SUCCESS ? ((status >> (int)APS_Define.SVON) & 1) == 1 : false;
        }
        #endregion
        /// <summary>
        ///     获取轴当前位置
        /// </summary>
        /// <param name="axisNo">轴标识</param>
        /// <returns>当前位置</returns>
        public override double GetCurrentCommandPosition(short axisNo)
        {
            var position = 0.0;
            var ret = Motion.mAcm_AxGetCmdPosition(m_Axises[axisNo], ref position);
            return ret == (uint)ErrorCode.SUCCESS ? (int)position : 0;
        }
        public override double GetCurrentFeedbackPosition(short axisNo)
        {
            var position = 0.0;
            var ret = Motion.mAcm_AxGetActualPosition(m_Axises[axisNo], ref position);
            return ret == (uint)ErrorCode.SUCCESS ? (int)position : 0;
        }
        public override double GetCurrentCommandSpeed(short axisNo)
        {
            var speed = 0.0;
            var ret = Motion.mAcm_AxGetCmdVelocity(m_Axises[axisNo], ref speed);
            return ret == (uint)ErrorCode.SUCCESS ? (int)speed : 0;
        }
        public int GetCurrentFeedbackSpeed(int axisNo)
        {
            var speed = 0.0;
            var ret = Motion.mAcm_AxGetActVelocity(m_Axises[axisNo], ref speed);
            return ret == (uint)ErrorCode.SUCCESS ? (int)speed : 0;
        }
        /// <summary>
        ///     设置指令位置计数器计数值
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="position"></param>
        public override void SetCommandPosition(short axisNo, int position)
        {
            var ret = Motion.mAcm_AxSetCmdPosition(m_Axises[axisNo], position);
        }
        /// <summary>
        ///     设置指令位置计数器计数值
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="position"></param>
        public override void SetFeedbackPosition(short axisNo, int position)
        {
            var ret = Motion.mAcm_AxSetActualPosition(m_Axises[axisNo], position);
        }
        public void CleanError(int axisNo)
        {
            var ret = Motion.mAcm_AxResetError(m_Axises[axisNo]);
        }
        public int GetState(int axisNo)
        {
            ushort AxState=0;
            var ret = Motion.mAcm_AxGetState(m_Axises[axisNo], ref AxState);
            if (ret == (uint)ErrorCode.SUCCESS) return AxState;
            return 0;
        }
        /// <summary>
        ///     轴上电
        /// </summary>
        /// <param name="noId"></param>
        public override void ServoOn(short axisNo)
        {
            Motion.mAcm_AxSetSvOn(m_Axises[axisNo], 1);
        }
        /// <summary>
        ///     轴掉电
        /// </summary>
        /// <param name="noId"></param>
        public override void ServoOff(short axisNo)
        {
            Motion.mAcm_AxSetSvOn(m_Axises[axisNo], 0);
        }
        public void ServoOn()
        {
            m_Axises.ForEach(axisNo =>
            {
                var result = Motion.mAcm_AxSetSvOn(axisNo, 1);
                var innerMsg = ((ErrorCode)result).ToString();
                if (innerMsg != "No Error")
                    MessageBox.Show(string.Format("伺服打开功能错误:{0}", innerMsg));
            });
        }

        public void ServoOff()
        {
            m_Axises.ForEach(axisNo =>
            {
                var result = Motion.mAcm_AxSetSvOn(axisNo, 0);
                var innerMsg =((ErrorCode)result).ToString();
                if (innerMsg != "No Error")
                    MessageBox.Show(string.Format("伺服关闭功能错误:{0}", innerMsg));
            });
        }

        #region Error Code

        #endregion

        /// <summary>
        ///     单轴相对运动
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="pulseNum"></param>
        /// <param name="velocityCurveParams"></param>
        /// <returns></returns>
        public override void RelativeMove(short axisNo, double position, VelocityCurve velocityCurveParams)
        {
            //设置速度
            SetAxisVelocity(axisNo, velocityCurveParams);
            //启动运动
            Motion.mAcm_AxMoveRel(m_Axises[axisNo], position);
        }

        /// <summary>
        ///     单轴绝对运动
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="position"></param>
        /// <param name="velocityCurveParams"></param>
        public override void AbsoluteMove(short axisNo, double position, VelocityCurve velocityCurveParams)
        {
            //设置速度
            SetAxisVelocity(axisNo, velocityCurveParams);
            //启动运动
            Motion.mAcm_AxMoveAbs(m_Axises[axisNo], position);
        }
        /// <summary>
        ///  设置轴速度
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="pulseNum"></param>
        /// <param name="velocityCurveParams"></param>
        public void SetAxisVelocity(int axisNo,VelocityCurve velocityCurveParams)
        {
            var ret = Motion.mAcm_SetF64Property(m_Axises[axisNo], (uint)PropertyID.PAR_AxVelLow, velocityCurveParams.Strvel);
            if (ret != (uint)ErrorCode.SUCCESS) return;
            ret = Motion.mAcm_SetF64Property(m_Axises[axisNo], (uint)PropertyID.PAR_AxVelHigh, velocityCurveParams.Maxvel);
            if (ret != (uint)ErrorCode.SUCCESS) return;
            ret = Motion.mAcm_SetF64Property(m_Axises[axisNo], (uint)PropertyID.PAR_AxAcc, velocityCurveParams.Tacc);
            if (ret != (uint)ErrorCode.SUCCESS) return;
            ret = Motion.mAcm_SetF64Property(m_Axises[axisNo], (uint)PropertyID.PAR_AxDec, velocityCurveParams.Tdec);
            if (ret != (uint)ErrorCode.SUCCESS) return;
            ret = Motion.mAcm_SetF64Property(m_Axises[axisNo], (uint)PropertyID.PAR_AxJerk, (int)velocityCurveParams.VelocityCurveType);
            if (ret != (uint)ErrorCode.SUCCESS) return;
        }
        /// <summary>
        ///  设置轴速度
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="pulseNum"></param>
        /// <param name="velocityCurveParams"></param>
        public void SetAxisHomeVelocity(int axisNo, VelocityCurve velocityCurveParams)
        {
            var ret = Motion.mAcm_SetF64Property(m_Axises[axisNo], (uint)PropertyID.PAR_AxVelLow, velocityCurveParams.Strvel);
            if (ret != (uint)ErrorCode.SUCCESS) return;
            ret = Motion.mAcm_SetF64Property(m_Axises[axisNo], (uint)PropertyID.PAR_AxVelHigh, velocityCurveParams.Maxvel);
            if (ret != (uint)ErrorCode.SUCCESS) return;
            ret = Motion.mAcm_SetF64Property(m_Axises[axisNo], (uint)PropertyID.PAR_AxAcc, velocityCurveParams.Tacc);
            if (ret != (uint)ErrorCode.SUCCESS) return;
            ret = Motion.mAcm_SetF64Property(m_Axises[axisNo], (uint)PropertyID.PAR_AxDec, velocityCurveParams.Tdec);
            if (ret != (uint)ErrorCode.SUCCESS) return;
            ret = Motion.mAcm_SetF64Property(m_Axises[axisNo], (uint)PropertyID.PAR_AxJerk, (int)velocityCurveParams.VelocityCurveType);
            if (ret != (uint)ErrorCode.SUCCESS) return;
        }
        public uint SetCmpTable(int axisNo,double[] TableArray, int ArrayCount)
        {
            return Motion.mAcm_AxSetCmpTable(m_Axises[axisNo], TableArray, ArrayCount);
        }
        /// <summary>
        ///     连续运动
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="moveDirection"></param>
        /// <param name="velocityCurveParams"></param>
        /// <returns></returns>
        public override void ContinuousMove(short axisNo, MoveDirection moveDirection, VelocityCurve velocityCurveParams)
        {
            SetAxisVelocity(axisNo, velocityCurveParams);
            Motion.mAcm_AxMoveVel(m_Axises[axisNo], (ushort)moveDirection);
        }
        /// <summary>
        ///     立即停止
        /// </summary>
        /// <param name="axisNo"></param>
        public override void ImmediateStop(short axisNo)
        {
            var reult = Motion.mAcm_AxStopEmg(m_Axises[axisNo]);
           
        }

        /// <summary>
        ///     减速停止指定机构轴脉冲输出
        /// </summary>
        /// <param name="axisNo"></param>
        public override void DecelStop(short axisNo)
        {
            var reult = Motion.mAcm_AxStopDec(m_Axises[axisNo]);        
        }

        /// <summary>
        ///     是否停止移动
        /// </summary>
        /// <returns></returns>
        public bool IsDown(int axisNo, bool hasExtEncode = false)
        {
            ushort status = 0;
            var ret = Motion.mAcm_AxGetState(m_Axises[axisNo], ref status);
            if (ret != (uint)ErrorCode.SUCCESS) return false;
            //判断是否停止
            if (status == (ushort)APS_StateStatus.STA_Stopping)
                return false;
            //判断是否正常停止
            if (status == (ushort)APS_StateStatus.STA_AxErrorStop)
                return false;
            return true;
        }

        /// <summary>
        ///     检测指定轴的运动状态
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="hasExtEncode">是否有编码器接入(步进电机无外部编码器)</param>
        /// <remarks>判断INP鑫海</remarks>
        public int CheckDone(int axisNo, double timeoutLimit, bool hasExtEncode = false)
        {
            ushort status = 0;
            var strtime = new Stopwatch();
            strtime.Start();

            do
            {
                //判断是否正常停止
                Motion.mAcm_AxGetState(m_Axises[axisNo], ref status);
                if (status == (int)APS_StateStatus.STA_AxErrorStop)
                    return -1;
                if (status == (int)APS_StateStatus.STA_Stopping)
                    break;
                //判断INP鑫海
                if (hasExtEncode)
                {
                    uint status1 = 0;
                    Motion.mAcm_AxGetMotionIO(m_Axises[axisNo], ref status1);
                    if (((status >> (int)APS_Define.INP) & 1) == 1)
                        return -2;
                }
                //检查是否超时
                strtime.Stop();
                if (strtime.ElapsedMilliseconds/1000.0 > timeoutLimit)
                {
                    Motion.mAcm_AxStopEmg(m_Axises[axisNo]);
                    return -3;
                }
                strtime.Start();
                //延时
                Thread.Sleep(20);
            } while (true);
            return 0;
        }

        /// <summary>
        /// 两轴做插补相对移动
        /// </summary>
        /// <param name="axisNo">轴ID</param>
        /// <param name="position1">坐标1</param>
        /// <param name="position2">坐标2</param>
        /// <param name="velocityCurveParams">速度参数</param>
        public void MoveLine2Relative(int axisNo, double position1, double position2, VelocityCurve velocityCurveParams)
        {
            var pos = new double[2];
            uint pArrayElements = 2; 
            pos[0] = position1;
            pos[1] = position2;
            //设置速度
            SetAxisVelocity(axisNo, velocityCurveParams);
            //启动运动
            Motion.mAcm_GpMoveDirectRel(m_Axises[axisNo], pos,ref pArrayElements);
        }
        /// <summary>
        ///     两轴直线插补绝对移动
        /// </summary>
        /// <param name="axisNo">轴ID</param>
        /// <param name="position1">坐标1</param>
        /// <param name="position2">坐标2</param>
        /// <param name="velocityCurveParams">速度参数</param>
        public void MoveLine2Absolute(int axisNo, double position1, double position2, VelocityCurve velocityCurveParams)
        {
            var pos = new double[2];
            uint pArrayElements = 2;
            pos[0] = position1;
            pos[1] = position2;
            //设置速度
            SetAxisVelocity(axisNo, velocityCurveParams);
            //启动运动
            Motion.mAcm_GpMoveDirectAbs(m_Axises[axisNo], pos, ref pArrayElements);
        }
        /// <summary>
        /// 两轴圆弧插补相对移动（方向）
        /// </summary>
        /// <param name="axisNo">轴ID</param>
        /// <param name="CX">圆心坐标X</param>
        /// <param name="CY">圆心坐标Y</param>
        /// <param name="EX">终点坐标X</param>
        /// <param name="EY">终点坐标Y</param>
        /// <param name="Direction">0:DIR_CW,1:DIR_CCW</param>
        /// <param name="velocityCurveParams">速度参数</param>
        public void MoveArc2Relative(int axisNo, double CX, double CY,double EX,double EY, short Direction, VelocityCurve velocityCurveParams)
        {
            var Cpos = new double[2];
            var Epos = new double[2];
            uint pArrayElements = 2;
            Cpos[0] = CX;
            Cpos[1] = CY;
            Epos[0] = EX;
            Epos[1] = EY;
            //设置速度
            SetAxisVelocity(axisNo, velocityCurveParams);
            //启动运动
            Motion.mAcm_GpMoveCircularRel(m_Axises[axisNo], Cpos, Epos, ref pArrayElements, Direction);
        }

        /// <summary>
        /// 两轴圆弧插补绝对移动（方向）
        /// </summary>
        /// <param name="axisNo">轴ID</param>
        /// <param name="CX">圆心坐标X</param>
        /// <param name="CY">圆心坐标Y</param>
        /// <param name="EX">终点坐标X</param>
        /// <param name="EY">终点坐标Y</param>
        /// <param name="Direction">0:DIR_CW,1:DIR_CCW</param>
        /// <param name="velocityCurveParams">速度参数</param>
        public void MoveArc2Absolute(int axisNo, double CX, double CY, double EX, double EY, short Direction, VelocityCurve velocityCurveParams)
        {
            var Cpos = new double[2];
            var Epos = new double[2];
            uint pArrayElements = 2;
            Cpos[0] = CX;
            Cpos[1] = CY;
            Epos[0] = EX;
            Epos[1] = EY;
            //设置速度
            SetAxisVelocity(axisNo, velocityCurveParams);
            //启动运动
            Motion.mAcm_GpMoveCircularAbs(m_Axises[axisNo], Cpos, Epos, ref pArrayElements, Direction);
        }
        /// <summary>
        ///     回零
        /// </summary>
        /// <param name="axisNo"></param>
        /// <param name="homeMode">回原点方式</param>
        /// <param name="DirMode">0:DIR_CW,1:DIR_CCW</param>
         public override void BackHome(short axisNo, short HomeMode, short Org, short Ez, short EZcount, short erc
          , VelocityCurve velocityCurveParams, double ORGOffset,uint DirMode)      
        {
            //设置速度
            SetAxisHomeVelocity(axisNo, velocityCurveParams);
            Motion.mAcm_AxHome(m_Axises[axisNo], (uint)HomeMode, DirMode);
        }
        public bool IsHoming(int axisNo)
        {
            ushort status = 0;
            Motion.mAcm_AxGetState(m_Axises[axisNo], ref status);
            return status == (int)APS_StateStatus.STA_AxHoming;
        }
        /// <summary>
        ///     检查回零是否完成
        /// </summary>
        /// <param name="axisNo"></param>
        /// <returns></returns>
        public override int CheckHomeDone(short axisNo, double timeoutLimit)
        {
            ushort status = 0;
            var strtime = new Stopwatch();
            strtime.Start();
            lock(obj)
            {
                do
                {
                    //判断是否正常停止
                    Motion.mAcm_AxGetState(m_Axises[axisNo], ref status);
                    if (status == (int)APS_StateStatus.STA_AxErrorStop)
                        return -1;
                    if ((status != (int)APS_StateStatus.STA_AxHoming)
                        && (status == (int)APS_StateStatus.STA_AxReady))
                        return 0;
                    //检查是否超时
                    strtime.Stop();
                    if (strtime.ElapsedMilliseconds / 1000.0 > timeoutLimit)
                    {
                        Motion.mAcm_AxStopEmg(m_Axises[axisNo]);
                        return -1;
                    }
                    strtime.Start();
                    //延时
                    Thread.Sleep(10);
                } while (true);
            }
        }
        private void ShowMessages(string DetailMessage, uint errorCode)
        {
            StringBuilder ErrorMsg = new StringBuilder("", 100);
            //Get the error message according to error code returned from API
            Boolean res = Motion.mAcm_GetErrorMessage(errorCode, ErrorMsg, 100);
            string ErrorMessage = "";
            if (res)
                ErrorMessage = ErrorMsg.ToString();
            MessageBox.Show(DetailMessage + "\r\nError Message:" + ErrorMessage, "Motion DAQ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #region Implementation of IDisposable

        /// <summary>
        ///     线性差补方式运动。
        /// </summary>
        /// <param name="axisNo1"></param>
        /// <param name="axisNo2"></param>
        /// <param name="pulseNum1"></param>
        /// <param name="pulseNum2"></param>
        /// <param name="velocityCurve"></param>
        public void MoveLine(int axisNo1, int axisNo2, int pulseNum1, int pulseNum2, VelocityCurve velocityCurve)
        {
            throw new NotImplementedException();
        }

        ~ApsController()
        {
            Dispose(false);
        }

        /// <summary>
        ///     Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;
            if (disposing)
            {
            }
            try
            {
                //APS168.APS_close();
            }
            catch (Exception)
            {
                //ignorl
            }
            _isInitialized = false;
            _disposed = true;
        }

        #endregion

        #region Implementation of ISwitchController

        public override bool Read(IoPoint ioPoint)
        {
            byte value = 0;
            uint ret = 0;
            if ((ioPoint.IoMode & IoModes.Responser) != 0)
            {
                ret = Motion.mAcm_AxDoGetBit(m_Axises[ioPoint.BoardNo], (ushort)ioPoint.PortNo, ref value);
            }
            else if ((ioPoint.IoMode & IoModes.Senser) != 0)
            {
                ret = Motion.mAcm_AxDiGetBit(m_Axises[ioPoint.BoardNo], (ushort)ioPoint.PortNo, ref value);
            }
            return ret != (uint)ErrorCode.SUCCESS ? false : value > 0 ? true : false;
        }

        public override void Write(IoPoint ioPoint, bool value)
        {
            Motion.mAcm_AxDoSetBit(m_Axises[ioPoint.BoardNo], (ushort)ioPoint.PortNo, (byte)(value ? 1 : 0));
        }

        #endregion
    }
}