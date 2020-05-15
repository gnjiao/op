using CMotion.Interfaces;
using System.Windows.Forms;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Enginee;
using CMotion.Interfaces.Axis;
using LogHeper;

namespace desaySV
{
    public partial class CameraAxis : UserControl, IRefreshing
    {
        private Station1 Station1;
        public CameraAxis(Station1 station1)
        {
            Station1 = station1;
            InitializeComponent();
        }
        public void Refreshing()
        {
            lblCurrentPositionLFX.Text = Station1.LFXaxisServo.CurrentPos.ToString();
            lblCurrentPositionLFY.Text = Station1.LFYaxisServo.CurrentPos.ToString();
            lblCurrentPositionLRX.Text = Station1.LRXaxisServo.CurrentPos.ToString();
            lblCurrentPositionLRY.Text = Station1.LRYaxisServo.CurrentPos.ToString();
            lblCurrentPositionX.Text = Station1.XaxisServo.CurrentPos.ToString();
            lblCurrentPositionRY.Text = Station1.RYaxisServo.CurrentPos.ToString();
        }

        private void CameraAxis_Load(object sender, System.EventArgs e)
        {
            lblLeftLFXPosition.Text = Position.Instance.LPhotoOriPostion.LFXPhoto.ToString();
            lblLeftLFYPosition.Text = Position.Instance.LPhotoOriPostion.LFYPhoto.ToString();
            lblLeftLRXPosition.Text = Position.Instance.LPhotoOriPostion.LRXPhoto.ToString();
            lblLeftLRYPosition.Text = Position.Instance.LPhotoOriPostion.LRYPhoto.ToString();
            lblLeftXPosition.Text = Position.Instance.LPhotoOriPostion.XPhoto.ToString();
            lblLeftRYPosition.Text = Position.Instance.LPhotoOriPostion.RYPhoto.ToString();
            lblRightLFXPosition.Text = Position.Instance.RPhotoOriPostion.LFXPhoto.ToString();
            lblRightLFYPosition.Text = Position.Instance.RPhotoOriPostion.LFYPhoto.ToString();
            lblRightLRXPosition.Text = Position.Instance.RPhotoOriPostion.LRXPhoto.ToString();
            lblRightLRYPosition.Text = Position.Instance.RPhotoOriPostion.LRYPhoto.ToString();
            lblRightXPosition.Text = Position.Instance.RPhotoOriPostion.XPhoto.ToString();
            lblRightRYPosition.Text = Position.Instance.RPhotoOriPostion.RYPhoto.ToString();
        }

        private void btnLeftPhotoPostionGoto_Click(object sender, System.EventArgs e)
        {
            switch (GotoPos(Position.Instance.LPhotoOriPostion))
            {
                case 0:
                    break;
                case -1:
                    MessageBox.Show("未初始化");
                    break;
                case -4:
                    MessageBox.Show("伺服报警或感应限位");
                    break;
                case -2:
                    MessageBox.Show("参数报错");
                    break;
                default:
                    break;
            }
         

        }

