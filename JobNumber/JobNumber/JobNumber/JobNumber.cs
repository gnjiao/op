using System;
using System.Drawing;
using System.ToolKit;
using System.Windows.Forms;

namespace JobNumber
{
    public partial class JobNumber : Form
    {
        public JobNumber(Scanner scanner)
        {
            InitializeComponent();
            sc = scanner;

        }
        public JobNumber()
        {
            InitializeComponent();
        }
        #region 参数

        private Scanner sc;

        public bool IsAdmin { get; set; } = false;
        public string OperatorID { get; set; } = "";
        public bool IsCancel { get; set; } = false;

        public bool isOperatorAdmin { get; set; } = false;

        public string AdminPassWord { get; set; } = MD5.TextToMd5("123456");
        #endregion
        private void JobNumber_Load(object sender, EventArgs e)
        {
            //窗体位置
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(1, 1);
            JobNumBox.Focus();
        }

        #region 私有函数
        private void Login()
        {
            if (MD5.TextToMd5(JobNumBox.Text) == AdminPassWord)
            {
                isOperatorAdmin = true;
                OperatorID = "10S88888";
                Close();
            }
            if (JobNumBox.Text.Length == 8 && (JobNumBox.Text.ToUpper().IndexOf("10S") == 0 || JobNumBox.Text.ToUpper().IndexOf("10A") == 0) || JobNumBox.Text.Length == 9 && JobNumBox.Text.ToUpper().IndexOf("10A") == 0)
            {
                OperatorID = JobNumBox.Text;
                IsAdmin = JobNumBox.Text.ToUpper().IndexOf("10S") == 0 ? true : false;
                Close();
            }
            else
            {
                JobNumBox.ForeColor = Color.Red;
                JobNumBox.ForeColor = Color.Black;
                JobNumBox.Text = "";
                JobNumBox.Focus();
                if (sc != null)
                {
                    sc.Start();
                }
                IsAdmin = false;
            }

        }


        #endregion
        private void confirmButton_Click(object sender, EventArgs e)
        {
            Login();
        }
        #region 控件事件
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void JobNumBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login();
            }
        }

        private void JobNumBox_TextChanged(object sender, EventArgs e)
        {
            //Login();
        }
        #endregion

        private void JobNumber_Shown(object sender, EventArgs e)
        {
            JobNumBox.Focus();
            if (sc != null)
            {
                if (sc.IsOpen)
                {
                    sc.Bangding(JobNumBox);
                    sc.Stop();
                    sc.Start();
                }
            }
        }
        DateTime _dt = DateTime.Now;
        private void JobNumBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            DateTime tempDt = DateTime.Now;
            TimeSpan ts = tempDt.Subtract(_dt);
            if (ts.Milliseconds > 50)
            {
                JobNumBox.Clear();
                JobNumBox.Focus();
            }
            _dt = tempDt;
        }
    }
}
