using System;
using System.Data;
using System.Linq;
using System.ToolKit;
using System.Windows.Forms;

namespace JobNumber
{
    public partial class AddModel : Form
    {
        public AddModel(string modelIniFile, Scanner scanner = null)
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
        }
        private Scanner sc;
        private string[] A2Cs;
        private string ModelIniPath;
        private INIFile iNIFile = new INIFile();
        private void AddModel_Load(object sender, EventArgs e)
        {
            if (!FileHelper.Exists(ModelIniPath))
            {
                MessageBox.Show($"找不到机型配置文件{ModelIniPath},请确认文件是否存在");
            }
            A2Cs = ShowModelInfo(ModelIniPath, "");
        }

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

        private void AddModel_Shown(object sender, EventArgs e)
        {
            txtA2C.Focus();
            if (sc != null)
            {
                if (sc.IsOpen)//串口参数正确
                {
                    sc.Bangding(txtA2C);
                    sc.Stop();
                    sc.Start();
                }
            }
        }
        private void SelectMode()
        {
            if (lvwModelInfo.SelectedIndices.Count > 0)
            {
                txtModel.Text = lvwModelInfo.Items[lvwModelInfo.SelectedIndices[0]].SubItems[1].Text;
                txtA2C.Text = lvwModelInfo.Items[lvwModelInfo.SelectedIndices[0]].SubItems[2].Text;
                txtCustomer.Text = lvwModelInfo.Items[lvwModelInfo.SelectedIndices[0]].SubItems[3].Text;
                txtLine.Text = lvwModelInfo.Items[lvwModelInfo.SelectedIndices[0]].SubItems[4].Text;
                //Close();
            }
        }
        private void lvwModelInfo_DoubleClick(object sender, EventArgs e)
        {
            SelectMode();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

            iNIFile.WriteValue(ModelIniPath, txtA2C.Text, "Model", txtModel.Text);
            iNIFile.WriteValue(ModelIniPath, txtA2C.Text, "Scan prefix", txtA2C.Text);
            iNIFile.WriteValue(ModelIniPath, txtA2C.Text, "Line", txtLine.Text);
            iNIFile.WriteValue(ModelIniPath, txtA2C.Text, "Customer", txtCustomer.Text);
            A2Cs = ShowModelInfo(ModelIniPath, "");
            iNIFile.CreateDir(txtA2C.Text.Trim());
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtA2C.Text))
            {
                MessageBox.Show("请选择要删除的项");
                return;
            }
            iNIFile.DeleteSection(ModelIniPath, txtA2C.Text);
            A2Cs = ShowModelInfo(ModelIniPath, "");
        }
    }
}
