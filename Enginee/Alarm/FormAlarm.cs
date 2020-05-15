using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace System.Enginee
{
    public partial class FormAlarm : Form
    {
        private int _iTimes = -1;
        private bool _bShowFlag = false;
        AlarmManage alarmManage;
        public FormAlarm(AlarmManage MalarmManage)
        {
            alarmManage = MalarmManage;
            InitializeComponent();
            timerRefresh.Start();
        }

        public void ShowAlarmMsg()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    Action action = () =>
                     {
                         lvCurrAlarm.Items.Clear();
                         foreach (KeyValuePair<string, Alarm> item in alarmManage.DicCurrAlarmMsg)
                         {
                             ListViewItem listViewItem = lvCurrAlarm.Items.Insert(0, item.Key, item.Value.AlarmTime.ToString(), 0);                          
                             listViewItem.SubItems.Add(item.Value.Name);
                             listViewItem.SubItems.Add(item.Value.AlarmRemark);
                         }

                         if (alarmManage.IsAlarm && this.Visible == false)
                         {
                             _iTimes = 0;
                             //this.Show();
                             _bShowFlag = true;
                         }
                         else if (!alarmManage.IsAlarm)
                         {
                             this.Hide();
                             _iTimes = -1;
                         }
                     };
                    this.Invoke(action);
                }
                else
                {
                    lvCurrAlarm.Items.Clear();
                    foreach (KeyValuePair<string, Alarm> item in alarmManage.DicCurrAlarmMsg)
                    {
                        ListViewItem listViewItem = lvCurrAlarm.Items.Insert(0, item.Key, item.Value.AlarmTime.ToString(), 0);                       
                        listViewItem.SubItems.Add(item.Value.Name);
                        listViewItem.SubItems.Add(item.Value.AlarmRemark);
                    }

                    if (alarmManage.IsAlarm && this.Visible == false)
                    {
                        _iTimes = 0;
                        _bShowFlag = true;
                        //this.Show();
                    }
                    else if (!alarmManage.IsAlarm)
                    {
                        this.Hide();
                        _iTimes = -1;
                    }
                }

            }
            catch (Exception)
            {

                // throw;
            }
        }
        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_bShowFlag)
                {
                    _bShowFlag = false;
                    if (alarmManage.IsAlarm)
                    {
                        _iTimes = 0;
                        this.Show();
                    }
                    else if (!alarmManage.IsAlarm)
                    {
                        _iTimes = -1;
                        this.Hide();
                    }
                }

                if (_iTimes >= 0)
                {
                    _iTimes++;
                    labTime.Text = (_iTimes / (600 * 60)).ToString("00") + ":" + (_iTimes / 600).ToString("00") + ":" + ((_iTimes / 10) % 60).ToString("00");
                    panel1.BackColor = ((_iTimes / 10) % 60) % 2 == 0 ? Color.LightCoral : Color.White;
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            alarmManage.RemoveAllAlarm();
        }

        private void FormAlarm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            return;
        }


        #region 窗体拖拽
        //鼠标移动位置变量
        private Point mouseOff;
        //是否是左键
        private bool leftFlag;

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y);   //得到变量的值
                leftFlag = true;
            }
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;
            }
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }
        #endregion
    }
}
