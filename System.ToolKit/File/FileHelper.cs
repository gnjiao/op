using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Reflection;
using System.Linq;

namespace System.ToolKit
{
    public class FileHelper
    {
        #region 属性
        /// <summary>
        /// 获取程序运行路径（路径后面带“\”）
        /// </summary>
        public static string AppPath { get; } = System.AppDomain.CurrentDomain.BaseDirectory;
        #endregion
        #region 私有函数
        private static void MoveDirecory(string sourcePath, string destPath)
        {
            CopyFolder(sourcePath, destPath);
            Directory.Delete(sourcePath, true);
        }
        /// <summary>
        /// 复制文件夹(递归)。如果isContainSourceFolder为真,整个文件夹复制,否则只复制文件夹内的所有文件和目录
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destPath"></param>
        /// <param name="isContainSourceFolder">是否包含源文件夹</param>
        private static void CopyFolder(string sourcePath, string destPath, bool isContainSourceFolder = true)
        {

            Directory.CreateDirectory(destPath);
            if (isContainSourceFolder)
            {
                destPath = Path.Combine(destPath, Path.GetFileName(sourcePath));
                Directory.CreateDirectory(destPath);
            }
            if (!Directory.Exists(sourcePath)) return;

            string[] directories = Directory.GetDirectories(sourcePath);

            if (directories.Length > 0)
            {
                foreach (string dir in directories)
                {
                    CopyFolder(dir, destPath + dir.Substring(dir.LastIndexOf("\\")));
                }
            }
            string[] files = Directory.GetFiles(sourcePath);
            if (files.Length > 0)
            {
                foreach (string file in files)
                {
                    File.Copy(file, destPath + file.Substring(file.LastIndexOf("\\")), true);
                }
            }
        }
      
        #endregion

        //文件或目录属性:大小,是否为空,只读等属性,是否文件或目录,是否存在
        #region 获取一个文件的长度:GetFileSize,GetFileSizeKB,GetFileSizeMB
        /// <summary>
        /// 获取一个文件的长度,单位为Byte
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static int GetFileSize(string filePath)
        {
            //创建一个文件对象
            FileInfo fi = new FileInfo(filePath);

            //获取文件的大小
            return (int)fi.Length;
        }

       
        #endregion
        #region 获取文件夹大小:GetDirectoryLength
        public static long GetDirectoryLength(string dirPath)
        {
            if (!Directory.Exists(dirPath))
                return 0;
            long len = 0;
            DirectoryInfo di = new DirectoryInfo(dirPath);
            foreach (FileInfo fi in di.GetFiles())
            {
                len += fi.Length;
            }
            DirectoryInfo[] dis = di.GetDirectories();
            if (dis.Length > 0)
            {
                for (int i = 0; i < dis.Length; i++)
                {
                    len += GetDirectoryLength(dis[i].FullName);
                }
            }
            return len;
        }
        #endregion

