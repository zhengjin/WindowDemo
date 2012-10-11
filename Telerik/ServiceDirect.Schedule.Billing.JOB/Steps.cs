using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.SqlServer.Management.Smo.Agent;
using ServiceDirect.Schedule.DAL;

namespace ServiceDirect.Schedule.Billing.JOB
{
    /// <summary>
    /// 创建日期：2011-02-21。
    /// 创建人：闫保振。
    /// 描述：创建JOB的步骤。
    /// </summary>
    public class Steps
    {
        public Steps()
        { }

        #region 创建步骤

        /// <summary>
        /// 创建步骤
        /// </summary>
        /// <param name="jb">JOB 对象</param>
        /// <param name="strDatabaseName">数据库名称</param>
        /// <param name="JobCommandString">要执行的命令行</param>
        /// <param name="strRunEndDate">运行结束时间</param>
        public void CreateStep(Job jb, string strDatabaseName, string JobCommandString, string strRunEndDate)
        {
            //创建作业步骤 1，检查UTP是否启动
            string strSQLUTPBilling = this.strSQLUPTBilling(jb, JobCommandString);
            this.CreatestrBillingSteps(jb, strDatabaseName, strSQLUTPBilling);

            //创建作业步骤 2,启动UTP,开始执行Billing
            // string strSQLBilling = this.strSQLBilling(JobCommandString);
            //string strSQLBilling = this.strSQLBilling("\"C:\\Program Files (x86)\\Able Software\\Utility Star Platinum\\Utility.exe\"");
            //string strSQLBilling = this.strSQLBilling("insert into tblEmail (EmailFrom) values('33333Test@163.com')");
            //this.CreateStartBilling(jb, strDatabaseName, strSQLBilling);

            //创建作业步骤 3,发送Email
            // string strSQLEmail = this.strSQLEmail(jb);
            //this.CreateEmail(jb, strDatabaseName, strSQLEmail);


            //创建作业步骤 3,循环判断UTP进程是否启动
            //string strSQLForUTPStart = this.strSQLForUTPStart(jb);
            //this.CreateForUTPStar(jb, strDatabaseName, strSQLForUTPStart);

            //创建作业步骤 4,循环判断UTP进程是否退出
            //string strSQLForUTPEnd = this.strSQLForUTPEnd(jb, strRunEndDate);
            //this.CreateForUTPEnd(jb, strDatabaseName, strSQLForUTPEnd);
        }
        #endregion

        #region 返回步骤的SQL命令

        /// <summary>
        /// 返回SQL命令：(创建作业步骤 1，检查UTP是否启动)
        /// </summary>
        /// <returns></returns>
        public string strSQLUPTBilling(Job jb, string JobCommandString)
        {
            string strTemp = JobCommandString;
            string[] strTemps = strTemp.Split('e');
            JOB job = new JOB();
            string strDatabaseName = string.Empty;
            strDatabaseName = job.GetConfig("strDatabaseName") + ".dbo.";

            string strSQLUPTBilling = "exec  " + strDatabaseName + "[PrJobUTPBilling] '" + jb.JobID + "'," + "'" + JobCommandString + "'";
            return strSQLUPTBilling;
        }

        /// <summary>
        /// 返回SQL命令：(创建作业步骤 2,启动UTP,开始执行Billing)
        /// </summary>
        /// <returns></returns>
        public string strSQLBilling(string JobCommandString)
        {
            string strSQLBilling = "exec PrJobUTPBilling '" + JobCommandString + "'";
            //string strSQLBilling = JobCommandString;
            return strSQLBilling;
        }

        /// <summary>
        /// 返回SQL命令：(创建作业步骤 3,发送Email)
        /// </summary>
        /// <returns></returns>
        public string strSQLEmail(Job jb)
        {
            //string strSQLEmail = "exec PrJobEmail '" + jb.JobID+ "'";

            string strSQLEmail = "insert into tblEmail (EmailFrom) values('44444Test@163.com')";
            return strSQLEmail;
        }

        #endregion

        #region 创建步骤