        private int GotoPos(PosPhoto posPhoto)
        {
            if (Global.IsLocating) return -1;
            if (!Station1.stationInitialize.InitializeDone)
            {
                return -1;
            }
            Global.IsLocating = true;
            Task.Factory.StartNew(() =>
            {
                try
                {
                    //将X、Y移动到指定位置
                    Station1.LFXaxisServo.MoveTo(posPhoto.LFXPhoto, AxisParameter.Instance.LFXVelocityCurve);
                    Station1.LFYaxisServo.MoveTo(posPhoto.LFYPhoto, AxisParameter.Instance.LFYVelocityCurve);
                    Station1.LRXaxisServo.MoveTo(posPhoto.LRXPhoto, AxisParameter.Instance.LRXVelocityCurve);
                    Station1.LRYaxisServo.MoveTo(posPhoto.LRYPhoto, AxisParameter.Instance.LRYVelocityCurve);
                    Station1.XaxisServo.MoveTo(posPhoto.XPhoto, AxisParameter.Instance.XVelocityCurve);
                    Station1.RYaxisServo.MoveTo(posPhoto.RYPhoto, AxisParameter.Instance.RYVelocityCurve);

                    while (true)
                    {
                        Thread.Sleep(10);
                        if (Station1.LFXaxisServo.CurrentPos == posPhoto.LFXPhoto && Station1.LFYaxisServo.CurrentPos == posPhoto.LFYPhoto &&
                        Station1.LRXaxisServo.CurrentPos == posPhoto.LRXPhoto && Station1.LRYaxisServo.CurrentPos == posPhoto.LRXPhoto &&
                        Station1.RYaxisServo.CurrentPos == posPhoto.RYPhoto && Station1.XaxisServo.CurrentPos == posPhoto.XPhoto)
                        {
                            break;
                        }

                        if (IsErr(Station1.LFXaxisServo)&& IsErr(Station1.LFYaxisServo)&& IsErr(Station1.LRXaxisServo)
                        && IsErr(Station1.LRYaxisServo)&& IsErr(Station1.XaxisServo)&& IsErr(Station1.RYaxisServo))
                        {
                            Station1.LFXaxisServo.Stop();
                            Station1.LFYaxisServo.Stop();
                            Station1.LRXaxisServo.Stop();
                            Station1.LRYaxisServo.Stop();
                            Station1.XaxisServo.Stop();
                            Station1.RYaxisServo.Stop();
                            Global.IsLocating = false;
                            return -4;
                        }
                    }

                    return 0;
                }
                catch (Exception ex)
                {
                    Global.IsLocating = false;
                    MessageBox.Show(ex.ToString());
                    return -2;
                }
            }, TaskCreationOptions.AttachedToParent | TaskCreationOptions.LongRunning);
            Global.IsLocating = false;
            return 0;


        }
        public bool IsErr(ApsAxis axis)
        {
            if (axis.IsAlarmed || axis.IsMEL || axis.IsPEL || !axis.IsServon || axis.IsEmg)
            {
                return false;
            }
            else
            { return true; }
        }

        private void btnLeftPhotoPostionSave_Click(object sender, System.EventArgs e)
        {

            lblLeftLFXPosition.Text = Station1.LFXaxisServo.CurrentPos.ToString();
            lblLeftLFYPosition.Text = Station1.LFYaxisServo.CurrentPos.ToString();
            lblLeftLRXPosition.Text = Station1.LRXaxisServo.CurrentPos.ToString();
            lblLeftLRYPosition.Text = Station1.LRYaxisServo.CurrentPos.ToString();
            lblLeftRYPosition.Text = Station1.RYaxisServo.CurrentPos.ToString();
            lblLeftXPosition.Text = Station1.XaxisServo.CurrentPos.ToString();
            LogHelper.Debug("左拍照位置保存："+ lblLeftLFXPosition.Text +":"+ lblLeftLFYPosition.Text + ":" +
                lblLeftLRXPosition.Text + ":" + lblLeftLRYPosition.Text + ":" + lblLeftRYPosition.Text + ":" +
                lblLeftXPosition.Text);
            Save();



        }

