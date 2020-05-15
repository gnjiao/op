using System.Windows.Forms;
using CMotion.Interfaces;
using YAMAHA;
using System.Enginee;
using System.Threading.Tasks;
using System.Threading;
using System.ToolKit;
using LogHeper;
namespace desaySV.View
{
    public partial class ProductTeach : UserControl, IRefreshing
    {
        private Yamaha mYAMAHA;
        private Station1 Station1;
        private CylinderOperate SwitchCylinderOperate;
        private CylinderOperate LeftVacuumOperate;
        private CylinderOperate RightVacuumOperate;
        private double[] pt = new double[6];
        public ProductTeach(Station1 station)
        {
            mYAMAHA = station.YAMAHA;
            Station1 = station;
            InitializeComponent();
            rdbLeftCalib.Checked = true;
        }
        public void Refreshing()
        {
            if (!Global.RootIsLocating)
            {
                lblCurrentPositionX.Text = mYAMAHA.CurrentPosX.ToString();
                lblCurrentPositionY.Text = mYAMAHA.CurrentPosY.ToString();
                lblCurrentPositionZ.Text = mYAMAHA.CurrentPosZ.ToString();
                lblCurrentPositionR.Text = mYAMAHA.CurrentPosR.ToString();
                //lblRootSturts.Text = mYAMAHA.CurrentRootStuts.ToString();
            }
            SwitchCylinderOperate.Refreshing();
            LeftVacuumOperate.Refreshing();
            RightVacuumOperate.Refreshing();
        }

        #region 手动按钮
        private void btnYadd_MouseDown(object sender, MouseEventArgs e)
        {           
            bool state;
            var result = mYAMAHA.Ready(out state);
            if (!state) return;
            if (moveSelectHorizontal1.MoveMode.Continue)
                mYAMAHA.Step(2, true);
            else
                mYAMAHA.Jog(2, true);
          
       
        }

        private void btnYadd_MouseUp(object sender, MouseEventArgs e)
        {
            if (!moveSelectHorizontal1.MoveMode.Continue)
                mYAMAHA.MovStop();
        }

        private void btnYdec_MouseDown(object sender, MouseEventArgs e)
        {
            bool state;
            var result = mYAMAHA.Ready(out state);
            if (!state) return;
            if (moveSelectHorizontal1.MoveMode.Continue)
                mYAMAHA.Step(2, false);
            else
                mYAMAHA.Jog(2, false);
        }

        private void btnYdec_MouseUp(object sender, MouseEventArgs e)
        {
            if (!moveSelectHorizontal1.MoveMode.Continue)
                mYAMAHA.MovStop();
        }

        private void btnZdec_MouseDown(object sender, MouseEventArgs e)
        {
            bool state;
            var result = mYAMAHA.Ready(out state);
            if (!state) return;
            if (moveSelectHorizontal1.MoveMode.Continue)
                mYAMAHA.Step(3, false);
            else
                mYAMAHA.Jog(3, false);
        }

        private void btnZdec_MouseUp(object sender, MouseEventArgs e)
        {
            if (!moveSelectHorizontal1.MoveMode.Continue)
                mYAMAHA.MovStop();
        }

        private void btnZadd_MouseDown(object sender, MouseEventArgs e)
        {
            bool state;
            var result = mYAMAHA.Ready(out state);
            if (!state) return;
            if (moveSelectHorizontal1.MoveMode.Continue)
                mYAMAHA.Step(3, true);
            else
                mYAMAHA.Jog(3, true);
        }

        private void btnZadd_MouseUp(object sender, MouseEventArgs e)
        {
            if (!moveSelectHorizontal1.MoveMode.Continue)
                mYAMAHA.MovStop();
        }

        private void btnXadd_MouseDown(object sender, MouseEventArgs e)
        {
            bool state;
            var result = mYAMAHA.Ready(out state);
            if (!state) return;
            if (moveSelectHorizontal1.MoveMode.Continue)
                mYAMAHA.Step(1, true);
            else
                mYAMAHA.Jog(1, true);
        }

        private void btnXadd_MouseUp(object sender, MouseEventArgs e)
        {
            if (!moveSelectHorizontal1.MoveMode.Continue)
                mYAMAHA.MovStop();
        }

