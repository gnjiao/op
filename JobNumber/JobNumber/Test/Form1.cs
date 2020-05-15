using System;
using System.Windows.Forms;
using JobNumber;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login(true);

        }
        Scanner sc;
        private void Login(bool isCloseWhenCancel)
        {
            try
            {
                using (JobNumber.JobNumber frm = new JobNumber.JobNumber())
                {
                    frm.ShowDialog();
                    frm.Hide();
                    if (frm.IsCancel && isCloseWhenCancel)
                    {
                        Close();
                    }
                    textBox1.Text = frm.OperatorID;


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("用户登录异常" + ex);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sc = new Scanner("COM1,115200,None,8,One,1500,1500");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (JobNumber.svfrmScanAndSelectMode frm = new JobNumber.svfrmScanAndSelectMode(@"D:\c#Test\测试模块\JobNumber\Test\bin\Debug\CA5_FCT_Model.ini", "CA5"))
                {
                    frm.ShowDialog();
                    if (frm.IsCancel)
                    {
                        Close();
                        return;
                    }
                    textBox2.Text = frm.A2C;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("扫描调取程序异常" + ex);
            }
        }
    }
}
