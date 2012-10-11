using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Security;

using ProjectCore.Model;
using ProjectDAL;

namespace ProjectCore
{
    public class RoleBLL
    {
        public RoleBLL()
        { }

        #region 返回Role数据集合
        /// <summary>
        /// 返回Role数据集合
        /// </summary>
        /// <param name="SearchStr">查询条件，可以为空 ( and it.name='aa')</param>
        /// <param name="strOrderby">排序方式，不可以为空，(it.name desc )</param>
        /// <returns></returns>
        public List<View_Role> GetAll(string strSearch, string strOrderby)
        {
            List<View_Role> query = null;
            using (MWDatabaseEntities MWDB = new MWDatabaseEntities())
            {
                string strWhere = " it.IsDeleted==false " + strSearch;
                
                try
                {
                    query = MWDB.View_Role.Where(strWhere).OrderBy(strOrderby).ToList();
                }
                catch (Exception ex)
                {
                    string userHostAddress = HttpContext.Current.Request.UserHostAddress;
                    DbLoggerBLL.SysLogger.Error("Find role view failed! Inquires the conditions: '"
                            + strWhere + "'. User host address: '" + userHostAddress + "' . Exception info:"
                            + ex.Message);
                    return null;
                }
            }
            
            return query;
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

            eModel.EntitySetName = "View_Role";
            eModel.DefaultContainerName = "MWDatabaseEntities";
            eModel.ConnectionString = "name=MWDatabaseEntities";
            eModel.Where = " it.IsDeleted=false";
            return eModel;
        }
        #endregion

        #region 新增数据

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="RoleObj"></param>
        /// <returns></returns>
        public bool Add(tblRole RoleObj)
        {
            //获取当前登录用户的ID(Cookies)
            string strUserID = HttpContext.Current.User.Identity.Name;
            if (string.IsNullOrEmpty(strUserID))
            {
                FormsAuthentication.RedirectToLoginPage();
                return false;
            }
            System.Guid guidUserID = new Guid(strUserID);
            RoleObj.RoleID = System.Guid.NewGuid();
            RoleObj.LastModifiedByID = guidUserID;
            RoleObj.CreatedByID = guidUserID;
            RoleObj.OwnerID = guidUserID;
            RoleObj.CreatedDate = Convert.ToDateTime(System.DateTime.Now);
            RoleObj.LastModifiedDate = Convert.ToDateTime(System.DateTime.Now);
            RoleObj.SystemModstamp = Convert.ToDateTime(System.DateTime.Now);
            RoleObj.IsDeleted = false;
            int counts = 0;
            //插入数据
            try
            {
                using (MWDatabaseEntities MWDB = new MWDatabaseEntities())
                {
                    MWDB.tblRole.AddObject(RoleObj);
                    counts = MWDB.SaveChanges();
                    if (counts > 0)
                    {
                        string userHostAddress = HttpContext.Current.Request.UserHostAddress;
                        DbLoggerBLL.SysLogger.Info("Increase role info successfully! Role ID: '"
                            + RoleObj.RoleID + "'. User host address: '" + userHostAddress + "'");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                string userHostAddress = HttpContext.Current.Request.UserHostAddress;
                DbLoggerBLL.SysLogger.Error("Increase role info failed! Role ID: '"
                    + RoleObj.RoleID + "'. User host address: '" + userHostAddress + "' . Exception info:"+ ex.Message);
                return false;
            }
            return false;
        }

        #endregion

        #region 更新数据
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="RoleObj"></param>
        /// <returns></returns>
        public bool Update(tblRole RoleNew)
        {
            tblRole RoleOld;
            int counts = 0;//影响行数标记
            using (MWDatabaseEntities MWDB = new MWDatabaseEntities())
            {
                try
                {
                    RoleOld = MWDB.tblRole.First(t => t.RoleID == RoleNew.RoleID);
                    //更新数据字段
                    RoleOld.RoleName = RoleNew.RoleName;
                    RoleOld.RoleCode = RoleNew.RoleCode;
                    RoleOld.State = RoleNew.State;
                    RoleOld.Desc = RoleNew.Desc;

                    //获取当前登录用户的ID(Cookies)
                    string strUserID = HttpContext.Current.User.Identity.Name;
                    if (string.IsNullOrEmpty(strUserID))
                    {
                        FormsAuthentication.RedirectToLoginPage();
                        return false;
                    }
                    System.Guid guidUserID = new Guid(strUserID);
                    RoleOld.LastModifiedByID = guidUserID;
                    RoleOld.LastModifiedDate = System.DateTime.Now;
                    RoleOld.SystemModstamp = System.DateTime.Now;

                    //日志处理
                    ObjectStateEntry ose = MWDB.ObjectStateManager.GetObjectStateEntry(RoleOld);
                    IEnumerable<string> list = ose.GetModifiedProperties();
                    string logBody = string.Empty;
                    foreach (string pr in list)
                    {
                        string strs = pr;//更新实体的属性名
                        string strNew = ose.CurrentValues[strs].ToString();//实体的新值
                        string strOld = ose.OriginalValues[strs].ToString();//实体的旧值
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
                        DbLoggerBLL.SysLogger.Info("Update role info failed! Role update log: '"
                            + logBody + "'. User host address: '" + userHostAddress + "' .");
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    string userHostAddress = HttpContext.Current.Request.UserHostAddress;
                    DbLoggerBLL.SysLogger.Error("Update role info failed! Role ID: '"
                        + RoleNew.RoleID + "'. User host address: '" + userHostAddress + "' . Exception info:" + ex.Message);
                    return false;
                }
            }
            return false;
        }
        #endregion

        #region 通过ID查询 返回数据
        /// <summary>
        /// 通过ID查询 返回数据
        /// </summary>
        /// <param name="strKeyID">主键值</param>
        /// <returns></returns>
        public tblRole GetByID(string strKeyID)
        {
            tblRole tblRoleObj;
            System.Guid KeyIdGuid = new Guid(strKeyID);

            try
            {
                using (MWDatabaseEntities MWDB = new MWDatabaseEntities())
                {
                    tblRoleObj = MWDB.tblRole.First(t => t.RoleID == KeyIdGuid);    
                }
            }
            catch (Exception ex)
            {
                string userHostAddress = HttpContext.Current.Request.UserHostAddress;
                DbLoggerBLL.SysLogger.Error("Find role info failed! Role ID: '"
                    + strKeyID + "'. User host address: '" + userHostAddress + "' . Exception info:" + ex.Message);
                return null;
            }
            return tblRoleObj;
        }
        #endregion

        #region 逻辑删除
        /// <summary>
        /// 逻辑删除
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
            tblRole tblRoles;

            try
            {
                using (MWDatabaseEntities MWDB = new MWDatabaseEntities())
                {
                    tblRoles = MWDB.tblRole.First(t => t.RoleID == KeyIdGuid);
                    tblRoles.IsDeleted = true;
                    counts = MWDB.SaveChanges();
                    if (counts > 0)
                    {
                        string userHostAddress = HttpContext.Current.Request.UserHostAddress;
                        DbLoggerBLL.SysLogger.Info("Delete role successfully! Role ID: '"
                            + strKeyId + "'. User host address: '" + userHostAddress + "' . ");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                string userHostAddress = HttpContext.Current.Request.UserHostAddress;
                DbLoggerBLL.SysLogger.Error("Delete role failed! Role ID: '"
                    + strKeyId + "'. User host address: '" + userHostAddress + "' . Exception info:" + ex.Message);
                return false;
            }
            return false;
        }
        #endregion

    }
}
