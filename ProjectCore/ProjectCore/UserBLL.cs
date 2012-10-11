using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.IO;
using System.Linq;
using System.Web;

using System.Web.Security;
using ProjectCore.Model;
using ProjectDAL;

namespace ProjectCore
{
    public class UserBLL
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UserBLL()
        { }

        #region 登陆相关,登录账号密码判断，角色判断
        /// <summary>
        /// 判断用户是否合法
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="Password">密码</param>
        /// <returns>用户的GUID</returns>
        public virtual string Verify(string UserName, string Password)
        {
            tblUser objUTPUser;

            using (MWDatabaseEntities MWDB = new MWDatabaseEntities())
            {
                string userHostAddress = HttpContext.Current.Request.UserHostAddress;
                try
                {
                    objUTPUser = MWDB.tblUser.FirstOrDefault<tblUser>(u => u.LoginName == UserName && u.State == true && u.IsDeleted == false);

                    if (objUTPUser!=null&&objUTPUser.LoginPwd.CompareTo(Password) == 0)
                    {
                        string roleId=GetRoleIdByUserId(objUTPUser.UserID);
                        if (!string.IsNullOrEmpty(roleId))
                        {
                            DbLoggerBLL.FileLoger.Info("'" + objUTPUser.UserName + "' login successfully! User host address: '" + userHostAddress + "'");
                            //核对密码正确，且角色可用
                            return objUTPUser.UserID.ToString();
                        }else
                        {
                            DbLoggerBLL.FileLoger.Warn("'" + UserName + "' login failed! Role has been forbidden! User host address: '"
                            + userHostAddress + "'");
                            return null;
                        }
                    }
                    else
                    {
                        DbLoggerBLL.FileLoger.Warn("'" + UserName + "' login failed! Password does not match! User host address: '" 
                            + userHostAddress + "'");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    DbLoggerBLL.FileLoger.Error("'" + UserName + "' login failed! Maybe connecte data server failed! User host address: '" 
                        + userHostAddress + "' Exception info : " + ex.Message);
                    //此处编写错误处理日志
                    return null;
                }
            }
        }

        /// <summary>
        /// 返回角色ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual string GetRoleIdByUserId(Guid userId)
        {
            View_RoleAndUser objRoleAndUser;

            using (MWDatabaseEntities MWDB = new MWDatabaseEntities())
            {
                try
                {
                    objRoleAndUser = MWDB.View_RoleAndUser.First(it => it.UserID == userId && it.State == 1 && it.IsDeleted == false);//1为可用

                    if (objRoleAndUser != null)
                    {
                        return objRoleAndUser.RoleID.ToString();
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    string userHostAddress = HttpContext.Current.Request.UserHostAddress;
                    DbLoggerBLL.SysLogger.Error("Find user and the role relationship failed! user id: '"
                            + userId + "'. User host address: '" + userHostAddress + "' . Exception info"
                            + ex.Message);
                    return null;
                }
            }
        }
        #endregion

        #region 获取客户基本信息
        /// <summary>
        /// 判断登陆名是否重复
        /// </summary>
        /// <param name="userId">登陆名称</param>
        /// <returns></returns>
        public bool ExistsLogName(string strLoginName)
        {
            tblUser userObj;

            using (MWDatabaseEntities MWDB = new MWDatabaseEntities())
            {
                try
                {
                    userObj = MWDB.tblUser.First(it => it.LoginName == strLoginName);
                    if (userObj!=null)
                    {
                        return true;
                    }
                    
                }
                catch (Exception ex)
                {
                    string userHostAddress = HttpContext.Current.Request.UserHostAddress;
                    DbLoggerBLL.SysLogger.Error("Find user failed! User login name: '"
                            + strLoginName + "'. User host address: '" + userHostAddress + "' . Exception info"
                            + ex.Message);
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// 查询用户名
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public virtual string GetUserNameById(Guid userId)
        {
            tblUser userObj;

            using (MWDatabaseEntities MWDB = new MWDatabaseEntities())
            {
                try
                {
                    userObj = MWDB.tblUser.First(it => it.UserID == userId&&it.IsDeleted==false);
                }
                catch (Exception ex)
                {
                    string userHostAddress = HttpContext.Current.Request.UserHostAddress;
                    DbLoggerBLL.SysLogger.Error("Find user failed! User id: '"
                        + userId + "'. User host address: '" + userHostAddress +"'. Exception info:" + ex.Message);
                    return null;
                }
            }
            return userObj.UserName;
        }

        /// <summary>
        /// 通过ID查询 返回数据
        /// </summary>
        /// <param name="strKeyID">主键值</param>
        /// <returns></returns>
        public tblUser GetByID(string strKeyID)
        {
            tblUser tblUserObj;
            System.Guid KeyIdGuid = new Guid(strKeyID);

            try
            {
                using (MWDatabaseEntities MWDB = new MWDatabaseEntities())
                {
                    tblUserObj = MWDB.tblUser.First(t => t.UserID == KeyIdGuid);    
                }
            }
            catch (Exception ex)
            {
                string userHostAddress = HttpContext.Current.Request.UserHostAddress;
                DbLoggerBLL.SysLogger.Error("Find user failed! User id: '"
                    + strKeyID + "'. User host address: '" + userHostAddress + "'. Exception info:" + ex.Message);
                return null;
            }
            return tblUserObj;
        }
        #endregion

        #region cookies相关信息
        /// <summary>
        /// 向客户端写入登录用户名
        /// </summary>
        /// <param name="userName">用户姓名</param>
        public virtual void ResponseCookies(string userName)
        {
            HttpCookie myCookie = new HttpCookie("LoginInfo");

            if (!string.IsNullOrEmpty(userName))
            {
                myCookie.Values.Add("userName", userName);
                myCookie.Expires = System.DateTime.Now.AddDays(1);
                HttpContext.Current.Response.AppendCookie(myCookie);
            }
        }

        /// <summary>
        /// 返回登陆用户名
        /// </summary>
        /// <returns></returns>
        public virtual string GetLoginUserName()
        {
            string userName;
            if (HttpContext.Current.Request.Cookies["LoginInfo"] != null)
            {
                userName = HttpContext.Current.Request.Cookies["LoginInfo"].Values["userName"];
            }
            else
            {
                userName = string.Empty;
            }
            return userName;
        }
        #endregion

        #region 操作用户数据
        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="KeyId">需要删除对象的ID</param>
        /// <returns>返回受影响的行数</returns>
        public bool Delete(string strKeyId)
        {
            //影响行数标记
            int counts = 0;

            //转换成Guid类型
            System.Guid KeyIdGuid = new Guid(strKeyId);

            //需要跟新的对象
            tblRole_User tblRole_Users;

            try
            {
                using (MWDatabaseEntities MWDB = new MWDatabaseEntities())
                {
                    tblRole_Users = MWDB.tblRole_User.FirstOrDefault(t => t.UserID == KeyIdGuid);
                    if (tblRole_Users != null)
                    {
                        MWDB.DeleteObject(tblRole_Users);
                        counts = MWDB.SaveChanges();
                    }
                    if (counts > 0)
                    {
                        string userHostAddress = HttpContext.Current.Request.UserHostAddress;
                        DbLoggerBLL.SysLogger.Info("Delete user successfully! User id: '"
                            + strKeyId + "'. User host address: '" + userHostAddress + "'. ");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                string userHostAddress = HttpContext.Current.Request.UserHostAddress;
                DbLoggerBLL.SysLogger.Error("Delete user failed! User id: '"
                    + strKeyId + "'. User host address: '" + userHostAddress + "'. Exception info:" + ex.Message);
                return false;
            }
            return false;
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="RoleObj"></param>
        /// <returns></returns>
        public bool Add(tblUser RoleObj)
        {
            //RoleObj.UserID = System.Guid.NewGuid();
            //获取当前登录用户的ID(Cookies)
            string strUserID = HttpContext.Current.User.Identity.Name;
            if (strUserID == string.Empty || strUserID == null)
            {
                return false;
            }
            System.Guid guidUserID = new Guid(strUserID);

            RoleObj.SendEmail = true;
            RoleObj.LastModifiedByID = guidUserID;
            RoleObj.CreatedByID = guidUserID;
            RoleObj.OwnerID = guidUserID;
            RoleObj.CreatedDate = Convert.ToDateTime(System.DateTime.Now);
            RoleObj.LastModifiedDate = Convert.ToDateTime(System.DateTime.Now);
            RoleObj.SystemModstamp = Convert.ToDateTime(System.DateTime.Now);
            RoleObj.IsDeleted = false;
            RoleObj.Avatar = this.GetImageIo();
            int counts = 0;
            //插入数据
            try
            {
                MWDatabaseEntities MWDB = new MWDatabaseEntities();
                MWDB.tblUser.AddObject(RoleObj);
                counts = MWDB.SaveChanges();
                if (counts > 0)
                {
                    string userHostAddress = HttpContext.Current.Request.UserHostAddress;
                    DbLoggerBLL.SysLogger.Info("Increase user successfully! User id: '"
                        + RoleObj.UserID + "'. User host address: '" + userHostAddress + "'. ");
                    return true;
                }
            }
            catch (Exception ex)
            {
                string userHostAddress = HttpContext.Current.Request.UserHostAddress;
                DbLoggerBLL.SysLogger.Error("Increase user failed! User id: '"
                    + RoleObj.UserID + "'. User host address: '" + userHostAddress + "'. Exception info:" + ex.Message);
                return false;
            }

            return false;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="RoleObj"></param>
        /// <returns></returns>
        public bool Update(tblUser UserNew)
        {
            tblUser UserOld;
            int counts = 0;//影响行数标记
            using (MWDatabaseEntities MWDB =new MWDatabaseEntities())
            {
                try
                {
                    UserOld = MWDB.tblUser.FirstOrDefault(t => t.UserID == UserNew.UserID);

                    //更新数据字段
                    UserOld.UserName = UserNew.UserName;
                    UserOld.LoginName = UserNew.LoginName;
                    UserOld.LoginPwd = UserNew.LoginPwd;
                    UserOld.UserCode = UserNew.UserCode;
                    UserOld.State = UserNew.State;
                    UserOld.Email = UserNew.Email;
                    UserOld.Desc = UserNew.Desc;
                    UserOld.Avatar = UserNew.Avatar;
                    //获取当前登录用户的ID(Cookies)
                    string strUserID = HttpContext.Current.User.Identity.Name;
                    if (string.IsNullOrEmpty(strUserID))
                    {
                        FormsAuthentication.RedirectToLoginPage();
                        return false;
                    }

                    UserOld.LastModifiedByID = new Guid(strUserID);
                    DateTime dates = System.DateTime.Now;
                    UserOld.LastModifiedDate = dates;
                    UserOld.SystemModstamp = dates;


                    ObjectStateEntry ose = MWDB.ObjectStateManager.GetObjectStateEntry(UserOld);
                    IEnumerable<string> list = ose.GetModifiedProperties();
                    string logBody = string.Empty;
                    foreach (string pr in list)
                    {
                        string strs = pr;//更新实体的属性名
                        string strNew = ose.CurrentValues[strs].ToString();
                        string strOld = ose.OriginalValues[strs].ToString();
                        if (strNew != strOld)
                        {
                            strNew = string.IsNullOrEmpty(strNew) ? "null" : strNew;//如果字符串为空，则将null赋给字符串
                            strOld = string.IsNullOrEmpty(strOld) ? "null" : strOld;
                            switch (strs)
                            {
                                case "LastModifiedByID":
                                    //系统字段不进行发送
                                    break;
                                case "LastModifiedDate":
                                    //系统字段不进行发送
                                    break;
                                case "SystemModstamp":
                                    //系统字段不进行发送
                                    break;
                                default:
                                    logBody += ", " + strs + ": '" + strOld + "'" + " had been changed '" + strNew + "'";
                                    break;
                            }               
                        }
                    }

                    counts = MWDB.SaveChanges();
                    if (counts > 0)
                    {
                        string userHostAddress = HttpContext.Current.Request.UserHostAddress;
                        DbLoggerBLL.SysLogger.Info("Update user info failed! User update log: '"
                            + logBody + "'. User host address: '" + userHostAddress + "' .");
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    string userHostAddress = HttpContext.Current.Request.UserHostAddress;
                    DbLoggerBLL.SysLogger.Error("Update user info failed! User id: '"
                            + UserNew.UserID + "'. User host address: '" + userHostAddress + "' . Exception info:" + ex.Message);
                    return false;
                }
            }
            return false;
        }
        #endregion

        #region 返回EntityModel类
        /// <summary>
        /// 返回EntityModel类
        /// </summary>
        /// <returns></returns>
        public EntityDataSourceModel GetEModel()
        {
            EntityDataSourceModel eModel = new EntityDataSourceModel();

            eModel.EntitySetName = "View_User";
            eModel.DefaultContainerName = "MWDatabaseEntities";
            eModel.ConnectionString = "name=MWDatabaseEntities";
            eModel.Where = " it.IsDeleted=false";
            return eModel;
        }
        #endregion

        #region 图片转流

        /// <summary>
        /// 图片转流
        /// </summary>
        /// <returns></returns>
        public Byte[] GetImageIo()
        {
            try
            {
                string strImagePath = HttpContext.Current.Request.PhysicalApplicationPath + "Image/DefaultAvatar.png";
                //创建文件流
                FileStream fs = new FileStream(strImagePath, FileMode.Open, FileAccess.Read);
                //创建byte数字
                Byte[] bytes = new Byte[fs.Length];
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                if (bytes != null)
                {
                    return bytes;
                }
            }
            catch (Exception ex)
            {
                string userHostAddress = HttpContext.Current.Request.UserHostAddress;
                DbLoggerBLL.SysLogger.Error("Head image create failed. User host address: '" 
                    + userHostAddress + "'. Exception info:" + ex.Message);
            }
            return null;
        }
        #endregion
    }
}
