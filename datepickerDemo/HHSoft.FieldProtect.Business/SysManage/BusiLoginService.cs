using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.SysManage;
using HHSoft.FieldProtect.DataAccess;
using System.Data;
using System.Web;
using System.Web.Security;
using HHSoft.FieldProtect.Framework.Utility;
using HHSoft.FieldProtect.DataEntity;

namespace HHSoft.FieldProtect.Business.SysManage
{
    public class BusiLoginService
    {
        private static BusiLoginService instance = new BusiLoginService();
        private BusiLoginService()
        {
        }

        public static BusiLoginService Instance
        {
            get
            {
                return instance;
            }
        }

        public void test()
        {
            string strSql = "select * from users";
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);

            string[] sSql = new string[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sSql[i] = "update users set Password = '{0}' where userName = '{1}'";
                sSql[i] = string.Format(sSql[i], EncryptHelper.EncryptString(dt.Rows[i]["userName"].ToString()), dt.Rows[i]["userName"].ToString());
            }
            OracleHelper.ExecuteCommand(sSql);
        }
        /// <summary>
        /// 登录系统
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public Users GetLoginUser(string loginName, string pwd,string cCode)
        {
            Users user = new Users();            
            string strSql = "select * from users a left join company b on a.cCode = b.cCode "
                    + " left join department c on  c.DeptCode = a.DeptCode "
                    + " where a.UserName = '{0}' and a.Password = '{1}' and substr(a.ccode,0,4) = '{2}'";
            strSql = string.Format(strSql, loginName, EncryptHelper.EncryptString(pwd), cCode.Substring(0, 4));
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count == 1)
            {
                user.UserId = dt.Rows[0]["UserId"].ToString();
                ////单位
                user.Company.CompanyCode = dt.Rows[0]["cCode"].ToString();
                user.Company.Name = dt.Rows[0]["CName"].ToString();
                user.Company.CompanyType = dt.Rows[0]["cType"].ToString();
                ////部门
                user.DepartMent.DeptCode = dt.Rows[0]["DeptCode"].ToString();
                user.DepartMent.DeptName = dt.Rows[0]["DeptName"].ToString();
                BusiUserService userService = new BusiUserService();
                string[] roleAry = userService.GetUserRoles(dt.Rows[0]["UserId"].ToString());
                ////角色
                user.Role.RoleId = roleAry[0];
                user.Role.RoleName = roleAry[1];
                user.Role.RoleKey = roleAry[2];
                ////用户基本信息
                user.UserName = dt.Rows[0]["UserName"].ToString();
                user.RealName = dt.Rows[0]["RealName"].ToString();
                user.TelePhone = dt.Rows[0]["TelePhone"].ToString();
                user.Sex = dt.Rows[0]["Sex"].ToString().Equals("0");
                user.State = dt.Rows[0]["State"].ToString().Equals("0");
            }
            else
            {
                user = null;
            }
            return user;
        }

        /// <summary>
        /// 登录时写入用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="context"></param>
        public void FormsAuthenticationTicks(Users user, HttpContext context)
        {
            string userData = user.UserId + "^" +
                user.UserName + "^" +
                user.RealName + "^" +
                user.Role.RoleId + "^" +
                user.Role.RoleKey + "^" +
                user.Role.RoleName + "^" +
                user.Company.CompanyCode + "^" +
                user.Company.Name + "^" +
                user.DepartMent.DeptCode + "^" +
                user.DepartMent.DeptName;
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1, // version
                user.UserId, // 用户名
                DateTime.Now, // 创建时间
                DateTime.Now.AddMonths(3),// 过期时间
                false,
                userData
                );
            //cookies加密
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            HttpCookie Cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            context.Response.Cookies.Add(Cookie);
        }


        /// <summary>
        /// 从Cookies中返回登陆对象
        /// </summary>
        /// <returns>登陆管理员</returns>
        public LoginUser GetLogUserFromCookies()
        {
            if (HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                string encryptTicket = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value;
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(encryptTicket);
                string[] userData = ticket.UserData.Split(Convert.ToChar("^"));
                LoginUser loginUser = new LoginUser();
                loginUser.UserId = userData[0];
                loginUser.UserName = userData[1];
                loginUser.RealName = userData[2];
                loginUser.RoleId = userData[3];
                loginUser.RoleKey = userData[4];
                loginUser.RoleName = userData[5];
                loginUser.CompanyCode = userData[6];
                loginUser.CompanyName = userData[7];
                loginUser.DepartCode = userData[8];
                loginUser.DepartName = userData[9];             

                return loginUser;
            }
            else
            {
                return null;
            }
        }
    }
}
