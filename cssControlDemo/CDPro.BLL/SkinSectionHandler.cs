using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml;

namespace CDPro.BLL
{
    public class SkinSectionHandler : IConfigurationSectionHandler
    {
        public SkinSectionHandler()
        {

        }

        public object Create(object parent, object configContext, XmlNode section)
        {
            //获取皮肤配置文件信息，返回对象
            if (section != null)
                return section;
            return null;
        }

        public static List<SkinsObject> GetSkinList(XmlNode section)
        {
            //解析配置文件信息，返回对象
            List<SkinsObject> skinOjbList = new List<SkinsObject>();
            if (section != null)
            {
                foreach (XmlNode item in section.SelectNodes("companySkin"))
                {
                    SkinsObject skinObj = new SkinsObject();
                    switch (item.Attributes["CompanyId"].InnerText)
                    {
                        case "0001":
                            skinObj.CompanyId = item.Attributes["CompanyId"].InnerText;
                            skinObj.Company = item.Attributes["Company"].InnerText;
                            skinObj.SkinId = item.Attributes["SkinId"].InnerText;
                            skinObj.HostAddress = item.Attributes["HostAddress"].InnerText;
                            skinObj.IsReadOnly = Boolean.Parse(item.Attributes["ReadOnly"].InnerText);
                            skinOjbList.Add(skinObj);
                            break;
                        case "0002":
                            skinObj.CompanyId = item.Attributes["CompanyId"].InnerText;
                            skinObj.Company = item.Attributes["Company"].InnerText;
                            skinObj.SkinId = item.Attributes["SkinId"].InnerText;
                            skinObj.HostAddress = item.Attributes["HostAddress"].InnerText;
                            skinObj.IsReadOnly = Boolean.Parse(item.Attributes["ReadOnly"].InnerText);
                            skinOjbList.Add(skinObj);
                            break;
                        case "0003":
                            skinObj.CompanyId = item.Attributes["CompanyId"].InnerText;
                            skinObj.Company = item.Attributes["Company"].InnerText;
                            skinObj.SkinId = item.Attributes["SkinId"].InnerText;
                            skinObj.HostAddress = item.Attributes["HostAddress"].InnerText;
                            skinObj.IsReadOnly = Boolean.Parse(item.Attributes["ReadOnly"].InnerText);
                            skinOjbList.Add(skinObj);
                            break;
                        case "0004":
                            skinObj.CompanyId = item.Attributes["CompanyId"].InnerText;
                            skinObj.Company = item.Attributes["Company"].InnerText;
                            skinObj.SkinId = item.Attributes["SkinId"].InnerText;
                            skinObj.HostAddress = item.Attributes["HostAddress"].InnerText;
                            skinObj.IsReadOnly = Boolean.Parse(item.Attributes["ReadOnly"].InnerText);
                            skinOjbList.Add(skinObj);
                            break;
                        default:
                            break;
                    }
                }
            }

            return skinOjbList;
        }
    }
}
