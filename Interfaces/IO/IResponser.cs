//using System.Toolkit.Interfaces;
namespace CMotion.Interfaces.IO
{
    /// <summary>
    ///     表示一个响应器。
    /// </summary>
    public interface IResponser<T> : IAutomatic where T : struct
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IResponser<T>.Value”的 XML 注释
        bool Value { set; }
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IResponser<T>.Value”的 XML 注释
    }
}