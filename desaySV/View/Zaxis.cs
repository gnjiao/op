using CMotion.Interfaces;
using System.Windows.Forms;
using CMotion.Interfaces.Axis;

namespace desaySV.View
{
    public partial class Zaxis : UserControl, IRefreshing
    {
        private Station1 Mstation1;
        public Zaxis(Station1 station)
        {
            Mstation1 = station;
            InitializeComponent();
        }
        public void Refreshing()
        {
            lblCurrentPositionX.Text = Mstation1.ZaxisServo.CurrentPos.ToString();
        }

        /// <summary>
        /// 数据初始化
        /// </summary>
        private void InitdgvPlatePositionRows()
        {
            this.dataGridView1.Rows.Clear();
            for (int i = 0; i < Position.Instance.ZaxisPostion.Length; i++)
            {
                dataGridView1.Rows.Add(new object[] {
                    Global.ZaxisName[i],
                    Position.Instance.ZaxisPostion[i].pos.ToString("0.000"),
                     "定位",
                     "保存"
                    });
            }
        }

        private void Zaxis_Load(object sender, System.EventArgs e)
        {
            InitdgvPlatePositionRows();
        }

        private void btnZadd_MouseDown(object sender, MouseEventArgs e)
        {
            if (!moveSelectHorizontal1.MoveMode.Continue || Global.IsLocating) return;
            Mstation1.ZaxisServo.Postive();
        }

        private void btnZadd_MouseUp(object sender, MouseEventArgs e)
        {
            Mstation1.ZaxisServo.Stop();
        }

        private void btnZdec_MouseDown(object sender, MouseEventArgs e)
        {
            if (!moveSelectHorizontal1.MoveMode.Continue || Global.IsLocating) return;
            Mstation1.ZaxisServo.Negative();
        }

        private void btnZdec_MouseUp(object sender, MouseEventArgs e)
        {
            Mstation1.ZaxisServo.Stop();
        }

        private void btnZadd_Click(object sender, System.EventArgs e)
        {
            if (moveSelectHorizontal1.MoveMode.Continue || Global.IsLocating) return;
            var velocityCurve = new VelocityCurve { Maxvel = Mstation1.ZaxisServo.Speed ?? 0 };
            Mstation1.ZaxisServo.MoveDelta(1 * moveSelectHorizontal1.MoveMode.Distance, velocityCurve);
        }

        private void btnZdec_Click(object sender, System.EventArgs e)
        {
            if (moveSelectHorizontal1.MoveMode.Continue || Global.IsLocating) return;
            var velocityCurve = new VelocityCurve { Maxvel = Mstation1.ZaxisServo.Speed ?? 0 };
            Mstation1.ZaxisServo.MoveDelta(-1 * moveSelectHorizontal1.MoveMode.Distance, velocityCurve);
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
                        Position.Instance.ZaxisPostion[e.RowIndex].pos = Mstation1.ZaxisServo.CurrentPos;
                        Position.Instance.ZaxisPostion[e.RowIndex].Name = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                        InitdgvPlatePositionRows();
                    }
                    break;
                default: break;
            }

        }

        private void MoveTo(double pos)
        {
            Mstation1.ZaxisServo.MoveTo(pos, new VelocityCurve() { Maxvel = Mstation1.ZaxisServo.Speed ?? 0 });

        }

        private void tbrJogSpeed_Scroll(object sender, System.EventArgs e)
        {
            Mstation1.ZaxisServo.Speed = tbrJogSpeed.Value;
        }
    }
}
