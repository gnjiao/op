using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace System.Toolkit.Helpers
{
    public enum DataType
    {
        String = 0,
        Int,
        Short,
        Double,
        Bool
    }
    public class DataItem
    {
        public string strItemName;
        public string strItemRemark;
        public DataType dataType;
        public object objValue;
        public bool bVisible;

        public DataItem()
        {
            strItemName = string.Empty;
            strItemRemark = string.Empty;
            dataType = DataType.String;
            bVisible = true;
        }
    }
    public class DataGroup
    {
        public string strGroupName;
        public string strGroupRemark;

        public List<DataItem> listDataItem;
        [XmlIgnore]
        public Dictionary<string, DataItem> dicDataItem;

        public DataGroup()
        {
            strGroupName = string.Empty;
            strGroupRemark = string.Empty;
            listDataItem = new List<DataItem>();
            dicDataItem = new Dictionary<string, DataItem>();
        }
    }
    public class DataDoc
    {
        public List<DataGroup> listDataGroup;
        [XmlIgnore]
        public Dictionary<string, DataGroup> dicDataGroup;
        [XmlIgnore]
        public EventHandler<EventArgs> eventDataSave;

        public DataDoc()
        {
            listDataGroup = new List<DataGroup>();
            dicDataGroup = new Dictionary<string, DataGroup>();
        }
        /// <summary>
        /// 保存词典到XML
        /// </summary>
        /// <param name="bRet"></param>
        /// <returns></returns>
        public static DataDoc LoadDoc(ref bool bRet)
        {
            bRet = true;
            DataDoc pDoc = null;
            FileStream fs = null;

            try
            {
                fs = File.OpenRead(@".//Parameter/Data/SystemData.xml");
                XmlSerializer xml = new XmlSerializer(typeof(DataDoc));
                pDoc = (DataDoc)xml.Deserialize(fs);
                fs.Close();

                pDoc.dicDataGroup = pDoc.listDataGroup.ToDictionary(p => p.strGroupName);
                foreach (DataGroup item in pDoc.listDataGroup)
                {
                    item.dicDataItem = item.listDataItem.ToDictionary(p => p.strItemName);
                }
                bRet = false;
            }
            catch (Exception)
            {
                if (null != fs)
                {
                    fs.Close();
                }
                pDoc = new DataDoc();
            }

            return pDoc;
        }
        /// <summary>
        /// 保存词典到设定路径XML
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <param name="bRet"></param>
        /// <returns></returns>
        public static DataDoc LoadDoc(string strFilePath, ref bool bRet)
        {
            bRet = false;
            DataDoc pDoc = null;
            FileStream fs = null;

            try
            {
                fs = File.OpenRead(strFilePath);
                XmlSerializer xml = new XmlSerializer(typeof(DataDoc));
                pDoc = (DataDoc)xml.Deserialize(fs);
                fs.Close();

                pDoc.dicDataGroup = pDoc.listDataGroup.ToDictionary(p => p.strGroupName);
                foreach (DataGroup item in pDoc.listDataGroup)
                {
                    item.dicDataItem = item.listDataItem.ToDictionary(p => p.strItemName);
                }
                bRet = true;
            }
            catch (Exception)
            {
                if (null != fs)
                {
                    fs.Close();
                }
                pDoc = new DataDoc();
            }

            return pDoc;
        }
        public bool SaveDoc()
        {
            FileStream fs = null;
            try
            {
                if (!Directory.Exists(@".//Parameter/Data/"))
                {
                    Directory.CreateDirectory(@".//Parameter/Data/");
                }
                fs = new FileStream(@".//Parameter/Data/SystemData.xml", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                XmlSerializer xml = new XmlSerializer(typeof(DataDoc));
                xml.Serialize(fs, this);
                fs.Close();
                this.eventDataSave?.Invoke(null, null);
                return true;
            }
            catch (Exception)
            {
                if (null != fs)
                {
                    fs.Close();
                }

                return false;
            }
        }
    }
}
