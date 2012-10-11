using System;
using System.Web;
using System.Web.Services;
using System.Collections.Generic;

namespace CD.CDService
{
    /// <summary>
    /// Summary description for CommodityService
    /// </summary>
    [WebService(Namespace = "http://rclound.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class CommodityService : System.Web.Services.WebService
    {
        #region Structures
        /// <summary>
        /// 
        /// </summary>
        public CommodityService() { }
        #endregion
    }
}
