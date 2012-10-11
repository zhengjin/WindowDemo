using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HHSoft.FieldProtect.DataAccess;
using HHSoft.FieldProtect.Framework.Utility;
using HHSoft.FieldProtect.DataEntity.SysManage;
using System.Data.OracleClient;
using HHSoft.FieldProtect.DataEntity;
using System.Collections;

namespace HHSoft.FieldProtect.Business.SysManage
{
    public class BusiUserService
    {
        public BusiUserService()
        { 
        }

        public bool ChanagePassWord(string userId, string OldPws, string NewPws, out string Message)
        {
            string strSql = string.Empty;
            strSql = "select * from users where userId = {0} and PassWord ='{1}'";
            strSql = string.Format(strSql, userId, EncryptHelper.EncryptString(OldPws.Trim()));
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count == 0)
            {
                Message = "原密码错误";
                return false;
            }
            strSql = "Update users Set PassWord = '{1}' where userId = {0}";
            strSql = string.Format(strSql, userId, EncryptHelper.EncryptString(NewPws.Trim()));
            Message = string.Empty;
            return OracleHelper.ExecuteCommand(strSql);
        }

        public DataTable GetUsers(Users user, out int recordCount)
        {
            string strWhere = string.Empty;
            //if (!string.IsNullOrEmpty(user.FilterUser)) strWhere = "upper(userName) not in ('" + user.FilterUser + "') And ";
            if (!string.IsNullOrEmpty(user.CompanyCode)) strWhere += "a.cCode = '" + user.CompanyCode + "' And ";
            if (!string.IsNullOrEmpty(user.DepartCode)) strWhere += "a.DeptCode = '" + user.DepartCode + "' And ";
            if (!string.IsNullOrEmpty(user.UserName)) strWhere += "userName like '%" + user.UserName + "%' And ";
            if (!string.IsNullOrEmpty(user.RealName)) strWhere += "realName like '%" + user.RealName + "%' And ";


            string strSql = "select a.*,b.cname,c.deptname from users a left join company b on a.cCode = b.cCode"                      
                      + " left join department c on a.deptcode = c.deptcode"
                      + " where {0} a.username <> b.ccode order by a.cCode,a.createDate desc";
            strSql = string.Format(strSql, strWhere);

            #region 设置参数
            OracleParameter pageSize = new OracleParameter("p_PageSize", OracleType.Number, 10);
            pageSize.Value = user.PageSize;
            OracleParameter pageIndex = new OracleParameter("p_PageIndex", OracleType.Number, 10);
            pageIndex.Value = user.PageIndex;
            OracleParameter sqlSelect = new OracleParameter("p_SqlSelect", OracleType.VarChar, 4000);
            sqlSelect.Value = strSql;
            OracleParameter recount = new OracleParameter("p_OutRecordCount", OracleType.Number, 5);
            recount.Direction = ParameterDirection.Output;
            OracleParameter returnCursor = new OracleParameter("p_OutCursor", OracleType.Cursor);
            returnCursor.Direction = ParameterDirection.Output;

            OracleParameter[] oracleParameters = { pageSize, pageIndex, sqlSelect, recount, returnCursor };
            #endregion

            DataTable dt = OracleHelper.ExecuteDataTable("CommonPackages.sp_Page", oracleParameters);
            recordCount = int.Parse(recount.Value.ToString());
            return dt;
        }

