using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Reflection;
using System.ComponentModel;
using System.Runtime.Serialization.Formatters.Binary;

namespace HHSoft.FieldProtect.Framework.Utility
{
    /// <summary>
    /// WebTools类
    /// </summary>
    public class CommonHelper
    {
        /// <summary>
        /// 长的行政编码转换成短的行政编码
        /// </summary>
        /// <param name="cCode"></param>
        /// <returns></returns>
        public static string GetShortCode(string cCode)
        {
            string shortCode = cCode;
            if (cCode.EndsWith("00"))
            {
                shortCode = cCode.Replace("00", string.Empty);
            }
            return shortCode;
        }
        /// <summary>
        /// 返回市一级的行政编码
        /// </summary>
        /// <param name="cCode"></param>
        /// <returns></returns>
        public static string GetSHICode(string cCode)
        {
            return cCode.Substring(0, 4).PadRight(6, '0');
        }

        #region 枚举

        /// <summary>
        /// 获取枚举字段的描述
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="enumValueOrName">枚举值或名称</param>
        /// <param name="isName">是否是枚举字段名称</param>
        /// <returns></returns>
        public static string GetEnumDesc(Type enumType, string enumValueOrName, bool isName)
        {
            string name = "";

            if (isName)

                name = enumValueOrName;
            else

                name = Enum.GetName(enumType, Convert.ToInt32(enumValueOrName));

            FieldInfo fi = enumType.GetField(name);

            DescriptionAttribute dna = (DescriptionAttribute)Attribute.GetCustomAttribute(fi, typeof(DescriptionAttribute));

            if (dna != null)
            {
                return dna.Description;
            }

            return "";
        }

        #endregion

        #region 地址栏参数
        /// <summary>
        /// 页面Guid码
        /// </summary>
        /// <param name="key">参数</param>
        /// <returns>Guid</returns>
        public static Guid GetGuidFromQueryString(string key)
        {
            Guid returnValue = Guid.Empty;
            string queryStringValue;
            queryStringValue = HttpContext.Current.Request.QueryString[key];
            if (queryStringValue == null)
            {
                return returnValue;
            }

            try
            {
                if (queryStringValue.IndexOf("#") > 0)
                {
                    queryStringValue = queryStringValue.Substring(0, queryStringValue.IndexOf("#"));
                }

                returnValue = new Guid(queryStringValue);
            }
            catch (Exception ex)
            {
                throw new Exception("地址栏参数页面Guid码异常", ex);
            }

            return returnValue;
        }

        /// <summary>
        /// 地址栏参数（string类型）
        /// </summary>
        /// <param name="key">参数</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>参数值</returns>
        public static string GetStrFromQueryString(string key, string defaultValue)
        {
            string queryStringValue;
            queryStringValue = HttpContext.Current.Request.QueryString[key];
            if (queryStringValue == null)
            {
                return defaultValue;
            }

            try
            {
                if (queryStringValue.IndexOf("#") > 0)
                {
                    queryStringValue = queryStringValue.Substring(0, queryStringValue.IndexOf("#"));
                }

                defaultValue = queryStringValue;
            }
            catch (Exception ex)
            {
                throw new Exception("地址栏参数页面Guid码异常", ex);
            }

            return defaultValue;
        }

        /// <summary>
        /// 地址栏参数（int类型）
        /// </summary>
        /// <param name="key">参数</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>参数值</returns>
        public static int GetIntFromQueryString(string key, int defaultValue)
        {
            string queryStringValue;
            queryStringValue = HttpContext.Current.Request.QueryString[key];
            if (queryStringValue == null)
            {
                return defaultValue;
            }

            try
            {
                if (queryStringValue.IndexOf("#") > 0)
                {
                    queryStringValue = queryStringValue.Substring(0, queryStringValue.IndexOf("#"));
                }

                defaultValue = Convert.ToInt32(queryStringValue);
            }
            catch (Exception ex)
            {
                throw new Exception("地址栏参数（int类型）异常", ex);
            }

            return defaultValue;
        }

        #endregion

        #region 获取客户端IP
        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <returns>字符串</returns>
        public static string GetClientIP()
        {
            string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }

            return result;
        }
        #endregion

        #region 字符串处理
        /// <summary>
        /// 判断字符串是否为空
        /// </summary>
        /// <param name="text">源字符串</param>
        /// <returns>bool</returns>
        public static bool IsNullorEmpty(string text)
        {
            return text == null || text.Trim() == string.Empty;
        }

        /// <summary>
        /// string型转换为bool型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的bool类型结果</returns>
        public static bool StrToBool(object strValue, bool defValue)
        {
            if ((strValue == null) || ((strValue.ToString().ToLower() != "true") && (strValue.ToString().ToLower() != "false")))
            {
                return defValue;
            }

            string val = strValue.ToString().ToLower();
            return Convert.ToBoolean(val);
        }

        /// <summary>
        /// string型转换为int型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static int StrToInt(object strValue, int defValue)
        {
            if ((strValue == null) || (strValue.ToString() == string.Empty) || (strValue.ToString().Length > 10))
            {
                return defValue;
            }

            string val = strValue.ToString();
            string firstletter = val[0].ToString();

            if (val.Length == 10 && IsNumber(firstletter) && int.Parse(firstletter) > 1)
            {
                return defValue;
            }
            else if (val.Length == 10 && !IsNumber(firstletter))
            {
                return defValue;
            }

            int intValue = defValue;
            if (strValue != null)
            {
                bool isint = new Regex(@"^([-]|[0-9])[0-9]*$").IsMatch(strValue.ToString());
                if (isint)
                {
                    intValue = Convert.ToInt32(strValue);
                }
            }

            return intValue;
        }

        /// <summary>
        /// string型转换为float型
        /// </summary>
        /// <param name="strValue">要转换的字符串</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>转换后的int类型结果</returns>
        public static float StrToFloat(object strValue, float defValue)
        {
            if ((strValue == null) || (strValue.ToString().Length > 10))
            {
                return defValue;
            }

            float intValue = defValue;
            if (strValue != null)
            {
                bool isfloat = new Regex(@"^([-]|[0-9])[0-9]*(\.\w*)?$").IsMatch(strValue.ToString());
                if (isfloat)
                {
                    intValue = Convert.ToSingle(strValue);
                }
            }

            return intValue;
        }

        /// <summary>
        /// 判断给定的字符串(strNumber)是否是数值型
        /// </summary>
        /// <param name="strNumber">要确认的字符串</param>
        /// <returns>是则返加true 不是则返回 false</returns>
        public static bool IsNumber(string strNumber)
        {
            return new Regex(@"^([0-9])[0-9]*(\.\w*)?$").IsMatch(strNumber);
        }

