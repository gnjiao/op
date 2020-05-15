using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Software.Device
{
    public class RawPrinterHelper
    {
        // 结构和API声明 
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

        /// <summary>
        /// 发送byte值给打印机
        /// </summary>
        /// <param name="szPrinterName">打印机名称</param>
        /// <param name="pBytes">byte</param>
        /// <param name="dwCount">字符长度</param>
        /// <returns>成功标记(true为成功)</returns>
        public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
        {
            Int32 dwError = 0, dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            bool bSuccess = false; // 返回标志，默认失败
            di.pDocName = "My Zebra Print File";
            di.pDataType = "RAW";

            // 打开打印机
            if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
            {
                // 开始文档
                if (StartDocPrinter(hPrinter, 1, di))
                {
                    // 开始页
                    if (StartPagePrinter(hPrinter))
                    {
                        // 写比特流
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                        EndPagePrinter(hPrinter);
                    }
                    EndDocPrinter(hPrinter);
                }
                ClosePrinter(hPrinter);
            }
            // 如果不成功，写错误原因
            if (bSuccess == false)
            {
                dwError = Marshal.GetLastWin32Error();
            }
            return bSuccess;
        }

        /// <summary>
        /// 将字符串转换为bytes值，驱动bytes打印函数
        /// </summary>
        /// <param name="szPrinterName"></param>
        /// <param name="szString"></param>
        /// <returns></returns>
        public static bool SendStringToPrinter(string szPrinterName, string szString)
        {
            //for (int i = 0; i < 3; i++)
            //{
            IntPtr pBytes;
            Int32 dwCount;
            //字符串长度
            dwCount = szString.Length;
            // 转换ANSIbyte码
            pBytes = Marshal.StringToCoTaskMemAnsi(szString);
            //驱动byte打印
            SendBytesToPrinter(szPrinterName, pBytes, dwCount);
            Marshal.FreeCoTaskMem(pBytes);
            //}
            return true;
        }
        /// <summary>
        /// 打印凭条设置
        /// </summary>
        /// <param name="width">凭条宽度</param>
        /// <param name="height">凭条高度</param>
        /// <returns>返回ZPL命令</returns>
        public static string ZPL_SetLabel(int width, int height)
        {
            //ZPL条码设置命令：^PW640^LL480
            string sReturn = "^PW{0}^LL{1}";
            return string.Format(sReturn, width, height);
        }
        /// <summary>
        ///  打印矩形
        /// </summary>
        /// <param name="px">起点X坐标</param>
        /// <param name="py">起点Y坐标</param>
        /// <param name="thickness">边框宽度</param>
        /// <param name="width">矩形宽度，0表示打印一条竖线</param>
        /// <param name="height">矩形高度，0表示打印一条横线</param>
        /// <returns>返回ZPL命令</returns>
        public static string ZPL_DrawRectangle(int px, int py, int thickness, int width, int height)
        {
            //ZPL矩形命令：^FO50,50^GB300,200,2^FS
            string sReturn = "^FO{0},{1}^GB{3},{4},{2}^FS";
            return string.Format(sReturn, px, py, thickness, width, height);
        }

        /// <summary>
        /// 打印英文
        /// </summary>
        /// <param name="EnText">待打印文本</param>
        /// <param name="px">起点X坐标</param>
        /// <param name="py">起点Y坐标</param>
        /// <param name="Orient">旋转角度N = normal，R = rotated 90 degrees (clockwise)，I = inverted 180 degrees，B = read from bottom up, 270 degrees</param>
        /// <param name="Height">字体高度</param>
        /// <param name="Width">字体宽度</param>
        /// <returns>返回ZPL命令</returns>
        public static string ZPL_DrawENText(string EnText, int px, int py, string Orient, int Height, int Width)
        {
            //ZPL打印英文命令：^FO50,50^A0N,32,25^FDZEBRA^FS
            string sReturn = "^FO{1},{2}^A0{3},{4},{5}^FD{0}^FS";
            return string.Format(sReturn, EnText, px, py, Orient, Height, Width);
        }

        /// <summary>
        /// 打印中文
        /// </summary>
        /// <param name="px">起点X坐标</param>
        /// <param name="py">起点Y坐标</param>
        /// <param name="FileName">文本字符流</param>
        /// <returns>返回ZPL命令</returns>
        public static string ZPL_DrawCHText(int px, int py, string FileName)
        {
            //ZPL打印英文命令：^FO60,90^XGtemp1,1,1^FS
            string sReturn = "^FO{0},{1}^XG{2},1,1^FS";
            return string.Format(sReturn, px, py, FileName);
        }

        /// <summary>
        /// 打印条形码（128码）
        /// </summary>
        /// <param name="px">起点X坐标</param>
        /// <param name="py">起点Y坐标</param>
        /// <param name="width">基本单元宽度</param>
        /// <param name="ratio">宽窄比</param>
        /// <param name="barheight">条码高度</param>
        /// <param name="barcode">条码内容</param>
        /// <returns>返回ZPL命令</returns>
        public static string ZPL_DrawBarcode(int px, int py, int width, int ratio, int barheight, string barcode)
        {
            //ZPL打印英文命令：^FO50,260^BY1,2^BCN,100,Y,N^FDSMJH2000544610^FS
            string sReturn = "^FO{0},{1}^BY{2},{3}^BCN,{4},N,N^FD{5}^FS";
            return string.Format(sReturn, px, py, width, ratio, barheight, barcode);
        }
        /// <summary>
        /// 打印QR码
        /// </summary>
        /// <param name="px">起点X坐标</param>
        /// <param name="py">起点Y坐标</param>
        /// <param name="width">基本单元宽度</param>
        /// <param name="ratio">宽窄比</param>
        /// <param name="barheight">条码高度</param>
        /// <param name="barcode">条码内容</param>
        /// <returns>返回ZPL命令</returns>
        public static string ZPL_DrawQRcode(int px, int py, string barcode)
        {
            //ZPL打印英文命令：^FO100,100^BQN,2,10^FDMM,AAC-42^FS
            string sReturn = "^FO{0},{1}^BQN,2,10^FDMM,A{2}^FS";
            return string.Format(sReturn, px, py, barcode);
        }
    }
}
