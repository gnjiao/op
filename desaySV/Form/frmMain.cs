using System;
using System.Collections.Generic;
using CMotion.Interfaces.IO;
using CMotion.Interfaces;
using System.Threading;
using System.ToolKit;
using System.Windows.Forms;
using System.Enginee;
using JobNumber;
using InterLocking;
using LogHelper = LogHeper.LogHelper;
using System.ToolKit.Helpers;
using Software.Device;
using CMotion.Interfaces.Base;
using ConfigPath;
using YAMAHA;

namespace desaySV
{
    public partial class frmMain : Form
    {

        private event Action<string> LoadingMessage;
        private event Action<UserLevel> UserLevelChangeEvent;
        private External m_External;
        private MachineOperate MachineOperation;

        private EventButton StartButton, EstopButton, StopButton, PauseButton, ResetButton;
        private LayerLight layerLight;
        private bool ManualAutoMode;

        Thread threadMachineRun = null;
        Thread threadAlarmCheck = null;
        Thread threadStatusCheck = null;
        private Station1 m_station1;
        private Yamaha yamaha;
        private Locking locking;
        private InterlockingClass interlock;
        AlarmManage mAlarmManage;
        private DM50S DM50S;
        private AlarmType MachineIsAlarm, Station1IsAlarm;
        public frmMain()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲         
            m_External = new External();
            mAlarmManage = new AlarmManage();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        private void UserLevelChange(UserLevel level)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<UserLevel>(UserLevelChange), level);
            }
            else
            {
                switch (level)
                {
                    case UserLevel.操作员:
                        contextMenuStrip1.Enabled = false;

                        break;
                    case UserLevel.工程师:
                        contextMenuStrip1.Enabled = true;

                        break;
                    case UserLevel.设计者:
                        contextMenuStrip1.Enabled = true;

                        break;
                    default:
                        contextMenuStrip1.Enabled = false;

                        break;
                }
            }
        }


        public void OnUserLevelChange(UserLevel level)
        {
            UserLevelChangeEvent?.Invoke(level);
        }

        /// <summary>
        /// 使用委托方式更新AppendText显示
        /// </summary>
        /// <param name="txt">消息</param>
        public void AppendText(string txt)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(AppendText), txt);
            }
            else
            {
                listBox1.Items.Insert(0, string.Format("{0}-{1}" + Environment.NewLine, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), txt));
                LogHelper.Debug(txt);
            }
        }

        public void StursView(MachineStatus txt)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<MachineStatus>(StursView), txt);
            }
            else
            {
                lblTestStep.Text = txt.ToString();
            }
        }

        #region 私有函数 

        private void Login()
        {
            try
            {

                using (JobNumber.JobNumber frm = new JobNumber.JobNumber())
                {
                    frm.ShowDialog();
                    frm.Hide();
                    if (frm.IsAdmin)
                    {
                        Global.userLevel = UserLevel.工程师;
                        OnUserLevelChange(Global.userLevel);
                    }
                    else if (frm.OperatorID == "10S88888")
                    {
                        Global.userLevel = UserLevel.设计者;
                        OnUserLevelChange(Global.userLevel);
                    }
                    else
                    {
                        Global.userLevel = UserLevel.操作员;
                        OnUserLevelChange(Global.userLevel);
                    }
                    lbJobnumber.Text = frm.OperatorID;
                    LogHelper.Info("用户登录：" + frm.OperatorID);
                };
            }
            catch (Exception ex)
            {
                LogHelper.Error("用户登录异常", ex);
            }
        }


        /// <summary>
        /// 扫描事件
        /// </summary>
        /// <param name="isCloseWhenCancel">是否换型</param>
        private void SelectMode()
        {
            try
            {
                bool isAdmin = Global.userLevel == UserLevel.设计者 || Global.userLevel == UserLevel.工程师;
                using (svfrmScanAndSelectMode frm = new svfrmScanAndSelectMode(AppConfig.ProductIntrinsicConfigModel, isAdmin))
                {
                    frm.ShowDialog();
                    if (string.IsNullOrEmpty(frm.A2C))
                    {
                        Close();
                        return;
                    }
                    AppConfig.ProductPath = frm.A2C;
                    interLockingParam.Instance = SerializerManager<interLockingParam>.Instance.Load(AppConfig.MesConfigPathName);
                    locking = new Locking();
                    interLockingParam.Instance.EvData.DeviceA2C = frm.A2C;
                    interLockingParam.Instance.EvData.LoginName = lbJobnumber.Text;
                    lbA2C.Text = AppConfig.ProductPath;
                    lbModel.Text = interLockingParam.Instance.EvData.ModelName = frm.Model;
                    lb.Text = interLockingParam.Instance.EvData.StationID = Config.Instance.StationID;
                    lbCustomer.Text = frm.Customer;
                    lbStationName.Text = frm.Line;
                    Delay.Instance = SerializerManager<Delay>.Instance.Load(AppConfig.ConfigDelayName);
                    Position.Instance = SerializerManager<Position>.Instance.Load(AppConfig.ConfigPositionName);
                    ProductConfig.Instance = SerializerManager<ProductConfig>.Instance.Load(AppConfig.ConfigOtherParamName);
                    interlock = new InterlockingClass();
                    lbInterlockingModel.Text = ProductConfig.Instance.switchCom.ToString();
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("扫描调取程序异常", ex);
            }
        }
        #endregion
        private void frmMain_Load(object sender, EventArgs e)
        {


            UserLevelChangeEvent += UserLevelChange;
            Global.userLevel = UserLevel.操作员;
            OnUserLevelChange(Global.userLevel);

            new Thread(new ThreadStart(() =>
            {
                frmStarting loading = new frmStarting(8);
                LoadingMessage += new Action<string>(loading.ShowMessage);
                loading.ShowDialog();
            })).Start();
            Thread.Sleep(500);

            Config.Instance = SerializerManager<Config>.Instance.Load(AppConfig.ConfigIntrinsicProductPathdName);
            AxisParameter.Instance = SerializerManager<AxisParameter>.Instance.Load(AppConfig.ConfigIntrinsicParamAxisCardName);
            #region  加载板卡

            LoadingMessage("加载板卡信息");
            try
            {
                IoPoints.ApsController.Initialize();
                if (!IoPoints.ApsController.LoadParamFromFile(AppConfig.ConfigAxisCardName("Param0.cfg")))
                { AppendText("配置文件失败:请将轴卡的配置文件" + ".cfg " + "拷贝到当前型号的路径下" + AppConfig.ConfigAxisCardName("Param0.cfg")); }

            }
            catch (Exception ex)
            {
                LogHelper.Error(ex.Message);
                AppendText("板卡初始化失败！请检查硬件！");
                timer1.Enabled = false;
            }
            #endregion

            #region 气缸信息

            LoadingMessage("加载气缸资源");
            var SwichCylinder = new DoubleCylinder(IoPoints.S15, IoPoints.S16, IoPoints.Y13, IoPoints.Y14)
            {
                Name = "切换气缸",
                Delay = Delay.Instance.SwitchCylinderDelay,
                Condition = new CylinderCondition(() => { return true; }, () => { return true; }) { External = m_External }
            };
            var LeftCylinder = new DoubleCylinder(IoPoints.S15, IoPoints.S16, IoPoints.Y13, IoPoints.Y14)
            {
                Name = "左负压",
                Delay = Delay.Instance.LeftCylinderDelay,
                Condition = new CylinderCondition(() => { return true; }, () => { return true; }) { External = m_External }
            };
            var RightCylinder = new DoubleCylinder(IoPoints.S15, IoPoints.S16, IoPoints.Y13, IoPoints.Y14)
            {
                Name = "右负压",
                Delay = Delay.Instance.RightCylinderDelay,
                Condition = new CylinderCondition(() => { return true; }, () => { return true; }) { External = m_External }
            };
            #endregion
            DM50S = new DM50S()
            {
                Name = "扫描枪"
            };
            try
            {
                DM50S.SetConnectionParam(Config.Instance.ReadCodePortConnectParam);
                DM50S.DeviceDataReceiveCompelete += new DataReceiveCompleteEventHandler(DealWithScrewDeviceReceiveData);
                DM50S.DeviceOpen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0}连接失败：{1}", DM50S.Name, ex.Message));
            }

            #region 轴信息

            LoadingMessage("加载轴控制资源");

            var XaxisServo = new StepAxis(IoPoints.ApsController)
            {
                NoId = 0,
                Transmission = AxisParameter.Instance.XTransParams,
                VelocityCurveHome = AxisParameter.Instance.HomeXVelocityCurve,
                VelocityCurveRun = AxisParameter.Instance.XVelocityCurve,
                Speed = 11,
                Name = "相机X轴"
            };
            XaxisServo._condition = new Func<bool>(() =>
            {
                return true;
            });
            var YaxisServo = new StepAxis(IoPoints.ApsController)
            {
                NoId = 1,
                Transmission = AxisParameter.Instance.YTransParams,
                VelocityCurveHome = AxisParameter.Instance.HomeYVelocityCurve,
                VelocityCurveRun = AxisParameter.Instance.YVelocityCurve,
                Speed = 11,
                Name = "治具Y轴"
            };
            YaxisServo._condition = new Func<bool>(() => { return true; });

            var ZaxisServo = new StepAxis(IoPoints.ApsController)
            {
                NoId = 2,
                Transmission = AxisParameter.Instance.ZTransParams,
                VelocityCurveHome = AxisParameter.Instance.HomeZVelocityCurve,
                VelocityCurveRun = AxisParameter.Instance.ZVelocityCurve,
                Speed = 11,
                Name = "升降Z轴"
            };
            ZaxisServo._condition = new Func<bool>(() => { return true; });
            var LFXaxisServo = new StepAxis(IoPoints.ApsController)
            {
                NoId = 3,
                Transmission = AxisParameter.Instance.LFXTransParams,
                VelocityCurveHome = AxisParameter.Instance.HomeLFXVelocityCurve,
                VelocityCurveRun = AxisParameter.Instance.LFXVelocityCurve,
                Speed = 11,
                Name = "相机左前X轴"
            };
            LFXaxisServo._condition = new Func<bool>(() => { return true; });
            var LFYaxisServo = new StepAxis(IoPoints.ApsController)
            {
                NoId = 4,
                Transmission = AxisParameter.Instance.LFYTransParams,
                VelocityCurveHome = AxisParameter.Instance.HomeLFYVelocityCurve,
                VelocityCurveRun = AxisParameter.Instance.LFYVelocityCurve,
                Speed = 11,
                Name = "相机左前Y轴"
            };
            LFYaxisServo._condition = new Func<bool>(() => { return true; });
            var LRXaxisServo = new StepAxis(IoPoints.ApsController)
            {
                NoId = 5,
                Transmission = AxisParameter.Instance.LRXTransParams,
                VelocityCurveHome = AxisParameter.Instance.HomeLRXVelocityCurve,
                VelocityCurveRun = AxisParameter.Instance.LRXVelocityCurve,
                Speed = 11,
                Name = "相机左后X轴"
            };
            LRXaxisServo._condition = new Func<bool>(() => { return true; });
            var LRYaxisServo = new StepAxis(IoPoints.ApsController)
            {
                NoId = 6,
                Transmission = AxisParameter.Instance.LRYTransParams,
                VelocityCurveHome = AxisParameter.Instance.HomeLRYVelocityCurve,
                VelocityCurveRun = AxisParameter.Instance.LRYVelocityCurve,
                Speed = 11,
                Name = "相机左后Y轴"
            };
            LRYaxisServo._condition = new Func<bool>(() => { return true; });
            var RYaxisServo = new StepAxis(IoPoints.ApsController)
            {
                NoId = 7,
                Transmission = AxisParameter.Instance.RYTransParams,
                VelocityCurveHome = AxisParameter.Instance.HomeRYVelocityCurve,
                VelocityCurveRun = AxisParameter.Instance.RYVelocityCurve,
                Speed = 11,
                Name = "相机右Y轴"
            };
            RYaxisServo._condition = new Func<bool>(() => { return true; });
            #endregion


            #region 工站模组操作

            LoadingMessage("加载模组操作资源");

            var Station1Initialize = new StationInitialize(
                () => { return !ManualAutoMode; },
                () => { return Station1IsAlarm.IsAlarm; });
            var Station1Operate = new StationOperate(
                () => { return Station1Initialize.InitializeDone; },
                () => { return Station1IsAlarm.IsAlarm; });



            MachineOperation = new MachineOperate(() =>
            {
                return Station1Initialize.InitializeDone;
            }, () =>
            {
                return Station1IsAlarm.IsAlarm | MachineIsAlarm.IsAlarm;
            });
            #endregion

            #region 雅马哈机器人
            yamaha = new Yamaha();
            if (!yamaha.Connect(Config.Instance.YamahaIP, Config.Instance.Port))
            {
                AppendText("机器人连接失败！");
            }
            #endregion

            #region 模组信息加载、启动

            LoadingMessage("加载模组信息");
            m_station1 = new Station1(m_External, Station1Initialize, Station1Operate)
            {
                mAlarmManage = mAlarmManage,
                XaxisServo = XaxisServo,
                YaxisServo = YaxisServo,
                ZaxisServo = ZaxisServo,
                LFXaxisServo = LFXaxisServo,
                LFYaxisServo = LFYaxisServo,
                LRXaxisServo = LRXaxisServo,
                LRYaxisServo = LRYaxisServo,
                RYaxisServo = RYaxisServo,
                YAMAHA = yamaha,
                SwichCylinder = SwichCylinder,
                LeftVacuum = LeftCylinder,
                RightVacuum = RightCylinder
            };
            m_station1.AddPart();
            m_station1.Run(RunningModes.Online);

            #endregion

            LoadingMessage("加载MES资源");
            #region 加载信号灯资源
            StartButton = new LightButton(IoPoints.S1, IoPoints.Y1);
            ResetButton = new LightButton(IoPoints.S2, IoPoints.Y10);
            PauseButton = new LightButton(IoPoints.S5, IoPoints.Y11);
            EstopButton = new EventButton(IoPoints.S3);
            StopButton = new LightButton(IoPoints.S4, IoPoints.Y12);


            layerLight = new LayerLight(IoPoints.Y11, IoPoints.Y10, IoPoints.Y9, IoPoints.Y12);

            StartButton.Pressed += btnStart_MouseDown;
            StartButton.Released += btnStart_MouseUp;
            PauseButton.Pressed += btnPause_MouseDown;
            PauseButton.Released += btnPause_MouseUp;
            ResetButton.Pressed += btnReset_MouseDown;
            ResetButton.Released += btnReset_MouseUp;

            MachineOperation.StartButton = StartButton;
            MachineOperation.PauseButton = PauseButton;
            MachineOperation.StopButton = StopButton;
            MachineOperation.ResetButton = ResetButton;
            MachineOperation.EstopButton = EstopButton;
            #endregion
            ManualAutoMode = false;
            LoadingMessage("加载线程资源");
            SerialStart();

            timer1.Enabled = true;
        }


        private void DealWithScrewDeviceReceiveData(object sender, string result)
        {
            var screw = new ScrewResult();
            try
            {
                if (result.Contains("Error!"))
                    throw new Exception(result);
                var strValue = result.Substring(result.IndexOf("TR"));
                screw.ProgramNo = strValue.Substring(2, 3);//程序序号
                screw.Output1 = strValue.Substring(6, 25).Trim();//输出信息1 
                screw.Output2 = strValue.Substring(31, 25).Trim();//输出信息2
                var strData = strValue.Substring(strValue.IndexOf("+") + 1);
                var strSubData = strData.Split('+');
                int strLen = strSubData.Length;
                screw.PeekTor = double.Parse(strSubData[strLen - 3].Trim().Substring(0, 5)); //最大力矩
                screw.PeekAngle = double.Parse(strSubData[strLen - 2].Trim().Substring(0, 9));//最大角度
                var strMid = strSubData[strLen - 2].Trim();
                screw.Temperature = double.Parse(strSubData[strLen - 1].Trim().Substring(0, 5));//温度

                MessageBox.Show(DM50S.Name + "获取数据成功，程序号：" + screw.ProgramNo + " 力矩：" + screw.PeekTor + " 角度：" + screw.PeekAngle);
            }
            catch (Exception ex)
            {
                MessageBox.Show(DM50S.Name + "获取数据失败：" + ex.Message);
                screw.ProgramNo = "";
                screw.PeekTor = 0.00;
                screw.PeekAngle = 0.00;

            }
            finally
            {
                //resultScrew.ProgramNO = screw.ProgramNo;
                //resultScrew.PeekTor = screw.PeekTor;
                //resultScrew.PeekAngle = screw.PeekAngle;
                //resultScrew.Time = screw.Time;
                //Marking.isScrewDeviceCompleted = true;
            }
        }


        private void frmMain_Shown(object sender, EventArgs e)
        {
            this.Maximized();
            Login();
            SelectMode();
            using (Target frm = new Target())
            {
                frm.ShowDialog();
                lbTarget.Text = frm.TargetNumber;
            }
        }

        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            IoPoints.Y16.Value = false;

            DialogResult result = MessageBox.Show("是否保存配置文件再退出？", "退出", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                threadStatusCheck?.Abort();
                threadMachineRun?.Abort();
                threadAlarmCheck?.Abort();
                SerializerManager<Config>.Instance.Save(AppConfig.ConfigIntrinsicProductPathdName, Config.Instance);
                SerializerManager<AxisParameter>.Instance.Save(AppConfig.ConfigIntrinsicParamAxisCardName, AxisParameter.Instance);
                SerializerManager<Position>.Instance.Save(AppConfig.ConfigPositionName, Position.Instance);
                SerializerManager<Delay>.Instance.Save(AppConfig.ConfigDelayName, Delay.Instance);
                SerializerManager<ProductConfig>.Instance.Save(AppConfig.ConfigOtherParamName, ProductConfig.Instance);
                SerializerManager<interLockingParam>.Instance.Save(AppConfig.MesConfigPathName, interLockingParam.Instance);
                Thread.Sleep(200);
                LogHelper.Debug("配置文件已保存");
            }
            else if (result == DialogResult.No)
            {
                threadStatusCheck?.Abort();
                threadMachineRun?.Abort();
                threadAlarmCheck?.Abort();
                LogHelper.Debug("配置文件不保存");
            }
            else
            {
                e.Cancel = true;
            }
        }

        #region UI_TestData的初始化与更新






        #endregion



        #region 线程处理
        private void SerialStart()
        {
            try
            {
                threadMachineRun = new Thread(MachineRun);
                threadMachineRun.IsBackground = true;
                threadMachineRun.Start();

                threadAlarmCheck = new Thread(AlarmCheck);
                threadAlarmCheck.IsBackground = true;
                threadAlarmCheck.Start();

                threadStatusCheck = new Thread(StatusCheck);
                threadStatusCheck.IsBackground = true;
                threadStatusCheck.Start();
            }
            catch (Exception ex)
            {
                AppendText("Server start Error: " + ex.Message);
            }
        }
        private void MachineRun()
        {
            while (true)
            {
                Thread.Sleep(5);

                m_External.AirSignal = !IoPoints.S1.Value;
                m_External.ManualAutoMode = ManualAutoMode;
                MachineOperation.ManualAutoModel = ManualAutoMode;
                MachineOperation.Run();

                IoPoints.Y10.Value = true;

                layerLight.Status = MachineOperation.Status;
                layerLight.Refreshing();

                StursView(MachineOperation.Status);
                #region 工站操作流程刷新
                m_station1.stationOperate.ManualAutoMode = ManualAutoMode;
                m_station1.stationOperate.AutoRun = MachineOperation.Running;
                m_station1.stationInitialize.Run();
                m_station1.stationOperate.Run();


                #endregion

                #region 光幕，光栅异常处理

                #endregion

                #region 急停处理
                if (!EstopButton.PressedIO.Value)//|| (IoPoints.S21.Value && m_Turntable.RotaryMotorServo.CurrentSpeed > 10)
                {

                    //m_Turntable.RotaryMotorServo.Stop();
                    //m_Turntable.DownServo.Stop();
                    //m_station1.stationInitialize.InitializeDone = false;
                    //m_station2.stationInitialize.InitializeDone = false;
                    //m_station3.stationInitialize.InitializeDone = false;
                    //m_station4.stationInitialize.InitializeDone = false;
                    //m_Turntable.stationInitialize.InitializeDone = false;
                    MachineOperation.IniliazieDone = false;
                }
                #endregion



                #region 设备复位中
                if (MachineOperation.Resetting)
                {
                    switch (MachineOperation.Flow)
                    {
                        case 0:
                            MachineOperation.IniliazieDone = false;
                            m_station1.stationInitialize.Flow = 0;
                            //    m_station2.stationInitialize.Flow = 0;
                            //    m_station3.stationInitialize.Flow = 0;
                            //    m_station4.stationInitialize.Flow = 0;
                            //    m_Turntable.stationInitialize.Flow = 0;
                            //    m_External.InitializingDone = false;
                            //    m_station1.stationInitialize.InitializeDone = false;
                            //    m_station1.stationInitialize.Start = false;
                            //    m_station2.stationInitialize.InitializeDone = false;
                            //    m_station2.stationInitialize.Start = false;
                            //    m_station3.stationInitialize.InitializeDone = false;
                            //    m_station3.stationInitialize.Start = false;
                            //    m_station4.stationInitialize.InitializeDone = false;
                            //    m_station4.stationInitialize.Start = false;
                            //    m_Turntable.stationInitialize.InitializeDone = false;
                            //    m_Turntable.stationInitialize.Start = false;
                            //    if (true) MachineOperation.Flow = 10;
                            //    break;
                            //case 10:
                            //    m_station1.stationInitialize.Start = true;
                            //    m_station2.stationInitialize.Start = true;
                            //    m_station3.stationInitialize.Start = true;
                            //    m_station4.stationInitialize.Start = true;
                            //    if (m_station1.stationInitialize.Running && m_station2.stationInitialize.Running &&
                            //        m_station3.stationInitialize.Running && m_station4.stationInitialize.Running)
                            //    {
                            //        MachineOperation.Flow = 20;
                            //    }
                            //    break;
                            //case 20:
                            //    if (m_station1.stationInitialize.Flow == -1 || m_station2.stationInitialize.Flow == -1 ||
                            //        m_station3.stationInitialize.Flow == -1 || m_station4.stationInitialize.Flow == -1)
                            //    {
                            //        MachineOperation.IniliazieDone = false;
                            //        MachineOperation.Flow = -1;
                            //    }
                            //    else
                            //    {
                            //        if (m_station1.stationInitialize.InitializeDone && m_station2.stationInitialize.InitializeDone &&
                            //            m_station3.stationInitialize.InitializeDone && m_station4.stationInitialize.InitializeDone)
                            //        {
                            //            MachineOperation.Flow = 30;
                            //        }
                            //    }
                            //    break;
                            //case 30:
                            //    m_Turntable.stationInitialize.Start = true;
                            //    MachineOperation.Flow = 40;
                            //    break;
                            //case 40:
                            //    if (m_Turntable.stationInitialize.Flow == -1)
                            //    {
                            //        MachineOperation.IniliazieDone = false;
                            //        MachineOperation.Flow = -1;
                            //    }
                            //    else
                            //    {
                            //        if (m_Turntable.stationInitialize.InitializeDone)
                            //        {
                            //            m_Turntable.RotaryMotorServo.intPos = 0;
                            //            MachineOperation.IniliazieDone = true;
                            //        }
                            //    }
                            break;
                        default:
                            m_station1.stationInitialize.Start = false;
                            break;
                    }
                }
                #endregion

                #region 设备停止中
                if (MachineOperation.Stopping)
                {
                    m_station1.stationInitialize.Estop = true;
                    //m_station2.stationInitialize.Estop = true;
                    //m_station3.stationInitialize.Estop = true;
                    //m_station4.stationInitialize.Estop = true;
                    //m_Turntable.stationInitialize.Estop = true;
                    if (!m_station1.stationInitialize.Running)
                    {
                        MachineOperation.IniliazieDone = false;
                        MachineOperation.Stopping = false;
                        m_station1.stationInitialize.Estop = false;

                    }
                }
                #endregion
            }
        }
        private void AlarmCheck()
        {
            while (true)
            {
                Thread.Sleep(100);
                Station1IsAlarm = AlarmCheck(m_station1.Alarms);
                var list = new List<Alarm>();
                list.Add(new Alarm(() => !EstopButton.PressedIO.Value)
                {
                    AlarmKey = AlarmKeys.ESTOP,
                    AlarmLevel = AlarmLevels.Error,
                    Name = "急停按钮已按下，注意安全！"
                });
                list.Add(new Alarm(() => !yamaha.Alarm)
                {
                    AlarmKey = AlarmKeys.RobotRCX340Alarm,
                    AlarmLevel = AlarmLevels.Error,
                    Name = "机器人报警！"
                });
                list.Add(new Alarm(() => !IoPoints.S1.Value)
                {
                    AlarmKey = AlarmKeys.PRESSURE,
                    AlarmLevel = AlarmLevels.Error,
                    Name = "气源未开启！"
                });
                mAlarmManage.allAlarms.AddRange(list);
                MachineIsAlarm = AlarmCheck(list);
            }
        }
        public AlarmType AlarmCheck(IList<Alarm> Alarms)
        {
            var Alarm = new AlarmType();
            foreach (Alarm alarm in Alarms)
            {
                var btemp = alarm.IsAlarm;
                if (alarm.AlarmLevel == AlarmLevels.Error)
                {
                    Alarm.IsAlarm |= btemp;
                    this.Invoke(new Action(() =>
                    {
                        Msg(string.Format("{0},{1}", alarm.AlarmLevel.ToString(), alarm.Name), btemp);
                    }));
                }
                else if (alarm.AlarmLevel == AlarmLevels.None)
                {
                    Alarm.IsPrompt |= btemp;
                    this.Invoke(new Action(() =>
                    {
                        Msg(string.Format("{0},{1}", alarm.AlarmLevel.ToString(), alarm.Name), btemp);
                    }));
                }
                else
                {
                    Alarm.IsWarning |= btemp;
                    this.Invoke(new Action(() =>
                    {
                        Msg(string.Format("{0},{1}", alarm.AlarmLevel.ToString(), alarm.Name), btemp);
                    }));
                }
            }
            return Alarm;
        }
        private void Msg(string str, bool value)
        {
            string tempstr = null;
            bool sign = false;
            try
            {
                var arrRight = new List<object>();
                foreach (var tmpist in listBox2.Items) arrRight.Add(tmpist);
                if (value)
                {
                    foreach (string tmplist in arrRight)
                    {
                        if (tmplist.IndexOf("-") > -1)
                        {
                            tempstr = tmplist.Substring(tmplist.IndexOf("-") + 1, tmplist.Length - tmplist.IndexOf("-") - 1);
                        }
                        if (tempstr == (str + "\r\n"))
                        {
                            sign = true;
                            break;
                        }
                    }
                    if (!sign)
                    {
                        listBox2.Items.Insert(0, (string.Format("{0}-{1}" + Environment.NewLine, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), str)));
                        LogHelper.Error(str);
                    }
                }
                else
                {
                    foreach (string tmplist in arrRight)
                    {
                        if (tmplist.IndexOf("-") > -1)
                        {
                            tempstr = tmplist.Substring(tmplist.IndexOf("-") + 1, tmplist.Length - tmplist.IndexOf("-") - 1);
                            if (tempstr == (str + "\r\n")) listBox2.Items.Remove(tmplist);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AppendText("消息显示异常：" + ex.ToString());
            }
        }
        private void StatusCheck()
        {
            var list = new List<ICylinderStatusJugger>();
            list.AddRange(m_station1.CylinderStatus);
            while (true)
            {
                Thread.Sleep(10);
                foreach (var lst in list)
                    lst.StatusJugger();
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            this.Text = "西威标准软件 版本号：" + Application.ProductVersion;
            lbOutputPass.Text = ProductConfig.Instance.ProductOkTotal.ToString();
            lbOutputFail.Text = ProductConfig.Instance.ProductNgTotal.ToString();
            if ((ProductConfig.Instance.ProductOkTotal + ProductConfig.Instance.ProductNgTotal) > 0)
            {
                int countall = ProductConfig.Instance.ProductOkTotal * 1000;
                int col = countall / (ProductConfig.Instance.ProductOkTotal + ProductConfig.Instance.ProductNgTotal);
                double x = col / 10.0;
                lbRate.Text = x.ToString("0.0") + "%";
            }
            else { lbRate.Text = "100.0%"; }
            lbRunTime.Text = Global.Beattime.ToString("0.000") + "s";
            #region 线体信息

            #endregion
            timer1.Enabled = true;
        }
        #endregion


        #region 设备按钮操作
        private void tlUseLoad_Click(object sender, EventArgs e)
        {

            if (ManualAutoMode) return;
            LogHelper.Debug("登陆操作");
            this.Maximized();
            Login();
            SelectMode();

        }
        private void toolCleanData_Click(object sender, EventArgs e)
        {
            LogHelper.Debug("数据清除操作");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (ManualAutoMode) return;
            LogHelper.Debug("扫描换型操作");
            SelectMode();
        }

        private void tooStripExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
            }
        }

        private void toolStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
            }
        }

        private void tlRun_MouseDown(object sender, MouseEventArgs e)
        {

            ManualAutoMode = ManualAutoMode ? false : true;
            tlRun.Image = ManualAutoMode ? Properties.Resources.Stop : Properties.Resources.Run;
            if (ManualAutoMode)
            {
                MachineOperation.Pause = true;
                Thread.Sleep(5);
                MachineOperation.Pause = false;
                btnStart_MouseDown(null, null);
            }
        }

        private void tlRun_MouseUp(object sender, MouseEventArgs e)
        {
            btnStart_MouseUp(null, null);
        }



        private void iO控制IToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            new frmIOmonitor().ShowDialog();
        }

        private void 图像学习ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogHelper.Debug("示教操作");
            new frmTeach(m_station1).ShowDialog();
        }

        private void 工位操作ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            LogHelper.Debug("参数设置操作");
            new frmParameter(m_station1).ShowDialog();
        }

        private void 打开程序所在位置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.ToolKit.DosHelper.OpenPath(System.ToolKit.FileHelper.AppPath);
        }

        private void toolStrip1_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
            }
        }

        private void 串口设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogHelper.Debug("串口设置");
            using (frmSerialPort frm = new frmSerialPort(DM50S, typeof(DM50S)))
            {
                frm.ShowDialog();
                Config.Instance.ReadCodePortConnectParam = DM50S.ConnectionParam;
            };

        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (Global.userLevel == UserLevel.操作员) { return; }
            if (e.KeyCode == Keys.F12 && contextMenuStrip1.Enabled == true)
            {
                using (F12_Modifier_Number frm = new F12_Modifier_Number())
                {
                    frm.ShowDialog();
                }
            }

            if (e.KeyCode == Keys.F1)
            {
                using (Target frm = new Target())
                {
                    frm.ShowDialog();
                    lbTarget.Text = frm.TargetNumber;
                }
            }
        }

      

        /// <summary>
        /// 启动按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_MouseDown(object sender, EventArgs e)
        {
            if (!ManualAutoMode)
            {
                AppendText("设备无法启动，必须在自动模式才能操作！");
                return;
            }
            MachineOperation.Start = true;
        }

        private void 生成报告设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FrmInterLockingSet frm = new FrmInterLockingSet(AppConfig.MesConfigPathName))
            {
                frm.ShowDialog();
            }            
        }

        private void 机器人测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (MaintForm frm = new MaintForm())
            {
                frm.ShowDialog();
            }
            
        }


        /// <summary>
        /// 启动按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_MouseUp(object sender, EventArgs e)
        {
            MachineOperation.Start = false;
        }
        /// <summary>
        /// 暂停按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPause_MouseDown(object sender, EventArgs e)
        {
            MachineOperation.Pause = true;
        }
        /// <summary>
        /// 暂停按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPause_MouseUp(object sender, EventArgs e)
        {
            MachineOperation.Pause = false;
        }
        /// <summary>
        /// 复位按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_MouseDown(object sender, EventArgs e)
        {
            if (ManualAutoMode)
            {
                if (!MachineIsAlarm.IsAlarm && !Station1IsAlarm.IsAlarm)
                    AppendText("设备手动状态时，才能复位。自动状态只能清除报警！");
                m_External.AlarmReset = true;
            }
            else
            {
                if (MachineOperation != null)
                {
                    MachineOperation.IniliazieDone = false;
                    MachineOperation.Flow = 0;
                    MachineOperation.Reset = true;
                }
            }
        }
        /// <summary>
        /// 复位按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_MouseUp(object sender, EventArgs e)
        {
            MachineOperation.Reset = false;
            m_External.AlarmReset = false;
        }

        #endregion
    }

}
