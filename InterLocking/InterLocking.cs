using System;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Data;
using LogHeper;
using MES_Interlock_DLL_EDCMESASS_64;
using System.ToolKit;
using System.IO;
using System.Text;




namespace InterLocking
{

    /// <summary>
    /// 用于生产报告、互锁
    /// </summary>
    public class Locking
    {

        static object obj = new object();

        public ME_MSTR[] Arr_ME_MSTR { get; set; }

        public Locking()
        {
        }

        #region 私有方法
        private bool CompareData(string comp, double data, double low, double high, ref string errorcode, ref string errorMessage)
        {
            if (comp.Equals(">=<="))
            {
                if ((data >= low) && (data <= high))
                {
                    //LogHelper.Info("NumericTest比较数据成功");
                    errorcode = "0";
                    errorMessage = "";
                    return true;

                }
                else if (data > high)
                {
                    errorcode = "1";
                    errorMessage = "Out of Upper Limit";
                    //LogHelper.Info("NumericTest比较数据超出上限");
                    return false;
                }
                else
                {
                    errorcode = "1";
                    errorMessage = "Out of Lower Limit";
                    //LogHelper.Info("NumericTest比较数据超出下限");
                    return false;
                }
            }
            else
            {
                errorcode = "2";
                errorMessage = "Check NumericValue Failed";
                LogHeper.LogHelper.Info("NumericTest比较数据失败");
                return false;
            }
        }
        #endregion

        #region 添加数据
        /// <summary>
        /// 添加数据,注意:stepOrder起始值为0,对应第一个测试项
        /// </summary>
        /// <param name="stepOrder"></param>
        /// <param name="limitLow"></param>
        /// <param name="limitHigh"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool add_ME_MSTR_NumericTest(ref int stepOrder, double limitLow, double limitHigh, double data)
        {
            ME_MSTR me = Arr_ME_MSTR[stepOrder];
            me.limitLow = limitLow.ToString();
            me.limitHigh = limitHigh.ToString();
            me.LimitStep_Data = data.ToString();
            bool result = CompareData(me.Comp, data, limitLow, limitHigh, ref me.ErrorCode, ref me.ErrorMessage);
            me.Step_Status = result ? "Passed" : "Failed";
            Arr_ME_MSTR[stepOrder] = me;
            return me.ErrorCode == "0";
        }
        /// <summary>
        ///新增Record类型，测试状态为Done，记录结果显示在ReportText中（实质为StringValueTest类型，只是取消字符串比较功能） 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stepOrder"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool add_ME_MSTR_Record<T>(ref int stepOrder, T data)
        {
            ME_MSTR me = Arr_ME_MSTR[stepOrder];
            me.StepType = "StringValueTest";
            me.Step_Status = "Done";
            me.ReportText = data.ToString();
            me.ErrorCode = "0";
            me.ErrorMessage = "";
            Arr_ME_MSTR[stepOrder] = me;
            return me.ErrorCode == "0";
        }
        /// <summary>
        /// 添加一项PassFail参数到ME_MSTR表中
        /// </summary>
        /// <param name="stepOrder">参数在工位采集数据中的序号，按此序号查找存放的位置</param>
        /// <param name="errorCode">错误代码，如果没有出错，则设为0</param>
        /// <param name="errorMessage">如果errorCode不为0，则输入错误信息；如果errorCode为0,则为空</param>
        /// <param name="startOfTime">此参数所在动作开始工作的时间,格式yymmddHHMMSS</param>
        /// <param name="toalTime">采集参数的动作所花的时间,单位ms</param>
        /// <param name="value">值，一般为Passed或Failed</param>
        /// <returns>添加成功则返true,否则返回false</returns>
        public bool add_ME_MSTR_PassFailTest(int stepOrder, int errorCode, string errorMessage, string startOfTime, int toalTime, string value)
        {
            if (Arr_ME_MSTR == null)
            {
                LogHelper.Info("未初始化对象,添加PassFailTest项失败!");
                return false;
            }
            try
            {
                ME_MSTR me = Arr_ME_MSTR[stepOrder];
                if (errorCode == 0)
                {
                    if (value.ToUpper().Trim().Equals("FAILED") || (value.ToUpper().Trim().Equals("FALSE")))
                    {
                        me.ErrorCode = "1";
                        value = "Failed";
                    }
                    else
                    {
                        me.ErrorCode = "0";
                        value = "Passed";
                    }
                    me.ErrorMessage = "";
                    me.Step_PassFail = value;
                    me.Step_Status = value;
                }
                else
                {
                    me.ErrorCode = errorCode.ToString();
                    me.ErrorMessage = errorMessage;
                    me.Step_PassFail = value;
                    me.Step_Status = "Error";
                }
                me.ProductionTime = startOfTime;
                me.TotalTime = toalTime.ToString();
                Arr_ME_MSTR[stepOrder] = me;
                LogHelper.Info("添加PasseFailTest项成功");
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Debug("添加PasseFailTest项异常", ex);
                return false;
            }

        }
        /// <summary>
        /// 添加一项字符串参数到ME_MSTR表中
        /// </summary>
        /// <param name="stepOrder">参数在工位采集数据中的序号，按此序号查找存放的位置</param>
        /// <param name="errorCode">错误代码，如果没有出错，则设为0</param>
        /// <param name="errorMessage">如果errorCode不为0，则输入错误信息；如果errorCode为0,则为空</param>
        /// <param name="startOfTime">此参数所在动作开始工作的时间,格式yymmddHHMMSS</param>
        /// <param name="toalTime">采集参数的动作所花的时间,单位ms</param>
        /// <param name="value">要比较的字符串的值</param>
        /// <returns>添加成功则返true,否则返回false</returns>
        public bool add_ME_MSTR_StringValueTest(ref int stepOrder, string limitString, string value)
        {
            ME_MSTR me = Arr_ME_MSTR[stepOrder];

            me.limits_String = limitString.Trim();
            me.ReportText = value.ToString();
            bool isEquals = me.ReportText.Equals(me.limits_String, StringComparison.CurrentCultureIgnoreCase);
            if (isEquals)
            {
                me.Step_Status = "Passed";
                me.ErrorCode = "0";
                me.ErrorMessage = "";
            }
            else
            {
                me.Step_Status = "Failed";
                me.ErrorMessage = "String Compare failed";
                me.ErrorCode = "0";
            }
            Arr_ME_MSTR[stepOrder] = me;
           
            return isEquals;
        }
        #endregion

