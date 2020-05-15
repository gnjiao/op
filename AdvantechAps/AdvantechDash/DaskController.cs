using System;
using System.Collections.Generic;
using CMotion.Interfaces;
using CMotion.Interfaces.IO;

namespace CMotion.AdvantechDash
{
    /// <summary>
    ///     研华科技  IO 卡控制器。
    /// </summary>
    public class DaskController : IdaskControl, ISwitchController, INeedInitialization, IDisposable
    {
#pragma warning disable CS0169 // 从不使用字段“DaskController._cardNos”
        private readonly byte[] _cardNos;
#pragma warning restore CS0169 // 从不使用字段“DaskController._cardNos”
#pragma warning disable CS0649 // 从未对字段“DaskController._devices”赋值，字段将一直保持其默认值 null
        private readonly Dictionary<int, short> _devices;
#pragma warning restore CS0649 // 从未对字段“DaskController._devices”赋值，字段将一直保持其默认值 null
        private bool _disposed;

        public DaskController()
        {
           
        }

        #region Implementation of INeedInitialization

        public override void Initialize()
        {
           
        }

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DaskController()
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
            //if (disposing)
            //{
            //    //清理托管资源
            //    if (managedResource != null)
            //    {
            //        managedResource.Dispose();
            //        managedResource = null;
            //    }
            //}
            
            // 清理非托管资源
            foreach (var device in _devices.Values)
            {
                //var ret = DASK.Release_Card((ushort) device);
                //if (ret != DASK.NoError)
                //{
                //    throw new DaskException("Unknown error.");
                //}
            }
            _disposed = true;
        }

        #endregion

        #region Implementation of ISwitchController

        public override bool Read(IoPoint ioPoint)
        {
          
            return  0> 0;
        }

        public override void Write(IoPoint ioPoint, bool value)
        {
           
        }

        #endregion

        /// <summary>
        ///     读端口数据
        /// </summary>
        /// <param name="boardNo"></param>
        /// <param name="portNo"></param>
        /// <returns></returns>
        public uint ReadPort(int boardNo, int portNo = 0)
        {
           
            return 1;
        }
    }
}