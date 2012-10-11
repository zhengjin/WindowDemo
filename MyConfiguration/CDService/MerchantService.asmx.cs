using System;
using System.Web;
using CD.MyCommon;
using System.Data;
using System.Web.Services;
using System.Collections.Generic;

namespace CD.CDService
{
    /// <summary>
    /// Summary description for MerchantService
    /// </summary>
    [WebService(Namespace = "http://rcloud.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class MerchantService : System.Web.Services.WebService
    {
        #region Members

        #region Variables of member

        Merchandizer Merchant;

        #endregion

        #region Methods of member

        #region Join
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public DataSet GetNewMerchant()
        {
            return Merchant.GetNewMerchant();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantShip"></param>
        /// <returns></returns>
        [WebMethod]
        public bool Join(DataSet merchantSet)
        {
            return Merchant.JoinUs(merchantSet);
        }
        #endregion
        #region Login
        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantCode"></param>
        /// <param name="merchantName"></param>
        /// <param name="merchantPassword"></param>
        /// <returns></returns>
        [WebMethod]
        public DataSet Login(string merchantCode, string merchantName, string merchantPassword)
        {
            return Merchant.Identification(merchantCode, merchantName, merchantPassword);
        }
        #endregion

        #endregion

        #endregion

        #region Structures
        /// <summary>
        /// 
        /// </summary>
        public MerchantService()
        {
            Merchant = new Merchandizer();
        }
        #endregion
    }
}
