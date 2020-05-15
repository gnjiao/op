using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;

namespace sv_Interlocking_Main
{
    public partial class SV_InterlockingForm : Form
    {
        public SV_InterlockingForm()
        {
            InitializeComponent();
        }

        public static int idex = 0;
        public static bool boolStatus = true;

        public static string[] arrStr;
        public static string Information = "";
        string serverNowtime;
        double serverNowtime1;

        public static int snSubSample = 0;
        public static int function = 5;


        public static bool job = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
             
                boolStatus = true;
                idex = 0;
                status = 0;
                this.DialogResult = DialogResult.None;
                string str1 = sv_Interlocking_Main_Class.Infor;
                textBox3.Text = str1;
                char[] separator = { ',' };
                arrStr = str1.Split(separator);
                label8.Text = arrStr[7];
                label9.Text = arrStr[4];
                label10.Text = arrStr[5];
                label11.Text = arrStr[6];
                label3.Text = "Debug = " + arrStr[8];
                function = Convert.ToInt16(arrStr[11]);
                label14.Text = arrStr[11];
                snSubSample = Convert.ToInt32(arrStr[4].Substring(arrStr[4].Length - 6, 6));
                string constr = "Server=" + arrStr[3] + ";" + "user=" + arrStr[1] + ";" + "pwd=" + arrStr[0] + ";" + "database=" + arrStr[2];
                SqlConnection Mysqlcon = new SqlConnection(constr);
                //获取服务器当时时间
                try
                {
                    Mysqlcon.Open();
                }
                catch
                {
                    textBox1.Text = "数据库连接超时";
                }
              
                serverNowtime = Time(12, true, Mysqlcon);
                DateTime dTime;
                DateTime.TryParse(serverNowtime, out dTime);
                TimeSpan nowtimespan = new TimeSpan(dTime.Ticks);
                serverNowtime1 = nowtimespan.TotalSeconds / 60;

                sv_Interlocking_Main(str1);
             
                if (row2 == status && row2 != 0)
                {
                    timer1.Interval = 500;
                    timer1.Start();
                    this.DialogResult = DialogResult.OK;
                    textBox1.BackColor = Color.LawnGreen;

                }
                else
                {
                    timer1.Stop();
                }
                Mysqlcon.Close();

                Query_Failed_ReTest_Permission(arrStr[7]);
                if (arrStr[7].ToUpper().IndexOf("10S") == 0 || (row > 1 && this.dataGridView4.Rows[0].Cells[0].Value.ToString().ToUpper() == "TRUE"))
                {
                    if (arrStr[8] == "True")
                    {
                        button2.Enabled = false;
                    }
                    else
                    {
                        button3.Visible = false;
                    }
                }
                else
                {
                    textBox2.ReadOnly = true;
                    textBox2.BorderStyle = BorderStyle.Fixed3D;
                    job = true;
                    button3.Visible = false;

                }

