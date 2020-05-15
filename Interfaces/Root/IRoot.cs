namespace CMotion.Interfaces
{
    public abstract class IRoot
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public abstract bool Initialize();
        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        public abstract bool Close();
        /// <summary>
        /// 连接状态
        /// </summary>
        /// <returns></returns>
        public abstract bool IsConnected();


    }
}
