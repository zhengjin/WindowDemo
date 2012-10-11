using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;

namespace ServiceDirect.Schedule.Billing.JOB
{
    /// <summary>
    /// 创建日期：2011-02-14。
    /// 创建人：闫保振。
    /// 描述：SQL Agent操作类。
    /// </summary>
    public class AgentService
    {
        public AgentService()
        { }

        #region 启动Agent服务
        /// <summary>
        /// 启动Agent服务,返回一个 int值，如果大于0成功，否则失败
        /// </summary>
        /// <returns></returns>
        public int StartServices()
        {
            int count = 0;
            SqlConnection con=null;
            try
            {
                //获取Web.config数据库连接
                string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                con = new SqlConnection(ConnectionString);
                //打开
                con.Open();
                //启动Agent
                string strSQL = "exec   master..xp_cmdshell 'net start SQLServerAgent '";
                SqlCommand cmd = new SqlCommand(strSQL, con);
                //执行命令
                count = cmd.ExecuteNonQuery();
                return count;
            }
            catch (Exception)
            {
                return count;
            }
            finally
            {
                //关闭
                con.Close();
            }
        }
        #endregion

    }
}