        /// <summary>
        /// 检查搜索字符串
        /// </summary>
        /// <param name="searchString">查询字符串</param>
        /// <returns>字符串</returns>
        public static string CleanSearchString(string searchString)
        {
            if (IsNullorEmpty(searchString))
            {
                return null;
            }

            searchString = searchString.Trim();
            //// Do wild card replacements
            searchString = searchString.Replace("*", "%");
            //// Strip any markup characters
            searchString = Regex.Replace(searchString, "<[^>]+>", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            //// Remove known bad SQL characters
            searchString = Regex.Replace(searchString, "--|;|'|\"", " ", RegexOptions.Compiled | RegexOptions.Multiline);
            //// Finally remove any extra spaces from the string
            searchString = Regex.Replace(searchString, " {1,}", " ", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Multiline);

            return searchString;
        }

        /// <summary>
        /// 为字符串中关键字给样式
        /// </summary>
        /// <param name="source">源</param>
        /// <param name="key">关键字</param>
        /// <param name="css">css样式</param>
        /// <returns>关键字加上样式的字符串</returns>
        public static string ColorForKey(string source, string key, string css)
        {
            if (string.IsNullOrEmpty(source))
            {
                return string.Empty;
            }

            if (source.Contains(key))
            {
                string[] result = source.Split(new string[] { key }, StringSplitOptions.RemoveEmptyEntries);
                int keypostion = source.IndexOf(key);
                int currentpositon = 0;
                StringBuilder sb = new StringBuilder();
                if (keypostion == 0)
                {
                    sb.AppendFormat("<span class='" + css + "'>{0}</span>", key);
                }

                for (int i = 0; i < result.Length; i++)
                {
                    sb.Append(result[i]);
                    currentpositon += result[i].Length;
                    if (keypostion == currentpositon)
                    {
                        sb.AppendFormat("<span class='" + css + "'>{0}</span>", key);
                    }
                }

                source = sb.ToString();
            }

            return source;
        }

        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="source">源</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns>截取后字符串</returns>
        public static string SubString(string source, int maxLength)
        {
            if (string.IsNullOrEmpty(source)) return string.Empty;
            if (source.Length <= maxLength)
            {
                return source;
            }
            else
            {
                return source.Substring(0, maxLength) + "...";
            }
        }

        /// <summary>
        /// 去掉字符串中重复的字符
        /// </summary>
        /// <param name="myString">源string</param>
        /// <returns>string</returns>
        public static string[] RemoveDupstring(string myString)
        {
            string[] myData = myString.Split(',');
            if (myData.Length > 0)
            {
                Array.Sort(myData);
                int size = 1;
                for (int i = 1; i < myData.Length; i++)
                    if (myData[i] != myData[i - 1])
                        size++;

                string[] myTempData = new string[size];

                int j = 0;

                myTempData[j++] = myData[0];

                for (int i = 1; i < myData.Length; i++)
                    if (myData[i] != myData[i - 1])
                        myTempData[j++] = myData[i];

                return myTempData;
            }

            return myData;
        }


        #endregion

        #region 文件处理
        /// <summary>
        /// 取得文件类型
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns>文件类型字符串</returns>
        public static string GetFileType(string fileName)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(fileName))
            {
                if (fileName.Length < 5)
                {
                    result = string.Empty;
                }

                result = fileName.Substring(fileName.Length - 3, 3).ToLower();
            }

