//using System.Toolkit.Interfaces;
namespace CMotion.Interfaces.IO
{
    /// <summary>
    ///     表示一个传感器。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISensor<out T> : IAutomatic where T : struct
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“ISensor<T>.Value”的 XML 注释
        T Value { get; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“ISensor<T>.Value”的 XML 注释
    }
}