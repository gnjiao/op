using JobNumber;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace desaySV
{
    public partial class FormProductCode : Form
    {

        public FormProductCode(string lbl="请扫描产品条码", Scanner scanner = null)
        {
            InitializeComponent();
            this.ControlBox = false; //取消右上角关闭按钮 
            if (!string.IsNullOrEmpty(lbl))
            { 
                label1.Text = lbl;
            }
           if(scanner!=null)
            sc = scanner;
        }

        private Scanner sc;
        public string ProductSn { get; set; } = string.Empty;

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }




        private void FormProductCode_Load(object sender, EventArgs e)
        {
            //窗体位置
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(1, 1);
            txtSN.Focus();
        }

        private void txtSN_TextChanged(object sender, EventArgs e)
        {

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void txtSN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Login();
            }
        }

        private void Login()
        {

            if (!string.IsNullOrEmpty(txtSN.Text))
            {
                ProductSn = txtSN.Text;
                Close();
            }
            else
            {
                txtSN.ForeColor = Color.Red;
                txtSN.ForeColor = Color.Black;
                txtSN.Text = "";
                txtSN.Focus();
                if (sc != null)
                {
                    sc.Start();
                }
            }

        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            Login();
        }
    }
}