        /// <summary>
        /// 生产报告
        /// </summary>        
        public void SendTestDataToME_MSTR()
        {           
            Arr_ME_MSTR = new ME_MSTR[interLockingParam.Instance.InterLockingListParam.Count];
            for (int i = 0; i < Arr_ME_MSTR.Length; i++)
            {
                Arr_ME_MSTR[i] = new ME_MSTR();
                switch (interLockingParam.Instance.InterLockingListParam[i].Istype)
                {
                    case 0:
                        add_ME_MSTR_Record(ref i, interLockingParam.Instance.InterLockingListParam[i].StringValue);
                        break;
                    case 1:
                        add_ME_MSTR_NumericTest(ref i, interLockingParam.Instance.InterLockingListParam[i].MinLimits, interLockingParam.Instance.InterLockingListParam[i].MaxLimits, interLockingParam.Instance.InterLockingListParam[i].Value);
                        break;
                    case 2:
                        add_ME_MSTR_StringValueTest(ref i, interLockingParam.Instance.InterLockingListParam[i].limitString, interLockingParam.Instance.InterLockingListParam[i].StringValue);
                        break;
                    case 3:
                        add_ME_MSTR_PassFailTest(i, interLockingParam.Instance.InterLockingListParam[i].errorCode, interLockingParam.Instance.InterLockingListParam[i].errorMessage, interLockingParam.Instance.InterLockingListParam[i].startOfTime, interLockingParam.Instance.InterLockingListParam[i].toalTime, interLockingParam.Instance.InterLockingListParam[i].StringValue);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 生成报告
        /// </summary>
        /// <param name="TestStatus">"Pass" or "Fail"</param>     
        /// <param name="productSn">产品SN</param>     
        /// <returns></returns>
        public bool Test_WriteMesTxtAndCsvFile()
        {

            lock (obj)
            {
                //System.ToolKit.LogHelper.Info("写入MES文件和CSV文件");
                bool result = false;
                try
                {
                    SendTestDataToME_MSTR();
                    bool isWriteMesTxtSucceed = WriteMesTxtToFile($"{interLockingParam.Instance.mesTxtPath}{interLockingParam.Instance.EvData.DeviceA2C}_{interLockingParam.Instance.EvData.SerialNumber}_{DateTime.Now.ToString("yyMMddHHmmss")}_{interLockingParam.Instance.EvData.TestStatus}.txt", interLockingParam.Instance.EvData);
                    bool isWriteCsvSucceed = AppendTestValuesRoCsvFile(interLockingParam.Instance.EvData, $"{interLockingParam.Instance.csvFilePath}{interLockingParam.Instance.EvData.ModelName.Replace("/", "-")}_Camera Test_{DateTime.Now.ToString("yyMMdd")}.csv");
                    LogHelper.Info(interLockingParam.Instance.EvData.SerialNumber + ",报告写入ok:");
                    result = isWriteCsvSucceed && isWriteMesTxtSucceed;
                }
                catch (Exception ex)
                {
                    LogHelper.Info(interLockingParam.Instance.EvData.SerialNumber + ",报告写入失败:" + ex.ToString());

                }
                return result;
            }


        }

        #region 生成MesTxt数据库采集文本和测试数据CSV文本       
        /// <summary>
        /// 将Mes数据写入到文件
        /// </summary>
        /// <param name="mesTxtPath">文件的路径</param>
        /// <returns>如果写入成功返回true,否则返回false</returns>
        public bool WriteMesTxtToFile(string mesTxtPath, EV_MSTR EvData)
        {
            try
            {
                if ((EvData == null) || (Arr_ME_MSTR == null))
                {
                    LogHelper.Info("成员对象为空,生成Mes数据失败");
                    return false;
                }
                bool result = true;
                foreach (ME_MSTR me in Arr_ME_MSTR)//所有步骤的状态为Pass或Done，则最终结果为Pass
                {
                    result = result && me.Step_Status.In("Passed", "Done");
                }
                EvData.TestStatus = result ? "Passed" : "Failed";

                StringBuilder strResult = new StringBuilder();
                strResult.AppendLine("UUT_Order|UUT_SOURCE|DeviceA2C|EquipmentFunction|SerialNumber|StationName||||ProductTime|TestStandName|LoginName|ExecutionTime|TestScoket|BatchSerialNumber|Error Code|Error Message||||||||||||||TestStatus|");
                strResult.AppendLine($"{EvData.UUT_Order}|{EvData.UUT_SOURCE}|{EvData.DeviceA2C}|{EvData.EquipmentFunction}|{EvData.SerialNumber}|{EvData.StationName}||||{EvData.ProductTime}|{EvData.TestStandName}|{EvData.LoginName}|{EvData.ExecutionTime}|{EvData.TestSocket}|{EvData.BatchSerialNumber}||||||||||||||||{EvData.TestStatus}|");
                strResult.AppendLine();
                strResult.AppendLine("Step_Order|Step_Source|ProductionTime|ErrorCode|ErrorMessage|TotalTime|StepName|Step_Status|ReportText|Num_Loops|Num_Passed|Num_Failed|StepGroup|StepType|Step_PassFail|Multiple_SubName|Units|Comp|Limit.Step_Data|Limit.Low|Limit.High|Result_String|Limits_String|");

                foreach (ME_MSTR me in Arr_ME_MSTR)
                {
                    strResult.AppendLine($"{me.Step_Order}|{me.Step_Source}|{me.ProductionTime}|{me.ErrorCode}|{me.ErrorMessage}|{me.TotalTime}|{me.StepName}|{me.Step_Status}|{me.ReportText}|{me.Num_Loops}|{me.Num_Passed}|{me.Num_Failed}|{me.Step_Group}|{me.StepType}|{me.Step_PassFail}|{me.Multiple_SubName}|{me.Units}|{me.Comp}|{me.LimitStep_Data}|{me.limitLow}|{me.limitHigh}|{me.Result_String}|{me.limits_String}|");
                }
                //LogHelper.Info("生成Mes数据成功");
                return FileHelper.WriteAllText(mesTxtPath, strResult.ToString());

            }
            catch (System.Exception ex)
            {
                LogHelper.Debug("生成Mes数据失败", ex);
                return false;
            }
        }
        #endregion

        #region 测试数据写入CSV文本
        public bool AppendTestValuesRoCsvFile(EV_MSTR EvData, string csvFilePath)
        {
            try
            {
                string[] testValues = new string[Arr_ME_MSTR.Length];
                string[] colNames = new string[Arr_ME_MSTR.Length];

                for (int i = 0; i < Arr_ME_MSTR.Length; i++)
                {
                    colNames[i] = Arr_ME_MSTR[i].StepName;
                    switch (Arr_ME_MSTR[i].StepType)
                    {
                        case "Record":
                        case "NumericLimitTest": testValues[i] = Arr_ME_MSTR[i].LimitStep_Data; break;
                        case "StringValueTest":
                            if (Arr_ME_MSTR[i].Step_Status == "Done")
                            {
                                testValues[i] = Arr_ME_MSTR[i].ReportText; break;
                            }
                            else
                            {
                                testValues[i] = Arr_ME_MSTR[i].Result_String; break;
                            }
                        case "PassFailTest":

                            break;
                        default: break;
                    }
                }
                if (!File.Exists(csvFilePath))//如果文件不存在(即新文件,则增加列首)
                {
                    FileHelper.WriteAllText(csvFilePath, "SN," + colNames.Join(",") + "\r\n", true);
                }
                FileHelper.WriteAllText(csvFilePath, EvData.SerialNumber + "," + testValues.Join(",") + "\r\n", true);
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"{ex}");
                return false;
                throw;
            }

        }
        #endregion
    }



    public class EV_MSTR
    {
        #region 通用参数
        public string UUT_Order = "8";
        public string UUT_SOURCE = "EV";
        public string ModelName = "";//新增
        public string DeviceA2C = "";//"907025501471"
        public string EquipmentFunction = "SL";
        public string SerialNumber = "";//实时  
        public string StationName = "SL";
        public string ProductTime = "";//实时 "160228142021"
        public string TestStandName = "EOL";
        public string LoginName = "10S00000";//用户名
        public string ExecutionTime = "";//实时
        public string TestSocket = "-1";
        public string BatchSerialNumber = "EV";//实时,用于区分供应商软件与自主软件
        public string TestStatus = "";
        public bool IsQueryInterlocking = true;//新增
        #endregion
        #region 数据库参数
        /// <summary>
        /// 登录数据库的密码
        /// </summary>
        public string DB_Password = "PedTest@rn_radio";
        /// <summary>
        /// 登录数据库的用户名
        /// </summary>
        public string DB_User = "TE_RN";
        /// <summary>
        /// 数据库名称
        /// </summary>
        public string DatabaseName = "ED_Test";
        /// <summary>
        /// 服务器名称
        /// </summary>
        public string ServerName = "HZHE015A";
        /// <summary>
        /// 工位名称
        /// </summary>
        public string StationID = "Resource01";
        /// <summary>
        /// 线体编号
        /// </summary>
        public string LineGroup = "1";
        /// <summary>
        /// 应用系统的用户名(用LoginName就可以,这个变量暂时没用到)
        /// </summary>
        public string SW_User = "";
        /// <summary>
        /// 是否进入调试状态,即是否允许强制Pass,无输入默认为False(管理员账号为True,操作员账号为False)
        /// </summary>
        public string Debug = "";
        /// <summary>
        /// 是否在结果为Fail时显示Fail窗口信息,无输入默认为False
        /// </summary>
        public string ShowWindow = "";
        /// <summary>
        /// 输入为False时表示没网络时结果为Fail,否则没网络时为Pass,无输入默认为False
        /// </summary>
        public string PassForNoDB = "";
        /// <summary>
        /// 功能选项(-1为全部功能打开,0为全部功能关闭,1为只打开Fail防错,2为只打开Pass防错,3为打开Pass&Fail防错,4为只打开互锁,5为互锁+Fail防错,6为互锁+Pass防错,7为互锁+Pass&Fail防错,无输入默认为互锁+Fail防错)
        /// </summary>
        public string Function = "";
        #endregion
    }
    public class ME_MSTR
    {
        public string Step_Order = "1";
        public string Step_Source = "ME";
        public string ProductionTime = "";//实时，检测的开始时间
        public string ErrorCode = "0";
        public string ErrorMessage = "";
        public string TotalTime = "";      //实时，检测的总时间
        public string StepName = "";
        public string Step_Status = "";   // 实时

        public string ReportText = "";    //新增
        public string Num_Loops = "1";    //新增
        public string Num_Passed = "1";    //新增
        public string Num_Failed = "0";    //新增
        public string Step_Group = "Main";    //新增

        public string StepType = "";
        public string Step_PassFail = "";
        public string Multiple_SubName = "";    //新增

        public string Units = "";
        public string Comp = "";
        public string LimitStep_Data = null;
        public string limitLow = null;
        public string limitHigh = null;
        public string Result_String = "";
        public string limits_String = "";
    }
    public struct MesData
    {
        /// <summary>
        /// NumericTest最小值
        /// </summary>
        public double MinLimits;
        /// <summary>
        /// NumericTest最大值
        /// </summary>
        public double MaxLimits;
        /// <summary>
        /// NumericTes USE
        /// </summary>
        public double Value;
        /// <summary>
        /// PassFailTest/StringValueTes USE
        /// </summary>
        public string StringValue;
        /// <summary>
        /// StringValueTes对比值
        /// </summary>
        public string limitString;
        /// <summary>
        /// PassFailTest错误代码
        /// </summary>
        public int errorCode;
        /// <summary>
        /// PassFailTest错误消息
        /// </summary>
        public string errorMessage;
        /// <summary>
        /// PassFailTest开始时间
        /// </summary>
        public string startOfTime;
        /// <summary>
        /// PassFailTest完成时间
        /// </summary>
        public int toalTime;


        /// <summary>
        /// 0:add_ME_MSTR_Record 1:add_ME_MSTR_NumericTest  2:add_ME_MSTR_StringValueTest  3:add_ME_MSTR_PassFailTest
        /// </summary>
        public int Istype;
    }

    #region  互锁
    /// <summary>
    /// 此类用于MES互锁和SV互锁
    /// </summary>
    public class InterlockingClass
    {
        [DllImport("C:\\MES\\MES Interlock DLL\\MES Interlock DLL_EDCMESASS.dll")]
        public extern static short MoveIn(string InputString);
        [DllImport("C:\\MES\\MES Interlock DLL\\MES Interlock DLL_EDCMESASS.dll")]
        public extern static short MoveStd(string InputString);
        [DllImport("C:\\MES\\MES Interlock DLL\\MES Interlock DLL_EDCMESASS.dll")]
        public extern static short SplitSN(string InputString);
        [DllImport("C:\\MES\\MES Interlock DLL\\MES Interlock DLL_EDCMESASS.dll")]
        public extern static short BindingSN(string InputString);
        [DllImport("C:\\MES\\MES Interlock DLL\\MES Interlock DLL_EDCMESASS.dll")]
        public extern static short CreateEOL_SN(string InputString);


        [DllImport("C:\\MES\\SV_Interlocking\\sv_Interlocking_Main.dll")]
        public extern static short Sv_Interlocking_Main(string InputString);

        #region  MES  

        #region MES  Labview dll 仅适用32位
        /// <summary>
        /// MES_MoveIn labview dll
        /// </summary>
        /// <param name="SerialNumber"></param>
        /// <param name="mes"></param>
        /// <returns></returns>         
        public short MES_MoveIn(string SerialNumber, EV_MSTR EvData)
        {
            string[] InputString = new string[5];
            InputString[0] = SerialNumber;//SN
            InputString[1] = EvData.StationID;//Station
            InputString[2] = "";//Spec
            InputString[3] = EvData.LoginName;//Employee
            InputString[4] = "False";//HideWindow
            short MoveInResult = MoveIn(String.Join(",", InputString));
            LogHelper.Info(SerialNumber + "，MoveInResult=" + MoveInResult);
            return MoveInResult;
        }
        /// <summary>
        /// MES_MoveStd labview dll
        /// </summary>
        public short MES_MoveStd(string SerialNumber, string TestResult, EV_MSTR EvData)
        {

            string[] InputString = new string[6];
            InputString[0] = SerialNumber;//SN
            InputString[1] = EvData.StationID;//Station
            InputString[2] = string.Format("{0:yyyy-MM-ddTHH:mm:ss}", DateTime.Now);//Time
            InputString[3] = TestResult;//TestResult
            InputString[4] = EvData.SW_User;//Employee
            InputString[5] = "False";//HideWindow                 
            short MoveStdResult = MoveStd(String.Join(",", InputString));
            LogHelper.Info(SerialNumber + "，MoveStdResult=" + MoveStdResult);
            return MoveStdResult;
        }

        /// <summary>
        /// MES_SplitSN labview dll
        /// </summary>
        public short MES_SplitSN(string SerialNumber, string SplitSerialNumber, EV_MSTR EvData)
        {
            string[] InputString = new string[4];
            InputString[0] = SerialNumber;//SN
            InputString[1] = SplitSerialNumber;//SplitSN
            InputString[2] = EvData.SW_User;//Employee
            InputString[3] = "False";//HideWindow
            short SplitSNResult = SplitSN(String.Join(",", InputString));
            LogHelper.Info(SplitSerialNumber + "，SplitSNResult=" + SplitSNResult);
            return SplitSNResult;

        }


        /// <summary>
        /// MES_BindingSN labview dll
        /// </summary>
        /// <param name="SerialNumber"></param>
        /// <param name="bindingSerialNumber"></param>
        /// <param name="mes"></param>
        /// <returns></returns>
        public short MES_BindingSN(string SerialNumber, string bindingSerialNumber, EV_MSTR EvData)
        {
            string[] InputString = new string[4];
            InputString[0] = SerialNumber;//SN
            InputString[1] = bindingSerialNumber;
            InputString[2] = EvData.SW_User;//Employee
            InputString[3] = "False";//HideWindow
            short bindingSNResult = BindingSN(String.Join(",", InputString));
            LogHelper.Info(bindingSerialNumber + "，bindingSNResult=" + bindingSNResult);
            return bindingSNResult;
        }

        /// <summary>
        /// MES_CreateEOL_SN labview dll
        /// </summary>
        public short MES_CreateEOL_SN(string SerialNumber, string CreateEOL_SerialNumber, EV_MSTR EvData)
        {
            string[] InputString = new string[4];
            InputString[0] = SerialNumber;//SN
            InputString[1] = CreateEOL_SerialNumber;
            InputString[2] = EvData.SW_User;//Employee
            InputString[3] = "False";//HideWindow
            short CreateEOL_SNResult = CreateEOL_SN(String.Join(",", InputString));
            LogHelper.Info(CreateEOL_SerialNumber + "，CreateEOL_SNResult=" + CreateEOL_SNResult);
            return CreateEOL_SNResult;
        }

        #endregion

        #region  MES 可兼容64位和32位，C#开发DLL
        /// <summary>
        /// MES_MoveIn_64 C#开发DLL
        /// </summary>
        public short MES_MoveIn_64(string SerialNumber, EV_MSTR EvData)
        {
            string[] InputString = new string[5];
            InputString[0] = SerialNumber;//SN
            InputString[1] = EvData.StationID;//Station
            InputString[2] = "";//Spec
            InputString[3] = EvData.LoginName;//Employee
            InputString[4] = "False";//HideWindow
            short MoveInResult = MES_Interlock_DLL_EDCMESASS_64_Class.MoveIn(String.Join(",", InputString));
            LogHelper.Info(SerialNumber + "，MoveInResult_64=" + MoveInResult);
            return MoveInResult;


        }
        /// <summary>
        /// MES_MoveStd_64 C#开发DLL
        /// </summary>
        public short MES_MoveStd_64(string SerialNumber, string TestResult, EV_MSTR EvData)
        {
            string[] InputString = new string[6];
            InputString[0] = SerialNumber;//SN
            InputString[1] = EvData.StationID;//Station
            InputString[2] = string.Format("{0:yyyy-MM-ddTHH:mm:ss}", DateTime.Now);//Time
            InputString[3] = TestResult;//TestResult
            InputString[4] = EvData.SW_User;//Employee
            InputString[5] = "False";//HideWindow                 
            short MoveStdResult = MES_Interlock_DLL_EDCMESASS_64_Class.MoveStd(String.Join(",", InputString));
            LogHelper.Info(SerialNumber + "，MoveStdResult_64=" + MoveStdResult);
            return MoveStdResult;
        }
        /// <summary>
        /// MES_SplitSN_64 C#开发DLL
        /// </summary>
        public short MES_SplitSN_64(string SerialNumber, string SplitSerialNumber, EV_MSTR EvData)
        {
            string[] InputString = new string[4];
            InputString[0] = SerialNumber;//SN
            InputString[1] = SplitSerialNumber;//SplitSN
            InputString[2] = EvData.SW_User;//Employee
            InputString[3] = "False";//HideWindow
            short SplitSNResult = MES_Interlock_DLL_EDCMESASS_64_Class.SplitSN(String.Join(",", InputString));
            LogHelper.Info(SplitSerialNumber + "，SplitSNResult_64=" + SplitSNResult);
            return SplitSNResult;

        }
        /// <summary>
        /// MES_BindingSN_64 C#开发DLL       
        /// </summary>
        /// <param name="SerialNumber"></param>
        /// <param name="bindingSerialNumber"></param>
        /// <param name="mes"></param>
        /// <returns></returns>
        public short MES_BindingSN_64(string SerialNumber, string bindingSerialNumber, EV_MSTR EvData)
        {
            string[] InputString = new string[4];
            InputString[0] = SerialNumber;//SN
            InputString[1] = bindingSerialNumber;
            InputString[2] = EvData.SW_User;//Employee
            InputString[3] = "False";//HideWindow
            short bindingSNResult = MES_Interlock_DLL_EDCMESASS_64_Class.BindingSN(String.Join(",", InputString));
            LogHelper.Info(bindingSerialNumber + "，bindingSNResult_64=" + bindingSNResult);
            return bindingSNResult;
        }
        /// <summary>
        /// MES_CreateEOL_SN_64 C#开发DLL
        /// </summary>
        public short MES_CreateEOL_SN_64(string SerialNumber, string CreateEOL_SerialNumber, EV_MSTR EvData)
        {
            string[] InputString = new string[4];
            InputString[0] = SerialNumber;//SN
            InputString[1] = CreateEOL_SerialNumber;
            InputString[2] = EvData.SW_User;//Employee
            InputString[3] = "False";//HideWindow
            short CreateEOL_SNResult = MES_Interlock_DLL_EDCMESASS_64_Class.CreateEOL_SN(String.Join(",", InputString));
            LogHelper.Info(CreateEOL_SerialNumber + "，CreateEOL_SNResult_64=" + CreateEOL_SNResult);
            return CreateEOL_SNResult;
        }
        #endregion

        #endregion

        #region SV_Interlocking  Labview DLL版
        /// <summary>
        /// SV_InterLocking(Llabview DLL)
        /// </summary>
        /// <param name="SN"></param>
        /// <param name="sv"></param>
        /// <returns></returns>
        public int Sv_Interlocking_LV(string SN, EV_MSTR EvData)
        {
            //DSV互锁模式
            string[] Information = new string[12];
            Information[0] = EvData.DB_Password;//DB_Password
            Information[1] = EvData.DB_User;//DB_User
            Information[2] = EvData.DatabaseName;//DatabaseName
            Information[3] = EvData.ServerName;//ServerName
            Information[4] = SN;//SerialNumber
            Information[5] = EvData.StationID;//StationID
            Information[6] = EvData.LineGroup;//LineGroup
            Information[7] = EvData.LoginName;//SV_User
            Information[8] = EvData.Debug;//Debug              
            Information[9] = EvData.ShowWindow;//ShowWindow
            Information[10] = "False";//PassForNoDB
            Information[11] = EvData.Function;//Function                          
            int SVResult = Sv_Interlocking_Main(String.Join(",", Information));
            LogHelper.Info(SN + "，SVResult=" + SVResult.ToString());
            return SVResult;
        }

        #endregion

        #region SV_Interlocking  C#版本
        /// <summary>
        /// SV_InterLocking(C#)
        /// </summary>
        /// <param name="SN"></param>
        /// <param name="sv"></param>
        /// <returns></returns>
        public int SV_Interlocking(string SN, EV_MSTR EvData)
        {
            //DSV互锁模式
            string[] Information = new string[12];
            Information[0] = EvData.DB_Password;//DB_Password
            Information[1] = EvData.DB_User;//DB_User
            Information[2] = EvData.DatabaseName;//DatabaseName
            Information[3] = EvData.ServerName;//ServerName
            Information[4] = SN;//SerialNumber
            Information[5] = EvData.StationID;//StationID
            Information[6] = EvData.LineGroup;//LineGroup
            Information[7] = EvData.LoginName;//SV_User
            Information[8] = EvData.Debug;//Debug              
            Information[9] = EvData.ShowWindow;//ShowWindow
            Information[10] = "False";//PassForNoDB
            Information[11] = EvData.Function;//Function                          
            int SVResult = sv_Interlocking_Main.sv_Interlocking_Main_Class.SV_Interlocking_Main(String.Join(",", Information));
            LogHelper.Info(SN + "，SVResult=" + SVResult.ToString());
            return SVResult;
        }
        #endregion

        #region 通过工站3扫描PCB2查询PCB1是否在工站2绑定及在工站1PCB1是否有数据上传Pass
        /// <summary>
        /// 通过工站3扫描PCB2查询PCB1是否在工站2绑定及在工站1PCB1是否有数据上传Pass
        /// </summary>
        public string CheckedPCB1FromFCB2Result(EV_MSTR EvData, string SN, string pcb1StationID, string pcb2StationID)
        {
            string strResult = "Error";
            using (SqlConnection conn_HZHE015A = new SqlConnection())
            {
                string connectionString;

                connectionString = @"Password=PedTest@rn_radio;User ID=TE_RN;Initial Catalog=" + EvData.DatabaseName + ";Data Source=HZHE015A";
                conn_HZHE015A.ConnectionString = connectionString;
                string queryStr = "SELECT(SELECT TOP 1[UUT_STATUS]" +
                                  "FROM[" + EvData.DatabaseName + "].[dbo].[UUT_RESULT]" +
                                  "WHERE STATION_ID = '" + pcb1StationID + "'" +
                                  " AND UUT_SERIAL_NUMBER = (SELECT TOP 1[BATCH_SERIAL_NUMBER]" +
                                  "FROM[" + EvData.DatabaseName + "].[dbo].[UUT_RESULT]" +
                                  " WHERE UUT_SERIAL_NUMBER = '" + SN + "'" +
                                  "AND STATION_ID = '" + pcb2StationID + "'" +
                                  "AND UUT_ERROR_MESSAGE <> 'ReWork' ORDER BY START_DATE_TIME DESC)" +
                                  "AND UUT_ERROR_MESSAGE <> 'ReWork' AND TEST_SOCKET_INDEX = '-1'" +
                                 " ORDER BY START_DATE_TIME DESC)";
                DataTable dt = ReadOrderData(connectionString, queryStr);

                try
                {
                    strResult = dt.Rows[0][0].ToString();
                    LogHelper.Info(SN + ",SN查询结果：" + strResult);
                }
                catch (Exception ex)
                {
                    LogHelper.Error(SN + ",查询异常" + ex);

                }

            }
            return strResult;


        }
        #endregion

        #region 查询上一工站到本工站时间间隔
        /// <summary>
        /// 查询上一工站到本工站时间间隔
        /// </summary>
        public DateTime Get_SN_time_interval(string SN, string frontStationID, EV_MSTR EvData)
        {
            DateTime enddatetime = new DateTime();
            using (SqlConnection conn_HZHE015A = new SqlConnection())
            {
                string constr = "Server=" + EvData.ServerName + ";" + "user=" + EvData.DB_User + ";" + "pwd=" + EvData.DB_Password + ";" + "database=" + EvData.DatabaseName;
                conn_HZHE015A.ConnectionString = constr;
                string timeSearchOrder = "SELECT *FROM dbo.UUT_RESULT where  UUT_SERIAL_NUMBER = '" + SN + "' And station_id = '" + frontStationID + "'And UUT_STATUS = 'PASSED'order by START_DATE_TIME desc";
                DataTable time = ReadOrderData(constr, timeSearchOrder);
                int con = time.Rows.Count;
                if (con > 0)
                {
                    string strResult = time.Rows[0][6].ToString();
                    enddatetime = Convert.ToDateTime(Convert.ToDateTime(strResult));
                    LogHelper.Info(SN + ",时间间隔：" + enddatetime);
                }
                else
                {
                    LogHelper.Info(SN + ",查询不到数据，输出默认时间：" + enddatetime);
                }

            }
            return enddatetime;
        }
        #endregion

        #region 单独查询SN在上一个工位状态 Passed or Failed   (注：也可以查询本工站SN状态，可防重测功能)
        /// <summary>
        /// 单独查询SN在上一个工位状态 Passed or Failed   (注：也可以查询本工站SN状态，可防重测功能)
        /// </summary>
        /// 
        public string SNStatus(string SN, string frontStationID, EV_MSTR EvData)
        {
            string strResult = "Error";
            using (SqlConnection conn_HZHE015A = new SqlConnection())
            {
                string constr = "Server=" + EvData.ServerName + ";" + "user=" + EvData.DB_User + ";" + "pwd=" + EvData.DB_Password + ";" + "database=" + EvData.DatabaseName;
                conn_HZHE015A.ConnectionString = constr;
                string snOrder = "SELECT TOP 1  UUT_STATUS  from UUT_RESULT where UUT_SERIAL_NUMBER = '" + SN + "' AND STATION_ID = '" + frontStationID + "'";//查询数据库数据结果
                DataTable status = ReadOrderData(constr, snOrder);
                try
                {
                    strResult = status.Rows[0][0].ToString();
                    LogHelper.Info(SN + ",SN查询结果：" + strResult);
                }
                catch (Exception ex)
                {

                    LogHelper.Error(SN + ",查询异常" + ex);
                }

            }
            return strResult;
        }
        #endregion

        #region 获取服务器时间
        /// <summary>
        /// 获取服务器时间
        /// </summary>
        /// 
        public DateTime GetServerTime(EV_MSTR EvData)
        {
            DateTime servertime = new DateTime();
            using (SqlConnection conn_HZHE015A = new SqlConnection())
            {
                string constr = "Server=" + EvData.ServerName + ";" + "user=" + EvData.DB_User + ";" + "pwd=" + EvData.DB_Password + ";" + "database=" + EvData.DatabaseName;
                conn_HZHE015A.ConnectionString = constr;
                string timeSearchOrder = "select getdate() as systemtimes";
                DataTable time = ReadOrderData(constr, timeSearchOrder);
                int con = time.Rows.Count;
                if (con > 0)
                {
                    string dt = time.Rows[0][0].ToString();
                    servertime = Convert.ToDateTime(Convert.ToDateTime(dt));
                    LogHelper.Info("服务器时间：" + servertime);
                }
                else
                {
                    LogHelper.Error("服务器时间查询异常，默认时间" + servertime);
                }


            }
            return servertime;
        }
        #endregion

        #region  数据库查询Conm
        /// <summary>
        /// 数据库查询Conm
        /// </summary>
        /// 
        private static DataTable ReadOrderData(string connectionString, string queryString)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    data.Load(reader);
                    reader.Close();

                }
                catch (Exception ex)
                {
                    LogHelper.Error("查询异常" + ex);
                }
                return data;

            }
        }
        #endregion

        #endregion
    }
}
