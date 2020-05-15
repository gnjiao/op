using System.IO;
using System.Windows.Forms;

namespace System.ToolKit
{
    public static partial class Extensions
    {
        #region 增删节点
        /// <summary>
        /// 添加根节点(自动忽略空值)
        /// </summary>
        /// <param name="tv"></param>
        /// <param name="rootNodeNames"></param>
        public static void AddRootNodes(this TreeView tv, params string[] rootNodeNames)
        {
            foreach (var item in rootNodeNames)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    //添加根节点
                    tv.Nodes.Add(item.Trim());
                }
            }
        }
        /// <summary>
        /// 在指定节点上添加子节点(自动忽略空值)
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="rootNodeNames"></param>
        public static void AddRange(this TreeNodeCollection nodes, params string[] rootNodeNames)
        {
            foreach (var item in rootNodeNames)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    //添加根节点
                    nodes.Add(item.Trim());
                }
            }
        }
        public static void AddSelectedSonNode(this TreeView tv, string sonNodeName)
        {
            //要添加的节点名称为空，即文本框是否为空
            if (string.IsNullOrEmpty(sonNodeName.Trim()))
            {
                MessageBox.Show("要添加的节点名称不能为空！");
                return;
            }
            if (tv.SelectedNode == null)
            {
                MessageBox.Show("请选择要添加子节点的节点！");
                return;
            }
            tv.SelectedNode.Nodes.Add(sonNodeName.Trim());
        }
        public static void DeleteSelectedNode(this TreeView tv)
        {
            if (tv.SelectedNode == null)
            {
                MessageBox.Show("请选择要删除的节点！");
                return;
            }
            tv.SelectedNode.Remove();
        }
        #endregion
        #region 展开折叠
        /// <summary>
        /// 展开所有
        /// </summary>
        /// <param name="tv"></param>
        public static void ExpandAll(this TreeView tv)
        {
            tv.SelectedNode = tv.Nodes[0];//定位根节点
            tv.SelectedNode.ExpandAll();//展开控件中的所有节点

        }
        /// <summary>
        /// 展开选定节点的下一级节点
        /// </summary>
        /// <param name="tv"></param>
        public static void ExpandSelectedNode(this TreeView tv)
        {
            tv.SelectedNode.Expand();
        }
        /// <summary>
        /// 折叠所有节点
        /// </summary>
        /// <param name="tv"></param>
        public static void CollapseAll(this TreeView tv)
        {
            tv.SelectedNode = tv.Nodes[0];//定位根节点
            tv.SelectedNode.Collapse();//折叠控件中的所有节点

        }

        #endregion
        #region 展开指定节点
        /// <summary>
        /// 展开指定节点Tag的节点
        /// </summary>
        /// <param name="tv"></param>
        /// <param name="tag"></param>
        public static void ExpandNodeTag(this TreeView tv, string tag)
        {
            //遍历Tree中的所有根节点
            foreach (TreeNode node in tv.Nodes)
            {
                //将每个根节点代入方法进行查找
                TreeNode temp = FindNodeTag(node, tag);
                //找到输出结果
                if (temp != null)
                {
                    //MessageBox.Show(string.Format("找到，深度{0},索引{1}", temp.Level, temp.Index));
                    //temp.BackColor = System.Drawing.Color.FromArgb(51,153,255);//临时增加
                    //temp.ForeColor = Color.White;//临时增加
                    temp.Expand();
                }
            }
        }
        /// <summary>
        /// 展开指定节点名称的节点
        /// </summary>
        /// <param name="tv"></param>
        /// <param name="name"></param>
        public static void ExpandNodeName(this TreeView tv, string name)
        {
            //遍历Tree中的所有根节点
            foreach (TreeNode node in tv.Nodes)
            {
                //将每个根节点代入方法进行查找
                TreeNode temp = FindNodeName(node, name);
                //找到输出结果
                if (temp != null)
                {
                    //MessageBox.Show(string.Format("找到，深度{0},索引{1}", temp.Level, temp.Index));
                    temp.Expand();
                }
            }
        }

        /// <summary>
        /// 递归查询,找到返回该节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="tag"></param>
        /// <returns></returns>
        private static TreeNode FindNodeTag(TreeNode node, string tag)
        {
            //接受返回的节点
            TreeNode ret = null;
            //循环查找
            foreach (TreeNode temp in node.Nodes)
            {
                //是否有子节点
                if (temp.Nodes.Count != 0)
                {
                    //如果找到
                    if ((ret = FindNodeTag(temp, tag)) != null)
                    {
                        return ret;
                    }
                }
                //如果找到
                if (string.Equals(temp.Tag.ToString(), tag))
                {
                    return temp;
                }
                else
                {
                    //temp.BackColor = Color.White;//临时增加
                    //temp.ForeColor = Color.Black;//临时增加
                }
            }
            return ret;
        }
        private static TreeNode FindNodeName(TreeNode node, string name)
        {
            //接受返回的节点
            TreeNode ret = null;
            //循环查找
            foreach (TreeNode temp in node.Nodes)
            {
                //是否有子节点
                if (temp.Nodes.Count != 0)
                {
                    //如果找到
                    if ((ret = FindNodeTag(temp, name)) != null)
                    {
                        return ret;
                    }
                }
                //如果找到
                if (string.Equals(temp.Name, name))
                {
                    return temp;
                }
            }
            return ret;
        }
        #endregion


        #region 树控件显示文件夹内容
        public static void AddDirectory(this TreeView tvw, string dirPath, string searchPattern = "*.*", bool isShowExtension = true)
        {
            TreeNode root = new TreeNode();
            //根目录名称
            root.Text = Path.GetDirectoryName(dirPath);
            //根目录路径
            GetFiles(dirPath, root, searchPattern, isShowExtension);
            tvw.Nodes.Add(root);
        }
        private static void GetFiles(string filePath, TreeNode node, string searchPattern = "*.*", bool isShowExtension = true)
        {
            DirectoryInfo folder = new DirectoryInfo(filePath);
            node.Text = folder.Name;
            node.Tag = folder.FullName;

            FileInfo[] chldFiles = folder.GetFiles(searchPattern);
            foreach (FileInfo chlFile in chldFiles)
            {
                TreeNode chldNode = new TreeNode();
                chldNode.Text = isShowExtension ? chlFile.Name : chlFile.Name.ReplaceReg(@"\.\w+$", "");
                chldNode.Tag = chlFile.FullName;
                node.Nodes.Add(chldNode);
            }
            DirectoryInfo[] chldFolders = folder.GetDirectories();
            foreach (DirectoryInfo chldFolder in chldFolders)
            {
                TreeNode chldNode = new TreeNode();
                chldNode.Text = folder.Name;
                chldNode.Tag = folder.FullName;
                node.Nodes.Add(chldNode);
                GetFiles(chldFolder.FullName, chldNode, searchPattern, isShowExtension);
            }
        }
        #endregion
    }
}
