using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace HHSoft.FieldProtect.Framework.Utility
{
    /// <summary>
    /// 数据类型转换帮助类
    /// </summary>
    public class DataConverter
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public DataConverter()
        {
        }

        /// <summary>
        /// 返回一个byte[]从第index个元素后长度为length的byte[]
        /// </summary>
        /// <param name="by">byte数组（源）</param>
        /// <param name="index">起始索引</param>
        /// <param name="length">长度</param>
        /// <returns>byte数组（截取）</returns>
        public static byte[] SubByteArray(byte[] by, int index, int length)
        {
            byte[] byr = new byte[length];
            for (int i = index; i < index + length; i++)
            {
                byr[i - index] = by[i];
            }

            return byr;
        }

        /// <summary>
        ///  返回一个byte[]从第index个元素到结尾的byte[]
        /// </summary>
        /// <param name="by">byte数组（源）</param>
        /// <param name="index">起始索引</param>
        /// <returns>byte数组（截取）</returns>
        public static byte[] SubByteArray(byte[] by, int index)
        {
            return SubByteArray(by, index, by.Length - index);
        }

        /// <summary>
        /// 返回两个byte数组连接后的byte数组。.
        /// </summary>
        /// <param name="by1">byte数组1</param>
        /// <param name="by2">byte数组2</param>
        /// <returns>byte数组(连接后)</returns>
        public static byte[] ByteAdd(byte[] by1, byte[] by2)
        {
            int l1 = by1.Length;
            int l2 = by2.Length;
            byte[] by = new byte[l1 + l2];
            for (int i = 0; i < l1; i++)
            {
                by[i] = by1[i];
            }

            for (int i = 0; i < l2; i++)
            {
                by[i + l1] = by2[i];
            }

            return by;
        }

        /// <summary>
        /// "aabbcc"变为"ccbbaa"
        /// </summary>
        /// <param name="str">长度必需是偶数</param>
        /// <returns>string</returns>
        public static string StrTOStrV(string str)
        {
            string retn = string.Empty;
            int length = str.Length;
            for (int i = 0; i <= length - 2; i += 2)
            {
                retn = str.Substring(i, 2) + retn;
            }

            return retn;
        }

        /// <summary>
        /// 把一个字节数组转化成十六进制的字符串形式，以空格隔开。
        /// </summary>
        /// <param name="da">byte数组（源）</param>
        /// <returns>string</returns>
        public static string ByteToHexStr(byte[] da)
        {
            string s = string.Empty;
            for (int i = 0; i < da.Length; i++)
            {
                s += Convert.ToString(da[i], 16).PadLeft(2, '0') + " ";
            }

            return s;
        }

        /// <summary>
        /// 把一个字节数组转化成十六进制的字符串形式
        /// </summary>
        /// <param name="da">byte数组</param>
        /// <returns>string</returns>
        public static string ByteToHexStr2(byte[] da)
        {
            string s = string.Empty;
            for (int i = 0; i < da.Length; i++)
            {
                s += Convert.ToString(da[i], 16).PadLeft(2, '0');
            }

            return s;
        }

        /// <summary>
        /// 把诸如：23 fe e3 的字符串转化成byte[]
        /// </summary>
        /// <param name="da">string</param>
        /// <returns>byte数组</returns>
        public static byte[] StrToHexByte(string da)
        {
            string sends = da;
            sends = sends.Replace(" ", string.Empty);
            sends = sends.Replace("\n", string.Empty);
            sends = sends.Replace("\r", string.Empty);
            int length = sends.Length / 2;
            byte[] sendb = new byte[length];

            for (int i = 0; i < length; i++)
            {
                sendb[i] = Convert.ToByte(sends.Substring(i * 2, 2), 16);
            }

            return (sendb);
        }

        /// <summary>
        /// byte数组转为特定进制字符串
        /// </summary>
        /// <param name="paramByteArray">数组</param>
        /// <param name="isSpace">中间是否空格</param>
        /// <param name="hexadecimal">按多少进制转换</param>
        /// <returns>string</returns>
        public static string ByteArrayToString(byte[] paramByteArray, bool isSpace, int hexadecimal)
        {
            if (isSpace)
            {
                string tempString = null;
                foreach (byte tempByte in paramByteArray)
                {
                    try
                    {
                        tempString += (Convert.ToString(tempByte, hexadecimal) + " ");
                    }
                    catch (Exception paramException)
                    {
                        throw new Exception("严重的警告,以" + hexadecimal.ToString() + "进制转换数组时出错", paramException);
                    }
                }

                return tempString.Trim();
            }
            else
            {
                StringBuilder tempString = new StringBuilder(20480);
                foreach (byte tempByte in paramByteArray)
                {
                    try
                    {
                        tempString.Append(Convert.ToChar(tempByte));
                    }
                    catch (Exception paramException)
                    {
                        throw new Exception("严重的警告,以" + hexadecimal.ToString() + "进制转换数组为字符时出错", paramException);
                    }
                }

                return tempString.ToString().Trim();
            }
        }

        /// <summary>
        /// isNumeric=True 把message当做数值转化；false把message当做string转化
        /// </summary>
        /// <param name="message">要转化的string</param>
        /// <returns>byte[]</returns>
        public static byte[] HexStringToByteArray(string message)
        {
            message = message.Replace("\n", string.Empty);
            message = message.Replace("\r", string.Empty);
            char[] wordArray = message.ToCharArray();
            List<byte> tempByteList = new List<byte>();
            byte[] tempByteArray = null;
            foreach (char tempChar in wordArray)
            {
                try
                {
                    tempByteList.Add(Convert.ToByte(tempChar));
                }
                catch (Exception paramException)
                {
                    throw new Exception("严重的警告,在编码RTU信息是出错,一个字节不能被转换为16进制数,来自字符串：" + message, paramException);
                }
            }

            tempByteArray = new byte[tempByteList.Count];
            tempByteList.CopyTo(tempByteArray);

            return tempByteArray;
        }

        /// <summary>
        /// 十六进制字符串转换为Byte数组
        /// </summary>
        /// <param name="message">十六进制字符串（如：“EB 90”）</param>
        /// <param name="isSpace">数据是否与空格分割，为假时一个字符就是一个十六进制数</param>
        /// <returns>Byte数组</returns>
        public static byte[] HexStringToByteArray(string message, bool isSpace)
        {
            message = message.Replace("\n", string.Empty);
            message = message.Replace("\r", string.Empty);
            if (isSpace)
            {
                string[] wordArray = message.Split(new char[] { ' ' });
                List<byte> tempByteList = new List<byte>();
                byte[] tempByteArray = null;
                foreach (string tempString in wordArray)
                {
                    try
                    {
                        if (String.IsNullOrEmpty(tempString)) continue;
                        if (tempString == string.Empty) continue;
                        tempByteList.Add(Convert.ToByte(tempString.Trim(), 16));
                    }
                    catch (Exception paramException)
                    {
                        throw new Exception("严重的警告,在编码RTU信息是出错,一个字节不能被转换为16进制数,来自字符串：" + message, paramException);
                    }
                }

                tempByteArray = new byte[tempByteList.Count];
                tempByteList.CopyTo(tempByteArray);
                return tempByteArray;
            }
            else
            {
                char[] wordArray = message.ToCharArray();
                List<byte> tempByteList = new List<byte>();
                byte[] tempByteArray = null;
                foreach (char tempChar in wordArray)
                {
                    try
                    {
                        tempByteList.Add(Convert.ToByte(tempChar));
                    }
                    catch (Exception paramException)
                    {
                        throw new Exception("严重的警告,在编码RTU信息是出错,一个字节不能被转换为16进制数,来自字符串：" + message, paramException);
                    }
                }

                tempByteArray = new byte[tempByteList.Count];
                tempByteList.CopyTo(tempByteArray);

                return tempByteArray;
            }
        }

        /// <summary>
        /// 半角转全角
        /// </summary>
        /// <param name="str">半角string</param>
        /// <returns>全角string</returns>
        public static string All2Half(string str)
        {
            string resultString = string.Empty;
            char[] charArray = str.ToCharArray();
            for (int i = 0; i < charArray.Length; i++)
            {
                byte[] b = System.Text.Encoding.Unicode.GetBytes(charArray, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 0)
                    {
                        b[0] = (byte)(b[0] - 32);
                        b[1] = 255;
                        charArray[i] = System.Text.Encoding.Unicode.GetChars(b)[0];
                    }
                    else
                    {
                        resultString += charArray[i].ToString();
                    }
                }
                else
                {
                    resultString += charArray[i].ToString();
                }
            }

            return resultString;
        }

        /// <summary>
        /// 全角转半角
        /// </summary>
        /// <param name="str">全角string</param>
        /// <returns>半角string</returns>
        public static string Half2All(string str)
        {
            string resultString = string.Empty;
            char[] c = str.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                byte[] b = System.Text.Encoding.Unicode.GetBytes(c, i, 1);
                if (b.Length == 2)
                {
                    if (b[1] == 255)
                    {
                        b[0] = (byte)(b[0] + 32);
                        b[1] = 0;
                        resultString += System.Text.Encoding.Unicode.GetChars(b)[0];
                    }
                    else
                    {
                        resultString += c[i].ToString();
                    }
                }
                else
                {
                    resultString += c[i].ToString();
                }
            }

            return resultString;
        }

        /// <summary>
        /// 转全角的函数(SBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>全角字符串</returns>
        /// <remarks>
        /// 全角空格为12288，半角空格为32
        /// 其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        /// </remarks>        
        public static string ToSBC(string input)
        {
            ////半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }

                if (c[i] < 127)
                {
                    c[i] = (char)(c[i] + 65248);
                }
            }

            return new string(c);
        }

        /// <summary>
        /// 转半角的函数(DBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>半角字符串</returns>
        ///  <remarks>
        ///  全角空格为12288，半角空格为32
        ///  其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        /// </remarks>
        public static string ToDBC(string input)
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

        /// <summary> 
        /// MD5　32位加密 
        /// </summary> 
        /// <param name="str">string</param> 
        /// <returns>加密后string</returns> 
        public static string CreateMD5Password(string str)
        {
            string cl = str;
            string pwd = string.Empty;
            ////实例化一个md5对像 
            MD5 md5 = MD5.Create();
            //// 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　 
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            //// 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得 
            for (int i = 0; i < s.Length; i++)
            {
               //// 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                pwd = pwd + s[i].ToString("X");
            }

            return pwd;
        }

        /// <summary>
        /// string居中
        /// </summary>
        /// <param name="companyName">string</param>
        /// <param name="length">string长度</param>
        /// <returns>居中string</returns>
        public static string PositionMiddle(string companyName, int length)
        {
            if (string.IsNullOrEmpty(companyName))
            {
                companyName = string.Empty;
                companyName.PadLeft(length);
                return companyName;
            }

            if (companyName.Length == length)
            {
                return companyName;
            }

            if (companyName.Length > length)
            {
                return companyName.Substring(length);
            }

            string leftstring = string.Empty;
            string rightstring = string.Empty;
            int rightLength = 0;
            int leftLength = 0;
            if (companyName.Length < length)
            {
                rightLength = (length - companyName.Length) / 2;
                leftLength = (length - companyName.Length) % 2;
            }

            leftstring = leftstring.PadRight(leftLength + rightLength);
            rightstring = rightstring.PadRight(rightLength);
            companyName = leftstring + companyName + rightstring;
            return companyName;
        }

        /// <summary>
        /// string 截取
        /// </summary>
        /// <param name="str">string</param>
        /// <param name="length">长度</param>
        /// <returns>截取string</returns>
        public static string SubStringByLegth(string str, int length)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(str))
            {
                string empet = @" ";
                empet.PadRight(length - 1, ' ');
                result = empet;
                return result;
            }
            else if (str.Length < length)
            {
                result = str.PadRight(length);
            }
            else if (str.Length > length)
            {
                result = str.Substring(length);
            }
            else
            {
                result = str;
            }

            return result;
        }
    }
}