        #region 指定目录是否为空:IsEmptyDirectory
        /// <summary>
        /// 指定目录是否为空
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>        
        public static bool IsEmptyDirectory(string directoryPath)
        {
            try
            {
                //判断是否存在文件
                string[] fileNames = GetFileNames(directoryPath);
                if (fileNames.Length > 0)
                {
                    return false;
                }

                //判断是否存在文件夹
                string[] directoryNames = GetDirNames(directoryPath);
                if (directoryNames.Length > 0)
                {
                    return false;
                }

                return true;
            }
            catch
            {
                //这里记录日志
                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
                return true;
            }
        }
        #endregion
        #region 指定目录中是否存在指定的文件:ContainFile
        /// <summary>
        /// 指定目录中是否存在指定的文件
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param> 
        /// <param name="isSearchChild">是否搜索子目录</param>
        public static bool ContainFile(string directoryPath, string searchPattern, bool isSearchChild = false)
        {
            try
            {
                //获取指定的文件列表
                string[] fileNames = GetFileNames(directoryPath, searchPattern, isSearchChild);

                //判断指定文件是否存在
                if (fileNames.Length == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
            }
        }
        #endregion

        #region  文件只读:FileIsReadOnly,SetFileReadonly
        /// <summary>
        /// 文件是否只读
        /// </summary>
        /// <param name="fullpath"></param>
        /// <returns></returns>
        public static bool FileIsReadOnly(string fullpath)
        {
            FileInfo file = new FileInfo(fullpath);
            if ((file.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 设置文件是否只读
        /// </summary>
        /// <param name="fullpath"></param>
        /// <param name="flag">true表示只读，反之</param>
        public static void SetFileReadonly(string fullpath, bool flag)
        {
            FileInfo file = new FileInfo(fullpath);

            if (flag)
            {
                // 添加只读属性
                file.Attributes |= FileAttributes.ReadOnly;
            }
            else
            {
                // 移除只读属性
                file.Attributes &= ~FileAttributes.ReadOnly;
            }
        }
        #endregion
        #region 获取指定文件详细属性:GetLastWriteTime,GetFileAttibe,GetFileCreateTime
        /// <summary>
        /// 获取文件或文件夹的修改时间
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static DateTime GetLastWriteTime(string path)
        {
            if (IsDir(path))
            {
                FileInfo objFI = new FileInfo(path);
                return objFI.LastWriteTime;
            }
            else
            {
                return Directory.GetLastWriteTime(path);
            }
        }
        /// <summary>
        /// 获取指定文件详细属性
        /// </summary>
        /// <param name="filePath">文件详细路径</param>
        /// <returns></returns>
        public static string GetFileAttibe(string filePath)
        {
            string str = "";
            FileInfo objFI = new FileInfo(filePath);
            str += "详细路径:" + objFI.FullName
                + "<br>文件名称:" + objFI.Name
                + "<br>文件长度:" + objFI.Length.ToString()
                + "字节<br>创建时间" + objFI.CreationTime.ToString()
                + "<br>最后访问时间:" + objFI.LastAccessTime.ToString()
                + "<br>修改时间:" + objFI.LastWriteTime.ToString()
                + "<br>所在目录:" + objFI.DirectoryName
                + "<br>扩展名:" + objFI.Extension;
            return str;
        }
        /// <summary>
        /// 取文件创建时间
        /// </summary>
        /// <param name="fullpath"></param>
        /// <returns></returns>
        public static DateTime GetFileCreateTime(string fullpath)
        {
            FileInfo fi = new FileInfo(fullpath);
            return fi.CreationTime;
        }
        #endregion

        #region 判断是否文件或文件夹:IsDir
        /// <summary>
        /// 判断目标是文件夹还是目录(目录包括磁盘)
        /// </summary>
        /// <param name="path">文件名</param>
        /// <returns></returns>
        public static bool IsDir(string path)
        {
            FileInfo fi = new FileInfo(path);
            return fi.Attributes == FileAttributes.Directory;
        }
        #endregion
        #region IsRoot
        private static bool IsRoot(string path)
        {
            if (!Path.IsPathRooted(path))
            {
                return false;
            } // if
            path = path.TrimEnd(new char[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar });
            return (String.Compare(path, Path.GetPathRoot(path), StringComparison.OrdinalIgnoreCase) == 0);
        }
        #endregion
        #region 文件或文件夹是否存在:Exists,ExistsFile
        /// <summary>
        /// 判断文件或文件夹是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool Exists(string path)
        {
            if (IsDir(path))
            {
                return Directory.Exists(path);
            }
            else
            {
                return File.Exists(path);
            }
        }
        /// <summary>
        /// 检查文件,如果文件不存在则创建  
        /// </summary>
        /// <param name="FilePath">路径,包括文件名</param>
        public static void ExistsFile(string FilePath)
        {
            //if(!File.Exists(FilePath))    
            //File.Create(FilePath);    
            //以上写法会报错,详细解释请看下文.........   
            if (!File.Exists(FilePath))
            {
                FileStream fs = File.Create(FilePath);
                fs.Close();
            }
        }
        #endregion

        #region 获取文本文件的行数
        /// <summary>
        /// 获取文本文件的行数
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static int GetLineCount(string filePath)
        {
            //将文本文件的各行读到一个字符串数组中
            string[] rows = File.ReadAllLines(filePath);

            //返回行数
            return rows.Length;
        }
        #endregion

        //文件操作:创建,写入,移动,复制,删除,重命名,清空
        #region 文件或目录创建:CreateFile,CreateTempZeroByteFile,CreateDirectory
        /// <summary>
        /// 创建一个空文件
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static void CreateFile(string filePath)
        {
            try
            {
                //如果文件不存在则创建该文件
                if (File.Exists(filePath) == false)
                {
                    //创建一个FileInfo对象
                    FileInfo file = new FileInfo(filePath);
                    //创建文件  
                    FileStream fs = file.Create();

                    // File.Create(filePath);

                    //关闭文件流
                    fs.Close();
                }

                //if (!File.Exists(iniFile))
                //{
                //    //1.使用以下语句会导致文件被占用,无法写入
                //    //File.Create(iniFilePath);
                //    //2.创建文件后可以正常使用
                //    using (FileStream fs = new FileStream(iniFile, FileMode.OpenOrCreate, FileAccess.ReadWrite)) { }
                //    //3.创建文件后可以正常使用
                //    //FileStream fs = new FileStream(iniFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);  //创建文件
                //    //fs.Close();
                //}
            }
            catch (Exception ex)
            {
                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
                throw ex;
            }
        }
        /// <summary>
        /// 5.5创建一个文件,并将字节流写入文件
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <param name="buffer">二进制流数据</param>
        public static void CreateFile(string filePath, byte[] buffer)
        {
            try
            {
                //如果文件不存在则创建该文件
                if (File.Exists(filePath) == false)
                {
                    //创建一个FileInfo对象
                    FileInfo file = new FileInfo(filePath);
                    //创建文件
                    FileStream fs = file.Create();
                    //写入二进制流
                    fs.Write(buffer, 0, buffer.Length);
                    //关闭文件流
                    fs.Close();

                    ////创建文件
                    //using (FileStream fs = File.Create(filePath))
                    //{
                    //    //写入二进制流
                    //    fs.Write(buffer, 0, buffer.Length);

                    //}
                }
            }
            catch (Exception ex)
            {
                //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
                throw ex;
            }
        }
        /// <summary>
        /// 创建一个零字节临时文件
        /// </summary>
        /// <returns></returns>
        public static string CreateTempZeroByteFile()
        {
            return Path.GetTempFileName();
        }
        /// <summary>
        /// 创建目录（如果目录不存在则创建该目录,支持创建多级目录）
        /// </summary>
        /// <param name="directoryPath">目录的绝对路径</param>
        public static void CreateDirectory(string directoryPath)
        {
            //如果目录不存在则创建该目录
            if (Directory.Exists(directoryPath) == false)
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
        #endregion
        #region 文件写入:WriteAllText(写入或追加)
        /// <summary>
        /// 文本文件写入(如果目录或文件不存在,则自动创建.默认覆盖写入,如果isAppend=True,则为追加写入)
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="contents"></param>
        /// <param name="isAppend"></param>
        /// <returns></returns>
        public static bool WriteAllText(string filePath, string contents, bool isAppend = false)
        {
            try
            {
                string dir = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(dir))
                {
                    CreateDirectory(dir);
                }
                if (isAppend)
                {
                    File.AppendAllText(filePath, contents, Encoding.Default);
                }
                else
                {
                    File.WriteAllText(filePath, contents, Encoding.Default);
                }
                return true;
            }
            catch (Exception ex)
            {
                //MessageUtil.ShowError($"{ex}");
                return false;
                throw ex;
            }
        }
        #endregion
        #region 移动文件或文件夹到指定目录:Move
        /// <summary>
        /// 移动文件或文件夹到指定目录
        /// </summary>
        /// <param name="sourcePath">需要移动的源文件的绝对路径</param>
        /// <param name="destPath">移动到的目录的绝对路径</param>
        public static bool Move(string sourcePath, string destPath)
        {
            bool isSucceed = true;
            try
            {
                if (IsDir(sourcePath))//判断是文件夹,参数是目录+目录:拷贝目录
                {
                    MoveDirecory(sourcePath, destPath);
                }
                else if (File.Exists(sourcePath))//判断是文件
                {
                    if (File.Exists(destPath))      //参数是文件+文件:拷贝文件,可以修改文件名
                    {
                        File.Move(sourcePath, destPath);
                    }
                    else                            //参数是文件+目录:拷贝文件,不修改文件名
                    {
                        File.Move(sourcePath, Path.Combine(destPath, Path.GetFileName(sourcePath)));
                    }
                }
                isSucceed = true;
            }
            catch (Exception)
            {
                isSucceed = false;
            }
            return isSucceed;
        }
        public static bool Move(string[] sourcePaths, string destPath)
        {
            bool isSucceed = true;
            foreach (var sourcePath in sourcePaths)
            {
                isSucceed = isSucceed && Move(sourcePath, destPath);
            }
            return isSucceed;
        }
        #endregion
        #region 复制文件或文件夹
        /// <summary>
        /// 复制文件或文件夹,如果参数是文件+文件,则复制文件并改名;如果是文件+目录,则复制文件;如果是目录+目录,则复制目录
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="destPath"></param>
        public static void Copy(string sourcePath, string destPath)
        {
            if (IsDir(sourcePath))//判断是文件夹
            {
                CopyFolder(sourcePath, destPath);
            }
            else if (File.Exists(sourcePath))//判断是文件
            {
                if (IsDir(destPath))      //参数是文件+文件:拷贝文件,可以修改文件名//参数是文件+目录:拷贝文件,不修改文件名
                {
                    File.Copy(sourcePath, destPath, true);
                }
                else         //参数是文件+文件:拷贝文件,可以修改文件名
                {
                    File.Copy(sourcePath, Path.Combine(Path.GetDirectoryName(destPath), Path.GetFileName(sourcePath)), true);
                }

            }
        }
        public static void Copy(string[] sourcePaths, string destPath)
        {
            foreach (var sourcePath in sourcePaths)
            {
                Copy(sourcePath, destPath);
            }
        }
        #endregion
        #region 删除文件或目录:Delete
        /// <summary>
        /// 删除文件或目录(不存在则不删除,默认删除到回收站)
        /// </summary>
        /// <param name="path"></param>
        public static void Delete(string path, bool isSendToRecycleBin = false)
        {
            //删除到回收站
            if (isSendToRecycleBin)
            {
          
                return;
            }

            //直接删除
            if (IsDir(path))//判断是文件夹,则删除文件夹
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
            }
            else//判断是文件,则删除文件
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }
        public static void Delete(string[] paths, bool isSendToRecycleBin = false)
        {
            foreach (var path in paths)
            {
                Delete(path, isSendToRecycleBin);
            }
        }
        #endregion
        #region 重命名:ReName
        /// <summary>
        /// 文件或目录重命名
        /// </summary>
        /// <param name="oldPath"></param>
        /// <param name="newPath"></param>
        public static void ReName(string oldPath, string newPath)
        {
            if (File.Exists(oldPath) || Directory.Exists(oldPath))
            {
                FileInfo fileInfo = new FileInfo(oldPath);
                fileInfo.MoveTo(newPath);
            }
        }
        public static void ReName(string dirPath, string oldName, string newName)
        {
            string oldPath = Path.Combine(dirPath, oldName);
            string newPath = Path.Combine(dirPath, newName);
            ReName(oldPath, newPath);
        }
        #endregion
        #region 清空文件或目录:ClearDirectory,ClearFile
        /// <summary>
        /// 7.1清空指定目录下所有文件及子目录,但该目录依然保存.
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        public static void ClearDirectory(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
            {
                //删除目录中所有的文件
                string[] fileNames = GetFileNames(directoryPath);
                for (int i = 0; i < fileNames.Length; i++)
                {
                    Delete(fileNames[i]);
                }

                //删除目录中所有的子目录
                string[] directoryNames = GetDirNames(directoryPath);
                for (int i = 0; i < directoryNames.Length; i++)
                {
                    Delete(directoryNames[i]);
                }
            }
        }
        /// <summary>
        /// 7.2清空文件内容
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static void ClearFile(string filePath)
        {
            //删除文件
            File.Delete(filePath);
            //重新创建该文件
            CreateFile(filePath);
        }
        #endregion
        
        //文件或目录路径:当前名称,上级名称
        #region 文件路径信息:GetExtension,GetFileName,GetDirectoryPath,CombinePath,AppPath
        #region 获取上个目录的路径和名称:GetParent,GetParentName
        /// <summary>
        /// 获取路径所在的目录路径(如果是目录,返回上一级目录路径;如果是文件,返回文件所在目录路径),支持返回第N个上级目录,最多返回到根目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetParent(string path, int level = 1)
        {
            string parentPath = path;
            if (FileHelper.IsDir(path))
            {
                if (Path.GetPathRoot(path) != parentPath)//如果不是根目录,才获取上一级目录
                {
                    parentPath = Directory.GetParent(path).FullName;
                }
            }
            else
            {
                parentPath = Path.GetDirectoryName(path);
            }
            if (level > 1)
            {
                return GetParent(parentPath, level - 1);
            }
            else
            {
                return parentPath;
            }
        }
        /// <summary>
        /// 获取路径所在的目录名称(如果是目录,返回上一级目录名称;如果是文件,返回文件所在目录名称)
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetParentName(string path)
        {
            if (FileHelper.IsDir(path))
            {
                return Directory.GetParent(path).Name;
            }
            else
            {
                return Path.GetFileName(path);
            }
        }
        #endregion
        #region 获取拓展名:GetExtension
        /// <summary>
        /// 从文件的绝对路径中获取扩展名,例如:".cs"
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static string GetExtension(string filePath)
        {
            //获取文件的名称
            FileInfo fi = new FileInfo(filePath);
            return fi.Extension;
        }
        #endregion
        #region  获取文件或目录名称:GetFileName,GetFileNames,GetDirNames
        public static string GetFileName(string fullpath, bool removeExt = false)
        {
            if (removeExt)
                return Path.GetFileNameWithoutExtension(fullpath);
            else
                return Path.GetFileName(fullpath);
        }
        public static string[] GetFileName(string[] fullpath, bool removeExt = false)
        {
            if (removeExt)
                return fullpath.Select(x => Path.GetFileNameWithoutExtension(x)).ToArray();
            else
                return fullpath.Select(x => Path.GetFileName(x)).ToArray();
        }
        /// <summary>
        /// 获取指定目录及子目录中所有文件列表,返回完整路径
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
        /// <param name="isSearchChild">是否搜索子目录</param>
        public static string[] GetFileNames(string directoryPath, string searchPattern = "*.*", bool isSearchChild = false, bool isFullName = true)
        {
            //如果目录不存在，则抛出异常
            if (Directory.Exists(directoryPath) == false)
            {
                throw new FileNotFoundException();
            }

            try
            {
                if (isSearchChild)
                {
                    string[] path = Directory.GetFiles(directoryPath, searchPattern, System.IO.SearchOption.AllDirectories);
                    if (!isFullName)
                    {
                        path = GetFileName(path).ToArray();
                    }
                    return path;

                }
                else
                {
                    string[] path = Directory.GetFiles(directoryPath, searchPattern, System.IO.SearchOption.TopDirectoryOnly);
                    if (!isFullName)
                    {
                        path = GetFileName(path).ToArray();
                    }
                    return path;
                }
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取指定目录及子目录中所有子目录列表
        /// </summary>
        /// <param name="directoryPath">指定目录的绝对路径</param>
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
        /// <param name="isSearchChild">是否搜索子目录</param>
        public static string[] GetDirNames(string directoryPath, string searchPattern = "*.*", bool isSearchChild = false, bool isFullName = true)
        {
            try
            {
                if (isSearchChild)
                {
                    string[] dirs = Directory.GetDirectories(directoryPath, searchPattern, System.IO.SearchOption.AllDirectories);
                    if (!isFullName)
                    {
                        for (int i = 0; i < dirs.Length; i++)
                        {
                            dirs[i] = dirs[i].Substring(dirs[i].LastIndexOf('\\') + 1);
                        }
                    }
                    return dirs;
                }
                else
                {
                    string[] dirs = Directory.GetDirectories(directoryPath, searchPattern, System.IO.SearchOption.TopDirectoryOnly);
                    if (!isFullName)
                    {
                        for (int i = 0; i < dirs.Length; i++)
                        {
                            dirs[i] = dirs[i].Substring(dirs[i].LastIndexOf('\\') + 1);
                        }
                    }
                    return dirs;
                }
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }
        #endregion
        #region 生成路径:CombinePath
        /// <summary>
        /// 生成路径(支持"..\",用于返回上一层目录路径),举例:CombinePath(Application.StartupPath, @"..\..\Backup\PMS.bak")
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="nameOrRelativePath"></param>
        /// <returns></returns>
        public static string CombinePath(string basePath, string nameOrRelativePath)
        {
            while (nameOrRelativePath.Contains(@"..\"))
            {
                nameOrRelativePath = nameOrRelativePath.ReplaceLast(@"..\", "");
                basePath = FileHelper.GetParent(basePath);
            }
            return Path.Combine(basePath, nameOrRelativePath);
        }
        #endregion
 
        #endregion


        #region Stream、byte[] 和 文件之间的转换:StreamToBytes,BytesToStream,StreamToFile,FileToStream,FileToBytes,FileToString,ReadFileFromEmbedded

        /// <summary>
        /// 将流读取到缓冲区中
        /// </summary>
        /// <param name="stream">原始流</param>
        public static byte[] StreamToBytes(Stream stream)
        {
            try
            {
                //创建缓冲区
                byte[] buffer = new byte[stream.Length];

                //读取流
                stream.Read(buffer, 0, Convert.ToInt32(stream.Length));

                //返回流
                return buffer;
            }
            catch (IOException ex)
            {
                throw ex;
            }
            finally
            {
                //关闭流
                stream.Close();
            }
        }

        /// <summary>
        /// 将 byte[] 转成 Stream
        /// </summary>
        public static Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        /// <summary>
        /// 将 Stream 写入文件
        /// </summary>
        public static void StreamToFile(Stream stream, string fileName)
        {
            // 把 Stream 转换成 byte[]
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            // 把 byte[] 写入文件
            FileStream fs = new FileStream(fileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(bytes);
            bw.Close();
            fs.Close();
        }

        /// <summary>
        /// 从文件读取 Stream
        /// </summary>
        public static Stream FileToStream(string fileName)
        {
            // 打开文件
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            // 读取文件的 byte[]
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            // 把 byte[] 转换成 Stream
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        /// <summary>
        /// 将文件读取到缓冲区中
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static byte[] FileToBytes(string filePath)
        {
            //获取文件的大小
            int fileSize = GetFileSize(filePath);

            //创建一个临时缓冲区
            byte[] buffer = new byte[fileSize];

            //创建一个文件流
            FileInfo fi = new FileInfo(filePath);
            FileStream fs = fi.Open(FileMode.Open);

            try
            {
                //将文件流读入缓冲区
                fs.Read(buffer, 0, fileSize);

                return buffer;
            }
            catch (IOException ex)
            {
                throw ex;
            }
            finally
            {
                //关闭文件流
                fs.Close();
            }
        }

        /// <summary>
        /// 将文件读取到字符串中
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static string FileToString(string filePath)
        {
            return FileToString(filePath, Encoding.Default);
        }

        /// <summary>
        /// 将文件读取到字符串中
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        /// <param name="encoding">字符编码</param>
        public static string FileToString(string filePath, Encoding encoding)
        {
            try
            {
                //创建流读取器
                using (StreamReader reader = new StreamReader(filePath, encoding))
                {
                    //读取流
                    return reader.ReadToEnd();
                }
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 从嵌入资源中读取文件内容(e.g: xml).
        /// </summary>
        /// <param name="fileWholeName">嵌入资源文件名，包括项目的命名空间.</param>
        /// <returns>资源中的文件内容.</returns>
        public static string ReadFileFromEmbedded(string fileWholeName)
        {
            string result = string.Empty;

            using (TextReader reader = new StreamReader(
                Assembly.GetExecutingAssembly().GetManifestResourceStream(fileWholeName)))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        #endregion
        #region 获取文件的编码类型:GetEncoding
        /// <summary>
        /// 获取文件编码
        /// </summary>
        /// <param name="filePath">文件绝对路径</param>
        /// <returns></returns>
        public static Encoding GetEncoding(string filePath)
        {
            return GetEncoding(filePath, Encoding.Default);
        }
        /// <summary>
        /// 获取文件编码
        /// </summary>
        /// <param name="filePath">文件绝对路径</param>
        /// <param name="defaultEncoding">找不到则返回这个默认编码</param>
        /// <returns></returns>
        public static Encoding GetEncoding(string filePath, Encoding defaultEncoding)
        {
            Encoding targetEncoding = defaultEncoding;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4))
            {
                if (fs != null && fs.Length >= 2)
                {
                    long pos = fs.Position;
                    fs.Position = 0;
                    int[] buffer = new int[4];
                    //long x = fs.Seek(0, SeekOrigin.Begin);
                    //fs.Read(buffer,0,4);
                    buffer[0] = fs.ReadByte();
                    buffer[1] = fs.ReadByte();
                    buffer[2] = fs.ReadByte();
                    buffer[3] = fs.ReadByte();

                    fs.Position = pos;

                    if (buffer[0] == 0xFE && buffer[1] == 0xFF)//UnicodeBe
                    {
                        targetEncoding = Encoding.BigEndianUnicode;
                    }
                    if (buffer[0] == 0xFF && buffer[1] == 0xFE)//Unicode
                    {
                        targetEncoding = Encoding.Unicode;
                    }
                    if (buffer[0] == 0xEF && buffer[1] == 0xBB && buffer[2] == 0xBF)//UTF8
                    {
                        targetEncoding = Encoding.UTF8;
                    }
                }
            }

            return targetEncoding;
        }
        #endregion
        #region 文件对比:CompareFilesHash
        /// <summary>
        /// 判断两个文件的哈希值是否一致
        /// </summary>
        /// <param name="fileName1"></param>
        /// <param name="fileName2"></param>
        /// <returns></returns>
        public static bool CompareFilesHash(string fileName1, string fileName2)
        {
            using (HashAlgorithm hashAlg = HashAlgorithm.Create())
            {
                using (FileStream fs1 = new FileStream(fileName1, FileMode.Open), fs2 = new FileStream(fileName2, FileMode.Open))
                {
                    byte[] hashBytes1 = hashAlg.ComputeHash(fs1);
                    byte[] hashBytes2 = hashAlg.ComputeHash(fs2);

                    // 比较哈希码
                    return (BitConverter.ToString(hashBytes1) == BitConverter.ToString(hashBytes2));
                }
            }
        }
        #endregion
      
    }




}
