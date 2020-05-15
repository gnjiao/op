using System.Windows.Forms;
namespace System.Enginee
{
    public partial class VacuoParameter : UserControl
    {
        private VacuoDelay m_vacuoDelay;
        public VacuoParameter()
        {
            InitializeComponent();
        }
        public VacuoParameter(VacuoDelay vacuoDelay) : this()
        {
            m_vacuoDelay = vacuoDelay;
            txtInhaleDelay.Text = (m_vacuoDelay.InhaleTime / 1000.00).ToString("0.00");
            txtBrokenDelay.Text = (m_vacuoDelay.BrokenTime / 1000.00).ToString("0.00");
            txtAlarmDelay.Text = (m_vacuoDelay.AlarmTime / 1000.00).ToString("0.00");
        }
#pragma warning disable CS0108 // '“VacuoParameter.Name”隐藏继承的成员“Control.Name”。如果是有意隐藏，请使用关键字 new。
        public string Name
#pragma warning restore CS0108 // '“VacuoParameter.Name”隐藏继承的成员“Control.Name”。如果是有意隐藏，请使用关键字 new。
        {
            set
            {
                gbxName.Text = value;
            }
        }
        public VacuoDelay Save
        {
            get
            {
                m_vacuoDelay.InhaleTime = (int)(double.Parse(txtInhaleDelay.Text) * 1000);
                m_vacuoDelay.BrokenTime = (int)(double.Parse(txtBrokenDelay.Text) * 1000);
                m_vacuoDelay.AlarmTime = (int)(double.Parse(txtAlarmDelay.Text) * 1000);
                return m_vacuoDelay;
            }
        }
    }
}
