using System.Windows.Forms;
using System.ToolKit;
using CMotion.Interfaces.Configuration;
namespace System.Enginee
{
    public partial class Point3DView : UserControl
    {
        private readonly TransmissionParams[] m_Axis = new TransmissionParams[3];
        private Point3D<double> point = new Point3D<double>();
        public Point3DView()
        {
            InitializeComponent();
        }

        public Point3DView(TransmissionParams[] axis) : this()
        {
            m_Axis = axis;
        }
        public Point3D<double> Point
        {
            set
            {
                point = value;
            }
        }
#pragma warning disable CS0114 // '“Point3DView.Refresh()”隐藏继承的成员“Control.Refresh()”。若要使当前成员重写该实现，请添加关键字 override。否则，添加关键字 new。
        public void Refresh()
#pragma warning restore CS0114 // '“Point3DView.Refresh()”隐藏继承的成员“Control.Refresh()”。若要使当前成员重写该实现，请添加关键字 override。否则，添加关键字 new。
        {
            lblGetProductX.Text = (point.X * m_Axis[0].PulseEquivalent).ToString("0.000");
            lblGetProductY.Text = (point.Y * m_Axis[1].PulseEquivalent).ToString("0.000");
            lblGetProductZ.Text = (point.Z * m_Axis[2].PulseEquivalent).ToString("0.000");
        }
    }
}
