using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ServiceDirect.Schedule.DAL;
namespace ServiceDirect.Schedule.Billing.JOB
{
    public class Email
    {
        public Email()
        { }
        /// <summary>
        /// 调用存储过程，创建发送的Email
        /// </summary>
        /// <param name="EmailID">EmailID</param>
        /// <returns></returns>
        public int CreateEmail(string EmailID)
        {
            int counts = 0;
            ServiceDirectDBEntities objDB;
            try
            {
                objDB = new ServiceDirectDBEntities();
                counts = objDB.PrJobEmail(EmailID);
                return counts;
            }
            catch (Exception)
            {
                return counts;
            }
        }
    }
}
