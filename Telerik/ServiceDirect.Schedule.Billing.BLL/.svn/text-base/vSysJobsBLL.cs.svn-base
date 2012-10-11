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
    /**Creat time: 	2011/02/24
    *Author:		jzheng
    *Description:	SysJobs信息控制类
    */
    public class vSysJobsBLL
    {
        ServiceDirectDBEntities ServiceDirectDB;//查询

        public vSysJobsBLL()
        {
            
        }

        /// <summary>
        /// 根据条件以及排序规则，获取对象集
        /// </summary>
        /// <param name="SearchStr">查询条件</param>
        /// <param name="orderby">排序规则</param>
        /// <returns>符合条件的对象集</returns>
        public ObjectQuery<vSchedulerTasks> GetVSysJobs(string SearchStr, string orderby)
        {
            string StrWhere = " it.job_id is not null " + SearchStr;
            ObjectQuery<vSchedulerTasks> query = null;
            try
            {
                ServiceDirectDB = new ServiceDirectDBEntities();
                query = ServiceDirectDB.vSchedulerTasks.Where(StrWhere).OrderBy(orderby);
            }
            catch (EntitySqlException)
            {
                throw;
            }
            return query;
        }

        /// <summary>
        /// 根据条件以及排序规则，获取对象集
        /// </summary>
        /// <param name="SearchStr">查询条件</param>
        /// <param name="orderby">排序规则</param>
        /// <returns>符合条件的对象集</returns>
        public ObjectQuery<vOnlySysJobs> GetVOnlyJobs(string SearchStr, string orderby)
        {
            string StrWhere = " it.job_id is not null " + SearchStr;
            ObjectQuery<vOnlySysJobs> query = null;
            try
            {
                ServiceDirectDB = new ServiceDirectDBEntities();
                query = ServiceDirectDB.vOnlySysJobs.Where(StrWhere).OrderBy(orderby);
            }
            catch (EntitySqlException)
            {
                throw;
            }
            return query;
        }

        /// <summary>
        /// 根据jobID查询job对象
        /// </summary>
        /// <param name="jobId">jobId</param>
        /// <returns>job对象</returns>
        public vOnlySysJobs FindVOnlyJobByJobId(string jobId)
        {
            vOnlySysJobs VOnlyJobObj = null;
            System.Guid KeyIdGuid = new Guid(jobId);

            try
            {
                ServiceDirectDB = new ServiceDirectDBEntities();
                VOnlyJobObj = ServiceDirectDB.vOnlySysJobs.FirstOrDefault(t => t.job_id == KeyIdGuid);
            }
            catch (EntitySqlException)
            {
                throw;
            }
            return VOnlyJobObj;
        }

        /// <summary>
        /// 根据TasksId查询job对象
        /// </summary>
        /// <param name="TasksId">TasksId</param>
        /// <returns>job对象</returns>
        public vSchedulerTasks FindTaskSchedulerByTaskId(string TasksId)
        {
            vSchedulerTasks vSchedulerTasksObj = null;
            System.Guid KeyIdGuid = new Guid(TasksId);

            try
            {
                ServiceDirectDB = new ServiceDirectDBEntities();
                vSchedulerTasksObj = ServiceDirectDB.vSchedulerTasks.First(t => t.ScheduleID == KeyIdGuid);
            }
            catch (EntitySqlException)
            {
                throw;
            }
            catch(InvalidOperationException)
            {
                return null;
            }
            return vSchedulerTasksObj;
        }
    }
}
