using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ServiceDirect.Membership;

using ServiceDirect.Schedule.Billing.JOB;
using System.Data.Objects;
using ServiceDirect.Schedule.DAL;

namespace ServiceDirect.Schedule.WEB
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UTPUserAuthenticate();       
        }

        #region UTP用户验证函数
        /// <summary>
        /// UTP用户验证函数
        /// </summary>
        public void UTPUserAuthenticate()
        {
            bool bln = true;
            //UTP登录用户ID
            string struid = string.Empty;

            //UTP中登录用户名+登录用户口令的MD5值，身份验证
            string strcid = string.Empty;

            //指定启动SD下的模块ID，指定功能
            string strmid = string.Empty;

            //测试变量
            string strDebug = string.Empty;

            #region 获取页面传值
            if (Request.QueryString["uid"] != null && Request.QueryString["uid"] != string.Empty)
            {
                struid = Request.QueryString["uid"].ToString();
            }
            if (Request.QueryString["cid"] != null && Request.QueryString["cid"] != string.Empty)
            {
                strcid = Request.QueryString["cid"].ToString();
            }
            if (Request.QueryString["mid"] != null && Request.QueryString["mid"] != string.Empty)
            {
                strmid = Request.QueryString["mid"].ToString();
            }
            if (Request.QueryString["Debug"] != null && Request.QueryString["Debug"] != string.Empty)
            {
                strDebug = Request.QueryString["Debug"].ToString();
            }
            #endregion
            
            //是否是测试状态
            if (strDebug.Equals("true"))
            {
                return;
            }
            else if (struid != string.Empty && strcid != string.Empty && strmid != string.Empty)
            {
                UTPUser utpuser = new UTPUser();

                //调用UTP用户验证函数
                bln = utpuser.UTPUserAuthenticate(struid, strcid, strmid);
                if (bln)
                {

                    //验证用户有效跳转到相应的页面
                    Response.Redirect("Schedule Billing/ScheduleTasksForm.aspx");
                }
                else
                {
                    Response.Redirect("NoPermissionForm.aspx");
                }
            }
            else
            {

                //页面没有接到任何值跳转到错误提示页面
                Response.Redirect("NoPermissionForm.aspx");
            }
        }

        #endregion

        protected void LabVersion_Load(object sender, EventArgs e)
        {
            LabVersion.Text = "Beta1 Ver. " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() +
                                " <br> <br> Release Time: 2011-03-4 8:09:00";
        }
    }
}