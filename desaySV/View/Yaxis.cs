using CMotion.Interfaces;
using System.Windows.Forms;
using CMotion.Interfaces.Axis;

namespace desaySV.View
{
    public partial class Yaxis : UserControl, IRefreshing
    {
        private Station1 Mstation1;

        public Yaxis(Station1 station)
        {
            Mstation1 = station;
            InitializeComponent();
        }
        public void Refreshing()
        {
            lblCurrentPositionY.Text = Mstation1.YaxisServo.CurrentPos.ToString();
        }



        private void Yaxis_Load(object sender, System.EventArgs e)
        {
            tbrJogSpeed.Value = (int)Mstation1.YaxisServo.Speed;
            InitdgvPlatePositionRows();
        }

        private void btnYadd_MouseDown(object sender, MouseEventArgs e)
        {
            if (!moveSelectHorizontal1.MoveMode.Continue || Global.IsLocating) return;
            Mstation1.YaxisServo.Postive();
        }

        private void btnYadd_MouseUp(object sender, MouseEventArgs e)
        {
            Mstation1.YaxisServo.Stop();
        }

        private void btnYdec_MouseDown(object sender, MouseEventArgs e)
        {
            if (!moveSelectHorizontal1.MoveMode.Continue || Global.IsLocating) return;
            Mstation1.YaxisServo.Negative();
        }

        private void btnYdec_MouseUp(object sender, MouseEventArgs e)
        {
            Mstation1.YaxisServo.Stop();
        }

        private void btnYadd_Click(object sender, System.EventArgs e)
        {
            if (moveSelectHorizontal1.MoveMode.Continue || Global.IsLocating) return;
            var velocityCurve = new VelocityCurve { Maxvel = Mstation1.YaxisServo.Speed ?? 0 };
            Mstation1.YaxisServo.MoveDelta(1 * moveSelectHorizontal1.MoveMode.Distance, velocityCurve);
        }

        private void btnYdec_Click(object sender, System.EventArgs e)
        {
            if (moveSelectHorizontal1.MoveMode.Continue || Global.IsLocating) return;
            var velocityCurve = new VelocityCurve { Maxvel = Mstation1.YaxisServo.Speed ?? 0 };
            Mstation1.YaxisServo.MoveDelta(-1 * moveSelectHorizontal1.MoveMode.Distance, velocityCurve);
        }

        /// <summary>
        /// 数据初始化
        /// </summary>
        private void InitdgvPlatePositionRows()
        {
            this.dataGridView1.Rows.Clear();
            for (int i = 0; i < Position.Instance.YaxisPostion.Length; i++)
            {
                dataGridView1.Rows.Add(new object[] {
                     Global.YaxisName[i],
                     Position.Instance.YaxisPostion[i].pos.ToString("0.000"),
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
                case 2:
                    if (MessageBox.Show(string.Format("是否定位{0}的数据", dataGridView1.Rows[e.RowIndex].Cells[0].Value),
                        "定位", MessageBoxButtons.OKCancel) == DialogResult.Cancel) break;
                    if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "定位")
                    {
                        double x = System.Convert.ToDouble(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                        MoveTo(x);
                    }
                    break;
                case 3:
                    if (MessageBox.Show(string.Format("是否保存{0}的数据", dataGridView1.Rows[e.RowIndex].Cells[0].Value),
                        "保存", MessageBoxButtons.OKCancel) == DialogResult.Cancel) break;
                    if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "保存")
                    {
                        Position.Instance.YaxisPostion[e.RowIndex].pos = Mstation1.YaxisServo.CurrentPos;
                        Position.Instance.YaxisPostion[e.RowIndex].Name = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                        InitdgvPlatePositionRows();
                    }
                    break;
                default: break;
            }

        }

        private void MoveTo(double pos)
        {
            Mstation1.YaxisServo.MoveTo(pos, new VelocityCurve() { Maxvel = Mstation1.YaxisServo.Speed ?? 0 });

        }

        private void tbrJogSpeed_Scroll(object sender, System.EventArgs e)
        {
            Mstation1.YaxisServo.Speed = tbrJogSpeed.Value;
        }
    }
}
