//using System.Toolkit.Interfaces;
namespace CMotion.Interfaces.IO
{
    /// <summary>
    ///     ��ʾһ����Ӧ����
    /// </summary>
    public interface IResponser<T> : IAutomatic where T : struct
    {
#pragma warning disable CS1591 // ȱ�ٶԹ����ɼ����ͻ��Ա��IResponser<T>.Value���� XML ע��
        bool Value { set; }
#pragma warning restore CS1591 // ȱ�ٶԹ����ɼ����ͻ��Ա��IResponser<T>.Value���� XML ע��
    }
}