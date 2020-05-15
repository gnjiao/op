using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using LogHeper;

namespace System.ToolKit
{
    public class AsynTcpClient
    {
        private Socket tcpClient;
        public AsynTcpClient(string ip, int port)
        {
            IP = ip;
            Port = port;
            tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            tcpClient.SendTimeout = 3000;
            tcpClient.ReceiveTimeout = 3000;
        }

        public string IP { get; set; }
        public int Port { get; set; }
        public bool IsConnected { get; set; }

        /// <summary>
        /// 客户端接收到数据内容
        /// </summary>
        public string strResultTCP { get; set; }
        /// <summary>
        /// 客户端接收到数据标志
        /// </summary>
        public bool IsResultTCP { get; set; }
        //{
        //    //get
        //    //{                
        //    //    return tcpClient.Connected;
        //    //}

        //}

        public void SynConnect()
        {
            //主机IP
            try
            {
                if (!tcpClient.Connected)
                {
                    tcpClient.Close();
                    tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPEndPoint serverIp = new IPEndPoint(IPAddress.Parse(IP), Port);
                    tcpClient.Connect(serverIp);
                    //LogHeper.LogHelper.Info("client-->-->" + serverIp.ToString());
                    //Console.WriteLine("client-->-->{0}", serverIp.ToString());
                    SynSend("");
                    IsConnected = tcpClient.Connected;
                }
            }
#pragma warning disable CS0168 // 声明了变量“ex”，但从未使用过
            catch (Exception ex)
#pragma warning restore CS0168 // 声明了变量“ex”，但从未使用过
            {
                IsConnected = false;
            }
        }

        #region 异步连接
        /// <summary>
        /// Tcp协议异步连接服务器
        /// </summary>
        public void AsynConnect()
        {
            //主机IP
            try
            {
                if (!tcpClient.Connected)
                {
                    tcpClient.Close();
                    tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPEndPoint serverIp = new IPEndPoint(IPAddress.Parse(IP), Port);
                    tcpClient.BeginConnect(serverIp, asyncResult =>
                    {
                        try
                        {
                            tcpClient.EndConnect(asyncResult);
                            //LogHeper.LogHelper.Info("client-->AsynConnect-->" + serverIp.ToString());                           
                            AsynSend("");
                            IsConnected = tcpClient.Connected;
                        }
                        catch (Exception ex)
                        {
                            LogHeper.LogHelper.Info(ex.ToString());
                            IsConnected = false;
                        }
                    }, null);
                }
            }
#pragma warning disable CS0168 // 声明了变量“ex”，但从未使用过
            catch (Exception ex)
#pragma warning restore CS0168 // 声明了变量“ex”，但从未使用过
            {
                //LogHelper.Info(ex.ToString());
                IsConnected = false;
            }
        }
        #endregion
        public void testsend()
        {
            try
            {
                if (!tcpClient.Connected)
                {
                    IsConnected = false;
                    AsynConnect();
                }
                else 
                { 
                    AsynSend("");
                    AsynRecive();
                }

            }
#pragma warning disable CS0168 // 声明了变量“ex”，但从未使用过
            catch (Exception ex)
#pragma warning restore CS0168 // 声明了变量“ex”，但从未使用过
            {
                IsConnected = false;
                AsynConnect();
            }
        }
        #region 异步接受消息
        /// <summary>
        /// 异步连接客户端回调函数
        /// </summary>
        /// <param name="tcpClient"></param>
        public string AsynRecive()
        {
            var watchCT = new Stopwatch();
            var data = new byte[1024];
            try
            {
                if (tcpClient.Connected)
                {
                    tcpClient.BeginReceive(data, 0, data.Length, SocketFlags.None, asyncResult =>
                    {
                        try
                        {
                            int length = tcpClient.EndReceive(asyncResult);
                            strResultTCP = Encoding.UTF8.GetString(data);
                            //LogHelper.Info("client<--<--server:" + strResultTCP);
                            IsResultTCP = true;
                        }
                        catch (Exception)
                        {
                            IsConnected = false;
                            IsResultTCP = false;                         
                        }
                       
                    }, null);

                }
                else
                {
                    LogHelper.Info("通信掉线");
                    IsConnected = false;
                }
            }
            catch
            {
                LogHelper.Info("通信访问异常");
            }
            return Encoding.UTF8.GetString(data);
        }

        public string SynRecive()
        {
            var watchCT = new Stopwatch();
            var data = new byte[1024];
            try
            {
                if (tcpClient.Connected)
                {
                    int byteCount = tcpClient.Receive(data, SocketFlags.None);
                    strResultTCP = Encoding.UTF8.GetString(data);
                    LogHelper.Info(strResultTCP);
                }
                else
                {
                    LogHelper.Info("通信掉线");
                }
            }
            catch
            {
                LogHelper.Info("通信访问异常");
            }
            return strResultTCP;
        }
        #endregion

        public void SynSend(string message)
        {
            try
            {
                if (tcpClient.Connected)
                {
                    var data = Encoding.UTF8.GetBytes(message);
                    tcpClient.Send(data, 0, data.Length, SocketFlags.None);
                }
                else
                {
                    LogHelper.Info("通信掉线");
                }
            }
#pragma warning disable CS0168 // 声明了变量“ex”，但从未使用过
            catch (Exception ex)
#pragma warning restore CS0168 // 声明了变量“ex”，但从未使用过
            {
                LogHelper.Info("通信访问异常");
            }

        }

        #region 异步发送消息
        /// <summary>
        /// 异步发送消息
        /// </summary>
        /// <param name="tcpClient">客户端套接字</param>
        /// <param name="message">发送消息</param>
        public void AsynSend(string message)
        {
            try
            {

                if (tcpClient.Connected)
                {
                    var data = Encoding.UTF8.GetBytes(message);
                    tcpClient.BeginSend(data, 0, data.Length, SocketFlags.None, asyncResult =>
                    {
                        //完成发送消息

                        int length = tcpClient.EndSend(asyncResult);
                        Console.WriteLine("client-->-->server:{0}", message);
                        if (!message.Contains("Online")) { }
                        LogHelper.Info("client-->-->server:" + message);
                    }, null);

                }
                else
                {
                    LogHelper.Info("通信掉线");
                }
            }
#pragma warning disable CS0168 // 声明了变量“ex”，但从未使用过
            catch (Exception ex)
#pragma warning restore CS0168 // 声明了变量“ex”，但从未使用过
            {
                LogHelper.Info("通信访问异常");
            }

        }
        #endregion
    }
}
