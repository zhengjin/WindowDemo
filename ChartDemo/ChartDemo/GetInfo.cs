using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Web;

namespace ChartDemo
{
    public class GetInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public GetInfo()
        { }

        public decimal GetBillingInfo(DateTime transactiondate)
        {
            try
            {
                using (MECEntities objDB = new MECEntities())
                {
                    string esql = "select value sum(it.TotalDue) from MECEntities.ViewBillHistory as it "
                               + " where it.TransactionDate between cast('" + transactiondate.AddMonths(-1).AddDays(1)
                           + "' as System.DateTime) and cast('" + transactiondate + "' as System.DateTime) group by it.TransactionMonth"; ;
                    ObjectQuery<decimal> query = objDB.CreateQuery<decimal>(esql);
                    foreach (var item in query)
                    {
                        return item;
                    }
                }
            }
            catch (Exception)
            {
                return 0;
            }
            return 0;
        }

        public decimal GetBillingInfo(DateTime transactiondate, string companyId)
        {
            try
            {
                using (MECEntities objDB = new MECEntities())
                {
                    string esql = "select value sum(it.TotalDue) from MECEntities.ViewBillHistory as it "
                               + " where it.TransactionDate between cast('" + transactiondate.AddMonths(-1).AddDays(1)
                           + "' as System.DateTime) and cast('" + transactiondate + "' as System.DateTime) and it.CompanyID=='"
                           + companyId + "' group by it.TransactionMonth"; ;
                    ObjectQuery<decimal> query = objDB.CreateQuery<decimal>(esql);
                    foreach (var item in query)
                    {
                        return item;
                    }
                }
            }
            catch (Exception)
            {
                return 0;
            }
            return 0;
        }

        public decimal GetBillingInfo(DateTime transactiondate, string companyId, string cycle)
        {
            try
            {
                using (MECEntities objDB = new MECEntities())
                {
                    string esql = "select value sum(it.TotalDue) from MECEntities.ViewBillHistory as it "
                               + " where it.TransactionDate between cast('" + transactiondate.AddMonths(-1).AddDays(1)
                           + "' as System.DateTime) and cast('" + transactiondate + "' as System.DateTime) and it.Cycle=="
                           + cycle + " and it.CompanyID=='" + companyId + "' group by it.TransactionMonth"; ;
                    ObjectQuery<decimal> query = objDB.CreateQuery<decimal>(esql);
                    foreach (var item in query)
                    {
                        return item;
                    }
                }
            }
            catch (Exception)
            {
                return 0;
            }
            return 0;
        }

        public decimal GetBillingInfo(DateTime transactiondate, string companyId, string cycle, string accNum)
        {
            try
            {
                using (MECEntities objDB = new MECEntities())
                {
                    string esql = "select value sum(it.TotalDue) from MECEntities.ViewBillHistory as it "
                               + " where it.TransactionDate between cast('" + transactiondate.AddMonths(-1).AddDays(1)
                           + "' as System.DateTime) and cast('" + transactiondate + "' as System.DateTime) and it.Cycle=="
                           + cycle + " and it.CompanyID=='" + companyId + "' and it.AcctNum='" + accNum + "' group by it.TransactionMonth"; ;
                    ObjectQuery<decimal> query = objDB.CreateQuery<decimal>(esql);
                    foreach (var item in query)
                    {
                        return item;
                    }
                }
            }
            catch (Exception)
            {
                return 0;
            }
            return 0;
        }

        public List<ViewTopCustomer> GetTopCustomer()
        {
            try
            {
                using (MECEntities objDB = new MECEntities())
                {
                    List<ViewTopCustomer> viewTopCustomerList =
                        objDB.ViewTopCustomer.Top("10").Where(it => it.AcctNum != null).ToList();
                    return viewTopCustomerList;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        #region 获取公司和区域
        public List<tblCompany> GetCompany()
        {
            List<tblCompany> companyObjList;

            try
            {
                using (MECEntities objDB = new MECEntities())
                {
                    companyObjList = objDB.tblCompany.Where(it => it.Comp != null).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return companyObjList;
        }

        public List<tblCycle> GetCycleByCompanyId(string companyId)
        {
            List<tblCycle> cycleObjList;

            try
            {
                using (MECEntities objDB = new MECEntities())
                {
                    cycleObjList = objDB.tblCycle.Where(it => it.CompanyID == companyId).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return cycleObjList;
        }
        #endregion
    }
}