using System;
using System.Windows.Forms;

namespace desaySV
{
    public partial class FrmTest : Form
    {
        private Station1 m_station;

        public FrmTest(Station1 station1)
        {
            InitializeComponent();
            m_station = station1;
        }

        private void FrmTest_Load(object sender, EventArgs e)
        {
            modelOperate1.StationIni = m_station.stationInitialize;
            modelOperate1.StationOpe = m_station.stationOperate;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            modelOperate1.Refreshing();

        }
    }
}
