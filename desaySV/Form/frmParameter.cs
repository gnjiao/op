using System;
using System.Threading;
using System.Windows.Forms;
using System.Enginee;
namespace desaySV
{
    public partial class frmParameter : Form
    {
#pragma warning disable CS0169 // 从不使用字段“frmParameter.BlackboardParameter”
#pragma warning disable CS0169 // 从不使用字段“frmParameter.Station4RiseParameter”
#pragma warning disable CS0169 // 从不使用字段“frmParameter.WriteBoardParameter”
#pragma warning disable CS0169 // 从不使用字段“frmParameter.Station2RiseParameter”
#pragma warning disable CS0169 // 从不使用字段“frmParameter.Station3RiseParameter”
        private CylinderParameter Station2RiseParameter, Station3RiseParameter, Station4RiseParameter, WriteBoardParameter, BlackboardParameter;
#pragma warning restore CS0169 // 从不使用字段“frmParameter.Station3RiseParameter”
#pragma warning restore CS0169 // 从不使用字段“frmParameter.Station2RiseParameter”
#pragma warning restore CS0169 // 从不使用字段“frmParameter.WriteBoardParameter”
#pragma warning restore CS0169 // 从不使用字段“frmParameter.Station4RiseParameter”
#pragma warning restore CS0169 // 从不使用字段“frmParameter.BlackboardParameter”
#pragma warning disable CS0169 // 从不使用字段“frmParameter.XaxisSpeedView”
        private AxisSpeed XaxisSpeedView;
#pragma warning restore CS0169 // 从不使用字段“frmParameter.XaxisSpeedView”
#pragma warning disable CS0169 // 从不使用字段“frmParameter.YaxisSpeedView”
        private AxisSpeed YaxisSpeedView;
#pragma warning restore CS0169 // 从不使用字段“frmParameter.YaxisSpeedView”

        private void button1_Click(object sender, EventArgs e)
        {
            frmTrayCalib trayCalib = new frmTrayCalib("1", m_station1.XaxisServo, m_station1.YaxisServo, m_station1.ZaxisServo, () => { return m_station1.stationInitialize.InitializeDone; });
             trayCalib.Text = "小托盘标定";
            trayCalib.ShowDialog();
        }

#pragma warning disable CS0169 // 从不使用字段“frmParameter.ZaxisSpeedView”
        private AxisSpeed ZaxisSpeedView;
#pragma warning restore CS0169 // 从不使用字段“frmParameter.ZaxisSpeedView”
        Station1 m_station1;
        public frmParameter(Station1 Station1)
        {
            InitializeComponent();
            m_station1 = Station1;
        }
     

     

      

        private void frmParameter_Load(object sender, EventArgs e)
        {

           
          
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
           
        }
    }
}