        public bool ManageUsers(Users user)
        {            
            ArrayList strSql = new ArrayList();
            string tmpSql = string.Empty;
            string[] roleAry = null;
            switch (user.Action)
            {
                case ActionEnum.Insert:
                    roleAry = user.Role.RoleId.Split(new char[] { ',' });
                    tmpSql = "select seq_user.nextval from dual";                    
                    string userId = string.Format("{0}{1}", user.CompanyCode,
                        OracleHelper.ExecuteDataTable(tmpSql).Rows[0][0].ToString());

                    tmpSql = "Insert into Users(UserId,CCODE,DeptCode,UserName,PassWord,RealName,Sex,Telephone,State,CreateDate)"
                           + " Values ({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}',to_date('{9}','yyyy-mm-dd hh24:mi:ss'))";
                    tmpSql = string.Format(tmpSql, userId, user.CompanyCode, user.DepartCode, user.UserName, user.PassWord, user.RealName,
                        user.Sex ? "0" : "1", user.TelePhone, user.State ? "0" : "1", user.CreateDate.ToString());
                    strSql.Add(tmpSql);
                    for (int i = 0; i < roleAry.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(roleAry[i]))
                        {
                            tmpSql = "Insert into usersandrole values ({0},'{1}')";
                            tmpSql = string.Format(tmpSql, userId, roleAry[i]);
                            strSql.Add(tmpSql);
                        }
                    }
                    break;
                case ActionEnum.Update:
                    roleAry = user.Role.RoleId.Split(new char[] { ',' });

                    tmpSql = "Update Users Set CCODE = '{1}',DeptCode = '{2}',RealName = '{3}',Sex = '{4}',Telephone = '{5}',State = '{6}' Where UserId = {0}";
                    tmpSql = string.Format(tmpSql, user.UserId, user.CompanyCode, user.DepartCode, user.RealName, user.Sex ? "0" : "1", user.TelePhone, user.State ? "0" : "1");
                    strSql.Add(tmpSql);
                    tmpSql = "delete from usersandrole where UserId = " + user.UserId;
                    strSql.Add(tmpSql);
                    for (int i = 0; i < roleAry.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(roleAry[i]))
                        {
                            tmpSql = "Insert into usersandrole values ({0},'{1}')";
                            tmpSql = string.Format(tmpSql, user.UserId, roleAry[i]);
                            strSql.Add(tmpSql);
                        }
                    }
                    break;
                case ActionEnum.Delete:                    
                    tmpSql = "delete from Users Where UserId = {0}";
                    tmpSql = string.Format(tmpSql, user.UserId);
                    strSql.Add(tmpSql);
                    tmpSql = "delete from usersandrole where UserId = " + user.UserId;
                    strSql.Add(tmpSql);
                    break;
            }
            return OracleHelper.ExecuteCommand(strSql);
        }

        public bool UpdataUserInfo(Users user)
        {
            string strSql = "Update Users set RealName = '{1}',Sex = '{2}', TelePhone = '{3}' where UserId = {0}";
            strSql = string.Format(strSql, user.UserId, user.RealName, user.Sex ? "0" : "1", user.TelePhone);
            return OracleHelper.ExecuteCommand(strSql);
        }

        public DataTable GetUserByName(string userName,string cCode)
        {
            string strSql = "select * from Users where substr(ccode,0,4) = '{1}' and UserName = '{0}'";
            strSql = string.Format(strSql, userName, cCode.Substring(0, 4));
            return OracleHelper.ExecuteDataTable(strSql);
        }

        public DataTable GetUserById(string userId)
        {
            string strSql = "select * from Users a "
            + " left join Company b on a.cCode = b.cCode"            
            + " where a.UserId = '{0}'";
            strSql = string.Format(strSql, userId);
            return OracleHelper.ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 根据部门编号取得用户
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public DataTable GetUserByDeptCode(string deptCode)
        {
            string strSql = "select * from Users where DEPTCODE = '{0}'";
            strSql = string.Format(strSql, deptCode);
            return OracleHelper.ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 根据userid取得用户姓名
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        public string RealName(string userid)
        {
            string strSql = "select realname from users a where a.userid= '{0}'";
            strSql = string.Format(strSql, userid);
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["realname"].ToString();

            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 根据用户ID取得行政区代码
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public string DistrictCode(string userid)
        {
            string strSql = "select ccode from users a where a.userid= '{0}'";
            strSql = string.Format(strSql, userid);
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["ccode"].ToString();
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 根据userid取得部门名称
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public string DeptName(string userid)
        {
            string strSql = "select b.deptname from Users a "
            + " left join department b on a.deptCode = b.deptCode"
            + " where a.UserId = '{0}'";
            strSql = string.Format(strSql, userid);
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["deptname"].ToString();

            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 根据userid取得单位名称
        /// </summary>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        public string CompanyName(string userid)
        {
            string strSql = "select b.cname from Users a "
            + " left join Company b on a.cCode = b.cCode"
            + " where a.UserId = '{0}'";
            strSql = string.Format(strSql, userid);
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["cname"].ToString();

            }
            else
            {
                return "";
            }
        }

        public IList<Users> GetUsers(Users user)
        {
            IList<Users> datalist = new List<Users>();
            string strWhere = string.Empty;

            if (!string.IsNullOrEmpty(user.CompanyCode)) strWhere += "a.ccode like '" + user.CompanyCode + "%' And ";
            if (!user.IsFilterSYSUser) strWhere += "a.username <> b.ccode And ";
            if (!string.IsNullOrEmpty(user.UserId)) strWhere += "a.userId In (" + user.UserId + ") And ";

            if (!string.IsNullOrEmpty(user.QueryUserLevel))
            {
                if (user.QueryUserLevel.IndexOf(",") == -1)
                {
                    if (user.QueryUserLevel.Equals(((int)CompanyTypeEnum.SHI).ToString()))
                    {
                        strWhere += "length(b.ShortCCode) = 4 And ";
                    }
                    if (user.QueryUserLevel.Equals(((int)CompanyTypeEnum.XIAN).ToString()))
                    {
                        strWhere += "length(b.ShortCCode) = 6 And ";
                    }
                }
            }

            string strSql = "select * from Users a left join company b on a.cCode = b.cCode where {0} 1 = 1  order by a.cCode, a.RealName";
            strSql = string.Format(strSql, strWhere);
            IDataReader dr = OracleHelper.ExecuteReader(strSql);
            while (dr.Read())
            {
                Users userEntity = new Users();
                userEntity.UserId = dr["UserId"].ToString();
                userEntity.UserName = dr["UserName"].ToString();
                userEntity.RealName = dr["RealName"].ToString();
                userEntity.FullName = string.Format("{0}--{1}({2})", dr["cName"].ToString(), dr["RealName"].ToString(), dr["UserName"].ToString());
                datalist.Add(userEntity);
            }
            dr.Close();
            return datalist;
        }

        public string[] GetUserRoles(string userId)
        {
            string[] userRoles = new string[3];
            string strSql = "select a.roleId,b.rolename,b.rolekey from usersandrole a left join role b on a.roleid = b.roleid where userId = {0}";
            strSql = string.Format(strSql, userId.Trim());
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                userRoles[0] += dt.Rows[i]["roleId"].ToString();
                userRoles[1] += dt.Rows[i]["rolename"].ToString();
                userRoles[2] += dt.Rows[i]["rolekey"].ToString();
                if (i != dt.Rows.Count - 1)
                {
                    userRoles[0] += ",";
                    userRoles[1] += ",";
                    userRoles[2] += ",";
                }                
            }            
            return userRoles;
        }
    }
}
