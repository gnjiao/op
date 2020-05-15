using System;
using System.Windows.Forms;
using CMotion.Interfaces.IO;
namespace desaySV
{
    public partial class frmIOmonitor : Form
    {
        private IoPoint[] Input;
        private IoPoint[] Output;
        public frmIOmonitor()
        {
            InitializeComponent();
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
        private void frmIOmonitor_Load(object sender, EventArgs e)
        {
            Input = new IoPoint[]
            {
                IoPoints.S1,IoPoints.S2,IoPoints.S3,IoPoints.S4,
                IoPoints.S5,IoPoints.S6,IoPoints.S7,IoPoints.S8,
                IoPoints.S9,IoPoints.S10,IoPoints.S11,IoPoints.S12,
                IoPoints.S13,IoPoints.S14,IoPoints.S15,IoPoints.S16,
                IoPoints.S17,IoPoints.S18,IoPoints.S19,IoPoints.S20,
                IoPoints.S21,IoPoints.S22,IoPoints.S23,IoPoints.S24,
                IoPoints.S25,IoPoints.S26,IoPoints.S27,IoPoints.S28,
                IoPoints.S29,IoPoints.S30,IoPoints.S31,IoPoints.S32

            };
            Output = new IoPoint[]
            {
                IoPoints.Y1,IoPoints.Y2,IoPoints.Y3,IoPoints.Y4,
                IoPoints.Y5,IoPoints.Y6,IoPoints.Y7,IoPoints.Y8,
                IoPoints.Y9,IoPoints.Y10,IoPoints.Y11,IoPoints.Y12,
                IoPoints.Y13,IoPoints.Y14,IoPoints.Y15,IoPoints.Y16,
                IoPoints.Y17,IoPoints.Y18,IoPoints.Y19,IoPoints.Y20,
                IoPoints.Y21,IoPoints.Y22,IoPoints.Y23,IoPoints.Y24,
                IoPoints.Y25,IoPoints.Y26,IoPoints.Y27,IoPoints.Y28,
                IoPoints.Y29,IoPoints.Y30,IoPoints.Y31,IoPoints.Y32,

            };
            InitdgvInputViewRows();
            InitdgvOutputViewRows();
            timer1.Enabled = true;
        }
        private void InitdgvInputViewRows()
        {
            this.dgvInputView.Rows.Clear();

            var i = 1;
            foreach (var di in Input)
            {
                dgvInputView.Rows.Add(new object[] {
                    i.ToString(),
                    di.Value?Properties.Resources.LedGreen:Properties.Resources.LedNone,
                    di.Name,
                    di.Description
                });
                i++;
            }
        }
        private void InitdgvOutputViewRows()
        {
            this.dgvOutputView.Rows.Clear();
            //in a real scenario, you may need to add different rows
            var i = 1;
            foreach (var DO in Output)
            {
                dgvOutputView.Rows.Add(new object[] {
                    i.ToString(),
                    DO.Value?Properties.Resources.LedGreen:Properties.Resources.LedNone,
                    DO.Name,
                    DO.Description
                });
                i++;
            }
        }
        private void refreshdgvInputViewRows()
        {
            //in a real scenario, you may need to add different rows
            var i = 1;
            foreach (var DI in Input)
            {
                dgvInputView.Rows[i - 1].SetValues(new object[] {
                    i.ToString(),
                    DI.Value?Properties.Resources.LedGreen:Properties.Resources.LedNone,
                    DI.Name,
                    DI.Description
                });
                i++;
            }
        }
        private void refreshdgvOutputViewRows()
        {
            //in a real scenario, you may need to add different rows
            var i = 1;
            foreach (var DO in Output)
            {
                dgvOutputView.Rows[i - 1].SetValues(new object[] {
                    i.ToString(),
                    DO.Value?Properties.Resources.LedGreen:Properties.Resources.LedNone,
                    DO.Name,
                    DO.Description
                });
                i++;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            refreshdgvInputViewRows();
            refreshdgvOutputViewRows();
            timer1.Enabled = true;
        }
    }
}
