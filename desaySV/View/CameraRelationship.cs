using CMotion.Interfaces;
using System.Windows.Forms;
using System.Threading;
using YAMAHA;
using System.Threading.Tasks;
using LogHeper;
using System.ToolKit;
namespace desaySV.View
{
    public partial class CameraRelationship : UserControl, IRefreshing
    {
        private Yamaha mYAMAHA;
        private Station1 Station1;
        private double[] pt = new double[6];
        /// <summary>
        /// 标定基准位置拍照结果
        /// </summary>
        private ArcParam<double>[] CalibPos = new ArcParam<double>[4];
        /// <summary>
        ///  标定X终点位置拍照结果
        /// </summary>
        private ArcParam<double>[] CalibPosXend = new ArcParam<double>[4];
        /// <summary>
        ///  标定Y终点位置拍照结果
        /// </summary>
        private ArcParam<double>[] CalibPosYend = new ArcParam<double>[4];

        private double[] angle = new double[4];
        private double[] angle1 = new double[4];
        private CameraRelationship()
        {
            InitializeComponent();
        }
        public CameraRelationship(Station1 station) : this()
        {
            mYAMAHA = station.YAMAHA;
            Station1 = station;
        }

        public void Refreshing()
        {

            if (!Global.RootIsLocating)
            {
                lblCurrentPositionX.Text = mYAMAHA.CurrentPosX.ToString();
                lblCurrentPositionY.Text = mYAMAHA.CurrentPosY.ToString();
                lblCurrentPositionZ.Text = mYAMAHA.CurrentPosZ.ToString();
                lblCurrentPositionR.Text = mYAMAHA.CurrentPosR.ToString();
              
            }
        }

        private void CameraRelationship_Load(object sender, System.EventArgs e)
        {

            lblXphotoPosition.Text = Config.Instance.PhotoCalibPostion.X.ToString();
            lblYphotoPosition.Text = Config.Instance.PhotoCalibPostion.Y.ToString();
            lblZphotoPosition.Text = Config.Instance.PhotoCalibPostion.Z.ToString();
            lblRphotoPosition.Text = Config.Instance.PhotoCalibPostion.R.ToString();
            numXgoto.Value = (decimal)Config.Instance.PhotoCalibEndPostion.X;
            numYgoto.Value = (decimal)Config.Instance.PhotoCalibEndPostion.Y;

            InitdgvPlatePositionRows();

        }

        /// <summary>
        /// 数据初始化
        /// </summary>
        private void InitdgvPlatePositionRows()
        {
            this.dataGridView2.Rows.Clear();
            dataGridView2.Rows.Add(new object[] {
                    Config.Instance.PhotoAngleOffice[0].ToString("0.000") ,
                    Config.Instance.PhotoAngleOffice[1].ToString("0.000"),
                    Config.Instance.PhotoAngleOffice[2].ToString("0.000"),
                    Config.Instance.PhotoAngleOffice[3].ToString("0.000")
                    });


        }
        /// <summary>
        /// 保存标定坐标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_Click(object sender, System.EventArgs e)
        {
            Config.Instance.PhotoCalibPostion.X = mYAMAHA.CurrentPosX;
            Config.Instance.PhotoCalibPostion.Y = mYAMAHA.CurrentPosY;
            Config.Instance.PhotoCalibPostion.Z = mYAMAHA.CurrentPosZ;
            Config.Instance.PhotoCalibPostion.R = mYAMAHA.CurrentPosR;
            Config.Instance.PhotoCalibEndPostion.X = (double)numXgoto.Value;
            Config.Instance.PhotoCalibEndPostion.Y = (double)numYgoto.Value;
            lblXphotoPosition.Text = Config.Instance.PhotoCalibPostion.X.ToString();
            lblYphotoPosition.Text = Config.Instance.PhotoCalibPostion.Y.ToString();
            lblZphotoPosition.Text = Config.Instance.PhotoCalibPostion.Z.ToString();
            lblRphotoPosition.Text = Config.Instance.PhotoCalibPostion.R.ToString();
            LogHelper.Debug("基准点位置保存：" + lblXphotoPosition.Text + ":" + lblYphotoPosition.Text
                + ":" + lblZphotoPosition.Text + ":" + lblRphotoPosition.Text);
        }
        /// <summary>
        /// 走标定位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGotoStart_Click(object sender, System.EventArgs e)
        {
            pt[0] = Config.Instance.PhotoCalibPostion.X;
            pt[1] = Config.Instance.PhotoCalibPostion.Y;
            pt[2] = Config.Instance.PhotoCalibPostion.Z;
            pt[3] = Config.Instance.PhotoCalibPostion.R;
            pt[4] = 0;
            pt[5] = 0;
            pt[6] = (int)(tbrJogSpeed.Value);
            mYAMAHA.MovP(pt);          
        }

