using System;
using System.Drawing;
using System.Windows.Forms;

namespace System.Enginee
{
    public partial class InhaleOperate : UserControl
    {
        private readonly Action _manualOperate;
        public InhaleOperate()
        {
            InitializeComponent();
        }
        public InhaleOperate(Action ManualOperate) : this()
        {
            _manualOperate = ManualOperate;
        }
        public string CylinderName
        {
            set
            {
                btn.Text = value;
            }
        }
        public bool OutMove
        {
            set
            {
                btn.BackColor = value ? Color.PaleGreen : Color.Transparent;
            }
        }
        private void btn_Click(object sender, EventArgs e)
        {
            _manualOperate();
        }
    }
}