        private void Save()
        {
            try
            {
                Position.Instance.LPhotoOriPostion.LFXPhoto = Convert.ToDouble(lblLeftLFXPosition.Text);
                Position.Instance.LPhotoOriPostion.LFYPhoto = Convert.ToDouble(lblLeftLFYPosition.Text);
                Position.Instance.LPhotoOriPostion.LRXPhoto = Convert.ToDouble(lblLeftLRXPosition.Text);
                Position.Instance.LPhotoOriPostion.LRYPhoto = Convert.ToDouble(lblLeftLRYPosition.Text);
                Position.Instance.LPhotoOriPostion.XPhoto = Convert.ToDouble(lblLeftXPosition.Text);
                Position.Instance.LPhotoOriPostion.RYPhoto = Convert.ToDouble(lblLeftRYPosition.Text);
                Position.Instance.RPhotoOriPostion.LFXPhoto = Convert.ToDouble(lblRightLFXPosition.Text);
                Position.Instance.RPhotoOriPostion.LFYPhoto = Convert.ToDouble(lblRightLFYPosition.Text);
                Position.Instance.RPhotoOriPostion.LRXPhoto = Convert.ToDouble(lblRightLRXPosition.Text);
                Position.Instance.RPhotoOriPostion.LRYPhoto = Convert.ToDouble(lblRightLRYPosition.Text);
                Position.Instance.RPhotoOriPostion.XPhoto = Convert.ToDouble(lblRightXPosition.Text);
                Position.Instance.RPhotoOriPostion.RYPhoto = Convert.ToDouble(lblRightRYPosition.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }

        }

        private void btnRightPhotoPostionSave_Click(object sender, EventArgs e)
        {

            lblRightLFXPosition.Text = Station1.LFXaxisServo.CurrentPos.ToString();
            lblRightLFYPosition.Text = Station1.LFYaxisServo.CurrentPos.ToString();
            lblRightLRXPosition.Text = Station1.LRXaxisServo.CurrentPos.ToString();
            lblRightLRYPosition.Text = Station1.LRYaxisServo.CurrentPos.ToString();
            lblRightRYPosition.Text = Station1.RYaxisServo.CurrentPos.ToString();
            lblRightXPosition.Text = Station1.XaxisServo.CurrentPos.ToString();
            LogHelper.Debug("右拍照位置保存：" + lblRightLFXPosition.Text + ":" + lblRightLFYPosition.Text + ":" +
              lblRightLRXPosition.Text + ":" + lblRightLRYPosition.Text + ":" + lblRightRYPosition.Text + ":" +
              lblRightXPosition.Text);
            Save();
        }

        private void btnRightPhotoPostionGoto_Click(object sender, EventArgs e)
        {
            switch (GotoPos(Position.Instance.RPhotoOriPostion))
            {
                case 0:
                    break;
                case -1:
                    MessageBox.Show("未初始化");
                    break;
                case -4:
                    MessageBox.Show("伺服报警或感应限位");
                    break;
                case -2:
                    MessageBox.Show("参数报错");
                    break;
                default:
                    break;
            }         
        }

        private void btnXadd_Click(object sender, EventArgs e)
        {
            if (moveSelectHorizontal1.MoveMode.Continue || Global.IsLocating) return;
            var velocityCurve = new VelocityCurve { Maxvel = Station1.XaxisServo.Speed ?? 0 };
            Station1.XaxisServo.MoveDelta(1 * moveSelectHorizontal1.MoveMode.Distance, velocityCurve);
        }

        private void btnXdec_Click(object sender, EventArgs e)
        {
            if (moveSelectHorizontal1.MoveMode.Continue || Global.IsLocating) return;
            var velocityCurve = new VelocityCurve { Maxvel = Station1.XaxisServo.Speed ?? 0 };
            Station1.XaxisServo.MoveDelta(-1 * moveSelectHorizontal1.MoveMode.Distance, velocityCurve);
        }

        private void btnLFXadd_Click(object sender, EventArgs e)
        {
            if (moveSelectHorizontal1.MoveMode.Continue || Global.IsLocating) return;
            var velocityCurve = new VelocityCurve { Maxvel = Station1.LFXaxisServo.Speed ?? 0 };
            Station1.LFXaxisServo.MoveDelta(1 * moveSelectHorizontal1.MoveMode.Distance, velocityCurve);
        }

        private void btnLFXdec_Click(object sender, EventArgs e)
        {
            if (moveSelectHorizontal1.MoveMode.Continue || Global.IsLocating) return;
            var velocityCurve = new VelocityCurve { Maxvel = Station1.LFXaxisServo.Speed ?? 0 };
            Station1.LFXaxisServo.MoveDelta(-1 * moveSelectHorizontal1.MoveMode.Distance, velocityCurve);
        }

        private void btnLRXadd_Click(object sender, EventArgs e)
        {
            if (moveSelectHorizontal1.MoveMode.Continue || Global.IsLocating) return;
            var velocityCurve = new VelocityCurve { Maxvel = Station1.LRXaxisServo.Speed ?? 0 };
            Station1.LRXaxisServo.MoveDelta(1 * moveSelectHorizontal1.MoveMode.Distance, velocityCurve);
        }

        private void btnLRXdec_Click(object sender, EventArgs e)
        {
            if (moveSelectHorizontal1.MoveMode.Continue || Global.IsLocating) return;
            var velocityCurve = new VelocityCurve { Maxvel = Station1.LRXaxisServo.Speed ?? 0 };
            Station1.LRXaxisServo.MoveDelta(-1 * moveSelectHorizontal1.MoveMode.Distance, velocityCurve);
        }

        private void btnLFYadd_Click(object sender, EventArgs e)
        {
            if (moveSelectHorizontal1.MoveMode.Continue || Global.IsLocating) return;
            var velocityCurve = new VelocityCurve { Maxvel = Station1.LFYaxisServo.Speed ?? 0 };
            Station1.LFYaxisServo.MoveDelta(1 * moveSelectHorizontal1.MoveMode.Distance, velocityCurve);
        }

        private void btnLFYdec_Click(object sender, EventArgs e)
        {
            if (moveSelectHorizontal1.MoveMode.Continue || Global.IsLocating) return;
            var velocityCurve = new VelocityCurve { Maxvel = Station1.LFYaxisServo.Speed ?? 0 };
            Station1.LFYaxisServo.MoveDelta(-1 * moveSelectHorizontal1.MoveMode.Distance, velocityCurve);
        }

        private void btnLRYadd_Click(object sender, EventArgs e)
        {
            if (moveSelectHorizontal1.MoveMode.Continue || Global.IsLocating) return;
            var velocityCurve = new VelocityCurve { Maxvel = Station1.LRYaxisServo.Speed ?? 0 };
            Station1.LRYaxisServo.MoveDelta(1 * moveSelectHorizontal1.MoveMode.Distance, velocityCurve);
        }

        private void btnLRYdec_Click(object sender, EventArgs e)
        {
            if (moveSelectHorizontal1.MoveMode.Continue || Global.IsLocating) return;
            var velocityCurve = new VelocityCurve { Maxvel = Station1.LRYaxisServo.Speed ?? 0 };
            Station1.LRYaxisServo.MoveDelta(-1 * moveSelectHorizontal1.MoveMode.Distance, velocityCurve);
        }

        private void btnRYadd_Click(object sender, EventArgs e)
        {
            if (moveSelectHorizontal1.MoveMode.Continue || Global.IsLocating) return;
            var velocityCurve = new VelocityCurve { Maxvel = Station1.RYaxisServo.Speed ?? 0 };
            Station1.RYaxisServo.MoveDelta(1 * moveSelectHorizontal1.MoveMode.Distance, velocityCurve);
        }

        private void btnRYdec_Click(object sender, EventArgs e)
        {
            if (moveSelectHorizontal1.MoveMode.Continue || Global.IsLocating) return;
            var velocityCurve = new VelocityCurve { Maxvel = Station1.RYaxisServo.Speed ?? 0 };
            Station1.RYaxisServo.MoveDelta(-1 * moveSelectHorizontal1.MoveMode.Distance, velocityCurve);
        }

        private void btnXadd_MouseDown(object sender, MouseEventArgs e)
        {
            if (!moveSelectHorizontal1.MoveMode.Continue || Global.IsLocating) return;
            Station1.XaxisServo.Postive();
        }

        private void btnXadd_MouseUp(object sender, MouseEventArgs e)
        {
            Station1.XaxisServo.Stop();
        }

        private void btnXdec_MouseDown(object sender, MouseEventArgs e)
        {
            if (!moveSelectHorizontal1.MoveMode.Continue || Global.IsLocating) return;
            Station1.XaxisServo.Negative();
        }

        private void btnXdec_MouseUp(object sender, MouseEventArgs e)
        {
            Station1.XaxisServo.Stop();
        }

        private void btnLFXadd_MouseDown(object sender, MouseEventArgs e)
        {
            if (!moveSelectHorizontal1.MoveMode.Continue || Global.IsLocating) return;
            Station1.LFXaxisServo.Postive();
        }

        private void btnLFXadd_MouseUp(object sender, MouseEventArgs e)
        {
            Station1.LFXaxisServo.Stop();
        }

        private void btnLFXdec_MouseDown(object sender, MouseEventArgs e)
        {
            if (!moveSelectHorizontal1.MoveMode.Continue || Global.IsLocating) return;
            Station1.LFXaxisServo.Negative();
        }

        private void btnLFXdec_MouseUp(object sender, MouseEventArgs e)
        {
            Station1.LFXaxisServo.Stop();
        }

        private void btnLRXadd_MouseDown(object sender, MouseEventArgs e)
        {
            if (!moveSelectHorizontal1.MoveMode.Continue || Global.IsLocating) return;
            Station1.LRXaxisServo.Postive();
        }

        private void btnLRXadd_MouseUp(object sender, MouseEventArgs e)
        {
            Station1.LRXaxisServo.Stop();
        }

        private void btnLRXdec_MouseDown(object sender, MouseEventArgs e)
        {
            if (!moveSelectHorizontal1.MoveMode.Continue || Global.IsLocating) return;
            Station1.LRXaxisServo.Negative();
        }

        private void btnLRXdec_MouseUp(object sender, MouseEventArgs e)
        {
            Station1.LRXaxisServo.Stop();
        }

        private void btnLFYadd_MouseDown(object sender, MouseEventArgs e)
        {
            if (!moveSelectHorizontal1.MoveMode.Continue || Global.IsLocating) return;
            Station1.LFYaxisServo.Postive();
        }

        private void btnLFYadd_MouseUp(object sender, MouseEventArgs e)
        {
            Station1.LFYaxisServo.Stop();
        }

        private void btnLFYdec_MouseDown(object sender, MouseEventArgs e)
        {
            if (!moveSelectHorizontal1.MoveMode.Continue || Global.IsLocating) return;
            Station1.LFYaxisServo.Negative();
        }

        private void btnLFYdec_MouseUp(object sender, MouseEventArgs e)
        {
            Station1.LFYaxisServo.Stop();
        }

        private void btnLRYadd_MouseDown(object sender, MouseEventArgs e)
        {
            if (!moveSelectHorizontal1.MoveMode.Continue || Global.IsLocating) return;
            Station1.LRYaxisServo.Postive();
        }

        private void btnLRYadd_MouseUp(object sender, MouseEventArgs e)
        {
            Station1.LRYaxisServo.Stop();
        }

        private void btnLRYdec_MouseDown(object sender, MouseEventArgs e)
        {
            if (!moveSelectHorizontal1.MoveMode.Continue || Global.IsLocating) return;
            Station1.LRYaxisServo.Negative();
        }

        private void btnLRYdec_MouseUp(object sender, MouseEventArgs e)
        {
            Station1.LRYaxisServo.Stop();
        }

        private void btnRYadd_MouseDown(object sender, MouseEventArgs e)
        {
            if (!moveSelectHorizontal1.MoveMode.Continue || Global.IsLocating) return;
            Station1.RYaxisServo.Postive();
        }

        private void btnRYadd_MouseUp(object sender, MouseEventArgs e)
        {
            Station1.RYaxisServo.Stop();
        }

        private void btnRYdec_MouseDown(object sender, MouseEventArgs e)
        {
            if (!moveSelectHorizontal1.MoveMode.Continue || Global.IsLocating) return;
            Station1.RYaxisServo.Negative();
        }

        private void btnRYdec_MouseUp(object sender, MouseEventArgs e)
        {
            Station1.RYaxisServo.Stop();
        }

        private void tbrJogSpeed_Scroll(object sender, EventArgs e)
        {
            if (Global.IsLocating) return;
          
            Station1.XaxisServo.Speed = (double)tbrJogSpeed.Value;
            Station1.LFXaxisServo.Speed = (double)tbrJogSpeed.Value;
            Station1.LFYaxisServo.Speed = (double)tbrJogSpeed.Value;
            Station1.LRXaxisServo.Speed = (double)tbrJogSpeed.Value;
            Station1.LRYaxisServo.Speed = (double)tbrJogSpeed.Value;
            Station1.RYaxisServo.Speed = (double)tbrJogSpeed.Value;
        }
    }
}
