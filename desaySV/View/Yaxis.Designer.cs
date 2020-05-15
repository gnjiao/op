namespace desaySV.View
{
    partial class Yaxis
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbrJogSpeed = new System.Windows.Forms.TrackBar();
            this.btnYdec = new System.Windows.Forms.Button();
            this.btnYadd = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCurrentPositionY = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.定位 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.保存 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.moveSelectHorizontal1 = new System.Enginee.MoveSelectHorizontal();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbrJogSpeed)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.moveSelectHorizontal1);
            this.groupBox3.Controls.Add(this.tbrJogSpeed);
            this.groupBox3.Controls.Add(this.btnYdec);
            this.groupBox3.Controls.Add(this.btnYadd);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(3, 91);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(387, 278);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "操作";
            // 
            // tbrJogSpeed
            // 
            this.tbrJogSpeed.AutoSize = false;
            this.tbrJogSpeed.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tbrJogSpeed.LargeChange = 1;
            this.tbrJogSpeed.Location = new System.Drawing.Point(3, 252);
            this.tbrJogSpeed.Maximum = 20000;
            this.tbrJogSpeed.Name = "tbrJogSpeed";
            this.tbrJogSpeed.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tbrJogSpeed.Size = new System.Drawing.Size(381, 23);
            this.tbrJogSpeed.TabIndex = 5;
            this.tbrJogSpeed.TickStyle = System.Windows.Forms.TickStyle.None;
            this.tbrJogSpeed.Scroll += new System.EventHandler(this.tbrJogSpeed_Scroll);
            // 
            // btnYdec
            // 
            this.btnYdec.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnYdec.Location = new System.Drawing.Point(154, 89);
            this.btnYdec.Name = "btnYdec";
            this.btnYdec.Size = new System.Drawing.Size(60, 60);
            this.btnYdec.TabIndex = 0;
            this.btnYdec.Tag = "2";
            this.btnYdec.Text = "Y-";
            this.btnYdec.UseVisualStyleBackColor = true;
            this.btnYdec.Click += new System.EventHandler(this.btnYdec_Click);
            this.btnYdec.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnYdec_MouseDown);
            this.btnYdec.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnYdec_MouseUp);
            // 
            // btnYadd
            // 
            this.btnYadd.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnYadd.Location = new System.Drawing.Point(52, 89);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(305, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(393, 383);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "轴操作面板";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(3, 17);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(387, 74);
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
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblCurrentPositionY, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label11, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(381, 54);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(4, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Y轴";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrentPositionY
            // 
            this.lblCurrentPositionY.AutoSize = true;
            this.lblCurrentPositionY.BackColor = System.Drawing.Color.Black;
            this.lblCurrentPositionY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentPositionY.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCurrentPositionY.ForeColor = System.Drawing.Color.SpringGreen;
            this.lblCurrentPositionY.Location = new System.Drawing.Point(194, 27);
            this.lblCurrentPositionY.Name = "lblCurrentPositionY";
            this.lblCurrentPositionY.Size = new System.Drawing.Size(183, 26);
            this.lblCurrentPositionY.TabIndex = 2;
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
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.dataGridView1);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox6.Location = new System.Drawing.Point(0, 0);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(305, 383);
            this.groupBox6.TabIndex = 6;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Y轴位置";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.名称,
            this.Y,
            this.定位,
            this.保存});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(3, 17);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(299, 363);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // 名称
            // 
            this.名称.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.名称.HeaderText = "名称";
            this.名称.Name = "名称";
            // 
            // Y
            // 
            this.Y.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Y.HeaderText = "Y";
            this.Y.Name = "Y";
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
            this.moveSelectHorizontal1.TabIndex = 42;
            // 
            // Yaxis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox6);
            this.Name = "Yaxis";
            this.Size = new System.Drawing.Size(698, 383);
            this.Load += new System.EventHandler(this.Yaxis_Load);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tbrJogSpeed)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TrackBar tbrJogSpeed;
        private System.Windows.Forms.Button btnYdec;
        private System.Windows.Forms.Button btnYadd;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCurrentPositionY;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn 名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y;
        private System.Windows.Forms.DataGridViewButtonColumn 定位;
        private System.Windows.Forms.DataGridViewButtonColumn 保存;
        private System.Enginee.MoveSelectHorizontal moveSelectHorizontal1;
    }
}
