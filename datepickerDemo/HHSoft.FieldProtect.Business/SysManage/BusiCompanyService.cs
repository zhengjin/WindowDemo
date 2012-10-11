using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.SysManage;
using System.Data.OracleClient;
using HHSoft.FieldProtect.DataAccess;
using System.Data;
using HHSoft.FieldProtect.DataEntity;
using HHSoft.FieldProtect.Framework.Utility;

namespace HHSoft.FieldProtect.Business.SysManage
{
    public class BusiCompanyService
    {
        public BusiCompanyService() { }

        public IList<Company> GetCompany(Company com)
        {
            string strWhere = string.Empty;
            if (!string.IsNullOrEmpty(com.ShortCcode)) strWhere = string.Format("substr(shortcCode,0,{0}) = '{1}' And",
                com.ShortCcode.Length.ToString(), com.ShortCcode);

            string strSql = "select * from Company where {0} 1=1 order by cCode";
            strSql = string.Format(strSql, strWhere);

            OracleDataReader dr = OracleHelper.ExecuteReader(strSql);
            IList<Company> datalist = new List<Company>();
            while (dr.Read())
            {
                Company company = new Company();                
                company.CompanyCode = dr["CCode"].ToString();
                company.ShortCcode = dr["shortCCode"].ToString();
                company.Name = dr["CNAME"].ToString();
                company.CompanyType = dr["cType"].ToString();
                company.OrderNo = int.Parse(dr["OrderNo"].ToString());               
                datalist.Add(company);
            }
            dr.Close();
            return datalist;
        }

        public IList<Company> GetCompanyInfo()
        {
            string strSql = "select * from Company order by cCode";            

            OracleDataReader dr = OracleHelper.ExecuteReader(strSql);
            IList<Company> datalist = new List<Company>();
            while (dr.Read())
            {
                Company company = new Company();
                company.CompanyCode = dr["CCode"].ToString();
                company.ShortCcode = dr["shortCCode"].ToString();
                company.Name = dr["CNAME"].ToString();
                company.CompanyType = dr["cType"].ToString();
                company.OrderNo = int.Parse(dr["OrderNo"].ToString());
                datalist.Add(company);
            }
            dr.Close();
            return datalist;
        }

        public DataTable GetCompanyByCode(string nodeCode)
        {
            string strSql = "select * from Company where shortCCode = {0}";
            strSql = string.Format(strSql, nodeCode.Trim());
            return OracleHelper.ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 取得组织机构信息
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public DataTable GetCompanyInfo(string compcode,int len)
        {
            string strSql = "select * from Company where  ccode like '{0}%' and  length(shortCCode) = {1}";
            strSql = string.Format(strSql, compcode,len);
            return OracleHelper.ExecuteDataTable(strSql);
        }

        public bool ManageCompany(Company com)
        {
            string[] strSql = null;
            switch (com.Action)
            {
                case ActionEnum.Insert:

                    strSql = new string[3];

                    strSql[0] = "insert into Company"
                    + " select '{0}','{1}','{2}','{3}','{4}','{5}',"
                    + " nvl(Max(OrderNo),0) + 1 OrderNo from Company "
                    + " where substr(ShortcCode, 0, {6}) = '{7}' and length(ShortcCode) = {8}";

                    strSql[0] = string.Format(strSql[0], com.CompanyCode, com.ShortCcode, com.CompanyType, com.Name, com.NameJc, com.Description,
                        com.ParentCode.Length.ToString(), com.ParentCode, (com.ParentCode.Length + 2).ToString(), com.CompanyCode);
                    strSql[1] = "insert into users (userId,ccode,userName,password,realname,sex,state,createdate) values ({0},'{1}','{2}','{3}','{4}',{5},{6},{7})";
                    strSql[1] = string.Format(strSql[1], com.CompanyCode, com.CompanyCode, com.CompanyCode, EncryptHelper.EncryptString(com.CompanyCode), "系统管理员",
                        "0", "0", "sysdate");
                    strSql[2] = "Insert into usersandrole values ({0},'{1}')";
                    strSql[2] = string.Format(strSql[2], com.CompanyCode, "1");
                    break;
                case ActionEnum.Update:
                    strSql = new string[1];
                    strSql[0] = "Update Company set CType = '{1}',cName = '{2}',cNameJC = '{3}',cDescription = '{4}' Where ShortcCode = '{0}'";
                    strSql[0] = string.Format(strSql[0], com.ShortCcode,  com.CompanyType, com.Name, com.NameJc, com.Description);
                    break;
                case ActionEnum.Delete:
                    strSql = new string[3];
                    strSql[0] = "delete from Company Where CCODE = '{0}'";
                    strSql[0] = string.Format(strSql[0], com.CompanyCode);                    
                    strSql[1] = "delete from usersandrole where userId in (select userid from users where UserName = '{0}')";
                    strSql[1] = string.Format(strSql[1], com.CompanyCode);
                    strSql[2] = "delete from users where UserName = '{0}'";
                    strSql[2] = string.Format(strSql[2], com.CompanyCode);

                    break;
            }
            return OracleHelper.ExecuteCommand(strSql);
        }

        public string ValidateDelete(string shortCode)
        {
            string strSql = string.Empty;
            strSql = "select Count(*) from Users where CCODE = " + shortCode.PadRight(6, '0') + " and UserName <> '" + shortCode.PadRight(6, '0') + "'";
            if (OracleHelper.ExecuteDataTable(strSql).Rows[0][0].ToString() != "0")
            {
                return "1";////单位下存在管理员
            }

            strSql = "select Count(*) from department where CCODE = " + shortCode.PadRight(6, '0') + " ";
            if (OracleHelper.ExecuteDataTable(strSql).Rows[0][0].ToString() != "0")
            {
                return "2";////单位下存在部门
            }

            strSql = "select Count(*) from Company where substr(ShortCCODE, 0, {1}) = '{0}'";
            strSql = string.Format(strSql, shortCode, shortCode.Length.ToString());
            if (OracleHelper.ExecuteDataTable(strSql).Rows[0][0].ToString() != "1")
            {
                return "3";////存在下级单位
            }
            return "0";
        }

        public DataTable GetCompanyBycCode(string cCode)
        {
            string strSql = string.Empty;
            strSql = "select * from Company where cCode = '{0}'";
            strSql = string.Format(strSql, cCode);
            
            return OracleHelper.ExecuteDataTable(strSql);
        }
    }
}