        private void BtnGotoYend_Click(object sender, System.EventArgs e)
        {
            pt[0] = Config.Instance.PhotoCalibPostion.X;
            pt[1] = Config.Instance.PhotoCalibPostion.Y +
                Config.Instance.PhotoCalibEndPostion.Y;
            pt[2] = Config.Instance.PhotoCalibPostion.Z;
            pt[3] = Config.Instance.PhotoCalibPostion.R;
            pt[4] = 0;
            pt[5] = 0;
            pt[6] = (int)(tbrJogSpeed.Value);
            mYAMAHA.MovP(pt);         
        }

        private void BtnGotoXend_Click(object sender, System.EventArgs e)
        {
            pt[0] = Config.Instance.PhotoCalibPostion.X +
                Config.Instance.PhotoCalibEndPostion.X;
            pt[1] = Config.Instance.PhotoCalibPostion.Y;
            pt[2] = Config.Instance.PhotoCalibPostion.Z;
            pt[3] = Config.Instance.PhotoCalibPostion.R;
            pt[4] = 0;
            pt[5] = 0;
            pt[6] = (int)(tbrJogSpeed.Value);
            mYAMAHA.MovP(pt);           
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
        /// 四相机关系标定计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show(string.Format("是否保存数据"), "保存", MessageBoxButtons.OKCancel) == DialogResult.Cancel) return;
            int step = 0;
            Global.IsLocating = true;
            Point<double> pos = new Point<double>();
            Point<double> pos1 = new Point<double>();

            Task.Factory.StartNew(() =>
            {
                try
                {

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
                            case 0:
                                pt[0] = Config.Instance.PhotoCalibPostion.X;
                                pt[1] = Config.Instance.PhotoCalibPostion.Y;
                                pt[2] = Config.Instance.PhotoCalibPostion.Z;
                                pt[3] = Config.Instance.PhotoCalibPostion.R;
                                pt[4] = 0;
                                pt[5] = 0;
                                pt[6] = (int)(tbrJogSpeed.Value);
                                mYAMAHA.MovP(pt);
                              
                                step = 10;
                                break;
                            case 10:
                                if (mYAMAHA.CurrentPosX == Config.Instance.PhotoCalibPostion.X &&
                                mYAMAHA.CurrentPosY == Config.Instance.PhotoCalibPostion.Y &&
                                mYAMAHA.CurrentPosZ == Config.Instance.PhotoCalibPostion.Z &&
                                mYAMAHA.CurrentPosR == Config.Instance.PhotoCalibPostion.R)
                                {
                                    step = 20;
                                }
                                break;
                            case 20:
                                CalibPos[0] = Station1.getMarkSenter1(0, 0);
                                CalibPos[1] = Station1.getMarkSenter2(0, 0);
                                CalibPos[2] = Station1.getMarkSenter3(0, 0);
                                CalibPos[3] = Station1.getMarkSenter4(0, 0);
                                step = 30;
                                break;
                            case 30:
                                pt[0] = Config.Instance.PhotoCalibPostion.X +
                                        Config.Instance.PhotoCalibEndPostion.X;
                                pt[1] = Config.Instance.PhotoCalibPostion.Y;
                                pt[2] = Config.Instance.PhotoCalibPostion.Z;
                                pt[3] = Config.Instance.PhotoCalibPostion.R;
                                pt[4] = 0;
                                pt[5] = 0;
                                pt[6] = (int)(tbrJogSpeed.Value);
                                mYAMAHA.MovP(pt);
                                step = 40;
                                break;
                            case 40:
                                if (mYAMAHA.CurrentPosX == Config.Instance.PhotoCalibPostion.X +
                                   Config.Instance.PhotoCalibEndPostion.X &&
                                   mYAMAHA.CurrentPosY == Config.Instance.PhotoCalibPostion.Y &&
                                   mYAMAHA.CurrentPosZ == Config.Instance.PhotoCalibPostion.Z &&
                                   mYAMAHA.CurrentPosR == Config.Instance.PhotoCalibPostion.R)
                                {
                                    step = 50;
                                }
                                break;
                            case 50:
                                CalibPosXend[0] = Station1.getMarkSenter1(100, 0);
                                CalibPosXend[1] = Station1.getMarkSenter2(100, 0);
                                CalibPosXend[2] = Station1.getMarkSenter3(100, 0);
                                CalibPosXend[3] = Station1.getMarkSenter4(100, 0);
                                step = 60;
                                break;
                            case 60:
                                pt[0] = Config.Instance.PhotoCalibPostion.X;
                                pt[1] = Config.Instance.PhotoCalibPostion.Y +
                                Config.Instance.PhotoCalibEndPostion.Y;
                                pt[2] = Config.Instance.PhotoCalibPostion.Z;
                                pt[3] = Config.Instance.PhotoCalibPostion.R;
                                pt[4] = 0;
                                pt[5] = 0;
                                pt[6] = (int)(tbrJogSpeed.Value);
                                mYAMAHA.MovP(pt);                              
                                step = 70;
                                break;
                            case 70:
                                if (mYAMAHA.CurrentPosX == Config.Instance.PhotoCalibPostion.X &&
                                   mYAMAHA.CurrentPosY == Config.Instance.PhotoCalibPostion.Y +
                                   Config.Instance.PhotoCalibEndPostion.Y &&
                                   mYAMAHA.CurrentPosZ == Config.Instance.PhotoCalibPostion.Z &&
                                   mYAMAHA.CurrentPosR == Config.Instance.PhotoCalibPostion.R)
                                {
                                    step = 80;
                                }
                                break;
                            case 80:
                                CalibPosYend[0] = Station1.getMarkSenter1(0, 100);
                                CalibPosYend[1] = Station1.getMarkSenter2(0, 100);
                                CalibPosYend[2] = Station1.getMarkSenter3(0, 100);
                                CalibPosYend[3] = Station1.getMarkSenter4(0, 100);
                                step = 90;
                                break;
                            case 90://计算相机偏移
                                for (int i = 0; i < 4; i++)
                                {
                                    Point<double> p1 = new Point<double>();
                                    Point<double> p2 = new Point<double>();
                                    p1.X = Config.Instance.PhotoCalibEndPostion.X;
                                    p1.Y = 0;
                                    p2.X = 0;
                                    p2.Y = Config.Instance.PhotoCalibEndPostion.Y;
                                    pos.X = CalibPosXend[i].X - CalibPos[i].X;
                                    pos.Y = CalibPosXend[i].Y - CalibPos[i].Y;
                                    pos1.X = CalibPosYend[i].X - CalibPos[i].X;
                                    pos1.Y = CalibPosYend[i].Y - CalibPos[i].Y;
                                    angle[i] = Calib.RotationAngle(p1, pos, out bool result);
                                    angle1[i] = Calib.RotationAngle(p2, pos1, out bool result1);
                                }
                                InitdgvPlatePositionRows1();
                                step = 100;
                                break;

                            default:
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

        private void InitdgvPlatePositionRows1()
        {
            this.dataGridView1.Rows.Clear();
            for (int i = 0; i < 4; i++)
            {
                dataGridView1.Rows.Add(new object[] {
                    i.ToString("0.000") ,
                    angle[i].ToString("0.000"),
                    angle1[i].ToString("0.000")
                    });
            }
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            LogHelper.Debug("标定数据保存：" + Config.Instance.PhotoAngleOffice[0].ToString() + ":" + Config.Instance.PhotoAngleOffice[1].ToString()
               + ":" + Config.Instance.PhotoAngleOffice[2].ToString() + ":" + Config.Instance.PhotoAngleOffice[3].ToString());
            Config.Instance.PhotoAngleOffice = angle;
            InitdgvPlatePositionRows();
        }
    }
}
