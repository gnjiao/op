namespace desaySV
{
    partial class frmParameter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flpVacuoParam = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.flpAxisSpeed = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radioButton_Null = new System.Windows.Forms.RadioButton();
            this.radioButton_SV = new System.Windows.Forms.RadioButton();
            this.radioButton_MES = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.flpVacuoParam.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flpVacuoParam
            // 
            this.flpVacuoParam.Controls.Add(this.groupBox5);
            this.flpVacuoParam.Controls.Add(this.groupBox1);
            this.flpVacuoParam.Location = new System.Drawing.Point(11, -4);
            this.flpVacuoParam.Margin = new System.Windows.Forms.Padding(2);
            this.flpVacuoParam.Name = "flpVacuoParam";
            this.flpVacuoParam.Size = new System.Drawing.Size(302, 142);
            this.flpVacuoParam.TabIndex = 22;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(693, 395);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(69, 22);
            this.btnSave.TabIndex = 28;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // flpAxisSpeed
            // 
            this.flpAxisSpeed.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpAxisSpeed.Location = new System.Drawing.Point(11, 141);
            this.flpAxisSpeed.Margin = new System.Windows.Forms.Padding(2);
            this.flpAxisSpeed.Name = "flpAxisSpeed";
            this.flpAxisSpeed.Size = new System.Drawing.Size(302, 295);
            this.flpAxisSpeed.TabIndex = 29;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.radioButton_Null);
            this.groupBox5.Controls.Add(this.radioButton_SV);
            this.groupBox5.Controls.Add(this.radioButton_MES);
            this.groupBox5.Location = new System.Drawing.Point(3, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(292, 51);
            this.groupBox5.TabIndex = 307;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Interlock模式";
            // 
            // radioButton_Null
            // 
            this.radioButton_Null.AutoSize = true;
            this.radioButton_Null.Checked = true;
            this.radioButton_Null.Location = new System.Drawing.Point(9, 23);
            this.radioButton_Null.Name = "radioButton_Null";
            this.radioButton_Null.Size = new System.Drawing.Size(47, 16);
            this.radioButton_Null.TabIndex = 2;
            this.radioButton_Null.TabStop = true;
            this.radioButton_Null.Tag = "空跑模式";
            this.radioButton_Null.Text = "空跑";
            this.radioButton_Null.UseVisualStyleBackColor = true;
            // 
            // radioButton_SV
            // 
            this.radioButton_SV.AutoSize = true;
            this.radioButton_SV.Location = new System.Drawing.Point(208, 23);
            this.radioButton_SV.Name = "radioButton_SV";
            this.radioButton_SV.Size = new System.Drawing.Size(59, 16);
            this.radioButton_SV.TabIndex = 1;
            this.radioButton_SV.Tag = "SV模式";
            this.radioButton_SV.Text = "SV模式";
            this.radioButton_SV.UseVisualStyleBackColor = true;
            // 
            // radioButton_MES
            // 
            this.radioButton_MES.AutoSize = true;
            this.radioButton_MES.Location = new System.Drawing.Point(92, 23);
            this.radioButton_MES.Name = "radioButton_MES";
            this.radioButton_MES.Size = new System.Drawing.Size(65, 16);
            this.radioButton_MES.TabIndex = 0;
            this.radioButton_MES.Tag = "MES模式";
            this.radioButton_MES.Text = "MES模式";
            this.radioButton_MES.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton1);
            this.groupBox1.Controls.Add(this.radioButton3);
            this.groupBox1.Location = new System.Drawing.Point(3, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(292, 51);
            this.groupBox1.TabIndex = 308;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "单双模式";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(9, 23);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(47, 16);
            this.radioButton1.TabIndex = 2;
            this.radioButton1.TabStop = true;
            this.radioButton1.Tag = "双屏模式";
            this.radioButton1.Text = "双屏";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(92, 23);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(47, 16);
            this.radioButton3.TabIndex = 0;
            this.radioButton3.Tag = "单屏模式";
            this.radioButton3.Text = "单屏";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // frmParameter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(913, 458);
            this.Controls.Add(this.flpAxisSpeed);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.flpVacuoParam);
            this.Name = "frmParameter";
            this.Text = "frmParameter";
            this.Load += new System.EventHandler(this.frmParameter_Load);
            this.flpVacuoParam.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flpVacuoParam;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.FlowLayoutPanel flpAxisSpeed;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radioButton_Null;
        private System.Windows.Forms.RadioButton radioButton_SV;
        private System.Windows.Forms.RadioButton radioButton_MES;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton3;
    }
}