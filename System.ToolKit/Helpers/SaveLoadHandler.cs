using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ToolKit.Helpers
{
    /// <summary>
    /// 用于序列化不能直接序列的数据
    /// </summary>
    public class SaveLoadHandler
    {
        /// <summary>
        /// 声明保存事件
        /// </summary>
        /// <param name="fileName"></param>
        public delegate void SaveEventHandler(string fileName);
        /// <summary>
        /// 声明加载事件
        /// </summary>
        /// <param name="fileName"></param>
        public delegate void LoadEventHandler(string fileName);
        /// <summary>
        /// 定义保存事件
        /// </summary>
        public event SaveEventHandler saveEvent;
        /// <summary>
        /// 定义加载事件
        /// </summary>
        public event LoadEventHandler loadEvent;

        public void Save(string fileName)
        {
            OnSave(fileName);
        }

        public void Load(string fileName)
        {
            OnLoad(fileName);
        }


        protected void OnSave(string fileName)
        {
            if (saveEvent != null)
            {
                saveEvent(fileName);
            }
        }

        protected void OnLoad(string fileName)
        {
            if (loadEvent != null)
            {
                loadEvent(fileName);
            }
        }
    }
}
