using desaySV.View;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace desaySV
{
    public partial class frmTeach : Form
    {
        private PositionName name = 0;

        private Station1 m_station1;


        #region 控件

        private Panel m_panelOperate;
        ProductTeach ProductTeach;
        CameraAxis mCameraAxis;
        CameraRelationship CameraRelationship;
        RootAxis RootAxis;


        Yaxis mYaxis;
        Zaxis mZaxis;
        #endregion

        public frmTeach()
        {
            InitializeComponent();
        }

        public frmTeach(Station1 station) : this()
        {
            m_station1 = station;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Text == "贴框设备")
            {
                return;
            }

            foreach (TreeNode node in treeView1.Nodes[0].Nodes)
            {
                if (node.Text == e.Node.Text)
                {
                    node.ForeColor = Color.White;
                    node.BackColor = Color.DodgerBlue;
                }
                else
                {
                    node.ForeColor = Color.Black;
                    node.BackColor = Color.Transparent;
                }
            }

            var nodetext = (PositionName)Enum.Parse(typeof(PositionName), e.Node.Text);
            panelVeiw.Controls.Clear();
            m_panelOperate.Controls.Clear();
            switch (nodetext)
            {

                case PositionName.Y轴位置设定:
                    m_panelOperate.Controls.Add(mYaxis);
                    panelVeiw.Controls.Add(m_panelOperate, 0, 0);
                    break;
                case PositionName.Z轴位置设定:
                    m_panelOperate.Controls.Add(mZaxis);
                    panelVeiw.Controls.Add(m_panelOperate, 0, 0);
                    break;
                case PositionName.产品标定:
                    m_panelOperate.Controls.Add(ProductTeach);
                    panelVeiw.Controls.Add(m_panelOperate, 0, 0);
                    break;

                case PositionName.机器人位置设定:
                    m_panelOperate.Controls.Add(RootAxis);
                    panelVeiw.Controls.Add(m_panelOperate, 0, 0);
                    break;
                case PositionName.相机校正:
                    m_panelOperate.Controls.Add(CameraRelationship);
                    panelVeiw.Controls.Add(m_panelOperate, 0, 0);
                    break;
                case PositionName.相机拍照位置:
                    m_panelOperate.Controls.Add(mCameraAxis);
                    panelVeiw.Controls.Add(m_panelOperate, 0, 0);
                    break;
                default:
                    //panelVeiw.Controls.Add(defaultView);
                    break;
            }
            name = nodetext;
        }

        private void frmTeach_Load(object sender, EventArgs e)
        {
            //defaultView = new PositionDefaultView();

            ProductTeach = new ProductTeach(m_station1);
            mCameraAxis = new CameraAxis(m_station1);
            CameraRelationship = new CameraRelationship(m_station1);
            RootAxis = new RootAxis(m_station1);

            mYaxis = new Yaxis(m_station1);
            mZaxis = new Zaxis(m_station1);
            m_panelOperate = new Panel();
            m_panelOperate.Dock = DockStyle.Fill;

            treeView1.Nodes.Add("贴框设备");
            foreach (string str in Enum.GetNames(typeof(PositionName)))
            {
                treeView1.Nodes[0].Nodes.Add(str);
            }

            treeView1.ExpandAll();
            timer1.Enabled = true;
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            switch (name)
            {
                case PositionName.Y轴位置设定:
                    mYaxis.Refreshing();
                    break;
                case PositionName.Z轴位置设定:
                    mZaxis.Refreshing();
                    break;
                case PositionName.产品标定:
                    ProductTeach.Refreshing();
                    break;
                case PositionName.机器人位置设定:
                    RootAxis.Refreshing();
                    break;
                case PositionName.相机校正:
                    CameraRelationship.Refreshing();
                    break;
                case PositionName.相机拍照位置:
                    mCameraAxis.Refreshing();
                    break;
                default:
                    break;
            }
            timer1.Enabled = true;
        }


        public enum PositionName
        {
            机器人位置设定,
            Y轴位置设定,
            Z轴位置设定,
            相机校正,
            产品标定,
            相机拍照位置
        }
    }
}
