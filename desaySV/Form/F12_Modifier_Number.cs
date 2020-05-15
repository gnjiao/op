using ConfigPath;
using System;
using System.ToolKit.Helpers;
using System.Windows.Forms;

namespace desaySV
{
    public partial class F12_Modifier_Number : Form
    {
        public F12_Modifier_Number()
        {
            InitializeComponent();
        }
        public static string OUTPUT_Modifier = "0";
        public static string NG_Modifier = "0";
        public static string Rate_Modifier = "0";
        public static string Modifier_Tpye;
        private void button1_Click(object sender, EventArgs e)
        {

            if (numericUpDown_output.Value != 0 || numericUpDown_NG.Value != 0)
            {
                ProductConfig.Instance.ProductOkTotal = Convert.ToInt16(numericUpDown_output.Value);
                ProductConfig.Instance.ProductNgTotal = Convert.ToInt16(numericUpDown_NG.Value);
                SerializerManager<ProductConfig>.Instance.Save(AppConfig.ConfigOtherParamName, ProductConfig.Instance);
            }

            this.Close();
        }

        private void F12_Modifier_Number_Load(object sender, EventArgs e)
        {
            //加载配置文件

            numericUpDown_output.Value = ProductConfig.Instance.ProductOkTotal;
            numericUpDown_NG.Value = ProductConfig.Instance.ProductNgTotal;

        }
    }
}
