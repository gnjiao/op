using CMotion.Interfaces;
using System.Windows.Forms;
using YAMAHA;
namespace desaySV
{
    public partial class RootAxis : UserControl, IRefreshing
    {
        private Yamaha mYAMAHA;
        private double[] pt = new double[6];
        private RootAxis()
        {
            InitializeComponent();
        }
        public RootAxis(Station1 station) : this()
        {
            mYAMAHA = station.YAMAHA;
        }
        public void Refreshing()
        {
            System.Threading.Thread.Sleep(100);
            if (!Global.RootIsLocating)
            {

                lblCurrentPositionX.Text = mYAMAHA.CurrentPosX.ToString();
                lblCurrentPositionY.Text = mYAMAHA.CurrentPosY.ToString();
                lblCurrentPositionZ.Text = mYAMAHA.CurrentPosZ.ToString();
                lblCurrentPositionR.Text = mYAMAHA.CurrentPosR.ToString();

            }
        }

        private void RootAxis_Load(object sender, System.EventArgs e)
        {
            InitdgvPlatePositionRows();
        }

        /// <summary>
        /// 数据初始化
        /// </summary>
        private void InitdgvPlatePositionRows()
        {
            this.dataGridView1.Rows.Clear();
            for (int i = 0; i < Position.Instance.RootPostion.Length; i++)
            {
                dataGridView1.Rows.Add(new object[] {
                   Global.RootName[i],
                    Position.Instance.RootPostion[i].X.ToString("0.000"),
                    Position.Instance.RootPostion[i].Y.ToString("0.000"),
                    Position.Instance.RootPostion[i].Z.ToString("0.000"),
                    Position.Instance.RootPostion[i].R.ToString("0.000"),
                     "定位",
                     "保存"
                    });
            }


        }
        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            switch (e.ColumnIndex)
            {
                case 5:
                    if (MessageBox.Show(string.Format("是否定位{0}的数据", dataGridView1.Rows[e.RowIndex].Cells[0].Value),
                        "定位", MessageBoxButtons.OKCancel) == DialogResult.Cancel) break;
                    if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "定位")
                    {
                        double x = System.Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                        double y = System.Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
                        double z = System.Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
                        double r = System.Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[4].Value);
                        pt[0] = x;
                        pt[1] = y;
                        pt[2] = z;
                        pt[3] = r;
                        pt[4] = 0;
                        pt[5] = 0;
                        pt[6] = (int)(tbrJogSpeed.Value);
                        mYAMAHA.MovP(pt);
                    }
                    break;
                case 6:
                    if (MessageBox.Show(string.Format("是否保存{0}的数据", dataGridView1.Rows[e.RowIndex].Cells[0].Value),
                        "保存", MessageBoxButtons.OKCancel) == DialogResult.Cancel) break;
                    if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "保存")
                    {
                        Position.Instance.RootPostion[e.RowIndex].X = mYAMAHA.CurrentPosX;
                        Position.Instance.RootPostion[e.RowIndex].Y = mYAMAHA.CurrentPosY;
                        Position.Instance.RootPostion[e.RowIndex].Z = mYAMAHA.CurrentPosZ;
                        Position.Instance.RootPostion[e.RowIndex].R = mYAMAHA.CurrentPosR;
                        //Position.Instance.RootPostion[e.RowIndex].Name = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                        InitdgvPlatePositionRows();
                    }
                    break;
                default: break;
            }

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

        private void btnYadd_Click(object sender, System.EventArgs e)
        {

        }
    }
}
