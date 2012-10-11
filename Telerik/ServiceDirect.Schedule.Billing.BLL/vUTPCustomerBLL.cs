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
    /**Creat time: 	2011/02/17
    *Author:		jzheng
    *Description:	vtpCustomer信息控制类
    */
    public class vUTPCustomerBLL
    {
        ServiceDirectDBEntities ServiceDirectDB;//查询

        public vUTPCustomerBLL()
        {
            
        }

        /// <summary>
        /// 根据条件以及排序规则，获取对象集
        /// </summary>
        /// <param name="SearchStr">查询条件</param>
        /// <param name="orderby">排序规则</param>
        /// <returns>符合条件的对象集</returns>
        public ObjectQuery<vUTPCompany> GetUTPCompanys(string SearchStr, string orderby)
        {
            string StrWhere = " it.CompanyID is not null " + SearchStr;
            ObjectQuery<vUTPCompany> query = null;
            try
            {
                ServiceDirectDB = new ServiceDirectDBEntities();
                query = ServiceDirectDB.vUTPCompany.Where(StrWhere).OrderBy(orderby);
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
        public ObjectQuery<vUTPCustomerState> GetUTPCustomerStates(string SearchStr, string orderby)
        {
            string StrWhere = " it.CustomerStateID is not null " + SearchStr;
            ObjectQuery<vUTPCustomerState> query = null;
            try
            {
                ServiceDirectDB = new ServiceDirectDBEntities();
                query = ServiceDirectDB.vUTPCustomerState.Where(StrWhere).OrderBy(orderby);
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
        public ObjectQuery<vUTPCycle> GetUTPCycles(string SearchStr, string orderby)
        {
            string StrWhere = " it.CycleID is not null " + SearchStr;
            ObjectQuery<vUTPCycle> query = null;
            try
            {
                ServiceDirectDB = new ServiceDirectDBEntities();
                query = ServiceDirectDB.vUTPCycle.Where(StrWhere).OrderBy(orderby);
            }
            catch (EntitySqlException)
            {
                throw;
            }
            return query;
        }

        /// <summary>
        /// 根据Company对象的ID查询数据
        /// </summary>
        /// <param name="KeyId">Company的ID</param>
        /// <returns>返回对象</returns>
        public virtual vUTPCycle FindFirstCycleByCompanyId(string KeyId, string orderby)
        {
            vUTPCycle CycleObj;

            try
            {
                ServiceDirectDB = new ServiceDirectDBEntities();
                CycleObj = ServiceDirectDB.vUTPCycle.OrderBy(orderby).First(t => t.CompanyID == KeyId);
            }
            catch (EntityException)
            {
                throw;
            }
            return CycleObj;
        }

        /// <summary>
        /// 根据Company对象的ID查询数据
        /// </summary>
        /// <param name="KeyId">Company的ID</param>
        /// <returns>返回对象</returns>
        public virtual vUTPCycle FindLastCycleByCompanyId(string KeyId, string orderby)
        {
            vUTPCycle CycleObj;

            string SearchStr = " and it.CompanyID='" + KeyId + "'";
            string StrWhere = " it.CycleID is not null " + SearchStr;
            List<vUTPCycle> query = null;
            try
            {
                ServiceDirectDB = new ServiceDirectDBEntities();
                query = ServiceDirectDB.vUTPCycle.Where(StrWhere).OrderBy(orderby).ToList();
                CycleObj = query.Last();
            }
            catch (EntitySqlException)
            {
                throw;
            }
            catch (EntityException)
            {
                throw;
            }
            return CycleObj;
        }
    }
}
