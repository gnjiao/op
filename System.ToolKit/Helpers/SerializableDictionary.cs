using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.IO;

namespace System.Toolkit.Helpers
{
    /// <summary>
    /// 数据类型
    /// </summary>
    [Serializable]
    public class LoginInfo
    {
        public LoginInfo()
        {
            UserName = "null";
            Password = "null";
            UserLevel = 0;
        }

        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserLevel { get; set; }
    }
    /// <summary>
    /// 序列化bat文件数据
    /// </summary>
    [Serializable]
    public class SerializableDictionary
    {
        public Dictionary<string, LoginInfo> dicLoginInfo;

        public SerializableDictionary()
        {
            dicLoginInfo = new Dictionary<string, LoginInfo>();
        }

        public static SerializableDictionary LoadLoginInfo(string Path)
        {
            SerializableDictionary loginDoc = new SerializableDictionary();

            BinaryFormatter fmt = new BinaryFormatter();
            FileStream fsReader = null;
            try
            {
                fsReader = File.OpenRead(Path);//Application.StartupPath + @"\Login\login.dat"
                loginDoc = (SerializableDictionary)fmt.Deserialize(fsReader);
                fsReader.Close();


            }
            catch (Exception)
            {
                if (fsReader != null)
                {
                    fsReader.Close();
                }
            }

            return loginDoc;
        }

        public bool SaveDoc(string Path)
        {
            //if (!Directory.Exists(Application.StartupPath + @"\Login\"))
            //{
            //    Directory.CreateDirectory(Application.StartupPath + @"\Login\");
            //}

            FileStream fsWriter = new FileStream(Path, FileMode.Create, FileAccess.Write, FileShare.Read);//Application.StartupPath + @"\Login\login.dat"
            BinaryFormatter fmt = new BinaryFormatter();
            fmt.Serialize(fsWriter, this);
            fsWriter.Close();

            return true;
        }

    }
}
