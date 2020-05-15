using System;
using CMotion.Interfaces;
using CMotion.Interfaces.IO;
using System.Collections.Generic;

namespace CMotion.AdlinkDash
{
    /// <summary>
    ///     凌华科技  IO 卡控制器。
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
            var device = DASK.Register_Card(Type, 0);
            if (device < 0)
            {
                switch (device)
                {
                    case DASK.ErrorTooManyCardRegistered:
                        throw new DaskException("32 cards have been registered.");
                    case DASK.ErrorUnknownCardType:
                        throw new DaskException(string.Format("The CardType argument {0} is not valid.", DASK.PCI_7432));
                    case DASK.ErrorOpenDriverFailed:
                        throw new DaskException("Failed to open the device driver.");
                    case DASK.ErrorOpenEventFailed:
                        throw new DaskException("Open event failed in device driver.");
                    default:
                        throw new DaskException("Unknown error.");
                }
            }
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
                var ret = DASK.Release_Card((ushort)device);
                if (ret != DASK.NoError)
                {
                    throw new DaskException("Unknown error.");
                }
            }
            _disposed = true;
        }

        #endregion

        #region Implementation of ISwitchController
        public override bool Read(IoPoint ioPoint)
        {
            var device = _devices[ioPoint.BoardNo];
            ushort value = 0;
            var ret = 0;
            if ((ioPoint.IoMode & IoModes.Senser) != 0)
            {
                ret = DASK.DI_ReadLine((ushort)device, 0, (ushort)ioPoint.PortNo, out value);
            }
            else
            {
                ret = DASK.DO_ReadLine((ushort)device, 0, (ushort)ioPoint.PortNo, out value);
            }
            if (ret != DASK.NoError)
            {
                switch (ret)
                {
                    case DASK.ErrorInvalidCardNumber:
                        throw new DaskException(string.Format("The CardNumber argument {0} is out of range (larger than 31).", device));
                    case DASK.ErrorCardNotRegistered:
                        throw new DaskException(string.Format("No card registered as {0} CardNumber.", device));
                    case DASK.ErrorFuncNotSupport:
                        throw new DaskException(string.Format("The {0} function called is not supported by this type of card.", "DO_WriteLine"));
                    case DASK.ErrorInvalidIoChannel:
                        throw new DaskException(string.Format("The specified Channel or Port argument {0} is out of range.", ioPoint.PortNo));
                    default:
                        throw new DaskException("Unknown error.");
                }
            }
            return value > 0;
        }

        public override void Write(IoPoint ioPoint, bool value)
        {
            var device = _devices[ioPoint.BoardNo];
            var ret = DASK.DO_WriteLine((ushort)device, 0, (ushort)ioPoint.PortNo, (ushort)(value ? 1 : 0));
            if (ret != DASK.NoError)
            {
                switch (ret)
                {
                    case DASK.ErrorInvalidCardNumber:
                        throw new DaskException(string.Format("The CardNumber argument {0} is out of range (larger than 31).", device));
                    case DASK.ErrorCardNotRegistered:
                        throw new DaskException(string.Format("No card registered as {0} CardNumber.", device));
                    case DASK.ErrorFuncNotSupport:
                        throw new DaskException(string.Format("The {0} function called is not supported by this type of card.", "DO_WriteLine"));
                    case DASK.ErrorInvalidIoChannel:
                        throw new DaskException(string.Format("The specified Channel or Port argument {0} is out of range.", ioPoint.PortNo));
                    default:
                        throw new DaskException("Unknown error.");
                }
            }
        }

        #endregion
       
    }
}