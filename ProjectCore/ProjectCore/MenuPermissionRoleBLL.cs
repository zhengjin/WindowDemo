using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using ProjectDAL;

namespace ProjectCore
{
    public class MenuPermissionRoleBLL
    {
        public MenuPermissionRoleBLL()
        { }

        #region 新增数据
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="RoleObj"></param>
        /// <returns></returns>
        public bool Add(tblMenu_Permission_Role tblMenu_Permission_RoleObj)
        {
            tblMenu_Permission_RoleObj.ID = System.Guid.NewGuid();
            string userHostAddress = HttpContext.Current.Request.UserHostAddress;
            int counts = 0;
            //插入数据
            try
            {
                using (MWDatabaseEntities MWDB = new MWDatabaseEntities())
                {
                    MWDB.tblMenu_Permission_Role.AddObject(tblMenu_Permission_RoleObj);
                    counts = MWDB.SaveChanges();
                    if (counts > 0)
                    {
                        DbLoggerBLL.SysLogger.Info("Increase menu and the role relationship successfully! ID: '"
                            + tblMenu_Permission_RoleObj.ID + "'. User host address: '"+ userHostAddress + "'");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                DbLoggerBLL.SysLogger.Error("Increase menu and the role relationship failed! ID: '"
                    + tblMenu_Permission_RoleObj.ID + "'. User host address: '" + userHostAddress + "' . Exception info"
                    + ex.Message);
                return false;
            }
            return false;
        }

        #endregion

        #region 删除一条数据
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public int DeleteT(string ID)
        {
            string userHostAddress = HttpContext.Current.Request.UserHostAddress;
            int count = 0;
            using (MWDatabaseEntities MWDB = new MWDatabaseEntities())
            {
                try
                {
                    System.Guid gu = new Guid(ID);
                    tblRole objRole = MWDB.tblRole.First(r => r.RoleID == gu);
                    if (objRole != null)
                    {
                        List<tblMenu_Permission_Role> list = MWDB.tblMenu_Permission_Role.Where<tblMenu_Permission_Role>(m => m.RoleID == gu).ToList();
                        if (list.Count > 0)
                        {
                            foreach (var lists in list)
                            {
                                tblMenu_Permission_Role tblMenu_Permission_RoleObj;
                                tblMenu_Permission_RoleObj = MWDB.tblMenu_Permission_Role.First(m => m.RoleID == lists.RoleID);
                                MWDB.DeleteObject(tblMenu_Permission_RoleObj);
                                count = MWDB.SaveChanges();
                                if (count>0)
                                {
                                    DbLoggerBLL.SysLogger.Info("Delete menu and the role relationship successfully! ID: '"
                                        + tblMenu_Permission_RoleObj.ID + "'. User host address: '" + userHostAddress + "'");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    DbLoggerBLL.SysLogger.Error("Delete menu and the role relationship failed! ID: '"
                            + ID + "'. User host address: '" + userHostAddress + "' . Exception info"
                            + ex.Message);
                }
            }
            return count;
        }
        #endregion
    }
}
