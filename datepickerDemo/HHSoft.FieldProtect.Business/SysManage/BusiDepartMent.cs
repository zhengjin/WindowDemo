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
    public class BusiDepartMent
    {
        public BusiDepartMent() {}

        public DataTable GetDeptMentByCode(string deptCode)
        {
            string strSql = "select * from department where DeptCode = '{0}'";
            strSql = string.Format(strSql, deptCode);
            return OracleHelper.ExecuteDataTable(strSql);
        }

        public bool ManageDeartMent(Department dept)
        {
            string strSql = string.Empty;
            switch (dept.Action)
            {
                case ActionEnum.Insert:
                    strSql = "select nvl(Max(OrderNo),0) + 1 OrderNo from department where cCode = '" + dept.CompanyCode + "'";
                    string orderNo = OracleHelper.ExecuteDataTable(strSql).Rows[0][0].ToString();

                    strSql = "Insert into DepartMent(DeptCode,CCODE,DeptName,Description,OrderNo) Values ('{1}'||{0},'{1}','{2}','{3}','{4}')";
                    strSql = string.Format(strSql, "seq_department.Nextval", dept.CompanyCode, dept.DeptName, dept.Description, orderNo);
                    break;
                case ActionEnum.Update:
                    strSql = "Update DepartMent Set DeptName = '{1}',Description = '{2}' Where DeptCode = {0}";
                    strSql = string.Format(strSql, dept.DeptCode, dept.DeptName, dept.Description);
                    break;
                case ActionEnum.Delete:
                    strSql = "delete from DepartMent Where DeptCode = {0}";
                    strSql = string.Format(strSql, dept.DeptCode);
                    break;
            }
            return OracleHelper.ExecuteCommand(strSql);
        }

        public string ValidateDelete(string deptCode)
        {
            string strSql = string.Empty;
            strSql = "select Count(*) from users where deptCode = " + deptCode + "";
            if (OracleHelper.ExecuteDataTable(strSql).Rows[0][0].ToString() != "0")
            {
                return "1";////部门下存在用户
            }
            return "0";
        }

        /// <summary>
        /// 获得部门的集合
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public IList<Department> GetDepartMent(Department dept)
        {
            IList<Department> datalist = new List<Department>();
            string strWhere = string.Empty;

            if (!string.IsNullOrEmpty(dept.CompanyCode)) strWhere = "a.CCODE = '" + dept.CompanyCode + "' And ";
            if (!string.IsNullOrEmpty(dept.DeptCode)) strWhere = "a.DeptCode in (" + dept.DeptCode + ")  And ";

            string strSql = "select * from Department a left join Company b on a.ccode = b.ccode where {0} 1 = 1  order by a.OrderNo";
            strSql = string.Format(strSql, strWhere);
            IDataReader dr = OracleHelper.ExecuteReader(strSql);
            while (dr.Read())
            {
                Department deptEntity = new Department();
                deptEntity.DeptCode = dr["DeptCode"].ToString();
                deptEntity.DeptName = dr["DeptName"].ToString();
                deptEntity.FullName = string.Format("{0}--{1}", dr["cName"].ToString(), dr["DeptName"].ToString());
                deptEntity.Description = dr["Description"].ToString();
                datalist.Add(deptEntity);
            }
            dr.Close();
            return datalist;
        }

        /// <summary>
        /// 查询部门
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public IList<Department> QueryDepartMent(Department dept)
        {
            IList<Department> datalist = new List<Department>();
            string strWhere = string.Empty;

            if (!string.IsNullOrEmpty(dept.CompanyCode)) strWhere += "a.CCODE like '" + CommonHelper.GetShortCode(dept.CompanyCode) + "%' And ";

            if (!string.IsNullOrEmpty(dept.QueryDeptLevel))
            {
                if (dept.QueryDeptLevel.IndexOf(",") == -1)
                {
                    if (dept.QueryDeptLevel.Equals(((int)CompanyTypeEnum.SHI).ToString()))
                    {
                        strWhere += "length(b.ShortCCode) = 4 And ";
                    }
                    if (dept.QueryDeptLevel.Equals(((int)CompanyTypeEnum.XIAN).ToString()))
                    {
                        strWhere += "length(b.ShortCCode) = 6 And ";
                    }
                }
            }

            string strSql = "select * from Department a left join Company b on a.ccode = b.ccode where {0} 1 = 1  order by  a.ccode, a.orderno";
            strSql = string.Format(strSql, strWhere);
            IDataReader dr = OracleHelper.ExecuteReader(strSql);
            while (dr.Read())
            {
                Department deptEntity = new Department();
                deptEntity.DeptCode = dr["DeptCode"].ToString();
                deptEntity.DeptName = dr["DeptName"].ToString();
                deptEntity.FullName = string.Format("{0}--{1}", dr["cName"].ToString(), dr["DeptName"].ToString());
                deptEntity.Description = dr["Description"].ToString();
                datalist.Add(deptEntity);
            }
            dr.Close();
            return datalist;
        }

        /// <summary>
        /// 根据组织结构取得部门
        /// </summary>
        /// <param name="compCode"></param>
        /// <returns></returns>
        public DataTable GetDeptMentInfo(string compCode)
        {
            string strSql = "select * from DepartMent where CCode = {0}";
            strSql = string.Format(strSql, compCode.Trim());
            return OracleHelper.ExecuteDataTable(strSql);
        }

        public DataTable GetDeptByName(string cCode, string deptCode, string deptName)
        {
            string strSql = string.Empty;
            if (deptCode == "0")
            {
                strSql = "select * from department where cCode = '{0}' and deptName = '{1}'";
                strSql = string.Format(strSql, cCode, deptName);
            }
            else
            {
                strSql = "select * from department where deptCode <> '{0}' and cCode = '{1}' and deptName = '{2}'";
                strSql = string.Format(strSql, deptCode, cCode, deptName);
            }
            return OracleHelper.ExecuteDataTable(strSql);
        }
    }
}
