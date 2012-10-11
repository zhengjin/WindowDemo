using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo.Agent;
using Microsoft.SqlServer.Management.Smo.Broker;
using Microsoft.SqlServer.Management.Smo.Internal;
using Microsoft.SqlServer.Management.Smo.Mail;
using Microsoft.SqlServer.Management.Smo.SqlEnum;

using System.Data.OleDb;
using System.IO;
using System.Data.SqlClient;
using ServiceDirect.Schedule.DAL;
using System.Data;
using System.Web;
using System.Xml;
namespace ServiceDirect.Schedule.Billing.JOB
{
    /// <summary>
    /// 创建日期：2011-02-14。
    /// 创建人：闫保振。
    /// 描述：Job操作类，创建Job、删除job。1
    /// 注释：使用SMO需要引用下面的dll文件，文件路径：C:\Program Files\Microsoft SQL Server\100\SDK\Assemblies\ 
    /// Microsoft.SqlServer.ConnectionInfo.dll
    /// Microsoft.SqlServer.Smo.dll
    /// Microsoft.SqlServer.Management.Sdk.Sfc.dll
    /// Microsoft.SqlServer.SqlEnum.dll
    /// </summary>
    public class JOB
    {
        public JOB()
        { }

        #region 创建包含步骤和计划的作业 Billing JOB
        /// <summary>
        /// 创建包含步骤和计划的作业，返回 string  jobid
        /// </summary>
        /// <param name="KeyId">对象ID</param>
        /// <returns>返回 string</returns>

