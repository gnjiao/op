using System.Collections.Generic;
using System.Enginee;
using CMotion.Interfaces;
using System.Threading;
using CMotion.Interfaces.IO;
using YAMAHA;
using System.Diagnostics;
using InterLocking;
using System.ToolKit;
namespace desaySV
{
    /// <summary>
    /// 
    /// </summary>
    public class Station1 : StationPart
    {
        private Station1Alarm m_Alarm;
        public AlarmManage mAlarmManage { get; set; }
        public Station1(External ExternalSign, StationInitialize stationIni, StationOperate stationOpe)
            : base(ExternalSign, stationIni, stationOpe, typeof(Station1))
        {
            InterlockingClass = new InterlockingClass();
        }
        /// <summary>
        /// 相机左前X轴dd
        /// </summary>
        public ApsAxis LFXaxisServo { get; set; }
        /// <summary>
        /// 相机左前Y轴
        /// </summary>
        public ApsAxis LFYaxisServo { get; set; }
        /// <summary>
        /// 相机左后X轴
        /// </summary>
        public ApsAxis LRXaxisServo { get; set; }
        /// <summary>
        /// 相机左后Y轴
        /// </summary>
        public ApsAxis LRYaxisServo { get; set; }
        /// <summary>
        /// 相机右Y轴
        /// </summary>
        public ApsAxis RYaxisServo { get; set; }
        /// <summary>
        /// 相机X轴
        /// </summary>
        public ApsAxis XaxisServo { get; set; }
        /// <summary>
        /// 治具Y轴
        /// </summary>
        public ApsAxis YaxisServo { get; set; }
        /// <summary>
        /// 升降Z轴
        /// </summary>
        public ApsAxis ZaxisServo { get; set; }
        /// <summary>
        /// 雅马哈
        /// </summary>
        public Yamaha YAMAHA { get; set; }

        public Locking mLocking { get; set; }

