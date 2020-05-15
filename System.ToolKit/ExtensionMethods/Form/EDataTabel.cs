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
using System.Collections;
using System.Reflection;

namespace System.ToolKit
{
    public static partial class Extensions
    {
        #region ToExcel
        /// <summary>
        /// DataTable转换为Excel，支持xls，不支持xlsx
        /// </summary>
        /// <param name="dtSource"></param>
        /// <param name="strPath"></param>
        /// <param name="strSheetName"></param>
        public static void ToExcel(this DataTable dtSource, string strPath, string strSheetName= "Sheet1")
        {
            //http://flyspirit99.blogspot.com/2007/07/export-more-than-255-characters-into.html-
            System.Data.OleDb.OleDbConnection OleDb_Conn = new System.Data.OleDb.OleDbConnection();
            OleDb_Conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties='Excel 8.0;HDR=No';" + "Data Source=\"" + strPath + "\"";
            try
            {
                OleDb_Conn.Open();
                System.Data.OleDb.OleDbCommand OleDb_Comm = new System.Data.OleDb.OleDbCommand();
                OleDb_Comm.Connection = OleDb_Conn;
                string strCmd;
                try
                {
                    strCmd = "drop table [" + strSheetName + "]";
                    OleDb_Comm.CommandText = strCmd;
                    OleDb_Comm.ExecuteNonQuery();
                }
                catch
                {

                }
                strCmd = "create Table [" + strSheetName + "]("; foreach (DataColumn dc in dtSource.Columns)
                {
                    strCmd += "[" + dc.ColumnName + "] memo,";
                }
                strCmd = strCmd.Trim().Substring(0, strCmd.Length - 1);
                strCmd += ")";
                OleDb_Comm.CommandText = strCmd;
                OleDb_Comm.ExecuteNonQuery();
                foreach (DataRow dr in dtSource.Rows)
                {
                    if (dr.RowState != System.Data.DataRowState.Deleted)
                    {
                        strCmd = "insert into [" + strSheetName + "] values(";
                        foreach (DataColumn dc in dtSource.Columns)
                        {

                            strCmd += "'" + dr[dc.ColumnName].ToString().Trim().Replace("'", "") + "',";
                        }
                        strCmd = strCmd.Substring(0, strCmd.Length - 1);
                        strCmd += ")"; OleDb_Comm.CommandText = strCmd;
                        OleDb_Comm.ExecuteNonQuery();
                    }
                }
                OleDb_Conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                OleDb_Conn.Close();
            }
        }
        #endregion
        #region DataTable与List的转换
        public static DataTable ToDataTable<T>(this IEnumerable<T> collection)
        {
            var props = typeof(T).GetProperties();
            var dt = new DataTable();
            dt.Columns.AddRange(props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());
            if (collection.Count() > 0)
            {
                for (int i = 0; i < collection.Count(); i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in props)
                    {
                        object obj = pi.GetValue(collection.ElementAt(i), null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    dt.LoadDataRow(array, true);
                }
            }
            return dt;
        }
        #endregion
        #region To2DArray
        public static string[,] To2DArray(this DataTable dtSource)
        {
            string[,] data = new string[dtSource.Rows.Count, dtSource.Columns.Count];
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    data[i, j] = dtSource.Rows[i][j].ToString();
                }
            }
            return data;
        }
        #endregion
    }
}