        public string CreateBilling(string KeyId, string JobType, string KillJobID)
        {
            try
            {

                #region 定义变量

                //数据库名称
                string strDatabaseName = string.Empty;

                //作业名称
                string strJobName = string.Empty;

                //作业ID
                string strJobID = string.Empty;

                //作业说明
                string strDescription = string.Empty;

                //开始时间
                DateTime dtStartDate = System.DateTime.Now;

                //计划频率，Daily：每天、Weekly：每周、Monthly：每月、Run Once：只运行一次
                FrequencyTypes FrequencyType = FrequencyTypes.Daily;

                //开始运行时间 如：10:30:00
                string strRunStartDate = string.Empty;

                //结束运行时间，如：11:05:00
                string strRunEndDate = string.Empty;

                //数据库服务名
                string strSQLServer = string.Empty;

                //数据库登录用户
                string strUser = string.Empty;

                //数据库登录密码
                string strPassword = string.Empty;

                //时
                int hours = 0;

                //分
                int minutes = 0;

                //秒
                int seconds = 0;

                //命令行
                string JobCommandString = string.Empty;
                vSchedulerEmailBackup vSchedulerEmailBackupObj = null;

                #endregion

                //返回vSchedulerEmailBackup 对象，根据id查询
                vSchedulerEmailBackupObj = this.FindvSchedulerEmailBackupById(KeyId);
                if (vSchedulerEmailBackupObj != null)
                {
                    #region 变量赋值

                    //strDatabaseName = "ServiceDirectDB";
                    strDatabaseName = this.GetConfig("strDatabaseName");
                    strJobName = vSchedulerEmailBackupObj.TaskName;
                    if (vSchedulerEmailBackupObj.JobID != null)
                    {
                        strJobID = vSchedulerEmailBackupObj.JobID.ToString();
                    }
                    //描述：保存的是KillJob的ID
                    strDescription = KillJobID;
                    dtStartDate = Convert.ToDateTime(vSchedulerEmailBackupObj.StartTime);
                    switch (vSchedulerEmailBackupObj.ScheduleType)
                    {
                        case "Daily":
                            FrequencyType = FrequencyTypes.Daily;
                            break;
                        case "Run Once":
                            FrequencyType = FrequencyTypes.OneTime;
                            break;
                    }
                    DateTime dtRunOnlyStart = Convert.ToDateTime(vSchedulerEmailBackupObj.RunOnlyStart);
                    DateTime dtRunOnlyEnd = Convert.ToDateTime(vSchedulerEmailBackupObj.RunOnlyEnd);
                    strRunStartDate = dtRunOnlyStart.ToLongTimeString();
                    strRunEndDate = dtRunOnlyEnd.ToLongTimeString();

                    //开始运行时间 时：分：秒：
                    hours = dtRunOnlyStart.Hour;
                    minutes = dtRunOnlyStart.Minute;
                    seconds = dtRunOnlyStart.Second;

                    //获取自定义config的值
                    strSQLServer = this.GetConfig("strSQLServer");
                    strUser = this.GetConfig("strUser");
                    strPassword = this.GetConfig("strPassword");
                    #endregion
                    #region 创建作业

                    ServiceDirectDBEntities objDB = new ServiceDirectDBEntities();

                    //获取数据库名
                    string strdatabase = objDB.Connection.Database;

                    //获取数据库服务器名
                    string strDataSource = objDB.Connection.DataSource;
                    ServerConnection conn = null;
                    Server myServer = null;
                    conn = new ServerConnection(strSQLServer, strUser, strPassword);
                    myServer = new Server(conn);

                    //调用删除Billing 的Job
                    //DeleteJob(strJobID);

                    Job jb = new Job(myServer.JobServer, strJobName);
                    jb.Description = strDescription;

                    //更改JOB状态
                    if (JobType == "enable")
                    {
                        jb.IsEnabled = true;
                    }
                    switch (JobType)
                    {
                        case "enable":
                            jb.IsEnabled = true;
                            break;
                        case "disable":
                            jb.IsEnabled = false;
                            break;
                        case "Insert":
                            jb.IsEnabled = true;
                            break;
                    }

                    //创建JOB
                    jb.Create();
                    #endregion 创建作业


                    if (jb.JobID != null)
                    {
                        //获取命令行
                        JobCommandString = this.GetJobCommandString(vSchedulerEmailBackupObj, KeyId, jb);
                    }

                    #region 作业步骤

                    Steps steps = new Steps();

                    //创建步骤
                    steps.CreateStep(jb, strDatabaseName, JobCommandString, strRunEndDate);

                    #endregion 作业步骤

                    #region 作业计划属性

                    JobSchedule jbsch = new JobSchedule(jb, "ScheduleBilling");

                    //计划频率，每几天一次              
                    jbsch.FrequencyTypes = FrequencyType;
                    if (vSchedulerEmailBackupObj.ScheduleType.Equals("Daily"))
                    {
                        //执行间隔 (天)
                        jbsch.FrequencyInterval = 1;

                        //当天只执行一次
                        jbsch.FrequencySubDayTypes = FrequencySubDayTypes.Once;
                    }
                    if (strRunStartDate != string.Empty)
                    {
                        //开始执行时间
                        jbsch.ActiveStartTimeOfDay = new TimeSpan(hours, minutes, seconds);
                    }

                    //持续时间                
                    if (dtStartDate != null)
                    {
                        //开始时间   
                        jbsch.ActiveStartDate = dtStartDate;
                    }
                    else
                    {
                        jbsch.ActiveStartDate = DateTime.Now;
                    }

                    //创建SQL代理实例的作业调度
                    jbsch.Create();
                    jb.ApplyToTargetServer(myServer.JobServer.Name);

                    //创建成功后立刻执行一次开始                    
                    //jb.Start();

                    //创建成功后立刻执行一次结束
                    #endregion 作业计划属性

                    //返回作业GUID
                    return jb.JobID.ToString();
                }
                else
                {
                    return "";
                }

            }
            catch (Exception)
            {
                return "";
            }
        }
        #endregion

        #region 创建包含步骤和计划的作业 Kill的JOB
        /// <summary>
        /// 创建包含步骤和计划的作业，返回 string  jobid, Kill的JOB
        /// </summary>
        /// <param name="KeyId">对象ID</param>
        /// <returns>返回 string</returns>

