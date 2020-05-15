using System.Windows.Forms;
namespace System.Enginee
{
    public partial class CylinderParameter : UserControl
    {
        private CylinderDelay m_cylinderDelay;
        public CylinderParameter()
        {
            InitializeComponent();
        }
        public CylinderParameter(CylinderDelay cylinderDelay):this()
        {
            m_cylinderDelay = cylinderDelay;
            txtOriginDelay.Text= (m_cylinderDelay.OriginTime / 1000.00).ToString("0.00");
            txtMoveDelay.Text = (m_cylinderDelay.MoveTime / 1000.00).ToString("0.00");
            txtAlarmDelay.Text= (m_cylinderDelay.AlarmTime / 1000.00).ToString("0.00");
        }
#pragma warning disable CS0108 // '“CylinderParameter.Name”隐藏继承的成员“Control.Name”。如果是有意隐藏，请使用关键字 new。
        public string Name
#pragma warning restore CS0108 // '“CylinderParameter.Name”隐藏继承的成员“Control.Name”。如果是有意隐藏，请使用关键字 new。
        {
            set
            {
                gbxName.Text = value;
            }
        }
        public CylinderDelay Save
        {
            get
            {
                m_cylinderDelay.OriginTime = (int)(double.Parse(txtOriginDelay.Text) * 1000);
                m_cylinderDelay.MoveTime = (int)(double.Parse(txtMoveDelay.Text) * 1000);
                m_cylinderDelay.AlarmTime = (int)(double.Parse(txtAlarmDelay.Text) * 1000);
                return m_cylinderDelay;
            }
        }
    }
}
