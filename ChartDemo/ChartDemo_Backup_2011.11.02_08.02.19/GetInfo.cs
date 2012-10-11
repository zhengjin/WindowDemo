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
                               + " where it.TransactionDate between cast('" + transactiondate.AddDays(1).AddMonths(-1)
                           + "' as System.DateTime) and cast('" + transactiondate + "' as System.DateTime) group by it.TransactionDate"; ;
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
    }
}