        #region 创建步骤 1 ,检查UTP是否启动
        /// <summary>
        /// 创建步骤 1 ,检查UTP是否启动
        /// </summary>
        /// <param name="jb">Job 实例</param>
        /// <param name="databaseName">数据库名</param>
        /// <param name="strSQl">SQL命令</param>
        public void CreatestrBillingSteps(Job jb, string databaseName, string strSQl)
        {
            try
            {
                JobStep jbstp = new JobStep(jb, "UTPBilling");

                //数据库
                jbstp.DatabaseName = databaseName;

                //计划执行的SQL命令   
                jbstp.Command = strSQl;
                /*
                 * 制定执行步骤
                //成功时执行的操作
                jbstp.OnSuccessAction = StepCompletionAction.GoToStep;
                jbstp.OnSuccessStep = 2;

                //失败时执行的操作
                jbstp.OnFailAction = StepCompletionAction.GoToStep;
                jbstp.OnFailStep = 6;
                */
                //成功时执行的操作
                jbstp.OnSuccessAction = StepCompletionAction.QuitWithSuccess;

                //失败时执行的操作
                jbstp.OnFailAction = StepCompletionAction.QuitWithFailure;

                //创建 SQL 代理实例的作业步骤. 
                jbstp.Create();
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion

        #region 创建步骤 2 ,开始Billing
        /// <summary>
        /// 创建步骤 2 ,开始Billing
        /// </summary>
        /// <param name="jb">Job 实例</param>
        /// <param name="databaseName">数据库名</param>
        /// <param name="strSQl">SQL命令</param>
        public void CreateStartBilling(Job jb, string databaseName, string strSQl)
        {
            try
            {
                JobStep jbstp = new JobStep(jb, "开始Billing");

                //数据库
                jbstp.DatabaseName = databaseName;

                //计划执行的SQL命令   
                jbstp.Command = strSQl;

                //* 制定执行步骤
                //成功时执行的操作
                jbstp.OnSuccessAction = StepCompletionAction.QuitWithSuccess;
                //jbstp.OnSuccessAction = StepCompletionAction.GoToStep;
                //jbstp.OnSuccessStep = 3;

                //失败时执行的操作
                jbstp.OnFailAction = StepCompletionAction.QuitWithFailure;


                //创建 SQL 代理实例的作业步骤. 
                jbstp.Create();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 创建步骤 3 ,发送Email
        /// <summary>
        /// 创建步骤 2 ,发送Email
        /// </summary>
        /// <param name="jb">Job 实例</param>
        /// <param name="databaseName">数据库名</param>
        /// <param name="strSQl">SQL命令</param>
        public void CreateEmail(Job jb, string databaseName, string strSQl)
        {
            try
            {
                JobStep jbstp = new JobStep(jb, "发送Email");

                //数据库
                jbstp.DatabaseName = databaseName;

                //计划执行的SQL命令   
                jbstp.Command = strSQl;

                //* 制定执行步骤
                //成功时执行的操作
                //成功时执行的操作
                jbstp.OnSuccessAction = StepCompletionAction.QuitWithSuccess;

                //失败时执行的操作
                jbstp.OnFailAction = StepCompletionAction.QuitWithFailure;

                //创建 SQL 代理实例的作业步骤. 
                jbstp.Create();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion



        #endregion


        #region 创建步骤

        /// <summary>
        /// 创建步骤
        /// </summary>
        /// <param name="jb">JOB 对象</param>
        /// <param name="strDatabaseName">数据库名称</param>
        /// <param name="JobCommandString">要执行的命令行</param>
        /// <param name="strRunEndDate">运行结束时间</param>
        public void CreateStepKill(Job jb, string strDatabaseName, string sid)
        {
            //创建作业步骤 1，杀掉UTP进程
            string strSQLUTPPKill = this.strSQLUTPKill(sid);
            this.CreateUTPKill(jb, strDatabaseName, strSQLUTPPKill);

        }
        #endregion

        /// <summary>
        /// 返回SQL命令：(创建作业步骤 1，杀掉UTP进程)
        /// </summary>
        /// <returns></returns>
        public string strSQLUTPKill(string sid)
        {
            JOB job = new JOB();
            string strDatabaseName = string.Empty;
            strDatabaseName = job.GetConfig("strDatabaseName") + ".dbo.";
            string strSQLForUTPStart = "exec " + strDatabaseName + "PrJobUTPKill '" + sid + "'";
            return strSQLForUTPStart;
        }

        #region 创建步骤 1 ,杀掉UTP进程
        /// <summary>
        /// 创建步骤 1 ,杀掉UTP进程
        /// </summary>
        /// <param name="jb">Job 实例</param>
        /// <param name="databaseName">数据库名</param>
        /// <param name="strSQl">SQL命令</param>
        public void CreateUTPKill(Job jb, string databaseName, string strSQl)
        {
            try
            {
                JobStep jbstp = new JobStep(jb, "KillUTP");

                //数据库
                jbstp.DatabaseName = databaseName;

                //计划执行的SQL命令   
                jbstp.Command = strSQl;

                //成功时执行的操作
                jbstp.OnSuccessAction = StepCompletionAction.QuitWithSuccess;

                //失败时执行的操作
                jbstp.OnFailAction = StepCompletionAction.QuitWithFailure;

                //创建 SQL 代理实例的作业步骤. 
                jbstp.Create();
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion

    }
}
