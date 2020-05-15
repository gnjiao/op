using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.ToolKit;
using System.Windows.Forms;

namespace InterLocking
{
    class SqlHelper
    {
      
            #region 构造函数
            /// <summary>
            /// 构造函数,生成数据库连接字符串并进行数据库连接(即不需要发送OpenConnection方法).如果账号密码为空,则使用Windows身份验证连接SQL数据库,否则使用SQL Server身份验证连接SQL数据库
            /// </summary>
            /// <param name="server"></param>
            /// <param name="database"></param>
            /// <param name="uid"></param>
            /// <param name="pwd"></param>
            public SqlHelper(string server, string database, string uid = "", string pwd = "")
            {
                Server = server;
                Database = database;
                UID = uid;
                PWD = pwd;
                ConnectionString = GetConnectionString(server, database, uid, pwd);
                OpenConnection();
            }
            #endregion

            #region 参数
            private string Server { get; set; }
            private string Database { get; set; }
            private string UID { get; set; }
            private string PWD { get; set; }

            public string ConnectionString { get; set; }
            private SqlConnection conn;
            #endregion

            #region 私有函数:获取数据库连接字符串
            /// <summary>
            /// 获取数据库连接字符串,根据用户和密码是否为空来判断选择哪种方式登录
            /// </summary>
            /// <param name="server"></param>
            /// <param name="database"></param>
            /// <param name="uid"></param>
            /// <param name="pwd"></param>
            /// <returns></returns>
            private string GetConnectionString(string server, string database, string uid = "", string pwd = "")
            {
                if (uid == "" && pwd == "")
                {
                    return GetConnectionString_Windows(server, database);
                }
                else
                {
                    return GetConnectionString_SqlServer(server, database, uid, pwd);
                }
            }
            /// <summary>
            /// 连接成功返回连接字符串,连接失败返回空字符串
            /// </summary>
            /// <param name="server">服务器名称,可以省略"\SQLEXPRESS"</param>
            /// <param name="database"></param>
            /// <param name="pwd"></param>
            /// <returns></returns>
            private string GetConnectionString_Windows(string server, string database)
            {
                server = server.Contains(@"\SQLEXPRESS") ? server : server + @"\SQLEXPRESS";
                string str_con = $"Data Source={server};Initial Catalog ={database};Integrated Security=SSPI;";//使用Windows身份验证连接SQL数据库

                using (SqlConnection connection = new SqlConnection(str_con))
                {
                    connection.Open();
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        return str_con;
                    }
                }
                return "";
            }
            /// <summary>
            /// 连接成功返回连接字符串,连接失败返回空字符串
            /// </summary>
            /// <param name="server">服务器名称</param>
            /// <param name="database"></param>
            /// <param name="uid"></param>
            /// <param name="pwd"></param>
            /// <returns></returns>
            private string GetConnectionString_SqlServer(string server, string database, string uid, string pwd)
            {

                string str_con = $"Data Source={server};Database={database};Uid={uid};Pwd={pwd};";//使用SQL Server身份验证连接SQL数据库

                using (SqlConnection connection = new SqlConnection(str_con))
                {
                    connection.Open();
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        return str_con;
                    }
                }
                return "";
            }
            #endregion

            #region 打开和关闭数据库,返回是否操作成功:OpenConnection,CloseConnection
            /// <summary>
            /// 打开数据库连接
            /// </summary>
            /// <returns></returns>
            public bool OpenConnection()
            {
                bool result = true;

                conn = new SqlConnection(ConnectionString);
                try
                {
                    conn.Open();
                    result = conn.State == ConnectionState.Open;
                }
                catch (Exception)
                {
                    result = false;
                    throw;
                }
                return result;
            }
            /// <summary>
            /// 关闭数据库连接
            /// </summary>
            /// <returns></returns>
            public bool CloseConnection()
            {
                bool result = true;

                conn = new SqlConnection(ConnectionString);
                try
                {
                    conn.Close();
                    result = conn.State == ConnectionState.Closed;
                }
                catch (Exception)
                {
                    result = false;
                    throw;
                }
                return result;
            }
            #endregion

            //查询功能
            #region 小范围查询或判断:判断是否存在结果,获取索引为[0,0]的内容:QueryHasRows,ExecuteScalar
            /// <summary>
            /// 判断查询结果是否存在(即行数>=1)
            /// </summary>
            /// <param name="sqlCmd"></param>
            /// <returns></returns>
            public bool QueryHasRows(string sqlCmd)
            {
                SqlDataReader dr = QueryToDataReader(sqlCmd);
                dr.Read();
                bool isHasRows = dr.HasRows;
                dr.Close();
                return isHasRows;
            }
            /// <summary>
            /// 执行SQL语句,返回结果集中的第一行的第一列
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="sqlCmd"></param>
            /// <returns></returns>
            public T ExecuteScalar<T>(string sqlCmd)
            {
                SqlCommand cmd = new SqlCommand(sqlCmd, conn);//创建命令对象
                return (T)cmd.ExecuteScalar();//执行SQL命令
            }

            #endregion
            #region 查询行或列:QueryAndSelectRow,QueryAndSelectColumn,QueryAndBindControl
            /// <summary>
            /// 执行SQL语句,并选择返回结果集中的指定行
            /// </summary>
            /// <param name="sqlCmd"></param>
            /// <returns></returns>
            public string[] QueryAndSelectRow(string sqlCmd, int index = 0)
            {
                return QueryTo2DArray(sqlCmd).IndexRow(index);
            }
            /// <summary>
            /// 执行SQL语句,并选择返回结果集中的指定列
            /// </summary>
            /// <param name="sqlCmd"></param>
            /// <param name="columnIndex"></param>
            /// <returns></returns>
            public string[] QueryAndSelectColumn(string sqlCmd, int columnIndex)
            {
                return QueryTo2DArray(sqlCmd).IndexColumn(columnIndex);
            }
            /// <summary>
            /// 执行SQL语句,并选择返回结果集中的指定列
            /// </summary>
            /// <param name="sqlCmd"></param>
            /// <param name="columnName"></param>
            /// <returns></returns>
            public string[] QueryAndSelectColumn(string sqlCmd, string columnName)
            {
                DataTable dt = QueryToDataSet(sqlCmd).Tables[0];
                string[] data = new string[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    data[i] = dt.Rows[i][columnName].ToString();
                }
                return data;
            }

            /// <summary>
            /// 执行SQL语句,并将返回结果集绑定到控件,支持ComboBox,ListBox,ListView,DataGridView(1D数组绑定第一列,2D数组全部绑定)
            /// </summary>
            /// <param name="sqlCmd"></param>
            /// <param name="obj"></param>
            public void QueryAndBindControl(string sqlCmd, Control obj)
            {
                string[,] data = QueryTo2DArray(sqlCmd);
                if (obj.GetType().ToString() == "System.Windows.Forms.ComboBox")
                {
                    ComboBox cmb = (ComboBox)obj;
                    cmb.Items.AddRange(data.IndexColumn(0));
                }
                else if (obj.GetType().ToString() == "System.Windows.Forms.ListBox")
                {
                    ListBox lst = (ListBox)obj;
                    lst.Items.AddRange(data.IndexColumn(0));
                }
                else if (obj.GetType().ToString() == "System.Windows.Forms.ListView")
                {
                    ListView lsv = (ListView)obj;
                    lsv.From2DArray(data, true);
                }
                else if (obj.GetType().ToString() == "System.Windows.Forms.DataGridView")
                {
                    DataGridView dgv = (DataGridView)obj;
                    SqlDataAdapter da = new SqlDataAdapter(sqlCmd, conn);//创建数据适配器对象
                    DataSet ds = new DataSet();//创建数据集对象
                    da.Fill(ds, "table");//填充数据集
                    dgv.DataSource = ds;//绑定到数据表
                    dgv.DataMember = "table";
                    ds.Dispose();//释放资源
                }
            }
            #endregion
            #region 数据库查询:ExecuteNonQuery,QueryToDataReader,QueryToDataSet,QueryToDataGridView,QueryTo2DArray
            /// <summary>
            /// 执行SQL语句,返回受影响的行数
            /// </summary>
            /// <param name="sqlCmd"></param>
            /// <returns></returns>
            public int ExecuteNonQuery(string sqlCmd)
            {
                SqlCommand comm = new SqlCommand(sqlCmd, conn);
                int rowNum = (int)comm.ExecuteNonQuery();//执行SQL命令
                return rowNum;
            }
            /// <summary>
            /// 执行SQL语句,并返回SqlDataReader结果
            /// </summary>
            /// <param name="sqlCmd"></param>
            /// <returns></returns>
            public SqlDataReader QueryToDataReader(string sqlCmd)
            {
                SqlCommand comm = new SqlCommand(sqlCmd, conn);
                SqlDataReader read = comm.ExecuteReader();
                return read;
            }
            /// <summary>
            /// 执行SQL语句,并返回DataSet结果
            /// </summary>
            /// <param name="sqlCmd"></param>
            /// <param name="srcTable"></param>
            /// <returns></returns>
            public DataSet QueryToDataSet(string sqlCmd, string srcTable = "table")
            {
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd, conn);//创建数据适配器对象
                DataSet ds = new DataSet();//创建数据集
                da.Fill(ds, srcTable);//填充数据集
                ds.Dispose();//释放资源
                return ds;//返回数据集
            }
            /// <summary>
            /// 执行SQL语句,并返回2D字符串数组结果
            /// </summary>
            /// <param name="sqlCmd"></param>
            /// <returns></returns>
            public string[,] QueryTo2DArray(string sqlCmd)
            {
                return QueryToDataSet(sqlCmd).Tables[0].To2DArray();
            }
            public void QueryToDataGridView(DataGridView dgv, string sqlCmd, string srcTable = "table")
            {
                SqlDataAdapter da = new SqlDataAdapter(sqlCmd, conn);//创建数据适配器对象
                DataSet ds = new DataSet();//创建数据集对象
                da.Fill(ds, srcTable);//填充数据集
                dgv.DataSource = ds;//绑定到数据表
                dgv.DataMember = srcTable;
                ds.Dispose();//释放资源
            }
            #endregion


            #region 数据库备份与恢复:BackupDatabase,RestoreDatabase
            /// <summary>
            /// 备份数据库
            /// </summary>
            /// <param name="bakPath"></param>
            public void BackupDatabase(string bakPath)
            {
                string sqltxt =//设置SQL字符串
                        $@"BACKUP DATABASE {Database} TO Disk='{bakPath}'";
                if (File.Exists(bakPath))//判断文件是否存在
                {
                    File.Delete(bakPath);//删除文件
                }
                ExecuteNonQuery(sqltxt);
            }
            /// <summary>
            /// 还原数据库(似乎有问题)
            /// </summary>
            /// <param name="bakPath"></param>
            public void RestoreDatabase(string bakPath)
            {
                string sqltxt = $"use master restore database {Database} from Disk='{bakPath}'";
                ExecuteNonQuery(sqltxt);
            }
            #endregion

            #region 静态方法:GetServers,GetDatabases
            #region 获取服务器名:GetServers,GetDatabases
            /// <summary>
            /// 获取局域网中服务器名
            /// </summary>
            /// <returns></returns>
            public static string[] GetServers()
            {
                System.Data.Sql.SqlDataSourceEnumerator instance = System.Data.Sql.SqlDataSourceEnumerator.Instance;
                System.Data.DataTable table = instance.GetDataSources();
                string[] servers = new string[table.Rows.Count];
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    System.Data.DataRow row = table.Rows[i];
                    servers[i] = row["ServerName"].ToString();
                }
                return servers;
            }
            #endregion

            #endregion
        }
    
}
