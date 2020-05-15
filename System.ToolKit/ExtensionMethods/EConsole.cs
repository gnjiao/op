using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ToolKit
{
    public static partial class Extensions
    {
        #region PrintEx:调试输出各种数据类型,包括数组,集合
        //PrintEx方法返回输入参数,仅用于显示输入参数的内容,而且不影响链式编程,太好用了
        
        public static Type PrintEx(this Type input)
        {
            Console.Write(input.GetType() + "="+input.ToString()+"\r\n");
            return input;
        }

        public static int PrintEx(this int input,params object[] inputs)
        {
            Console.Write(input.GetType()+"="+ input);
            foreach (var item in inputs)
            {
                Console.Write("   "+item);
            }
            Console.WriteLine();
            return input;
        }
        public static long PrintEx(this long input,params object[] inputs)
        {
            Console.Write(input.GetType()+"="+ input);
            foreach (var item in inputs)
            {
                Console.Write("   "+item);
            }
            Console.WriteLine();
            return input;
        }
        public static double PrintEx(this double input,params object[] inputs)
        {
            Console.Write(input.GetType()+"="+ input);
            foreach (var item in inputs)
            {
                Console.Write("   "+item);
            }
            Console.WriteLine();
            return input;
        }
        public static bool PrintEx(this bool input,params object[] inputs)
        {
            Console.Write(input.GetType()+"="+ input);
            foreach (var item in inputs)
            {
                Console.Write("   "+item);
            }
            Console.WriteLine();
            return input;
        }
        public static string PrintEx(this string input,params object[] inputs)
        {
            Console.Write(input.GetType()+"="+ input);
            foreach (var item in inputs)
            {
                Console.Write("   "+item);
            }
            Console.WriteLine();
            return input;
        }
        public static T PrintEx<T>(this T input, params T[] inputs)
        {
            Console.Write(input.GetType() + "=" + input);
            foreach (var item in inputs)
            {
                Console.Write("   " + item);
            }
            Console.WriteLine();
            return input;
        }

        public static int[] PrintEx(this int[] input)
        {
            Console.Write(input.GetType() + "=");
            foreach (var item in input)
            {
                Console.Write(item + "   ");
            }
            Console.WriteLine();
            return input;
        }
        public static double[] PrintEx(this double[] input)
        {
            Console.Write(input.GetType() + "=");
            foreach (var item in input)
            {
                Console.Write(item + "   ");
            }
            Console.WriteLine();
            return input;
        }
        public static bool[] PrintEx(this bool[] input)
        {
            Console.Write(input.GetType() + "=");
            foreach (var item in input)
            {
                Console.Write(item + "   ");
            }
            Console.WriteLine();
            return input;
        }
        public static string[] PrintEx(this string[] input)
        {
            Console.Write(input.GetType() + "=");
            foreach (var item in input)
            {
                Console.Write(item + "   ");
            }
            Console.WriteLine();
            return input;
        }
        

        public static int[,] PrintEx(this int[,] input)
        {
            Console.WriteLine(input.GetType() + "=");
            for (int i = 0; i < input.GetLength(0); i++)
            {
                Console.Write("            ");
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    Console.Write(input[i,j]+"    ");
                }
                Console.WriteLine();
            }
            return input;
        }
        public static string[,] PrintEx(this string[,] input)
        {
            Console.WriteLine(input.GetType() + "=");
            for (int i = 0; i < input.GetLength(0); i++)
            {
                Console.Write("            ");
                for (int j = 0; j < input.GetLength(1); j++)
                {
                    Console.Write(input[i,j]+"    ");
                }
                Console.WriteLine();
            }
             return input;   

        }
        public static List<T> PrintEx<T>(this List<T> list)
        {
            Console.Write("List=");
            foreach (var item in list)
            {
                Console.Write(item+"    ");
            }
            Console.WriteLine();
            return list;
        }
        public static List<T[]> PrintEx<T>(this List<T[]> list)
        {
            Console.WriteLine("List<T[]>=");
            foreach (var item in list)
            {
                Console.Write("          ");
                foreach (var i in item)
                {
                    Console.Write(i+"    ");
                }
                Console.WriteLine();
            }
            return list;
        }
        
        public static IEnumerable<T> PrintEx<T>(this IEnumerable<T> list)
        {
            Console.Write("List=");
            foreach (var item in list)
            {
                Console.Write(item+"    ");
            }
            Console.WriteLine();
            return list;
        }

        #endregion
        #region PrintExLine
        public static List<T> PrintLineEx<T>(this List<T> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
            return list;
        }
        #endregion
        #region 弹框显示:PrintMsgBoxEx
        //public static string PrintMsgBoxEx(this string @this)
        //{
        //    System.Windows.Forms.MessageBox.Show(@this.ToString());
        //    return @this;
        //}
        public static T PrintMsgBoxEx<T>(this T @this)
        {
            System.Windows.Forms.MessageBox.Show(@this.ToString());
            return @this;
        }
        #endregion
    }
}
