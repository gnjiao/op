﻿namespace CMotion.Interfaces.IO
{
    /// <summary>
    ///     通讯 IO 开关量控制器
    /// </summary>
    public interface IAnalogController
    {
        /// <summary>
        ///     读取开关量值。
        /// </summary>
        /// <param name="ioPoint">要读取的通讯开关量</param>
        /// <returns>读取的开关量值。</returns>
        double Read(AIoPoint ioPoint);

        /// <summary>
        ///     写入开关量值。
        /// </summary>
        /// <param name="ioPoint">要写入的通讯开关量。</param>
        /// <param name="value">要写入的开关量的值。</param>
        void Write(AIoPoint ioPoint, double value);
    }
}