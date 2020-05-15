using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data;
using System.IO;

namespace System.ToolKit
{
    public static partial class Extensions
    {
        /// <summary>
        /// 导出到Excel,如果文件不存在,会自动生成
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="excelPath"></param>
        public static void ToExcel(this DataGridView dgv, string excelPath)
        {
            FileStream fsFile = new FileStream(excelPath, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fsFile, System.Text.Encoding.GetEncoding(-0));

            string strHeaderText = "";

            try
            {
                //写标题
                for (int i = 0; i < dgv.ColumnCount; i++)
                {
                    if (i > 0)
                    {
                        strHeaderText += "\t";
                    }

                    strHeaderText += dgv.Columns[i].HeaderText;
                }

                sw.WriteLine(strHeaderText);

                //写内容
                string strItemValue = "";

                for (int j = 0; j < dgv.RowCount; j++)
                {
                    strItemValue = "";

                    for (int k = 0; k < dgv.ColumnCount; k++)
                    {
                        if (k > 0)
                        {
                            strItemValue += "\t";
                        }

                        strItemValue += dgv.Rows[j].Cells[k].Value.ToString();
                    }

                    sw.WriteLine(strItemValue); //把dgv的每一行的信息写为sw的每一行
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "软件提示");
                throw ex;
            }
            finally
            {
                sw.Close();
                fsFile.Close();
            }
        }
        public static void FromExcel(this DataGridView dgv, string excelPath)
        {
            try
            {
                string strCon = "provider=microsoft.jet.oledb.4.0;data source=" + excelPath + ";extended properties=excel 8.0";//关键是红色区域
                OleDbConnection Con = new OleDbConnection(strCon);//建立连接
                string strSql = "select * from [Sheet1$]";//表名的写法也应注意不同，对应的excel表为sheet1，在这里要在其后加美元符号$，并用中括号
                OleDbCommand Cmd = new OleDbCommand(strSql, Con);//建立要执行的命令
                OleDbDataAdapter da = new OleDbDataAdapter(Cmd);//建立数据适配器
                DataSet ds = new DataSet();//新建数据集
                da.Fill(ds, "shyman");//把数据适配器中的数据读到数据集中的一个表中（此处表名为shyman，可以任取表名）
                                      //指定dgv的数据源为数据集ds的第一张表（也就是shyman表），也可以写ds.Table["shyman"]

                dgv.DataSource = ds.Tables[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);//捕捉异常
            }
        }

        public static DataTable ImportExcel(string path)
        {
            DataSet ds = new DataSet();
            string strConn = "";
            if (Path.GetExtension(path) == ".xls")
            {
                strConn = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;", path);
            }
            else
            {
                strConn = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0;", path);
            }
            using (var oledbConn = new OleDbConnection(strConn))
            {
                oledbConn.Open();
                var sheetName = oledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new[] { null, null, null, "Table" });
                var sheet = new string[sheetName.Rows.Count];
                for (int i = 0, j = sheetName.Rows.Count; i < j; i++)
                {
                    sheet[i] = sheetName.Rows[i]["TABLE_NAME"].ToString();
                }
                var adapter = new OleDbDataAdapter(string.Format("select * from [{0}]", sheet[0]), oledbConn);
                adapter.Fill(ds);
            }
            return ds.Tables[0];
        }
        ///// <summary>
        ///// 导出时报错
        ///// </summary>
        ///// <param name="dgv"></param>
        ///// <param name="fileName"></param>
        //public static void ExportExcel(this DataGridView dgv, string fileName)
        //{
        //    if (dgv.Rows.Count > 0)
        //    {

        //        string saveFileName = "";
        //        //bool fileSaved = false;  
        //        SaveFileDialog saveDialog = new SaveFileDialog();
        //        saveDialog.DefaultExt = "xls";
        //        saveDialog.Filter = "Excel文件|*.xls";
        //        saveDialog.FileName = fileName;
        //        saveDialog.ShowDialog();
        //        saveFileName = saveDialog.FileName;
        //        if (saveFileName.IndexOf(":") < 0) return; //被点了取消   
        //        Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
        //        if (xlApp == null)
        //        {
        //            MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
        //            return;
        //        }

        //        Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
        //        Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
        //        Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1  

        //        //写入标题  
        //        for (int i = 0; i < dgv.ColumnCount; i++)
        //        {
        //            worksheet.Cells[1, i + 1] = dgv.Columns[i].HeaderText;
        //        }
        //        //写入数值  
        //        for (int r = 0; r < dgv.Rows.Count; r++)
        //        {
        //            for (int i = 0; i < dgv.ColumnCount; i++)
        //            {
        //                worksheet.Cells[r + 2, i + 1] = dgv.Rows[r].Cells[i].Value;
        //            }
        //            System.Windows.Forms.Application.DoEvents();
        //        }
        //        worksheet.Columns.EntireColumn.AutoFit();//列宽自适应  
        //                                                 //   if (Microsoft.Office.Interop.cmbxType.Text != "Notification")  
        //                                                 //   {  
        //                                                 //       Excel.Range rg = worksheet.get_Range(worksheet.Cells[2, 2], worksheet.Cells[ds.Tables[0].Rows.Count + 1, 2]);  
        //                                                 //      rg.NumberFormat = "00000000";  
        //                                                 //   }  

        //        if (saveFileName != "")
        //        {
        //            try
        //            {
        //                workbook.Saved = true;
        //                workbook.SaveCopyAs(saveFileName);
        //                //fileSaved = true;  
        //            }
        //            catch (Exception ex)
        //            {
        //                //fileSaved = false;  
        //                MessageBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
        //            }

