using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace System.Enginee.Formview
{
   public  class MessageList
    {
        //private ListBox ListBox;

        //public AlarmType AlarmCheck(IList<Alarm> Alarms)
        //{
        //    var Alarm = new AlarmType();
        //    foreach (Alarm alarm in Alarms)
        //    {
        //        var btemp = alarm.IsAlarm;
        //        if (alarm.AlarmLevel == AlarmLevels.Error)
        //        {
        //            Alarm.IsAlarm |= btemp;
        //            this.Invoke(new Action(() =>
        //            {
        //                Msg(string.Format("{0},{1}", alarm.AlarmLevel.ToString(), alarm.Name), btemp);
        //            }));
        //        }
        //        else if (alarm.AlarmLevel == AlarmLevels.None)
        //        {
        //            Alarm.IsPrompt |= btemp;
        //            this.Invoke(new Action(() =>
        //            {
        //                Msg(string.Format("{0},{1}", alarm.AlarmLevel.ToString(), alarm.Name), btemp);
        //            }));
        //        }
        //        else
        //        {
        //            Alarm.IsWarning |= btemp;
        //            this.Invoke(new Action(() =>
        //            {
        //                Msg(string.Format("{0},{1}", alarm.AlarmLevel.ToString(), alarm.Name), btemp);
        //            }));
        //        }
        //    }
        //    return Alarm;
        //}
        //private void Msg(string str, bool value)
        //{
        //    string tempstr = null;
        //    bool sign = false;
        //    try
        //    {
        //        var arrRight = new List<object>();
        //        foreach (var tmpist in ListBox.Items) arrRight.Add(tmpist);
        //        if (value)
        //        {
        //            foreach (string tmplist in arrRight)
        //            {
        //                if (tmplist.IndexOf("-") > -1)
        //                {
        //                    tempstr = tmplist.Substring(tmplist.IndexOf("-") + 1, tmplist.Length - tmplist.IndexOf("-") - 1);
        //                }
        //                if (tempstr == (str + "\r\n"))
        //                {
        //                    sign = true;
        //                    break;
        //                }
        //            }
        //            if (!sign)
        //            {
        //                ListBox.Items.Insert(0, (string.Format("{0}-{1}" + Environment.NewLine, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), str)));
        //                LogHelper.Error(str);
        //            }
        //        }
        //        else
        //        {
        //            foreach (string tmplist in arrRight)
        //            {
        //                if (tmplist.IndexOf("-") > -1)
        //                {
        //                    tempstr = tmplist.Substring(tmplist.IndexOf("-") + 1, tmplist.Length - tmplist.IndexOf("-") - 1);
        //                    if (tempstr == (str + "\r\n")) ListBox.Items.Remove(tmplist);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        AppendText("消息显示异常：" + ex.ToString());
        //    }
        //}
    }
}
