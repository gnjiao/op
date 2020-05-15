using System;
using System.Windows.Forms;

namespace InterLocking
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

#pragma warning disable CS0169 // 从不使用字段“TestForm.InterLocking”
        Locking InterLocking;
#pragma warning restore CS0169 // 从不使用字段“TestForm.InterLocking”

        private void Form1_Load(object sender, EventArgs e)
        {
          
        }
    }
}
