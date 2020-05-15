using System;
using System.Windows.Forms;


namespace JobNumber
{
    /// <summary>
    /// 串口扫描枪类，注意：StopBit的值可以写成One，不要写成1;
    /// 调用说明:
    /// 1,实例化对象后,绑定一个控件
    /// 2,Start(),这样扫描的内容就会显示在控件中
    /// </summary>
    public class Scanner
    {
        /// <summary>
        /// Ini的串口信息,格式为:"ini文件路径,Section,COM,BaudRate,Parity,DataBit,StopBit,TriggerData,StopTriggerData"
        /// </summary>
        /// <param name="iniInfos"></param>
        public Scanner(string iniInfos)
        {
            string[] infos = iniInfos.Split(',');
            if (infos.Length == 7)
            {
                Init(infos[0], infos[1], infos[2], infos[3], infos[4]);
                TriggerData = infos[5];
                StopTriggerData =infos[6];
                IsOpen = true;
            }
            else
            {
               MessageBox.Show($"加载的串口配置信息不完整,{iniInfos}");
            }
        }
        #region 参数
        private string TriggerData = "";
        private string StopTriggerData = "";
        private Port serial;
        private string ScanCodeText="";//扫描的内容
        private TextBox bangdingTextBox;
        public  string bangdingstr;
        private Label bangdingLabel;//label绑定有问题,无法显示扫描内容
      

        public bool MessageHave { get; set; } = false;
        public bool IsOpen { get; set; } = false;
        #endregion

        #region 私有方法
        private void serial_DataReceived(DataReceivedEventArgs e)
        {
            System.Threading.Thread.Sleep(5);
            //字符串传递
            ScanCodeText= e.DataReceived.Replace("\r","").Replace("\n", "");           
            //文本框传递
            try
            {
                bangdingstr = ScanCodeText;
                bangdingTextBox.Invoke(new MethodInvoker(delegate
                {
                    bangdingTextBox.Text=ScanCodeText;
                }));
                bangdingLabel.Invoke(new MethodInvoker(delegate
                {
                    bangdingLabel.Text=ScanCodeText;
                }));

            }
            catch (Exception)
            {
            }
            MessageHave = true;
        }
        private void Init(string com, string baudRate, string parity, string dataBit, string stopBit)
        {
           
            try
            {
               
                serial = new Port(com, baudRate, parity, dataBit, stopBit);
                serial.EndByte = Convert.ToByte('\r');         
               
                serial.OpenPort();
             
            }
            catch (Exception ex)
            {
               MessageBox.Show($"打开条码枪串口异常!"+ex);
            }
        }
        #endregion

        #region 公有方法
        public void Bangding(string  txt)
        {
            try { serial.DataReceived -= new DataReceivedEventHandler(serial_DataReceived); }
            catch (Exception) { }
            bangdingstr = txt;
            serial.DataReceived += new DataReceivedEventHandler(serial_DataReceived);

        }
        public void Bangding(TextBox txt)
        {
            try
            {
                serial.DataReceived -= new DataReceivedEventHandler(serial_DataReceived); }
            catch (Exception)  {  }
            bangdingTextBox=txt;
            serial.DataReceived += new DataReceivedEventHandler(serial_DataReceived);
         
        }
        public void Bangding(Label lbl)
        {
            try { serial.DataReceived -= new DataReceivedEventHandler(serial_DataReceived); }
            catch (Exception) { }
            bangdingLabel = lbl;
            serial.DataReceived += new DataReceivedEventHandler(serial_DataReceived);
        }
        /// <summary>
        /// 开始触发扫描器
        /// </summary>
        public void Start()
        {
            try
            {
                serial.WriteData(TriggerData);
            }
            catch (Exception ex)
            {
               MessageBox.Show($"串口写入信息异常!"+ex);
            }
        }
        /// <summary>
        /// 停止触发扫描器
        /// </summary>
        public void Stop()
        {
            try
            {
                serial.WriteData(StopTriggerData);
            }
            catch (Exception ex)
            {
               MessageBox.Show($"串口写入信息异常!"+ ex);
            }
        }
        ///// <summary>
        ///// 返回读取扫描的内容,同时传输到Textbox中.
        ///// </summary>
        ///// <returns></returns>
        //public string Read()
        //{
        //    string temp = ScanCodeText;
        //    ScanCodeText = "";
        //    return temp;
        //}
        /// <summary>
        /// 关闭串口
        /// </summary>
        public void Close()
        {
            serial.ClosePort();
        }
        #endregion


    }
}
