using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ServiceDirect.Membership.DAL;
using System.Data.Objects;

using System.Web;

namespace ServiceDirect.Membership
{
    /// <summary>
    /// 创建日期：2011-02-14。
    /// 创建人：闫保振。
    /// 描述：处理UTP用户信息，核对UTP用户权限。
    /// </summary>
    public class UTPUser
    {
        public UTPUser()
        { }
        //构造函数
        //参数：UTP UserID
        //返回值：vUTPUserWithRol对象

        public vUTPUserWithRole GetUTPUserBLL(string UTPUserName)
        {
            vUTPUserWithRole objUTPUser;

            ServiceDirectMembershipDBEntities objDB = new ServiceDirectMembershipDBEntities();

            try
            {
                objUTPUser = objDB.vUTPUserWithRole.First<vUTPUserWithRole>(u => u.UserName == UTPUserName);
            }
            catch (Exception)
            {
                objUTPUser = null;
            }

            return objUTPUser;
        }

        //UTP用户验证函数
        //参数：UTP UserID，UTP Password
        //返回值：布尔类型 真：假

        public bool UTPUserAuthenticate(string struid, string strcid, string RightCode)
        {
           
            vUTPUserWithRole objUTPUser=null;
            ServiceDirectMembershipDBEntities objDB = new ServiceDirectMembershipDBEntities();            
            try
            {
                objUTPUser = objDB.vUTPUserWithRole.First(u => u.UserID == struid);

                //用户密码
                string strUTPPassword =string.Empty;

                //用户ID
                string strUserID = string.Empty;
                if (objUTPUser.Password != null && objUTPUser.Password != string.Empty 
                    &&objUTPUser.UserID!=null && objUTPUser.UserID!=string.Empty)
                {
                    strUTPPassword = objUTPUser.Password;
                    strUserID = objUTPUser.UserID;
                    //通过MD5加密
                    strUTPPassword = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(strUserID+strUTPPassword, "MD5");
                }
                //统一转换成大写在做比较
                if (strUTPPassword.ToUpper().CompareTo(strcid.ToUpper()) == 0)
                {

                    //核对UTP用户是否设置了SD Backflow功能权限
                    bool bol= UTPUserRightCheck(objUTPUser, RightCode);
                    if (bol)
                    {
                        //写入UserIdCookies
                        HttpContext.Current.Response.Cookies["UserIdCookies"].Value = objUTPUser.UserID;

                        //为Cookies设置过期时间 为1天
                        HttpContext.Current.Response.Cookies["UserIdCookies"].Expires = System.DateTime.Now.AddDays(1);

                        //获取Cookies
                        //string str = HttpContext.Current.Request.Cookies["UserIdCookies"].Value;

                        //写入UserPasswordCookies
                        HttpContext.Current.Response.Cookies["UserPasswordCookies"].Value = objUTPUser.Password;

                        //为Cookies设置过期时间 为1天
                        HttpContext.Current.Response.Cookies["UserPasswordCookies"].Expires = System.DateTime.Now.AddDays(1);


                    }
                    return bol;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        //UTP用户权限验证
        //参数：UTP用户实体
        //返回值：布尔类型 真：假

        private bool UTPUserRightCheck(vUTPUserWithRole DataItem, string RightCode)
        {
            ServiceDirectMembershipDBEntities objDB = new ServiceDirectMembershipDBEntities();

            try
            {
                ObjectQuery<vUTPRightWithRole> query = objDB.vUTPRightWithRole.Where("it.RoleID='" + DataItem.RoleID + "'");

                foreach (vUTPRightWithRole RightItem in query)
                {
                    if (RightItem.RightCode.CompareTo(RightCode) == 0)
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception)
            {

                //此处编写错误处理日志
                return false;
            }
        }
    }
}