        public string Create(string KeyId, string JobType)
        {
            try
            {

                #region 定义变量

                //数据库名称
                string strDatabaseName = string.Empty;

                //作业名称
                string strJobName = string.Empty;

                //作业ID
                string strJobID = string.Empty;

                //作业说明
                string strDescription = string.Empty;

                //开始时间
                DateTime dtStartDate = System.DateTime.Now;

                //计划频率，Daily：每天、Weekly：每周、Monthly：每月、Run Once：只运行一次
                FrequencyTypes FrequencyType = FrequencyTypes.Daily;

                //开始运行时间 如：10:30:00
                string strRunStartDate = string.Empty;

                //结束运行时间，如：11:05:00
                string strRunEndDate = string.Empty;

                //数据库服务名
                string strSQLServer = string.Empty;

                //数据库登录用户
                string strUser = string.Empty;

                //数据库登录密码
                string strPassword = string.Empty;

                //时
                int hours = 0;

                //分
                int minutes = 0;

                //秒
                int seconds = 0;

                //命令行
                string JobCommandString = string.Empty;
                vSchedulerEmailBackup vSchedulerEmailBackupObj = null;

                #endregion

                //返回vSchedulerEmailBackup 对象，根据id查询
                vSchedulerEmailBackupObj = this.FindvSchedulerEmailBackupById(KeyId);
                if (vSchedulerEmailBackupObj != null)
                {
                    #region 变量赋值

                    //strDatabaseName = "ServiceDirectDB";
                    strDatabaseName = this.GetConfig("strDatabaseName");
                    string[] strJobNames = vSchedulerEmailBackupObj.TaskName.Split('-');
                    strJobName = strJobNames[0].ToString() + "-Kill";

                    if (vSchedulerEmailBackupObj.JobID != null)
                    {
                        strJobID = vSchedulerEmailBackupObj.JobID.ToString();
                    }
                    strDescription = "KillUTP";
                    dtStartDate = Convert.ToDateTime(vSchedulerEmailBackupObj.RunOnlyEnd);
                    switch (vSchedulerEmailBackupObj.ScheduleType)
                    {
                        case "Daily":
                            FrequencyType = FrequencyTypes.Daily;
                            break;
                        case "Run Once":
                            FrequencyType = FrequencyTypes.OneTime;
                            break;
                    }
                    DateTime dtRunOnlyStart = Convert.ToDateTime(vSchedulerEmailBackupObj.RunOnlyStart);
                    DateTime dtRunOnlyEnd = Convert.ToDateTime(vSchedulerEmailBackupObj.RunOnlyEnd);
                    strRunStartDate = dtRunOnlyStart.ToLongTimeString();
                    strRunEndDate = dtRunOnlyEnd.ToLongTimeString();

                    //开始运行时间 时：分：秒：
                    hours = dtRunOnlyEnd.Hour;
                    //结束时间减去5分钟之后在运行Kill的JOB
                    minutes = dtRunOnlyEnd.Minute - 5;
                    seconds = dtRunOnlyEnd.Second;

                    //获取自定义config的值
                    strSQLServer = this.GetConfig("strSQLServer");
                    strUser = this.GetConfig("strUser");
                    strPassword = this.GetConfig("strPassword");
                    #endregion
                    #region 创建作业

                    ServiceDirectDBEntities objDB = new ServiceDirectDBEntities();

                    //获取数据库名
                    string strdatabase = objDB.Connection.Database;

                    //获取数据库服务器名
                    string strDataSource = objDB.Connection.DataSource;
                    ServerConnection conn = null;
                    Server myServer = null;
                    conn = new ServerConnection(strSQLServer, strUser, strPassword);
                    myServer = new Server(conn);

                    string KillID = string.Empty;

                    //调用删除job方法，传入job名称
                    //DeleteJob(KillID);
                    DeleteJob(strJobID);

                    Job jb = new Job(myServer.JobServer, strJobName);
                    jb.Description = strDescription;

                    //更改JOB状态
                    if (JobType == "enable")
                    {
                        jb.IsEnabled = true;
                    }
                    switch (JobType)
                    {
                        case "enable":
                            jb.IsEnabled = true;
                            break;
                        case "disable":
                            jb.IsEnabled = false;
                            break;
                        case "Insert":
                            jb.IsEnabled = true;
                            break;
                    }

                    //创建JOB
                    jb.Create();
                    #endregion 创建作业

                    #region 作业步骤

                    Steps steps = new Steps();

                    //创建步骤
                    steps.CreateStepKill(jb, strDatabaseName, KeyId);

                    #endregion 作业步骤

                    #region 作业计划属性

                    JobSchedule jbsch = new JobSchedule(jb, "ScheduleKill");

                    //计划频率，每几天一次              
                    jbsch.FrequencyTypes = FrequencyType;
                    if (vSchedulerEmailBackupObj.ScheduleType.Equals("Daily"))
                    {
                        //执行间隔 (天)
                        jbsch.FrequencyInterval = 1;

                        //当天只执行一次
                        jbsch.FrequencySubDayTypes = FrequencySubDayTypes.Once;
                    }
                    if (strRunStartDate != string.Empty)
                    {
                        //开始执行时间
                        jbsch.ActiveStartTimeOfDay = new TimeSpan(hours, minutes, seconds);
                    }

                    //持续时间                
                    if (dtStartDate != null)
                    {
                        //开始时间   
                        jbsch.ActiveStartDate = dtStartDate;
                    }
                    else
                    {
                        jbsch.ActiveStartDate = DateTime.Now;
                    }

                    //创建SQL代理实例的作业调度
                    jbsch.Create();
                    jb.ApplyToTargetServer(myServer.JobServer.Name);

                    //创建成功后立刻执行一次开始                    
                    //jb.Start();

                    //创建成功后立刻执行一次结束
                    #endregion 作业计划属性

                    string KillJobID = jb.JobID.ToString();
                    string BillinJobID = string.Empty;
                    BillinJobID = this.CreateBilling(KeyId, JobType, KillJobID);
                    //返回作业GUID
                    return BillinJobID;
                }
                else
                {
                    return "";
                }

            }
            catch (Exception)
            {
                return "";
            }
        }
        #endregion

