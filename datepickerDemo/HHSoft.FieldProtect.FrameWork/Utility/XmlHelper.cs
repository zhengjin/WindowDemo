using System;
using System.Collections.Generic;
using System.Xml;

namespace HHSoft.FieldProtect.FrameWork.Utility
{
    public class XmlHelper
    {
        private static XmlDocument xmldoc;
        private static XmlNode xmlnode;
        private static XmlElement xmlelem;

        #region 创建Xml文档
        /// <summary>
        /// 创建一个带有根节点的Xml文件
        /// </summary>
        /// <param name="FileName">Xml文件名称</param>
        /// <param name="rootName">根节点名称</param>
        /// <param name="Encode">编码方式:gb2312，UTF-8等常见的</param>
        /// <param name="DirPath">保存的目录路径</param>
        /// <returns></returns>
        public static bool CreateXmlDocument(string FileName, string rootName)
        {
            try
            {
                xmldoc = new XmlDocument();

                XmlDeclaration xmldecl = xmldoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xmldoc.AppendChild(xmldecl);

                xmlelem = xmldoc.CreateElement(rootName);
                xmldoc.AppendChild(xmlelem);

                //xmldoc.NamespaceURI

                xmldoc.Save(FileName);
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw new Exception(e.Message);
            }
        }

        #endregion

