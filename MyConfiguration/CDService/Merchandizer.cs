using System;
using CD.MyCommon;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CD.CDService
{
    #region Cancellaction
    ///// <summary>
    ///// 
    ///// </summary>
    //[XmlRoot("RegionalShipper")]
    //public struct MerchantShip
    //{
    //    #region Variables of member

    //    #region Merchant ID
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    private string _merchantid;
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string MerchantID
    //    {
    //        get
    //        {
    //            return _merchantid;
    //        }
    //        set
    //        {
    //            _merchantid = Guid.NewGuid().ToString();
    //        }
    //    }
    //    #endregion
    //    #region Merchant Code
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    private string _merchantcode;
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string MerchantCode
    //    {
    //        get
    //        {
    //            return _merchantcode;
    //        }
    //        set
    //        {
    //            if (_merchantcode != value)
    //            {
    //                _merchantcode = value;
    //            }
    //        }
    //    }
    //    #endregion
    //    #region Merchant Name
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    private string _merchantname;
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string MerchantName
    //    {
    //        get
    //        {
    //            return _merchantname;
    //        }
    //        set
    //        {
    //            if (_merchantname != value)
    //            {
    //                _merchantname = value;
    //            }
    //        }
    //    }
    //    #endregion
    //    #region Merchant Password
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    private string _password;
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string MerchantPassword
    //    {
    //        get
    //        {
    //            return _password;
    //        }
    //        set
    //        {
    //            if (_password != value)
    //            {
    //                _password = value;
    //            }
    //        }
    //    }
    //    #endregion
    //    #region Service Address
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    private string _address;
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string ServiceAddress
    //    {
    //        get
    //        {
    //            return _address;
    //        }
    //        set
    //        {
    //            if (_address != value)
    //            {
    //                _address = value;
    //            }
    //        }
    //    }
    //    #endregion
    //    #region Country
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    private string _country;
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string Country
    //    {
    //        get
    //        {
    //            return _country;
    //        }
    //        set
    //        {
    //            if (_country != value)
    //            {
    //                _country = value;
    //            }
    //        }
    //    }
    //    #endregion
    //    #region City
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    private string _city;
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string City
    //    {
    //        get
    //        {
    //            return _city;
    //        }
    //        set
    //        {
    //            if (_city != value)
    //            {
    //                _city = value;
    //            }
    //        }
    //    }
    //    #endregion
    //    #region State
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    private string _state;
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string State
    //    {
    //        get
    //        {
    //            return _state;
    //        }
    //        set
    //        {
    //            if (_state != value)
    //            {
    //                _state = value;
    //            }
    //        }
    //    }
    //    #endregion
    //    #region Phone
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    private string _phone;
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string Phone
    //    {
    //        get
    //        {
    //            return _phone;
    //        }
    //        set
    //        {
    //            if (_phone != value)
    //            {
    //                _phone = value;
    //            }
    //        }
    //    }
    //    #endregion
    //    #region Fax
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    private string _fax;
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string Fax
    //    {
    //        get
    //        {
    //            return _fax;
    //        }
    //        set
    //        {
    //            if (_fax != value)
    //            {
    //                _fax = value;
    //            }
    //        }
    //    }
    //    #endregion
    //    #region Email
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    private string _email;
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string Email
    //    {
    //        get
    //        {
    //            return _email;
    //        }
    //        set
    //        {
    //            if (_email != value)
    //            {
    //                _email = value;
    //            }
    //        }
    //    }
    //    #endregion
    //    #region Build Date
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    private DateTime _build;
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public DateTime BuildDate
    //    {
    //        get
    //        {
    //            return _build;
    //        }
    //        set
    //        {
    //            if (_build != value)
    //            {
    //                _build = value;
    //            }
    //        }
    //    }
    //    #endregion
    //    #region Effective Date
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    private Int32 _effective;
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public Int32 EffectiveDate
    //    {
    //        get
    //        {
    //            return _effective;
    //        }
    //        set
    //        {
    //            if (_effective != value)
    //            {
    //                _effective = value;
    //            }
    //        }
    //    }
    //    #endregion
    //    #region Shutoff
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    private bool _shutoff;
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public bool Shutoff
    //    {
    //        get
    //        {
    //            return _shutoff;
    //        }
    //        set
    //        {
    //            if (_shutoff != value)
    //            {
    //                _shutoff = value;
    //            }
    //        }
    //    }
    //    #endregion

    //    #endregion
    //}
    #endregion

    /// <summary>
    /// 
    /// </summary>
    public class Merchandizer
    {
        #region Members

        #region Variables of member

        #endregion

        #region Methods of member

        #region Get New Merchant
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal DataSet GetNewMerchant()
        {
            DataSet MerchantSet = new DataSet();
            MerchantSet = Identification(String.Empty, String.Empty, String.Empty);
            if (MerchantSet != null && MerchantSet.Tables.Count.Equals(1))
            {
                DataRow MerchantRow = MerchantSet.Tables[0].NewRow();
                MerchantRow["MerchantCode"]=DateTime.Now.ToString("yyyyMMddhhmmss") 
                    + Guid.NewGuid().ToString().Split('-')[0];
                MerchantSet.Tables[0].Rows.Add(MerchantRow);
            }
            return MerchantSet;
        }
        #endregion
        #region John Us
        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantSet"></param>
        /// <returns></returns>
        internal bool JoinUs(DataSet merchantSet)
        {
            bool result = false;
            if (merchantSet != null && merchantSet.Tables[0].Rows.Count.Equals(1))
            {
                try
                {
                    if (Common.ExecuteQuery(Common.CDServiceConnStr, CommandType.Text,
                        "INSERT INTO [dbo].[Merchant](" +
                        "[MerchantID], [MerchantCode], " +
                        "[MerchantName], [MerchantPassword], " +
                        "[ServiceAddress], [Country], " +
                        "[City], [State], [Zip], [Phone], " +
                        "[Fax], [Email], [BuildDate], " +
                        "[EffectiveDays], [Shutoff]" +
                        ")" +
                        "VALUES(" +
                        "@MerchantID, @MerchantCode, " +
                        "@MerchantName, @MerchantPassword, " +
                        "@ServiceAddress, @Country, " +
                        "@City, @State, @Zip, @Phone, @Fax, @Email, " +
                        "@BuildDate, @EffectiveDays, @Shutoff" +
                        ")",
                        new SqlParameter("@MerchantID", merchantSet.Tables[0].Rows[0]["MerchantID"].ToString()),
                        new SqlParameter("@MerchantCode", merchantSet.Tables[0].Rows[0]["MerchantCode"].ToString()),
                        new SqlParameter("@MerchantName", merchantSet.Tables[0].Rows[0]["MerchantName"].ToString()),
                        new SqlParameter("@MerchantPassword", merchantSet.Tables[0].Rows[0]["MerchantPassword"].ToString()),
                        new SqlParameter("@ServiceAddress", merchantSet.Tables[0].Rows[0]["ServiceAddress"].ToString()),
                        new SqlParameter("@Country", merchantSet.Tables[0].Rows[0]["Country"].ToString()),
                        new SqlParameter("@City", merchantSet.Tables[0].Rows[0]["City"].ToString()),
                        new SqlParameter("@State", merchantSet.Tables[0].Rows[0]["State"].ToString()),
                        new SqlParameter("@Zip", merchantSet.Tables[0].Rows[0]["Zip"].ToString()),
                        new SqlParameter("@Phone", merchantSet.Tables[0].Rows[0]["Phone"].ToString()),
                        new SqlParameter("@Fax", merchantSet.Tables[0].Rows[0]["Fax"].ToString()),
                        new SqlParameter("@Email", merchantSet.Tables[0].Rows[0]["Email"].ToString()),
                        new SqlParameter("@BuildDate", (DateTime)merchantSet.Tables[0].Rows[0]["BuildDate"]),
                        new SqlParameter("@EffectiveDays", (int)merchantSet.Tables[0].Rows[0]["EffectiveDays"]),
                        new SqlParameter("@Shutoff", (Boolean)merchantSet.Tables[0].Rows[0]["Shutoff"])
                        ).Equals(1))
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
                catch(Exception ex)
                {
                    result = false;
                }
                finally
                {
                    //
                }
            }
            else
            {
                result = false;
            }
            return result;
        }
        #endregion
        #region Identification
        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantCode"></param>
        /// <param name="merchantName"></param>
        /// <param name="merchantPassword"></param>
        /// <returns></returns>
        internal DataSet Identification(string merchantCode, string merchantName, string merchantPassword)
        {
            DataSet MerchantSet = new DataSet();
            MerchantSet = Common.ExecuteDataset(Common.CDServiceConnStr, CommandType.Text,
                "SELECT * FROM [dbo].[Merchant] " +
                "WHERE [MerchantCode]=@MerchantCode " +
                "AND [MerchantName]=@MerchantName " +
                "AND [MerchantPassword]=@MerchantPassword",
                new SqlParameter("@MerchantCode", merchantCode),
                new SqlParameter("@MerchantName", merchantName),
                new SqlParameter("@MerchantPassword", merchantPassword));
            //if (MerchantSet != null && MerchantSet.Tables[0].Rows.Count > 0)
            //{
            //    return MerchantSet;
            //}
            //else
            //{
            //    return null;
            //}
            return MerchantSet;
        }
        #endregion

        #endregion

        #endregion

        #region Structures
        /// <summary>
        /// 
        /// </summary>
        public Merchandizer() { }
        #endregion
    }
}