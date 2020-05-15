using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace System.ToolKit
{
    public static partial class Extensions
    {
        #region 字符串初始化:Repeat
        public static string Repeat(this string @this, int repeatCount)
        {
            if (@this.Length == 1)
            {
                return new string(@this[0], repeatCount);
            }

            var sb = new StringBuilder(repeatCount * @this.Length);
            while (repeatCount-- > 0)
            {
                sb.Append(@this);
            }
            return sb.ToString();
        }
        #endregion
        #region 字符串包含:Contains,ContainsAll,ContainsAny
        public static bool Contains(this string @this, string value)
        {
            return @this.IndexOf(value) != -1;
        }
        public static bool Contains(this string @this, string value, StringComparison comparisonType)
        {
            return @this.IndexOf(value, comparisonType) != -1;
        }

        public static bool ContainsAll(this string @this, params string[] values)
        {
            foreach (string value in values)
            {
                if (@this.IndexOf(value) == -1)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool ContainsAll(this string @this, StringComparison comparisonType, params string[] values)
        {
            foreach (string value in values)
            {
                if (@this.IndexOf(value, comparisonType) == -1)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool ContainsAny(this string @this, params string[] values)
        {
            foreach (string value in values)
            {
                if (@this.IndexOf(value) != -1)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool ContainsAny(this string @this, StringComparison comparisonType, params string[] values)
        {
            foreach (string value in values)
            {
                if (@this.IndexOf(value, comparisonType) != -1)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region 判断:IsLike,IsContain,IfEmpty,IsEmpty,IsNotEmpty,IsNullOrEmpty,IsNotNullOrWhiteSpace,IsNullOrWhiteSpace,IsNotNullOrWhiteSpace,NullIfEmpty,IsValid,IsIP,IsUrl,IsLetter,IsAlpha,IsAlphaNumeric,IsNumber,IsChinese,IsPassword,IsPhoneNumber,IsTelephone,IsIDcard,IsPostalcode,IsEmail

        /// <summary>
        ///     A string extension method that query if '@this' satisfy the specified pattern.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="pattern">The pattern to use. Use '*' as wildcard string.</param>
        /// <returns>true if '@this' satisfy the specified pattern, false if not.</returns>
        public static bool IsLike(this string @this, string pattern)
        {
            // Turn the pattern into regex pattern, and match the whole string with ^$
            string regexPattern = "^" + Regex.Escape(pattern) + "$";

            // Escape special character ?, #, *, [], and [!]
            regexPattern = regexPattern.Replace(@"\[!", "[^")
                                       .Replace(@"\[", "[")
                                       .Replace(@"\]", "]")
                                       .Replace(@"\?", ".")
                                       .Replace(@"\*", ".*")
                                       .Replace(@"\#", @"\d");

            return Regex.IsMatch(@this, regexPattern);
        }

        /// <summary>
        ///     A string extension method that if empty.
        /// </summary>
        /// <param name="value">The value to act on.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>A string.</returns>
        public static string IfEmpty(this string value, string defaultValue)
        {
            return (value.IsNotEmpty() ? value : defaultValue);
        }

        public static bool IsEmpty(this string @this)
        {
            return @this == "";
        }
        public static bool IsNotEmpty(this string @this)
        {
            return @this != "";
        }

        public static bool IsNullOrEmpty(this string @this)
        {
            return string.IsNullOrEmpty(@this);
        }
        public static bool IsNotNullOrEmpty(this string @this)
        {
            return !string.IsNullOrEmpty(@this);
        }

        public static Boolean IsNullOrWhiteSpace(this String value)
        {
            return String.IsNullOrWhiteSpace(value);
        }
        public static Boolean IsNotNullOrWhiteSpace(this string value)
        {
            return !String.IsNullOrWhiteSpace(value);
        }

        public static string NullIfEmpty(this string @this)
        {
            return @this == "" ? null : @this;
        }
        /// <summary>
        /// 检查字符串中是否包含非法字符
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
		public static bool IsValid(this string @this)
        {
            if (@this.IndexOf("'") > 0 || @this.IndexOf("&") > 0 || @this.IndexOf("%") > 0 || @this.IndexOf("+") > 0 || @this.IndexOf("\"") > 0 || @this.IndexOf("=") > 0 || @this.IndexOf("!") > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }





        /// <summary>
        /// 验证IP是否正确
        /// </summary>
        /// <param name="IP">IP地址字符串</param>
        /// <returns>方法返回布尔值</returns>
        public static bool IsIP(this string IP)
        {
            string num = "(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)";
            return Regex.IsMatch(IP, ("^" + num + "\\." + num + "\\." + num + "\\." + num + "$"));
        }
        /// <summary>
        /// 验证网址格式是否正确
        /// </summary>
        /// <param name="str_url">网址字符串</param>
        /// <returns>方法返回布尔值</returns>
        public static bool IsUrl(this string str_url)
        {
            return Regex.IsMatch(str_url,
                    @"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
        }
        /// <summary>
        /// 验证字符串是否为大小写字母组成
        /// </summary>
        /// <param name="str_Letter">字符串</param>
        /// <returns>方法返回布尔值</returns>
        public static bool IsLetter(this string str_Letter)
        {
            return System.Text.RegularExpressions.Regex.
                   IsMatch(str_Letter, @"^[A-Za-z]+$");
        }
        /// <summary>
        ///     A string extension method that query if '@this' is Alpha.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if Alpha, false if not.</returns>
        public static bool IsAlpha(this string @this)
        {
            return !Regex.IsMatch(@this, "[^a-zA-Z]");
        }
        /// <summary>
        ///     A string extension method that query if '@this' is Alphanumeric.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>true if Alphanumeric, false if not.</returns>
        public static bool IsAlphaNumeric(this string @this)
        {
            return !Regex.IsMatch(@this, "[^a-zA-Z0-9]");
        }
        /// <summary>
        /// 验证输入是否为数字
        /// </summary>
        /// <param name="str_number">用户输入的字符串</param>
        /// <returns>方法返回布尔值</returns>
        public static bool IsNumber(string str_number)
        {
            return System.Text.RegularExpressions.Regex.
                   IsMatch(str_number, @"^[0-9]*$");
        }
        /// <summary>
        /// 验证字符串是否为汉字
        /// </summary>
        /// <param name="str_chinese">字符串</param>
        /// <returns>方法返回布尔值</returns>
        public static bool IsChinese(string str_chinese)
        {
            return System.Text.RegularExpressions.Regex.
                   IsMatch(str_chinese, @"^[\u4e00-\u9fa5],{0,}$");
        }
        /// <summary>
        /// 验证密码输入条件（数字和26位英文字母）
        /// </summary>
        /// <param name="str_password">密码字符串</param>
        /// <returns>返回布尔值</returns>
        public static bool IsPassword(string str_password)
        {
            return System.Text.RegularExpressions.
                   Regex.IsMatch(str_password, @"[A-Za-z]+[0-9]");
        }
        /// <summary>
        /// 验证手机号是否正确
        /// </summary>
        /// <param name="str_handset">手机号码字符串</param>
        /// <returns>返回布尔值</returns>
        public static bool IsPhoneNumber(string str_handset)
        {
            return System.Text.RegularExpressions.Regex.
                   IsMatch(str_handset, @"^[1]+[3,5]+\d{9}$");
        }
        /// <summary>
        /// 验证电话号码格式是否正确
        /// </summary>
        /// <param name="str_telephone">电话号码信息</param>
        /// <returns>方法返回布尔值</returns>
        public static bool IsTelephone(string str_telephone)
        {
            return System.Text.RegularExpressions.
                   Regex.IsMatch(str_telephone, @"^(\d{3,4}-)?\d{6,8}$");
        }
        /// <summary>
        /// 验证身份证号是否正确
        /// </summary>
        /// <param name="str_idcard">身份证号字符串</param>
        /// <returns>返回布尔值</returns>
        public static bool IsIDcard(string str_idcard)
        {
            return System.Text.RegularExpressions.Regex.
                   IsMatch(str_idcard, @"(^\d{18}$)|(^\d{15}$)");
        }
        /// <summary>
        /// 验证邮编格式是否正确
        /// </summary>
        /// <param name="str_postalcode">邮编字符串</param>
        /// <returns>返回布尔值</returns>
        public static bool IsPostalcode(string str_postalcode)
        {
            return System.Text.RegularExpressions.
                   Regex.IsMatch(str_postalcode, @"^\d{6}$");
        }
        /// <summary>
        /// 验证Email格式是否正确
        /// </summary>
        /// <param name="str_Email">Email地址字符串</param>
        /// <returns>方法返回布尔值</returns>
        public static bool IsEmail(string str_Email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(str_Email,
                    @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        #endregion
        #region 出现次数:GetStringCount,GetCharCount
        /// <summary>
        /// 获取某一字符串在字符串中出现的次数
        /// </summary>
        public static int Count(this string sourceString, string findString)
        {
            int count = 0;
            int findStringLength = findString.Length;
            string subString = sourceString;

            while (subString.IndexOf(findString) >= 0)
            {
                subString = subString.Substring(subString.IndexOf(findString) + findStringLength);
                count += 1;
            }
            return count;
        }
        /// <summary>
        /// 统计char出现在string中的次数
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="chr">字符</param>
        /// <returns></returns>
        public static int Count(this string str, char chr)
        {
            int i = 0;
            for (int j = 0; j < str.Length; j++)
            {
                if (str[j] == chr)
                {
                    i++;
                }
            }
            return i;
        }
        #endregion
        #region 获取字符串信息:GetLength,GetKeyCode,GetChineseCharactersCount,ComputeMD5Hash,TextInfo
        /// <summary> 
        /// 检测含有中文字符串的实际长度 
        /// </summary> 
        /// <param name="str">字符串</param> 
        public static int GetLength(this string str)
        {
            System.Text.ASCIIEncoding n = new System.Text.ASCIIEncoding();
            byte[] b = n.GetBytes(str);
            int l = 0; // l 为字符串之实际长度 
            for (int i = 0; i <= b.Length - 1; i++)
            {
                if (b[i] == 63) //判断是否为汉字或全脚符号 
                {
                    l++;
                }
                l++;
            }
            return l;
        }
        /// <summary>
        /// 获取按键码
        /// </summary>
        /// <param name="cha"></param>
        /// <returns></returns>
        public static int GetKeyCode(string cha)
        {
            switch (cha)
            {
                case "Back":
                    return 8;
                case "Tab":
                    return 9;
                case "Return":
                    return 13;
                case "ShiftKey":
                    return 16;
                case "ControlKey":
                    return 17;
                case "Menu":
                    return 18;
                case "Pause":
                    return 19;
                case "Capital":
                    return 20;
                case "Escape":
                    return 27;
                case "space":
                    return 32;
                case "PageUp":
                    return 33;
                case "Next":
                    return 34;
                case "end":
                    return 35;
                case "home":
                    return 36;
                case "left":
                    return 37;
                case "up":
                    return 38;
                case "right":
                    return 39;
                case "down":
                    return 40;
                case "PrintScreen":
                    return 44;
                case "Insert":
                    return 45;
                case "Delete":
                    return 46;
                case "OemQuestion":
                    return 47;
                case "D0":
                    return 48;
                case "D1":
                    return 49;
                case "D2":
                    return 50;
                case "D3":
                    return 51;
                case "D4":
                    return 52;
                case "D5":
                    return 53;
                case "D6":
                    return 54;
                case "D7":
                    return 55;
                case "D8":
                    return 56;
                case "D9":
                    return 57;
                case "A":
                case "a":
                    return 65;
                case "B":
                case "b":
                    return 66;
                case "C":
                case "c":
                    return 67;
                case "D":
                case "d":
                    return 68;
                case "E":
                case "e":
                    return 69;
                case "F":
                case "f":
                    return 70;
                case "G":
                case "g":
                    return 71;
                case "H":
                case "h":
                    return 72;
                case "I":
                case "i":
                    return 73;
                case "J":
                case "j":
                    return 74;
                case "K":
                case "k":
                    return 75;
                case "L":
                case "l":
                    return 76;
                case "M":
                case "m":
                    return 77;
                case "N":
                case "n":
                    return 78;
                case "O":
                case "o":
                    return 79;
                case "P":
                case "p":
                    return 80;
                case "Q":
                case "q":
                    return 81;
                case "R":
                case "r":
                    return 82;
                case "S":
                case "s":
                    return 83;
                case "T":
                case "t":
                    return 84;
                case "U":
                case "u":
                    return 85;
                case "V":
                case "v":
                    return 86;
                case "W":
                case "w":
                    return 87;
                case "X":
                case "x":
                    return 88;
                case "Y":
                case "y":
                    return 89;
                case "Z":
                case "z":
                    return 90;
                case "RWin":
                    return 91;
                case "NumPad0":
                    return 96;
                case "NumPad1":
                    return 97;
                case "NumPad2":
                    return 98;
                case "NumPad3":
                    return 99;
                case "NumPad4":
                    return 100;
                case "NumPad5":
                    return 101;
                case "NumPad6":
                    return 102;
                case "NumPad7":
                    return 103;
                case "NumPad8":
                    return 104;
                case "NumPad9":
                    return 105;
                case "Multiply":
                    return 106;
                case "Add":
                    return 107;
                case "Subtract":
                    return 109;
                case "Decimal":
                    return 110;
                case "Divide":
                    return 111;
                case "F1":
                    return 112;
                case "F2":
                    return 113;
                case "F3":
                    return 114;
                case "F4":
                    return 115;
                case "F5":
                    return 116;
                case "F6":
                    return 117;
                case "F7":
                    return 118;
                case "F8":
                    return 119;
                case "F9":
                    return 120;
                case "F10":
                    return 121;
                case "F11":
                    return 122;
                case "F12":
                    return 123;
                case "Oem5":
                    return 124;
                case "Oemtilde":
                    return 126;
                case "NumLock":
                    return 144;
                case "Scroll":
                    return 145;
                case "Oem1":
                    return 186;
                case "Oemplus":
                    return 187;
                case "Oemcomma":
                    return 188;
                case "OemMinus":
                    return 189;
                case "OemOpenBrackets":
                    return 219;
                case "Oem6":
                    return 221;
            }
            return -1;
        }
        /// <summary>
        /// 得到字符串中汉字的数量
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>返回 汉字数量</returns>
        public static string GetChineseCharactersCount(this string str)
        {
            //定义值类型变量并赋值为0
            int P_scalar = 0;
            //创建正则表达式对象，用于判断字符是否为汉字
            Regex P_regex = new Regex("^[\u4E00-\u9FA5]{0,}$");
            //遍历字符串中每一个字符
            for (int i = 0; i < str.Length; i++)
            {
                //如果检查的字符是汉字则计数器加1
                P_scalar = P_regex.IsMatch(str[i].ToString()) ? ++P_scalar : P_scalar;
            }
            //返回汉字数量
            return P_scalar.ToString();
        }
        /// <summary> 
        /// 计算字符串的 MD5 哈希。若字符串为空，则返回空，否则返回计算结果。 
        /// </summary> 
        public static string ComputeMD5Hash(this string str)
        {
            string hash = str;

            if (str != null)
            {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] data = Encoding.ASCII.GetBytes(str);
                data = md5.ComputeHash(data);
                hash = "";
                for (int i = 0; i < data.Length; i++)
                    hash += data[i].ToString("x2");
            }
            return hash;
        }
        /// <summary>
        /// 字数统计
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TextInfo(this string str)
        {
            int iAllChr = 0; //字符总数：不计字符'\n'和'\r'
            int iChineseChr = 0; //中文字符计数
            int iChinesePnct = 0;//中文标点计数
            int iEnglishChr = 0; //英文字符计数
            int iEnglishPnct = 0;//中文标点计数
            int iNumber = 0;  //数字字符：0-9
            foreach (char ch in str)
            {
                if (ch != '\n' && ch != '\r') iAllChr++;
                if ("～！＠＃￥％…＆（）—＋－＝".IndexOf(ch) != -1 ||
                 "｛｝【】：“”；‘'《》，。、？｜＼".IndexOf(ch) != -1) iChinesePnct++;
                if (ch >= 0x4e00 && ch <= 0x9fbb) iChineseChr++;
                if ("`~!@#$%^&*()_+-={}[]:\";'<>,.?/\\|".IndexOf(ch) != -1) iEnglishPnct++;
                if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z')) iEnglishChr++;
                if (ch >= '0' && ch <= '9') iNumber++;
            }
            string sStats = string.Format(string.Concat(
             "字符总数：{0}\r\n",
             "中文字符数：{1}\r\n",
             "中文标点数：{2}\r\n",
             "英文字符数：{3}\r\n",
             "英文标点数：{4}\r\n",
             "数字字符数：{5}\r\n"),
             iAllChr.ToString(), iChineseChr.ToString(), iEnglishChr.ToString(),
             iEnglishChr.ToString(), iEnglishPnct.ToString(), iNumber.ToString());
            return sStats;
        }
        #endregion
        #region 字符串索引与分割:Left,Right,Middle
        /// <summary>
        /// 返回字符串左边的内容,length可以是负数,例如:"123456".Left(-1)的值为"12345";如果长度超出字符串长度,则取全部字符
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="length">The length.</param>
        /// <returns>The left part.</returns>
        public static string Left(this string @this, int length)
        {
            if (length >= 0)
            {
                return @this.Substring(0, Math.Min(length, @this.Length));
            }
            else
            {
                return @this.Substring(0, @this.Length + length);
            }

        }
        /// <summary>
        /// 取字符串右边指定长度字符,如果长度超出字符串长度,则取全部字符
        /// </summary>
        /// <param name="this"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Right(this string @this, int length)
        {
            return @this.Substring(Math.Max(0, @this.Length - length));
        }

        /// <summary>
        ///     A string extension method that get the string between the two specified string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="before">The string before to search.</param>
        /// <param name="after">The string after to search.</param>
        /// <returns>The string between the two specified string.</returns>
        public static string Middle(this string @this, string before, string after, bool isContainBeforeString = false, bool isContainAfterString = false)
        {
            int beforeStartIndex = @this.IndexOf(before);
            int startIndex = beforeStartIndex + before.Length;
            int afterStartIndex = @this.IndexOf(after, startIndex);

            if (beforeStartIndex == -1 || afterStartIndex == -1)
            {
                return "";
            }
            string result = @this.Substring(startIndex, afterStartIndex - startIndex);
            return (isContainBeforeString ? before : "")
                    + @this.Substring(startIndex, afterStartIndex - startIndex) + (isContainAfterString ? after : "");
        }

        /// <summary>
        /// 获取匹配文本之前的内容
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Left(this string str, string value, bool isIncludeValue = false)
        {
            int index = str.IndexOf(value);
            if (index == -1)
            {
                return str;
            }
            else
            {
                return str.Substring(0, index) + (isIncludeValue ? value : "");
            }
        }
        /// <summary>
        /// 获取匹配文本之后的内容
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Right(this string str, string value, bool isIncludeValue = false)
        {
            int index = str.IndexOf(value);
            if (index == -1)
            {
                return str;
            }
            else
            {
                return (isIncludeValue ? value : "") + str.Substring(index + value.Length);
            }
        }
        /// <summary>
        /// 从后面开始搜索,获取匹配文本之前的内容
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <param name="isIncludeValue">是否包含匹配文本</param>
        /// <returns></returns>
        public static string LastLeft(this string str, string value, bool isIncludeValue = false)
        {
            int index = str.LastIndexOf(value);
            if (index == -1)
            {
                return str;
            }
            else
            {
                return str.Substring(0, index) + (isIncludeValue ? value : "");
            }
        }
        /// <summary>
        /// 从后面开始搜索,获取匹配文本之后的内容
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <param name="isIncludeValue">是否包含匹配文本</param>
        /// <returns></returns>
        public static string LastRight(this string str, string value, bool isIncludeValue = false)
        {
            int index = str.LastIndexOf(value);
            if (index == -1)
            {
                return str;
            }
            else
            {
                return (isIncludeValue ? value : "") + str.Substring(index + value.Length);
            }
        }
        /// <summary>
        /// 仿Python的字符串索引函数,支持多段截取,例如:s.Sub("1"),s.Sub("7:-1"),s.Sub("2:6;8:10;1")
        /// </summary>
        /// <param name="str"></param>
        /// <param name="subset"></param>
        /// <returns></returns>
        public static string Sub(this string str, string subset)
        {
            //举例:
            //string s = "Python Runoob";
            //s.Sub("1").PrintEx();//y,取单个字符
            //s.Sub("-1").PrintEx();//b,取单个字符
            //s.Sub("1:5").PrintEx();//ytho,取某段,第一个字符到第5个字符,包含第一个字符,不包含第5个字符
            //s.Sub("7:-1").PrintEx();//Runoo,取某段
            //s.Sub(":5").PrintEx();//Pytho,取左边
            //s.Sub("2:").PrintEx();//thon Runoob,取右边
            //s.Sub("2:6;8:10;1").PrintEx();//取多段,分号隔开,thonuny
            //s.Sub("8:;:6").PrintEx();//取多段,分号隔开,unoobPython
            //(s.Sub(":2").Repeat(3) + s.Sub("-2:").Repeat(2)).PrintEx();//PyPyPyobob

            string[] strs = subset.Contains(";") ? subset.Split(';') : new string[] { subset };
            string newStr = "";

            foreach (var item in strs)
            {
                if (item.Contains(":"))//含有：的截取一段字符串
                {
                    string temp = item.Remove((item.IndexOf(':')));
                    int n1 = temp == "" ? 0 : Convert.ToInt32(temp);
                    temp = item.Substring((item.IndexOf(':') + 1));
                    int n2 = temp == "" ? str.Length - 1 : Convert.ToInt32(temp) - 1;
                    n1 = n1 >= 0 ? n1 : str.Length + n1;//负数的处理
                    n2 = n2 >= 0 ? n2 : str.Length + n2;//负数的处理
                    newStr += str.Substring(n1, n2 - n1 + 1);
                }
                else//不含有：的截取一个字符
                {
                    int n1 = Convert.ToInt32(item);
                    n1 = n1 >= 0 ? n1 : str.Length + n1;
                    newStr += str.Substring(n1, 1);
                }
            }
            return newStr;
        }
        #endregion
        #region 字符串删除:RemoveLastString,RemoveEmptyLine,RemoveWhiteSpaceAll,RemoveSpaceAll,RemoveLetter,RemoveNumber,RemoveWhere
        public static string RemoveLastString(this string str, int length = 1)//删除末尾指定长度字符串
        {
            return str.Substring(0, str.Length - length);
        }
        public static string RemoveLastString(this string str, string lastStr)//删除指定字符串及其之后的字符串（即保留指定字符串前面的字符串）。如果不符合则返回原字符串
        {
            if (str.LastIndexOf(lastStr) != -1)
            {
                return str.Substring(0, str.LastIndexOf(lastStr));
            }
            else
            {
                return str;
            }

        }
        public static string RemoveEmptyLine(this string str)
        {
            return Regex.Replace(str, @"\n\s*\n", "\r\n");
        }
        public static string RemoveWhiteSpaceAll(this string str)
        {
            return Replace(str, @"\s", "");
        }
        public static string RemoveSpaceAll(this string str)
        {
            return Replace(str, " ", "", true);
        }
        public static string RemoveLetter(this string @this)
        {
            return new string(@this.ToCharArray().Where(x => !Char.IsLetter(x)).ToArray());
        }
        public static string RemoveNumber(this string @this)
        {
            return new string(@this.ToCharArray().Where(x => !Char.IsNumber(x)).ToArray());
        }
        /// <summary>
        ///     A string extension method that removes the letter.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns>A string.</returns>
        public static string RemoveWhere(this string @this, Func<char, bool> predicate)
        {
            return new string(@this.ToCharArray().Where(x => !predicate(x)).ToArray());
        }
        #endregion
        #region 正则匹配:IsMatch,Match,MatchReg
        public static Boolean IsMatch(this String input, String pattern)
        {
            return Regex.IsMatch(input, pattern);
        }
        public static Boolean IsMatch(this String input, String pattern, RegexOptions options)
        {
            return Regex.IsMatch(input, pattern, options);
        }

        /// <summary>
        /// 字符串匹配,举例:string s = "ldp615".Match("[a-zA-Z]+");
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string Match(this string s, string pattern)
        {
            if (s == null) return "";
            return Regex.Match(s, pattern).Value;
        }

        /// <summary>
        /// 返回匹配值或子匹配值
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="matIndex">0：匹配值;1-6:子匹配值</param>
        /// <returns></returns>
        public static string MatchReg(this string input, string pattern, int matIndex = 0)
        {
            Match match = Regex.Match(input, pattern);
            switch (matIndex)
            {
                case 1: return match.Groups[1].Value;
                case 2: return match.Groups[2].Value;
                case 3: return match.Groups[3].Value;
                case 4: return match.Groups[4].Value;
                case 5: return match.Groups[5].Value;
                case 6: return match.Groups[6].Value;
                default: return match.Value;
            }
        }
        public static string[] MatchesReg(this string input, string pattern, int matIndex = 0)
        {
            return MatchesReg(input, pattern, RegexOptions.None, matIndex);
        }
        /// <summary>
        /// 匹配某个分组的内容
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="mode"></param>
        /// <param name="iGroupIndex">第几个分组, 从1开始, 0代表不分组</param>
        /// <returns></returns>
        public static string[] MatchesReg(this string input, string pattern, RegexOptions mode, int iGroupIndex = 0)
        {
            MatchCollection mc = Regex.Matches(input, pattern, mode);
            string[] strs = new string[mc.Count];
            for (int i = 0; i < mc.Count; i++)
            {
                switch (iGroupIndex)
                {
                    case 1: strs[i] = mc[i].Groups[1].ToString(); break;
                    case 2: strs[i] = mc[i].Groups[2].ToString(); break;
                    case 3: strs[i] = mc[i].Groups[3].ToString(); break;
                    case 4: strs[i] = mc[i].Groups[4].ToString(); break;
                    case 5: strs[i] = mc[i].Groups[5].ToString(); break;
                    case 6: strs[i] = mc[i].Groups[6].ToString(); break;
                    default: strs[i] = mc[i].Value; break;
                }
            }
            return strs;
        }
        /// <summary>
        /// 匹配多个分组的内容
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="iGroupIndices">分组索引数组, 从1开始, 0代表不分组</param>
        /// <returns></returns>
        public static string[,] MatchesReg(this string input, string pattern, params int[] iGroupIndices)
        {
            MatchCollection mc = Regex.Matches(input, pattern);
            string[,] arr = new string[mc.Count, iGroupIndices.Length];
            for (int i = 0; i < mc.Count; i++)
            {
                for (int j = 0; j < iGroupIndices.Length; j++)
                {
                    if (iGroupIndices[j] == 0)
                    {
                        arr[i, j] = mc[i].Value;
                    }
                    else
                    {
                        arr[i, j] = mc[i].Groups[iGroupIndices[j]].ToString();
                    }
                }
            }
            return arr;
        }

        #endregion
        #region 字符串替换:Replace,ReplaceByEmpty,ReplaceWhenEquals,ReplaceLast,ReplaceReg
        public static string Replace(this string str, string oldStr, string newStr, bool replacelAll = true)
        {
            if (replacelAll)
            {
                int startIndex = 0;
                while (str.IndexOf(oldStr, startIndex) != -1)
                {
                    str = str.Replace(oldStr, newStr);
                }
                return str;
            }
            else
            {
                return str.Replace(oldStr, newStr);
            }
        }
        public static string Replace(this string str, string[] oldStrs, string[] newStrs, bool replacelAll = true)
        {
            for (int i = 0; i < oldStrs.Length; i++)
            {
                str = Replace(str, oldStrs[i], newStrs[i], replacelAll);
            }
            return str;
        }

        /// <summary>
        ///     A string extension method that replace all values specified by an empty string.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>A string with all specified values replaced by an empty string.</returns>
        public static string ReplaceByEmpty(this string @this, params string[] values)
        {
            foreach (string value in values)
            {
                @this = @this.Replace(value, "");
            }

            return @this;
        }
        /// <summary>
        ///     A string extension method that replace when equals.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns>The new value if the string equal old value; Otherwise old value.</returns>
        public static string ReplaceWhenEquals(this string @this, string oldValue, string newValue)
        {
            return @this == oldValue ? newValue : @this;
        }

        /// <summary>
        ///     A string extension method that replace last occurence.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="oldValue">The old value.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns>The string with the last occurence of old value replace by new value.</returns>
        public static string ReplaceLast(this string @this, string oldValue, string newValue)
        {
            int startindex = @this.LastIndexOf(oldValue);

            if (startindex == -1)
            {
                return @this;
            }

            return @this.Remove(startindex, oldValue.Length).Insert(startindex, newValue);
        }

        public static string ReplaceReg(this string input, string pattern, string replace)
        {
            Regex reg = new Regex(pattern);
            return reg.Replace(input, replace);
        }
        #endregion
        #region 正则拆分与连接:Join,SplitLines,JoinLines,SplitTo2DArray,SplitReg,SpiltByLength
        public static string Join(this string[] inputs, string separator)
        {
            return String.Join(separator, inputs);
        }
        public static string[] SplitLines(this string str)
        {
            return str.Split('\n');
        }
        public static string JoinLines(this string[] lines)
        {
            return String.Join("\r\n", lines);
        }
        public static string JoinLines(this List<string> sList)
        {
            return String.Join("\r\n", sList.ToArray());
        }
        /// <summary>
        /// 字符串转换为2D数组(字符串按正则表达式rowPattern分行后,排除空行,每行按colPattern拆分,最后转为2D数组)
        /// </summary>
        /// <param name="str"></param>
        /// <param name="colPattern"></param>
        /// <param name="rowPattern"></param>
        /// <param name="isSkipFirstRow">是否跳过第一行</param>
        /// <returns></returns>
        public static string[,] SplitTo2DArray(this string str, string colPattern = ",", string rowPattern = "\n", bool isSkipFirstRow = false)
        {
            //字符串分行后,排除空行,每行按"|"拆分,最后转为2D数组
            string[] data;
            if (isSkipFirstRow)
            {
                data = str.SplitReg(rowPattern).Where(x => x.Length > 0).Skip(1).ToArray();
            }
            else
            {
                data = str.SplitReg(rowPattern).Where(x => x.Length > 0).ToArray();
            }
            return data.Select(x => x.Split('|')).ToArray().To2DArray();
        }

        public static string[] SplitReg(this string input, string pattern)
        {
            Regex reg = new Regex(pattern);
            return reg.Split(input);
        }
        /// <summary>
        /// 分割字符串 按着指定分隔符分割字符串 返回指定个数数组
        /// </summary>
        /// <param name="strContent">字符串</param>
        /// <param name="strSplit">分隔符</param>
        /// <param name="count">数组个数</param>
        /// <returns></returns>
        public static string[] SplitReg(this string input, string pattern, int count)
        {
            string[] result = new string[count];

            string[] splited = SplitReg(input, pattern);

            for (int i = 0; i < count; i++)
            {
                if (i < splited.Length)
                    result[i] = splited[i];
                else
                    result[i] = string.Empty;
            }
            return result;
        }
        /// <summary>
        /// 字符串按长度拆分
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static List<string> SplitByLength(this string str, int length, bool rightToLeft = false)
        {
            List<string> slist = new List<string>();
            int value;
            while (str.Length > 0)
            {
                if (rightToLeft)//从右到左拆分
                {
                    value = str.Length % length == 0 ? length : str.Length % length;
                }
                else//从左到右拆分
                {
                    value = str.Length > length ? length : str.Length;
                }
                slist.Add(str.Substring(0, value));
                str = str.Remove(0, value);
            }
            return slist;
        }
        #endregion
        #region 字符串移位:Shift
        /// <summary>
        /// 字符串移位,正整数为右移,负整数为左移,例如:"1234".Shift(-1)的值为"2341"
        /// </summary>
        /// <param name="this"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Shift(this string @this, int length)
        {
            if (length >= 0)
            {
                return @this.Substring(@this.Length - length) + @this.Substring(0, @this.Length - length);
            }
            else
            {
                return @this.Substring(Math.Abs(length), @this.Length + length) + @this.Substring(0, Math.Abs(length));
            }

        }
        #endregion

        #region 字符串筛选:GetNumberAll,GetEnglishAll,FilterArray,FilterArrayByRow,FilterArrayByColumn
        /// <summary>
        /// 返回字符串中的数字
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string GetNumberAll(this string @this)
        {
            return Regex.Replace(@this, "[a-z]", "", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 返回字符串中的英文
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string GetEnglishAll(this string @this)
        {
            return Regex.Replace(@this, "[0-9]", "", RegexOptions.IgnoreCase);
        }
        public static string[] FilterArray(this string[] @this, string matchString)
        {
            return @this.Where(x => x.IsMatch(matchString)).ToArray();
        }
        public static string[,] FilterArrayByRow(this string[,] @this, string matchString, int index)
        {
            List<string[]> list = new List<string[]>();
            for (int i = 0; i < @this.GetLength(1); i++)
            {
                if (@this[index, i].IsMatch(matchString))
                {
                    list.AddRange(@this.IndexColumn(i));
                }
            }
            return list.To2DArray();
        }
        public static string[,] FilterArrayByColumn(this string[,] @this, string matchString, int index)
        {
            List<string[]> list = new List<string[]>();
            for (int i = 0; i < @this.GetLength(0); i++)
            {
                if (@this[i, index].IsMatch(matchString))
                {
                    list.AddRange(@this.IndexRow(i));
                }
            }
            return list.To2DArray();
        }
        #endregion

        #region 类型转换(与数值无关，但与字符串有关的)
        public static byte[] ToByteArray(this string @this)
        {
            return System.Text.Encoding.Default.GetBytes(@this);
        }

        #region 半角全角转换:ToSBC,ToDBC
        /// <summary>
        /// 转全角(SBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>全角字符串</returns>
        public static string ToSBC(this string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }
        /// <summary>
        /// 转半角(DBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>半角字符串</returns>
        public static string ToDBC(this string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }
        #endregion
        #region 大小写转换相关:Swapcase,Capitalize,ToCamel,ToPascal
        /// <summary>
        /// 大小写互换
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Swapcase(this string str)
        {
            char[] x = { 'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P', 'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'Z', 'X', 'C', 'V', 'B', 'N', 'M' };
            char[] o = { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm' };

            StringBuilder de = new StringBuilder();
            string zi = str;
            for (int i = 0; i < zi.Length; i++)
            {
                int bol = Array.IndexOf(x, zi[i]);
                if (bol == -1)
                {
                    int bol2 = Array.IndexOf(o, zi[i]);
                    if (bol2 == -1)
                    {
                        de.Append(zi[i]);
                        continue;
                    }
                    else
                    {
                        for (int da = 0; da < o.Length; da++)
                        {
                            if (o[da] == zi[i])
                            {
                                de.Append(x[da]);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    for (int da = 0; da < x.Length; da++)
                    {
                        if (x[da] == zi[i])
                        {
                            de.Append(o[da]);
                            break;
                        }
                    }
                }
            }
            return de.ToString();

        }
        /// <summary>
        /// 把字符串的第一个字符大写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Capitalize(this string @this)
        {
            return @this[0].ToUpper() + @this.Substring(1).ToLower();
        }
        public static string ToCamel(this string s)
        {
            if (s.IsNullOrEmpty()) return s;
            return s[0].ToString().ToLower() + s.Substring(1);
        }
        public static string ToPascal(this string s)
        {
            if (s.IsNullOrEmpty()) return s;
            return s[0].ToString().ToUpper() + s.Substring(1);
        }
        #endregion
        //#region 简繁体转换:StringConvert
        ///// <summary>
        ///// 简繁体转换
        ///// </summary>
        ///// <param name="str"></param>
        ///// <param name="type">0:简体转繁体;1:繁体转简体</param>
        ///// <returns></returns>
        //public static string StringConvert(this string str, int type)
        //{
        //    String value = String.Empty;
        //    switch (type)
        //    {
        //        case 0://转繁体
        //            value = Microsoft.VisualBasic.Strings.StrConv(str, Microsoft.VisualBasic.VbStrConv.TraditionalChinese, 0);
        //            break;
        //        case 1:
        //            value = Microsoft.VisualBasic.Strings.StrConv(str, Microsoft.VisualBasic.VbStrConv.SimplifiedChinese, 0);
        //            break;
        //        default:
        //            break;
        //    }
        //    return value;
        //}
        //#endregion

        #region 进制转换相关:字符串进制转换(2,8,10,16):ConvertBase,isBaseNumber
        /// <summary>
        /// 实现各进制数间的转换。ConvertBase("15",10,16)表示将十进制数15转换为16进制的数。
        /// </summary>
        /// <param name="value">要转换的值,即原值</param>
        /// <param name="from">原值的进制,只能是2,8,10,16四个值。</param>
        /// <param name="to">要转换到的目标进制，只能是2,8,10,16四个值。</param>
        public static string ConvertBase(this string value, int from, int to)
        {
            if (!isBaseNumber(from))
                throw new ArgumentException("参数from只能是2,8,10,16四个值。");

            if (!isBaseNumber(to))
                throw new ArgumentException("参数to只能是2,8,10,16四个值。");
            foreach (var item in value.ToCharArray())
            {
                if (item >= Convert.ToString(from, 16).ToCharArray()[0])
                {
                    //throw new ArgumentException("输入参数value有误");
                    return "";
                }
            }
            int intValue = Convert.ToInt32(value, from);  //先转成10进制
            string result = Convert.ToString(intValue, to);  //再转成目标进制
            if (to == 2)
            {
                int resultLength = result.Length;  //获取二进制的长度
                switch (resultLength)
                {
                    case 7:
                        result = "0" + result;
                        break;
                    case 6:
                        result = "00" + result;
                        break;
                    case 5:
                        result = "000" + result;
                        break;
                    case 4:
                        result = "0000" + result;
                        break;
                    case 3:
                        result = "00000" + result;
                        break;
                }
            }
            return result;
        }
        /// <summary>
        /// 判断是否是  2 8 10 16
        /// </summary>
        /// <param name="baseNumber"></param>
        /// <returns></returns>
        private static bool isBaseNumber(int baseNumber = 10)
        {
            if (baseNumber == 2 || baseNumber == 8 || baseNumber == 10 || baseNumber == 16)
                return true;
            return false;
        }
        #endregion

        #region 字符串格式转换:Center
        /// <summary>
        /// 返回一个原字符串居中,并使用空格填充至长度 width 的新字符串
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string Center(this string @this, int length, string fillchar = "-")
        {
            int leftLength = (length - @this.Length) / 2;
            return fillchar.Repeat(leftLength) + @this + fillchar.Repeat(length - @this.Length - leftLength);
        }
        #endregion
        #region 时间转换:ToDateTime
        public static DateTime ToDateTime(this string @this, string format = "yyyyMMdd")
        {
            return DateTime.ParseExact(@this, format, System.Globalization.CultureInfo.CurrentCulture);
        }
        #endregion

        #endregion


        #region 字符串格式化:FormatWith
        /// <summary>
        ///     Replaces the format item in a specified String with the text equivalent of the value of a corresponding
        ///     Object instance in a specified array.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="arg0">The argument 0.</param>
        /// <returns>
        ///     A copy of format in which the format items have been replaced by the String equivalent of the corresponding
        ///     instances of Object in args.
        /// </returns>
        public static String FormatWith(this String @this, System.Object arg0)
        {
            return String.Format(@this, arg0);
        }

        /// <summary>
        ///     Replaces the format item in a specified String with the text equivalent of the value of a corresponding
        ///     Object instance in a specified array.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="arg0">The argument 0.</param>
        /// <param name="arg1">The first argument.</param>
        /// <returns>
        ///     A copy of format in which the format items have been replaced by the String equivalent of the corresponding
        ///     instances of Object in args.
        /// </returns>
        public static String FormatWith(this String @this, System.Object arg0, System.Object arg1)
        {
            return String.Format(@this, arg0, arg1);
        }

        /// <summary>
        ///     Replaces the format item in a specified String with the text equivalent of the value of a corresponding
        ///     Object instance in a specified array.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="arg0">The argument 0.</param>
        /// <param name="arg1">The first argument.</param>
        /// <param name="arg2">The second argument.</param>
        /// <returns>
        ///     A copy of format in which the format items have been replaced by the String equivalent of the corresponding
        ///     instances of Object in args.
        /// </returns>
        public static String FormatWith(this String @this, System.Object arg0, System.Object arg1, System.Object arg2)
        {
            return String.Format(@this, arg0, arg1, arg2);
        }

        /// <summary>
        ///     Replaces the format item in a specified String with the text equivalent of the value of a corresponding
        ///     Object instance in a specified array.
        /// </summary>
        /// <param name="this">A String containing zero or more format items.</param>
        /// <param name="values">An Object array containing zero or more objects to format.</param>
        /// <returns>
        ///     A copy of format in which the format items have been replaced by the String equivalent of the corresponding
        ///     instances of Object in args.
        /// </returns>
        public static string FormatWith(this string @this, params object[] values)
        {
            return String.Format(@this, values);
        }
        #endregion

        #region 字符串反转Reverse
        public static string Reverse(this string @this)
        {
            if (@this.Length <= 1)
            {
                return @this;
            }

            char[] chars = @this.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }
        #endregion
        #region Br和Nl的转换:Br2Nl,Nl2Br
        /// <summary>
        ///     A string extension method that line break 2 newline.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A string.</returns>
        public static string Br2Nl(this string @this)
        {
            return @this.Replace("<br />", "\r\n").Replace("<br>", "\r\n");
        }
        /// <summary>
        ///     A string extension method that newline 2 line break.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>A string.</returns>
        public static string Nl2Br(this string @this)
        {
            return @this.Replace("\r\n", "<br />").Replace("\n", "<br />");
        }
        #endregion

        #region CompareOrdinal
        /// <summary>
        ///     Compares two specified  objects by evaluating the numeric values of the corresponding  objects in each string.
        /// </summary>
        /// <param name="strA">The first string to compare.</param>
        /// <param name="strB">The second string to compare.</param>
        /// <returns>
        ///     An integer that indicates the lexical relationship between the two comparands.ValueCondition Less than zero
        ///     is less than . Zero  and  are equal. Greater than zero  is greater than .
        /// </returns>
        public static Int32 CompareOrdinal(this String strA, String strB)
        {
            return String.CompareOrdinal(strA, strB);
        }

        /// <summary>
        ///     Compares substrings of two specified  objects by evaluating the numeric values of the corresponding  objects
        ///     in each substring.
        /// </summary>
        /// <param name="strA">The first string to use in the comparison.</param>
        /// <param name="indexA">The starting index of the substring in .</param>
        /// <param name="strB">The second string to use in the comparison.</param>
        /// <param name="indexB">The starting index of the substring in .</param>
        /// <param name="length">The maximum number of characters in the substrings to compare.</param>
        /// <returns>
        ///     A 32-bit signed integer that indicates the lexical relationship between the two comparands.ValueCondition
        ///     Less than zero The substring in  is less than the substring in . Zero The substrings are equal, or  is zero.
        ///     Greater than zero The substring in  is greater than the substring in .
        /// </returns>
        public static Int32 CompareOrdinal(this String strA, Int32 indexA, String strB, Int32 indexB, Int32 length)
        {
            return String.CompareOrdinal(strA, indexA, strB, indexB, length);
        }
        #endregion
        #region Intern,IsInterned
        public static String Intern(this String str)
        {
            return String.Intern(str);
        }
        public static String IsInterned(this String str)
        {
            return String.IsInterned(str);
        }
        #endregion

        #region Char相关,布尔判断:ConvertToUtf32,GetNumericValue,GetUnicodeCategory....
        public static Int32 ConvertToUtf32(this String s, Int32 index)
        {
            return Char.ConvertToUtf32(s, index);
        }
        public static Double GetNumericValue(this String s, Int32 index)
        {
            return Char.GetNumericValue(s, index);
        }
        public static UnicodeCategory GetUnicodeCategory(this String s, Int32 index)
        {
            return Char.GetUnicodeCategory(s, index);
        }
        public static Boolean IsControl(this String s, Int32 index)
        {
            return Char.IsControl(s, index);
        }
        public static Boolean IsDigit(this String s, Int32 index)
        {
            return Char.IsDigit(s, index);
        }
        public static Boolean IsHighSurrogate(this String s, Int32 index)
        {
            return Char.IsHighSurrogate(s, index);
        }
        public static Boolean IsLetter(this String s, Int32 index)
        {
            return Char.IsLetter(s, index);
        }
        public static Boolean IsLetterOrDigit(this String s, Int32 index)
        {
            return Char.IsLetterOrDigit(s, index);
        }
        public static Boolean IsLower(this String s, Int32 index)
        {
            return Char.IsLower(s, index);
        }
        public static Boolean IsLowSurrogate(this String s, Int32 index)
        {
            return Char.IsLowSurrogate(s, index);
        }
        public static Boolean IsNumber(this String s, Int32 index)
        {
            return Char.IsNumber(s, index);
        }
        public static Boolean IsPunctuation(this String s, Int32 index)
        {
            return Char.IsPunctuation(s, index);
        }
        public static Boolean IsSeparator(this String s, Int32 index)
        {
            return Char.IsSeparator(s, index);
        }
        public static Boolean IsSurrogate(this String s, Int32 index)
        {
            return Char.IsSurrogate(s, index);
        }
        public static Boolean IsSurrogatePair(this String s, Int32 index)
        {
            return Char.IsSurrogatePair(s, index);
        }
        public static Boolean IsSymbol(this String s, Int32 index)
        {
            return Char.IsSymbol(s, index);
        }
        public static Boolean IsUpper(this String s, Int32 index)
        {
            return Char.IsUpper(s, index);
        }
        public static Boolean IsWhiteSpace(this String s, Int32 index)
        {
            return Char.IsWhiteSpace(s, index);
        }
        #endregion

        #region 文件相关:SaveAs,ToDirectoryInfo,ToFileInfo
        /// <summary>
        ///     A string extension method that save the string into a file.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="fileName">Filename of the file.</param>
        /// <param name="append">(Optional) if the text should be appended to file file if it's exists.</param>
        public static void SaveAs(this string @this, string fileName, bool append = false)
        {
            using (TextWriter tw = new StreamWriter(fileName, append))
            {
                tw.Write(@this);
            }
        }

        /// <summary>
        ///     A string extension method that save the string into a file.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="file">The FileInfo.</param>
        /// <param name="append">(Optional) if the text should be appended to file file if it's exists.</param>
        public static void SaveAs(this string @this, FileInfo file, bool append = false)
        {
            using (TextWriter tw = new StreamWriter(file.FullName, append))
            {
                tw.Write(@this);
            }
        }

        /// <summary>
        ///     A string extension method that converts the @this to a directory information.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a DirectoryInfo.</returns>
        public static DirectoryInfo ToDirectoryInfo(this string @this)
        {
            return new DirectoryInfo(@this);
        }
        /// <summary>
        ///     A string extension method that converts the @this to a file information.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <returns>@this as a FileInfo.</returns>
        public static FileInfo ToFileInfo(this string @this)
        {
            return new FileInfo(@this);
        }
        #endregion

        #region HTML相关操作:ClearTag,ConvertToJS,ReplaceNbsp,StringToHtml,AcquireAssignString,GetLetter,AddBlankAtForefront,DelHtmlString,DelTag,DelTagArray,GetAllURL,GetAllLinkText
        public static string ClearTag(string sHtml, string sRegex = @"(<[^>\s]*\b(\w)+\b[^>]*>)|(<>)|(&nbsp;)|(&gt;)|(&lt;)|(&amp;)|\r|\n|\t")
        {
            string sTemp = sHtml;
            Regex re = new Regex(sRegex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
            return re.Replace(sHtml, "");
        }
        public static string ConvertToJS(string sHtml)
        {
            StringBuilder sText = new StringBuilder();
            Regex re;
            re = new Regex(@"\r\n", RegexOptions.IgnoreCase);
            string[] strArray = re.Split(sHtml);
            foreach (string strLine in strArray)
            {
                sText.Append("document.writeln(\"" + strLine.Replace("\"", "\\\"") + "\");\r\n");
            }
            return sText.ToString();
        }
        public static string ReplaceNbsp(string str)
        {
            string sContent = str;
            if (sContent.Length > 0)
            {
                sContent = sContent.Replace(" ", "");
                sContent = sContent.Replace("&nbsp;", "");
                sContent = "&nbsp;&nbsp;&nbsp;&nbsp;" + sContent;
            }
            return sContent;
        }
        public static string StringToHtml(string str)
        {
            string sContent = str;
            if (sContent.Length > 0)
            {
                char csCr = (char)13;
                sContent = sContent.Replace(csCr.ToString(), "<br>");
                sContent = sContent.Replace(" ", "&nbsp;");
                sContent = sContent.Replace("　", "&nbsp;&nbsp;");
            }
            return sContent;
        }

        //截取长度并转换为HTML
        public static string AcquireAssignString(string str, int num)
        {
            string sContent = str;
            sContent = GetLetter(sContent, num, false);
            sContent = StringToHtml(sContent);
            return sContent;
        }
        //截取长度,num是英文字母的总数，一个中文算两个英文
        public static string GetLetter(string str, int iNum, bool bAddDot)
        {
            if (str == null || iNum <= 0) return "";

            if (str.Length < iNum && str.Length * 2 < iNum)
            {
                return str;
            }

            string sContent = str;
            int iTmp = iNum;

            char[] arrC;
            if (sContent.Length >= iTmp) //防止因为中文的原因使ToCharArray溢出
            {
                arrC = str.ToCharArray(0, iTmp);
            }
            else
            {
                arrC = str.ToCharArray(0, sContent.Length);
            }

            int i = 0;
            int iLength = 0;
            foreach (char ch in arrC)
            {
                iLength++;

                int k = (int)ch;
                if (k > 127 || k < 0)
                {
                    i += 2;
                }
                else
                {
                    i++;
                }

                if (i > iTmp)
                {
                    iLength--;
                    break;
                }
                else if (i == iTmp)
                {
                    break;
                }
            }

            if (iLength < str.Length && bAddDot)
                sContent = sContent.Substring(0, iLength - 3) + "...";
            else
                sContent = sContent.Substring(0, iLength);

            return sContent;
        }
        public static string AddBlankAtForefront(string str)
        {
            string sContent = str;
            return sContent;
        }

        /// <summary>
        /// 删除所有的html标记 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DelHtmlString(string str)
        {
            string[] Regexs =
                                {
                                    @"<script[^>]*?>.*?</script>",
                                    @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
                                    @"([\r\n])[\s]+",
                                    @"&(quot|#34);",
                                    @"&(amp|#38);",
                                    @"&(lt|#60);",
                                    @"&(gt|#62);",
                                    @"&(nbsp|#160);",
                                    @"&(iexcl|#161);",
                                    @"&(cent|#162);",
                                    @"&(pound|#163);",
                                    @"&(copy|#169);",
                                    @"&#(\d+);",
                                    @"-->",
                                    @"<!--.*\n"
                                };

            string[] Replaces =
                                {
                                    "",
                                    "",
                                    "",
                                    "\"",
                                    "&",
                                    "<",
                                    ">",
                                    " ",
                                    "\xa1", //chr(161),
                                    "\xa2", //chr(162),
                                    "\xa3", //chr(163),
                                    "\xa9", //chr(169),
                                    "",
                                    "\r\n",
                                    ""
                                };

            string s = str;
            for (int i = 0; i < Regexs.Length; i++)
            {
                s = new Regex(Regexs[i], RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(s, Replaces[i]);
            }
            s.Replace("<", "");
            s.Replace(">", "");
            s.Replace("\r\n", "");
            return s;
        }

        /// <summary>
        /// 删除字符串中的特定标记 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="tag"></param>
        /// <param name="isContent">是否清除内容 </param>
        /// <returns></returns>
        public static string DelTag(string str, string tag, bool isContent)
        {
            if (tag == null || tag == " ")
            {
                return str;
            }

            if (isContent) //要求清除内容 
            {
                return Regex.Replace(str, string.Format("<({0})[^>]*>([\\s\\S]*?)<\\/\\1>", tag), "", RegexOptions.IgnoreCase);
            }

            return Regex.Replace(str, string.Format(@"(<{0}[^>]*(>)?)|(</{0}[^>] *>)|", tag), "", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 删除字符串中的一组标记 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="tagA"></param>
        /// <param name="isContent">是否清除内容 </param>
        /// <returns></returns>
        public static string DelTagArray(string str, string tagA, bool isContent)
        {

            string[] tagAa = tagA.Split(',');

            foreach (string sr1 in tagAa) //遍历所有标记，删除 
            {
                str = DelTag(str, sr1, isContent);
            }
            return str;

        }

        /// <summary>
        /// 取得所有链接URL
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string GetAllURL(string html)
        {
            StringBuilder sb = new StringBuilder();
            Match m = Regex.Match(html.ToLower(), "<a href=(.*?)>.*?</a>");

            while (m.Success)
            {
                sb.AppendLine(m.Result("$1"));
                m.NextMatch();
            }

            return sb.ToString();
        }

        /// <summary>
        /// 获取所有连接文本
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string GetAllLinkText(string html)
        {
            StringBuilder sb = new StringBuilder();
            Match m = Regex.Match(html.ToLower(), "<a href=.*?>(1,100})</a>");

            while (m.Success)
            {
                sb.AppendLine(m.Result("$1"));
                m.NextMatch();
            }

            return sb.ToString();
        }
        #endregion HTML相关操作
    }
}