                if (arrStr[9] == "False")
                {
                    this.DialogResult = DialogResult.No;
                    this.Close();
                }


            }
            catch
            {
                if(textBox1.Text== "数据库连接超时")
                {
                    textBox1.Text = "数据库连接超时";
                    button3.Visible = false;
                }
                else
                {
                    textBox1.Text = "信息输入错误";
                    button3.Visible = false;
                }
              
            }
       

        }

        public static int status = 0;
        public static int row2 = 0;
        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            boolStatus = false;
            this.Close();

        }

        public static int row1 = 0;
        public static int j = 0;

        private void sv_Interlocking_Main(string Information)
        {
      
            while (boolStatus)
            {
                Thread.Sleep(10);
            
                switch (idex)
                {

                    //Read Database
                    case 0:
                        //string constr = "Server=HZHE015A" + ";" + "user=TE_HVAC" + ";" + "pwd=ped@Hvac" + ";" + "database=ED_Test";
                        string constr = "Server=" + arrStr[3] + ";" + "user=" + arrStr[1] + ";" + "pwd=" + arrStr[0] + ";" + "database=" + arrStr[2];
                        SqlConnection Mysqlcon = new SqlConnection(constr);
                        Mysqlcon.Open();


                        try
                        {
                            using (SqlConnection conn = new SqlConnection(constr))
                            {

                            
                                string cunchu = "visn_QueryInterlockingStatus";//要调用存储过程名称
                                DataTable dt = new DataTable();
                                using (SqlDataAdapter sqlData = new SqlDataAdapter(cunchu, constr))
                                {
                                    //表示要执行的是存储过程
                                    sqlData.SelectCommand.CommandType = CommandType.StoredProcedure;
                                    //增加参数
                                    SqlParameter[] pms = new SqlParameter[] {
                        new SqlParameter("@SN",SqlDbType.Char){ Value=arrStr[4]},
                        new SqlParameter("@Current_Station",SqlDbType.Char){ Value=arrStr[5]},
                         new SqlParameter("@LineGroup",SqlDbType.Int){ Value=Convert.ToInt16(arrStr[6])},
   
                    };
                                  
                                    sqlData.SelectCommand.Parameters.AddRange(pms);
                                    sqlData.Fill(dt);

                                    this.dataGridView1.DataSource = dt;
                                    int row = dataGridView1.Rows.Count;//总行


                                    if(row==2)
                                    {

                                        for (int i = 0; i < row - 1; i++)
                                        {
                                            string aaa = this.dataGridView1.Rows[i].Cells[2].Value.ToString();

                                            if (this.dataGridView1.Rows[i].Cells[4].Value.ToString() == "")
                                            {
                                                string[] bb = { this.dataGridView1.Rows[i].Cells[2].Value.ToString(), this.dataGridView1.Rows[i].Cells[1].Value.ToString(), "", "" };//提取dataGridView1中数据
                                                this.dataGridView2.Rows.Add(bb);
                                            }
                                            else
                                            {
                                                string[] bb = { this.dataGridView1.Rows[i].Cells[2].Value.ToString(), this.dataGridView1.Rows[i].Cells[1].Value.ToString(), this.dataGridView1.Rows[i].Cells[4].Value.ToString(), this.dataGridView1.Rows[i].Cells[5].Value.ToString() };//提取dataGridView1中数据
                                                this.dataGridView2.Rows.Add(bb);

                                            }

                                        }
                                    }
                                    else
                                    {
                                        int b = 0;
                                        double frontTime = 0;
                                        double laterTime = 0;
                                        List<double> list = new List<double>();
                                        for (int i = 0; i < row - 1; i++)
                                        {
                                            string aaa = this.dataGridView1.Rows[i].Cells[2].Value.ToString();

                                            if (this.dataGridView1.Rows[i].Cells[4].Value.ToString() == "")
                                            {
                                                string[] bb = { this.dataGridView1.Rows[i].Cells[2].Value.ToString(), this.dataGridView1.Rows[i].Cells[1].Value.ToString(), "00/00/0000 00:00:00", "No Test" };//提取dataGridView1中数据
                                                this.dataGridView2.Rows.Add(bb);
                                            }
                                            else
                                            {
                                            
                                                DateTime dTime1;
                                                DateTime.TryParse(this.dataGridView1.Rows[i].Cells[4].Value.ToString(), out dTime1);
                                                TimeSpan nowtimespan = new TimeSpan(dTime1.Ticks);
                                                double dTime2 = nowtimespan.TotalSeconds / 60;

                                            
                                                list.Add(dTime2);
                                            
                                                for ( int j = 1; j <= b; j++)
                                                {
                                                    frontTime = list[j - 1];
                                                    laterTime = list[j];

                                                }
                                                b++;
                                                if (frontTime > laterTime)
                                                {
                                                    dataGridView2.Rows.RemoveAt(i-1);
                                                    string[] bb = { this.dataGridView1.Rows[i-1].Cells[2].Value.ToString(), this.dataGridView1.Rows[i-1].Cells[1].Value.ToString(), this.dataGridView1.Rows[i-1].Cells[4].Value.ToString(), "Time Order Wrong" };//提取dataGridView1中数据
                                                    this.dataGridView2.Rows.Add(bb);
                                                    string[] bbb = { this.dataGridView1.Rows[i].Cells[2].Value.ToString(), this.dataGridView1.Rows[i].Cells[1].Value.ToString(), this.dataGridView1.Rows[i].Cells[4].Value.ToString(), this.dataGridView1.Rows[i].Cells[5].Value.ToString() };//提取dataGridView1中数据
                                                    this.dataGridView2.Rows.Add(bbb);
                                                }
                                                else
                                                {

                                                    string[] bb = { this.dataGridView1.Rows[i].Cells[2].Value.ToString(), this.dataGridView1.Rows[i].Cells[1].Value.ToString(), this.dataGridView1.Rows[i].Cells[4].Value.ToString(), this.dataGridView1.Rows[i].Cells[5].Value.ToString() };//提取dataGridView1中数据
                                                    this.dataGridView2.Rows.Add(bb);
                                                }

                                            }

                                        }
                                    }

                                   
                                    row1 = dataGridView2.Rows.Count;//总行
                                    row2 = row1 - 1;
                                    j = 0;
                                    switch (function)
                                    {
                                        case 5:

                                            Function_5();
                                            break;
                                        case 6:
                                            Function_6();
                                            break;
                                        case 7:
                                            Function_7();
                                            break;
                                    }

                          
              

                                    //swictch事件判断Function模式
                                    #region
                                    //if(row1==2)
                                    //{


                                    //    for (int i = 0; i < row1 - 1; i++)
                                    //    {
                                    //        if (dataGridView2.Rows[i].Cells[3].Value.ToString() == "")
                                    //        {
                                    //            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.LawnGreen; //背景颜色
                                    //            this.DialogResult = DialogResult.OK;
                                    //            status = j + 1;
                                    //            j++;
                                    //        }
                                    //        else if(dataGridView2.Rows[i].Cells[3].Value.ToString() == "Passed")
                                    //        {
                                    //            switch (function)
                                    //            {
                                    //                case 5:
                                    //                    if(row2 == status)
                                    //                    {
                                    //                        this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.LawnGreen; //背景颜色
                                    //                        this.DialogResult = DialogResult.OK;
                                    //                        status = j + 1;
                                    //                        j++;
                                    //                    }


                                    //                    break;
                                    //                case 6:
                                    //                    if (snSubSample >= 999980 && snSubSample <= 999999)
                                    //                    {
                                    //                        this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.LawnGreen; //背景颜色
                                    //                        this.DialogResult = DialogResult.OK;
                                    //                        status = j + 1;
                                    //                        j++;
                                    //                    }
                                    //                    else
                                    //                    {

                                    //                        this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.LawnGreen; //背景颜色
                                    //                        textBox1.Text = " Record Passed ReTest";
                                    //                        if (job == true)
                                    //                        {

                                    //                        }
                                    //                        else
                                    //                        {
                                    //                            timer2.Interval = 2000;
                                    //                            timer2.Start();
                                    //                        }
                                    //                    }
                                    //                    break;
                                    //                case 7:
                                    //                    if (snSubSample >= 999980 && snSubSample <= 999999)
                                    //                    {
                                    //                        this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.LawnGreen; //背景颜色
                                    //                        this.DialogResult = DialogResult.OK;
                                    //                        status = j + 1;
                                    //                        j++;
                                    //                    }
                                    //                    else
                                    //                    {

                                    //                        this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.LawnGreen; //背景颜色
                                    //                        textBox1.Text = " Record Passed ReTest";
                                    //                        if (job == true)
                                    //                        {

                                    //                        }
                                    //                        else
                                    //                        {
                                    //                            timer2.Interval = 2000;
                                    //                            timer2.Start();
                                    //                        }
                                    //                    }
                                    //                    break;

                                    //                default:

                                    //                    break;

                                    //            }


                                    //        }
                                    //        else
                                    //        {
                                    //            switch (function)
                                    //            {
                                    //                case 5:
                                    //                    if (snSubSample >= 999980 && snSubSample <= 999999)
                                    //                    {
                                    //                        this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                                    //                        textBox1.Text = " Record Failed";
                                    //                        this.DialogResult = DialogResult.OK;
                                    //                        status = j + 1;
                                    //                        j++;
                                    //                    }
                                    //                    else
                                    //                    {
                                    //                        this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                                    //                        textBox1.Text = " Record Failed";
                                    //                        if (job == true)
                                    //                        {

                                    //                        }
                                    //                        else
                                    //                        {
                                    //                            timer2.Interval = 2000;
                                    //                            timer2.Start();
                                    //                        }
                                    //                    }
                                    //                    break;
                                    //                case 6:
                                    //                    if (row2 == status)
                                    //                    {
                                    //                        this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                                    //                        textBox1.Text = " Record Failed";
                                    //                        this.DialogResult = DialogResult.OK;
                                    //                        status = j + 1;
                                    //                        j++;
                                    //                    }

                                    //                    break;
                                    //                case 7:
                                    //                    if (snSubSample >= 999980 && snSubSample <= 999999)
                                    //                    {
                                    //                        this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                                    //                        textBox1.Text = " Record Failed";
                                    //                        this.DialogResult = DialogResult.OK;
                                    //                        status = j + 1;
                                    //                        j++;
                                    //                    }
                                    //                    else
                                    //                    {
                                    //                        this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                                    //                        textBox1.Text = " Record Failed";
                                    //                        if (job == true)
                                    //                        {

                                    //                        }
                                    //                        else
                                    //                        {
                                    //                            timer2.Interval = 2000;
                                    //                            timer2.Start();
                                    //                        }
                                    //                    }
                                    //                    break;

                                    //                default:

                                    //                    break;

                                    //            }


                                    //        }
                                    //    }

                                    //}
                                    //else
                                    //{
                                    //    for (int i = 0; i < row1 - 1; i++)
                                    //    {

                                    //        if (dataGridView2.Rows[i].Cells[3].Value.ToString() == "Passed")
                                    //        {

                                    //            switch (function)
                                    //            {
                                    //                case 5:
                                    //                    if (row2 == status)
                                    //                    {
                                    //                        this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.LawnGreen; //背景颜色
                                    //                        this.DialogResult = DialogResult.OK;
                                    //                        status = j + 1;
                                    //                        j++;
                                    //                    }

                                    //                    break;
                                    //                case 6:
                                    //                    if (i == row1 - 2 && snSubSample < 999980)
                                    //                    {
                                    //                        this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.LawnGreen; //背景颜色
                                    //                        textBox1.Text = " Record Passed ReTest";
                                    //                        if (job == true)
                                    //                        {

                                    //                        }
                                    //                        else
                                    //                        {
                                    //                            timer2.Interval = 2000;
                                    //                            timer2.Start();
                                    //                        }
                                    //                    }
                                    //                    else
                                    //                    {
                                    //                        this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.LawnGreen; //背景颜色
                                    //                        this.DialogResult = DialogResult.OK;
                                    //                        status = j + 1;
                                    //                        j++;

                                    //                    }
                                    //                    break;
                                    //                case 7:
                                    //                    if (i == row1 - 2 && snSubSample < 999980)
                                    //                    {
                                    //                        this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.LawnGreen; //背景颜色
                                    //                        textBox1.Text = " Record Passed ReTest";
                                    //                        if (job == true)
                                    //                        {

                                    //                        }
                                    //                        else
                                    //                        {
                                    //                            timer2.Interval = 2000;
                                    //                            timer2.Start();
                                    //                        }
                                    //                    }
                                    //                    else
                                    //                    {
                                    //                        this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.LawnGreen; //背景颜色
                                    //                        this.DialogResult = DialogResult.OK;
                                    //                        status = j + 1;
                                    //                        j++;

                                    //                    }
                                    //                    break;

                                    //                default:

                                    //                    break;
                                    //            }



                                    //        }
                                    //        else if (dataGridView2.Rows[i].Cells[3].Value.ToString() == "Failed")
                                    //        {

                                    //            switch (function)
                                    //            {
                                    //                case 5:
                                    //                    if (snSubSample >= 999980 && snSubSample <= 999999)
                                    //                    {
                                    //                        this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                                    //                        textBox1.Text = " Record Failed";

                                    //                        this.DialogResult = DialogResult.OK;
                                    //                        status = j + 1;
                                    //                        j++;
                                    //                    }
                                    //                    else
                                    //                    {
                                    //                        this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                                    //                        textBox1.Text = " Record Failed";
                                    //                        if (job == true)
                                    //                        {

                                    //                        }
                                    //                        else
                                    //                        {
                                    //                            timer2.Interval = 2000;
                                    //                            timer2.Start();
                                    //                        }

                                    //                    }

                                    //                    break;
                                    //                case 6:
                                    //                    if (row2 == status)
                                    //                    {
                                    //                        this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                                    //                        textBox1.Text = " Record Failed";

                                    //                        this.DialogResult = DialogResult.OK;
                                    //                        status = j + 1;
                                    //                        j++;
                                    //                    }


                                    //                    break;
                                    //                case 7:
                                    //                    if (snSubSample >= 999980 && snSubSample <= 999999)
                                    //                    {
                                    //                        this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                                    //                        textBox1.Text = " Record Failed";

                                    //                        this.DialogResult = DialogResult.OK;
                                    //                        status = j + 1;
                                    //                        j++;
                                    //                    }
                                    //                    else
                                    //                    {
                                    //                        this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                                    //                        textBox1.Text = " Record Failed";
                                    //                        if (job == true)
                                    //                        {

                                    //                        }
                                    //                        else
                                    //                        {
                                    //                            timer2.Interval = 2000;
                                    //                            timer2.Start();
                                    //                        }

                                    //                    }

                                    //                    break;

                                    //                default:

                                    //                    break;
                                    //            }


                                    //        }
                                    //        else
                                    //        {
                                    //            if (snSubSample >= 999980 && snSubSample <= 999999)
                                    //            {
                                    //                this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                                    //                textBox1.Text = "Record NoTest";
                                    //                this.DialogResult = DialogResult.OK;
                                    //                status = j + 1;
                                    //                j++;
                                    //            }
                                    //            else
                                    //            {
                                    //                this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                                    //                textBox1.Text = "Record NoTest";
                                    //                if (job == true)
                                    //                {

                                    //                }
                                    //                else
                                    //                {
                                    //                    timer2.Interval = 2000;
                                    //                    timer2.Start();
                                    //                }
                                    //            }


                                    //        }


                                    //    }
                                    //}

                                    #endregion


                                    Mysqlcon.Close();
                                    //boolStatus = false;
                                    idex = 11;
                                }
                            }
                        }
                        catch
                        {
                            idex = 11;
                            textBox1.Text = "登录数据库异常";
                        }
                        break;

                    //Verify Stations Status
                    case 11:
                        boolStatus = false;

                        break;


                }
            }


        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g0 = e.Graphics;
            g0.DrawLine(Pens.Silver, new Point(10, 70), new Point(489, 70));

            Graphics g1 = e.Graphics;
            g1.DrawLine(Pens.Silver, new Point(10, 180), new Point(489, 180));

            Graphics g2 = e.Graphics;
            g1.DrawLine(Pens.Silver, new Point(10, 440), new Point(489, 440));
   
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }


        #region   获取服务器时间
        public String Time(int a, bool b, SqlConnection com)
        {
            //如a=12，Time(12,true,Conn)表示以yymmddhhmmss格式返回时间字符串，12表示从第一位开始12位
            //如果是a=6,那么输出yymmdd格式。
            //Time(a,false,Conn)表示以2007-1-7 10:30:05（注意不是2007-01-07）格式返回时间字符串，前面的数字a没用到。
            SqlCommand Commup3 = new SqlCommand("select getdate() as systemtimes", com);
            SqlDataReader dr1;
            dr1 = Commup3.ExecuteReader();
            String cc = "";
            String aa = "";
            String[] s = null;
            String[] ymd = null;
            String[] hms = null;
            String x = " "; String y = "-"; String z = ":";
            while (dr1.Read())
            {
                cc = Convert.ToString(dr1.GetDateTime(0));
                s = cc.Split(x.ToCharArray(), 2);
                ymd = s[0].Split(y.ToCharArray(), 3);
                hms = s[1].Split(z.ToCharArray(), 3);
                for (int i = 0; i < ymd.Length; i++)
                {
                    if (ymd[i].Length == 1) ymd[i] = "0" + ymd[i];
                    aa = aa + ymd[i];
                }
                for (int i = 0; i < hms.Length; i++)
                {
                    if (hms[i].Length == 1) hms[i] = "0" + hms[i];
                    aa = aa + hms[i];
                }
            }
            dr1.Dispose();
            if (b == true)
                return cc;
            //return aa.Substring(2, a);
            else
                return cc;
        }
        #endregion

        public static  bool Query_Failed_ReTest_Permission_Str = false;
        private void button2_Click(object sender, EventArgs e)
        {

            if (this.textBox2.Text.Length == 8 && (this.textBox2.Text.ToUpper().IndexOf("10S") == 0 || this.textBox2.Text.ToUpper().IndexOf("10A") == 0) || this.textBox2.Text.Length == 9 && this.textBox2.Text.ToUpper().IndexOf("10A") == 0)
            {



                Query_Failed_ReTest_Permission(textBox2.Text);
                if (row > 1 && this.dataGridView4.Rows[0].Cells[0].Value.ToString().ToUpper() == "TRUE")
                {
                    string constr = "Server=" + "HZHE015A" + ";" + "user=" + "TE_HVAC" + ";" + "pwd=" + "ped@Hvac" + ";" + "database=" + "Common_Tools";
                    SqlConnection Mysqlcon = new SqlConnection(constr);
                    Mysqlcon.Open();
                    string sql_string_Failed_ReTest_AdminID_Result = "insert into Failed_ReTest_AdminID_Result (STATION_ID,Failed_SN,UserID,DATE_TIME) values('" + label10.Text + "','" + label9.Text + "','" + textBox2.Text + "','" + serverNowtime + "')";
                    SqlCommand myCom = new SqlCommand(sql_string_Failed_ReTest_AdminID_Result, Mysqlcon);
                    myCom.ExecuteNonQuery();
                    Mysqlcon.Close();

                    Query_Failed_ReTest_Permission_Str = true;
                    this.DialogResult = DialogResult.OK;
                    boolStatus = false;
                    this.Close();

                }
                else
                {
                    Query_Failed_ReTest_Permission_Str = false;

                    timer2.Interval = 2000;
                    timer2.Start();
                    textBox2.BackColor = Color.Red;
                    textBox2.Clear();
                    textBox2.Focus();
                }



            }

            else if (this.textBox2.Text.Length != 8 || this.textBox2.Text.ToUpper().IndexOf("10S") != 0 || this.textBox2.Text.ToUpper().IndexOf("10A") != 0 || this.textBox2.Text.Length != 9 && this.textBox2.Text.ToUpper().IndexOf("10A") != 0)
            {
                timer2.Interval = 2000;
                timer2.Start();
                textBox2.BackColor = Color.Red;
                textBox2.Clear();
                textBox2.Focus();


            }
            else
            {

            }
        }

        private void timer2_Tick_1(object sender, EventArgs e)
        {
         
            textBox2.Clear();
            textBox2.Focus();
            textBox2.BackColor = Color.White;
            textBox2.Focus();
        }
        #region 工号解锁查询
        public static int row = 0;
        private void Query_Failed_ReTest_Permission(string adminID )
        {
            try
            {
                string constr = "Server=" + "HZHE015A" + ";" + "user=" + "TE_HVAC" + ";" + "pwd=" + "ped@Hvac" + ";" + "database=" + "Common_Tools";
                SqlConnection Mysqlcon = new SqlConnection(constr);
                Mysqlcon.Open();
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    string cunchu = "Query_Failed_ReTest_Permission";//要调用存储过程名称
                    DataTable dt = new DataTable();
                    using (SqlDataAdapter sqlData = new SqlDataAdapter(cunchu, constr))
                    {
                        //表示要执行的是存储过程
                        sqlData.SelectCommand.CommandType = CommandType.StoredProcedure;
                        //增加参数
                        SqlParameter[] pms = new SqlParameter[] {
                        new SqlParameter("@AdminID",SqlDbType.Char){ Value=adminID},


                    };

                        sqlData.SelectCommand.Parameters.AddRange(pms);
                        sqlData.Fill(dt);

                        this.dataGridView4.DataSource = dt;
                       row = dataGridView4.Rows.Count;//总行

                      


                    }
                }
            }
            catch
            {
                MessageBox.Show("异常报错");
            }

        }
        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            Query_Failed_ReTest_Permission_Str = true;
            this.DialogResult = DialogResult.OK;
            boolStatus = false;
            this.Close();
        }


        #region  Function 5 互锁+Failed防错
        int a = 0;

        private void Function_5()
        {
            if (row1 == 2)
            {
                for (int i = 0; i < row1 - 1; i++)
                {
                    if (dataGridView2.Rows[i].Cells[3].Value.ToString() == ""|| dataGridView2.Rows[i].Cells[3].Value.ToString() == "Passed")
                    {
                        this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.LawnGreen; //背景颜色
                        this.DialogResult = DialogResult.OK;
                        status = j + 1;
                        j++;
                    }           
                    else
                    {
                        if (snSubSample >= 999980 && snSubSample <= 999999)
                        {
                            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                            textBox1.Text = " Record Failed";
                            this.DialogResult = DialogResult.OK;
                            status = j + 1;
                            j++;
                        }
                        else
                        {
                            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                            textBox1.Text = " Record Failed";
                            this.DialogResult = DialogResult.None;
                            if (job == true)
                            {

                            }
                            else
                            {
                                timer2.Interval = 2000;
                                timer2.Start();
                            }
                        }

                    }
                }

            }
            else
            {
                for (int i = 0; i < row1 - 1; i++)
                {

                    if (dataGridView2.Rows[i].Cells[3].Value.ToString() == "Passed")
                    {
                        this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.LawnGreen; //背景颜色
                        this.DialogResult = DialogResult.OK;
                        status = j + 1;
                        j++;
                        a += 1;
                    }
                    else if (dataGridView2.Rows[i].Cells[3].Value.ToString() == "Failed")
                    {
                        if (snSubSample >= 999980 && snSubSample <= 999999)
                        {
                            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                            textBox1.Text = " Record Failed";

                            this.DialogResult = DialogResult.OK;
                            status = j + 1;
                            j++;
                            a += 1;
                        }
                        else
                        {
                            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                            textBox1.Text = " Record Failed";
                            this.DialogResult = DialogResult.None;
                            if (job == true)
                            {

                            }
                            else
                            {
                                timer2.Interval = 2000;
                                timer2.Start();
                            }

                        }


                    }
                    else if (dataGridView2.Rows[i].Cells[3].Value.ToString() == "Time Order Wrong")
                    {
                        if (snSubSample >= 999980 && snSubSample <= 999999)
                        {
                            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                            textBox1.Text = "Record Time Order Wrong";
                            this.DialogResult = DialogResult.OK;
                            status = j + 1;
                            j++;
                            a += 1;
                        }
                        else
                        {
                            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                            textBox1.Text = "Record Time Order Wrong";
                            this.DialogResult = DialogResult.None;
                            if (job == true)
                            {

                            }
                            else
                            {
                                timer2.Interval = 2000;
                                timer2.Start();
                            }
                        }


                    }
                    else
                    {
                        if (snSubSample >= 999980 && snSubSample <= 999999)
                        {
                            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                            textBox1.Text = "Record NoTest";
                            this.DialogResult = DialogResult.OK;
                            status = j + 1;
                            j++;
                            a += 1;
                        }
                        else
                        {
                            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                            textBox1.Text = "Record NoTest";
                            this.DialogResult = DialogResult.None;
                            if (job == true)
                            {

                            }
                            else
                            {
                                timer2.Interval = 2000;
                                timer2.Start();
                            }
                        }
                    }


                }
                if(a == row1 - 1)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    this.DialogResult = DialogResult.None;
                }

            }

        }
        #endregion


        #region Function 6 互锁+Passed防错
        private void Function_6()
        {
            if (row1 == 2)
            {
                for (int i = 0; i < row1 - 1; i++)
                {
                    if (dataGridView2.Rows[i].Cells[3].Value.ToString() == "")
                    {
                        this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.LawnGreen; //背景颜色
                        this.DialogResult = DialogResult.OK;
                        status = j + 1;
                        j++;
                    }
                    else if (dataGridView2.Rows[i].Cells[3].Value.ToString() == "Passed")
                    {
                        if (snSubSample >= 999980 && snSubSample <= 999999)
                        {
                            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.LawnGreen; //背景颜色
                            this.DialogResult = DialogResult.OK;
                            status = j + 1;
                            j++;
                        }
                        else
                        {

                            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.LawnGreen; //背景颜色
                            textBox1.Text = " Record Passed ReTest";
                            if (job == true)
                            {

                            }
                            else
                            {
                                timer2.Interval = 2000;
                                timer2.Start();
                            }
                        }

                    }
                    else
                    {
                        this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                        textBox1.Text = " Record Failed";
                        this.DialogResult = DialogResult.OK;
                        status = j + 1;
                        j++;

                    }
                }

            }
            else
            {
                for (int i = 0; i < row1 - 1; i++)
                {

                    if (dataGridView2.Rows[i].Cells[3].Value.ToString() == "Passed")
                    {
                        if (i == row1 - 2 && snSubSample < 999980)
                        {
                            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.LawnGreen; //背景颜色
                            textBox1.Text = " Record Passed ReTest";
                            if (job == true)
                            {

                            }
                            else
                            {
                                timer2.Interval = 2000;
                                timer2.Start();
                            }
                        }
                        else
                        {
                            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.LawnGreen; //背景颜色
                            this.DialogResult = DialogResult.OK;
                            status = j + 1;
                            j++;
                            a += 1;
                        }


                    }
                    else if (dataGridView2.Rows[i].Cells[3].Value.ToString() == "Failed")
                    {
                        if (snSubSample >= 999980 && snSubSample <= 999999)
                        {
                            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                            textBox1.Text = " Record Failed";

                            this.DialogResult = DialogResult.OK;
                            status = j + 1;
                            j++;
                            a += 1;
                        }
                        else
                        {
                            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                            textBox1.Text = " Record Failed";
                            this.DialogResult = DialogResult.OK;
                            a += 1;
                            if (job == true)
                            {

                            }
                            else
                            {
                                timer2.Interval = 2000;
                                timer2.Start();
                            }

                        }


                    }
                    else if (dataGridView2.Rows[i].Cells[3].Value.ToString() == "")
                    {
                        if (snSubSample >= 999980 && snSubSample <= 999999)
                        {
                            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                            textBox1.Text = "Record Time Order Wrong";
                            this.DialogResult = DialogResult.OK;
                            status = j + 1;
                            j++;
                            a += 1;
                        }
                        else
                        {
                            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                            textBox1.Text = "Record Time Order Wrong";
                            this.DialogResult = DialogResult.None;
                            if (job == true)
                            {

                            }
                            else
                            {
                                timer2.Interval = 2000;
                                timer2.Start();
                            }
                        }

                        
                    }
                    else
                    {
                        if (snSubSample >= 999980 && snSubSample <= 999999)
                        {
                            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                            textBox1.Text = "Record NoTest";
                            this.DialogResult = DialogResult.OK;
                            status = j + 1;
                            j++;
                            a += 1;
                        }
                        else
                        {
                            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                            textBox1.Text = "Record NoTest";
                            this.DialogResult = DialogResult.None;
                            if (job == true)
                            {

                            }
                            else
                            {
                                timer2.Interval = 2000;
                                timer2.Start();
                            }
                        }
                    }


                }
                if (a == row1 - 1)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    this.DialogResult = DialogResult.None;
                }
            }
        }

        #endregion


        #region Function 7 互锁+Passed&Failed防错
        private void Function_7()
        {
            if (row1 == 2)
            {
                for (int i = 0; i < row1 - 1; i++)
                {
                    if (dataGridView2.Rows[i].Cells[3].Value.ToString() == "")
                    {
                        this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.LawnGreen; //背景颜色
                        this.DialogResult = DialogResult.OK;
                        status = j + 1;
                        j++;
                    }
                    else if (dataGridView2.Rows[i].Cells[3].Value.ToString() == "Passed")
                    {
                        if (snSubSample >= 999980 && snSubSample <= 999999)
                        {
                            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.LawnGreen; //背景颜色
                            this.DialogResult = DialogResult.OK;
                            status = j + 1;
                            j++;
                        }
                        else
                        {

                            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.LawnGreen; //背景颜色
                            textBox1.Text = " Record Passed ReTest";
                            if (job == true)
                            {

                            }
                            else
                            {
                                timer2.Interval = 2000;
                                timer2.Start();
                            }
                        }

                    }
                    else
                    {
                        if (snSubSample >= 999980 && snSubSample <= 999999)
                        {
                            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                            textBox1.Text = " Record Failed";
                            this.DialogResult = DialogResult.OK;
                            status = j + 1;
                            j++;
                        }
                        else
                        {
                            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                            textBox1.Text = " Record Failed";
                            if (job == true)
                            {

                            }
                            else
                            {
                                timer2.Interval = 2000;
                                timer2.Start();
                            }
                        }

                    }
                }

            }
            else
            {
                for (int i = 0; i < row1 - 1; i++)
                {

                    if (dataGridView2.Rows[i].Cells[3].Value.ToString() == "Passed")
                    {
                        if (i == row1 - 2 && snSubSample < 999980)
                        {
                            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.LawnGreen; //背景颜色
                            textBox1.Text = " Record Passed ReTest";
                            if (job == true)
                            {

                            }
                            else
                            {
                                timer2.Interval = 2000;
                                timer2.Start();
                            }
                        }
                        else
                        {
                            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.LawnGreen; //背景颜色
                            this.DialogResult = DialogResult.OK;
                            status = j + 1;
                            j++;
                            a += 1;
                        }


                    }
                    else if (dataGridView2.Rows[i].Cells[3].Value.ToString() == "Failed")
                    {
                        if (snSubSample >= 999980 && snSubSample <= 999999)
                        {
                            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                            textBox1.Text = " Record Failed";

                            this.DialogResult = DialogResult.OK;
                            status = j + 1;
                            j++;
                            a += 1;
                        }
                        else
                        {
                            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                            textBox1.Text = " Record Failed";
                            this.DialogResult = DialogResult.None;
                            if (job == true)
                            {

                            }
                            else
                            {
                                timer2.Interval = 2000;
                                timer2.Start();
                            }

                        }


                    }
                    else if(dataGridView2.Rows[i].Cells[3].Value.ToString() == "")
                    {
                        if (snSubSample >= 999980 && snSubSample <= 999999)
                        {
                            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                            textBox1.Text = "Record Time Order Wrong";
                            this.DialogResult = DialogResult.OK;
                            status = j + 1;
                            j++;
                            a += 1;
                        }
                        else
                        {
                            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                            textBox1.Text = "Record Time Order Wrong";
                            this.DialogResult = DialogResult.None;
                            if (job == true)
                            {

                            }
                            else
                            {
                                timer2.Interval = 2000;
                                timer2.Start();
                            }
                        }


                    }
                    else
                    {
                        if (snSubSample >= 999980 && snSubSample <= 999999)
                        {
                            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                            textBox1.Text = "Record NoTest";
                            this.DialogResult = DialogResult.OK;
                            status = j + 1;
                            j++;
                            a += 1;
                        }
                        else
                        {
                            this.dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red; //背景颜色
                            textBox1.Text = "Record NoTest";
                            this.DialogResult = DialogResult.None;
                            if (job == true)
                            {

                            }
                            else
                            {
                                timer2.Interval = 2000;
                                timer2.Start();
                            }
                        }

                    }


                }
                if (a == row1 - 1)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    this.DialogResult = DialogResult.None;
                }

            }
        }

        #endregion

    }
}