        #region 获取命令行
        /// <summary>
        /// 获取命令行
        /// </summary>
        /// <param name="vSchedulerEmailBackupObj">vSchedulerEmailBackup对象</param>
        /// <param name="KeyId">主键值</param>
        /// <returns></returns>
        public string GetJobCommandString(vSchedulerEmailBackup vSchedulerEmailBackupObj, string KeyId, Job jb)
        {
            string JobCommandString = string.Empty;
            UTPCommand comm = new UTPCommand();
            try
            {
                //获取UTP命令行
                comm = this.UTPCommands(vSchedulerEmailBackupObj, jb);

                //UTP命令赋值
                UTPCommandBuilder builder = new UTPCommandBuilder(comm);
                JobCommandString = builder.JobCommandString;

                //删除数据
                this.LogicDelete(KeyId);

                //插入数据
                this.InsertCommand(builder, KeyId);
                return JobCommandString;
            }
            catch (Exception)
            {
                return "";
            }

        }
        #endregion

        #region UTP命令行赋值

        public UTPCommand UTPCommands(vSchedulerEmailBackup vSchedulerEmailBackupObj, Job jb)
        {
            UTPCommand comm = new UTPCommand();

            //操作类型
            switch (vSchedulerEmailBackupObj.Action)
            {
                case "Billing":
                    comm.ActiveType = ActionType.Billing;
                    break;
            }

            //自动计算用量           
            switch (vSchedulerEmailBackupObj.Calc)
            {
                case "True":
                    comm.AutoCalc = CalcUsageBeforeBillingRegister.Yes;
                    break;
                case "False":
                    comm.AutoCalc = CalcUsageBeforeBillingRegister.No;
                    break;
            }

            //公司标识
            comm.Company = vSchedulerEmailBackupObj.Company;

            //算费周期范围
            comm.Cycle = vSchedulerEmailBackupObj.Cycle;

            //镜像复制
            switch (vSchedulerEmailBackupObj.Copy)
            {
                case "True":
                    comm.ShadowCopies = BillingShadowCopies.Yes;
                    break;
                case "False":
                    comm.ShadowCopies = BillingShadowCopies.No;
                    break;
            }

            //账户状态
            if (vSchedulerEmailBackupObj.Status != "All")
            {
                comm.Status = vSchedulerEmailBackupObj.Status;
            }
            //日志
            switch (vSchedulerEmailBackupObj.Trace)
            {
                case "True":
                    comm.Trace = Trace.Yes;
                    break;
                case "False":
                    comm.Trace = Trace.Yes;
                    break;
            }

            //日志数据库连接字符串
            comm.TraceDBConnectionString = vSchedulerEmailBackupObj.TraceTable;

            //日志文件保存路径
            //comm.TraceFilePath=vSchedulerEmailBackupObj.trace

            //日志类型
            switch (vSchedulerEmailBackupObj.TraceType)
            {
                case "File":
                    comm.TraceType = TraceType.File;
                    break;
                case "Table":
                    comm.TraceType = TraceType.Table;
                    break;
                case "None":
                    comm.TraceType = TraceType.None;
                    break;
            }

            //口令
            comm.UTPPassword = vSchedulerEmailBackupObj.UTPPwd;

            //用户名
            comm.UTPUsername = vSchedulerEmailBackupObj.UTPUser;

            //计划标识
            comm.GUID = vSchedulerEmailBackupObj.ScheduleID.ToString();

            //UTP程序路径,保存了UTP EXE文件所在的应用程序路径,此属性值要求包含Utility.exe的全路径。
            comm.UTPPath = "*" + this.GetConfig("UTPPath") + "*";

            //计划标识符
            comm.SID = vSchedulerEmailBackupObj.ScheduleID;


            return comm;
        }

