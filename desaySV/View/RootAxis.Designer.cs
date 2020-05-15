namespace desaySV
{
    partial class RootAxis
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblRootSturts = new System.Windows.Forms.Label();
            this.lblCurrentPositionR = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblCurrentPositionZ = new System.Windows.Forms.Label();
            this.lblCurrentPositionX = new System.Windows.Forms.Label();
            this.lblCurrentPositionY = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnZadd = new System.Windows.Forms.Button();
            this.btnRadd = new System.Windows.Forms.Button();
            this.btnRdec = new System.Windows.Forms.Button();
            this.tbrJogSpeed = new System.Windows.Forms.TrackBar();
            this.btnZdec = new System.Windows.Forms.Button();
            this.btnYdec = new System.Windows.Forms.Button();
            this.btnXdec = new System.Windows.Forms.Button();
            this.btnYadd = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnXadd = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Z = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.定位 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.保存 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.moveSelectHorizontal1 = new System.Enginee.MoveSelectHorizontal();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbrJogSpeed)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 17);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(387, 154);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "显示";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lblRootSturts, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblCurrentPositionR, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblCurrentPositionZ, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lblCurrentPositionX, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblCurrentPositionY, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label11, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(381, 134);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // lblRootSturts
            // 
            this.lblRootSturts.AutoSize = true;
            this.lblRootSturts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRootSturts.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRootSturts.Location = new System.Drawing.Point(4, 1);
            this.lblRootSturts.Name = "lblRootSturts";
            this.lblRootSturts.Size = new System.Drawing.Size(183, 25);
            this.lblRootSturts.TabIndex = 10;
            this.lblRootSturts.Text = "机器人状态";
            this.lblRootSturts.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrentPositionR
            // 
            this.lblCurrentPositionR.AutoSize = true;
            this.lblCurrentPositionR.BackColor = System.Drawing.Color.Black;
            this.lblCurrentPositionR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentPositionR.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCurrentPositionR.ForeColor = System.Drawing.Color.SpringGreen;
            this.lblCurrentPositionR.Location = new System.Drawing.Point(194, 105);
            this.lblCurrentPositionR.Name = "lblCurrentPositionR";
            this.lblCurrentPositionR.Size = new System.Drawing.Size(183, 28);
            this.lblCurrentPositionR.TabIndex = 9;
            this.lblCurrentPositionR.Text = "0000.000";
            this.lblCurrentPositionR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(4, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(183, 28);
            this.label5.TabIndex = 8;
            this.label5.Text = "R轴";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(4, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "X轴";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(4, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(183, 25);
            this.label3.TabIndex = 1;
            this.label3.Text = "Y轴";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(4, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(183, 25);
            this.label4.TabIndex = 1;
            this.label4.Text = "Z轴";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrentPositionZ
            // 
            this.lblCurrentPositionZ.AutoSize = true;
            this.lblCurrentPositionZ.BackColor = System.Drawing.Color.Black;
            this.lblCurrentPositionZ.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentPositionZ.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCurrentPositionZ.ForeColor = System.Drawing.Color.SpringGreen;
            this.lblCurrentPositionZ.Location = new System.Drawing.Point(194, 79);
            this.lblCurrentPositionZ.Name = "lblCurrentPositionZ";
            this.lblCurrentPositionZ.Size = new System.Drawing.Size(183, 25);
            this.lblCurrentPositionZ.TabIndex = 1;
            this.lblCurrentPositionZ.Text = "0000.000";
            this.lblCurrentPositionZ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrentPositionX
            // 
            this.lblCurrentPositionX.AutoSize = true;
            this.lblCurrentPositionX.BackColor = System.Drawing.Color.Black;
            this.lblCurrentPositionX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentPositionX.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCurrentPositionX.ForeColor = System.Drawing.Color.SpringGreen;
            this.lblCurrentPositionX.Location = new System.Drawing.Point(194, 27);
            this.lblCurrentPositionX.Name = "lblCurrentPositionX";
            this.lblCurrentPositionX.Size = new System.Drawing.Size(183, 25);
            this.lblCurrentPositionX.TabIndex = 2;
            this.lblCurrentPositionX.Text = "0000.000";
            this.lblCurrentPositionX.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrentPositionY
            // 
            this.lblCurrentPositionY.AutoSize = true;
            this.lblCurrentPositionY.BackColor = System.Drawing.Color.Black;
            this.lblCurrentPositionY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentPositionY.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCurrentPositionY.ForeColor = System.Drawing.Color.SpringGreen;
            this.lblCurrentPositionY.Location = new System.Drawing.Point(194, 53);
            this.lblCurrentPositionY.Name = "lblCurrentPositionY";
            this.lblCurrentPositionY.Size = new System.Drawing.Size(183, 25);
            this.lblCurrentPositionY.TabIndex = 3;
            this.lblCurrentPositionY.Text = "0000.000";
            this.lblCurrentPositionY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(194, 1);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(183, 25);
            this.label11.TabIndex = 7;
            this.label11.Text = "当前位置mm";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnZadd
            // 
            this.btnZadd.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnZadd.Location = new System.Drawing.Point(283, 88);
            this.btnZadd.Name = "btnZadd";
            this.btnZadd.Size = new System.Drawing.Size(60, 60);
            this.btnZadd.TabIndex = 0;
            this.btnZadd.Tag = "3";
            this.btnZadd.Text = "Z+";
            this.btnZadd.UseVisualStyleBackColor = true;
            this.btnZadd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnZadd_MouseDown);
            this.btnZadd.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnZadd_MouseUp);
            // 
            // btnRadd
            // 
            this.btnRadd.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRadd.Location = new System.Drawing.Point(283, 157);
            this.btnRadd.Name = "btnRadd";
            this.btnRadd.Size = new System.Drawing.Size(60, 60);
            this.btnRadd.TabIndex = 7;
            this.btnRadd.Tag = "4";
            this.btnRadd.Text = "R+";
            this.btnRadd.UseVisualStyleBackColor = true;
            this.btnRadd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnRadd_MouseDown);
            this.btnRadd.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnRadd_MouseUp);
            // 
            // btnRdec
            // 
            this.btnRdec.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRdec.Location = new System.Drawing.Point(222, 157);
            this.btnRdec.Name = "btnRdec";
            this.btnRdec.Size = new System.Drawing.Size(60, 60);
            this.btnRdec.TabIndex = 8;
            this.btnRdec.Tag = "4";
            this.btnRdec.Text = "R-";
            this.btnRdec.UseVisualStyleBackColor = true;
            this.btnRdec.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnRdec_MouseDown);
            this.btnRdec.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnRdec_MouseUp);
            // 
            // tbrJogSpeed
            // 
            this.tbrJogSpeed.AutoSize = false;
            this.tbrJogSpeed.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbrJogSpeed.LargeChange = 1;
            this.tbrJogSpeed.Location = new System.Drawing.Point(3, 252);
            this.tbrJogSpeed.Maximum = 7;
            this.tbrJogSpeed.Name = "tbrJogSpeed";
            this.tbrJogSpeed.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbrJogSpeed.Size = new System.Drawing.Size(381, 23);
            this.tbrJogSpeed.TabIndex = 5;
            this.tbrJogSpeed.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // btnZdec
            // 
            this.btnZdec.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnZdec.Location = new System.Drawing.Point(222, 88);
            this.btnZdec.Name = "btnZdec";
            this.btnZdec.Size = new System.Drawing.Size(60, 60);
            this.btnZdec.TabIndex = 0;
            this.btnZdec.Tag = "3";
            this.btnZdec.Text = "Z-";
            this.btnZdec.UseVisualStyleBackColor = true;
            this.btnZdec.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnZdec_MouseDown);
            this.btnZdec.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnZdec_MouseUp);
            // 
            // btnYdec
            // 
            this.btnYdec.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnYdec.Location = new System.Drawing.Point(97, 88);
            this.btnYdec.Name = "btnYdec";
            this.btnYdec.Size = new System.Drawing.Size(60, 60);
            this.btnYdec.TabIndex = 0;
            this.btnYdec.Tag = "2";
            this.btnYdec.Text = "Y-";
            this.btnYdec.UseVisualStyleBackColor = true;
            this.btnYdec.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnYdec_MouseDown);
            this.btnYdec.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnYdec_MouseUp);
            // 
            // btnXdec
            // 
            this.btnXdec.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnXdec.Location = new System.Drawing.Point(97, 157);
            this.btnXdec.Name = "btnXdec";
            this.btnXdec.Size = new System.Drawing.Size(60, 60);
            this.btnXdec.TabIndex = 0;
            this.btnXdec.Tag = "1";
            this.btnXdec.Text = "X-";
            this.btnXdec.UseVisualStyleBackColor = true;
            this.btnXdec.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnXdec_MouseDown);
            this.btnXdec.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnXdec_MouseUp);
            // 
            // btnYadd
            // 
            this.btnYadd.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnYadd.Location = new System.Drawing.Point(34, 88);
            this.btnYadd.Name = "btnYadd";
            this.btnYadd.Size = new System.Drawing.Size(60, 60);
            this.btnYadd.TabIndex = 0;
            this.btnYadd.Tag = "2";
            this.btnYadd.Text = "Y+";
            this.btnYadd.UseVisualStyleBackColor = true;
            this.btnYadd.Click += new System.EventHandler(this.btnYadd_Click);
            this.btnYadd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnYadd_MouseDown);
            this.btnYadd.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnYadd_MouseUp);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.moveSelectHorizontal1);
            this.groupBox3.Controls.Add(this.btnRadd);
            this.groupBox3.Controls.Add(this.btnRdec);
            this.groupBox3.Controls.Add(this.tbrJogSpeed);
            this.groupBox3.Controls.Add(this.btnZadd);
            this.groupBox3.Controls.Add(this.btnZdec);
            this.groupBox3.Controls.Add(this.btnXadd);
            this.groupBox3.Controls.Add(this.btnYdec);
            this.groupBox3.Controls.Add(this.btnXdec);
            this.groupBox3.Controls.Add(this.btnYadd);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(3, 171);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(387, 278);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "操作";
            // 
            // btnXadd
            // 
            this.btnXadd.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnXadd.Location = new System.Drawing.Point(34, 157);
            this.btnXadd.Name = "btnXadd";
            this.btnXadd.Size = new System.Drawing.Size(60, 60);
            this.btnXadd.TabIndex = 0;
            this.btnXadd.Tag = "1";
            this.btnXadd.Text = "X+";
            this.btnXadd.UseVisualStyleBackColor = true;
            this.btnXadd.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnXadd_MouseDown);
            this.btnXadd.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnXadd_MouseUp);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(459, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(393, 458);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "轴操作面板";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.dataGridView1);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(0, 0);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(459, 458);
            this.groupBox6.TabIndex = 4;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "机器人位置";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.名称,
            this.X,
            this.Y,
            this.Z,
            this.R,
            this.定位,
            this.保存});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(3, 17);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(453, 438);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // 名称
            // 
            this.名称.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.名称.FillWeight = 200F;
            this.名称.HeaderText = "名称";
            this.名称.Name = "名称";
            // 
            // X
            // 
            this.X.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.X.HeaderText = "X";
            this.X.Name = "X";
            // 
            // Y
            // 
            this.Y.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Y.HeaderText = "Y";
            this.Y.Name = "Y";
            // 
            // Z
            // 
            this.Z.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Z.HeaderText = "Z";
            this.Z.Name = "Z";
            // 
            // R
            // 
            this.R.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.R.HeaderText = "R";
            this.R.Name = "R";
            // 
            // 定位
            // 
            this.定位.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.定位.HeaderText = "定位";
            this.定位.Name = "定位";
            // 
            // 保存
            // 
            this.保存.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.保存.HeaderText = "保存";
            this.保存.Name = "保存";
            // 
            // moveSelectHorizontal1
            // 
            this.moveSelectHorizontal1.Dock = System.Windows.Forms.DockStyle.Top;
            this.moveSelectHorizontal1.Location = new System.Drawing.Point(3, 17);
            this.moveSelectHorizontal1.Margin = new System.Windows.Forms.Padding(2);
            this.moveSelectHorizontal1.Name = "moveSelectHorizontal1";
            this.moveSelectHorizontal1.Size = new System.Drawing.Size(381, 57);
            this.moveSelectHorizontal1.TabIndex = 43;
            // 
            // RootAxis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox1);
            this.Name = "RootAxis";
            this.Size = new System.Drawing.Size(852, 458);
            this.Load += new System.EventHandler(this.RootAxis_Load);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbrJogSpeed)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblRootSturts;
        private System.Windows.Forms.Label lblCurrentPositionR;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblCurrentPositionZ;
        private System.Windows.Forms.Label lblCurrentPositionX;
        private System.Windows.Forms.Label lblCurrentPositionY;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnZadd;
        private System.Windows.Forms.Button btnRadd;
        private System.Windows.Forms.Button btnRdec;
        private System.Windows.Forms.TrackBar tbrJogSpeed;
        private System.Windows.Forms.Button btnZdec;
        private System.Windows.Forms.Button btnYdec;
        private System.Windows.Forms.Button btnXdec;
        private System.Windows.Forms.Button btnYadd;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnXadd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn X;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y;
        private System.Windows.Forms.DataGridViewTextBoxColumn Z;
        private System.Windows.Forms.DataGridViewTextBoxColumn R;
        private System.Windows.Forms.DataGridViewButtonColumn 定位;
        private System.Windows.Forms.DataGridViewButtonColumn 保存;
        private System.Enginee.MoveSelectHorizontal moveSelectHorizontal1;
    }
}
