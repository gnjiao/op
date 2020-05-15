using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace System.ToolKit
{
    public class INIFile
    {
        /* 
         * 针对INI文件的API操作方法，其中的节点（Section)、键（KEY）都不区分大小写 
         * 如果指定的INI文件不存在，会自动创建该文件。 
         *  
         * CharSet定义的时候使用了什么类型，在使用相关方法时必须要使用相应的类型 
         *      例如 GetPrivateProfileSectionNames声明为CharSet.Auto,那么就应该使用 Marshal.PtrToStringAuto来读取相关内容 
         *      如果使用的是CharSet.Ansi，就应该使用Marshal.PtrToStringAnsi来读取内容 
         *       
         *       
         *     INIFile不能为静态类,否则导致有时写入失败  
         *       
         */

        #region API声明  

        /// <summary>  
        /// 获取所有节点名称(Section)  
        /// </summary>  
        /// <param name="lpszReturnBuffer">存放节点名称的内存地址,每个节点之间用\0分隔</param>  
        /// <param name="nSize">内存大小(characters)</param>  
        /// <param name="lpFileName">Ini文件</param>  
        /// <returns>内容的实际长度,为0表示没有内容,为nSize-2表示内存大小不够</returns>  
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetPrivateProfileSectionNames(IntPtr lpszReturnBuffer, uint nSize, string lpFileName);

        /// <summary>  
        /// 获取某个指定节点(Section)中所有KEY和Value  
        /// </summary>  
        /// <param name="lpAppName">节点名称</param>  
        /// <param name="lpReturnedString">返回值的内存地址,每个之间用\0分隔</param>  
        /// <param name="nSize">内存大小(characters)</param>  
        /// <param name="lpFileName">Ini文件</param>  
        /// <returns>内容的实际长度,为0表示没有内容,为nSize-2表示内存大小不够</returns>  
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetPrivateProfileSection(string lpAppName, IntPtr lpReturnedString, uint nSize, string lpFileName);

        /// <summary>  
        /// 读取INI文件中指定的Key的值  
        /// </summary>  
        /// <param name="lpAppName">节点名称。如果为null,则读取INI中所有节点名称,每个节点名称之间用\0分隔</param>  
        /// <param name="lpKeyName">Key名称。如果为null,则读取INI中指定节点中的所有KEY,每个KEY之间用\0分隔</param>  
        /// <param name="lpDefault">读取失败时的默认值</param>  
        /// <param name="lpReturnedString">读取的内容缓冲区，读取之后，多余的地方使用\0填充</param>  
        /// <param name="nSize">内容缓冲区的长度</param>  
        /// <param name="lpFileName">INI文件名</param>  
        /// <returns>实际读取到的长度</returns>  
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, [In, Out] char[] lpReturnedString, uint nSize, string lpFileName);

        //另一种声明方式,使用 StringBuilder 作为缓冲区类型的缺点是不能接受\0字符，会将\0及其后的字符截断,  
        //所以对于lpAppName或lpKeyName为null的情况就不适用  
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

        //再一种声明，使用string作为缓冲区的类型同char[]  
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, string lpReturnedString, uint nSize, string lpFileName);

        /// <summary>  
        /// 将指定的键值对写到指定的节点，如果已经存在则替换。  
        /// </summary>  
        /// <param name="lpAppName">节点，如果不存在此节点，则创建此节点</param>  
        /// <param name="lpString">Item键值对，多个用\0分隔,形如key1=value1\0key2=value2  
        /// <para>如果为string.Empty，则删除指定节点下的所有内容，保留节点</para>  
        /// <para>如果为null，则删除指定节点下的所有内容，并且删除该节点</para>  
        /// </param>  
        /// <param name="lpFileName">INI文件</param>  
        /// <returns>是否成功写入</returns>  
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]     //可以没有此行  
        private static extern bool WritePrivateProfileSection(string lpAppName, string lpString, string lpFileName);

        /// <summary>  
        /// 将指定的键和值写到指定的节点，如果已经存在则替换  
        /// </summary>  
        /// <param name="lpAppName">节点名称</param>  
        /// <param name="lpKeyName">键名称。如果为null，则删除指定的节点及其所有的项目</param>  
        /// <param name="lpString">值内容。如果为null，则删除指定节点中指定的键。</param>  
        /// <param name="lpFileName">INI文件</param>  
        /// <returns>操作是否成功</returns>  
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        #endregion

        #region 封装  

        #region 读取所有节点名称(Section)
        /// <summary>  
        /// 读取INI文件中的所有节点名称(Section)  
        /// </summary>  
        /// <param name="iniFile">Ini文件</param>  
        /// <returns>所有节点,没有内容返回string[0]</returns>  
        public string[] ReadAllSectionNames(string iniFile)
        {
            uint MAX_BUFFER = 32767;    //默认为32767  

            string[] sections = new string[0];      //返回值  

            //申请内存  
            IntPtr pReturnedString = Marshal.AllocCoTaskMem((int)MAX_BUFFER * sizeof(char));
            uint bytesReturned = INIFile.GetPrivateProfileSectionNames(pReturnedString, MAX_BUFFER, iniFile);
            if (bytesReturned != 0)
            {
                //读取指定内存的内容  
                string local = Marshal.PtrToStringAuto(pReturnedString, (int)bytesReturned).ToString();

                //每个节点之间用\0分隔,末尾有一个\0  
                sections = local.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
            }

            //释放内存  
            Marshal.FreeCoTaskMem(pReturnedString);

            return sections;
        }
        /// <summary>
        /// 获取INI文件的所有节点中指定Key的所有值
        /// </summary>
        /// <param name="iniFile"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string[] ReadAllSectionValues(string iniFile, string key)
        {
            string[] sections = ReadAllSectionNames(iniFile);
            List<string> values = new List<string>();
            for (int i = 0; i < sections.Length; i++)
            {
                values.Add(ReadValue(iniFile, sections[i], key));
            }
            return values.RemoveDuplates().ToArray();
        }
        /// <summary>
        /// 获取INI文件的所有节点中所有Key的值,可以选择是否包括Section这一列内容
        /// </summary>
        /// <param name="iniFile"></param>
        /// <param name="isContainSection"></param>
        /// <returns></returns>
        public string[,] ReadAllSectionValues(string iniFile, bool isContainSection = false)
        {
            string[] sections = ReadAllSectionNames(iniFile);
            string[] keys = ReadAllItemValues(iniFile, sections[0]);
            List<string[]> values = new List<string[]>();
            for (int i = 0; i < sections.Length; i++)
            {
                string[] data;
                if (isContainSection)
                {
                    data = ReadAllItemValues(iniFile, sections[i]).InsertElement(sections[i], 0);
                }
                else
                {
                    data = ReadAllItemValues(iniFile, sections[i]);
                }
                values.Add(data);
            }
            return values.To2DArray();
        }
        #endregion
        #region 获取或写入条目(key=value形式) 
        /// <summary>  
        /// 获取INI文件中指定节点(Section)中的所有条目(key=value形式)  
        /// </summary>  
        /// <param name="iniFile">Ini文件</param>  
        /// <param name="section">节点名称</param>  
        /// <returns>指定节点中的所有项目,没有内容返回string[0]</returns>  
        public string[] ReadAllItems(string iniFile, string section)
        {
            //返回值形式为 key=value,例如 Color=Red  
            uint MAX_BUFFER = 32767;    //默认为32767  

            string[] items = new string[0];      //返回值  

            //分配内存  
            IntPtr pReturnedString = Marshal.AllocCoTaskMem((int)MAX_BUFFER * sizeof(char));

            uint bytesReturned = INIFile.GetPrivateProfileSection(section, pReturnedString, MAX_BUFFER, iniFile);

            if (!(bytesReturned == MAX_BUFFER - 2) || (bytesReturned == 0))
            {

                string returnedString = Marshal.PtrToStringAuto(pReturnedString, (int)bytesReturned);
                items = returnedString.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
            }

            Marshal.FreeCoTaskMem(pReturnedString);     //释放内存  

            return items;
        }
        /// <summary>  
        /// 在INI文件中，将指定的键值对写到指定的节点，如果已经存在则替换  
        /// </summary>  
        /// <param name="iniFile">INI文件</param>  
        /// <param name="section">节点，如果不存在此节点，则创建此节点</param>  
        /// <param name="items">键值对，多个用\0分隔,形如key1=value1\0key2=value2</param>  
        /// <returns></returns>  
        public bool WriteItems(string iniFile, string section, string items)
        {
            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentException("必须指定节点名称", "section");
            }

            if (string.IsNullOrEmpty(items))
            {
                throw new ArgumentException("必须指定键值对", "items");
            }

            return INIFile.WritePrivateProfileSection(section, items, iniFile);
        }
        #endregion

        #region 读取指定节点的所有Key或Value
        /// <summary>  
        /// 获取INI文件中指定节点(Section)中的所有条目的Key列表  
        /// </summary>  
        /// <param name="iniFile">Ini文件</param>  
        /// <param name="section">节点名称</param>  
        /// <returns>如果没有内容,反回string[0]</returns>  
        public string[] ReadAllItemKeys(string iniFile, string section)
        {
            string[] value = new string[0];
            const int SIZE = 1024 * 10;

            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentException("必须指定节点名称", "section");
            }

            char[] chars = new char[SIZE];
            uint bytesReturned = INIFile.GetPrivateProfileString(section, null, null, chars, SIZE, iniFile);

            if (bytesReturned != 0)
            {
                value = new string(chars).Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
            }
            chars = null;

            return value;
        }
        public string[] ReadAllItemValues(string iniFile, string section)
        {
            List<string> list = new List<string>();

            foreach (var key in ReadAllItemKeys(iniFile, section))
            {
                list.Add(ReadValue(iniFile, section, key));
            }
            return list.ToArray();
        }
        #endregion

        #region 读取指定节点指定Key的值、指定多个Key的值，读取所有节点指定Key的值
        /// <summary>  
        /// 读取INI文件中指定KEY的字符串型值  
        /// </summary>  
        /// <param name="iniFile">Ini文件</param>  
        /// <param name="section">节点名称</param>  
        /// <param name="key">键名称</param>  
        /// <param name="defaultValue">如果没此KEY所使用的默认值</param>  
        /// <returns>读取到的值</returns>  
        public string ReadValue(string iniFile, string section, string key, string defaultValue = "")
        {
            string value = defaultValue;
            const int SIZE = 1024 * 10;

            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentException("必须指定节点名称", "section");
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("必须指定键名称(key)", "key");
            }

            StringBuilder sb = new StringBuilder(SIZE);
            uint bytesReturned = INIFile.GetPrivateProfileString(section, key, defaultValue, sb, SIZE, iniFile);

            if (bytesReturned != 0)
            {
                value = sb.ToString();
            }
            sb = null;

            return value;
        }
        /// <summary>
        /// 获取ini文件所有节点中指定键的值
        /// </summary>
        /// <param name="iniFile"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string[] ReadValues(string iniFile, string key)
        {
            List<string> sList = new List<string>();
            foreach (var item in ReadAllSectionNames(iniFile))
            {
                string value = ReadValue(iniFile, item, key);
                sList.Add(value);
            }
            return sList.ToArray();
        }
        /// <summary>
        /// 获取ini文件指定节点中指定多个键的值
        /// </summary>
        /// <param name="iniFile"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string[] ReadValues(string iniFile, string section, string[] keys)
        {
            List<string> sList = new List<string>();
            foreach (var key in keys)
            {
                string value = ReadValue(iniFile, section, key);
                sList.Add(value);
            }
            return sList.ToArray();
        }
        #endregion

        #region 写入指定节点指定Key的值
        object lockStr = new object();
        /// <summary>  
        /// 在INI文件中，指定节点写入指定的键及值。如果已经存在，则替换。如果没有则创建。  
        /// </summary>  
        /// <param name="iniFile">INI文件</param>  
        /// <param name="section">节点</param>  
        /// <param name="key">键</param>  
        /// <param name="value">值</param>  
        /// <returns>操作是否成功</returns>  
        public bool WriteValue<T>(string iniFile, string section, string key, T value)
        {
            //1.使用以下语句会导致文件被占用,无法写入
            //    File.Create(iniFilePath);
            //2.创建文件后可以正常使用
            FileHelper.CreateFile(iniFile);
            
            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentException("必须指定节点名称", "section");
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("必须指定键名称", "key");
            }

            if (value == null)
            {
                throw new ArgumentException("值不能为null", "value");
            }

            return INIFile.WritePrivateProfileString(section, key, value.ToString(), iniFile);
        }
        #endregion

        #region 复制段
        public void CopySection(string path, string srcSection, string dstSection)
        {
            string[] keys = ReadAllItemKeys(path, srcSection);
            string[] values = ReadAllItemValues(path, srcSection);
            for (int i = 0; i < keys.Length; i++)
            {
                WriteValue(path, dstSection, keys[i], values[i]);
            }
        }
        #endregion

        #region 删除键、删除节点、删除节点内容
        /// <summary>  
        /// 在INI文件中，删除指定节点中的指定的键。  
        /// </summary>  
        /// <param name="iniFile">INI文件</param>  
        /// <param name="section">节点</param>  
        /// <param name="key">键</param>  
        /// <returns>操作是否成功</returns>  
        public bool DeleteKey(string iniFile, string section, string key)
        {
            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentException("必须指定节点名称", "section");
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("必须指定键名称", "key");
            }

            return INIFile.WritePrivateProfileString(section, key, null, iniFile);
        }

        /// <summary>  
        /// 在INI文件中，删除指定的节点。  
        /// </summary>  
        /// <param name="iniFile">INI文件</param>  
        /// <param name="section">节点</param>  
        /// <returns>操作是否成功</returns>  
        public bool DeleteSection(string iniFile, string section)
        {
            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentException("必须指定节点名称", "section");
            }

            return INIFile.WritePrivateProfileString(section, null, null, iniFile);
        }

        /// <summary>  
        /// 在INI文件中，删除指定节点中的所有内容。  
        /// </summary>  
        /// <param name="iniFile">INI文件</param>  
        /// <param name="section">节点</param>  
        /// <returns>操作是否成功</returns>  
        public bool EmptySection(string iniFile, string section)
        {
            if (string.IsNullOrEmpty(section))
            {
                throw new ArgumentException("必须指定节点名称", "section");
            }

            return INIFile.WritePrivateProfileSection(section, string.Empty, iniFile);
        }
        #endregion
        #endregion

        #region 读取或写入窗口大小和位置
        /// <summary>
        /// 读取窗体的大小和位置(如果文件不存在,返回false)(有问题:无法设置窗口大小,是否Form没有传递出来)
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="iniFile">默认在程序同一个文件夹,名称为"Config.ini"</param>
        public bool ReadFormConfig(Form frm, string iniFile = "")
        {
            if (iniFile == "")
            {
                iniFile = Application.StartupPath + @"\Config.ini";
            }
            if (!File.Exists(iniFile))
            {
                return false;//如果文件不存在,返回false
            }
            if (File.Exists(iniFile))
            {
                frm.Width = ReadValue(iniFile, "Form", "WinWidth", "").ToInt();
                frm.Height = ReadValue(iniFile, "Form", "WinHeigh", "").ToInt();
                int x = ReadValue(iniFile, "Form", "WinLeft", "").ToInt();
                int y = ReadValue(iniFile, "Form", "WinTop", "").ToInt();
                frm.StartPosition = FormStartPosition.Manual; //窗体的位置由Location属性决定
                frm.Location = (Point)new Size(x, y);         //窗体的起始位置为(x,y)
            }
            return true;
        }
        /// <summary>
        /// 写入窗体的大小和位置
        /// </summary>
        /// <param name="frm"></param>
        /// <param name="iniFile">默认在程序同一个文件夹,名称为"Config.ini"</param>
        public void WriteFormConfig(Form frm, string iniFile = "")
        {
            if (iniFile == "")
            {
                iniFile = Environment.CurrentDirectory + "\\Config.ini";
            }
            WriteValue(iniFile, "Form", "WinWidth", frm.Width.ToString());
            WriteValue(iniFile, "Form", "WinHeigh", frm.Height.ToString());
            frm.StartPosition = FormStartPosition.Manual; //窗体的位置由Location属性决定
            WriteValue(iniFile, "Form", "WinLeft", frm.Location.X.ToString());
            WriteValue(iniFile, "Form", "WinTop", frm.Location.Y.ToString());
        }
        #endregion

        #region 测试代码

        //private void Test()
        //{

        //    string file = "e:\\3.ini";

        //    //写入/更新键值  
        //    INIFile.WriteValue(file, "Desktop", "Color", "Red");
        //    INIFile.WriteValue(file, "Desktop", "Width", "3270");

        //    INIFile.WriteValue(file, "Toolbar", "Items", "Save,Delete,Open");
        //    INIFile.WriteValue(file, "Toolbar", "Dock", "True");

        //    //写入一批键值  
        //    INIFile.WriteItems(file, "Menu", "File=文件\0View=视图\0Edit=编辑");

        //    //获取文件中所有的节点  
        //    string[] sections = INIFile.GetAllSectionNames(file);

        //    //获取指定节点中的所有项  
        //    string[] items = INIFile.GetAllItems(file, "Menu");

        //    //获取指定节点中所有的键  
        //    string[] keys = INIFile.GetAllItemKeys(file, "Menu");

        //    //获取指定KEY的值  
        //    string value = INIFile.GetValue(file, "Desktop", "color", null);

        //    //删除指定的KEY  
        //    INIFile.DeleteKey(file, "desktop", "color");

        //    //删除指定的节点  
        //    INIFile.DeleteSection(file, "desktop");

        //    //清空指定的节点  
        //    INIFile.EmptySection(file, "toolbar");

        //    //==================================================================

        //    string file = @"D:\C#\SV C#\SV教程汇总\SV方法\Config.ini";

        //    //写入/更新键值  
        //    INIFile.WriteValue(file, "A2C903045200518", "Jingzhong", "11");
        //    INIFile.WriteValue(file, "A2C903045200518", "Maozhong", "100");

        //    //写入一批键值  
        //    INIFile.WriteItems(file, "A2C903094201618", "Maozhong2=22\0Jingzhong2=21.8\0Select_ZPL2=2.zpl");

        //    //获取文件中所有的节点  
        //    string[] sections = INIFile.GetAllSectionNames(file);

        //    //获取指定节点中的所有项  
        //    string[] items = INIFile.GetAllItems(file, "A2C903094201618");

        //    //获取指定节点中所有的键  
        //    string[] keys = INIFile.GetAllItemKeys(file, "A2C903094201618");

        //    //获取指定KEY的值  
        //    string value = INIFile.GetValue(file, "A2C903094201618", "Maozhong", null);

        //    //删除指定的KEY  
        //    INIFile.DeleteKey(file, "A2C903094201618", "String_offest");

        //    //删除指定的节点  
        //    INIFile.DeleteSection(file, "A2C901008714618");

        //    //清空指定的节点  
        //    INIFile.EmptySection(file, "A2C901036726618");

        //}

        #endregion

    }






}
