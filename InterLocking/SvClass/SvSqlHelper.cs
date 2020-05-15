using System;
using System.ToolKit;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace InterLocking
{

    public class SvSqlHelper
    {
        public SvSqlHelper(string database)
        {
            sql = new SqlHelper("HZHE015A", database, "TE_HVAC", "ped@Hvac");
            ConnectionString = sql.ConnectionString;
        }
        #region 参数      
        private SqlHelper sql;
        private string ConnectionString;
        #endregion


        #region 查询指令字符串
        /// <summary>
        /// UUT_Result表格查询
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="station"></param>
        /// <param name="A2C"></param>
        /// <param name="SN"></param>
        /// <param name="isFailOnly"></param>
        /// <param name="otherCmd"></param>
        /// <returns></returns>
        private static string GetSqlCmd_UUT_Result(string station, string A2C, string SN, int rows, string status, string otherCmd = "")
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT TOP " + rows);
            sb.AppendLine(@"
                         [ID]
                         ,[STATION_ID]
                         ,[BATCH_SERIAL_NUMBER]
                         ,[TEST_SOCKET_INDEX]
                         ,[UUT_SERIAL_NUMBER]
                         ,[USER_LOGIN_NAME]
                         ,[START_DATE_TIME]
                         ,[EXECUTION_TIME]
                         ,[UUT_STATUS]
                         ,[UUT_ERROR_CODE]
                         ,[UUT_ERROR_MESSAGE]
                         FROM UUT_RESULT
                         where 0=0");
            sb.AppendLine(station.IsNotEmpty() ? $"and STATION_ID = '{station}'" : "");
            sb.AppendLine(A2C.IsNotEmpty() ? $"and UUT_SERIAL_NUMBER like '%{A2C}%'" : "");
            sb.AppendLine(SN.IsNotEmpty() ? $"and UUT_SERIAL_NUMBER = '{SN}'" : "");
            sb.AppendLine(status.IsNotEmpty() ? $"and UUT_STATUS = '{status}'" : "");
            sb.AppendLine(otherCmd);
            return sb.ToString();
        }
        private static string GetSqlCmd_Step_Result(string ID)
        {
            return $@"SELECT 
	rtrim(STEP_NAME) as STEP_NAME, 
	isnull(rtrim([NAME]), '') as SubItem,
	isnull(MEAS_NUMERICLIMIT.DATA, STEP_NUMERICLIMIT.DATA) as DATA,
	isnull(rtrim(isnull(MEAS_NUMERICLIMIT.UNITS, STEP_NUMERICLIMIT.UNITS)), '') as UNITS,
	isnull(MEAS_NUMERICLIMIT.LOW_LIMIT, STEP_NUMERICLIMIT.LOW_LIMIT) as LOW_LIMIT,
	isnull(MEAS_NUMERICLIMIT.HIGH_LIMIT, STEP_NUMERICLIMIT.HIGH_LIMIT) as HIGH_LIMIT,
	rtrim(isnull(MEAS_NUMERICLIMIT.STATUS, STEP_RESULT.STATUS)) as STATUS,
	isnull(REPORT_TEXT,STEP_STRINGVALUE.STRING_VALUE) as REPORT_TEXT,
	TOTAL_TIME,
	ERROR_CODE,
	isnull(rtrim(ERROR_MESSAGE), '') as ERROR_MESSAGE
,UUT_RESULT
FROM STEP_RESULT
LEFT JOIN STEP_NUMERICLIMIT
ON
	STEP_RESULT.[ID] = STEP_NUMERICLIMIT.STEP_RESULT
LEFT JOIN MEAS_NUMERICLIMIT
ON
	STEP_RESULT.[ID] = MEAS_NUMERICLIMIT.STEP_RESULT
LEFT JOIN STEP_STRINGVALUE
ON
STEP_RESULT.[ID] = STEP_STRINGVALUE.STEP_RESULT
WHERE
	UUT_RESULT = '{ID}'
ORDER BY ORDER_NUMBER";
        }
        private static string GetSqlCmd_Stations()
        {
            return @"SELECT TOP 10000 [STATION_ID]
      ,[Process_Order]
  FROM [visn_STATIONS]
  order by Process_Order asc";
        }
        private static string GetSqlCmd_A2C()
        {
            return @"SELECT 
distinct left(UUT_SERIAL_NUMBER,12)
from UUT_RESULT
where left(UUT_SERIAL_NUMBER,1) >= '9' 
and substring(UUT_SERIAL_NUMBER,12,1)!=' '
order by left(UUT_SERIAL_NUMBER,12) asc";
        }
        private static string GetSqlCmd_Packing(string A2C = "", string SN = "", int rows = 50, string otherCmd = "")
        {
            string a2cStr = A2C != "" ? $"and left(UUT_SERIAL_NUMBER, 12) = '{A2C}'" : "";
            string snStr = SN != "" ? $"and UUT_SERIAL_NUMBER = '{SN}'" : "";
            return $@"SELECT TOP {rows} [ID]
      ,[SN]
      ,[ProductTime]
      ,[CartonNumber]
  FROM [Packing]
where 0=0 {a2cStr} {snStr} {otherCmd}
order by ProductTime desc";
        }

        private static string GetSqlCmd_IsOperatorIdAdmin(string operatorId)
        {
            return $@"SELECT * FROM [Common_Tools].[dbo].[Failed_ReTest_AdminID]
 where AdminID = '{operatorId}' and Is_Valid='True'
order by Set_Permission_Time desc";
        }
        #endregion

        #region 动态函数:常用查询功能
        public string[] Query_Stations()
        {
            string sqlCmd = GetSqlCmd_Stations();
            return sql.QueryAndSelectColumn(sqlCmd, 0);
        }
        public string[,] Query_UUT_Result(string station, string A2C, string SN, int rows, string status, string otherCmd = "")
        {
            string sqlCmd = GetSqlCmd_UUT_Result(station, A2C, SN, rows, status, otherCmd);
            return sql.QueryTo2DArray(sqlCmd);
        }
        public string[,] Query_Step_Result(string ID, bool FailOnly = false)
        {
            string sqlCmd = GetSqlCmd_Step_Result(ID);
            return sql.QueryTo2DArray(sqlCmd);
        }
        public string[] Query_A2Cs()
        {
            string sqlCmd = GetSqlCmd_A2C();
            return sql.QueryAndSelectColumn(sqlCmd, 0);
        }

        public string[,] Query_Packing(string A2C = "", string SN = "", int rows = 50, string otherCmd = "")
        {
            string sqlCmd = GetSqlCmd_Packing();
            return sql.QueryTo2DArray(sqlCmd); ;
        }
        public bool Query_IsOperatorIdAdmin(string operatorId)
        {
            string sqlCmd = GetSqlCmd_IsOperatorIdAdmin(operatorId);
            return sql.QueryHasRows(sqlCmd);
        }
        #endregion

        #region 静态函数:获取服务器时间,获取服务器中的数据库列表,获取工号权限
        /// <summary>
        /// 获取服务器时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetServerTime()
        {
            DateTime dt = new DateTime();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "server=HZHE015A;database=;uid=TE_HVAC;pwd=ped@Hvac";
            con.Open();
            SqlCommand com = new SqlCommand();
            com.Connection = con;
            com.CommandType = CommandType.Text;
            com.CommandText = "select getdate()";
            SqlDataReader dr = com.ExecuteReader();//执行SQL语句
            if (dr.Read())
            {
                dt = dr.GetDateTime(0);
            }
            dr.Close();//关闭执行
            con.Close();//关闭数据库
            return dt;
        }
        /// <summary>
        /// 获取服务器中的数据库列表
        /// </summary>
        /// <returns></returns>
        public static string[] GetDatabases()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "server=HZHE015A;database=;uid=TE_HVAC;pwd=ped@Hvac";
            SqlConnection sqlcon = new SqlConnection(con.ConnectionString);//实例化数据库连接对象
            SqlDataAdapter da = new SqlDataAdapter("select name from sysdatabases ", sqlcon);
            DataTable dt = new DataTable("sysdatabases");//实例化DataTable对象
            da.Fill(dt);//填充DataTable数据表
            con.Close();//关闭数据库
            return dt.To2DArray().IndexColumn(0);
        }

        public static bool IsAdminID(string userId)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "server=HZHE015A;database=Common_Tools;uid=TE_HVAC;pwd=ped@Hvac";
            SqlConnection sqlcon = new SqlConnection(con.ConnectionString);//实例化数据库连接对象
            SqlDataAdapter da = new SqlDataAdapter($"select * from Failed_ReTest_AdminID where AdminID='{userId}'", sqlcon);
            DataTable dt = new DataTable("sysdatabases");//实例化DataTable对象
            da.Fill(dt);//填充DataTable数据表
            con.Close();//关闭数据库
            return dt.Rows.Count > 0;
        }


        #endregion


    }

}