        #region 常用操作方法(增删改)
        /// <summary>
        /// 插入一个节点和它的若干子节点
        /// </summary>
        /// <param name="XmlFile">Xml文件路径</param>
        /// <param name="NewNodeName">插入的节点名称</param>
        /// <param name="HasAttributes">此节点是否具有属性，True为有，False为无</param>
        /// <param name="fatherNode">此插入节点的父节点</param>
        /// <param name="htAtt">此节点的属性，Key为属性名，Value为属性值</param>
        /// <param name="htSubNode">子节点的属性，Key为Name,Value为InnerText</param>
        /// <returns>返回真为更新成功，否则失败</returns>
        public static bool InsertNode(string XmlFile, string fatherNode, string NewNodeName,
            Dictionary<string, string> Attributes, Dictionary<string, string> SubNode)
        {
            try
            {
                xmldoc = new XmlDocument();
                xmldoc.Load(XmlFile);

                XmlNode root = xmldoc.SelectSingleNode(fatherNode); 
                xmlelem = xmldoc.CreateElement(NewNodeName);

                if (Attributes != null) //若此节点有属性，则先添加属性
                {
                    SetAttributes(xmlelem, Attributes);

                    SetNodes(xmldoc, xmlelem, SubNode);//添加完此节点属性后，再添加它的子节点和它们的InnerText

                }
                else
                {
                    SetNodes(xmldoc, xmlelem, SubNode);//若此节点无属性，那么直接添加它的子节点
                }

                root.AppendChild(xmlelem);
                xmldoc.Save(XmlFile);

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="XmlFile"></param>
        /// <param name="fatherNode"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        public static bool InsertNode(string XmlFile, string fatherNode, Dictionary<string, string> node)
        {
            try
            {
                xmldoc = new XmlDocument();
                xmldoc.Load(XmlFile);               

                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmldoc.NameTable);
                nsmgr.AddNamespace("foo", "http://www.teleware.com.cn/Tdzl");

                //XmlNode root = xmldoc.SelectSingleNode(string.Format("descendant::foo:{0}", fatherNode), nsmgr);
                XmlNode root = xmldoc.SelectSingleNode(fatherNode);

                foreach (KeyValuePair<string, string> item in node)
                {
                    xmlelem = xmldoc.CreateElement(item.Key.ToString());
                    xmlelem.InnerText = item.Value.ToString();
                    root.AppendChild(xmlelem);
                }
                xmldoc.Save(XmlFile);

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// <summary>
        /// 更新节点
        /// </summary>
        /// <param name="XmlFile">Xml文件路径</param>
        /// <param name="fatherNode">需要更新节点的上级节点</param>
        /// <param name="htAtt">需要更新的属性表，Key代表需要更新的属性，Value代表更新后的值</param>
        /// <param name="htSubNode">需要更新的子节点的属性表，Key代表需要更新的子节点名字Name,Value代表更新后的值InnerText</param>
        /// <returns>返回真为更新成功，否则失败</returns>
        public static bool UpdateNode(string XmlFile, string fatherNode, Dictionary<string, string> Attributes, Dictionary<string, string> SubNode)
        {
            try
            {
                xmldoc = new XmlDocument();
                xmldoc.Load(XmlFile);
                XmlNodeList root = xmldoc.SelectSingleNode(fatherNode).ChildNodes;
                UpdateNodes(root, Attributes, SubNode);
                xmldoc.Save(XmlFile);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 删除指定节点下的子节点
        /// </summary>
        /// <param name="XmlFile">Xml文件路径</param>
        /// <param name="fatherNode">制定节点</param>
        /// <returns>返回真为更新成功，否则失败</returns>
        public static bool DeleteNodes(string XmlFile, string fatherNode)
        {
            try
            {
                xmldoc = new XmlDocument();
                xmldoc.Load(XmlFile);
                xmlnode = xmldoc.SelectSingleNode(fatherNode);
                xmlnode.RemoveAll();
                xmldoc.Save(XmlFile);
                return true;
            }
            catch (XmlException xe)
            {
                throw new XmlException(xe.Message);
            }
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 设置节点属性
        /// </summary>
        /// <param name="xe">节点所处的Element</param>
        /// <param name="htAttribute">节点属性，Key代表属性名称，Value代表属性值</param>
        private static void SetAttributes(XmlElement xe, Dictionary<string, string> Attributes)
        {
            foreach (KeyValuePair<string, string> item in Attributes)
            {
                xe.SetAttribute(item.Key.ToString(), item.Value.ToString());
            }
        }

        /// <summary>
        /// 增加子节点到根节点下
        /// </summary>
        /// <param name="rootNode">上级节点名称</param>
        /// <param name="XmlDoc">Xml文档</param>
        /// <param name="rootXe">父根节点所属的Element</param>
        /// <param name="SubNodes">子节点属性，Key为Name值，Value为InnerText值</param>
        private static void SetNodes(XmlDocument XmlDoc, XmlElement rootXe, Dictionary<string, string> SubNode)
        {
            xmlnode = XmlDoc.SelectSingleNode(rootXe.Name);
            foreach (KeyValuePair<string, string> item in SubNode)
            {
                XmlElement subNode = XmlDoc.CreateElement(item.Key.ToString());
                subNode.InnerText = string.IsNullOrEmpty(item.Value) ? string.Empty : item.Value.ToString();
                rootXe.AppendChild(subNode);
            }
        }
        /// <summary>
        /// 更新节点属性和子节点InnerText值
        /// </summary>
        /// <param name="root">根节点名字</param>
        /// <param name="htAtt">需要更改的属性名称和值</param>
        /// <param name="htSubNode">需要更改InnerText的子节点名字和值</param>
        private static void UpdateNodes(XmlNodeList root, Dictionary<string, string> Attributes, Dictionary<string, string> SubNode)
        {
            foreach (XmlNode xn in root)
            {
                xmlelem = (XmlElement)xn;
                if (xmlelem.HasAttributes)//如果节点如属性，则先更改它的属性
                {
                    foreach (KeyValuePair<string, string> item in Attributes)
                    {
                        if (xmlelem.HasAttribute(item.Key.ToString()))
                        {
                            xmlelem.SetAttribute(item.Key.ToString(), item.Value.ToString());
                        }
                    }
                }
                if (xmlelem.HasChildNodes)//如果有子节点，则修改其子节点的InnerText
                {
                    XmlNodeList xnl = xmlelem.ChildNodes;
                    foreach (XmlNode xn1 in xnl)
                    {
                        XmlElement xe = (XmlElement)xn1;
                        foreach (KeyValuePair<string, string> item in SubNode)
                        {
                            if (xe.Name == item.Key.ToString())//htSubNode中的key存储了需要更改的节点名称，
                            {
                                xe.InnerText = item.Value.ToString();//htSubNode中的Value存储了Key节点更新后的数据
                            }
                        }
                    }
                }

            }
        }
        #endregion


        /// <summary>
        /// 对 本结点(xNode) 的 Namespace 注册
        /// </summary>
        /// <param name="xNode">含有或继承有命名空间的结点</param>
        /// <param name="xNameSpaceManager">命名空间管理器</param>
        /// <returns>返回该 Namespace 的 prefix</returns>
        public static string AutoPrefix(System.Xml.XmlNode xNode, System.Xml.XmlNamespaceManager xNameSpaceManager)
        {
            string xPrefix;
            if (xNode.NamespaceURI == string.Empty)
            {
                return string.Empty;
            }
            else
            {
                xPrefix = xNameSpaceManager.LookupPrefix(xNode.NamespaceURI);
                if (xPrefix == null || xPrefix == string.Empty)
                {
                    xPrefix = "x" + xNode.GetHashCode().ToString();
                    xNameSpaceManager.AddNamespace(xPrefix, xNode.NamespaceURI);
                }
                return xPrefix + (xPrefix.Length > 0 ? ":" : "");
            }
        }

        /// <summary>
        /// 使用命名空间前辍, 生成新的 xPath
        /// </summary>
        /// <param name="xNode">含有或继承有命名空间的结点</param>
        /// <param name="xPath">无访问前辍的 xPath</param>
        /// <param name="xNameSpaceManager">命名空间管理器</param>
        /// <returns>返回 带命名空间的 xPath</returns>
        public static string AutoXPath(System.Xml.XmlNode xNode, string xPath, System.Xml.XmlNamespaceManager xNameSpaceManager)
        {
            string xPrefix = AutoPrefix(xNode, xNameSpaceManager);
            string[] Nodes = xPath.Split('/');
            string xPathOut = "";

            if (xPrefix == string.Empty)
            {
                xPathOut = xPath;
            }
            else
            {
                if (Nodes.Length > 1)
                {
                    foreach (string sNode in Nodes)
                    {
                        xPathOut += (xPathOut == "" ? "" : "/") + (sNode == string.Empty ? "" : xPrefix + sNode);
                    }
                }
                else
                {
                    xPathOut = xPrefix + xPath;
                }
            }
            return xPathOut;
        }
    }
}