        #endregion

        #region 删除job
        /// <summary>
        /// 删除job
        /// </summary>
        /// <param name="jobName">jobID</param>
        /// <returns></returns>
        public int DeleteJob(string jobID)
        {
            try
            {
                //数据库服务名
                string strSQLServer = string.Empty;

                //数据库登录用户
                string strUser = string.Empty;

                //数据库登录密码
                string strPassword = string.Empty;

                //获取自定义config的值
                strSQLServer = this.GetConfig("strSQLServer");
                strUser = this.GetConfig("strUser");
                strPassword = this.GetConfig("strPassword");

                System.Guid GuidJobID = new Guid(jobID);

                ServerConnection conn = null;
                Server myServer = null;
                conn = new ServerConnection(strSQLServer, strUser, strPassword);
                myServer = new Server(conn);

                //删除vUTPCommand
                string strsid = string.Empty;
                strsid = GetScheduleID(jobID);
                if (strsid != string.Empty)
                {
                    LogicDelete(strsid);
                }

                //获取 Billing Job 获取Kill JOB的JOBID
                string KillID = GetKillID(jobID);
                System.Guid GuidJobKillID = new Guid(KillID);

                //删除JOB
                myServer.JobServer.RemoveJobByID(GuidJobID);
                myServer.JobServer.RemoveJobByID(GuidJobKillID);


                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }


        #endregion

        #region 插入数据 vUTPCommand
        public void InsertCommand(UTPCommandBuilder builder, string RelateID)
        {
            ServiceDirectDBEntities objDB;
            vUTPCommand vUTPCommandObj;

            //影响行数标记            
            int counts = 0;

            //插入数据
            try
            {
                objDB = new ServiceDirectDBEntities();
                vUTPCommandObj = new vUTPCommand();
                vUTPCommandObj.CommandID = builder.CommandID;
                vUTPCommandObj.CommandLine = builder.CommandString;
                vUTPCommandObj.RelateID = RelateID;
                vUTPCommandObj.Sequence = 1;
                objDB.vUTPCommand.AddObject(vUTPCommandObj);
                counts = objDB.SaveChanges();
            }
            catch (EntitySqlException)
            {
                throw;
            }
        }
        #endregion

        #region 删除数据

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="KeyId">需要删除对象的ID</param>
        /// <returns>返回受影响的行数</returns>
        public virtual int LogicDelete(string KeyId)
        {
            int counts = 0;//影响行数标记
            ServiceDirectDBEntities objDB;
            vUTPCommand vUTPCommandObj;
            try
            {
                objDB = new ServiceDirectDBEntities();
                vUTPCommandObj = objDB.vUTPCommand.FirstOrDefault(c => c.RelateID == KeyId);
                if (vUTPCommandObj != null)
                {
                    if (vUTPCommandObj.RelateID != null && vUTPCommandObj.RelateID != string.Empty)
                    {
                        objDB.DeleteObject(vUTPCommandObj);
                        counts = objDB.SaveChanges();
                    }
                }
            }
            catch (EntityException)
            {
                throw;
            }
            return counts;
        }

        #endregion

        #region 返回vSchedulerEmailBackup对象
        /// <summary>
        /// 根据对象的ID查询数据
        /// </summary>
        /// <param name="KeyId">对象ID</param>
        /// <returns>返回对象</returns>
        public virtual vSchedulerEmailBackup FindvSchedulerEmailBackupById(string KeyId)
        {
            ServiceDirectDBEntities objDB;
            vSchedulerEmailBackup vSchedulerEmailBackupObj;
            System.Guid KeyIdGuid = new Guid(KeyId);
            try
            {
                objDB = new ServiceDirectDBEntities();
                vSchedulerEmailBackupObj = objDB.vSchedulerEmailBackup.FirstOrDefault(s => s.ScheduleID == KeyIdGuid);
            }
            catch (EntityException)
            {
                return null;
            }
            return vSchedulerEmailBackupObj;
        }
        #endregion

        #region 获取自定义 Config文件中的节点值
        /// <summary>
        /// 获取自定义 Config文件中的节点值
        /// </summary>
        /// <param name="strKey">节点的KeyName</param>
        /// <returns></returns>
        public string GetConfig(string strKeyName)
        {
            string strValue = string.Empty;
            //获取config文件绝对路径
            string strPath = HttpContext.Current.Server.MapPath("~/AddInManager.config");

            // new一个XMLTextReader实例
            XmlTextReader reader = new XmlTextReader(strPath);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);

            //关闭reader，不然config文件就变成只读的了
            reader.Close();
            XmlNode node = doc.SelectSingleNode(@"//add[@key='" + strKeyName + "']");

            //获取值
            strValue = node.Attributes["value"].Value;
            return strValue;
        }
        #endregion

