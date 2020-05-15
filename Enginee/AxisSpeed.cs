using CMotion.Interfaces.Configuration;
using System.Windows.Forms;

namespace System.Enginee
{
    public partial class AxisSpeed : UserControl
    {
        private TransmissionParams m_Transmission;
        private double m_velocityMax;

        public AxisSpeed()
        {
            InitializeComponent();
        }
        public AxisSpeed(TransmissionParams transmission, double velocityMax) : this()
        {
            m_Transmission = transmission;
            m_velocityMax = velocityMax;
        }
#pragma warning disable CS0108 // '“AxisSpeed.Name”隐藏继承的成员“Control.Name”。如果是有意隐藏，请使用关键字 new。
        public string Name { get; set; }
#pragma warning restore CS0108 // '“AxisSpeed.Name”隐藏继承的成员“Control.Name”。如果是有意隐藏，请使用关键字 new。
        public int SpeedRate
        {
            get
            {
                return tkrSpeedRate.Value;
            }
            set
            {
                tkrSpeedRate.Value = value;
                lblAxisSpeedRate.Text = Name + "速度(" + value + "%)";
                lblAxisSpeed.Text = (((m_velocityMax * value) / 100)
                    * m_Transmission.PulseEquivalent).ToString("0.00") + "mm/s";
            }
        }
        private void tkrSpeedRate_Scroll(object sender, EventArgs e)
        {
            lblAxisSpeedRate.Text = Name + "速度(" + tkrSpeedRate.Value + "%)";
            lblAxisSpeed.Text = (((m_velocityMax * tkrSpeedRate.Value) / 100)
                * m_Transmission.PulseEquivalent).ToString("0.00") + "mm/s";
        }
    }
}
