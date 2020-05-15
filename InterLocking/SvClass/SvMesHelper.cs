using System;
using System.ToolKit;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.IO;
using LogHeper;

namespace InterLocking
{
    /// <summary>
    /// MES数据采集类，功能如下：
    /// 1、MesTxt文本生成：包含一个EV_MSTR对象和ME_MSTR列表，列表的长度由要采集的参数个数决定。调用方法：构造后，添加数据到Arr_ME_MSTR，WriteMesTxtToFile生成
    /// 2、Csv文件保存测试项信息：在上一步的基础上直接调用AppendTestValuesRoCsvFile方法就可以了
    /// 3、Interlocking：
    /// </summary>
    public class SvMesHelper
    {
        #region 构造函数      
        public SvMesHelper()
        {       
        }
        #endregion
      

        
      

       
       
    }
}