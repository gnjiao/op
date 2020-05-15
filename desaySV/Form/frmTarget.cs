using System;
using System.Windows.Forms;
using System.Drawing;

namespace desaySV
{
    public partial class Target : Form
    {


        public string TargetNumber { get; set; }

        public Target()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TargetNumber = Target_numericUpDown.Value.ToString();
            this.Close();
        }

        private void Target_Load(object sender, EventArgs e)
        {
            //窗体位置
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(1, 1);
            button1.Focus();
        }

        private void Target_numericUpDown_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;//非数字则禁止输入
            }
        }

        private void Target_FormClosing(object sender, FormClosingEventArgs e)
        {
            TargetNumber = Target_numericUpDown.Value.ToString();
        }
    }
}
