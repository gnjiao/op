using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace System.ToolKit
{
    public static partial class Extensions
    {
        public static void SpiltSelectedTextEx(this RichTextBox rtb, out string selectTextLeft, out string selectText, out string selectTextRight)
        {
            if (rtb.SelectedText != "")
            {
                selectText = rtb.SelectedText;
                selectTextLeft = rtb.Text.Substring(0, rtb.SelectionStart);
                selectTextRight = rtb.Text.Substring(rtb.SelectionStart + rtb.SelectionLength);
            }
            else
            {
                selectTextLeft = "";
                selectText = rtb.Text;
                selectTextRight = "";
            }
        }
        public static int GetLinesCountByBoxEx(this RichTextBox rtb)
        {
            return rtb.GetLineFromCharIndex(rtb.Text.Length) + 1;
        }
        public static int GetLinesCountByTextEx(this RichTextBox rtb)
        {
            return rtb.Lines.Length;
        }
        /// <summary>
        /// 得到当前行的行号,从0开始
        /// </summary>
        /// <param name="rtb"></param>
        /// <returns></returns>
        public static int GetCurrentLineEx(this RichTextBox rtb)
        {
            int index = rtb.GetFirstCharIndexOfCurrentLine();//得到当前行第一个字符的索引
            return rtb.GetLineFromCharIndex(index);//得到当前行的行号,从0开始
        }
        /// <summary>
        /// 得到当前行光标所在位置的索引,从0开始
        /// </summary>
        /// <param name="rtb"></param>
        /// <returns></returns>
        public static int GetCurrentColEx(this RichTextBox rtb)
        {
            int index = rtb.GetFirstCharIndexOfCurrentLine();//得到当前行第一个字符的索引

            //.SelectionStart得到光标所在位置的索引 减去 当前行第一个字符的索引 = 光标所在的列数（从0开始)
            return rtb.SelectionStart - index;
        }
        /// <summary>
        /// 增加带颜色的文本
        /// </summary>
        /// <param name="rtb"></param>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <param name="addNewLine"></param>
        public static void AppendTextColorfulEx(this RichTextBox rtb, string text, Color color, bool addNewLine = true)
        {
            //举例:
            //RichTextBoxHelper.AppendTextColorful(rtxtNotepad, "1111", Color.Green);
            if (addNewLine)
            {
                text += Environment.NewLine;
            }
            rtb.SelectionStart = rtb.TextLength;
            rtb.SelectionLength = 0;
            rtb.SelectionColor = color;
            rtb.AppendText(text);
            rtb.SelectionColor = rtb.ForeColor;
        }
        //public static void InsertImageEx(this RichTextBox rtb, string path = "")
        //{
        //    if (path == "")
        //    {
        //        path = FileDialogHelper.OpenImage();
        //    }
        //    if (path != "")
        //    {
        //        Clipboard.Clear();   //清空剪贴板
        //        Bitmap bmp = new Bitmap(path);  //创建Bitmap类对象
        //        Clipboard.SetImage(bmp);  //将Bitmap类对象写入剪贴板
        //        rtb.Paste();   //将剪贴板中的对象粘贴到RichTextBox1
        //    }
        //}

        #region 文本框滚动条保持在最低端
        public static void ScrollToEnd(this RichTextBox rtb)
        {
            rtb.SelectionStart = rtb.Text.Length;//光标定位到文本最后
            rtb.SelectionLength = 0;
            rtb.ScrollToCaret();//滚动到光标处
        }        
        #endregion
        #region 关键字着色
        /// <summary>
        /// 给关键字着色 - 调用方法
        /// </summary>
        /// <param name="ric">RichTextBox 对象</param>
        /// <param name="Forecolor">前景颜色（默认颜色）</param>
        /// <param name="BunchColor">高亮颜色（把关键字颜色设置成这个颜色）</param>
        public static void SetKeyTextColor(this RichTextBox rtb,string[] keystr, Color Forecolor, Color BunchColor)
        {
            //记录修改位置，修改完要把光标定位回去
            int index = rtb.SelectionStart;
            rtb.SelectAll();
            rtb.SelectionColor = Forecolor;
            
            for (int i = 0; i < keystr.Length; i++)
                Getbunch(keystr[i], rtb.Text, rtb, BunchColor);
            //返回修改的位置
            rtb.Select(index, 0);
            rtb.SelectionColor = Forecolor;
        }
        /// <summary>
        /// 给关键字着色 - 调用方法
        /// </summary>
        /// <param name="ric">RichTextBox 对象</param>
        /// <param name="Forecolor">前景颜色（默认颜色）</param>
        /// <param name="BunchColor">高亮颜色（把关键字颜色设置成这个颜色）</param>
        public static void SetKeyTextColor_CSharp(this RichTextBox rtb, Color Forecolor, Color BunchColor)
        {
            //记录修改位置，修改完要把光标定位回去
            int index = rtb.SelectionStart;
            rtb.SelectAll();
            rtb.SelectionColor = Forecolor;
            //C#关键字 - 还有很多关键字请自行添加
            string[] keystr =
            {
                "abstract ", "enum ", "long ", "stackalloc ","as ", "event ", "namespace ", "static ",
                "base ", "explicit ", "new ", "string ","bool ", "extern ", "null ", "struct ",
                "break ", "false ", "object ", "switch ","byte ", "finally ", "operator ", "this ",
                "case ", "fixed ", "out ", "throw ","catch ", "for ", "params ", "try ",
                "checked ", "foreach ", "private ", "typeof ","class ", "goto ", "protected ", "uint ",
                "const ", "if ", "public ", "ulong ","continue ", "implicit ", "readonly ", "unchecked ",
                "decimal ", "in ", "ref ", "unsafe ","default ", "int ", "return ", "ushort ",
                "delegate ", "interface ", "sbyte ", "using ","do ", "internal ", "sealed ", "virtual ",
                "double ", "is ", "short ", "void ","else ", "lock ", "sizeof ", "while ", "Point ", "Bitmap "
            };
            for (int i = 0; i < keystr.Length; i++)
                Getbunch(keystr[i], rtb.Text, rtb, BunchColor);
            //返回修改的位置
            rtb.Select(index, 0);
            rtb.SelectionColor = Forecolor;
        }
        /// <summary>
        /// 给关键字着色
        /// </summary>
        /// <param name="p">关键字</param>
        /// <param name="s">RichTextBox 内容</param>
        /// <param name="ric">RichTextBox 对象</param>
        /// <param name="BunchColor">高亮颜色</param>
        /// <returns></returns>
        private static int Getbunch(string p, string s, RichTextBox ric, Color BunchColor)
        {
            int cnt = 0;
            int M = p.Length;
            int N = s.Length;
            char[] ss = s.ToCharArray(), pp = p.ToCharArray();
            if (M > N) return 0;
            for (int i = 0; i < N - M + 1; i++)
            {
                int j;
                for (j = 0; j < M; j++)
                {
                    if (ss[i + j] != pp[j]) break;
                }
                if (j == p.Length)
                {
                    ric.Select(i, p.Length);
                    ric.SelectionColor = BunchColor;//关键字颜色
                    cnt++;
                }
            }
            return cnt;
        }
        #endregion
    }
}
