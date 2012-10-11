using CD.MyCommon;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CD.CDService
{
    #region Cancellation
    ///// <summary>
    ///// 
    ///// </summary>
    //public abstract class Merchant
    //{

    //    #region Members

    //    #region IsVerified
    //    private bool _isverified;
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public bool IsVerified
    //    {
    //        get
    //        {
    //            return _isverified;
    //        }
    //    }
    //    #endregion
    //    #region MerchantID
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
    //            if (_merchantid != value)
    //            {
    //                _merchantid = value;
    //            }
    //        }
    //    }
    //    #endregion
    //    #region MerchantCode
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
    //    #region MerchantName
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
    //    #region MerchantPassword
    //    private string _merchantpassword;
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string MerchantPassword
    //    {
    //        set
    //        {
    //            if (_merchantpassword != value)
    //            {
    //                _merchantpassword = value;
    //            }
    //        }
    //    }
    //    #endregion
    //    #region ServiceAddress
    //    private string _serviceaddress;
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string ServiceAddress
    //    {
    //        get
    //        {
    //            return _serviceaddress;
    //        }
    //        set
    //        {
    //            if (_serviceaddress != value)
    //            {
    //                _serviceaddress = value;
    //            }
    //        }
    //    }
    //    #endregion
    //    #region Country
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
    //    #region MaxGroup
    //    private string _maxgroup;
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string MaxGroup
    //    {
    //        get
    //        {
    //            return _maxgroup;
    //        }
    //        set
    //        {
    //            if (_maxgroup != value)
    //            {
    //                _maxgroup = value;
    //            }
    //        }
    //    }
    //    #endregion
    //    #region ShutOff
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
    //    #region BuildDate
    //    private string _builddate;
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string BuildDate
    //    {
    //        get
    //        {
    //            return _builddate;
    //        }
    //        set
    //        {
    //            if (_builddate != value)
    //            {
    //                _builddate = value;
    //            }
    //        }
    //    }
    //    #endregion
    //    #region EffectiveDays
    //    private string _effectivedays;
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public string EffectiveDays
    //    {
    //        get
    //        {
    //            return _effectivedays;
    //        }
    //        set
    //        {
    //            if (_effectivedays != value)
    //            {
    //                _effectivedays = value;
    //            }
    //        }
    //    }
    //    #endregion

    //    #endregion

    //    #region Methods
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="merchantCode"></param>
    //    /// <param name="merchantName"></param>
    //    /// <param name="merchantPassword"></param>
    //    /// <returns></returns>
    //    public bool Identification(string merchantCode, string merchantName, string merchantPassword)
    //    {

    //    }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public void LoadInformation()
    //    {
    //        //
    //    }
    //    #endregion
    //}
    #endregion

    /// <summary>
    /// 
    /// </summary>
    [XmlRoot("RegionalShipper")]
    public class MerchandizerTest
    {
        #region Variables of member
        private bool _isverified;
        private DataSet _merchantset;
        private string _merchantid;
        private string _merchantcode;
        private string _merchantname;
        private string _merchantpassword;
        private string _serviceaddress;
        private string _country;
        private string _city;
        private string _state;
        private string _phone;
        private string _fax;
        private string _email;
        private string _maxgroup;
        private bool _shutoff;
        private string _builddate;
        private string _effectivedays;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public MerchandizerTest()
        {
            //
        }

        #region Merchant Members
        #region Properties of member
        /// <summary>
        /// 
        /// </summary>
        public bool IsVerified
        {
            get
            {
                return _isverified;
            }
            set
            {
                if (_isverified != value)
                {
                    _isverified = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MerchantID
        {
            get
            {
                return _merchantid;
            }
            set
            {
                if (_merchantid != value)
                {
                    _merchantid = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MerchantCode
        {
            get
            {
                return _merchantcode;
            }
            set
            {
                if (_merchantcode != value)
                {
                    _merchantcode = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MerchantName
        {
            get
            {
                return _merchantname;
            }
            set
            {
                if (_merchantname != value)
                {
                    _merchantname = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MerchantPassword
        {
            set
            {
                if (_merchantpassword != value)
                {
                    _merchantpassword = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ServiceAddress
        {
            get
            {
                return _serviceaddress;
            }
            set
            {
                if (_serviceaddress != value)
                {
                    _serviceaddress = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                if (_country != value)
                {
                    _country = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                if (_city != value)
                {
                    _city = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string State
        {
            get
            {
                return _state;
            }
            set
            {
                if (_state != value)
                {
                    _state = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                if (_phone != value)
                {
                    _phone = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Fax
        {
            get
            {
                return _fax;
            }
            set
            {
                if (_fax != value)
                {
                    _fax = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if (_email != value)
                {
                    _email = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MaxGroup
        {
            get
            {
                return _maxgroup;
            }
            set
            {
                if (_maxgroup != value)
                {
                    _maxgroup = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Shutoff
        {
            get
            {
                return _shutoff;
            }
            set
            {
                if (_shutoff != value)
                {
                    _shutoff = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BuildDate
        {
            get
            {
                return _builddate;
            }
            set
            {
                if (_builddate != value)
                {
                    _builddate = value;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EffectiveDays
        {
            get
            {
                return _effectivedays;
            }
            set
            {
                if (_effectivedays != value)
                {
                    _effectivedays = value;
                }
            }
        }
        #endregion

        #region Methods of member
        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantCode"></param>
        /// <param name="merchantName"></param>
        /// <param name="merchantPassword"></param>
        /// <returns></returns>
        public bool Identification(string merchantCode, string merchantName, string merchantPassword)
        {
            _merchantset = Common.ExecuteDataset(Common.CDServiceConnStr, CommandType.Text,
                "SELECT * FROM [dbo].[Merchant] WHERE [MerchantCode]=@MerchantCode AND [MerchantName]=@MerchantName",
                new SqlParameter("@MerchantCode", merchantCode), new SqlParameter("@MerchantName", merchantName));
            if (_merchantset != null && _merchantset.Tables[0].Rows.Count.Equals(1) && _merchantset.Tables[0].Rows[0]["MerchantPassword"].ToString().Equals(merchantPassword))
            {
                _isverified = true;
            }
            else
            {
                _merchantset.Dispose();
                _merchantset = null;
                _isverified = false;
            }
            return _isverified;
        }
        /// <summary>
        /// 
        /// </summary>
        public void LoadInformation()
        {
            if (_merchantset != null && _merchantset.Tables[0].Rows.Count > 0)
            {
                _merchantid = _merchantset.Tables[0].Rows[0]["MerchantID"].ToString();
                _merchantcode = _merchantset.Tables[0].Rows[0]["MerchantCode"].ToString();
                _merchantname = _merchantset.Tables[0].Rows[0]["MerchantName"].ToString();
                _serviceaddress = _merchantset.Tables[0].Rows[0]["ServiceAddress"].ToString();
                _country = _merchantset.Tables[0].Rows[0]["Country"].ToString();
                _city = _merchantset.Tables[0].Rows[0]["City"].ToString();
                _state = _merchantset.Tables[0].Rows[0]["State"].ToString();
                _phone = _merchantset.Tables[0].Rows[0]["Phone"].ToString();
                _fax = _merchantset.Tables[0].Rows[0]["Fax"].ToString();
                _email = _merchantset.Tables[0].Rows[0]["Email"].ToString();
                _maxgroup = _merchantset.Tables[0].Rows[0]["MaxGroup"].ToString();
                _shutoff = (bool)_merchantset.Tables[0].Rows[0]["Shutoff"];
                _builddate = _merchantset.Tables[0].Rows[0]["BuildDate"].ToString();
                _effectivedays = _merchantset.Tables[0].Rows[0]["EffectiveDays"].ToString();
            }
        }
        #endregion
        #endregion
    }
}