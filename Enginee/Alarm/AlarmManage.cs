using System.Collections.Generic;
using System.Threading;

namespace System.Enginee
{
    public class AlarmManage
    {
        private object _objLock;
        /// <summary>
        /// 私有当前报警消息
        /// </summary>
        private Dictionary<string, Alarm> _dicCurrAlarmMsg;
        /// <summary>
        /// 用于添加所有的报警
        /// </summary>
        public List<Alarm> allAlarms { get; set; }
        private FormAlarm _formAlarm;
       

        public delegate void EventAlarmInsertHandler(string strAlarmKey);
        /// <summary>
        /// 添加事件
        /// </summary>
        public EventAlarmInsertHandler eventAlarmInsert;

        public AlarmManage()
        {
            allAlarms = new List<Alarm>();
            _objLock = new object();
            _dicCurrAlarmMsg = new Dictionary<string, Alarm>();


            _formAlarm = new FormAlarm(this);
            _formAlarm.TopLevel = true;
            _formAlarm.TopMost = true;

            //formAlarmManage = new FormAlarmManage();
            StartScan();
        }
       
        public bool IsAlarm
        {
            get
            {
                return _dicCurrAlarmMsg.Count > 0 ? true : false;
            }
            private set {; }
        }
        /// <summary>
        /// 获取当前报警辞典
        /// </summary>
        public Dictionary<string, Alarm> DicCurrAlarmMsg
        {
            get { return _dicCurrAlarmMsg; }
            private set {; }
        }

        /// <summary>
        /// 添加新的报警
        /// </summary>
        /// <param name="strKey"></param>
        /// <param name="strAlarmMsg"></param>
        public void InsertAlarm(Alarm alarm)
        {
            lock (_objLock)
            {
                try
                {
                    if (string.IsNullOrEmpty(alarm.AlarmKey))
                        return;

                    if (_dicCurrAlarmMsg.ContainsKey(alarm.AlarmKey))
                        return;
                    _dicCurrAlarmMsg.Add(alarm.AlarmKey, alarm);

                    if (null != eventAlarmInsert)
                    {
                        this.eventAlarmInsert(alarm.AlarmKey);
                    }
                    if (_formAlarm != null)
                        _formAlarm.ShowAlarmMsg();

                }
                catch (Exception)
                {
                }
            }
        }
        /// <summary>
        /// 移除报警
        /// </summary>
        /// <param name="strKey"></param>
        public void RemoveAlarm(Alarm alarmy)
        {
            lock (_objLock)
            {
                if (!_dicCurrAlarmMsg.ContainsKey(alarmy.AlarmKey))
                    return;
                _dicCurrAlarmMsg.Remove(alarmy.AlarmKey);
                if (_formAlarm != null)
                    _formAlarm.ShowAlarmMsg();
            }
        }
        /// <summary>
        /// 移除所有报警
        /// </summary>
        public void RemoveAllAlarm()
        {
            if (!IsAlarm)
                return;
            lock (_objLock)
            {
                try
                {
                    _dicCurrAlarmMsg.Clear();
                    if (_formAlarm != null)
                        _formAlarm.ShowAlarmMsg();

                }
                catch (Exception)
                {
                }
            }
        }
        private bool Exit = false;
        /// <summary>
        /// 资源卸载
        /// </summary>
        private void Disble()
        {
            Exit = true;
        }

        #region Auto scan thread
        private void StartScan()
        {
            Thread threadScan = new Thread(ThreadScan);
            threadScan.IsBackground = true;
            threadScan.Start();
        }
        private void ThreadScan()
        {
            while (true)
            {
                Thread.Sleep(30);
                try
                {
                    for (int i = 0; i < allAlarms.Count; i++)
                    {
                        if (allAlarms[i].IsAlarm)
                        {
                            InsertAlarm(allAlarms[i]);
                        }
                        else
                        {
                            RemoveAlarm(allAlarms[i]);
                        }
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                if (Exit)
                    break;
            }
        }
        #endregion
    }
}
