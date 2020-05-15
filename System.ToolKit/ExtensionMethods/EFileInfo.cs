using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Drawing;
using System.ToolKit;
using System.IO;

namespace System.ToolKit
{
    public static partial class Extensions
    {
        #region FileInfo教程
        //FileInfo 类提供创建、复制、删除、移动和打开文件的属性和实例方法，并且帮助创建 FileStream 对象。  此类不能被继承。
        //该类含有以下属性：
        //Attributes 获取或设置当前文件或目录的特性。 
        //CreationTime 获取或设置当前文件或目录的创建时间。 
        //CreationTimeUtc 获取或设置当前文件或目录的创建时间，其格式为协调世界时(UTC)。 
        //Directory 获取父目录的实例。 
        //DirectoryName 获取表示目录的完整路径的字符串。 
        //Exists 获取指示文件是否存在的值。 （重写 FileSystemInfo.Exists。） 
        //Extension 获取表示文件扩展名部分的字符串。 
        //FullName 获取目录或文件的完整目录。  
        //IsReadOnly 获取或设置确定当前文件是否为只读的值。 
        //LastAccessTime 获取或设置上次访问当前文件或目录的时间。 
        //LastAccessTimeUtc 获取或设置上次访问当前文件或目录的时间，其格式为协调世界时 (UTC)。 
        //LastWriteTime 获取或设置上次写入当前文件或目录的时间。 
        //LastWriteTimeUtc 获取或设置上次写入当前文件或目录的时间，其格式为协调世界时 (UTC)。  
        //Length 获取当前文件的大小（字节）。 
        //Name 获取文件名。 （重写 FileSystemInfo.Name。）


        //并有以下方法：
        //AppendText 创建一个 StreamWriter，它向 FileInfo 的此实例表示的文件追加文本。 
        //CopyTo(String) 将现有文件复制到新文件，不允许覆盖现有文件。 
        //CopyTo(String, Boolean) 将现有文件复制到新文件，允许覆盖现有文件。 
        //Create 创建文件。 
        //CreateObjRef 创建一个对象，该对象包含生成用于与远程对象进行通信的代理所需的全部相关信息。 
        //CreateText 创建写入新文本文件的 StreamWriter。 
        //Decrypt 使用 Encrypt 方法解密由当前帐户加密的文件。 
        //Delete 永久删除文件。 
        //Encrypt 将某个文件加密，使得只有加密该文件的帐户才能将其解密。 
        //Equals(Object) 确定指定的对象是否等于当前对象。 
        //GetAccessControl() 获取 FileSecurity 对象，该对象封装当前 FileInfo 对象所描述的文件的访问控制列表 (ACL) 项。
        // GetAccessControl(AccessControlSections)
        // 获取 FileSecurity 对象，该对象封装当前 FileInfo 对象所描述的文件的指定类型的访问控制列表 (ACL) 项。
        //GetHashCode 作为默认哈希函数。  
        //GetLifetimeService 检索控制此实例的生存期策略的当前生存期服务对象。 
        //GetObjectData 设置带有文件名和附加异常信息的 SerializationInfo 对象。 
        //GetType 获取当前实例的 Type。  
        //InitializeLifetimeService 获取控制此实例的生存期策略的生存期服务对象。 
        //MoveTo 将指定文件移到新位置，并提供指定新文件名的选项。 
        //Open(FileMode) 在指定的模式中打开文件。 
        //Open(FileMode, FileAccess) 用读、写或读/写访问权限在指定模式下打开文件。
        // Open(FileMode, FileAccess, FileShare)
        // 用读、写或读/写访问权限和指定的共享选项在指定的模式中打开文件。
        //OpenRead 创建只读 FileStream。 
        //OpenText 创建使用 UTF8 编码、从现有文本文件中进行读取的 StreamReader。 
        //OpenWrite 创建只写 FileStream。 
        //Refresh 刷新对象的状态。 
        //Replace(String, String) 使用当前 FileInfo 对象所描述的文件替换指定文件的内容，这一过程将删除原始文件，并创建被替换文件的备份。
        // Replace(String, String, Boolean)
        // 使用当前 FileInfo 对象所描述的文件替换指定文件的内容，这一过程将删除原始文件，并创建被替换文件的备份。还指定是否忽略合并错误。
        //SetAccessControl 将 FileSecurity 对象所描述的访问控制列表 (ACL) 项应用于当前 FileInfo 对象所描述的文件。 
        //ToString 以字符串形式返回路径。


        //string path = DropDownList1.SelectedValue;
        //DirectoryInfo di = new DirectoryInfo(path);
        ////FileInfo[] files = di.GetFiles();                  //获取文档
        ////DirectoryInfo[] files = di.GetDirectories();      //获取目录（文件夹）
        //FileSystemInfo[] files = di.GetFileSystemInfos();   //获取文档+目录
        //    foreach (FileSystemInfo file in files)
        //    {
        //        TreeNode node = new TreeNode();
        //node.Text = file.ToString();
        //        this.TreeView1.Nodes.Add(node);
        //    }
        #endregion
        public static double GetLength_KB(this FileInfo @this)
        {
            return @this.Length.ToDouble() / 1024;
        }
        public static double GetLength_MB(this FileInfo @this)
        {
            return @this.Length.ToDouble() / 1024 / 1024;
        }
        public static double GetLength_GB(this FileInfo @this)
        {
            return @this.Length.ToDouble() / 1024 / 1024 / 1024;
        }

    }
}
