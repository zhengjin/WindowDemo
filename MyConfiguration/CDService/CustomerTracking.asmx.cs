using System;
using CD.MyCommon;
using System.Data;
using System.Web.Services;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CD.CDService
{
    /// <summary>
    /// Summary description for CustomerTracking
    /// </summary>
    [WebService(Namespace = "http://rcloud.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CustomerTracking : System.Web.Services.WebService
    {
        #region Members
        /// <summary>
        /// 
        /// </summary>
        protected enum OPType
        {
            /// <summary>
            /// 
            /// </summary>
            AccountList,
            /// <summary>
            /// 
            /// </summary>
            PaymentList,
            /// <summary>
            /// 
            /// </summary>
            Synchronous
        }
        #endregion

        #region MerchantInfo
        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantCode"></param>
        /// <param name="merchantName"></param>
        /// <param name="merchantPassword"></param>
        /// <returns></returns>
        [WebMethod]
        [XmlInclude(typeof(MerchandizerTest))]
        public MerchandizerTest Merchant(string merchantCode, string merchantName, string merchantPassword)
        {
            MerchandizerTest merchant = new MerchandizerTest();
            if (merchant.Identification(merchantCode, merchantName, merchantPassword))
            {
                merchant.LoadInformation();
            }
            return merchant;
        }
        #endregion
        #region HaveNewAccounts
        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchant"></param>
        /// <returns></returns>
        [WebMethod]
        [XmlInclude(typeof(MerchandizerTest))]
        public bool HaveNewAccounts(MerchandizerTest merchant)
        {
            if (merchant.IsVerified)
            {
                if (long.Parse(Common.ExecuteDataset(Common.CDServiceConnStr, CommandType.Text,
                    "SELECT COUNT(*) FROM [dbo].[Account] WHERE [MerchantID]=@MerchantID",
                    new SqlParameter("@MerchantID", merchant.MerchantID)).Tables[0].Rows[0][0].ToString()) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region BuildAccountList
        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchant"></param>
        /// <returns></returns>
        [WebMethod]
        [XmlInclude(typeof(MerchandizerTest))]
        public string BuildAccountList(MerchandizerTest merchant)
        {
            string fileName = String.Empty;
            if (merchant.IsVerified)
            {
                DataSet CustomerSet = new DataSet();
                CustomerSet = Common.ExecuteDataset(Common.CDServiceConnStr, CommandType.Text,
                    "SELECT * FROM [dbo].[Account] WHERE [MerchantID]=@MerchantID",
                    new SqlParameter("@MerchantID", merchant.MerchantID));
                if (CustomerSet != null && CustomerSet.Tables[0].Rows.Count > 0)
                {
                    fileName = Guid.NewGuid().ToString() + ".xml";
                    OutputFile(fileName, CustomerSet, OPType.AccountList);
                }
            }
            return fileName;
        }
        #endregion
        #region GetAccountList
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [WebMethod]
        [XmlInclude(typeof(MerchandizerTest))]
        public DataSet GetAccountList(MerchandizerTest merchant, string fileName)
        {
            if (merchant.IsVerified && !fileName.Equals(String.Empty))
            {
                DataSet AccountSet = new DataSet();
                InputFile(fileName, AccountSet, OPType.AccountList);
                if (AccountSet != null && AccountSet.Tables[0].Rows.Count > 0)
                {
                    return AccountSet;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        #endregion
        #region OverloadedAccountList
        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchant"></param>
        /// <param name="accountSet"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [WebMethod]
        [XmlInclude(typeof(MerchandizerTest))]
        public bool OverloadedAccountList(MerchandizerTest merchant, DataSet accountSet, string fileName)
        {
            if (merchant.IsVerified)
            {
                if (accountSet != null && accountSet.Tables[0].Rows.Count > 0 && !fileName.Equals(String.Empty))
                {
                    OutputFile(fileName, accountSet, OPType.AccountList);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region Synchronous
        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchant"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [WebMethod]
        [XmlInclude(typeof(MerchandizerTest))]
        public bool Synchronous(MerchandizerTest merchant, string fileName)
        {
            if (merchant.IsVerified)
            {
                int result = 0;
                DataSet AccountSet = new DataSet();
                InputFile(fileName, AccountSet, OPType.Synchronous);
                if (AccountSet != null && AccountSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in AccountSet.Tables[0].Rows)
                    {
                        if (Common.ExecuteQuery(Common.CDServiceConnStr, CommandType.Text,
                            "UPDATE [dbo].[Account] "
                            + "SET [BillDate]=@BillDate, "
                            + "[TotalBalance]=@TotalBalance, "
                            + "[TransferDate]=@TransferDate "
                            + "WHERE [MerchantID]=@MerchantID AND "
                            + "[GroupID]=@GroupID AND "
                            + "[ConsumerID]=@ConsumerID AND "
                            + "[AccountNumber]=@AccountNumber",
                            new SqlParameter("@BillDate", row["BillDate"].ToString()),
                            new SqlParameter("@TotalBalance", row["TotalBalance"].ToString()),
                            new SqlParameter("@TransferDate", row["TransferDate"].ToString()),
                            new SqlParameter("@MerchantID", row["MerchantID"].ToString()),
                            new SqlParameter("@GroupID", row["GroupID"].ToString()),
                            new SqlParameter("@ConsumerID", row["ConsumerID"].ToString()),
                            new SqlParameter("@AccountNumber", row["AccountNumber"].ToString())).Equals(1))
                        {
                            result++;
                        }
                    }
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region BuildPaymentList
        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchant"></param>
        /// <returns></returns>
        [WebMethod]
        [XmlInclude(typeof(MerchandizerTest))]
        public string BuildPaymentList(MerchandizerTest merchant)
        {
            string fileName = String.Empty;
            if (merchant.IsVerified)
            {
                DataSet PaymentSet = new DataSet();
                PaymentSet = Common.ExecuteDataset(Common.CDServiceConnStr, CommandType.Text,
                    "SELECT * FROM [dbo].[Payment] WHERE [MerchantID]=@MerchantID",
                    new SqlParameter("@MerchantID", merchant.MerchantID));
                if (PaymentSet != null && PaymentSet.Tables[0].Rows.Count > 0)
                {
                    fileName = Guid.NewGuid().ToString() + ".xml";
                    OutputFile(fileName, PaymentSet, OPType.PaymentList);
                }
            }
            return fileName;
        }
        #endregion
        #region GetPaymentList
        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchant"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [WebMethod]
        [XmlInclude(typeof(MerchandizerTest))]
        public DataSet GetPaymentList(MerchandizerTest merchant, string fileName)
        {
            if (merchant.IsVerified && !fileName.Equals(String.Empty))
            {
                DataSet PaymentSet = new DataSet();
                InputFile(fileName, PaymentSet, OPType.PaymentList);
                if (PaymentSet != null && PaymentSet.Tables[0].Rows.Count > 0)
                {
                    return PaymentSet;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region OutputFile
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="dsObject"></param>
        /// <param name="opType"></param>
        private void OutputFile(string fileName, DataSet dsObject, OPType opType)
        {
            switch (opType)
            {
                case OPType.AccountList:
                    {
                        //
                        break;
                    }
                case OPType.PaymentList:
                    {
                        //
                        break;
                    }
            }
            dsObject.WriteXml(Server.MapPath(String.Empty) + @"\" + fileName, XmlWriteMode.WriteSchema);
        }
        #endregion
        #region InputFile
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="dsObject"></param>
        /// <param name="opType"></param>
        private void InputFile(string fileName, DataSet dsObject, OPType opType)
        {
            switch (opType)
            {
                case OPType.AccountList:
                    {
                        //
                        break;
                    }
                case OPType.Synchronous:
                    {
                        //
                        break;
                    }
            }
            dsObject.ReadXml(Server.MapPath(String.Empty) + @"\" + fileName, XmlReadMode.ReadSchema);
        }
        #endregion
    }
}
