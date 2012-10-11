using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ServiceDirect.Schedule.DAL;
using System.Data;
using System.Data.Objects;
using System.Web;
using System.Text.RegularExpressions;

namespace ServiceDirect.Schedule.Billing.BLL
{
    //Creat time: 	2011/03/14
    //Author:		jzheng
    //Description:	Job的执行情况
    public class vScheduleLogBLL
    {
        ServiceDirectDBEntities ServiceDirectDB;//查询

        //构造方法
        public vScheduleLogBLL()
        { }

        /// <summary>
        /// 根据Schedele的id查询job的执行消息，消息大于1个则说明job执行失败
        /// </summary>
        /// <param name="KeyId">Schedele的ID</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns></returns>
        public virtual string FindJobResultByScheduleId(string KeyId, string startTime, string endTime)
        {
            ObjectQuery<vUTPScheduleMessager> ScheduleLogObjs;
            string orderBy;
            string StrWhere;
            string strTemp;

            StrWhere = "it.PID is not null and it.SID='" + KeyId + "' and it.SDT>=cast('" + startTime + "' as System.DateTime) and it.SDT<=cast('" + endTime + "' as System.DateTime)";
            orderBy = "it.GDT desc";
            string str = HttpContext.GetGlobalResourceObject("WebResource", "ScheduleTasksForm_JobResult_value").ToString();

            try
            {
                ServiceDirectDB = new ServiceDirectDBEntities();
                ScheduleLogObjs = ServiceDirectDB.vUTPScheduleMessager.Where(StrWhere).OrderBy(orderBy);
            }
            catch (EntityException)
            {
                throw;
            }
            if (ScheduleLogObjs.Count() > 0)
            {
                foreach (vUTPScheduleMessager item in ScheduleLogObjs)
                {
                    strTemp = item.SMessage;
                    if (strTemp.IndexOf("Successfully") > 0)
                    {
                        return str = HttpContext.GetGlobalResourceObject("WebResource", "ScheduleTasksForm_JobResult_SuccessValue").ToString();
                    }
                    else
                    {
                        return str = HttpContext.GetGlobalResourceObject("WebResource", "ScheduleTasksForm_JobResult_FailValue").ToString();
                    }
                }
            }
            return str;
        }

        /// <summary>
        /// 查询所有job的结果信息
        /// </summary>
        /// <param name="SearchStr">查询条件</param>
        /// <param name="orderby">倒序排列</param>
        /// <returns></returns>
        public virtual ObjectQuery<vUTPScheduleMessager> FindJobResult(string SearchStr, string orderby)
        {
            ObjectQuery<vUTPScheduleMessager> ScheduleLogObjs;
            string StrWhere = " it.PID is not null " + SearchStr;

            try
            {
                ServiceDirectDB = new ServiceDirectDBEntities();
                ScheduleLogObjs = ServiceDirectDB.vUTPScheduleMessager.Where(StrWhere).OrderBy(orderby);
            }
            catch (EntitySqlException)
            {
                throw;
            }

            return ScheduleLogObjs;
        }

        /// <summary>
        /// 根据ScheduleId查询UserName信息
        /// </summary>
        /// <param name="ScheduleId"></param>
        /// <returns></returns>
        public vUTPScheduleMessager FindUserNameByScheduleId(string ScheduleId)
        {
            vUTPScheduleMessager ScheduleLogObj = null;

            try
            {
                ServiceDirectDB = new ServiceDirectDBEntities();
                ScheduleLogObj = ServiceDirectDB.vUTPScheduleMessager.FirstOrDefault(t => t.SID == ScheduleId);
            }
            catch (EntitySqlException)
            {
                throw;
            }
            return ScheduleLogObj;
        }
    }
}