        #region 获取KillJOB的ID
        public string GetKillID(string jobID)
        {
            string strid = string.Empty;
            vSchedulerTasks vSchedulerTasksObj;
            vSchedulerTasksObj = FindvSchedulerTasksById(jobID);
            if (vSchedulerTasksObj != null)
            {
                strid = vSchedulerTasksObj.description;
            }

            return strid;
        }
        #endregion

        #region 获取ScheduleID
        /// <summary>
        /// 获取ScheduleID
        /// </summary>
        /// <param name="jobID">jobID</param>
        /// <returns></returns>
        public string GetScheduleID(string jobID)
        {
            string strid = string.Empty;
            vSchedulerTasks vSchedulerTasksObj;
            vSchedulerTasksObj = FindvSchedulerTasksById(jobID);
            if (vSchedulerTasksObj != null)
            {
                strid = vSchedulerTasksObj.ScheduleID.ToString();
            }

            return strid;
        }
        #endregion

        #region 返回vSchedulerTasks对象
        /// <summary>
        /// 根据对象的ID查询数据
        /// </summary>
        /// <param name="KeyId">对象ID</param>
        /// <returns>返回对象</returns>
        public virtual vSchedulerTasks FindvSchedulerTasksById(string jobID)
        {
            ServiceDirectDBEntities objDB;
            vSchedulerTasks vSchedulerTasksObj;
            System.Guid KeyIdGuid = new Guid(jobID);
            try
            {
                objDB = new ServiceDirectDBEntities();
                vSchedulerTasksObj = objDB.vSchedulerTasks.FirstOrDefault(s => s.JobID == KeyIdGuid);
            }
            catch (EntityException)
            {
                return null;
            }
            return vSchedulerTasksObj;
        }
        #endregion

    }

}
