using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CDPro.BLL
{
    public class SkinsObject
    {
        private string _company;
        private string _companyId;
        private string _skinId;
        private bool _isReadOnly;
        private string _hostAddress;
        /// <summary>
        /// 公司名称
        /// </summary>
        public string Company
        {
            get { return _company; }
            set { _company = value; }
        }

        /// <summary>
        /// 公司编号
        /// </summary>
        public string CompanyId
        {
            get { return _companyId; }
            set { _companyId = value; }
        }

        /// <summary>
        /// 域名
        /// </summary>
        public string HostAddress
        {
            get { return _hostAddress; }
            set { _hostAddress = value; }
        }

        /// <summary>
        /// 皮肤ID
        /// </summary>
        public string SkinId
        {
            get { return _skinId; }
            set { _skinId = value; }
        }
        /// <summary>
        /// 是否只读
        /// </summary>
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set { _isReadOnly = value; }
        }
    }
}
