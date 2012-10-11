using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Web;
using System.Net;
using System.Collections;
using System.Xml.Serialization;

namespace HHSoft.FieldProtect.Framework.Utility
{
    public class CallWebService
    {
                /// <summary>   
        /// 缓存xmlNameSpace，避免重复调用GetNameSpace   
        /// </summary>   
        private static Hashtable _xmlNamespaces = new Hashtable();           
  
        /// <summary>   
        /// 采用Post方式调用WebService   
        /// </summary>   
        /// <param name="url"></param>   
        /// <param name="methodName"></param>   
        /// <param name="pars">string型的参数名和参数值组成的哈希表</param>   
        /// <returns></returns>   
        public static XmlDocument QueryPostWebService(string url, string methodName, Hashtable pars)   
        {   
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url + "/" + methodName);   
            request.Method = "POST";   
            request.ContentType = "application/x-www-form-urlencoded";   
            SetWebRequest(request);   
            byte[] data = EncodePars(pars);   
            WriteRequestData(request, data);   
  
            return ReadXmlResponse(request.GetResponse());   
        }   
        /// <summary>   
        /// 采用Get方式调用WebService   
        /// </summary>   
        /// <param name="url"></param>   
        /// <param name="methodName"></param>   
        /// <param name="pars"></param>   
        /// <returns></returns>   
        public static XmlDocument QueryGetWebService(string url, string methodName, Hashtable pars)   
        {   
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url + "/" + methodName + "?" + ParsToString(pars));   
            request.Method = "GET";   
            request.ContentType ="application/x-www-form-urlencoded";   
            SetWebRequest(request);   
  
            return ReadXmlResponse(request.GetResponse());   
        }   
        /// <summary>   
        /// 通过SOAP协议调用WebService   
        /// </summary>   
        /// <param name="url"></param>   
        /// <param name="methodName"></param>   
        /// <param name="pars"></param>   
        /// <returns></returns>   
        public static XmlDocument QuerySoapWebService(string url, string methodName, Hashtable pars)   
        {   
            if (_xmlNamespaces.ContainsKey(url))   
            {                   
                return QuerySoapWebService(url, methodName, pars, _xmlNamespaces[url].ToString());   
            }   
            else  
            {   
                return QuerySoapWebService(url, methodName, pars, GetNamespace(url));   
            }   
        }  
 
        #region private method   
  
        private static XmlDocument QuerySoapWebService(String url, String methodName, Hashtable Pars, string XmlNs)   
        {   
            _xmlNamespaces[url] = XmlNs;//加入缓存，提高效率   
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);   
            request.Method = "POST";   
            request.ContentType = "text/xml; charset=utf-8";   
            request.Headers.Add("SOAPAction", "\"" + XmlNs + (XmlNs.EndsWith("/") ? "" : "/") + methodName + "\"");   
            SetWebRequest(request);   
            byte[] data = EncodeParsToSoap(Pars, XmlNs, methodName);   
            WriteRequestData(request, data);   
            XmlDocument doc = new XmlDocument(), doc2 = new XmlDocument();   
            doc = ReadXmlResponse(request.GetResponse());               
            XmlNamespaceManager mgr = new XmlNamespaceManager(doc.NameTable);   
            mgr.AddNamespace("soap", "http://schemas.xmlsoap.org/soap/envelope/");   
            String RetXml = doc.SelectSingleNode("//soap:Body/*/*", mgr).InnerXml;   
            doc2.LoadXml("<root>" + RetXml + "</root>");   
            AddDelaration(doc2);   
            return doc2;   
        }   
           
        /// <summary>   
        /// 根据WebService的Url地址(以.asmx结尾的地址)获取其命名空间   
        /// </summary>   
        /// <param name="URL"></param>   
        /// <returns></returns>   
        private static string GetNamespace(String URL)   
        {   
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL + "?WSDL");   
            SetWebRequest(request);   
            WebResponse response = request.GetResponse();   
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);   
            XmlDocument doc = new XmlDocument();   
            doc.LoadXml(sr.ReadToEnd());   
            sr.Close();   
  
            return doc.SelectSingleNode("//@targetNamespace").Value;   
        }   
           
        private static byte[] EncodeParsToSoap(Hashtable Pars, String XmlNs, String MethodName)   
        {   
            XmlDocument doc = new XmlDocument();   
            doc.LoadXml("<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"></soap:Envelope>");   
            AddDelaration(doc);   
            XmlElement soapBody = doc.CreateElement("soap", "Body", "http://schemas.xmlsoap.org/soap/envelope/");   
            XmlElement soapMethod = doc.CreateElement(MethodName);   
            soapMethod.SetAttribute("xmlns", XmlNs);   
            foreach (string k in Pars.Keys)   
            {   
                XmlElement soapPar = doc.CreateElement(k);   
                soapPar.InnerXml = ObjectToSoapXml(Pars[k]);   
                soapMethod.AppendChild(soapPar);                   
            }   
            soapBody.AppendChild(soapMethod);   
            doc.DocumentElement.AppendChild(soapBody);   
            return Encoding.UTF8.GetBytes(doc.OuterXml);   
        }   
           
        private static string ObjectToSoapXml(object o)   
        {   
            XmlSerializer mySerializer = new XmlSerializer(o.GetType());   
            MemoryStream ms = new MemoryStream();   
            mySerializer.Serialize(ms, o);   
            XmlDocument doc = new XmlDocument();   
            doc.LoadXml(Encoding.UTF8.GetString(ms.ToArray()));   
            if (doc.DocumentElement != null)   
            {   
                return doc.DocumentElement.InnerXml;   
            }   
            else  
            {   
                return o.ToString();   
            }   
        }   
           
        private static void SetWebRequest(HttpWebRequest request)   
        {   
            request.Credentials = CredentialCache.DefaultCredentials;   
            request.Timeout = 10000;   
        }   
  
        private static void WriteRequestData(HttpWebRequest request, byte[] data)   
        {   
            request.ContentLength = data.Length;   
            Stream writer = request.GetRequestStream();   
            writer.Write(data, 0, data.Length);   
            writer.Close();   
        }   
  
        private static byte[] EncodePars(Hashtable Pars)   
        {   
            return Encoding.UTF8.GetBytes(ParsToString(Pars));   
        }   
  
        private static String ParsToString(Hashtable Pars)   
        {   
            StringBuilder sb = new StringBuilder();   
            foreach (string k in Pars.Keys)   
            {   
                if (sb.Length > 0)   
                {   
                    sb.Append("&");   
                }   
                sb.Append(HttpUtility.UrlEncode(k) + "=" + HttpUtility.UrlEncode(Pars[k].ToString()));   
            }   
            return sb.ToString();   
        }   
  
        private static XmlDocument ReadXmlResponse(WebResponse response)   
        {   
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);   
            String retXml = sr.ReadToEnd();   
            sr.Close();   
            XmlDocument doc = new XmlDocument();   
            doc.LoadXml(retXml);   
            return doc;   
        }   
  
        private static void AddDelaration(XmlDocument doc)   
        {   
            XmlDeclaration decl = doc.CreateXmlDeclaration("1.0", "utf-8", null);   
            doc.InsertBefore(decl, doc.DocumentElement);   
        }  
 
        #endregion //private method   
    }
}
