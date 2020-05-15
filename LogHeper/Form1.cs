using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LogHeper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LogHelper.Error(textBox1.Text.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LogHelper.Debug(textBox2.Text.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LogHelper.Info(textBox3.Text.ToString());
        }
    }
}
