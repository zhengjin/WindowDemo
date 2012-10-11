using System;
using System.Data;
using System.Linq;
using System.Web;

using ProjectDAL;

namespace ProjectCore
{
    public class RoleUserBLL
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public RoleUserBLL()
        { }

        #region 通过ID查询 返回数据
        /// <summary>
        /// 通过ID查询 返回数据
        /// </summary>
        /// <param name="strKeyID">主键值</param>
        /// <returns></returns>
        public tblRole_User GetByID(string strKeyID)
        {
            tblRole_User tblRole_UserObj;
            System.Guid KeyIdGuid = new Guid(strKeyID);

            try
            {
                using (MWDatabaseEntities MWDB = new MWDatabaseEntities())
                {
                    tblRole_UserObj = MWDB.tblRole_User.FirstOrDefault(t => t.UserID == KeyIdGuid);    
                }
            }
            catch (Exception ex)
            {
                string userHostAddress = HttpContext.Current.Request.UserHostAddress;
                DbLoggerBLL.SysLogger.Info("Find user and role relationship failed! User and role relationship ID: '"
                    + strKeyID + "'. User host address: '" + userHostAddress + "' . Exception info:" + ex.Message);
                return null;
            }
            return tblRole_UserObj;
        }
        #endregion

        #region 通过ID查询 返回数据
        /// <summary>
        /// 通过ID查询 返回数据
        /// </summary>
        /// <param name="strKeyID">主键值</param>
        /// <returns></returns>
        public tblRole_User GetByRoleID(string strKeyID)
        {
            tblRole_User tblRole_UserObj;
            System.Guid KeyIdGuid = new Guid(strKeyID);

            try
            {
                using (MWDatabaseEntities MWDB = new MWDatabaseEntities())
                {
                    tblRole_UserObj = MWDB.tblRole_User.FirstOrDefault(t => t.RoleID == KeyIdGuid);
                }
            }
            catch (Exception ex)
            {
                string userHostAddress = HttpContext.Current.Request.UserHostAddress;
                DbLoggerBLL.SysLogger.Error("Find user and role relationship failed! User and role relationship ID: '"
                    + strKeyID + "'. User host address: '" + userHostAddress + "' . Exception info:" + ex.Message);
                return null;
            }
            return tblRole_UserObj;
        }
        #endregion

        #region 新增数据

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="RoleObj"></param>
        /// <returns></returns>
        public bool Add(tblRole_User Role_UserObj)
        {
            Role_UserObj.ID = System.Guid.NewGuid();
            int counts = 0;
            //插入数据
            try
            {
                using (MWDatabaseEntities MWDB = new MWDatabaseEntities())
                {
                    MWDB.tblRole_User.AddObject(Role_UserObj);
                    counts = MWDB.SaveChanges();
                    if (counts > 0)
                    {
                        string userHostAddress = HttpContext.Current.Request.UserHostAddress;
                        DbLoggerBLL.SysLogger.Info("Increase user and role relationship successfully! User and role relationship ID: '"
                            + Role_UserObj.ID + "'. User host address: '" + userHostAddress + "' . ");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                string userHostAddress = HttpContext.Current.Request.UserHostAddress;
                DbLoggerBLL.SysLogger.Error("Increase user and role relationship failed! User and role relationship ID: '"
                    + Role_UserObj.ID + "'. User host address: '" + userHostAddress + "' . Exception info:" + ex.Message);
                return false;
            }

            return false;
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
                        DbLoggerBLL.SysLogger.Info("Delete user and role relationship successfully! User and role relationship ID: '"
                            + strKeyId + "'. User host address: '" + userHostAddress + "' . ");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                string userHostAddress = HttpContext.Current.Request.UserHostAddress;
                DbLoggerBLL.SysLogger.Error("Increase user and role relationship failed! User and role relationship ID: '"
                    + strKeyId + "'. User host address: '" + userHostAddress + "' . Exception info:" + ex.Message);
                return false;
            }
            return false;
        }
        #endregion

    }
}
