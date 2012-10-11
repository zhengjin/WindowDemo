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
    *Description:	邮件配置任务控制类
    */
    public class EmailBLL
    {
        ServiceDirectDBEntities ServiceDirectDBEntitieAdd;//增加
        ServiceDirectDBEntities ServiceDirectDBEntitiesDelete;//删除
        ServiceDirectDBEntities ServiceDirectDB;//查询

        //构造方法
        public EmailBLL()
        { 
            
        }

        /// <summary>
        /// 根据条件以及排序规则，获取对象集
        /// </summary>
        /// <param name="SearchStr">查询条件</param>
        /// <param name="orderby">排序规则</param>
        /// <returns>符合条件的对象集</returns>
        public ObjectQuery<tblEmail> GetEmails(string SearchStr, string orderby)
        {
            string StrWhere = " it.ScheduleID is not null " + SearchStr;
            ObjectQuery<tblEmail> query = null;
            try
            {
                query = ServiceDirectDB.tblEmail.Where(StrWhere).OrderBy(orderby);
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
        /// <param name="EmailObj">需要插入的对象</param>
        /// <returns>受影响的行数</returns>
        public virtual string Insert(tblEmail EmailObj)
        {
            string strErrorMessage = "InsertError";
            int counts = 0;//影响行数标记

            //插入数据
            try
            {
                ServiceDirectDBEntitieAdd = new ServiceDirectDBEntities();
                ServiceDirectDBEntitieAdd.tblEmail.AddObject(EmailObj);
                counts = ServiceDirectDBEntitieAdd.SaveChanges();
            }
            catch (EntitySqlException)
            {
                throw;
            }
            if (counts > 0)
            {
                return EmailObj.EmailID.ToString();
            }
            return strErrorMessage;
        }

        /// <summary>
        /// 根据对象的ID查询数据
        /// </summary>
        /// <param name="KeyId">对象的ID</param>
        /// <returns>返回对象</returns>
        public virtual tblEmail FindEmailById(string KeyId)
        {
            tblEmail EmailObj;
            System.Guid KeyIdGuid = new Guid(KeyId);

            try
            {
                ServiceDirectDB = new ServiceDirectDBEntities();
                EmailObj = ServiceDirectDB.tblEmail.First(t => t.EmailID == KeyIdGuid);
            }
            catch (EntityException)
            {
                throw;
            }
            return EmailObj;
        }

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="EmailObj">需要更新的对象</param>
        /// <returns>返回受影响的行数</returns>
        public virtual string Update(tblEmail EmailObj)
        {
            tblEmail EmailObjUpdate;//需要跟新的对象

            string strMessage = "InsertError";
            int counts = 0;//影响行数标记

            try
            {
                ServiceDirectDB = new ServiceDirectDBEntities();
                EmailObjUpdate = ServiceDirectDB.tblEmail.First(t => t.EmailID == EmailObj.EmailID);
            }catch(EntitySqlException)
            {
                throw;
            }
            catch (EntityException)
            {
                throw;
            }

            //对象进行赋值，准备更新
            EmailObjUpdate.EmailFrom = EmailObj.EmailFrom;
            //EmailObjUpdate.EmailTo = EmailObj.EmailTo;
            EmailObjUpdate.EmailPort = EmailObj.EmailPort;
            EmailObjUpdate.EmailSSL = EmailObj.EmailSSL;

            EmailObjUpdate.SMTPHost = EmailObj.SMTPHost;
            EmailObjUpdate.SMTPUsername = EmailObj.SMTPUsername;
            EmailObjUpdate.SMTPPassword = EmailObj.SMTPPassword;

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
            tblEmail BackupObjDelete;//需要跟新的对象

            try
            {
                ServiceDirectDBEntitiesDelete = new ServiceDirectDBEntities();
                BackupObjDelete = ServiceDirectDBEntitiesDelete.tblEmail.First(t => t.EmailID == KeyIdGuid);
                ServiceDirectDBEntitiesDelete.DeleteObject(BackupObjDelete);
                counts = ServiceDirectDBEntitiesDelete.SaveChanges();
            }
            catch (EntityException)
            {
                throw;
            }
            return counts;
        }
    }
}