        public InterlockingClass InterlockingClass { get; set; }
        /// <summary>
        /// 切换气缸
        /// </summary>
        public DoubleCylinder SwichCylinder { get; set; }
        /// <summary>
        /// 左负压
        /// </summary>
        public DoubleCylinder LeftVacuum { get; set; }
        /// <summary>
        /// 右负压
        /// </summary>
        public DoubleCylinder RightVacuum { get; set; }
        private Stopwatch watchinit = new Stopwatch();
        private Stopwatch watchRun = new Stopwatch();
        public override void Running(RunningModes runningMode)
        {
            while (true)
            {
                Thread.Sleep(10);
                #region  自动流程
                if (stationOperate.Running)
                {
                    switch (stationOperate.step)
                    {
                        case 0://判断mes选择  
                            switch (ProductConfig.Instance.switchCom)
                            {
                                case SwitchCom.None:
                                    stationOperate.step = 10;
                                    break;
                                case SwitchCom.MES:
                                    stationOperate.step = 10;
                                    break;
                                case SwitchCom.Interlocking:
                                    stationOperate.step = 10;
                                    break;
                            }
                            break;
                        case 10://请扫描产品SN  
                            using (FormProductCode frm = new FormProductCode("请扫描产品SN!"))
                            {
                                Global.ProductSn = frm.ProductSn;
                                stationOperate.step = 20;
                            }
                            break;
                        case 20://确认扫描SN  
                            if (!string.IsNullOrEmpty(Global.ProductSn))
                            {
                                switch (ProductConfig.Instance.switchCom)
                                {
                                    case SwitchCom.None:
                                        stationOperate.step = 30;
                                        break;
                                    case SwitchCom.MES:
                                        //if (InterlockingClass.MES_MoveIn_64(Global.ProductSn, mLocking.mes) == 0)
                                        //{
                                        //    stationOperate.step = 30;
                                        //}
                                        //else
                                        //{
                                        //    m_Alarm = Station1Alarm.互锁失败;
                                        //    stationOperate.step = 10;
                                        //}
                                        break;
                                    case SwitchCom.Interlocking:
                                        //if (InterlockingClass.SV_Interlocking(Global.ProductSn, mLocking.mes) == 0)
                                        //{
                                        //    stationOperate.step = 30;
                                        //}
                                        //else
                                        //{
                                        //    m_Alarm = Station1Alarm.互锁失败;
                                        //    stationOperate.step = 10;
                                        //}
                                        break;
                                }
                            }
                            else
                            {
                                stationOperate.step = 10;
                            }
                            break;
                        case 30://按钮启动  
                            watchRun.Restart();
                            stationOperate.step = 40;
                            break;
                        case 40://产品是否放好 是否在待料位置

                            stationOperate.step = 50;
                            break;
                        case 50://产品定位


                            stationOperate.step = 60;
                            break;
                        case 60://定位OK，判断左边开始还是右边开始（右边开始）
                            if (IoPoints.S1.Value)
                                stationOperate.step = 70;

                            break;
                        case 500://相机到对应拍照位置，气缸到对应位置

                            break;
                        case 510://相机到位，气缸到位，切换完成

                            break;
                        case 70://Y轴到取料位置

                            stationOperate.step = 80;
                            break;
                        case 80://Y轴到位Z轴上升到拍照位置

                            stationOperate.step = 90;
                            break;
                        case 90://Z轴到位，位置切换完成，拍照

                            stationOperate.step = 100;
                            break;
                        case 100://拍照完成结果计算

                            stationOperate.step = 110;
                            break;
                        case 110://负压开启，机械手取屏，Z轴下降

                            stationOperate.step = 120;
                            break;
                        case 120://机械手取屏完成,机械手到拍屏位置

                            stationOperate.step = 130;
                            break;
                        case 130://机械手到位拍照

                            stationOperate.step = 140;
                            break;
                        case 140://拍照后数据处理

                            stationOperate.step = 150;
                            break;
                        case 150://机械手补正（XYR）

                            stationOperate.step = 160;
                            break;
                        case 160://补正后拍照

                            stationOperate.step = 170;
                            break;
                        case 170://拍照后数据处理

                            stationOperate.step = 180;
                            break;
                        case 180://数据OK，Z轴上升到贴合位置

                            stationOperate.step = 190;
                            break;
                        case 190://Z轴完成,

                            stationOperate.step = 200;
                            break;
                        case 200://贴合延时,

                            stationOperate.step = 210;
                            break;
                        case 210://Z轴下降到出料位置，机械手到安全高度

                            stationOperate.step = 220;
                            break;
                        case 220://机械手到位，机械手退回安全位置

                            stationOperate.step = 230;
                            break;
                        case 230://Z轴到位，机械手到待机位置

                            stationOperate.step = 240;
                            break;
                        case 240://机械手到位，判断是否右边贴合
                            stationOperate.step = 60; //换另一边
                            stationOperate.step = 250;//贴合完成
                            break;
                        case 250://Y轴到待料位置，切换到右边，生成报告
                            switch (ProductConfig.Instance.switchCom)
                            {
                                case SwitchCom.None:
                                    stationOperate.step = 270;
                                    break;
                                case SwitchCom.MES:
                                    //InterlockingClass.MES_MoveStd_64(Global.ProductSn, "Passed", mLocking.mes);
                                    stationOperate.step = 260;
                                    break;
                                case SwitchCom.Interlocking:
                                    stationOperate.step = 260;
                                    break;
                            }
                            break;

                        case 260://生成报告
                            if (mLocking.Test_WriteMesTxtAndCsvFile())
                            {
                                stationOperate.step = 270;
                            }
                            else
                            {
                                m_Alarm = Station1Alarm.报告生成失败;
                                //stationOperate.step = 270;
                            }
                            break;
                        case 270://Y轴到位，切换完成
                            watchRun.Stop();
                            stationOperate.step = 280;
                            break;
                        case 280://判断产品是否取走
                            stationOperate.step = 290;
                            break;
                        default:
                            stationOperate.RunningSign = false;
                            stationOperate.step = 0;
                            break;
                    }
                    Global.Beattime = watchRun.ElapsedMilliseconds * 1.0 / 1000;
                }
                #endregion

                #region 初始化流程
                if (stationInitialize.Running)
                {
                    if (watchinit.ElapsedTicks > 10000) { m_Alarm = Station1Alarm.初始化故障; }
                    switch (stationInitialize.Flow)
                    {
                        case 0:
                            watchinit.Restart();
                            stationInitialize.InitializeDone = false;
                            stationOperate.RunningSign = false;
                            stationOperate.step = 0;
                            stationInitialize.Flow = 10;
                            break;
                        case 10:
                            ZaxisServo.BackHome();
                            YaxisServo.BackHome();
                            stationInitialize.Flow = 20;
                            break;
                        case 20:
                            if (ZaxisServo.CheckHomeDone(500) == 0 && YaxisServo.CheckHomeDone(500) == 0)
                            {
                                stationInitialize.Flow = 30;
                            }
                            break;
                        case 30://判断机器人是否安全
                            if (YAMAHA.CurrentPosR <= 50 && YAMAHA.CurrentPosZ <= 10)
                            {
                                stationInitialize.Flow = 70; //机器人安全
                            }
                            else
                            {
                                stationInitialize.Flow = 40; //机器人不安全
                            }
                            break;
                        case 40://判断机器人是否安全

                            break;
                        case 70: //相机轴回原点
                            LFXaxisServo.BackHome();
                            LFYaxisServo.BackHome();
                            XaxisServo.BackHome();
                            RYaxisServo.BackHome();
                            LRYaxisServo.BackHome();
                            LRXaxisServo.BackHome();
                            stationInitialize.Flow = 80;
                            break;
                        case 80:
                            if (LFXaxisServo.CheckHomeDone(500) == 0 && LFYaxisServo.CheckHomeDone(500) == 0
                                && LRXaxisServo.CheckHomeDone(500) == 0 && LRYaxisServo.CheckHomeDone(500) == 0
                                && XaxisServo.CheckHomeDone(500) == 0 && RYaxisServo.CheckHomeDone(500) == 0)
                            {
                                stationInitialize.Flow = 90;
                            }
                            break;
                        case 90:
                            stationInitialize.InitializeDone = true;
                            break;
                        default:
                            break;
                    }
                }
                #endregion


                //故障清除
                if (AlarmReset.AlarmReset)
                {
                    m_Alarm = Station1Alarm.无消息;
                }
            }
        }
        /// <summary>
        /// 拍照模拟(Dk)
        /// </summary>
        public ArcParam<double> GetDkMark1(double x, double y)
        {
            ArcParam<double> arcParam = new ArcParam<double>();
            arcParam.X = 100 * Config.Instance.PhotoConverMM[0].XX;
            arcParam.Y = 100 * Config.Instance.PhotoConverMM[0].YY;
            arcParam.R = 10;
            return arcParam;
        }
        /// <summary>
        /// 拍照模拟(Dk)
        /// </summary>
        public ArcParam<double> GetDkMark2(double x, double y)
        {
            ArcParam<double> arcParam = new ArcParam<double>();
            arcParam.X = 100 * Config.Instance.PhotoConverMM[1].XX;
            arcParam.Y = 100 * Config.Instance.PhotoConverMM[1].YY;
            arcParam.R = 10;
            return arcParam;
        }
        /// <summary>
        /// 拍照模拟(Dk)
        /// </summary>
        public ArcParam<double> GetDkMark3(double x, double y)
        {
            ArcParam<double> arcParam = new ArcParam<double>();
            arcParam.X = 100 * Config.Instance.PhotoConverMM[2].XX;
            arcParam.Y = 100 * Config.Instance.PhotoConverMM[2].YY;
            arcParam.R = 10;
            return arcParam;
        }
        /// <summary>
        /// 拍照模拟(Dk)
        /// </summary>
        public ArcParam<double> GetDkMark4(double x, double y)
        {
            ArcParam<double> arcParam = new ArcParam<double>();
            arcParam.X = 100 * Config.Instance.PhotoConverMM[3].XX;
            arcParam.Y = 100 * Config.Instance.PhotoConverMM[3].YY;
            arcParam.R = 10;
            return arcParam;
        }
        /// <summary>
        /// 拍照模拟(屏幕)
        /// </summary>
        public ArcParam<double> GetPmMark1(double x, double y)
        {
            ArcParam<double> arcParam = new ArcParam<double>();
            arcParam.X = 100 * Config.Instance.PhotoConverMM[0].XX;
            arcParam.Y = 100 * Config.Instance.PhotoConverMM[0].YY;
            arcParam.R = 10;
            return arcParam;
        }
        /// <summary>
        /// 拍照模拟(屏幕)
        /// </summary>
        public ArcParam<double> GetPmMark2(double x, double y)
        {
            ArcParam<double> arcParam = new ArcParam<double>();
            arcParam.X = 100 * Config.Instance.PhotoConverMM[1].XX;
            arcParam.Y = 100 * Config.Instance.PhotoConverMM[1].YY;
            arcParam.R = 10;
            return arcParam;
        }
        /// <summary>
        /// 拍照模拟(屏幕)
        /// </summary>
        public ArcParam<double> GetPmMark3(double x, double y)
        {
            ArcParam<double> arcParam = new ArcParam<double>();
            arcParam.X = 100 * Config.Instance.PhotoConverMM[2].XX;
            arcParam.Y = 100 * Config.Instance.PhotoConverMM[2].YY;
            arcParam.R = 10;
            return arcParam;
        }
        /// <summary>
        /// 拍照模拟(屏幕)
        /// </summary>
        public ArcParam<double> GetPmMark4(double x, double y)
        {
            ArcParam<double> arcParam = new ArcParam<double>();
            arcParam.X = 100 * Config.Instance.PhotoConverMM[3].XX;
            arcParam.Y = 100 * Config.Instance.PhotoConverMM[3].YY;
            arcParam.R = 10;
            return arcParam;
        }
        /// <summary>
        /// 拍照模拟(标定板)
        /// </summary>
        public ArcParam<double> getMarkSenter1(double x, double y)
        {
            ArcParam<double> arcParam = new ArcParam<double>();
            arcParam.X = 100 * Config.Instance.PhotoConverMM[0].XX;
            arcParam.Y = 100 * Config.Instance.PhotoConverMM[0].YY;
            arcParam.R = 10;
            return arcParam;
        }
        /// <summary>
        /// 拍照模拟(标定板)
        /// </summary>
        public ArcParam<double> getMarkSenter2(double x, double y)
        {
            ArcParam<double> arcParam = new ArcParam<double>();
            arcParam.X = 100 * Config.Instance.PhotoConverMM[1].XX;
            arcParam.Y = 100 * Config.Instance.PhotoConverMM[1].YY;
            arcParam.R = 10;
            return arcParam;
        }
        /// <summary>
        /// 拍照模拟(标定板)
        /// </summary>
        public ArcParam<double> getMarkSenter3(double x, double y)
        {
            ArcParam<double> arcParam = new ArcParam<double>();
            arcParam.X = 100 * Config.Instance.PhotoConverMM[2].XX;
            arcParam.Y = 100 * Config.Instance.PhotoConverMM[2].YY;
            arcParam.R = 10;
            return arcParam;
        }
        /// <summary>
        /// 拍照模拟(标定板)
        /// </summary>
        public ArcParam<double> getMarkSenter4(double x, double y)
        {
            ArcParam<double> arcParam = new ArcParam<double>();
            arcParam.X = 100 * Config.Instance.PhotoConverMM[3].XX;
            arcParam.Y = 100 * Config.Instance.PhotoConverMM[3].YY;
            arcParam.R = 10;
            return arcParam;
        }


