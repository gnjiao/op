using System;
using System.ToolKit;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace JobNumber
{
    public partial class svfrmScanAndSelectMode : Form
    {

        public svfrmScanAndSelectMode(string modelIniFile, bool isAdmin = false, Scanner scanner = null)
        {
            InitializeComponent();
            this.lvwModelInfo.View = View.Details;
            this.lvwModelInfo.GridLines = true;
            this.lvwModelInfo.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.lvwModelInfo.FullRowSelect = true;//是否可以选择行
            this.lvwModelInfo.Columns.Add("No.", 30, HorizontalAlignment.Center);
            this.lvwModelInfo.Columns.Add("Model Name", 120, HorizontalAlignment.Center);
            this.lvwModelInfo.Columns.Add("A2C", 135, HorizontalAlignment.Center);
            this.lvwModelInfo.Columns.Add("Customer", 135, HorizontalAlignment.Center);
            this.lvwModelInfo.Columns.Add("Line", 50, HorizontalAlignment.Center);
            ModelIniPath = modelIniFile;
            sc = scanner;
            btnADD.Visible = isAdmin;

        }


        #region 参数
        private Scanner sc;
        private string[] A2Cs;
        private string ModelIniPath;
        private INIFile iNIFile = new INIFile();

       
        public string Model { get; set; } = "";
        public string A2C { get; set; } = "";
        public string Customer { get; set; } = "";
        public string Line { get; set; } = "";
        #endregion

        #region 私有函数
        /// <summary>
        /// 列举所有满足匹配文本的项目在控件中,返回所有机型的A2C
        /// </summary>
        /// <param name="iniPath"></param>
        /// <returns></returns>
        private string[] ShowModelInfo(string iniPath, string matchStr)
        {
            string[] sections = iNIFile.ReadAllSectionNames(iniPath).Where(x => x.IsMatch(matchStr)).ToArray();
            this.lvwModelInfo.Clear();
            this.lvwModelInfo.View = View.Details;
            this.lvwModelInfo.GridLines = true;
            this.lvwModelInfo.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            this.lvwModelInfo.FullRowSelect = true;//是否可以选择行
            this.lvwModelInfo.Columns.Add("No.", 30, HorizontalAlignment.Center);
            this.lvwModelInfo.Columns.Add("Model Name", 120, HorizontalAlignment.Center);
            this.lvwModelInfo.Columns.Add("A2C", 135, HorizontalAlignment.Center);
            this.lvwModelInfo.Columns.Add("Customer", 135, HorizontalAlignment.Center);
            this.lvwModelInfo.Columns.Add("Line", 50, HorizontalAlignment.Center);
            string[] a2cs = new string[sections.Length];

            for (int i = 0; i < sections.Length; i++)
            {
                a2cs[i] = iNIFile.ReadValue(iniPath, sections[i], "Scan prefix");
                ListViewItem listViewItem = new ListViewItem();

                listViewItem.SubItems[0].Text = $"{i + 1}";
                listViewItem.SubItems.Add(iNIFile.ReadValue(iniPath, sections[i], "Model"));
                listViewItem.SubItems.Add(iNIFile.ReadValue(iniPath, sections[i], "Scan prefix"));
                listViewItem.SubItems.Add(iNIFile.ReadValue(iniPath, sections[i], "Customer"));
                listViewItem.SubItems.Add(iNIFile.ReadValue(iniPath, sections[i], "Line"));
                this.lvwModelInfo.Items.Add(listViewItem);
            }
            return a2cs;
        }

        public static ListView Replacess(ListView lsv, string[,] _replaceArray, int _rowIndex = 0, int _columnIndex = 0)
        {
            for (int i = 0; i < _replaceArray.GetLength(1); i++)
            {
                for (int j = 0; j < _replaceArray.GetLength(0); j++)
                {
                    lsv.Items[i + _rowIndex].SubItems[j + _columnIndex].Text = _replaceArray[j, i].ToString();
                }
            }
            return lsv;
        }


        /// <summary>
        /// 根据扫描框内容进行选择机型(找到后自动关闭,找不到则清空扫描框)
        /// </summary>
        private void FindAndSelectMode()
        {
            int[] a2cIndices = A2Cs.FindMatchIndices(txtSN.Text.Left(12));
            if (a2cIndices.Length == 1)
            {
                Model = lvwModelInfo.Items[a2cIndices[0]].SubItems[1].Text;
                A2C = lvwModelInfo.Items[a2cIndices[0]].SubItems[2].Text;
                Customer = lvwModelInfo.Items[a2cIndices[0]].SubItems[3].Text;
                Close();
            }
            else
            {
                ShowModelInfo(ModelIniPath, txtSN.Text.Left(12));
            }
            if (sc != null)
            {
                sc.Start();
            }
        }
        private void SelectMode()
        {
            if (lvwModelInfo.SelectedIndices.Count > 0)
            {
                Model = lvwModelInfo.Items[lvwModelInfo.SelectedIndices[0]].SubItems[1].Text;
                A2C = lvwModelInfo.Items[lvwModelInfo.SelectedIndices[0]].SubItems[2].Text;
                Customer = lvwModelInfo.Items[lvwModelInfo.SelectedIndices[0]].SubItems[3].Text;
                Line= lvwModelInfo.Items[lvwModelInfo.SelectedIndices[0]].SubItems[4].Text;
                iNIFile.CreateDir(A2C);
                Close();
            }
        }
        private void SelectMode(int index)
        {
            if (lvwModelInfo.Items.Count >= index + 1)
            {
                Model = lvwModelInfo.Items[index].SubItems[1].Text;
                A2C = lvwModelInfo.Items[index].SubItems[2].Text;
                Customer = lvwModelInfo.Items[index].SubItems[3].Text;
                Line = lvwModelInfo.Items[index].SubItems[4].Text;
                iNIFile.CreateDir(A2C);
                Close();
            }
        }
        #endregion

        private void svfrmScanAndSelectMode_Load(object sender, EventArgs e)
        {
            if (!FileHelper.Exists(ModelIniPath))
            {
                MessageBox.Show($"找不到机型配置文件{ModelIniPath},请确认文件是否存在");
            }
            A2Cs = ShowModelInfo(ModelIniPath, "");
        }

        private void svfrmScanAndSelectMode_Shown(object sender, EventArgs e)
        {
            txtSN.Focus();
            if (sc != null)
            {
                if (sc.IsOpen)//串口参数正确
                {
                    sc.Bangding(txtSN);
                    sc.Stop();
                    sc.Start();
                }
            }
        }

        private void txtSN_TextChanged(object sender, EventArgs e)
        {
            if (txtSN.Text.Trim().Length == 12)
            {
                A2Cs = ShowModelInfo(ModelIniPath, txtSN.Text.Left(12));
                FindAndSelectMode();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            txtSN_TextChanged(null, null);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SelectMode();
        }

        private void btnCanel_Click(object sender, EventArgs e)
        {            
            Close();
        }

        private void txtSN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (int)Keys.Enter)
            {
                txtSN_TextChanged(null, null);
            }
            for (int i = 0; i < 12; i++)//按F1~F12快速选择机型
            {
                if (e.KeyValue == (int)Keys.F1 + i)
                {
                    SelectMode(i);
                }
            }
        }

        private void lvwModelInfo_DoubleClick_1(object sender, EventArgs e)
        {
            SelectMode();
        }

        private void btnADD_Click(object sender, EventArgs e)
        {
            using (AddModel frm = new AddModel(ModelIniPath))
            {
                frm.ShowDialog();
                A2Cs = ShowModelInfo(ModelIniPath, "");
            }
        }
    }
}
