using System;
using System.ToolKit;
using System.Drawing;
using SqlHelper;
using System.Windows.Forms;
using System.Device;

namespace JobNumber
{
    public partial class svfrmScanAndLogin : Form
    {
        public svfrmScanAndLogin(Scanner scanner, string database)
        {
            InitializeComponent();
            sc = scanner;
            Database = database;
        }

        #region 参数
        private string Database;
        private Scanner sc;

        public bool IsAdmin { get; set; } = false;
        public string OperatorID { get; set; } = "";
        public bool IsCancel { get; set; } = false;
        #endregion
        #region 私有函数
        private void Login()
        {
            bool isLengthSucceed = txtOperatorID.Text.Length == 8 || txtOperatorID.Text.Length == 9;
            bool isTextSucceed = txtOperatorID.Text.Trim().IsMatch("10[ASas]");
            if (isLengthSucceed && isTextSucceed)
            {
                OperatorID = txtOperatorID.Text;
                IsAdmin = new SvSqlHelper(Database).Query_IsOperatorIdAdmin(txtOperatorID.Text);
                Close();
            }
            else
            {
                txtOperatorID.ForeColor = Color.Red;
                System.Threading.Thread.Sleep(500);
                txtOperatorID.ForeColor = Color.Black;
                txtOperatorID.Text = "";
                txtOperatorID.Focus();
                if (sc != null)
                {
                    sc.Start();
                }
            }
        }
        #endregion

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            IsCancel = true;
            Close();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            txtOperatorID.Focus();
            if (sc != null)
            {
                if (sc.IsOpen)
                {
                    sc.Bangding(txtOperatorID);
                    sc.Stop();
                    sc.Start();
                }
            }
        }

        private void txtOperatorID_TextChanged(object sender, EventArgs e)
        {
            Login();
        }

        private void svfrmScanAndLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
