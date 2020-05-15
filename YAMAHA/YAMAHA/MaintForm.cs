using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YAMAHA
{
    public partial class MaintForm : Form
    {
        private Yamaha yamaha = new Yamaha();


        Button[] btnDIArray = new Button[16];
        Button[] btnDOArray = new Button[8];


        public MaintForm()
        {
            InitializeComponent();

            //DI按钮数组赋值
            btnDIArray[0] = button_DI1;
            btnDIArray[1] = button_DI2;
            btnDIArray[2] = button_DI3;
            btnDIArray[3] = button_DI4;
            btnDIArray[4] = button_DI5;
            btnDIArray[5] = button_DI6;
            btnDIArray[6] = button_DI7;
            btnDIArray[7] = button_DI8;
            btnDIArray[8] = button_DI9;
            btnDIArray[9] = button_DI10;
            btnDIArray[10] = button_DI11;
            btnDIArray[11] = button_DI12;
            btnDIArray[12] = button_DI13;
            btnDIArray[13] = button_DI14;
            btnDIArray[14] = button_DI15;
            btnDIArray[15] = button_DI16;

            //DO按钮数组赋值
            btnDOArray[0] = button_DO1;
            btnDOArray[1] = button_DO2;
            btnDOArray[2] = button_DO3;
            btnDOArray[3] = button_DO4;
            btnDOArray[4] = button_DO5;
            btnDOArray[5] = button_DO6;
            btnDOArray[6] = button_DO7;
            btnDOArray[7] = button_DO8;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!yamaha.Connect("10.219.177.32", 23))//yamaha.Connect("192.168.0.2", 23);
            {
                MessageBox.Show("机械手连接失败!");
                //Application.Exit();
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!yamaha.Connect("192.168.0.2", 23))
            {
                MessageBox.Show("机械手连接失败!");
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            yamaha.DisConnect();
            //MessageBox.Show("机械手连接已断开!");
        }


        private void trackBar_Jog_Speed_Scroll(object sender, EventArgs e)
        {
            textBox_Jog_Speed.Text = trackBar_Jog_Speed.Value.ToString();
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            yamaha.DisConnect();
        }

        private void buttonApplySpeed_Click(object sender, EventArgs e)
        {
            int distance = 0;  //单位um
            int speed = 5;//单位%
            bool state = false;

            if (!yamaha.RobotConnectState(out state))
                return;

            if (!state)
                return;

            distance = Convert.ToInt32(textBox_Cartesian_Distance.Text);

            if (distance < 1 || distance > 1000 * 10)
                throw new ArgumentOutOfRangeException();


            speed = trackBar_Jog_Speed.Value;

            if (speed < 1 || speed > 100)
                throw new ArgumentOutOfRangeException();

            yamaha.SetMSpeed(speed);
            yamaha.SetASpeed(speed);
            yamaha.SetCartesianDistance(distance);

        }

        private void timer_Update_Tick(object sender, EventArgs e)
        {

            bool result;
            bool state = false;

            result = yamaha.RobotConnectState(out state);

            if (!state)
            {
                toolStripBtnServo.Image = Image.FromFile("icon\\Servo_Off.ico");
                toolStripBtnRun.Image = Image.FromFile("icon\\Start_gray.ico");
                toolStripBtnPause.Image = Image.FromFile("icon\\Pause_gray.ico");
                toolStripBtnStop.Image = Image.FromFile("icon\\Stop_gray.ico");

                button_X_Positive.Enabled = false;
                button_X_Negtive.Enabled = false;
                button_Y_Positive.Enabled = false;
                button_Y_Negtive.Enabled = false;
                button_Z_Positive.Enabled = false;
                button_Z_Negtive.Enabled = false;
                button_RZ_Positive.Enabled = false;
                button_RZ_Negtive.Enabled = false;
                textBox_Cartesian_Distance.Enabled = false;
                trackBar_Jog_Speed.Enabled = false;
                buttonApplySpeed.Enabled = false;
                radioButton_Step.Enabled = false;
                radioButtonContinuous.Enabled = false;

                btnConnect.BackColor = Color.Gray;
                btnDisconnect.BackColor = Color.Red;
                return;
            }

            btnConnect.BackColor = Color.Green;
            btnDisconnect.BackColor = Color.Gray;

            result = yamaha.ServoState(out state);
            if (state)
            {
                toolStripBtnServo.Image = Image.FromFile("icon\\Servo_On.ico");
            }
            else
            {
                toolStripBtnServo.Image = Image.FromFile("icon\\Servo_Off.ico");
            }

            double[] pos;
            result = yamaha.GetRobotPosition(out pos);
            if (result)
            {
                toolStripStatusLabel_X.Text = "  X: " + pos[0].ToString("0.000");
                toolStripStatusLabel_Y.Text = "  Y: " + pos[1].ToString("0.000");
                toolStripStatusLabel_Z.Text = "  Z: " + pos[2].ToString("0.000");
                toolStripStatusLabel_Theta.Text = "  RZ: " + pos[3].ToString("0.000");
            }

            string exestatue;
            if (!yamaha.ExecutorState(out exestatue))
                return;

            if ("Running" == exestatue)
            {
                toolStripBtnRun.Image = Image.FromFile("icon\\Start.ico");
                toolStripBtnPause.Image = Image.FromFile("icon\\Pause_gray.ico");
                toolStripBtnStop.Image = Image.FromFile("icon\\Stop_gray.ico");
            }
            else if ("Pause" == exestatue)
            {
                toolStripBtnRun.Image = Image.FromFile("icon\\Start_gray.ico");
                toolStripBtnPause.Image = Image.FromFile("icon\\Pause.ico");
                toolStripBtnStop.Image = Image.FromFile("icon\\Stop_gray.ico");
            }
            else if ("Close" == exestatue)
            {
                toolStripBtnRun.Image = Image.FromFile("icon\\Start_gray.ico");
                toolStripBtnPause.Image = Image.FromFile("icon\\Pause_gray.ico");
                toolStripBtnStop.Image = Image.FromFile("icon\\Stop.ico");
            }



            //if (_bWarn)
            //{
            //    toolStripBtnReset.Image = Image.FromFile("icon\\Error.ico");
            //}
            //else
            //    toolStripBtnReset.Image = Image.FromFile("icon\\Ok.ico");


            //更新示教状态
            if ("Close" == exestatue)
            {
                button_X_Positive.Enabled = true;
                button_X_Negtive.Enabled = true;
                button_Y_Positive.Enabled = true;
                button_Y_Negtive.Enabled = true;
                button_Z_Positive.Enabled = true;
                button_Z_Negtive.Enabled = true;
                button_RZ_Positive.Enabled = true;
                button_RZ_Negtive.Enabled = true;
                textBox_Cartesian_Distance.Enabled = true;
                trackBar_Jog_Speed.Enabled = true;
                buttonApplySpeed.Enabled = true;
                radioButton_Step.Enabled = true;
                radioButtonContinuous.Enabled = true;
            }
            else
            {
                button_X_Positive.Enabled = false;
                button_X_Negtive.Enabled = false;
                button_Y_Positive.Enabled = false;
                button_Y_Negtive.Enabled = false;
                button_Z_Positive.Enabled = false;
                button_Z_Negtive.Enabled = false;
                button_RZ_Positive.Enabled = false;
                button_RZ_Negtive.Enabled = false;
                textBox_Cartesian_Distance.Enabled = false;
                trackBar_Jog_Speed.Enabled = false;
                buttonApplySpeed.Enabled = false;
                radioButton_Step.Enabled = false;
                radioButtonContinuous.Enabled = false;
            }


            //IO状态刷新
            for (int i = 0; i < btnDIArray.Length; i++)
            {
                result = yamaha.ReadUserDI(i, out state);

                if (state)
                    btnDIArray[i].BackColor = Color.GreenYellow;
                else
                    btnDIArray[i].BackColor = Color.Transparent;
            }

            for (int i = 0; i < btnDOArray.Length; i++)
            {
                result = yamaha.ReadUserDO(i, out state);

                if (state)
                    btnDOArray[i].BackColor = Color.GreenYellow;
                else
                    btnDOArray[i].BackColor = Color.Transparent;
            }

            result = yamaha.Ready(out state);
            if (state)
                btnReady.BackColor = Color.Green;
            else
                btnReady.BackColor = Color.Gray;
        }

        private void toolStripBtnServo_Click(object sender, EventArgs e)
        {
            bool state = false;
            if (!yamaha.ServoState(out state))
                return;

            if (state)
            {
                yamaha.ServoOFF();
            }
            else
            {
                yamaha.ServoON();
            }
        }

        private void button_X_Positive_MouseDown(object sender, MouseEventArgs e)
        {
            if (radioButton_Step.Checked)
                yamaha.Step(1, true);
            else
                yamaha.Jog(1, true);
        }

        private void button_X_Positive_MouseUp(object sender, MouseEventArgs e)
        {
            if (!radioButton_Step.Checked)
                yamaha.MovStop();
        }

        private void button_X_Negtive_MouseDown(object sender, MouseEventArgs e)
        {
            if (radioButton_Step.Checked)
                yamaha.Step(1, false);
            else
                yamaha.Jog(1, false);
        }

        private void button_X_Negtive_MouseUp(object sender, MouseEventArgs e)
        {
            if (!radioButton_Step.Checked)
                yamaha.MovStop();
        }

        private void button_Y_Positive_MouseDown(object sender, MouseEventArgs e)
        {
            if (radioButton_Step.Checked)
                yamaha.Step(2, true);
            else
                yamaha.Jog(2, true);
        }

        private void button_Y_Positive_MouseUp(object sender, MouseEventArgs e)
        {
            if (!radioButton_Step.Checked)
                yamaha.MovStop();
        }

        private void button_Y_Negtive_MouseDown(object sender, MouseEventArgs e)
        {
            if (radioButton_Step.Checked)
                yamaha.Step(2, false);
            else
                yamaha.Jog(2, false);
        }

        private void button_Y_Negtive_MouseUp(object sender, MouseEventArgs e)
        {
            if (!radioButton_Step.Checked)
                yamaha.MovStop();
        }

        private void button_Z_Positive_MouseDown(object sender, MouseEventArgs e)
        {
            if (radioButton_Step.Checked)
                yamaha.Step(3, true);
            else
                yamaha.Jog(3, true);
        }

        private void button_Z_Positive_MouseUp(object sender, MouseEventArgs e)
        {
            if (!radioButton_Step.Checked)
                yamaha.MovStop();
        }

        private void button_Z_Negtive_MouseDown(object sender, MouseEventArgs e)
        {
            if (radioButton_Step.Checked)
                yamaha.Step(3, false);
            else
                yamaha.Jog(3, false);
        }

        private void button_Z_Negtive_MouseUp(object sender, MouseEventArgs e)
        {
            if (!radioButton_Step.Checked)
                yamaha.MovStop();
        }

        private void button_RZ_Positive_MouseDown(object sender, MouseEventArgs e)
        {
            if (radioButton_Step.Checked)
                yamaha.Step(4, true);
            else
                yamaha.Jog(4, true);
        }

        private void button_RZ_Positive_MouseUp(object sender, MouseEventArgs e)
        {
            if (!radioButton_Step.Checked)
                yamaha.MovStop();
        }

        private void button_RZ_Negtive_MouseDown(object sender, MouseEventArgs e)
        {
            if (radioButton_Step.Checked)
                yamaha.Step(4, false);
            else
                yamaha.Jog(4, false);
        }

        private void button_RZ_Negtive_MouseUp(object sender, MouseEventArgs e)
        {
            if (!radioButton_Step.Checked)
                yamaha.MovStop();
        }

        private void ContinuousMove()
        {
            bool result, ready;

            for (int i = 0; i < 3; i++)
            {
                if (radioButtonMovP.Checked)
                    yamaha.MovP(point);
                else
                    yamaha.MovL(point);

                do
                {
                    result = yamaha.Ready(out ready);
                }
                while (!ready);

                if (radioButtonMovP.Checked)
                    yamaha.MovP(point);
                else
                    yamaha.MovL(point);

                do
                {
                    result = yamaha.Ready(out ready);
                }
                while (!ready);
            }
        }
        int point = 0;
        private void btnMoveP8_Click(object sender, EventArgs e)
        {
            point = Convert.ToInt16(numPointName.Value);

            Task tk = new Task(ContinuousMove);
            tk.Start();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            point = Convert.ToInt16(numPointName.Value);
            if (radioButtonMovP.Checked)
                yamaha.MovP(point);
            else
                yamaha.MovL(point);
        }


        private void button_DO_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Button btnSender = (System.Windows.Forms.Button)sender;
            int i = 0;
            bool result;
            bool state;

            try
            {
                if (null == yamaha)
                    throw new Exception("机器人未连接，请检查！");

                result = yamaha.RobotConnectState(out state);

                if (!result || !state)
                    throw new Exception("机器人未连接，请检查！");

                foreach (System.Windows.Forms.Button button in btnDOArray)
                {
                    if (button.Name == btnSender.Name)
                    {
                        if (yamaha.ReadUserDO(i, out state))
                        {
                            state = !(Color.GreenYellow == button.BackColor);
                            yamaha.WriteUserDO(i, state);
                        }
                        break;
                    }
                    i++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void toolStripBtnRun_Click(object sender, EventArgs e)
        {
            bool result;
            bool state;

            try
            {
                if (null == yamaha)
                    throw new Exception("机器人未连接，请检查！");

                result = yamaha.RobotConnectState(out state);

                if (!result || !state)
                    throw new Exception("机器人未连接，请检查！");

                yamaha.Run();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

        }

        private void toolStripBtnPause_Click(object sender, EventArgs e)
        {
            bool result;
            bool state;

            try
            {
                if (null == yamaha)
                    throw new Exception("机器人未连接，请检查！");

                result = yamaha.RobotConnectState(out state);

                if (!result || !state)
                    throw new Exception("机器人未连接，请检查！");

                yamaha.Pause();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void toolStripBtnStop_Click(object sender, EventArgs e)
        {
            bool result;
            bool state;

            try
            {
                if (null == yamaha)
                    throw new Exception("机器人未连接，请检查！");

                result = yamaha.RobotConnectState(out state);

                if (!result || !state)
                    throw new Exception("机器人未连接，请检查！");

                yamaha.Stop();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void toolStripBtnReset_Click(object sender, EventArgs e)
        {
            bool result;
            bool state;

            try
            {
                if (null == yamaha)
                    throw new Exception("机器人未连接，请检查！");

                result = yamaha.RobotConnectState(out state);

                if (!result || !state)
                    throw new Exception("机器人未连接，请检查！");

                yamaha.ResetAlarm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void btnMove_Click(object sender, EventArgs e)
        {
            double[] pt = new double[6];

            pt[0] = double.Parse(textBoxX.Text);
            pt[1] = double.Parse(textBoxY.Text);
            pt[2] = double.Parse(textBoxZ.Text);
            pt[3] = double.Parse(textBoxRZ.Text);
            pt[4] = 0;
            pt[5] = 0;
            pt[6] = int.Parse(comboBoxHand.Text);

            if (radioButtonMovP.Checked)
                yamaha.MovP(pt);
            else
                yamaha.MovL(pt);
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            bool result;
            bool state;
            int index = 0;
            double[] data = new double[1] { 0 };

            try
            {
                if (null == yamaha)
                    throw new Exception("机器人未连接，请检查！");

                result = yamaha.RobotConnectState(out state);

                if (!result || !state)
                    throw new Exception("机器人未连接，请检查！");

                index = int.Parse(comboBoxDataIndex.Text);


                if (yamaha.ReadData(index, 1, out data))
                {
                    textBoxData.Text = data[0].ToString("0.000");
                }
                else
                {
                    textBoxData.Text = "FAILED";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            bool result;
            bool state;
            int index = 0;
            double[] data = new double[1] { 0 };

            try
            {
                if (null == yamaha)
                    throw new Exception("机器人未连接，请检查！");

                result = yamaha.RobotConnectState(out state);

                if (!result || !state)
                    throw new Exception("机器人未连接，请检查！");

                index = int.Parse(comboBoxDataIndex.Text);
                data[0] = double.Parse(textBoxData.Text);

                if (!yamaha.WriteData(index, data))
                {
                    throw new Exception("写入失败！");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void radioButtonContinuous_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnReady_Click(object sender, EventArgs e)
        {

        }
    }
}
