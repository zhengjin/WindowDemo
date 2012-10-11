using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.SysManage;
using HHSoft.FieldProtect.DataEntity.PersonalManage;
using System.Data.OracleClient;
using HHSoft.FieldProtect.DataAccess;
using System.Data;
using HHSoft.FieldProtect.DataEntity;
using System.Collections;
using HHSoft.FieldProtect.Business.Common;
using System.Configuration;
using System.IO;
using System.Web;


namespace HHSoft.FieldProtect.Business.PersonalManage
{
    public class MessageManage
    {
        /// <summary>
        /// 添加消息
        /// </summary>
        /// <param name="xtxx"></param>
        /// <returns></returns>
        public bool AddMessage(XTXX xtxx, List<XTXXJS> xtxxjs)
        {
            ArrayList sqls = new ArrayList();
            sqls.Add(SqlBuilder.BuildInsertSql(xtxx));
            foreach (var xtxxj in xtxxjs)
            {
                sqls.Add(SqlBuilder.BuildInsertSql(xtxxj));
            }
            return OracleHelper.ExecuteCommand(sqls);
        }

        /// <summary>
        /// 根据消息编号取得消息详细内容
        /// </summary>
        /// <param name="xxbh"></param>
        /// <returns></returns>
        public DataTable GetMessage(string xxbh)
        {
            string strSql = "select * from XTXX Where XXBH='{0}'";
            strSql = string.Format(strSql, xxbh);
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            return dt;
        }

        /// <summary>
        /// 删除消息。
        /// </summary>
        /// <param name="type"></param>
        /// <param name="xxbh"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool DelMessage(int type, string xxbh, string userId)
        {
            DataTableOperation dtOperation = new DataTableOperation();
            OracleDbOperation dbOperation = new OracleDbOperation(OracleHelper.ConnectionString);

            dbOperation.BeginTransaction();

            string tableName;
            string sql;
            List<OracleParameter> parameters = new List<OracleParameter>();

            tableName = "xtxx";
            sql = "select * from " + tableName + " a where a.xxbh = :xxbh";
            parameters.Clear();
            parameters.Add(new OracleParameter("xxbh", xxbh));
            DataTable dtXtxx = dbOperation.ExecuteDataTable(sql, tableName, parameters);
            if (dtXtxx.Rows.Count > 0)
            {
                DataRow drXtxx = dtXtxx.Rows[0];

                tableName = "xtxxjs";
                sql = "select * from " + tableName + " a where a.jsxxbh = :jsxxbh";
                parameters.Clear();
                parameters.Add(new OracleParameter("jsxxbh", xxbh));
                DataTable dtXtxxjs = dbOperation.ExecuteDataTable(sql, tableName, parameters);
                switch (type)
                {
                    case 0:
                        var drXtxxjs = (from DataRow dr in dtXtxxjs.Rows
                                        where dr["JSR"].ToString() == userId
                                        select dr).First();
                        drXtxxjs["jsrsc"] = "1";
                        break;
                    case 1:
                        drXtxx["fsrsc"] = "1";
                        break;
                }
                int xtxxjsUndeleteCount = (from DataRow dr in dtXtxxjs.Rows
                                           where dr["jsrsc"].ToString() == "0"
                                           select dr).Count();
                string xtxxDeleteState = drXtxx["fsrsc"].ToString();
                if (xtxxjsUndeleteCount == 0 && xtxxDeleteState == "1")
                {
                    dtOperation.ClearDbDataTable(dtXtxx);
                    dtOperation.ClearDbDataTable(dtXtxxjs);
                    DeleteMessageFile(xxbh, null);
                }
                dbOperation.UpdateDataTable(dtXtxx);
                dbOperation.UpdateDataTable(dtXtxxjs);
                dbOperation.Commit();
            }
            else
            {
                dbOperation.Rollback();
                return false;
            }
            return true;
        }

        public void DeleteMessageFile(string xxbh, Action action)
        {
            string path = HttpContext.Current.Server.MapPath(CommonManage.GetLocalMessagePath(xxbh));
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            if (action != null)
            {
                action();
            }
        }

        /// <summary>
        /// 将消息标识为已读
        /// </summary>
        /// <param name="xxbh"></param>
        /// <returns></returns>
        public void StateSet(string jsxxbh, string jsrId)
        {
            string strSql = "Update XTXXJS set YDZT=1 Where JSXXBH = '{0}' and JSR = '{1}'";
            strSql = string.Format(strSql, jsxxbh, jsrId);
            OracleHelper.ExecuteCommand(strSql);
        }

        /// <summary>
        /// 取得消息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public DataTable GetMessages(string condition, int pageIndex, int pageSize, out int recordCount)
        {
            string strSql = "select xxbh,min(fssj) fssj,min(XXBT) XXBT,min(FSR) FSR,wm_concat(JSR) JSR,min(YDZT) YDZT from XTXX a left join xtxxjs b on a.xxbh = b.jsxxbh where {0} 1 = 1 group by xxbh order by fssj DESC";
            strSql = string.Format(strSql, string.IsNullOrEmpty(condition) ? string.Empty : condition + " and ");

            #region 设置参数
            OracleParameter parameterPageSize = new OracleParameter("p_PageSize", OracleType.Number, 10);
            parameterPageSize.Value = pageSize;
            OracleParameter parameterPageIndex = new OracleParameter("p_PageIndex", OracleType.Number, 10);
            parameterPageIndex.Value = pageIndex;
            OracleParameter parameterSqlSelect = new OracleParameter("p_SqlSelect", OracleType.VarChar, 4000);
            parameterSqlSelect.Value = strSql;
            OracleParameter parameterRecordCount = new OracleParameter("p_OutRecordCount", OracleType.Number, 5);
            parameterRecordCount.Direction = ParameterDirection.Output;
            OracleParameter returnCursor = new OracleParameter("p_OutCursor", OracleType.Cursor);
            returnCursor.Direction = ParameterDirection.Output;

            OracleParameter[] oracleParameters = { parameterPageSize, parameterPageIndex, parameterSqlSelect, parameterRecordCount, returnCursor };
            #endregion

            DataTable dt = OracleHelper.ExecuteDataTable("CommonPackages.sp_Page", oracleParameters);
            recordCount = int.Parse(parameterRecordCount.Value.ToString());
            return dt;
        }
    }
}