        private void btnXdec_MouseDown(object sender, MouseEventArgs e)
        {
            bool state;
            var result = mYAMAHA.Ready(out state);
            if (!state) return;
            if (moveSelectHorizontal1.MoveMode.Continue)
                mYAMAHA.Step(1, false);
            else
                mYAMAHA.Jog(1, false);
        }

        private void btnXdec_MouseUp(object sender, MouseEventArgs e)
        {
            string Dir;
            if (!moveSelectHorizontal1.MoveMode.Continue)
                mYAMAHA.MovStop();
        }

        private void btnRdec_MouseDown(object sender, MouseEventArgs e)
        {
            bool state;
            var result = mYAMAHA.Ready(out state);
            if (!state) return;
            if (moveSelectHorizontal1.MoveMode.Continue)
                mYAMAHA.Step(4, false);
            else
                mYAMAHA.Jog(4, false);
        }

        private void btnRdec_MouseUp(object sender, MouseEventArgs e)
        {
            if (!moveSelectHorizontal1.MoveMode.Continue)
                mYAMAHA.MovStop();
        }

        private void btnRadd_MouseDown(object sender, MouseEventArgs e)
        {
            bool state;
            var result = mYAMAHA.Ready(out state);
            if (!state) return;
            if (moveSelectHorizontal1.MoveMode.Continue)
                mYAMAHA.Step(4, true);
            else
                mYAMAHA.Jog(4, true);
        }

        private void btnRadd_MouseUp(object sender, MouseEventArgs e)
        {
            if (!moveSelectHorizontal1.MoveMode.Continue)
                mYAMAHA.MovStop();
        }



        #endregion

