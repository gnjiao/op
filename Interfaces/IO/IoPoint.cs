using System;
using System.Diagnostics;

namespace CMotion.Interfaces.IO
{
    /// <summary>
    ///     表示一个通讯开关量。
    /// </summary>
    public class IoPoint : Automatic
    {
        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="controller">通讯 IO 控制器。</param>
        /// <param name="boardNo"></param>
        /// <param name="portNo"></param>
        /// <param name="ioMode">通讯模式<see cref="IoModes" /></param>
        public IoPoint(ISwitchController controller, int boardNo,int portNo, IoModes ioMode)
        {
            Controller = controller;
            BoardNo = boardNo;
            PortNo = portNo;
            IoMode = ioMode;
        }

        /// <summary>
        ///     读写控制器
        /// </summary>
        protected ISwitchController Controller { get; private set; }

        /// <summary>
        ///     板卡号
        /// </summary>
        public int BoardNo { get; set; }
        /// <summary>
        ///     通道序号
        /// </summary>
        public int PortNo { get; set; }

        /// <summary>
        ///     通讯模式
        /// </summary>
        public IoModes IoMode { get; set; }

        /// <summary>
        ///     信号量值
        /// </summary>
        public bool Value
        {
            get
            {
                VerifyIoPoint(IoModes.Senser);
                return Controller.Read(this);
            }
            set
            {
                VerifyIoPoint(IoModes.Responser);
                Controller.Write(this, value);
            }
        }

        #region Overrides of Object

#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IoPoint.ToString()”的 XML 注释
        public override string ToString()
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IoPoint.ToString()”的 XML 注释
        {
            return string.Format("IoPoint[{0},{1}]", BoardNo, PortNo);
        }

        #endregion

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IoPoint.VerifyIoPoint(IoModes)”的 XML 注释
        public void VerifyIoPoint(IoModes ioMode)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IoPoint.VerifyIoPoint(IoModes)”的 XML 注释
        {
            if ((IoMode & ioMode) == 0)
                throw new InvalidOperationException(String.Format("非法{0}操作{1}开关量：Board:{2} Index:{3}", ioMode, IoMode, BoardNo, PortNo));
        }
    }
}