        /// <summary>
        /// 气缸状态集合
        /// </summary>
        protected override IList<ICylinderStatusJugger> cylinderStatus()
        {
            var list = new List<ICylinderStatusJugger>();
            list.Add(SwichCylinder);
            list.Add(LeftVacuum);
            list.Add(RightVacuum);
            return list;
        }
        /// <summary>
        /// 流程报警集合
        /// </summary>
        protected override IList<Alarm> alarms()
        {
            var Alarms = new List<Alarm>();
            Alarms.AddRange(XaxisServo.Alarms);
            Alarms.AddRange(YaxisServo.Alarms);
            Alarms.AddRange(ZaxisServo.Alarms);           
            Alarms.AddRange(LRXaxisServo.Alarms);
            Alarms.AddRange(LRYaxisServo.Alarms);
            Alarms.AddRange(LFXaxisServo.Alarms);
            Alarms.AddRange(LFYaxisServo.Alarms);
            Alarms.AddRange(RYaxisServo.Alarms);
            Alarms.AddRange(SwichCylinder.Alarms);
            Alarms.AddRange(LeftVacuum.Alarms);
            Alarms.AddRange(RightVacuum.Alarms);
            Alarms.Add(new Alarm(() => m_Alarm == Station1Alarm.初始化故障)
            {
                AlarmKey= AlarmKeys.Alarm_2,
                AlarmLevel = AlarmLevels.Error,
                Name = Station1Alarm.初始化故障.ToString()
            });
            Alarms.Add(new Alarm(() => YAMAHA.Alarm)
            {
                AlarmKey = AlarmKeys.Alarm_3,
                AlarmLevel = AlarmLevels.Error,
                Name = Station1Alarm.YAMAHA报警.ToString()
            });
            Alarms.Add(new Alarm(() => m_Alarm == Station1Alarm.当前产品不是当前系列)
            {
                AlarmKey = AlarmKeys.Alarm_4,
                AlarmLevel = AlarmLevels.Warrning,
                Name = Station1Alarm.当前产品不是当前系列.ToString()
            });
            Alarms.Add(new Alarm(() => m_Alarm == Station1Alarm.产品尺寸超出设定范围)
            {
                AlarmKey = AlarmKeys.Alarm_5,
                AlarmLevel = AlarmLevels.Error,
                Name = Station1Alarm.产品尺寸超出设定范围.ToString()
            });
            Alarms.Add(new Alarm(() => m_Alarm == Station1Alarm.报告生成失败)
            {
                AlarmKey = AlarmKeys.Alarm_6,
                AlarmLevel = AlarmLevels.Error,
                Name = Station1Alarm.报告生成失败.ToString()
            });
            Alarms.Add(new Alarm(() => m_Alarm == Station1Alarm.设备未标定)
            {
                AlarmKey = AlarmKeys.Alarm_7,
                AlarmLevel = AlarmLevels.Error,
                Name = Station1Alarm.设备未标定.ToString()
            });
            Alarms.Add(new Alarm(() => m_Alarm == Station1Alarm.机器人通信超时)
            {
                AlarmKey = AlarmKeys.Alarm_8,
                AlarmLevel = AlarmLevels.Error,
                Name = Station1Alarm.机器人通信超时.ToString()
            });
            Alarms.Add(new Alarm(() => m_Alarm == Station1Alarm.产品连续校正失败)
            {
                AlarmKey = AlarmKeys.Alarm_9,
                AlarmLevel = AlarmLevels.Error,
                Name = Station1Alarm.产品连续校正失败.ToString()
            });
            Alarms.Add(new Alarm(() => m_Alarm == Station1Alarm.条码互锁失败)
            {
                AlarmKey = AlarmKeys.Alarm_10,
                AlarmLevel = AlarmLevels.Warrning,
                Name = Station1Alarm.条码互锁失败.ToString()
            });
            mAlarmManage.allAlarms.AddRange(Alarms);
            return Alarms;
        }
        public enum Station1Alarm : int
        {
            无消息,
            初始化故障,
            当前产品不是当前系列,
            互锁失败,
            报告生成失败,
            产品尺寸超出设定范围,
            设备未标定,
            机器人通信超时,
            产品连续校正失败,
            条码互锁失败,
            YAMAHA报警
        }

    }
}