        //        }
        //        //else  
        //        //{  
        //        //    fileSaved = false;  
        //        //}  
        //        xlApp.Quit();
        //        GC.Collect();//强行销毁   
        //                     // if (fileSaved && System.IO.File.Exists(saveFileName)) System.Diagnostics.Process.Start(saveFileName); //打开EXCEL  
        //        MessageBox.Show("导出文件成功", "提示", MessageBoxButtons.OK);
        //    }
        //    else
        //    {
        //        MessageBox.Show("报表为空,无表格需要导出", "提示", MessageBoxButtons.OK);
        //    }

        //}
        #region From2DArray
        public static void From2DArray(this DataGridView dgv, string[,] table)
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < table.GetLength(1); i++)
                dt.Columns.Add($"{i}", typeof(string));
            for (int i = 0; i < table.GetLength(0); i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < table.GetLength(1); j++)
                    dr[j] = table[i, j];
                dt.Rows.Add(dr);
            }
            dgv.DataSource = dt;
        }
        public static void From2DArray(this DataGridView dgv,string[,] table,string[] colNames)
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < table.GetLength(1); i++)
                dt.Columns.Add(colNames[i], typeof(string));
            for (int i = 0; i < table.GetLength(0); i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < table.GetLength(1); j++)
                    dr[j] = table[i, j];
                dt.Rows.Add(dr);
            }
            dgv.DataSource = dt;
        }
        #endregion
        #region 将dataGridView数据转成DataTable
        ///// <summary>
        ///// 将dataGridView数据转成DataTable:已绑定过数据源
        ///// </summary>
        ///// <param name="dgv"></param>
        ///// <returns></returns>
        //public static DataTable ToDataTable(this DataGridView dgv)
        //{
        //    return (dgv.DataSource as DataTable);
        //}
        /// <summary>
        /// 将dataGridView数据转成DataTable:未绑定过数据源
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
        public static DataTable ToDataTable(this DataGridView dgv)
        {
            DataTable dt = new DataTable();

            // 列强制转换
            for (int count = 0; count < dgv.Columns.Count; count++)
            {
                DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                dt.Columns.Add(dc);
            }

            // 循环行
            for (int count = 0; count < dgv.Rows.Count; count++)
            {
                DataRow dr = dt.NewRow();
                for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                {
                    dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        #endregion

        #region 设置奇偶行颜色
        public static void SetOddAndEvenRowBackColor(this DataGridView dgv, Color oddRowColor, Color evenRowColor)
        {
            foreach (DataGridViewRow dgvRow in dgv.Rows)//遍历所有行
            {
                if (dgvRow.Index % 2 == 0)//判断是否是偶数行
                {
                    //设置偶数行颜色
                    dgv.Rows[dgvRow.Index].DefaultCellStyle.BackColor = evenRowColor;
                }
                else//奇数行
                {
                    //设置奇数行颜色
                    dgv.Rows[dgvRow.Index].DefaultCellStyle.BackColor = oddRowColor;
                }
            }
        }
        #endregion
        #region 清空DataGridView
        /// <summary>
        /// 清空DataGridView
        /// </summary>
        /// <param name="dgv"></param>
        public static void DataGridViewReset(this DataGridView dgv)
        {
            if (dgv.DataSource != null)
            {
                //若DataGridView绑定的数据源为DataTable
                if (dgv.DataSource.GetType() == typeof(DataTable))
                {
                    DataTable dt = dgv.DataSource as DataTable;
                    dt.Clear();
                }

                //若DataGridView绑定的数据源为BindingSource
                if (dgv.DataSource.GetType() == typeof(BindingSource))
                {
                    BindingSource bs = dgv.DataSource as BindingSource;
                    DataTable dt = bs.DataSource as DataTable;
                    dt.Clear();
                }
            }
        }
        #endregion
        /// <summary>
        /// 在DataGridView控件的指定位置插入行
        /// </summary>
        /// <param name="dgv">DataGridView控件</param>
        /// <param name="bs">BindingSource组件</param>
        /// <param name="dt">DataTable内存数据表</param>
        /// <param name="intPosIndex">指定位置的索引值</param>
        /// <returns>DataGridViewRow对象的引用</returns>
        public static DataGridViewRow DataGridViewInsertRow(this DataGridView dgv, BindingSource bs, DataTable dt, int intPosIndex)
        {
            DataGridViewRow dgvr = null;

            try
            {
                DataRow dr = dt.NewRow(); //基于某个DataTable的结构( 列结构仍然使用初始时产生的结构(如：sda.Fill(dt)) )，创建一个DataRow对象
                dt.Rows.InsertAt(dr, intPosIndex); //在数据源中插入新创建的DataRow对象
                bs.DataSource = dt;
                dgv.DataSource = bs;
                dgvr = dgv.Rows[intPosIndex];
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dgvr;
        }

        /// <summary>
        /// 在DataGridView控件的末尾添加行
        /// </summary>
        /// <param name="dgv">DataGridView控件</param>
        /// <param name="bs">BingdingSource组件</param>
        /// <param name="dt">DataTable内存数据表</param>
        /// <returns>DataGridViewRow对象的引用</returns>
        public static DataGridViewRow DataGridViewInsertRowAtEnd(DataGridView dgv, BindingSource bs, DataTable dt)
        {
            DataGridViewRow dgvr = null;

            try
            {
                DataRow dr = dt.NewRow();
                dt.Rows.Add(dr); //在结尾添加数据行对象
                bs.DataSource = dt;
                dgv.DataSource = bs;
                dgvr = dgv.Rows[dgv.RowCount - 1];
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dgvr;
        }
    }
}
