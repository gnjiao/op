using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace YAMAHA
{
    public enum eDirection
    {
        J1P = 0,
        J1N = 1,
        J2P = 2,
        J2N = 3,
        J3P = 4,
        J3N = 5,
        J4P = 6,
        J4N = 7,
        J5P = 8,
        J5N = 9,
        J6P = 10,
        J6N = 11,
        J7P = 12,
        J7N = 13,
        XP = 14,
        XN = 15,
        YP = 16,
        YN = 17,
        ZP = 18,
        ZN = 19,
        RXP = 20,
        RXN = 21,
        RYP = 22,
        RYN = 23,
        RZP = 24,
        RZN = 25
    }

    public class Yamaha
    {
        #region ***********************************变量定义******************************************

        private Socket _socket;                         //通讯socket

        private bool _bWork = false;                    //后台线程工作命令

        private bool _bJogging = false;                 //机械手正在Jog运动中

        private bool _bWaitingEnd = false;              //机械手正在等待END信号中

        private bool _bReady = false;                   //机械手可以接收运动命令

        private AutoResetEvent _evRecv = new AutoResetEvent(false);                 //同步事件

        private Thread _tWorkThread;                    //后台线程
        #endregion ***********************************************************************************



        #region ***********************************机械手状态变量************************************

        private bool _bServoOn = false;                 //伺服状态

        private bool _bConnected = false;               //连接状态

        public bool Alarm = false;

        public double CurrentPosX { set; get; }
        public double CurrentPosY { set; get; }
        public double CurrentPosZ { set; get; }
        public double CurrentPosR { set; get; }

        private double[] _dPosition = new double[7] { 0, 0, 0, 0, 0, 0, 0 };        //机械手当前位置X Y Z RZ 0 0 HAND

        //private bool _bAlarm = false;                   //报警状态

        //private bool _bWarn = false;                    //警告状态

        private bool[] _arrDI = new bool[32];           //DI信号  4组  0、1组是系统，2、3组始是用户

        private bool[] _arrDO = new bool[32];           //DO信号  4组  0、1组是系统，2、3组始是用户

        private bool[] _arrSysDO = new bool[32];        /*系统DO信号
                                                            SO0(1):CPU OK
                                                            SO0(2):Servo on 
                                                            SO0(3):Alarm output
                                                            SO1(0):Auto mode output
                                                            SO1(2):Sequence program in progress
                                                            SO1(3):Robot program in progress
                                                            SO1(4):Program reset status output
                                                            SO1(5):Warning output
                                                        */


        #endregion **********************************************************************************



        #region **************************************连接断开函数***********************************
        public bool Connect(string ip, int port)
        {
            bool result = true;
            string data;
            byte[] buffer = new byte[100];

            //如果后台线程已经开启，关闭线程。正常重新连接时线程是自动关闭退出的
            if (_tWorkThread != null)
            {
                _evRecv.WaitOne();
                _bWork = false;
                _evRecv.Set();
                _tWorkThread.Abort();
                _tWorkThread = null;
            }

            //关闭socket
            if (_socket != null)
            {
                if (_socket.Connected)
                {
                    _socket.Disconnect(false);
                    _socket.Close();
                }
                _socket.Dispose();
            }

            EndPoint endPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            try
            {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _socket.ReceiveTimeout = 500;
                _socket.SendTimeout = 200;
                _socket.Connect(endPoint);  //建立连接，

                _socket.Receive(buffer);   //必须返回Welcome
                data = Encoding.ASCII.GetString(buffer).Replace('\0', ' ').Trim();

                if (!data.Contains("Welcome"))
                    throw new Exception("Connect failed!");
                //清除CR LF字符  或者这里休眠100ms后一次性接收完整
                _socket.Receive(buffer);  //必须返回空行

                //初始化变量
                _bJogging = false;
                _bWaitingEnd = false;

                _bWork = true;

                _evRecv.Reset();

                //设置连接状态
                _bConnected = true;

                //启动后台刷新线程
                _tWorkThread = new Thread(WorkThreadFunc);
                _tWorkThread.IsBackground = true;
                _tWorkThread.Start();

                //启动后台刷新线程
                _evRecv.Set();

            }
            catch (Exception ex)
            {
                //设置连接状态
                _bConnected = false;
                // _socket.Disconnect(false);
                result = false;
            }

            return result;
        }

        public bool DisConnect()
        {
            if (_socket != null)
            {
                //后台线程关闭
                if (_tWorkThread != null && _tWorkThread.IsAlive)
                {
                    _evRecv.WaitOne();
                    _evRecv.Set();
                    _tWorkThread.Abort();
                }

                _bConnected = false;
                _bWork = false;

                if (_socket.Connected)
                {
                    _socket.Disconnect(false);
                    _socket.Close();
                }

                _socket.Dispose();
            }
            return true;
        }

        #endregion ***********************************************************************************



        #region ***************************************程序控制函数***********************************
        public bool Run()
        {
            bool result = true;

            byte[] buffer1 = new byte[10];

            byte[] cmd = System.Text.Encoding.Default.GetBytes("@RUN" + "\r\n");
            string data;

            try
            {
                if (_bJogging)
                    throw new Exception("Jogging!");

                if (_bWaitingEnd)
                    throw new Exception("Waitting end!");

                if (!_socket.Connected || !_bConnected)
                    throw new Exception("Connection is loss!");

                //等待后台线程释放控制权
                _evRecv.WaitOne();

                _socket.Send(cmd);

                _socket.Receive(buffer1);
                data = Encoding.ASCII.GetString(buffer1).Replace('\0', ' ').Trim();


                if (data != "OK")
                    throw new Exception("Command error!");

            }
            catch (SocketException e)
            {
                result = false;
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                _evRecv.Set();
            }

            return result;
        }

        public bool Pause()
        {
            bool result = true;

            byte[] buffer1 = new byte[10];

            byte[] cmd = System.Text.Encoding.Default.GetBytes("@STOP" + "\r\n");
            string data;

            try
            {
                if (_bJogging)
                    throw new Exception("Jogging!");

                if (_bWaitingEnd)
                    throw new Exception("Waitting end!");

                if (!_socket.Connected || !_bConnected)
                    throw new Exception("Connection is loss!");

                //等待后台线程释放控制权
                _evRecv.WaitOne();

                _socket.Send(cmd);

                _socket.Receive(buffer1);
                data = Encoding.ASCII.GetString(buffer1).Replace('\0', ' ').Trim();


                if (data != "OK")
                    throw new Exception("Command error!");

            }
            catch (SocketException e)
            {
                result = false;
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                _evRecv.Set();
            }

            return result;
        }

        public bool Stop()
        {
            bool result = true;

            byte[] buffer1 = new byte[10];
            byte[] buffer2 = new byte[10];

            byte[] cmdStop = System.Text.Encoding.Default.GetBytes("@STOP " + "\r\n");
            byte[] cmdReset = System.Text.Encoding.Default.GetBytes("@RESET " + "\r\n");
            string data;

            try
            {
                if (_bJogging)
                    throw new Exception("Jogging!");

                if (_bWaitingEnd)
                    throw new Exception("Waitting end!");

                if (!_socket.Connected || !_bConnected)
                    throw new Exception("Connection is loss!");

                //等待后台线程释放控制权
                _evRecv.WaitOne();

                _socket.Send(cmdStop);

                _socket.Receive(buffer1);
                data = Encoding.ASCII.GetString(buffer1).Replace('\0', ' ').Trim();

                if (data != "OK")
                    throw new Exception("Command error!");

                _socket.Send(cmdReset);

                _socket.Receive(buffer2);
                data = Encoding.ASCII.GetString(buffer2).Replace('\0', ' ').Trim();

                if (data != "OK")
                    throw new Exception("Command error!");
            }
            catch (SocketException e)
            {
                result = false;
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                _evRecv.Set();
            }

            return result;
        }

        public bool OpenFunction(short Function_Num)
        {
            return false;
        }

        public bool CloseFunction()
        {
            return false;
        }

        #endregion ************************************************************************************



        #region ***************************************状态获取函数***********************************

        public bool ExecutorState(out string state)
        {
            bool result = true;

            state = "Close";

            try
            {
                if (!_socket.Connected || !_bConnected)
                    throw new Exception("Connection is loss!");

                if (_arrSysDO[12])
                    state = "Running";
                else
                    state = "Close";

            }
            catch (SocketException e)
            {
                result = false;
            }
            catch (Exception e)
            {
                result = false;
            }

            return result;
        }

        public bool GetAlarm(out bool alarmflag, out string AL)
        {
            bool result = true;

            alarmflag = false;
            AL = "";
            try
            {
                if (!_socket.Connected || !_bConnected)
                    throw new Exception("Connection is loss!");

                alarmflag = _arrSysDO[13];
                Alarm = alarmflag;
            }
            catch (SocketException e)
            {
                result = false;
            }
            catch (Exception e)
            {
                result = false;
            }

            return result;
        }

        public bool GetWarnning(out bool warnningflag, out string WA)
        {
            bool result = true;

            warnningflag = false;
            WA = "";
            try
            {
                if (!_socket.Connected || !_bConnected)
                    throw new Exception("Connection is loss!");

                warnningflag = _arrSysDO[4];
            }
            catch (SocketException e)
            {
                result = false;
            }
            catch (Exception e)
            {
                result = false;
            }

            return result;
        }

        public bool GetRobotPosition(out double[] pos)
        {
            bool result = true;
            pos = new double[4] { 0, 0, 0, 0 };

            try
            {
                if (!_socket.Connected || !_bConnected)
                    throw new Exception("Connection is loss!");

                pos[0] = _dPosition[0];
                pos[1] = _dPosition[1];
                pos[2] = _dPosition[2];
                pos[3] = _dPosition[3];

            }
            catch (SocketException e)
            {
                result = false;
            }
            catch (Exception e)
            {
                result = false;
            }

            return result;
        }
        /// <summary>
        /// 写入DI
        /// </summary>
        /// <param name="DI_Num"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool ReadUserDI(int DI_Num, out bool state)
        {
            bool result = true;
            state = false;

            try
            {
                if (DI_Num < 0 || DI_Num > 15)
                    throw new Exception("DI is out of range!");

                if (!_socket.Connected || !_bConnected)
                    throw new Exception("Connection is loss!");

                state = _arrDI[DI_Num + 16];//从组2开始

            }
            catch (SocketException e)
            {
                result = false;
            }
            catch (Exception e)
            {
                result = false;
            }

            return result;
        }
        /// <summary>
        /// 读取DO
        /// </summary>
        /// <param name="DO_Num"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool ReadUserDO(int DO_Num, out bool state)
        {
            bool result = true;
            state = false;

            try
            {
                if (DO_Num < 0 || DO_Num > 7)
                    throw new Exception("DO is out of range!");

                if (!_socket.Connected || !_bConnected)
                    throw new Exception("Connection is loss!");

                state = _arrDO[DO_Num + 16];//从组2开始

            }
            catch (SocketException e)
            {
                result = false;
            }
            catch (Exception e)
            {
                result = false;
            }

            return result;
        }
        /// <summary>
        /// 获取机器人连接状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool RobotConnectState(out bool state)
        {
            bool result = true;
            state = false;
            try
            {
                state = _socket.Connected && _bConnected;
            }
            catch (Exception e)
            {
                result = false;
            }

            return result;
        }
        /// <summary>
        /// 检测通信是否待机中
        /// </summary>
        /// <param name="State"></param>
        /// <returns></returns>
        public bool Ready(out bool State)
        {
            bool result = true;
            State = false;

            try
            {
                if (!_socket.Connected || !_bConnected)
                    throw new Exception("Connection is loss!");

                State = _bReady;

            }
            catch (SocketException e)
            {
                result = false;
            }
            catch (Exception e)
            {
                result = false;
            }

            return result;
        }

        public bool ServoState(out bool state)
        {
            bool result = true;
            state = false;

            try
            {
                if (!_socket.Connected || !_bConnected)
                    throw new Exception("Connection is loss!");

                state = _bServoOn;

            }
            catch (SocketException e)
            {
                result = false;
            }
            catch (Exception e)
            {
                result = false;
            }

            return result;
        }

        #endregion *************************************************************************************



        #region ***************************************运动控制函数***********************************
        /// <summary>
        /// 连续JOG
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public bool Jog(int Axis, bool dir)
        {
            bool result = true;

            byte[] buffer = new byte[10];
            byte[] cmd = new byte[1] { 0x03 };
            string data;
            string d = dir ? "+" : "-";
            if (Axis < 1 || Axis > 4) { return false; }
            cmd = Encoding.Default.GetBytes("@JOGXY " + Axis.ToString() + d + "\r\n");
            try
            {

                if (_bJogging)
                    throw new Exception("Jogging!");

                if (_bWaitingEnd)
                    throw new Exception("Waitting end!");

                if (!_socket.Connected || !_bConnected)
                    throw new Exception("Connection is loss!");

                //等待后台线程释放控制权
                _evRecv.WaitOne();

                _socket.Send(cmd);
                _socket.Receive(buffer);
                data = Encoding.ASCII.GetString(buffer).Replace('\0', ' ').Trim();

                if (data != "RUN")
                    throw new Exception("Command error!");

                _bReady = false;
                _bJogging = true;

            }
            catch (SocketException e)
            {
                result = false;
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                _evRecv.Set();
            }

            return result;
        }

        /// <summary>
        /// 连续移动
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool MovL(double[] point)
        {
            bool result = true;

            byte[] buffer = new byte[10];
            byte[] cmd = new byte[1] { 0x03 };
            string data;

            try
            {
                if (_bJogging)
                    throw new Exception("Jogging!");

                if (_bWaitingEnd)
                    throw new Exception("Waitting end!");

                if (!_socket.Connected || !_bConnected)
                    throw new Exception("Connection is loss!");

                //等待后台线程释放控制权
                _evRecv.WaitOne();

                cmd = System.Text.Encoding.Default.GetBytes("@MOVE L," + point[0].ToString("0.000") + " " + point[1].ToString("0.000") + " " + point[2].ToString("0.000") + " " + point[3].ToString("0.000") + " 0 0 " + point[6].ToString("0") + "\r\n");

                _socket.Send(cmd);

                _socket.Receive(buffer);
                data = Encoding.ASCII.GetString(buffer).Replace('\0', ' ').Trim();

                if (data != "RUN")
                    throw new Exception("Command error!");

                _bReady = false;
                _bWaitingEnd = true;
            }
            catch (SocketException e)
            {
                result = false;
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                _evRecv.Set();
            }

            return result;
        }
        /// <summary>
        /// 连续移动
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool MovL(int index)
        {
            bool result = true;

            byte[] buffer = new byte[10];
            byte[] cmd = new byte[1] { 0x03 };
            string data;

            try
            {
                if (_bJogging)
                    throw new Exception("Jogging!");

                if (_bWaitingEnd)
                    throw new Exception("Waitting end!");

                if (!_socket.Connected || !_bConnected)
                    throw new Exception("Connection is loss!");

                if (index < 0 || index > 1000)
                    throw new ArgumentOutOfRangeException("Index is out of range!");

                //等待后台线程释放控制权
                _evRecv.WaitOne();

                cmd = Encoding.Default.GetBytes("@MOVE L,P" + index.ToString("0") + "\r\n");

                _socket.Send(cmd);
                _socket.Receive(buffer);
                data = Encoding.ASCII.GetString(buffer).Replace('\0', ' ').Trim();

                if (data != "RUN")
                    throw new Exception("Command error!");

                _bReady = false;
                _bWaitingEnd = true;
            }
            catch (SocketException e)
            {
                result = false;
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                _evRecv.Set();
            }

            return result;
        }
        /// <summary>
        /// 机器人绝对移动到地址
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool MovP(double[] point)
        {
            bool result = true;

            byte[] buffer = new byte[10];
            byte[] cmd = new byte[1] { 0x03 };
            string data;

            try
            {
                if (_bJogging)
                    throw new Exception("Jogging!");

                if (_bWaitingEnd)
                    throw new Exception("Waitting end!");

                if (!_socket.Connected || !_bConnected)
                    throw new Exception("Connection is loss!");

                //等待后台线程释放控制权
                _evRecv.WaitOne();

                cmd = Encoding.Default.GetBytes("@MOVE P," + point[0].ToString("0.000") + " " + point[1].ToString("0.000") + " " + point[2].ToString("0.000") + " " + point[3].ToString("0.000") + " 0 0 " + point[6].ToString("0") + "\r\n");

                _socket.Send(cmd);

                _socket.Receive(buffer);
                data = Encoding.ASCII.GetString(buffer).Replace('\0', ' ').Trim();

                if (data != "RUN")
                    throw new Exception("Command error!");

                _bReady = false;
                _bWaitingEnd = true;
            }
            catch (SocketException e)
            {
                result = false;
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                _evRecv.Set();
            }

            return result;
        }
        /// <summary>
        /// 机器人相对移动
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool Movi(double[] point)
        {
            bool result = true;

            byte[] buffer = new byte[10];
            byte[] cmd = new byte[1] { 0x03 };
            string data;

            try
            {
                if (_bJogging)
                    throw new Exception("Jogging!");

                if (_bWaitingEnd)
                    throw new Exception("Waitting end!");

                if (!_socket.Connected || !_bConnected)
                    throw new Exception("Connection is loss!");

                //等待后台线程释放控制权
                _evRecv.WaitOne();

                cmd = Encoding.Default.GetBytes("@MOVEI P," + point[0].ToString("0.000") + " " + point[1].ToString("0.000") + " " + point[2].ToString("0.000") + " " + point[3].ToString("0.000") + " 0 0 " + point[6].ToString("0") + "\r\n");

                _socket.Send(cmd);

                _socket.Receive(buffer);
                data = Encoding.ASCII.GetString(buffer).Replace('\0', ' ').Trim();

                if (data != "RUN")
                    throw new Exception("Command error!");

                _bReady = false;
                _bWaitingEnd = true;
            }
            catch (SocketException e)
            {
                result = false;
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                _evRecv.Set();
            }

            return result;
        }
        /// <summary>
        /// 机器人绝对移动到P点位
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool MovP(int index)
        {
            bool result = true;

            byte[] buffer = new byte[10];
            byte[] cmd = new byte[1] { 0x03 };
            string data;

            try
            {
                if (_bJogging)
                    throw new Exception("Jogging!");

                if (_bWaitingEnd)
                    throw new Exception("Waitting end!");

                if (!_socket.Connected || !_bConnected)
                    throw new Exception("Connection is loss!");

                if (index < 0 || index > 1000)
                    throw new ArgumentOutOfRangeException("Index is out of range!");

                //等待后台线程释放控制权
                _evRecv.WaitOne();

                cmd = System.Text.Encoding.Default.GetBytes("@MOVE P,P" + index.ToString("0") + "\r\n");

                _socket.Send(cmd);

                _socket.Receive(buffer);
                data = Encoding.ASCII.GetString(buffer).Replace('\0', ' ').Trim();

                if (data != "RUN")
                    throw new Exception("Command error!");

                _bReady = false;
                _bWaitingEnd = true;
            }
            catch (SocketException e)
            {
                result = false;
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                _evRecv.Set();
            }

            return result;
        }
        /// <summary>
        /// 寸动
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public bool Step(int Axis, bool dir)
        {
            bool result = true;

            byte[] buffer = new byte[10];
            byte[] cmd = new byte[1] { 0x03 };
            string data;
            string d = dir ? "+" : "-";
            if (Axis < 1 || Axis > 4) { return false; }
            cmd = Encoding.Default.GetBytes("@INCHXY " + Axis.ToString() + d + "\r\n");
            try
            {
                if (_bJogging)
                    throw new Exception("Jogging!");

                if (_bWaitingEnd)
                    throw new Exception("Waitting end!");

                if (!_socket.Connected || !_bConnected)
                    throw new Exception("Connection is loss!");

                //等待后台线程释放控制权
                _evRecv.WaitOne();

                _socket.Send(cmd);

                _socket.Receive(buffer);
                data = Encoding.ASCII.GetString(buffer).Replace('\0', ' ').Trim();

                if (data != "RUN")
                    throw new Exception("Command error!");

                _bReady = false;
                _bWaitingEnd = true;

            }
            catch (SocketException e)
            {
                result = false;
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                _evRecv.Set();
            }

            return result;
        }
        /// <summary>
        /// 停止
        /// </summary>
        /// <returns></returns>
        public bool MovStop()
        {
            bool result = true;

            byte[] buffer = new byte[10];
            byte[] cmd = new byte[1] { 0x03 };
            string data;

            try
            {
                if (!_bJogging && !_bWaitingEnd)
                    throw new Exception("Not Jogging!");

                if (!_socket.Connected || !_bConnected)
                    throw new Exception("Connection is loss!");

                //等待后台线程释放控制权
                _evRecv.WaitOne();

                _socket.Send(cmd);

                _bWaitingEnd = false;

                if (_bJogging)
                {
                    _socket.Receive(buffer);
                    data = Encoding.ASCII.GetString(buffer).Replace('\0', ' ').Trim();

                    //这里接收的是Jog的反馈
                    if (data != "END")
                        throw new Exception("Command error!");

                    _bJogging = false;
                    _bReady = true;
                }


            }
            catch (SocketException e)
            {
                result = false;
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                _evRecv.Set();
            }

            return result;
        }

        #endregion ************************************************************************************* 



        #region ***************************************参数设置函数**************************************
        //索引从1开始
        public bool WriteUserDO(int DO_Num, bool DO_State)
        {
            bool result = true;
            bool recvok = false;

            byte[] buffer = new byte[20];
            byte[] cmd = new byte[] { 0x03 };
            string data;

            try
            {
                if (!_socket.Connected || !_bConnected)
                    throw new Exception("Connection is loss!");

                if (DO_Num < 0 || DO_Num > 7)
                    throw new Exception("DI is out of range!");

                //等待后台线程释放控制权
                _evRecv.WaitOne();

                int group = DO_Num / 8 + 2;   //从组2开始
                int index = DO_Num % 8 - 1;

                cmd = System.Text.Encoding.Default.GetBytes("@DO" + group.ToString("0") + "(" + index.ToString("0") + ")=" + (DO_State ? "1" : "0") + "\r\n");

                _socket.Send(cmd);

                //开始启动接收数据  有数据 OK END以及END夹在OK或者数据中
                int count = 0;
                do
                {
                    buffer = new byte[20];
                    _socket.Receive(buffer);
                    data = Encoding.ASCII.GetString(buffer).Replace('\0', ' ').Trim();
                    data = data.Replace("\r\n", " ").Trim();

                    if (data == null)
                        throw new Exception("Command error!");

                    //解析数据
                    string[] sArray = data.Split(new char[] { ' ', ',' });

                    if (data.Contains("OK"))
                        recvok = true;

                    if (data.Contains("END"))
                    {
                        _bWaitingEnd = false;
                        _bReady = true;
                    }

                    if (data.Contains("NG"))
                        throw new Exception("Command failed!");

                    count++;
                    if (recvok)
                        count = 2;

                } while (count < 2);

                if (!recvok)
                    result = false;
            }
            catch (SocketException e)
            {
                result = false;
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                _evRecv.Set();
            }

            return result;
        }
        /// <summary>
        /// 报警复位
        /// </summary>
        /// <returns></returns>
        public bool ResetAlarm()
        {
            bool result = true;
            bool recvok = false;    //是否收到OK
            byte[] buffer;

            byte[] cmd = new byte[1] { 0x03 };
            string data;

            try
            {
                if (!_socket.Connected || !_bConnected)
                    throw new Exception("Connection is loss!");

                //等待后台线程释放控制权
                _evRecv.WaitOne();

                cmd = System.Text.Encoding.Default.GetBytes("@ALMRST" + "\r\n"); ;

                _socket.Send(cmd);

                //开始启动接收数据  有数据 OK END以及END夹在OK或者数据中
                int count = 0;
                do
                {
                    buffer = new byte[20];
                    _socket.Receive(buffer);
                    data = Encoding.ASCII.GetString(buffer).Replace('\0', ' ').Trim();
                    data = data.Replace("\r\n", " ").Trim();

                    if (data == null)
                        throw new Exception("Command error!");

                    //解析数据
                    string[] sArray = data.Split(new char[] { ' ', ',' });

                    if (data.Contains("OK"))
                        recvok = true;

                    if (data.Contains("END"))
                    {
                        _bWaitingEnd = false;
                        _bReady = true;
                    }

                    if (data.Contains("NG"))
                        throw new Exception("Command failed!");

                    count++;
                    if (recvok)
                        count = 2;

                } while (count < 2);

                if (!recvok)
                    result = false;
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                _evRecv.Set();
            }

            return result;
        }
        /// <summary>
        /// 伺服使能关闭
        /// </summary>
        /// <returns></returns>
        public bool ServoOFF()
        {
            bool result = true;

            byte[] buffer1 = new byte[10];
            byte[] buffer2 = new byte[10];

            byte[] cmd = new byte[1] { 0x03 };
            string data;

            try
            {
                if (_bJogging)
                    throw new Exception("Jogging!");

                if (_bWaitingEnd)
                    throw new Exception("Waitting end!");

                if (!_socket.Connected || !_bConnected)
                    throw new Exception("Connection is loss!");

                //等待后台线程释放控制权
                _evRecv.WaitOne();

                cmd = System.Text.Encoding.Default.GetBytes("@MOTOR OFF" + "\r\n");

                _socket.Send(cmd);

                _socket.Receive(buffer1);
                data = Encoding.ASCII.GetString(buffer1).Replace('\0', ' ').Trim();

                if (data != "RUN")
                    throw new Exception("Command error!");

                _socket.Receive(buffer2);
                data = Encoding.ASCII.GetString(buffer2).Replace('\0', ' ').Trim();

                if (data != "END")
                    throw new Exception("Command error!");
            }
            catch (SocketException e)
            {
                result = false;
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                _evRecv.Set();
            }

            return result;

        }
        /// <summary>
        /// 伺服使能开启
        /// </summary>
        /// <returns></returns>
        public bool ServoON()
        {
            bool result = true;

            byte[] buffer1 = new byte[10];
            byte[] buffer2 = new byte[10];

            byte[] cmd = new byte[1] { 0x03 };
            string data;

            try
            {
                if (_bJogging)
                    throw new Exception("Jogging!");

                if (_bWaitingEnd)
                    throw new Exception("Waitting end!");

                if (!_socket.Connected || !_bConnected)
                    throw new Exception("Connection is loss!");

                //等待后台线程释放控制权
                _evRecv.WaitOne();

                cmd = System.Text.Encoding.Default.GetBytes("@MOTOR ON" + "\r\n");

                _socket.Send(cmd);

                _socket.Receive(buffer1);
                data = Encoding.ASCII.GetString(buffer1).Replace('\0', ' ').Trim();

                if (data != "RUN")
                    throw new Exception("Command error!");

                _socket.Receive(buffer2);
                data = Encoding.ASCII.GetString(buffer2).Replace('\0', ' ').Trim();

                if (data != "END")
                    throw new Exception("Command error!");
            }
            catch (SocketException e)
            {
                result = false;
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                _evRecv.Set();
            }

            return result;
        }
        /// <summary>
        /// 更改寸动距离
        /// </summary>
        /// <param name="distance"></param>
        /// <returns></returns>
        public bool SetCartesianDistance(int distance)
        {
            bool result = true;
            bool recvok = false;    //是否收到OK
            byte[] buffer;

            byte[] cmd = new byte[1] { 0x03 };
            string data;

            try
            {
                if (!_socket.Connected || !_bConnected)
                    throw new Exception("Connection is loss!");

                if (distance > 1000 * 50 || distance < 5)
                    throw new ArgumentException("Distance is out of range!");

                //等待后台线程释放控制权
                _evRecv.WaitOne();

                cmd = System.Text.Encoding.Default.GetBytes("@IDIST " + distance.ToString("0") + "\r\n");

                _socket.Send(cmd);

                //开始启动接收数据  有数据 OK END以及END夹在OK或者数据中
                int count = 0;
                do
                {
                    buffer = new byte[20];
                    _socket.Receive(buffer);
                    data = Encoding.ASCII.GetString(buffer).Replace('\0', ' ').Trim();
                    data = data.Replace("\r\n", " ").Trim();

                    if (data == null)
                        throw new Exception("Command error!");

                    //解析数据
                    string[] sArray = data.Split(new char[] { ' ', ',' });

                    if (data.Contains("OK"))
                        recvok = true;

                    if (data.Contains("END"))
                    {
                        _bWaitingEnd = false;
                        _bReady = true;
                    }

                    if (data.Contains("NG"))
                        throw new Exception("Command failed!");

                    count++;
                    if (recvok)
                        count = 2;

                } while (count < 2);

                if (!recvok)
                    result = false;
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                _evRecv.Set();
            }

            return result;
        }

        public bool SetAcc(int Acc)
        {
            bool result = true;
            bool recvok = false;    //是否收到OK
            byte[] buffer;

            byte[] cmd = new byte[1] { 0x03 };
            string data;

            try
            {
                if (!_socket.Connected || !_bConnected)
                    throw new Exception("Connection is loss!");

                if (Acc > 100 || Acc < 3)
                    throw new ArgumentException("Speed is out of range!");

                //等待后台线程释放控制权
                _evRecv.WaitOne();

                cmd = System.Text.Encoding.Default.GetBytes("@ACCEL " + Acc.ToString("0") + "\r\n");

                _socket.Send(cmd);

                //开始启动接收数据  有数据 OK END以及END夹在OK或者数据中
                int count = 0;
                do
                {
                    buffer = new byte[20];
                    _socket.Receive(buffer);
                    data = Encoding.ASCII.GetString(buffer).Replace('\0', ' ').Trim();
                    data = data.Replace("\r\n", " ").Trim();

                    if (data == null)
                        throw new Exception("Command error!");

                    //解析数据
                    string[] sArray = data.Split(new char[] { ' ', ',' });

                    if (data.Contains("OK"))
                        recvok = true;

                    if (data.Contains("END"))
                    {
                        _bWaitingEnd = false;
                        _bReady = true;
                    }

                    if (data.Contains("NG"))
                        throw new Exception("Command failed!");

                    count++;
                    if (recvok)
                        count = 2;

                } while (count < 2);

                if (!recvok)
                    result = false;
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                _evRecv.Set();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Dec"></param>
        /// <returns>运动过程中（Jog、Move），返回false；执行完成返回true</returns>
        public bool SetDec(int Dec)
        {
            bool result = true;
            bool recvok = false;    //是否收到OK
            byte[] buffer;

            byte[] cmd = new byte[1] { 0x03 };
            string data;

            try
            {
                if (!_socket.Connected || !_bConnected)
                    throw new Exception("Connection is loss!");

                if (Dec > 100 || Dec < 3)
                    throw new ArgumentException("Speed is out of range!");

                //等待后台线程释放控制权
                _evRecv.WaitOne();

                cmd = System.Text.Encoding.Default.GetBytes("@DECEL " + Dec.ToString("0") + "\r\n");

                _socket.Send(cmd);

                //开始启动接收数据  有数据 OK END以及END夹在OK或者数据中
                int count = 0;
                do
                {
                    buffer = new byte[20];
                    _socket.Receive(buffer);
                    data = Encoding.ASCII.GetString(buffer).Replace('\0', ' ').Trim();
                    data = data.Replace("\r\n", " ").Trim();

                    if (data == null)
                        throw new Exception("Command error!");

                    //解析数据
                    string[] sArray = data.Split(new char[] { ' ', ',' });

                    if (data.Contains("OK"))
                        recvok = true;

                    if (data.Contains("END"))
                    {
                        _bWaitingEnd = false;
                        _bReady = true;
                    }

                    if (data.Contains("NG"))
                        throw new Exception("Command failed!");


                    count++;
                    if (recvok)
                        count = 2;

                } while (count < 2);

                if (!recvok)
                    result = false;
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                _evRecv.Set();
            }

            return result;
        }
        /// <summary>
        /// 更改手动速度
        /// </summary>
        /// <param name="Speed"></param>
        /// <returns></returns>
        public bool SetMSpeed(int Speed)
        {
            bool result = true;
            bool recvok = false;    //是否收到OK
            byte[] buffer;

            byte[] cmd = new byte[1] { 0x03 };
            string data;

            try
            {
                if (!_socket.Connected || !_bConnected)
                    throw new Exception("Connection is loss!");

                if (Speed > 100 || Speed < 3)
                    throw new ArgumentException("Speed is out of range!");

                //等待后台线程释放控制权
                _evRecv.WaitOne();

                cmd = System.Text.Encoding.Default.GetBytes("@MSPEED " + Speed.ToString("0") + "\r\n");

                _socket.Send(cmd);

                //开始启动接收数据  有数据 OK END以及END夹在OK或者数据中
                int count = 0;
                do
                {
                    buffer = new byte[20];
                    _socket.Receive(buffer);
                    data = Encoding.ASCII.GetString(buffer).Replace('\0', ' ').Trim();
                    data = data.Replace("\r\n", " ").Trim();

                    if (data == null)
                        throw new Exception("Command error!");

                    //解析数据
                    string[] sArray = data.Split(new char[] { ' ', ',' });

                    if (data.Contains("OK"))
                        recvok = true;

                    if (data.Contains("END"))
                    {
                        _bWaitingEnd = false;
                        _bReady = true;
                    }

                    if (data.Contains("NG"))
                        throw new Exception("Command failed!");

                    count++;
                    if (recvok)
                        count = 2;

                } while (count < 2);

                if (!recvok)
                    result = false;
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                _evRecv.Set();
            }

            return result;
        }
        /// <summary>
        /// 更改自动速度
        /// </summary>
        /// <param name="Speed"></param>
        /// <returns></returns>
        public bool SetASpeed(int Speed)
        {
            bool result = true;
            bool recvok = false;    //是否收到OK
            byte[] buffer;

            byte[] cmd = new byte[1] { 0x03 };
            string data;

            try
            {
                if (!_socket.Connected || !_bConnected)
                    throw new Exception("Connection is loss!");

                if (Speed > 100 || Speed < 3)
                    throw new ArgumentException("Speed is out of range!");

                //等待后台线程释放控制权
                _evRecv.WaitOne();

                cmd = System.Text.Encoding.Default.GetBytes("@ASPEED " + Speed.ToString("0") + "\r\n");

                _socket.Send(cmd);

                //开始启动接收数据  有数据 OK END以及END夹在OK或者数据中
                int count = 0;
                do
                {
                    buffer = new byte[20];
                    _socket.Receive(buffer);
                    data = Encoding.ASCII.GetString(buffer).Replace('\0', ' ').Trim();
                    data = data.Replace("\r\n", " ").Trim();

                    if (data == null)
                        throw new Exception("Command error!");

                    //解析数据
                    string[] sArray = data.Split(new char[] { ' ', ',' });

                    if (data.Contains("OK"))
                        recvok = true;

                    if (data.Contains("END"))
                    {
                        _bWaitingEnd = false;
                        _bReady = true;
                    }

                    if (data.Contains("NG"))
                        throw new Exception("Command failed!");

                    count++;
                    if (recvok)
                        count = 2;

                } while (count < 2);

                if (!recvok)
                    result = false;
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                _evRecv.Set();
            }

            return result;
        }

        public bool TeachGlobalPoint(int index)
        {
            bool result = true;
            bool recvok = false;    //是否收到OK
            byte[] buffer;

            byte[] cmd = new byte[1] { 0x03 };
            string data;

            try
            {
                if (!_socket.Connected || !_bConnected)
                    throw new Exception("Connection is loss!");

                if (index > 1000 || index < 0)
                    throw new ArgumentException("index is out of range!");

                //等待后台线程释放控制权
                _evRecv.WaitOne();

                cmd = System.Text.Encoding.Default.GetBytes("@TCHXY " + index.ToString("0") + "\r\n");

                _socket.Send(cmd);

                //开始启动接收数据  有数据 OK END以及END夹在OK或者数据中
                int count = 0;
                do
                {
                    buffer = new byte[20];
                    _socket.Receive(buffer);
                    data = Encoding.ASCII.GetString(buffer).Replace('\0', ' ').Trim();
                    data = data.Replace("\r\n", " ").Trim();

                    if (data == null)
                        throw new Exception("Command error!");

                    //解析数据
                    string[] sArray = data.Split(new char[] { ' ', ',' });

                    if (data.Contains("OK"))
                        recvok = true;

                    if (data.Contains("END"))
                    {
                        _bWaitingEnd = false;
                        _bReady = true;
                    }

                    if (data.Contains("NG"))
                        throw new Exception("Command failed!");

                    count++;
                    if (recvok)
                        count = 2;

                } while (count < 2);

                if (!recvok)
                    result = false;
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                _evRecv.Set();
            }

            return result;

        }

        #endregion ************************************************************************************



        #region ***************************************数据读写函数***********************************
        /// <summary>
        /// 读取P0-19.XYZR的值
        /// </summary>
        /// <param name="index"></param>
        /// <param name="len"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool ReadData(int index, int len, out double[] data)
        {
            bool result = true;
            int point = 0;
            int col = 0;

            data = new double[len];

            try
            {
                if (!_socket.Connected || !_bConnected)
                    throw new Exception("Connection is loss!");

                if (index > 20 || index < 0)
                    throw new ArgumentException("Indes is out of range!");

                if (len > 20 || len < 1)
                    throw new ArgumentException("Len is out of range!");

                //等待后台线程释放控制权
                _evRecv.WaitOne();

                for (int i = 0; i < len; i++)
                {
                    point = index / 5 + _iDataStartIndex;
                    col = index % 4;

                    if (!ReadDouble(point, col, out data[i]))
                        throw new Exception("Read Error!");
                }
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                _evRecv.Set();
            }

            return result;
        }
        /// <summary>
        /// 写入P0-19.XYZR的值
        /// </summary>
        /// <param name="index"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool WriteData(int index, double[] data)
        {
            bool result = true;
            int point = 0;
            int col = 0;

            try
            {
                if (!_socket.Connected || !_bConnected)
                    throw new Exception("Connection is loss!");

                if (index > 20 || index < 0)
                    throw new ArgumentException("Indes is out of range!");

                //等待后台线程释放控制权
                _evRecv.WaitOne();

                for (int i = 0; i < data.Length; i++)
                {
                    point = index / 5 + _iDataStartIndex;
                    col = index % 4;

                    if (!WriteDouble(point, col, data[i]))
                        throw new Exception("Write Error!");
                }
            }
            catch (Exception e)
            {
                result = false;
            }
            finally
            {
                _evRecv.Set();
            }

            return result;
        }

        #endregion **************************************************************************************



        #region ***************************************数据读写辅助函数***********************************

        private int _iDataStartIndex = 50; //数据读写起始点位
        /// <summary>
        /// 获取某个位置P？的对应轴的数据
        /// </summary>
        /// <param name="point">点位P</param>
        /// <param name="col">轴号</param>
        /// <param name="dat">返回数据</param>
        /// <returns></returns>
        private bool ReadDouble(int point, int col, out double dat)
        {
            bool result = true;

            bool recvok = false;    //是否收到OK
            bool recvdata = false;  //是否收到数据

            byte[] cmd = new byte[] { 0x03 };
            string data;

            byte[] buffer;

            dat = 0;

            try
            {
                cmd = Encoding.Default.GetBytes("@?LOC" + col.ToString("0") + "(P" + point.ToString("0") + ")" + "\r\n");

                _socket.Send(cmd);

                //开始启动接收数据  有数据 OK END以及END夹在OK或者数据中
                int count = 0;
                do
                {
                    buffer = new byte[20];
                    _socket.Receive(buffer);
                    data = Encoding.ASCII.GetString(buffer).Replace('\0', ' ').Trim();
                    data = data.Replace("\r\n", " ").Trim();

                    if (data == null)
                        throw new Exception("Command error!");

                    //解析数据
                    string[] sArray = data.Split(new char[] { ' ', ',' });

                    if (data.Contains("OK"))
                        recvok = true;

                    if (data.Contains("END"))
                    {
                        _bWaitingEnd = false;
                        _bReady = true;
                    }

                    if (data.Contains("NG"))
                        throw new Exception("Command failed!");

                    double temp;
                    for (int i = 0; i < sArray.Length; i++)
                    {
                        if (double.TryParse(sArray[i], out temp))
                        {
                            recvdata = true;
                            dat = temp;
                            break;
                        }
                    }


                    count++;
                    if (recvok && recvdata)
                        count = 3;

                } while (count < 3);


                if (!recvok || !recvdata)
                    return false;
            }
            catch (Exception e)
            {
                result = false;
            }

            return result;
        }
        /// <summary>
        /// 设置某个位置P？的对应轴的数据
        /// </summary>
        /// <param name="point">点位P</param>
        /// <param name="col">轴号</param>
        /// <param name="dat">设置数据</param>
        /// <returns></returns>
        private bool WriteDouble(int point, int col, double dat)
        {
            bool result = true;

            bool recvok = false;    //是否收到OK

            byte[] cmd = new byte[] { 0x03 };
            string data;

            byte[] buffer;

            try
            {
                cmd = System.Text.Encoding.Default.GetBytes("@LOC" + col.ToString("0") + "(P" + point.ToString("0") + ")=" + dat.ToString("0.000") + "\r\n");

                _socket.Send(cmd);

                //开始启动接收数据  有数据 OK END以及END夹在OK或者数据中
                int count = 0;
                do
                {
                    buffer = new byte[20];
                    _socket.Receive(buffer);
                    data = Encoding.ASCII.GetString(buffer).Replace('\0', ' ').Trim();
                    data = data.Replace("\r\n", " ").Trim();

                    if (data == null)
                        throw new Exception("Command error!");

                    //解析数据
                    string[] sArray = data.Split(new char[] { ' ', ',' });

                    if (data.Contains("OK"))
                        recvok = true;

                    if (data.Contains("END"))
                    {
                        _bWaitingEnd = false;
                        _bReady = true;
                    }

                    if (data.Contains("NG"))
                        throw new Exception("Command failed!");

                    count++;
                    if (recvok)
                        count = 2;

                } while (count < 2);


                if (!recvok)
                    return false;
            }
            catch (Exception e)
            {
                result = false;
            }

            return result;
        }

        #endregion *****************************************************************************************



        #region ***************************************状态刷新函数***********************************  
        /// <summary>
        /// 刷新DI显示
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        private bool RefreshDI(int group)
        {
            bool result = true;
            bool recvok = false;    //是否收到OK
            bool recvdata = false;  //是否收到数据

            byte[] cmd = new byte[] { 0x03 };
            string data;
            byte[] buffer;

            try
            {
                if (!_socket.Connected)
                    throw new Exception("Connection is loss!");

                cmd = System.Text.Encoding.Default.GetBytes("@?DI" + group.ToString("0") + "()" + "\r\n");

                _socket.Send(cmd);

                //开始启动接收数据  有数据 OK END以及END夹在OK或者数据中
                int count = 0;

                do
                {
                    buffer = new byte[20];

                    _socket.Receive(buffer);
                    data = Encoding.ASCII.GetString(buffer).Replace('\0', ' ').Trim();
                    data = data.Replace("\r\n", " ").Trim();

                    if (data == null)
                        throw new Exception("Command error!");

                    //解析数据
                    string[] sArray = data.Split(new char[] { ' ', ',' });

                    if (data.Contains("OK"))
                        recvok = true;

                    if (data.Contains("END"))
                    {
                        _bWaitingEnd = false;
                        _bReady = true;
                    }

                    if (data.Contains("NG"))
                        throw new Exception("Command failed!");

                    int DI = 0;

                    for (int j = 0; j < sArray.Length; j++)
                    {
                        if (int.TryParse(sArray[j], out DI))
                        {
                            short mask = 0x0001;

                            for (int k = 0; k < 8; k++)
                            {
                                if ((DI & mask) > 0)
                                    _arrDI[group * 8 + k] = true;
                                else
                                    _arrDI[group * 8 + k] = false;

                                mask = (short)(mask << 1);
                            }

                            recvdata = true;
                            break;
                        }
                    }


                    count++;
                    if (recvok)
                        count = 3;

                } while (count < 3);

                if (!recvok || !recvdata)
                    return false;

            }
            catch (SocketException e)
            {
                result = false;
            }
            catch (Exception e)
            {
                result = false;
            }

            return result;
        }
        /// <summary>
        /// 刷新DO显示
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        private bool RefreshDO(int group)
        {
            bool result = true;
            bool recvok = false;    //是否收到OK
            bool recvdata = false;  //是否收到数据

            byte[] cmd = new byte[] { 0x03 };
            string data;
            byte[] buffer;

            try
            {
                if (!_socket.Connected)
                    throw new Exception("Connection is loss!");

                cmd = System.Text.Encoding.Default.GetBytes("@?DO" + group.ToString("0") + "()" + "\r\n");

                _socket.Send(cmd);

                //开始启动接收数据  有数据 OK END以及END夹在OK或者数据中

                int count = 0;

                do
                {
                    buffer = new byte[20];

                    _socket.Receive(buffer);
                    data = Encoding.ASCII.GetString(buffer).Replace('\0', ' ').Trim();
                    data = data.Replace("\r\n", " ").Trim();

                    if (data == null)
                        throw new Exception("Command error!");

                    //解析数据
                    string[] sArray = data.Split(new char[] { ' ', ',' });

                    if (data.Contains("OK"))
                        recvok = true;

                    if (data.Contains("END"))
                    {
                        _bWaitingEnd = false;
                        _bReady = true;
                    }

                    if (data.Contains("NG"))
                        throw new Exception("Command failed!");

                    int DO = 0;

                    for (int j = 0; j < sArray.Length; j++)
                    {
                        if (int.TryParse(sArray[j], out DO))
                        {
                            short mask = 0x0001;

                            for (int k = 0; k < 8; k++)
                            {
                                if ((DO & mask) > 0)
                                    _arrDO[group * 8 + k] = true;
                                else
                                    _arrDO[group * 8 + k] = false;

                                mask = (short)(mask << 1);
                            }

                            recvdata = true;
                            break;
                        }
                    }


                    count++;
                    if (recvok)
                        count = 3;

                } while (count < 3);

                if (!recvok || !recvdata)
                    return false;

            }
            catch (SocketException e)
            {
                result = false;
            }
            catch (Exception e)
            {
                result = false;
            }

            return result;
        }
        /// <summary>
        /// 刷新系统DO显示
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        private bool RefreshSysDO(int group)
        {
            bool result = true;
            bool recvok = false;    //是否收到OK
            bool recvdata = false;  //是否收到数据

            byte[] cmd = new byte[] { 0x03 };
            string data;
            byte[] buffer;

            try
            {
                if (!_socket.Connected)
                    throw new Exception("Connection is loss!");

                cmd = System.Text.Encoding.Default.GetBytes("@?SO" + group.ToString("0") + "()" + "\r\n");

                _socket.Send(cmd);

                //开始启动接收数据  有数据 OK END以及END夹在OK或者数据中

                int count = 0;

                do
                {
                    buffer = new byte[20];

                    _socket.Receive(buffer);
                    data = Encoding.ASCII.GetString(buffer).Replace('\0', ' ').Trim();
                    data = data.Replace("\r\n", " ").Trim();

                    if (data == null)
                        throw new Exception("Command error!");

                    //解析数据
                    string[] sArray = data.Split(new char[] { ' ', ',' });

                    if (data.Contains("OK"))
                        recvok = true;

                    if (data.Contains("END"))
                    {
                        _bWaitingEnd = false;
                        _bReady = true;
                    }

                    if (data.Contains("NG"))
                        throw new Exception("Command failed!");

                    int DO = 0;

                    for (int j = 0; j < sArray.Length; j++)
                    {
                        if (int.TryParse(sArray[j], out DO))
                        {
                            short mask = 0x0001;

                            for (int k = 0; k < 8; k++)
                            {
                                if ((DO & mask) > 0)
                                    _arrSysDO[group * 8 + k] = true;
                                else
                                    _arrSysDO[group * 8 + k] = false;

                                mask = (short)(mask << 1);
                            }

                            recvdata = true;
                            break;
                        }
                    }


                    count++;
                    if (recvok)
                        count = 3;

                } while (count < 3);

                if (!recvok || !recvdata)
                    return false;

            }
            catch (SocketException e)
            {
                result = false;
            }
            catch (Exception e)
            {
                result = false;
            }

            return result;
        }
        /// <summary>
        /// 返回当前位置
        /// </summary>
        /// <returns></returns>
        private bool RefreshPosition()
        {
            bool result = true;

            bool recvok = false;    //是否收到OK
            bool recvdata = false;  //是否收到数据

            byte[] cmd = System.Text.Encoding.Default.GetBytes("@?WHRXY" + "\r\n"); ;
            string data;
            byte[] buffer;
            try
            {
                if (!_socket.Connected)
                    throw new Exception("Connection is loss!");

                _socket.Send(cmd);

                //开始启动接收数据  有数据 OK END以及END夹在OK或者数据中
                int count = 0;
                do
                {
                    buffer = new byte[100];
                    _socket.Receive(buffer);
                    data = Encoding.ASCII.GetString(buffer).Replace('\0', ' ').Trim();
                    data = data.Replace("\r\n", " ").Trim();

                    if (data == null)
                        throw new Exception("Command error!");

                    //解析数据
                    string[] sArray = data.Split(new char[] { ' ', ',' });

                    if (data.Contains("OK"))
                        recvok = true;

                    if (data.Contains("END"))
                    {
                        _bWaitingEnd = false;
                        _bReady = true;
                    }

                    if (data.Contains("NG"))
                        throw new Exception("Command failed!");

                    if (sArray.Length > 3)
                    {
                        double temp;
                        //找到第一个数据起始
                        for (int j = 0; j < sArray.Length; j++)
                        {
                            if (double.TryParse(sArray[j], out temp))//(sArray[j].Length > 4)
                            {
                                _dPosition[0] = double.Parse(sArray[j]);
                                _dPosition[1] = double.Parse(sArray[j + 1]);
                                _dPosition[2] = double.Parse(sArray[j + 2]);
                                _dPosition[3] = double.Parse(sArray[j + 3]);
                                _dPosition[4] = double.Parse(sArray[j + 4]);
                                _dPosition[5] = double.Parse(sArray[j + 5]);
                                _dPosition[6] = double.Parse(sArray[j + 6]);//手系
                                recvdata = true;
                                break;
                            }
                        }
                        CurrentPosX = _dPosition[0];
                        CurrentPosY = _dPosition[1];
                        CurrentPosZ = _dPosition[2];
                        CurrentPosR = _dPosition[3];
                    }

                    count++;

                    if (recvok)
                        count = 3;

                } while (count < 3);


                if (!recvok || !recvdata)
                    return false;

            }
            catch (SocketException e)
            {
                result = false;
            }
            catch (Exception e)
            {
                result = false;
            }

            return result;
        }
        /// <summary>
        /// 返回伺服状态
        /// </summary>
        /// <returns></returns>
        private bool RefreshServoState()
        {
            bool result = true;
            bool recvok = false;    //是否收到OK
            bool recvdata = false;  //是否收到数据

            byte[] cmd = Encoding.Default.GetBytes("@?SERVO" + "\r\n"); ;
            string data;
            Stopwatch sw = new Stopwatch();


            try
            {
                if (!_socket.Connected)
                    throw new Exception("Connection is loss!");

                _socket.Send(cmd);

                //开始启动接收数据  有数据 OK END以及END夹在OK或者数据中
                int count = 0;

                do
                {
                    byte[] buffer1 = new byte[100];
                    sw.Reset();
                    sw.Start();
                    _socket.Receive(buffer1);
                    data = Encoding.ASCII.GetString(buffer1).Replace('\0', ' ').Trim();
                    data = data.Replace("\r\n", " ").Trim();

                    if (data == null)
                        throw new Exception("Command error!");

                    //解析数据
                    string[] sArray = data.Split(new char[] { ' ', ',' });

                    if (data.Contains("OK"))
                        recvok = true;

                    if (data.Contains("END"))
                    {
                        _bWaitingEnd = false;
                        _bReady = true;
                    }

                    if (data.Contains("NG"))
                        throw new Exception("Command failed!");

                    if (sArray.Length > 3)
                    {
                        //找到第一个数据起始
                        for (int j = 0; j < sArray.Length; j++)
                        {
                            if (sArray[j].Length == 1)
                            {
                                if (sArray[j] == "1")
                                    _bServoOn = true;
                                else
                                    _bServoOn = false;
                                recvdata = true;
                                break;
                            }
                        }
                    }

                    count++;
                    if (recvok)
                        count = 3;

                } while (count < 3);


                if (!recvok || !recvdata)
                    return false;

            }
            catch (SocketException e)
            {
                result = false;
            }
            catch (Exception e)
            {
                result = false;
            }

            return result;
        }
        /// <summary>
        /// 连续JOG处理
        /// </summary>
        /// <returns></returns>
        private bool JogCmd()
        {
            bool result = true;

            byte[] buffer = new byte[10];
            byte[] cmd = new byte[1] { 0x16 };

            try
            {
                if (!_socket.Connected)
                    throw new Exception("Connection is loss!");

                _socket.Send(cmd);

            }
            catch (Exception)
            {
                result = false;
            }

            return result;

        }
        /// <summary>
        /// 刷新线程
        /// </summary>
        private void WorkThreadFunc()
        {
            while (_bWork)
            {
                if (!_bConnected)
                    break;

                try
                {
                    _evRecv.WaitOne();

                    //Step1:刷新伺服状态
                    if (!RefreshServoState())
                        throw new Exception("Refresh servo state failed!");

                    ////Step2: 刷新位置状态
                    if (!RefreshPosition())
                        throw new Exception("Refresh position failed!");

                    //Step3:刷新DI状态
                    if (!RefreshDI(2))
                        throw new Exception("Refresh DI failed!");

                    //Step4:刷新DI状态
                    if (!RefreshDI(3))
                        throw new Exception("Refresh DI failed!");

                    //Step5:刷新DO状态
                    if (!RefreshDO(2))
                        throw new Exception("Refresh DI failed!");


                    ////Step7:刷新系统DO状态
                    if (!RefreshSysDO(0))
                        throw new Exception("Refresh System DO failed!");

                    if (!RefreshSysDO(1))
                        throw new Exception("Refresh System DO failed!");


                    ////Step8:处理Jog连续命令
                    if (_bJogging)
                    {
                        if (!JogCmd())
                            throw new Exception("JogTimer failed!");
                    }
                    _evRecv.Set();       //释放控制权
                    Thread.Sleep(2);
                }
                catch (Exception e)
                {
                    //sokect连接异常时  后台线程会自动关闭  需要再次连接
                    //if(_socket!=null && _socket.Connected)
                    //{
                    //    _socket.Disconnect(false);
                    //    _socket.Close();
                    //}

                    _bConnected = false;
                    _bWork = false;

                    _evRecv.Set();          //释放控制权
                    Thread.Sleep(2);
                }
            }
        }

        #endregion *************************************************************************************

    }
}
