namespace desaySV
{  

    partial class frmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.iO控制IToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.示教ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.参数设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开程序所在位置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.串口设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.生成报告设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tlUseLoad = new System.Windows.Forms.ToolStripButton();
            this.tlRun = new System.Windows.Forms.ToolStripButton();
            this.toolCleanData = new System.Windows.Forms.ToolStripButton();
            this.tlScanChange = new System.Windows.Forms.ToolStripButton();
            this.tlReset = new System.Windows.Forms.ToolStripButton();
            this.tlExit = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lbSN = new System.Windows.Forms.Label();
            this.lbRate = new System.Windows.Forms.Label();
            this.lbOutputFail = new System.Windows.Forms.Label();
            this.lbOutputPass = new System.Windows.Forms.Label();
            this.lbTarget = new System.Windows.Forms.Label();
            this.lblTestStep = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lbRunTime = new System.Windows.Forms.Label();
            this.lb = new System.Windows.Forms.Label();
            this.lbA2C = new System.Windows.Forms.Label();
            this.lbModel = new System.Windows.Forms.Label();
            this.lbCustomer = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbInterlockingModel = new System.Windows.Forms.Label();
            this.lbJobnumber = new System.Windows.Forms.Label();
            this.lbStationName = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabDebugger = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.rtxtLog = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.label12 = new System.Windows.Forms.Label();
            this.机器人测试ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabDebugger.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iO控制IToolStripMenuItem,
            this.示教ToolStripMenuItem,
            this.参数设置ToolStripMenuItem,
            this.打开程序所在位置ToolStripMenuItem,
            this.串口设置ToolStripMenuItem,
            this.生成报告设置ToolStripMenuItem,
            this.机器人测试ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 180);
            // 
            // iO控制IToolStripMenuItem
            // 
            this.iO控制IToolStripMenuItem.Name = "iO控制IToolStripMenuItem";
            this.iO控制IToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.iO控制IToolStripMenuItem.Text = "IO控制(&I)";
            this.iO控制IToolStripMenuItem.Click += new System.EventHandler(this.iO控制IToolStripMenuItem_Click_1);
            // 
            // 示教ToolStripMenuItem
            // 
            this.示教ToolStripMenuItem.Name = "示教ToolStripMenuItem";
            this.示教ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.示教ToolStripMenuItem.Text = "示教";
            this.示教ToolStripMenuItem.Click += new System.EventHandler(this.图像学习ToolStripMenuItem_Click);
            // 
            // 参数设置ToolStripMenuItem
            // 
            this.参数设置ToolStripMenuItem.Name = "参数设置ToolStripMenuItem";
            this.参数设置ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.参数设置ToolStripMenuItem.Text = "参数设置";
            this.参数设置ToolStripMenuItem.Click += new System.EventHandler(this.工位操作ToolStripMenuItem_Click_1);
            // 
            // 打开程序所在位置ToolStripMenuItem
            // 
            this.打开程序所在位置ToolStripMenuItem.Name = "打开程序所在位置ToolStripMenuItem";
            this.打开程序所在位置ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.打开程序所在位置ToolStripMenuItem.Text = "打开程序所在位置";
            this.打开程序所在位置ToolStripMenuItem.Click += new System.EventHandler(this.打开程序所在位置ToolStripMenuItem_Click);
            // 
            // 串口设置ToolStripMenuItem
            // 
            this.串口设置ToolStripMenuItem.Name = "串口设置ToolStripMenuItem";
            this.串口设置ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.串口设置ToolStripMenuItem.Text = "串口设置";
            this.串口设置ToolStripMenuItem.Click += new System.EventHandler(this.串口设置ToolStripMenuItem_Click);
            // 
            // 生成报告设置ToolStripMenuItem
            // 
            this.生成报告设置ToolStripMenuItem.Name = "生成报告设置ToolStripMenuItem";
            this.生成报告设置ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.生成报告设置ToolStripMenuItem.Text = "生成报告设置";
            this.生成报告设置ToolStripMenuItem.Click += new System.EventHandler(this.生成报告设置ToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AllowItemReorder = true;
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlUseLoad,
            this.tlRun,
            this.toolCleanData,
            this.tlScanChange,
            this.tlReset,
            this.tlExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(839, 68);
            this.toolStrip1.TabIndex = 89;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolStrip1_MouseDown_1);
            // 
            // tlUseLoad
            // 
            this.tlUseLoad.AutoSize = false;
            this.tlUseLoad.Image = global::desaySV.Properties.Resources.UserLogin;
            this.tlUseLoad.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tlUseLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlUseLoad.Margin = new System.Windows.Forms.Padding(0, 1, 10, 1);
            this.tlUseLoad.Name = "tlUseLoad";
            this.tlUseLoad.Size = new System.Drawing.Size(60, 65);
            this.tlUseLoad.Text = "登录";
            this.tlUseLoad.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlUseLoad.ToolTipText = "登录";
            this.tlUseLoad.Click += new System.EventHandler(this.tlUseLoad_Click);
            // 
            // tlRun
            // 
            this.tlRun.AutoSize = false;
            this.tlRun.Image = global::desaySV.Properties.Resources.Run;
            this.tlRun.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tlRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlRun.Margin = new System.Windows.Forms.Padding(0, 1, 10, 2);
            this.tlRun.Name = "tlRun";
            this.tlRun.Size = new System.Drawing.Size(60, 65);
            this.tlRun.Text = "运行";
            this.tlRun.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlRun.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tlRun_MouseDown);
            this.tlRun.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tlRun_MouseUp);
            // 
            // toolCleanData
            // 
            this.toolCleanData.AutoSize = false;
            this.toolCleanData.Image = global::desaySV.Properties.Resources.Modify;
            this.toolCleanData.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolCleanData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolCleanData.Margin = new System.Windows.Forms.Padding(0, 1, 10, 2);
            this.toolCleanData.Name = "toolCleanData";
            this.toolCleanData.Size = new System.Drawing.Size(60, 65);
            this.toolCleanData.Text = "重置数量";
            this.toolCleanData.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolCleanData.ToolTipText = "重置数量";
            this.toolCleanData.Click += new System.EventHandler(this.toolCleanData_Click);
            // 
            // tlScanChange
            // 
            this.tlScanChange.AutoSize = false;
            this.tlScanChange.Image = global::desaySV.Properties.Resources.Scan;
            this.tlScanChange.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tlScanChange.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlScanChange.Margin = new System.Windows.Forms.Padding(0, 1, 10, 2);
            this.tlScanChange.Name = "tlScanChange";
            this.tlScanChange.Size = new System.Drawing.Size(60, 65);
            this.tlScanChange.Text = "扫描换型";
            this.tlScanChange.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlScanChange.ToolTipText = "扫描换型";
            this.tlScanChange.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tlReset
            // 
            this.tlReset.AutoSize = false;
            this.tlReset.Image = global::desaySV.Properties.Resources.Home;
            this.tlReset.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tlReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlReset.Margin = new System.Windows.Forms.Padding(0, 1, 10, 2);
            this.tlReset.Name = "tlReset";
            this.tlReset.Size = new System.Drawing.Size(60, 65);
            this.tlReset.Text = "复位";
            this.tlReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlReset.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnReset_MouseDown);
            this.tlReset.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnReset_MouseUp);
            // 
            // tlExit
            // 
            this.tlExit.AutoSize = false;
            this.tlExit.Image = global::desaySV.Properties.Resources.Close;
            this.tlExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tlExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlExit.Margin = new System.Windows.Forms.Padding(0, 1, 10, 2);
            this.tlExit.Name = "tlExit";
            this.tlExit.Size = new System.Drawing.Size(60, 65);
            this.tlExit.Text = "退出";
            this.tlExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tlExit.Click += new System.EventHandler(this.tooStripExit_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 290F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.statusStrip1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 68);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(839, 674);
            this.tableLayoutPanel1.TabIndex = 90;
            // 
            // statusStrip1
            // 
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 654);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(290, 20);
            this.statusStrip1.TabIndex = 150;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.Color.Yellow;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(68, 15);
            this.toolStripStatusLabel1.Text = "机器人连接";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BackColor = System.Drawing.Color.Cyan;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(53, 15);
            this.toolStripStatusLabel2.Text = "PLC连接";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(68, 15);
            this.toolStripStatusLabel3.Text = "传感器连接";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.lblResult);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(284, 648);
            this.panel1.TabIndex = 88;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(211)))), ((int)(((byte)(241)))));
            this.groupBox4.Controls.Add(this.lbSN);
            this.groupBox4.Controls.Add(this.lbRate);
            this.groupBox4.Controls.Add(this.lbOutputFail);
            this.groupBox4.Controls.Add(this.lbOutputPass);
            this.groupBox4.Controls.Add(this.lbTarget);
            this.groupBox4.Controls.Add(this.lblTestStep);
            this.groupBox4.Controls.Add(this.label25);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.ForeColor = System.Drawing.Color.Black;
            this.groupBox4.Location = new System.Drawing.Point(3, 319);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(271, 280);
            this.groupBox4.TabIndex = 79;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "生产信息";
            // 
            // lbSN
            // 
            this.lbSN.BackColor = System.Drawing.Color.White;
            this.lbSN.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbSN.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbSN.ForeColor = System.Drawing.Color.Black;
            this.lbSN.Location = new System.Drawing.Point(106, 157);
            this.lbSN.Name = "lbSN";
            this.lbSN.Size = new System.Drawing.Size(155, 50);
            this.lbSN.TabIndex = 3;
            this.lbSN.Text = "*************************";
            // 
            // lbRate
            // 
            this.lbRate.BackColor = System.Drawing.Color.White;
            this.lbRate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbRate.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbRate.ForeColor = System.Drawing.Color.Black;
            this.lbRate.Location = new System.Drawing.Point(107, 120);
            this.lbRate.Name = "lbRate";
            this.lbRate.Size = new System.Drawing.Size(156, 26);
            this.lbRate.TabIndex = 3;
            // 
            // lbOutputFail
            // 
            this.lbOutputFail.BackColor = System.Drawing.Color.White;
            this.lbOutputFail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbOutputFail.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbOutputFail.ForeColor = System.Drawing.Color.Black;
            this.lbOutputFail.Location = new System.Drawing.Point(107, 87);
            this.lbOutputFail.Name = "lbOutputFail";
            this.lbOutputFail.Size = new System.Drawing.Size(156, 26);
            this.lbOutputFail.TabIndex = 3;
            // 
            // lbOutputPass
            // 
            this.lbOutputPass.BackColor = System.Drawing.Color.White;
            this.lbOutputPass.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbOutputPass.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbOutputPass.ForeColor = System.Drawing.Color.Black;
            this.lbOutputPass.Location = new System.Drawing.Point(107, 54);
            this.lbOutputPass.Name = "lbOutputPass";
            this.lbOutputPass.Size = new System.Drawing.Size(156, 26);
            this.lbOutputPass.TabIndex = 3;
            // 
            // lbTarget
            // 
            this.lbTarget.BackColor = System.Drawing.Color.White;
            this.lbTarget.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbTarget.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbTarget.ForeColor = System.Drawing.Color.Black;
            this.lbTarget.Location = new System.Drawing.Point(107, 20);
            this.lbTarget.Name = "lbTarget";
            this.lbTarget.Size = new System.Drawing.Size(156, 26);
            this.lbTarget.TabIndex = 3;
            // 
            // lblTestStep
            // 
            this.lblTestStep.BackColor = System.Drawing.Color.Lime;
            this.lblTestStep.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTestStep.Font = new System.Drawing.Font("微软雅黑", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTestStep.ForeColor = System.Drawing.Color.Black;
            this.lblTestStep.Location = new System.Drawing.Point(3, 226);
            this.lblTestStep.Name = "lblTestStep";
            this.lblTestStep.Size = new System.Drawing.Size(265, 51);
            this.lblTestStep.TabIndex = 61;
            this.lblTestStep.Text = "Loading";
            this.lblTestStep.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label25.Location = new System.Drawing.Point(6, 174);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(84, 21);
            this.label25.TabIndex = 0;
            this.label25.Text = "产  品  SN";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(7, 122);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(78, 21);
            this.label13.TabIndex = 0;
            this.label13.Text = "直  通  率";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(7, 89);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(78, 21);
            this.label14.TabIndex = 0;
            this.label14.Text = "不  合  格";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(6, 57);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(82, 21);
            this.label15.TabIndex = 0;
            this.label15.Text = "产        出";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.Location = new System.Drawing.Point(7, 24);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(82, 21);
            this.label16.TabIndex = 0;
            this.label16.Text = "目        标";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(211)))), ((int)(((byte)(241)))));
            this.groupBox3.Controls.Add(this.lbRunTime);
            this.groupBox3.Controls.Add(this.lb);
            this.groupBox3.Controls.Add(this.lbA2C);
            this.groupBox3.Controls.Add(this.lbModel);
            this.groupBox3.Controls.Add(this.lbCustomer);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.label23);
            this.groupBox3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.ForeColor = System.Drawing.Color.Black;
            this.groupBox3.Location = new System.Drawing.Point(3, 127);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(271, 190);
            this.groupBox3.TabIndex = 78;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "项目信息";
            // 
            // lbRunTime
            // 
            this.lbRunTime.BackColor = System.Drawing.Color.White;
            this.lbRunTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbRunTime.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbRunTime.ForeColor = System.Drawing.Color.Black;
            this.lbRunTime.Location = new System.Drawing.Point(107, 157);
            this.lbRunTime.Name = "lbRunTime";
            this.lbRunTime.Size = new System.Drawing.Size(156, 26);
            this.lbRunTime.TabIndex = 3;
            // 
            // lb
            // 
            this.lb.BackColor = System.Drawing.Color.White;
            this.lb.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb.ForeColor = System.Drawing.Color.Black;
            this.lb.Location = new System.Drawing.Point(107, 124);
            this.lb.Name = "lb";
            this.lb.Size = new System.Drawing.Size(156, 26);
            this.lb.TabIndex = 3;
            // 
            // lbA2C
            // 
            this.lbA2C.BackColor = System.Drawing.Color.White;
            this.lbA2C.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbA2C.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbA2C.ForeColor = System.Drawing.Color.Black;
            this.lbA2C.Location = new System.Drawing.Point(107, 90);
            this.lbA2C.Name = "lbA2C";
            this.lbA2C.Size = new System.Drawing.Size(156, 26);
            this.lbA2C.TabIndex = 3;
            // 
            // lbModel
            // 
            this.lbModel.BackColor = System.Drawing.Color.White;
            this.lbModel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbModel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbModel.ForeColor = System.Drawing.Color.Black;
            this.lbModel.Location = new System.Drawing.Point(107, 56);
            this.lbModel.Name = "lbModel";
            this.lbModel.Size = new System.Drawing.Size(156, 26);
            this.lbModel.TabIndex = 3;
            // 
            // lbCustomer
            // 
            this.lbCustomer.BackColor = System.Drawing.Color.White;
            this.lbCustomer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbCustomer.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbCustomer.ForeColor = System.Drawing.Color.Black;
            this.lbCustomer.Location = new System.Drawing.Point(107, 20);
            this.lbCustomer.Name = "lbCustomer";
            this.lbCustomer.Size = new System.Drawing.Size(156, 26);
            this.lbCustomer.TabIndex = 3;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.Location = new System.Drawing.Point(6, 161);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(84, 21);
            this.label19.TabIndex = 2;
            this.label19.Text = "运 行时 间";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label20.Location = new System.Drawing.Point(6, 128);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(84, 21);
            this.label20.TabIndex = 2;
            this.label20.Text = "项 目阶 段";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label21.Location = new System.Drawing.Point(6, 93);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(83, 21);
            this.label21.TabIndex = 2;
            this.label21.Text = "产  品   号";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label22.Location = new System.Drawing.Point(6, 56);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(87, 21);
            this.label22.TabIndex = 2;
            this.label22.Text = "机         型";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label23.Location = new System.Drawing.Point(6, 21);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(87, 21);
            this.label23.TabIndex = 2;
            this.label23.Text = "客         户";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(211)))), ((int)(((byte)(241)))));
            this.groupBox2.Controls.Add(this.lbInterlockingModel);
            this.groupBox2.Controls.Add(this.lbJobnumber);
            this.groupBox2.Controls.Add(this.lbStationName);
            this.groupBox2.Controls.Add(this.label38);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.ForeColor = System.Drawing.Color.Black;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(272, 121);
            this.groupBox2.TabIndex = 77;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "工位信息";
            // 
            // lbInterlockingModel
            // 
            this.lbInterlockingModel.BackColor = System.Drawing.Color.White;
            this.lbInterlockingModel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbInterlockingModel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbInterlockingModel.ForeColor = System.Drawing.Color.Black;
            this.lbInterlockingModel.Location = new System.Drawing.Point(108, 89);
            this.lbInterlockingModel.Name = "lbInterlockingModel";
            this.lbInterlockingModel.Size = new System.Drawing.Size(156, 26);
            this.lbInterlockingModel.TabIndex = 3;
            // 
            // lbJobnumber
            // 
            this.lbJobnumber.BackColor = System.Drawing.Color.White;
            this.lbJobnumber.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbJobnumber.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbJobnumber.ForeColor = System.Drawing.Color.Black;
            this.lbJobnumber.Location = new System.Drawing.Point(108, 55);
            this.lbJobnumber.Name = "lbJobnumber";
            this.lbJobnumber.Size = new System.Drawing.Size(156, 26);
            this.lbJobnumber.TabIndex = 3;
            // 
            // lbStationName
            // 
            this.lbStationName.BackColor = System.Drawing.Color.White;
            this.lbStationName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbStationName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbStationName.ForeColor = System.Drawing.Color.Black;
            this.lbStationName.Location = new System.Drawing.Point(108, 23);
            this.lbStationName.Name = "lbStationName";
            this.lbStationName.Size = new System.Drawing.Size(156, 26);
            this.lbStationName.TabIndex = 3;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label38.Location = new System.Drawing.Point(9, 89);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(84, 21);
            this.label38.TabIndex = 2;
            this.label38.Text = "互 锁模 式";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.Location = new System.Drawing.Point(7, 55);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(92, 21);
            this.label17.TabIndex = 2;
            this.label17.Text = "工          号";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.Location = new System.Drawing.Point(8, 22);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(86, 21);
            this.label18.TabIndex = 2;
            this.label18.Text = "Station ID";
            // 
            // lblResult
            // 
            this.lblResult.BackColor = System.Drawing.Color.Red;
            this.lblResult.Font = new System.Drawing.Font("微软雅黑", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblResult.ForeColor = System.Drawing.Color.Black;
            this.lblResult.Location = new System.Drawing.Point(10, 603);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(257, 59);
            this.lblResult.TabIndex = 72;
            this.lblResult.Text = "Failled";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.tabDebugger);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(293, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(543, 648);
            this.panel2.TabIndex = 89;
            // 
            // tabDebugger
            // 
            this.tabDebugger.Controls.Add(this.tabPage3);
            this.tabDebugger.Controls.Add(this.tabPage2);
            this.tabDebugger.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabDebugger.Location = new System.Drawing.Point(0, 443);
            this.tabDebugger.Name = "tabDebugger";
            this.tabDebugger.SelectedIndex = 0;
            this.tabDebugger.Size = new System.Drawing.Size(543, 205);
            this.tabDebugger.TabIndex = 71;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.listBox1);
            this.tabPage3.Controls.Add(this.rtxtLog);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(535, 179);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "消息日志";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 14;
            this.listBox1.Location = new System.Drawing.Point(3, 3);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.Size = new System.Drawing.Size(529, 173);
            this.listBox1.TabIndex = 72;
            // 
            // rtxtLog
            // 
            this.rtxtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtLog.Location = new System.Drawing.Point(3, 3);
            this.rtxtLog.Name = "rtxtLog";
            this.rtxtLog.ReadOnly = true;
            this.rtxtLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtxtLog.Size = new System.Drawing.Size(529, 173);
            this.rtxtLog.TabIndex = 71;
            this.rtxtLog.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(535, 179);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "报警日志";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listBox2
            // 
            this.listBox2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.listBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox2.FormattingEnabled = true;
            this.listBox2.HorizontalScrollbar = true;
            this.listBox2.ItemHeight = 14;
            this.listBox2.Location = new System.Drawing.Point(3, 3);
            this.listBox2.Name = "listBox2";
            this.listBox2.ScrollAlwaysVisible = true;
            this.listBox2.Size = new System.Drawing.Size(529, 173);
            this.listBox2.TabIndex = 73;
            // 
            // label12
            // 
            this.label12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(211)))), ((int)(((byte)(241)))));
            this.label12.Image = global::desaySV.Properties.Resources.Desay_SV_Logo;
            this.label12.Location = new System.Drawing.Point(639, 7);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(192, 55);
            this.label12.TabIndex = 147;
            // 
            // 机器人测试ToolStripMenuItem
            // 
            this.机器人测试ToolStripMenuItem.Name = "机器人测试ToolStripMenuItem";
            this.机器人测试ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.机器人测试ToolStripMenuItem.Text = "机器人测试";
            this.机器人测试ToolStripMenuItem.Click += new System.EventHandler(this.机器人测试ToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(211)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(839, 742);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "西威标准软件 版本号：";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frmMain_MouseClick);
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tabDebugger.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem iO控制IToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 示教ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 参数设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开程序所在位置ToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabDebugger;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListBox listBox1;
        public System.Windows.Forms.RichTextBox rtxtLog;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripButton tlUseLoad;
        private System.Windows.Forms.ToolStripButton tlRun;
        private System.Windows.Forms.ToolStripButton toolCleanData;
        private System.Windows.Forms.ToolStripButton tlScanChange;
        private System.Windows.Forms.ToolStripButton tlReset;
        private System.Windows.Forms.ToolStripButton tlExit;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ToolStripMenuItem 串口设置ToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lbSN;
        private System.Windows.Forms.Label lbRate;
        private System.Windows.Forms.Label lbOutputFail;
        private System.Windows.Forms.Label lbOutputPass;
        private System.Windows.Forms.Label lbTarget;
        private System.Windows.Forms.Label lblTestStep;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lbRunTime;
        private System.Windows.Forms.Label lb;
        private System.Windows.Forms.Label lbA2C;
        private System.Windows.Forms.Label lbModel;
        private System.Windows.Forms.Label lbCustomer;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbInterlockingModel;
        private System.Windows.Forms.Label lbJobnumber;
        private System.Windows.Forms.Label lbStationName;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripMenuItem 生成报告设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 机器人测试ToolStripMenuItem;
    }
}

