using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using System.Collections.Specialized;

namespace MyConfiguration
{
    /// <summary>
    /// Class: MySectionClass
    /// </summary>
    public class MySectionClass : IConfigurationSectionHandler
    {

        #region IConfigurationSectionHandler Members
        /// <summary>
        /// Methods: Create
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="configContext"></param>
        /// <param name="section"></param>
        /// <returns></returns>
        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            NameValueCollection configs;
            NameValueSectionHandler baseHandler = new NameValueSectionHandler();
            configs = (NameValueCollection)baseHandler.Create(parent, configContext, section);
            return configs;
        }

        #endregion
    }
}