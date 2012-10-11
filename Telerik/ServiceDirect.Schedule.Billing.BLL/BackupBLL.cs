using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ServiceDirect.Schedule.DAL;
using System.Data;
using System.Data.Objects;
using System.Web;

namespace ServiceDirect.Schedule.Billing.BLL
{
    /**Creat time: 	2011/02/15
    *Author:		jzheng
    *Description:	备份配置任务控制类
    */
    public class BackupBLL
    {
        ServiceDirectDBEntities ServiceDirectDBEntitieAdd;//增加
        ServiceDirectDBEntities ServiceDirectDBEntitiesDelete;//删除
        ServiceDirectDBEntities ServiceDirectDB;//查询

        //构造方法
        public BackupBLL()
        { 
            
        }

        /// <summary>
        /// 根据条件以及排序规则，获取对象集
        /// </summary>
        /// <param name="SearchStr">查询条件</param>
        /// <param name="orderby">排序规则</param>
        /// <returns>符合条件的对象集</returns>
        public ObjectQuery<tblBackup> GetBackups(string SearchStr, string orderby)
        {
            string StrWhere = " it.ScheduleID is not null " + SearchStr;
            ObjectQuery<tblBackup> query = null;
            try
            {
                query = ServiceDirectDB.tblBackup.Where(StrWhere).OrderBy(orderby);
            }
            catch (EntitySqlException)
            {
                throw;
            }
            return query;
        }

        /// <summary>
        /// 插入的对象
        /// </summary>
        /// <param name="BackupObj">需要插入的对象</param>
        /// <returns>受影响的行数</returns>
        public virtual string Insert(tblBackup BackupObj)
        {
            int counts = 0;//影响行数标记
            string strErrorMessage = "InsertError";

            //插入数据
            try
            {
                ServiceDirectDBEntitieAdd = new ServiceDirectDBEntities();
                ServiceDirectDBEntitieAdd.tblBackup.AddObject(BackupObj);
                counts = ServiceDirectDBEntitieAdd.SaveChanges();
            }
            catch (EntitySqlException)
            {
                throw;
            }
            if (counts>0)
            {
                return BackupObj.BackupID.ToString();
            }
            return strErrorMessage;
        }

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="BackupObj">需要更新的对象</param>
        /// <returns>返回受影响的行数</returns>
        public virtual string Update(tblBackup BackupObj)
        {
            tblBackup SchedulerObjUpdate;//需要跟新的对象

            string strMessage = "InsertError";
            int counts = 0;//影响行数标记

            try
            {
                ServiceDirectDB = new ServiceDirectDBEntities();
                SchedulerObjUpdate = ServiceDirectDB.tblBackup.FirstOrDefault(t => t.BackupID == BackupObj.BackupID);
            }catch(EntitySqlException)
            {
                throw;
            }
            catch (EntityException)
            {
                throw;
            }

            if (SchedulerObjUpdate != null)//如果为空则进行插入
            {
                //对象进行赋值，准备更新
                SchedulerObjUpdate.BackupFloder = BackupObj.BackupFloder;
                SchedulerObjUpdate.BackupDatabase = BackupObj.BackupDatabase;
                SchedulerObjUpdate.DeleteBackupOlder = BackupObj.DeleteBackupOlder;
                SchedulerObjUpdate.BackupServer = BackupObj.BackupServer;

                SchedulerObjUpdate.BackupUser = BackupObj.BackupUser;
                SchedulerObjUpdate.BackupPassword = BackupObj.BackupPassword;

                try
                {
                    counts = ServiceDirectDB.SaveChanges();
                }
                catch (EntityException)
                {
                    throw;
                }
            }
            else
            {
                try
                {
                    ServiceDirectDBEntitieAdd = new ServiceDirectDBEntities();
                    ServiceDirectDBEntitieAdd.tblBackup.AddObject(BackupObj);
                    counts = ServiceDirectDBEntitieAdd.SaveChanges();
                }
                catch (EntityException)
                {
                    
                    throw;
                }
            }

            if (counts > 0)
            {
                strMessage = "update success";
                return strMessage;
            }
            return strMessage;
        }

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="KeyId">需要删除对象的ID</param>
        /// <returns>返回受影响的行数</returns>
        public virtual int LogicDelete(string KeyId)
        {
            int counts = 0;//影响行数标记
            System.Guid KeyIdGuid = new Guid(KeyId);//转换成Guid类型
            tblBackup BackupObjDelete;//需要跟新的对象

            try
            {
                ServiceDirectDBEntitiesDelete = new ServiceDirectDBEntities();
                BackupObjDelete = ServiceDirectDBEntitiesDelete.tblBackup.First(t => t.BackupID == KeyIdGuid);
                ServiceDirectDBEntitiesDelete.DeleteObject(BackupObjDelete);
                counts = ServiceDirectDBEntitiesDelete.SaveChanges();
            }
            catch (EntityException)
            {
                throw;
            }
            return counts;
        }

        /// <summary>
        /// 根据对象的ID查询数据
        /// </summary>
        /// <param name="KeyId">对象的ID</param>
        /// <returns>是否存在对象</returns>
        public virtual Boolean ExistFindBackupById(string KeyId)
        {
            tblBackup BackupObj;
            System.Guid KeyIdGuid = new Guid(KeyId);

            try
            {
                ServiceDirectDB = new ServiceDirectDBEntities();
                BackupObj = ServiceDirectDB.tblBackup.FirstOrDefault(t => t.BackupID == KeyIdGuid);
            }
            catch (EntityException)
            {
                throw;
            }
            if (BackupObj!=null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据对象的ID查询数据
        /// </summary>
        /// <param name="KeyId">对象的ID</param>
        /// <returns>返回对象</returns>
        public virtual tblBackup FindBackupById(string KeyId)
        {
            tblBackup BackupObj;
            System.Guid KeyIdGuid = new Guid(KeyId);

            try
            {
                ServiceDirectDB = new ServiceDirectDBEntities();
                BackupObj = ServiceDirectDB.tblBackup.FirstOrDefault(t => t.BackupID == KeyIdGuid);
            }
            catch (EntityException)
            {
                throw;
            }
            return BackupObj;
        }
    }
}
