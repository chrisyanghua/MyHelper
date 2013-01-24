using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MyHelper4Web
{
    /// <summary>
    /// 字符串操作类
    /// </summary>
    public class MyStringHelper
    {
        #region 常规字符串操作

        /// <summary>
        /// 检查字符串是否为空
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>返回值</returns>
        public static bool IsEmpty(string str)
        {
            if (string.IsNullOrEmpty(str))
                return true;
            return false;
        }

        /// <summary>
        /// 检查字符串中是否包含非法字符
        /// </summary>
        /// <param name="s">单字符</param>
        /// <returns>返回值</returns>
        public static bool CheckValidity(string s)
        {
            string str = s;
            if (str.IndexOf("'") > 0 || str.IndexOf("&") > 0 || str.IndexOf("%") > 0 || str.IndexOf("+") > 0 ||
                str.IndexOf("\"") > 0 || str.IndexOf("=") > 0 || str.IndexOf("!") > 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 把价格精确至小数点两位
        /// </summary>
        /// <param name="dPrice">价格</param>
        /// <returns>返回值</returns>
        public static string TransformPrice(double dPrice)
        {
            double d = dPrice;
            var myNfi = new NumberFormatInfo { NumberNegativePattern = 2 };
            string s = d.ToString("N", myNfi);
            return s;
        }

        /// <summary> 
        /// 检测含有中文字符串的实际长度 
        /// </summary> 
        /// <param name="str">字符串</param> 
        /// <returns>返回值</returns>
        public static int GetLength(string str)
        {
            var n = new ASCIIEncoding();
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
        /// 截取长度,num是英文字母的总数，一个中文算两个英文
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="iNum">英文字母的总数</param>
        /// <param name="bAddDot">是否计算标点</param>
        /// <returns>返回值</returns>
        public static string GetLetter(string str, int iNum, bool bAddDot)
        {
            if (str == null || iNum <= 0) return "";

            if (str.Length < iNum && str.Length * 2 < iNum)
            {
                return str;
            }

            string sContent = str;
            int iTmp = iNum;

            char[] arrC = str.ToCharArray(0, sContent.Length >= iTmp ? iTmp : sContent.Length);

            int i = 0;
            int iLength = 0;
            foreach (char ch in arrC)
            {
                iLength++;

                int k = ch;
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
                if (i == iTmp)
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

        /// <summary>
        /// 获取日期字符串
        /// </summary>
        /// <param name="dt">日期时间</param>
        /// <returns>返回值</returns>
        public static string GetDateString(DateTime dt)
        {
            return dt.Year.ToString() + dt.Month.ToString().PadLeft(2, '0') + dt.Day.ToString().PadLeft(2, '0');
        }

        /// <summary>
        /// 根据指定字符，截取相应字符串
        /// </summary>
        /// <param name="sOrg">源字符串</param>
        /// <param name="sLast">指定字符串</param>
        /// <returns>返回值</returns>
        public static string GetStrByLast(string sOrg, string sLast)
        {
            int iLast = sOrg.LastIndexOf(sLast);
            if (iLast > 0)
                return sOrg.Substring(iLast + 1);
            return sOrg;
        }

        /// <summary>
        /// 根据指定字符，截取相应字符串
        /// </summary>
        /// <param name="sOrg">源字符串</param>
        /// <param name="sLast">指定字符串</param>
        /// <returns>返回值</returns>
        public static string GetPreStrByLast(string sOrg, string sLast)
        {
            int iLast = sOrg.LastIndexOf(sLast);
            if (iLast > 0)
                return sOrg.Substring(0, iLast);
            return sOrg;
        }

        /// <summary>
        /// 根据指定字符，截取相应字符串
        /// </summary>
        /// <param name="sOrg">源字符串</param>
        /// <param name="sEnd">终止字符串</param>
        /// <returns>返回值</returns>
        public static string RemoveEndWith(string sOrg, string sEnd)
        {
            if (sOrg.EndsWith(sEnd))
                sOrg = sOrg.Remove(sOrg.IndexOf(sEnd), sEnd.Length);
            return sOrg;
        }

        #endregion  常规字符串操作

        #region HTML相关操作

        /// <summary>
        /// 清除HTML标记
        /// </summary>
        /// <param name="sHtml">HTML标记</param>
        /// <returns>返回值</returns>
        public static string ClearTag(string sHtml)
        {
            if (sHtml == "")
                return "";
            var re = new Regex(@"(<[^>\s]*\b(\w)+\b[^>]*>)|(<>)|(&nbsp;)|(&gt;)|(&lt;)|(&amp;)|\r|\n|\t",
                               RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
            return re.Replace(sHtml, "");
        }

        /// <summary>
        /// 根据正则清除HTML标记
        /// </summary>
        /// <param name="sHtml">HTML标记</param>
        /// <param name="sRegex">正则</param>
        /// <returns>返回值</returns>
        public static string ClearTag(string sHtml, string sRegex)
        {
            var re = new Regex(sRegex,
                               RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
            return re.Replace(sHtml, "");
        }

        /// <summary>
        /// 转化成JS
        /// </summary>
        /// <param name="sHtml">HTML标记</param>
        /// <returns>返回值</returns>
        public static string ConvertToJS(string sHtml)
        {
            var sText = new StringBuilder();
            var re = new Regex(@"\r\n", RegexOptions.IgnoreCase);
            string[] strArray = re.Split(sHtml);
            foreach (string strLine in strArray)
            {
                sText.Append("document.writeln(\"" + strLine.Replace("\"", "\\\"") + "\");\r\n");
            }
            return sText.ToString();
        }

        /// <summary>
        /// 替换空格
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>返回值</returns>
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

        /// <summary>
        /// 字符串转HTML
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>返回值</returns>
        public static string StringToHtml(string str)
        {
            string sContent = str;
            if (sContent.Length > 0)
            {
                const char csCr = (char)13;
                sContent = sContent.Replace(csCr.ToString(), "<br>");
                sContent = sContent.Replace(" ", "&nbsp;");
                sContent = sContent.Replace("　", "&nbsp;&nbsp;");
            }
            return sContent;
        }

        /// <summary>
        /// 截取长度并转换为HTML
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="num">长度</param>
        /// <returns>返回值</returns>
        public static string AcquireAssignString(string str, int num)
        {
            string sContent = str;
            sContent = GetLetter(sContent, num, false);
            sContent = StringToHtml(sContent);
            return sContent;
        }

        /// <summary>
        /// 此方法与AcquireAssignString的功能已经一样，为了不报错，故保留此方法
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="num">长度</param>
        /// <returns>返回值</returns>
        public static string TranslateToHtmlString(string str, int num)
        {
            string sContent = str;
            sContent = GetLetter(sContent, num, false);
            sContent = StringToHtml(sContent);
            return sContent;
        }

        /// <summary>
        /// 删除所有的html标记 
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>返回值</returns>
        public static string DelHtmlString(string str)
        {
            string[] regexs =
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

            string[] replaces =
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
            for (int i = 0; i < regexs.Length; i++)
            {
                s = new Regex(regexs[i], RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(s, replaces[i]);
            }
            return s.Replace("<", "").Replace(">", "").Replace("\r\n", "");
        }

        /// <summary>
        /// 删除字符串中的特定标记 
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="tag">标签</param>
        /// <param name="isContent">是否清除内容 </param>
        /// <returns>返回值</returns>
        public static string DelTag(string str, string tag, bool isContent)
        {
            if (tag == null || tag == " ")
            {
                return str;
            }

            if (isContent) //要求清除内容 
            {
                return Regex.Replace(str, string.Format("<({0})[^>]*>([\\s\\S]*?)<\\/\\1>", tag), "",
                                     RegexOptions.IgnoreCase);
            }

            return Regex.Replace(str, string.Format(@"(<{0}[^>]*(>)?)|(</{0}[^>] *>)|", tag), "",
                                 RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 删除字符串中的一组标记 
        /// </summary>
        /// <param name="str">字符串</param>
        /// <param name="tagA">标签</param>
        /// <param name="isContent">是否清除内容 </param>
        /// <returns>返回值</returns>
        public static string DelTagArray(string str, string tagA, bool isContent)
        {
            string[] tagAa = tagA.Split(',');

            return tagAa.Aggregate(str, (current, sr1) => DelTag(current, sr1, isContent));
        }

        #endregion HTML相关操作

        #region 其他字符串操作

        /// <summary>
        /// 格式化为版本号字符串
        /// </summary>
        /// <param name="sVersion">版本号</param>
        /// <returns>返回值</returns>
        public static string SetVersionFormat(string sVersion)
        {
            if (string.IsNullOrEmpty(sVersion)) return "";
            int n = 0, k = 0;

            while (n < 4 && k > -1)
            {
                k = sVersion.IndexOf(".", k + 1);
                n++;
            }
            string stmVersion = k > 0 ? sVersion.Substring(0, k) : sVersion;

            return stmVersion;
        }

        /// <summary>
        /// 在前面补0
        /// </summary>
        /// <param name="sheep">数字</param>
        /// <param name="length">补0长度</param>
        /// <returns>返回值</returns>
        public static string AddZero(int sheep, int length)
        {
            return AddZero(sheep.ToString(), length);
        }

        /// <summary>
        /// 在前面补0
        /// </summary>
        /// <param name="sheep">数字</param>
        /// <param name="length">补0长度</param>
        /// <returns>返回值</returns>
        public static string AddZero(string sheep, int length)
        {
            var goat = new StringBuilder(sheep);
            for (int i = goat.Length; i < length; i++)
            {
                goat.Insert(0, "0");
            }

            return goat.ToString();
        }

        /// <summary>
        /// 简介：获得唯一的字符串
        /// </summary>
        /// <returns>返回值</returns>
        public static string GetUniqueString()
        {
            var rand = new Random();
            return ((int)(rand.NextDouble() * 10000)).ToString() + DateTime.Now.Ticks.ToString();
        }

        /// <summary>
        /// 获得干净,无非法字符的字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>返回值</returns>
        public static string GetCleanJsString(string str)
        {
            str = str.Replace("\"", "“");
            str = str.Replace("'", "”");
            str = str.Replace("\\", "\\\\");
            var re = new Regex(@"\r|\n|\t",
                               RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
            str = re.Replace(str, " ");

            return str;
        }

        /// <summary>
        /// 获得干净,无非法字符的字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>返回值</returns>
        public static string GetCleanJsString2(string str)
        {
            str = str.Replace("\"", "\\\"");
            //str = str.Replace("'", "\\\'");
            //str = str.Replace("\\", "\\\\");
            var re = new Regex(@"\r|\n|\t",
                               RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
            str = re.Replace(str, " ");

            return str;
        }

        /// <summary>
        /// 将原始字串转换为unicode,格式为\u.\u.
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>返回值</returns>
        public static string StringToUnicode(string str)
        {
            //中文转为UNICODE字符
            string outStr = "";
            if (!string.IsNullOrEmpty(str))
            {
                outStr = str.Aggregate(outStr, (current, t) => current + ("\\u" + ((int)t).ToString("x")));
            }
            return outStr;
        }

        /// <summary>
        /// 将Unicode字串\u.\u.格式字串转换为原始字符串
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>返回值</returns>
        public static string UnicodeToString(string str)
        {
            string outStr = "";

            str = Regex.Replace(str, "[\r\n]", "", 0);

            if (!string.IsNullOrEmpty(str))
            {
                string[] strlist = str.Replace("\\u", "㊣").Split('㊣');
                try
                {
                    outStr += strlist[0];
                    for (int i = 1; i < strlist.Length; i++)
                    {
                        string strTemp = strlist[i];
                        if (!string.IsNullOrEmpty(strTemp) && strTemp.Length >= 4)
                        {
                            strTemp = strlist[i].Substring(0, 4);
                            //将unicode字符转为10进制整数，然后转为char中文字符
                            outStr += (char)int.Parse(strTemp, NumberStyles.HexNumber);
                            outStr += strlist[i].Substring(4);
                        }
                    }
                }
                catch (FormatException)
                {
                    outStr += "Erorr"; //ex.Message;
                }
            }
            return outStr;
        }

        /// <summary>
        /// GB2312转换成unicode编码 
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>返回值</returns>
        public static string GB2Unicode(string str)
        {
            string hexs = "";
            Encoding gb = Encoding.GetEncoding("GB2312");
            byte[] gbBytes = gb.GetBytes(str);
            for (int i = 0; i < gbBytes.Length; i++)
            {
                string hh = "%" + gbBytes[i].ToString("X");
                hexs += hh;
            }
            return hexs;
        }

        /// <summary>
        /// 得到单个字符的值
        /// </summary>
        /// <param name="ch"></param>
        /// <returns>返回值</returns>
        private static ushort GetByte(char ch)
        {
            ushort rtnNum;
            switch (ch)
            {
                case 'a':
                case 'A':
                    rtnNum = 10;
                    break;
                case 'b':
                case 'B':
                    rtnNum = 11;
                    break;
                case 'c':
                case 'C':
                    rtnNum = 12;
                    break;
                case 'd':
                case 'D':
                    rtnNum = 13;
                    break;
                case 'e':
                case 'E':
                    rtnNum = 14;
                    break;
                case 'f':
                case 'F':
                    rtnNum = 15;
                    break;
                default:
                    rtnNum = ushort.Parse(ch.ToString());
                    break;
            }
            return rtnNum;
        }

        /// <summary>
        /// 转换一个字符，输入如"Π"中的"03a0"
        /// </summary>
        /// <param name="unicodeSingle">unicode字符</param>
        /// <returns>返回值</returns>
        public static string ConvertSingle(string unicodeSingle)
        {
            if (unicodeSingle.Length != 4)
                return null;
            Encoding unicode = Encoding.Unicode;
            var unicodeBytes = new byte[] { 0, 0 };
            for (int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 0:
                        unicodeBytes[1] += (byte)(GetByte(unicodeSingle[i]) * 16);
                        break;
                    case 1:
                        unicodeBytes[1] += (byte)GetByte(unicodeSingle[i]);
                        break;
                    case 2:
                        unicodeBytes[0] += (byte)(GetByte(unicodeSingle[i]) * 16);
                        break;
                    case 3:
                        unicodeBytes[0] += (byte)GetByte(unicodeSingle[i]);
                        break;
                }
            }

            var asciiChars = new char[unicode.GetCharCount(unicodeBytes, 0, unicodeBytes.Length)];
            unicode.GetChars(unicodeBytes, 0, unicodeBytes.Length, asciiChars, 0);
            var asciiString = new string(asciiChars);

            return asciiString;
        }

        /// <summary>
        /// unicode编码转换成GB2312汉字 
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>返回值</returns>
        public static string UtoGB(string str)
        {
            string[] ss = str.Replace("\\", "").Split('u');
            var bs = new Byte[ss.Length - 1];
            for (int i = 1; i < ss.Length; i++)
            {
                bs[i - 1] = Convert.ToByte(Convert2Hex(ss[i])); //ss[0]为空串   
            }
            char[] chrs = Encoding.GetEncoding("GB2312").GetChars(bs);
            string s = "";
            for (int i = 0; i < chrs.Length; i++)
            {
                s += chrs[i].ToString();
            }
            return s;
        }

        /// <summary>
        /// 字符串转成Hex
        /// </summary>
        /// <param name="pstr">字符串</param>
        /// <returns>返回值</returns>
        private static string Convert2Hex(string pstr)
        {
            if (pstr.Length == 2)
            {
                pstr = pstr.ToUpper();
                const string hexstr = "0123456789ABCDEF";
                int cint = hexstr.IndexOf(pstr.Substring(0, 1)) * 16 + hexstr.IndexOf(pstr.Substring(1, 1));
                return cint.ToString();
            }
            return "";
        }

        #endregion 其他字符串操作
    }
}