            return result;
        }

        /// <summary>
        /// 获取文件路径
        /// </summary>
        /// <param name="url">url</param>
        /// <returns>文件路径</returns>
        public static string GetDefaultFilePath(string url)
        {
            string dir = Environment.CurrentDirectory;
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            return Path.Combine(baseDir, url);
        }

        #endregion

        #region 路径处理
        /// <summary>
        /// 返回website根路径
        /// </summary>
        /// <returns>返回website根路径字符串</returns>
        public static string GetBaseDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        /// 返回虚拟应用程序的根目录
        /// </summary>
        /// <returns>虚拟应用程序的根目录</returns>
        public static string GetApplicationRoot()
        {
            if (HttpContext.Current.Request.ApplicationPath.Length == 1)
            {
                return string.Empty;
            }
            else
            {
                return HttpContext.Current.Request.ApplicationPath;
            }
        }

        /// <summary>
        /// 返回不带参数的绝对Url
        /// </summary>
        /// <returns>不带参数的绝对Url</returns>
        public static string GetCurrentAbsoluteUrl()
        {
            return HttpContext.Current.Request.Url.AbsolutePath;
        }

        /// <summary>
        /// 不带参数的当前Url
        /// </summary>
        /// <returns>当前Url</returns>
        public static string GetCurrentUrl()
        {
            return HttpContext.Current.Request.Url.PathAndQuery;
        }

        /// <summary>
        /// 获取访问的原始地址
        /// </summary>
        /// <returns>原始地址</returns>
        public static string GetRawUrl()
        {
            return HttpContext.Current.Request.RawUrl;
        }

        /// <summary>
        /// 设置地址栏Query中一项的值,并返回新的QueryString
        /// </summary>
        /// <param name="key">参数</param>
        /// <param name="value">参数值</param>
        /// <returns>字符串</returns>
        public static string SetQueryValue(string key, string value)
        {
            string queryString = string.Empty;
            bool isexist = false;
            NameValueCollection queryCollection = new NameValueCollection();
            foreach (string keyparam in HttpContext.Current.Request.QueryString)
            {
                queryCollection.Add(keyparam, HttpContext.Current.Request.QueryString[keyparam]);
            }

            foreach (string keyparam in queryCollection.Keys)
            {
                if (keyparam == key)
                {
                    isexist = true;
                    queryCollection[key] = value;
                    break;
                }
            }

            if (!isexist)
            {
                queryCollection.Add(key, value);
            }

            foreach (string keyparam in queryCollection.Keys)
            {
                queryString += "&" + keyparam + "=" + HttpUtility.UrlEncode(queryCollection[keyparam].ToString());
            }

            if (queryString.StartsWith("&"))
            {
                queryString = queryString.Substring(1, queryString.Length - 1);
            }

            queryString = "?" + queryString;

            return queryString;
        }

        /// <summary>
        /// 设置url参数值
        /// </summary>
        /// <param name="nameValues">NameValueCollection</param>
        /// <returns>string</returns>
        public static string SetQueryValue(NameValueCollection nameValues)
        {
            string queryString = string.Empty;

            if (nameValues == null)
            {
                return queryString;
            }

            bool exist = false;
            List<string> notExistKey = new List<string>();
            NameValueCollection queryCollection = new NameValueCollection();
            foreach (string key in HttpContext.Current.Request.QueryString)
            {
                queryCollection.Add(key, HttpContext.Current.Request.QueryString[key]);
            }

            foreach (string key2 in nameValues.Keys)
            {
                foreach (string key in queryCollection.Keys)
                {
                    if (key == key2)
                    {
                        exist = true;
                        queryCollection[key] = nameValues[key2];
                        break;
                    }
                }

                if (!exist)
                {
                    notExistKey.Add(key2);
                }
            }

            foreach (string key in notExistKey)
            {
                queryCollection.Add(key, nameValues[key]);
            }

            foreach (string key in queryCollection.Keys)
            {
                queryString += "&" + key + "=" + HttpUtility.UrlEncode(queryCollection[key].ToString());
            }

            if (queryString.StartsWith("&"))
            {
                queryString = queryString.Substring(1, queryString.Length - 1);
            }

            queryString = "?" + queryString;

            return queryString;
        }

        /// <summary>
        /// 检查当前QueryString是否包含Key
        /// </summary>
        /// <param name="key">参数key</param>
        /// <returns>bool</returns>
        public static bool IsContainQueryKey(string key)
        {
            bool isexist = false;
            if (HttpContext.Current != null && HttpContext.Current.Request != null)
            {
                foreach (string keyParam in HttpContext.Current.Request.QueryString)
                {
                    if (keyParam.ToLower() == key.ToLower())
                    {
                        isexist = true;
                        break;
                    }
                }
            }

            return isexist;
        }

        /// <summary>
        /// 返回站点路径，包括端口号
        /// </summary>
        /// <returns>站点路径，包括端口号</returns>
        public static string GetSiteRoot()
        {
            string protocol = "http";
            if (HttpContext.Current.Request.ServerVariables["HTTPS"] == "on")
            {
                protocol += "s";
            }

            string host = GetHost(protocol);
            return protocol + "://" + host + GetApplicationRoot();
        }

        /// <summary>
        /// web页面跳转
        /// </summary>
        /// <param name="url">源url</param>
        /// <param name="target">目标url</param>
        /// <param name="windowFeatures">窗体属性</param>
        public static void Redirect(string url, string target, string windowFeatures)
        {
            HttpContext context = HttpContext.Current;
            if ((String.IsNullOrEmpty(target) || target.Equals("_self", StringComparison.OrdinalIgnoreCase)) && String.IsNullOrEmpty(windowFeatures))
            {
                context.Response.Redirect(url);
            }
            else
            {
                Page page = (Page)context.Handler;
                if (page == null)
                {
                    throw new InvalidOperationException("跳转页面失败");
                }

                url = page.ResolveClientUrl(url);
                string script;
                if (!String.IsNullOrEmpty(windowFeatures))
                {
                    script = @"window.open(""{0}"", ""{1}"", ""{2}"");";
                }
                else
                {
                    script = @"window.open(""{0}"", ""{1}"");";
                }

                script = String.Format(script, url, target, windowFeatures);
                page.ClientScript.RegisterStartupScript(typeof(Page), "Redirect", script, true);
            }
        }

        /// <summary>
        /// website获取主目录
        /// </summary>
        /// <param name="protocol">协议</param>
        /// <returns>字符串</returns>
        private static string GetHost(string protocol)
        {
            string serverName = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
            string serverPort = HttpContext.Current.Request.ServerVariables["SERVER_PORT"];

            ////默认协议的端口则省略
            if ((protocol == "http" && serverPort == "80")
                || (protocol == "https" && serverPort == "443"))
            {
                serverPort = null;
            }

            string host = serverName;
            if (serverPort != null)
            {
                host += ":" + serverPort;
            }

            return host;
        }
        #endregion

        #region 页面控件处理
        /// <summary>
        /// 获取CheckBoxList选中值
        /// </summary>
        /// <param name="checkList"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string GetValueFromCheckboxlist(CheckBoxList checkList)
        {
            string checkValue = string.Empty;
            for (int i = 0; i < checkList.Items.Count; i++)
            {
                if (checkList.Items[i].Selected)
                {
                    checkValue += checkList.Items[i].Value;
                    checkValue += ",";
                }
            }
            if (checkValue.EndsWith(",")) checkValue = checkValue.Substring(0, checkValue.Length - 1);
            return checkValue;
        }
        /// <summary>
        /// 设置CheckBoxList选中值
        /// </summary>
        /// <param name="checkList"></param>
        /// <param name="values"></param>
        public static void SetValueToCheckboxlist(CheckBoxList checkList, string values)
        {
            string[] param = values.Split(',');
            for (int i = 0; i < checkList.Items.Count; i++)
            {
                foreach (String s in param)
                {
                    if (checkList.Items[i].Value == s)
                    {
                        checkList.Items[i].Selected = true;
                    }
                }
            }
        }
        #endregion

        public static int GetDays(DateTime dt1, DateTime dt2, bool workday)
        {
            TimeSpan ts = dt2.Subtract(dt1);
            int countday = ts.Days;

            int weekday = 0;
            if (workday)
            {
                ////扣除总天数中的双休日
                for (int i = 0; i < countday; i++)
                {
                    DateTime tempdt = dt1.Date.AddDays(i);
                    if (tempdt.DayOfWeek != System.DayOfWeek.Saturday && tempdt.DayOfWeek != System.DayOfWeek.Sunday)
                    {
                        weekday++;
                    }
                }
            }
            return countday - weekday;
        }


        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataList"></param>
        /// <returns></returns>
        public static byte[] ConvertToByte<T>(List<T> dataList)
        {
            System.IO.MemoryStream memory = new System.IO.MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memory, dataList);
            byte[] pBytes = new byte[memory.Length];
            memory.Seek(0, SeekOrigin.Begin); // 设定当前流中的位置
            memory.Read(pBytes, 0, pBytes.Length);
            memory.Close();
            return pBytes;
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static List<T> ConvertToList<T>(byte[] bytes)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            System.IO.MemoryStream memory = new System.IO.MemoryStream(bytes);

            object rstObj = formatter.Deserialize(memory);
            List<T> dataList = rstObj as List<T>;
            memory.Close();
            return dataList;
        }

        /// <summary>
        /// 获取字符串长度（中文算2）。
        /// </summary>
        /// <param name="content">字符串内容。</param>
        /// <returns>长度。</returns>
        public static int GetStringAscIILength(string content)
        {
            return Encoding.Default.GetByteCount(content);
        }

        /// <summary>
        /// 截取字符串（中文算2）。
        /// </summary>
        /// <param name="content">字符串内容。</param>
        /// <param name="length">截取长度。</param>
        /// <returns>截取结果。</returns>
        public static string GetSubString(string content, int length)
        {
            string temp = content;
            int j = 0;
            int k = 0;
            for (int i = 0; i < temp.Length; i++)
            {
                if (Regex.IsMatch(temp.Substring(i, 1), @"[\u4e00-\u9fa5]+"))
                {
                    j += 2;
                }
                else
                {
                    j += 1;
                }
                if (j <= length)
                {
                    k += 1;
                }
                if (j >= length)
                {
                    return temp.Substring(0, k);
                }
            }
            return temp;
        }
    }
}
