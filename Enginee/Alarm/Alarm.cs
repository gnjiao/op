namespace System.Enginee
{
    /// <summary>
    /// 报警信息判断
    /// </summary>
    public class Alarm
    {

        /// <summary>
        /// 故障代码
        /// </summary>
        public string AlarmKey { get; set; }

        /// <summary>
        /// 故障消息
        /// </summary>      
        public string AlarmMsg { get; set; }
        /// <summary>
        /// 故障处理方法
        /// </summary>      
        public string AlarmRemark { get; set; }

        /// <summary>
        /// 故障时间
        /// </summary>      
        public DateTime AlarmTime { get; set; }

        private readonly Func<bool> _condition;
        public Alarm(Func<bool> condition)
        {
            _condition = condition;
            AlarmKey = "000";
            Name = " ";
            AlarmMsg = " ";
            AlarmTime = DateTime.Now;
        }
        /// <summary>
        ///     报警级别
        /// </summary>
        public AlarmLevels AlarmLevel { get; set; }
        /// <summary>
        /// 是否报警
        /// </summary>
        public bool IsAlarm
        {
            get
            {
                try
                {
                    return _condition();
                }
                catch (Exception)
                {
                    return true;
                }
            }
        }
        /// <summary>
        /// 报警名称
        /// </summary>
        public string Name { get; set; }
    }
    /// <summary>
    /// 报警类型
    /// </summary>
    public struct AlarmType
    {
        public bool IsAlarm;
        public bool IsPrompt;
        public bool IsWarning;
    }
}