        /// <summary>
        /// 相机拍照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCarameOn_Click(object sender, System.EventArgs e)
        {

        }
        /// <summary>
        /// 标定待机位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalibWaitPos_Click(object sender, System.EventArgs e)
        {
          
            pt[0] = Position.Instance.RootPostion[0].X;
            pt[1] = Position.Instance.RootPostion[0].Y;
            pt[2] = Position.Instance.RootPostion[0].Z;
            pt[3] = Position.Instance.RootPostion[0].R;
            pt[4] = 0;
            pt[5] = 0;
            pt[6] = (int)(tbrJogSpeed.Value);

            mYAMAHA.MovP(pt);
            Station1.YaxisServo.MoveTo(Position.Instance.YaxisPostion[0].pos);
            Station1.ZaxisServo.MoveTo(Position.Instance.ZaxisPostion[0].pos);
            if (rdbLeftCalib.Checked)
            {
                Station1.LFXaxisServo.MoveTo(Position.Instance.LPhotoOriPostion.LFXPhoto);
                Station1.LFYaxisServo.MoveTo(Position.Instance.LPhotoOriPostion.LFYPhoto);
                Station1.LRXaxisServo.MoveTo(Position.Instance.LPhotoOriPostion.LRXPhoto);
                Station1.LRYaxisServo.MoveTo(Position.Instance.LPhotoOriPostion.LRYPhoto);
                Station1.RYaxisServo.MoveTo(Position.Instance.LPhotoOriPostion.RYPhoto);
                Station1.XaxisServo.MoveTo(Position.Instance.LPhotoOriPostion.XPhoto);
                Station1.SwichCylinder.Reset();
            }
            if (rdbRightCalib.Checked)
            {
                Station1.LFXaxisServo.MoveTo(Position.Instance.RPhotoOriPostion.LFXPhoto);
                Station1.LFYaxisServo.MoveTo(Position.Instance.RPhotoOriPostion.LFYPhoto);
                Station1.LRXaxisServo.MoveTo(Position.Instance.RPhotoOriPostion.LRXPhoto);
                Station1.LRYaxisServo.MoveTo(Position.Instance.RPhotoOriPostion.LRYPhoto);
                Station1.RYaxisServo.MoveTo(Position.Instance.RPhotoOriPostion.RYPhoto);
                Station1.XaxisServo.MoveTo(Position.Instance.RPhotoOriPostion.XPhoto);
                Station1.SwichCylinder.Set();
            }
        }
        /// <summary>
        /// 标定开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInhaleCalib_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show(string.Format("产品标定"), "确认", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;
            stuts(1);
            Global.IsLocating = true;
            ArcParam<double> pos4 = new ArcParam<double>();
            ArcParam<double> pos1 = new ArcParam<double>();
            ArcParam<double> pos2 = new ArcParam<double>();
            ArcParam<double> pos3 = new ArcParam<double>();
            Point<double> posd1 = new Point<double>();
            Point<double> posd2 = new Point<double>();
            Task.Factory.StartNew(() =>
            {
                try
                {
                    int step = 0;
                    while (true)
                    {
                        Thread.Sleep(10);
                        if (mYAMAHA.Alarm)
                        {
                            Global.IsLocating = false;
                            return -4;
                        }
                        switch (step)
                        {
                            case 0://去待机位
                               
                                pt[0] = Position.Instance.RootPostion[0].X;
                                pt[1] = Position.Instance.RootPostion[0].Y;
                                pt[2] = Position.Instance.RootPostion[0].Z;
                                pt[3] = Position.Instance.RootPostion[0].R;
                                pt[4] = 0;
                                pt[5] = 0;
                                pt[6] = (int)(tbrJogSpeed.Value);
                                mYAMAHA.MovP(pt);
                                Station1.YaxisServo.MoveTo(Position.Instance.YaxisPostion[0].pos);
                                Station1.ZaxisServo.MoveTo(Position.Instance.ZaxisPostion[0].pos);
                                if (rdbLeftCalib.Checked)
                                {
                                    Station1.LFXaxisServo.MoveTo(Position.Instance.LPhotoOriPostion.LFXPhoto);
                                    Station1.LFYaxisServo.MoveTo(Position.Instance.LPhotoOriPostion.LFYPhoto);
                                    Station1.LRXaxisServo.MoveTo(Position.Instance.LPhotoOriPostion.LRXPhoto);
                                    Station1.LRYaxisServo.MoveTo(Position.Instance.LPhotoOriPostion.LRYPhoto);
                                    Station1.RYaxisServo.MoveTo(Position.Instance.LPhotoOriPostion.RYPhoto);
                                    Station1.XaxisServo.MoveTo(Position.Instance.LPhotoOriPostion.XPhoto);
                                    Station1.SwichCylinder.Reset();
                                }
                                if (rdbRightCalib.Checked)
                                {
                                    Station1.LFXaxisServo.MoveTo(Position.Instance.RPhotoOriPostion.LFXPhoto);
                                    Station1.LFYaxisServo.MoveTo(Position.Instance.RPhotoOriPostion.LFYPhoto);
                                    Station1.LRXaxisServo.MoveTo(Position.Instance.RPhotoOriPostion.LRXPhoto);
                                    Station1.LRYaxisServo.MoveTo(Position.Instance.RPhotoOriPostion.LRYPhoto);
                                    Station1.RYaxisServo.MoveTo(Position.Instance.RPhotoOriPostion.RYPhoto);
                                    Station1.XaxisServo.MoveTo(Position.Instance.RPhotoOriPostion.XPhoto);
                                    Station1.SwichCylinder.Set();
                                }
                                step = 10;
                                break;
                            case 10://判断是否在待机位
                                if (rdbLeftCalib.Checked)
                                {
                                    if (mYAMAHA.CurrentPosX == Position.Instance.RootPostion[0].X &&
                                    mYAMAHA.CurrentPosY == Position.Instance.RootPostion[0].Y &&
                                    mYAMAHA.CurrentPosZ == Position.Instance.RootPostion[0].Z &&
                                    mYAMAHA.CurrentPosR == Position.Instance.RootPostion[0].R &&
                                    Station1.YaxisServo.IsInPosition(Position.Instance.YaxisPostion[0].pos) &&
                                    Station1.ZaxisServo.IsInPosition(Position.Instance.ZaxisPostion[0].pos) &&
                                    Station1.LFXaxisServo.IsInPosition(Position.Instance.LPhotoOriPostion.LFXPhoto) &&
                                    Station1.LFYaxisServo.IsInPosition(Position.Instance.LPhotoOriPostion.LFYPhoto) &&
                                    Station1.LRXaxisServo.IsInPosition(Position.Instance.LPhotoOriPostion.LRXPhoto) &&
                                    Station1.LRYaxisServo.IsInPosition(Position.Instance.LPhotoOriPostion.LRYPhoto) &&
                                    Station1.RYaxisServo.IsInPosition(Position.Instance.LPhotoOriPostion.RYPhoto) &&
                                    Station1.XaxisServo.IsInPosition(Position.Instance.LPhotoOriPostion.XPhoto) &&
                                    Station1.SwichCylinder.OutMoveStatus)
                                    {
                                        step = 20;
                                    }
                                }
                                if (rdbRightCalib.Checked)
                                {
                                    if (mYAMAHA.CurrentPosX == Position.Instance.RootPostion[0].X &&
                                    mYAMAHA.CurrentPosY == Position.Instance.RootPostion[0].Y &&
                                    mYAMAHA.CurrentPosZ == Position.Instance.RootPostion[0].Z &&
                                    mYAMAHA.CurrentPosR == Position.Instance.RootPostion[0].R &&
                                    Station1.YaxisServo.IsInPosition(Position.Instance.YaxisPostion[0].pos) &&
                                    Station1.ZaxisServo.IsInPosition(Position.Instance.ZaxisPostion[0].pos) &&
                                    Station1.LFXaxisServo.IsInPosition(Position.Instance.LPhotoOriPostion.LFXPhoto) &&
                                    Station1.LFYaxisServo.IsInPosition(Position.Instance.LPhotoOriPostion.LFYPhoto) &&
                                    Station1.LRXaxisServo.IsInPosition(Position.Instance.LPhotoOriPostion.LRXPhoto) &&
                                    Station1.LRYaxisServo.IsInPosition(Position.Instance.LPhotoOriPostion.LRYPhoto) &&
                                    Station1.RYaxisServo.IsInPosition(Position.Instance.LPhotoOriPostion.RYPhoto) &&
                                    Station1.XaxisServo.IsInPosition(Position.Instance.LPhotoOriPostion.XPhoto) &&
                                    Station1.SwichCylinder.OutOriginStatus)
                                    {
                                        step = 20;
                                    }
                                }
                                break;
                            case 20://机械手去前安全位
                                if (rdbLeftCalib.Checked)
                                {
                                    pt[0] = Position.Instance.RootPostion[1].X;
                                    pt[1] = Position.Instance.RootPostion[1].Y;
                                    pt[2] = Position.Instance.RootPostion[1].Z;
                                    pt[3] = Position.Instance.RootPostion[1].R;
                                    pt[4] = 0;
                                    pt[5] = 0;
                                    pt[6] = (int)(tbrJogSpeed.Value);
                                    mYAMAHA.MovP(pt);                                 
                                    Station1.ZaxisServo.MoveTo(Position.Instance.ZaxisPostion[1].pos);
                                }
                                if (rdbRightCalib.Checked)
                                {
                                    pt[0] = Position.Instance.RootPostion[2].X;
                                    pt[1] = Position.Instance.RootPostion[2].Y;
                                    pt[2] = Position.Instance.RootPostion[2].Z;
                                    pt[3] = Position.Instance.RootPostion[2].R;
                                    pt[4] = 0;
                                    pt[5] = 0;
                                    pt[6] = (int)(tbrJogSpeed.Value);
                                    mYAMAHA.MovP(pt);                                 
                                    Station1.ZaxisServo.MoveTo(Position.Instance.ZaxisPostion[2].pos);
                                }
                                step = 30;
                                break;
                            case 30://机械手到位
                                if (mYAMAHA.CurrentPosX == Position.Instance.RootPostion[1].X &&
                                   mYAMAHA.CurrentPosY == Position.Instance.RootPostion[1].Y &&
                                   mYAMAHA.CurrentPosZ == Position.Instance.RootPostion[1].Z &&
                                   mYAMAHA.CurrentPosR == Position.Instance.RootPostion[1].R &&
                                   Station1.ZaxisServo.IsInPosition(Position.Instance.ZaxisPostion[1].pos) && rdbLeftCalib.Checked)
                                {
                                    step = 40;
                                }
                                if (mYAMAHA.CurrentPosX == Position.Instance.RootPostion[2].X &&
                                   mYAMAHA.CurrentPosY == Position.Instance.RootPostion[2].Y &&
                                   mYAMAHA.CurrentPosZ == Position.Instance.RootPostion[2].Z &&
                                   mYAMAHA.CurrentPosR == Position.Instance.RootPostion[2].R &&
                                   Station1.ZaxisServo.IsInPosition(Position.Instance.ZaxisPostion[2].pos) && rdbRightCalib.Checked)
                                {
                                    step = 40;
                                }
                                break;
                            case 40://拍照（DK）
                                stuts(2);
                                pos1 = Station1.GetDkMark1(10, 10);
                                pos2 = Station1.GetDkMark2(10, 10);
                                pos3 = Station1.GetDkMark3(10, 10);
                                pos4 = Station1.GetDkMark4(10, 10);
                                step = 50;
                                break;
                            case 50://拍照完成，取结果
                                Point<double> p1 = pp(pos1);
                                Point<double> p2 = pp(pos2);
                                Point<double> p3 = pp(pos3);
                                Point<double> p4 = pp(pos4);
                                posd1 = Calib.ConventToPos(p1, p2, p3, p4, Config.Instance.PhotoAngleOffice[0], Config.Instance.PhotoAngleOffice[1],
                                    Config.Instance.PhotoAngleOffice[2], Config.Instance.PhotoAngleOffice[3]);
                                step = 60;
                                break;
                            case 60://Z轴取屏位置，机械手去上方安全位置
                                stuts(3);
                                if (rdbLeftCalib.Checked)
                                {
                                    pt[0] = Position.Instance.RootPostion[3].X;
                                    pt[1] = Position.Instance.RootPostion[3].Y;
                                    pt[2] = Position.Instance.RootPostion[3].Z;
                                    pt[3] = Position.Instance.RootPostion[3].R;
                                    pt[4] = 0;
                                    pt[5] = 0;
                                    pt[6] = (int)(tbrJogSpeed.Value);
                                    mYAMAHA.MovP(pt);                                  
                                    Station1.ZaxisServo.MoveTo(Position.Instance.ZaxisPostion[3].pos);
                                }
                                if (rdbRightCalib.Checked)
                                {
                                    pt[0] = Position.Instance.RootPostion[4].X;
                                    pt[1] = Position.Instance.RootPostion[4].Y;
                                    pt[2] = Position.Instance.RootPostion[4].Z;
                                    pt[3] = Position.Instance.RootPostion[4].R;
                                    pt[4] = 0;
                                    pt[5] = 0;
                                    pt[6] = (int)(tbrJogSpeed.Value);
                                    mYAMAHA.MovP(pt);
                                    Station1.ZaxisServo.MoveTo(Position.Instance.ZaxisPostion[4].pos);
                                }
                                step = 70;
                                break;
                            case 70://机械手到位去组装位置
                                if (mYAMAHA.CurrentPosX == Position.Instance.RootPostion[3].X &&
                                  mYAMAHA.CurrentPosY == Position.Instance.RootPostion[3].Y &&
                                  mYAMAHA.CurrentPosZ == Position.Instance.RootPostion[3].Z &&
                                  mYAMAHA.CurrentPosR == Position.Instance.RootPostion[3].R &&
                                  Station1.ZaxisServo.IsInPosition(Position.Instance.ZaxisPostion[3].pos) && rdbLeftCalib.Checked)
                                {
                                    pt[0] = Position.Instance.RootPostion[11].X;
                                    pt[1] = Position.Instance.RootPostion[11].Y;
                                    pt[2] = Position.Instance.RootPostion[11].Z;
                                    pt[3] = Position.Instance.RootPostion[11].R;
                                    pt[4] = 0;
                                    pt[5] = 0;
                                    pt[6] = (int)(tbrJogSpeed.Value);
                                    mYAMAHA.MovP(pt);
                                    Station1.LeftVacuum.Set();
                                    step = 80;
                                }
                                if (mYAMAHA.CurrentPosX == Position.Instance.RootPostion[4].X &&
                                   mYAMAHA.CurrentPosY == Position.Instance.RootPostion[4].Y &&
                                   mYAMAHA.CurrentPosZ == Position.Instance.RootPostion[4].Z &&
                                   mYAMAHA.CurrentPosR == Position.Instance.RootPostion[4].R &&
                                   Station1.ZaxisServo.IsInPosition(Position.Instance.ZaxisPostion[4].pos) && rdbRightCalib.Checked)
                                {
                                    pt[0] = Position.Instance.RootPostion[12].X;
                                    pt[1] = Position.Instance.RootPostion[12].Y;
                                    pt[2] = Position.Instance.RootPostion[12].Z;
                                    pt[3] = Position.Instance.RootPostion[12].R;
                                    pt[4] = 0;
                                    pt[5] = 0;
                                    pt[6] = (int)(tbrJogSpeed.Value);
                                    mYAMAHA.MovP(pt);
                                    Station1.RightVacuum.Set();
                                    step = 80;
                                }
                                break;
                            case 80://到吸产品位置
                                stuts(4);
                                if (mYAMAHA.CurrentPosX == Position.Instance.RootPostion[11].X &&
                                 mYAMAHA.CurrentPosY == Position.Instance.RootPostion[11].Y &&
                                 mYAMAHA.CurrentPosZ == Position.Instance.RootPostion[11].Z &&
                                 mYAMAHA.CurrentPosR == Position.Instance.RootPostion[11].R &&
                                  Station1.LeftVacuum.OutMoveStatus && rdbLeftCalib.Checked)
                                {
                                    Thread.Sleep(50);
                                    Station1.ZaxisServo.MoveTo(Position.Instance.ZaxisPostion[0].pos);
                                    step = 90;
                                }
                                if (mYAMAHA.CurrentPosX == Position.Instance.RootPostion[12].X &&
                                   mYAMAHA.CurrentPosY == Position.Instance.RootPostion[12].Y &&
                                   mYAMAHA.CurrentPosZ == Position.Instance.RootPostion[12].Z &&
                                   mYAMAHA.CurrentPosR == Position.Instance.RootPostion[12].R &&
                                   Station1.RightVacuum.OutMoveStatus && rdbRightCalib.Checked)
                                {
                                    Thread.Sleep(50);
                                    Station1.ZaxisServo.MoveTo(Position.Instance.ZaxisPostion[0].pos);
                                    step = 90;
                                }
                                break;
                            case 90://Z轴到位，机器人去拍照位置
                                if (Station1.ZaxisServo.IsInPosition(Position.Instance.ZaxisPostion[0].pos)
                                && rdbLeftCalib.Checked)
                                {
                                    pt[0] = Position.Instance.RootPostion[5].X;
                                    pt[1] = Position.Instance.RootPostion[5].Y;
                                    pt[2] = Position.Instance.RootPostion[5].Z;
                                    pt[3] = Position.Instance.RootPostion[5].R;
                                    pt[4] = 0;
                                    pt[5] = 0;
                                    pt[6] = (int)(tbrJogSpeed.Value);
                                    mYAMAHA.MovP(pt);
                                    step = 100;
                                }
                                if (Station1.ZaxisServo.IsInPosition(Position.Instance.ZaxisPostion[0].pos)
                                && rdbLeftCalib.Checked)
                                {
                                    pt[0] = Position.Instance.RootPostion[6].X;
                                    pt[1] = Position.Instance.RootPostion[6].Y;
                                    pt[2] = Position.Instance.RootPostion[6].Z;
                                    pt[3] = Position.Instance.RootPostion[6].R;
                                    pt[4] = 0;
                                    pt[5] = 0;
                                    pt[6] = (int)(tbrJogSpeed.Value);
                                    mYAMAHA.MovP(pt);
                                    step = 100;
                                }
                                break;
                            case 100://机械手去拍照位置
                                stuts(5);
                                if (mYAMAHA.CurrentPosX == Position.Instance.RootPostion[5].X &&
                                mYAMAHA.CurrentPosY == Position.Instance.RootPostion[5].Y &&
                                mYAMAHA.CurrentPosZ == Position.Instance.RootPostion[5].Z &&
                                mYAMAHA.CurrentPosR == Position.Instance.RootPostion[5].R &&
                                  rdbLeftCalib.Checked)
                                {

                                    step = 110;
                                }
                                if (mYAMAHA.CurrentPosX == Position.Instance.RootPostion[6].X &&
                                   mYAMAHA.CurrentPosY == Position.Instance.RootPostion[6].Y &&
                                   mYAMAHA.CurrentPosZ == Position.Instance.RootPostion[6].Z &&
                                   mYAMAHA.CurrentPosR == Position.Instance.RootPostion[6].R &&
                                   rdbRightCalib.Checked)
                                {
                                    step = 110;
                                }
                                break;
                            case 110://拍照
                                stuts(6);
                                pos1 = Station1.GetPmMark1(10, 10);
                                pos2 = Station1.GetPmMark2(10, 10);
                                pos3 = Station1.GetPmMark3(10, 10);
                                pos4 = Station1.GetPmMark4(10, 10);
                                step = 120;
                                break;
                            case 120://拍照完计算
                                Point<double> p11 = pp(pos1);
                                Point<double> p12 = pp(pos2);
                                Point<double> p13 = pp(pos3);
                                Point<double> p14 = pp(pos4);
                                posd2 = Calib.ConventToPos(p11, p12, p13, p14, Config.Instance.PhotoAngleOffice[0], Config.Instance.PhotoAngleOffice[1],
                                    Config.Instance.PhotoAngleOffice[2], Config.Instance.PhotoAngleOffice[3]);
                                step = 130;
                                break;
                            case 130://
                                stuts(7);
                                if (rdbLeftCalib.Checked)
                                {

                                    Config.Instance.OfficePhoto[0].X = posd2.X + posd1.X;
                                    Config.Instance.OfficePhoto[0].Y = posd2.Y + posd1.Y;
                                }
                                if (rdbRightCalib.Checked)
                                {
                                    Config.Instance.OfficePhoto[1].X = posd2.X + posd1.X;
                                    Config.Instance.OfficePhoto[1].Y = posd2.Y + posd1.Y;
                                }
                                step = 140;
                                break;
                            case 140://标定完成
                                MessageBox.Show("标定完成");
                                step = 150;
                                break;
                            default:
                                stuts(0);
                                Global.IsLocating = false;
                                return 0;
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Global.IsLocating = false;
                    LogHelper.Error("设备驱动程序异常" + ex);
                    return -2;
                }
            }, TaskCreationOptions.AttachedToParent | TaskCreationOptions.LongRunning);
            Global.IsLocating = false;
        }

        private Point<double> pp(ArcParam<double> arc)
        {
            Point<double> pos = new Point<double>();
            pos.X = arc.X;
            pos.Y = arc.Y;
            return pos;
        }

        /// <summary>
        /// 标定数据保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalibSave_Click(object sender, System.EventArgs e)
        {
            LogHelper.Debug("标定数据保存：" + Config.Instance.OfficePhoto[0].X.ToString() + ":" + Config.Instance.OfficePhoto[0].Y.ToString());

        }

        private void ProductTeach_Load(object sender, System.EventArgs e)
        {
            SwitchCylinderOperate = new CylinderOperate(Station1.SwichCylinder);
            LeftVacuumOperate = new CylinderOperate(Station1.LeftVacuum);
            RightVacuumOperate = new CylinderOperate(Station1.RightVacuum);
            flowLayoutPanel2.Controls.Add(SwitchCylinderOperate);
            flowLayoutPanel2.Controls.Add(LeftVacuumOperate);
            flowLayoutPanel2.Controls.Add(RightVacuumOperate);
        }
        private void stuts(int step)
        {
            pic1.Image = step == 1 ? Properties.Resources.LedGreen : Properties.Resources.LedNone;
            pic2.Image = step == 2 ? Properties.Resources.LedGreen : Properties.Resources.LedNone;
            pic3.Image = step == 3 ? Properties.Resources.LedGreen : Properties.Resources.LedNone;
            pic4.Image = step == 4 ? Properties.Resources.LedGreen : Properties.Resources.LedNone;
            pic5.Image = step == 5 ? Properties.Resources.LedGreen : Properties.Resources.LedNone;
            pic6.Image = step == 6 ? Properties.Resources.LedGreen : Properties.Resources.LedNone;
            pic7.Image = step == 7 ? Properties.Resources.LedGreen : Properties.Resources.LedNone;
        }

        private void tbrJogSpeed_Scroll(object sender, System.EventArgs e)
        {

        }
    }

}
