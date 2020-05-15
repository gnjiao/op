using System;
using System.Windows.Forms;
using System.ToolKit.Helpers;

namespace InterLocking
{
    public partial class FrmInterLockingSet : Form
    {
        private string path;
        public FrmInterLockingSet(string mpath)
        {
            InitializeComponent();
            path = mpath;
        }

        private void FrmInterLockingSet_Load(object sender, EventArgs e)
        {
            NumSetCount.Value = interLockingParam.Instance.InterLockingListParam.Count;
            InitdgvPlatePositionRows();
        }

        /// <summary>
        /// 数据初始化
        /// </summary>
        private void InitdgvPlatePositionRows()
        {

            this.dgvSetdata.Rows.Clear();
            for (int i = 0; i < interLockingParam.Instance.InterLockingListParam.Count; i++)
            {
                switch (interLockingParam.Instance.InterLockingListParam[i].Istype)
                {
                    case 0:
                        dgvSetdata.Rows.Add(new object[] {
                             i.ToString(),
                             "Record",
                             "",
                             "",
                             "",
                             "",
                             ""
                             });
                        break;
                    case 1:
                        dgvSetdata.Rows.Add(new object[] {
                             i.ToString(),
                             "NumericTest",
                              interLockingParam.Instance.InterLockingListParam[i].MaxLimits.ToString(),
                              interLockingParam.Instance.InterLockingListParam[i].MinLimits.ToString(),
                              "",
                              "0",
                              ""
                             });
                        break;
                    case 2:
                        dgvSetdata.Rows.Add(new object[] {
                             i.ToString(),
                             "StringValueTest",
                             "",
                             "",
                              interLockingParam.Instance.InterLockingListParam[i].limitString,
                              "0",
                              ""
                             });
                        break;
                    case 3:
                        dgvSetdata.Rows.Add(new object[] {
                             i.ToString(),
                             "PassFailTest",
                             "",
                             "",
                             "",
                             "0",
                             "Fail"
                             });
                        break;
                }

            }
        }
        private void btnSet_Click(object sender, EventArgs e)
        {
            int value = (int)NumSetCount.Value;
            int value1 = 0;
            if (interLockingParam.Instance.InterLockingListParam.Count > value)
            {
                value1 = interLockingParam.Instance.InterLockingListParam.Count - value;
                for (int i = 0; i < value1; i++)
                {
                    MesData mesData = interLockingParam.Instance.InterLockingListParam[interLockingParam.Instance.InterLockingListParam.Count - i - 1];
                    interLockingParam.Instance.InterLockingListParam.Remove(mesData);
                }
            }
            if (interLockingParam.Instance.InterLockingListParam.Count < value)
            {
                value1 = value - interLockingParam.Instance.InterLockingListParam.Count;
                for (int i = 0; i < value1; i++)
                {
                    MesData mesData = new MesData();
                    interLockingParam.Instance.InterLockingListParam.Add(mesData);
                }
            }
            InitdgvPlatePositionRows();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MesData mesData = new MesData();
            int Count = interLockingParam.Instance.InterLockingListParam.Count;
            for (int i = 0; i < Count; i++)
            {
                string value = dgvSetdata.Rows[i].Cells[1].Value.ToString();
                switch (value)
                {
                    case "":
                        MessageBox.Show("未选择类型");
                        return;
                    case "Record":
                        mesData = interLockingParam.Instance.InterLockingListParam[i];
                        mesData.Istype = 0;
                        mesData.limitString = "";
                        mesData.MaxLimits = 0;
                        mesData.MinLimits = 0;
                        mesData.StringValue = "默认值";
                        interLockingParam.Instance.InterLockingListParam.Add(mesData);
                        break;
                    case "NumericTest":
                        mesData = interLockingParam.Instance.InterLockingListParam[i];
                        mesData.Istype = 1;
                        mesData.limitString = "";
                        mesData.MaxLimits = Convert.ToInt32(dgvSetdata.Rows[i].Cells[2].Value);
                        mesData.MinLimits = Convert.ToInt32(dgvSetdata.Rows[i].Cells[3].Value);
                        mesData.Value = 9999;
                        interLockingParam.Instance.InterLockingListParam.Add(mesData);
                        break;
                    case "StringValueTest":
                        mesData = interLockingParam.Instance.InterLockingListParam[i];
                        mesData.Istype = 2;
                        mesData.limitString = dgvSetdata.Rows[i].Cells[4].Value.ToString();
                        mesData.MaxLimits = 0;
                        mesData.MinLimits = 0;
                        mesData.StringValue = "默认值";
                        interLockingParam.Instance.InterLockingListParam.Add(mesData);
                        break;
                    case "PassFailTest":
                        mesData = interLockingParam.Instance.InterLockingListParam[i];
                        mesData.Istype = 3;
                        mesData.limitString = "";
                        mesData.MaxLimits = 0;
                        mesData.MinLimits = 0;
                        mesData.StringValue = "Fail";
                        interLockingParam.Instance.InterLockingListParam.Add(mesData);
                        break;
                };
            }

            interLockingParam.Instance.InterLockingListParam.RemoveRange(0, Count);


            SerializerManager<interLockingParam>.Instance.Save(path, interLockingParam.Instance);
            InitdgvPlatePositionRows();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Locking mLocking = new Locking();
            interLockingParam.Instance.EvData.SerialNumber = "123456789012";
            mLocking.Test_WriteMesTxtAndCsvFile();
        }
    }
}
