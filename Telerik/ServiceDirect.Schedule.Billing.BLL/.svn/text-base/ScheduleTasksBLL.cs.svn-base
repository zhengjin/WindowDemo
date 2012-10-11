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
    /**Creat time: 	2011/02/14
    *Author:		jzheng
    *Description:	Schedule任务控制类
    */
    public class ScheduleTasksBLL
    {
        ServiceDirectDBEntities ServiceDirectDBEntitieAdd;//增加
        ServiceDirectDBEntities ServiceDirectDBEntitiesDelete;//删除
        ServiceDirectDBEntities ServiceDirectDB;//查询

        //构造方法
        public ScheduleTasksBLL()
        { 
            
        }

        /// <summary>
        /// 根据条件以及排序规则，获取对象集
        /// </summary>
        /// <param name="SearchStr">查询条件</param>
        /// <param name="orderby">排序规则</param>
        /// <returns>符合条件的对象集</returns>
        public ObjectQuery<tblScheduler> GetSchedulers(string SearchStr, string orderby)
        {
            string StrWhere = " it.ScheduleID is not null " + SearchStr;
            ObjectQuery<tblScheduler> query = null;
            try
            {
                ServiceDirectDB = new ServiceDirectDBEntities();
                query = ServiceDirectDB.tblScheduler.Where(StrWhere).OrderBy(orderby);
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
        /// <param name="SchedulerObj">需要插入的对象</param>
        /// <returns>受影响的行数</returns>
        public virtual string Insert(tblScheduler SchedulerObj)
        {
            int counts;//影响行数标记
            string strScheduleID;//插入对象的ID
            string strErrorMessage="InsertError";

            SchedulerObj.ScheduleID = System.Guid.NewGuid();

            //插入数据
            try
            {
                ServiceDirectDBEntitieAdd = new ServiceDirectDBEntities();
                ServiceDirectDBEntitieAdd.tblScheduler.AddObject(SchedulerObj);
                counts = ServiceDirectDBEntitieAdd.SaveChanges();
            }
            catch (EntitySqlException)
            {
                throw;
            }
            if(counts>0)
            {
                strScheduleID = SchedulerObj.ScheduleID.ToString();
                return strScheduleID;
            }
            return strErrorMessage;
        }

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="SchedulerObj">需要更新的对象</param>
        /// <returns>返回受影响的行数</returns>
        public virtual Boolean UpdateInTaskView(tblScheduler SchedulerObj)
        {
            tblScheduler SchedulerObjUpdate;//需要跟新的对象

            Boolean flagSuccess = false;
            int counts = 0;//影响行数标记

            try
            {
                ServiceDirectDB = new ServiceDirectDBEntities();
                SchedulerObjUpdate = ServiceDirectDB.tblScheduler.First(t => t.ScheduleID == SchedulerObj.ScheduleID);
            }
            catch (EntitySqlException)
            {
                throw;
            }
            catch (EntityException)
            {
                throw;
            }

            //对象进行赋值，准备更新

            //SchedulerObjUpdate.Action = SchedulerObj.Action;
            //SchedulerObjUpdate.TraceType = SchedulerObj.TraceType;
            //SchedulerObjUpdate.PostAction = SchedulerObj.PostAction;
            //SchedulerObjUpdate.ScheduleType = SchedulerObj.ScheduleType;
            //SchedulerObjUpdate.StartTime = SchedulerObj.StartTime;
            //SchedulerObjUpdate.TaskName = SchedulerObj.TaskName;
            //SchedulerObjUpdate.RunOnly = SchedulerObj.RunOnly;
            //SchedulerObjUpdate.RunOnlyStart = SchedulerObj.RunOnlyStart;
            //SchedulerObjUpdate.RunOnlyEnd = SchedulerObj.RunOnlyEnd;

            SchedulerObjUpdate.Trace = SchedulerObj.Trace;
            SchedulerObjUpdate.TraceTable = SchedulerObj.TraceTable;
            SchedulerObjUpdate.JobID = SchedulerObj.JobID;

            //SchedulerObjUpdate.Status = SchedulerObj.Status;
            //SchedulerObjUpdate.UTPPwd = SchedulerObj.UTPPwd;
            //SchedulerObjUpdate.UTPUser = SchedulerObj.UTPUser;
            //SchedulerObjUpdate.Cycle = SchedulerObj.Cycle;
            //SchedulerObjUpdate.Calc = SchedulerObj.Calc;
            //SchedulerObjUpdate.Company = SchedulerObj.Company;
            //SchedulerObjUpdate.Copy = SchedulerObj.Copy;
            try
            {
                counts = ServiceDirectDB.SaveChanges();
            }
            catch (EntityException)
            {
                throw;
            }
            if (counts > 0)
            {
                flagSuccess = true;
                return flagSuccess;
            }

            return flagSuccess;
        }

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="SchedulerObj">需要更新的对象</param>
        /// <returns>返回受影响的行数</returns>
        public virtual string UpdateInBillingPage(tblScheduler SchedulerObj)
        {
            tblScheduler SchedulerObjUpdate;//需要跟新的对象

            string strMessage = "UpdateError";
            int counts = 0;//影响行数标记

            try
            {
                ServiceDirectDB = new ServiceDirectDBEntities();
                SchedulerObjUpdate = ServiceDirectDB.tblScheduler.First(t => t.ScheduleID == SchedulerObj.ScheduleID);
            }catch(EntitySqlException)
            {
                throw;
            }
            catch (EntityException)
            {
                throw;
            }

            //对象进行赋值，准备更新

            //SchedulerObjUpdate.Action = SchedulerObj.Action;
            //SchedulerObjUpdate.TraceType = SchedulerObj.TraceType;
            //SchedulerObjUpdate.PostAction = SchedulerObj.PostAction;
            //SchedulerObjUpdate.ScheduleType = SchedulerObj.ScheduleType;
            //SchedulerObjUpdate.StartTime = SchedulerObj.StartTime;
            //SchedulerObjUpdate.TaskName = SchedulerObj.TaskName;
            //SchedulerObjUpdate.RunOnly = SchedulerObj.RunOnly;
            //SchedulerObjUpdate.RunOnlyStart = SchedulerObj.RunOnlyStart;
            //SchedulerObjUpdate.RunOnlyEnd = SchedulerObj.RunOnlyEnd;

            SchedulerObjUpdate.Trace = SchedulerObj.Trace;
            SchedulerObjUpdate.TraceTable = SchedulerObj.TraceTable;

            SchedulerObjUpdate.Status = SchedulerObj.Status;
            SchedulerObjUpdate.UTPPwd = SchedulerObj.UTPPwd;
            SchedulerObjUpdate.UTPUser = SchedulerObj.UTPUser;
            SchedulerObjUpdate.Cycle = SchedulerObj.Cycle;
            SchedulerObjUpdate.Calc = SchedulerObj.Calc;
            SchedulerObjUpdate.Company = SchedulerObj.Company;
            SchedulerObjUpdate.Copy = SchedulerObj.Copy;
            try
            {
                counts = ServiceDirectDB.SaveChanges();
            }
            catch (EntityException)
            {
                throw;
            }
            if (counts > 0)
            {
                strMessage = "Update success";
                return strMessage;
            }

            return strMessage;
        }

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="SchedulerObj">需要更新的对象</param>
        /// <returns>返回受影响的行数</returns>
        public virtual string UpdateInTaskDetailForm(tblScheduler SchedulerObj)
        {
            tblScheduler SchedulerObjUpdate;//需要跟新的对象

            string strMessage = "UpdateError";
            int counts = 0;//影响行数标记

            try
            {
                ServiceDirectDB = new ServiceDirectDBEntities();
                SchedulerObjUpdate = ServiceDirectDB.tblScheduler.First(t => t.ScheduleID == SchedulerObj.ScheduleID);
            }
            catch (EntitySqlException)
            {
                throw;
            }
            catch (EntityException)
            {
                throw;
            }

            //对象进行赋值，准备更新

            SchedulerObjUpdate.Action = SchedulerObj.Action;
            SchedulerObjUpdate.TraceType = SchedulerObj.TraceType;
            SchedulerObjUpdate.PostAction = SchedulerObj.PostAction;
            SchedulerObjUpdate.ScheduleType = SchedulerObj.ScheduleType;
            SchedulerObjUpdate.StartTime = SchedulerObj.StartTime;
            SchedulerObjUpdate.TaskName = SchedulerObj.TaskName;
            SchedulerObjUpdate.RunOnly = SchedulerObj.RunOnly;
            SchedulerObjUpdate.RunOnlyStart = SchedulerObj.RunOnlyStart;
            SchedulerObjUpdate.RunOnlyEnd = SchedulerObj.RunOnlyEnd;
            SchedulerObjUpdate.BackupID = SchedulerObj.BackupID;

            SchedulerObjUpdate.Trace = SchedulerObj.Trace;
            SchedulerObjUpdate.TraceTable = SchedulerObj.TraceTable;

            //SchedulerObjUpdate.Status = SchedulerObj.Status;
            //SchedulerObjUpdate.UTPPwd = SchedulerObj.UTPPwd;
            //SchedulerObjUpdate.UTPUser = SchedulerObj.UTPUser;
            //SchedulerObjUpdate.Cycle = SchedulerObj.Cycle;
            //SchedulerObjUpdate.Calc = SchedulerObj.Calc;
            //SchedulerObjUpdate.Company = SchedulerObj.Company;
            //SchedulerObjUpdate.Copy = SchedulerObj.Copy;
            try
            {
                counts = ServiceDirectDB.SaveChanges();
            }
            catch (EntityException)
            {
                throw;
            }
            if (counts > 0)
            {
                strMessage = "Update success";
                return strMessage;
            }

            return strMessage;
        }

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="SchedulerObj">需要更新的对象</param>
        /// <returns>返回受影响的行数</returns>
        public virtual string UpdateInEmailForm(tblScheduler SchedulerObj)
        {
            tblScheduler SchedulerObjUpdate;//需要跟新的对象

            string strMessage = "UpdateError";
            int counts = 0;//影响行数标记

            try
            {
                ServiceDirectDB = new ServiceDirectDBEntities();
                SchedulerObjUpdate = ServiceDirectDB.tblScheduler.First(t => t.ScheduleID == SchedulerObj.ScheduleID);
            }
            catch (EntitySqlException)
            {
                throw;
            }
            catch (EntityException)
            {
                throw;
            }

            //对象进行赋值，准备更新

            //SchedulerObjUpdate.Action = SchedulerObj.Action;
            //SchedulerObjUpdate.TraceType = SchedulerObj.TraceType;
            //SchedulerObjUpdate.PostAction = SchedulerObj.PostAction;
            //SchedulerObjUpdate.ScheduleType = SchedulerObj.ScheduleType;
            //SchedulerObjUpdate.StartTime = SchedulerObj.StartTime;
            //SchedulerObjUpdate.TaskName = SchedulerObj.TaskName;
            //SchedulerObjUpdate.RunOnly = SchedulerObj.RunOnly;
            //SchedulerObjUpdate.RunOnlyStart = SchedulerObj.RunOnlyStart;
            //SchedulerObjUpdate.RunOnlyEnd = SchedulerObj.RunOnlyEnd;
            //SchedulerObjUpdate.BackupID = SchedulerObj.BackupID;

            SchedulerObjUpdate.Trace = SchedulerObj.Trace;
            SchedulerObjUpdate.TraceTable = SchedulerObj.TraceTable;

            //SchedulerObjUpdate.Status = SchedulerObj.Status;
            //SchedulerObjUpdate.UTPPwd = SchedulerObj.UTPPwd;
            //SchedulerObjUpdate.UTPUser = SchedulerObj.UTPUser;
            //SchedulerObjUpdate.Cycle = SchedulerObj.Cycle;
            //SchedulerObjUpdate.Calc = SchedulerObj.Calc;
            //SchedulerObjUpdate.Company = SchedulerObj.Company;
            //SchedulerObjUpdate.Copy = SchedulerObj.Copy;

            SchedulerObjUpdate.EmailTo = SchedulerObj.EmailTo;
            try
            {
                counts = ServiceDirectDB.SaveChanges();
            }
            catch (EntityException)
            {
                throw;
            }
            if (counts > 0)
            {
                strMessage = "Update success";
                return strMessage;
            }

            return strMessage;
        }

        /// <summary>
        /// 根据对象的ID查询数据
        /// </summary>
        /// <param name="KeyId">对象的ID</param>
        /// <returns>返回对象</returns>
        public virtual tblScheduler FindSchedulerById(string KeyId)
        {
            tblScheduler SchedulerObj;
            System.Guid KeyIdGuid = new Guid(KeyId);

            try
            {
                ServiceDirectDB = new ServiceDirectDBEntities();
                SchedulerObj = ServiceDirectDB.tblScheduler.First(t => t.ScheduleID == KeyIdGuid);
            }
            catch (EntityException)
            {
                throw;
            }
            catch (NullReferenceException)
            {
                return null;
            }
            return SchedulerObj;
        }

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="KeyId">需要删除对象的ID</param>
        /// <returns>返回受影响的行数</returns>
        public virtual string LogicDelete(string KeyId)
        {
            int counts = 0;//影响行数标记
            string strMessage = "InsertError";

            System.Guid KeyIdGuid = new Guid(KeyId);//转换成Guid类型
            tblScheduler SchedulerObjDelete;//需要跟新的对象

            try
            {
                ServiceDirectDBEntitiesDelete = new ServiceDirectDBEntities();
                SchedulerObjDelete = ServiceDirectDBEntitiesDelete.tblScheduler.First(t => t.ScheduleID == KeyIdGuid);
                ServiceDirectDBEntitiesDelete.DeleteObject(SchedulerObjDelete);
                counts = ServiceDirectDBEntitiesDelete.SaveChanges();
            }
            catch (EntityException)
            {
                throw;
            }
            if (counts > 0)
            {
                strMessage = "update success";
                return strMessage;
            }
            return strMessage;
        }

        /// <summary>
        /// 根据条件以及排序规则，获取对象集
        /// </summary>
        /// <param name="SearchStr">查询条件</param>
        /// <param name="orderby">排序规则</param>
        /// <returns>符合条件的对象集</returns>
        public ObjectQuery<vBackupAndEmailSetting> GetBackupAndEmailSetting(string SearchStr, string orderby)
        {
            string StrWhere = " it.BackupID is not null " + SearchStr;
            ObjectQuery<vBackupAndEmailSetting> query = null;
            try
            {
                ServiceDirectDB = new ServiceDirectDBEntities();
                query = ServiceDirectDB.vBackupAndEmailSetting.Top("1").Where(StrWhere).OrderBy(orderby);
            }
            catch (EntitySqlException)
            {
                throw;
            }
            return query;
        }
    }
}
