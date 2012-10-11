using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.ItemManage;
using System.Data;
using HHSoft.FieldProtect.Framework.Utility;
using System.Data.OracleClient;
using HHSoft.FieldProtect.DataAccess;
using HHSoft.FieldProtect.DataEntity;

namespace HHSoft.FieldProtect.Business.ItemMonitor
{
    /// <summary>
    /// 项目信息查询类
    /// </summary>    
    public class BusiItemMoitor_XMXX
    {
        /// <summary>
        ///获得所有项目信息
        /// </summary>
        /// <param name="xmxx">项目信息实体类</param>
        /// <param name="recordCount">记录总数</param>
        /// <returns></returns>
        public DataTable GetXmxxByXmxx(Xm_Xmxx xmxx, ref int recordCount)
        {
            string sql = "select t.* from XM_XMXX t {0} order by t.ItemCode ";

            //初始化查询条件
            string whereStr = "where t.ccode like '{0}%'";

            string shotCode = xmxx.Ccode;

            whereStr = string.Format(whereStr, shotCode);

            //添加项目年度条件
            if (!string.IsNullOrEmpty(xmxx.Xmnd)) whereStr += string.Format(" and t.xmnd='{0}'", xmxx.Xmnd);

            //添加资金年度条件
            if (!string.IsNullOrEmpty(xmxx.Zjnd)) whereStr += string.Format(" and t.zjnd='{0}'", xmxx.Zjnd);

            //添加项目阶段条件
            if (xmxx.ItemStage != 0) whereStr += string.Format(" and t.itemstage='{0}'", Convert.ToInt32(xmxx.ItemStage));

            sql = string.Format(sql, whereStr);

            #region 设置参数
            OracleParameter pageSize = new OracleParameter("p_PageSize", OracleType.Number, 10);
            pageSize.Value = xmxx.PageSize;
            OracleParameter pageIndex = new OracleParameter("p_PageIndex", OracleType.Number, 10);
            pageIndex.Value = xmxx.PageIndex;
            OracleParameter sqlSelect = new OracleParameter("p_SqlSelect", OracleType.VarChar, 4000);
            sqlSelect.Value = sql;
            OracleParameter recount = new OracleParameter("p_OutRecordCount", OracleType.Number, 5);
            recount.Direction = ParameterDirection.Output;
            OracleParameter returnCursor = new OracleParameter("p_OutCursor", OracleType.Cursor);
            returnCursor.Direction = ParameterDirection.Output;

            OracleParameter[] oracleParameters = { pageSize, pageIndex, sqlSelect, recount, returnCursor };
            #endregion

            DataTable dt = OracleHelper.ExecuteDataTable("CommonPackages.sp_Page", oracleParameters);

            recordCount = int.Parse(recount.Value.ToString());

            return dt;
        }

        /// <summary>
        /// 获取各个城市或区县各阶段项目数量
        /// </summary>
        /// <param name="shortCode">组织机构短编码</param>
        /// <param name="zjnd">资金年度</param>
        /// <returns>数据集</returns>
        public DataTable GetAllPhaseCount(string shortCode, string xmnd, string zjnd)
        {
            OracleParameter oraShortcCode = new OracleParameter("ShortcCode", OracleType.VarChar, 10);
            oraShortcCode.Value = shortCode;
            OracleParameter oraZjnd = new OracleParameter("Zjnd", OracleType.VarChar, 10);
            oraZjnd.Value = zjnd;
            OracleParameter oraXmnd = new OracleParameter("Xmnd", OracleType.VarChar, 10);
            oraXmnd.Value = xmnd;
            OracleParameter returnCursor = new OracleParameter("ReturnCursor", OracleType.Cursor);
            returnCursor.Direction = ParameterDirection.Output;

            OracleParameter[] oracleParameters = { oraShortcCode, oraZjnd, oraXmnd, returnCursor };
            
            return OracleHelper.ExecuteDataTable("QueryPackages.ItemStageQuery", oracleParameters);
        }

        /// <summary>
        /// 取全省或全市各城市或区县资金使用情况
        /// </summary>
        /// <param name="shortCode"></param>
        /// <param name="zjnd"></param>
        /// <returns></returns>
        public DataTable GetAllCompanyZjQk(string shortCode, string xmnd, string zjnd)
        {
            OracleParameter oraShortcCode = new OracleParameter("shortccode", OracleType.VarChar, 10);
            oraShortcCode.Value = shortCode;

            OracleParameter oraZjnd = new OracleParameter("zjnd", OracleType.VarChar, 10);
            oraZjnd.Value = zjnd;

            OracleParameter oraXmnd = new OracleParameter("xmnd", OracleType.VarChar, 10);
            oraXmnd.Value = xmnd;
                     
            OracleParameter returnCursor = new OracleParameter("returncursor", OracleType.Cursor);
            returnCursor.Direction = ParameterDirection.Output;

            OracleParameter[] oracleParameters = { oraShortcCode, oraZjnd, oraXmnd, returnCursor };

            return OracleHelper.ExecuteDataTable("QueryPackages.ItemZjQuery", oracleParameters);
        }

        /// <summary>
        /// 项目工程信息统计
        /// </summary>
        /// <param name="shortCode"></param>
        /// <param name="queryitem"></param>
        /// <param name="xmnd"></param>
        /// <param name="zjnd"></param>
        /// <returns></returns>
        public DataTable GetXmGcxx(string shortCode,string queryitem, string stage, string xmnd, string zjnd)
        {
            OracleParameter oraShortcCode = new OracleParameter("shortccode", OracleType.VarChar, 10);
            oraShortcCode.Value = shortCode;

            OracleParameter oraItem = new OracleParameter("QueryItem", OracleType.VarChar, 2000);
            oraItem.Value = queryitem;

            OracleParameter oraStage = new OracleParameter("Stage", OracleType.VarChar, 30);
            oraStage.Value = stage;

            OracleParameter oraZjnd = new OracleParameter("zjnd", OracleType.VarChar, 30);
            oraZjnd.Value = zjnd;

            OracleParameter oraXmnd = new OracleParameter("xmnd", OracleType.VarChar, 30);
            oraXmnd.Value = xmnd;

            OracleParameter returnCursor = new OracleParameter("returncursor", OracleType.Cursor);
            returnCursor.Direction = ParameterDirection.Output;

            OracleParameter[] oracleParameters = { oraShortcCode, oraItem, oraStage, oraZjnd, oraXmnd, returnCursor };

            return OracleHelper.ExecuteDataTable("QueryPackages.ItemGcxxQuery", oracleParameters);
 
        }

        /// <summary>
        /// 所有本年度项目资金使用情况
        /// </summary>
        /// <param name="xmxx">项目信息实体类</param>
        /// <param name="recordCount">总记录数</param>
        /// <returns>数据集</returns>
        public DataTable GetXmZjQk(Xm_Xmxx xmxx, ref int recordCount)
        {
            StringBuilder sbSql = new StringBuilder();

            //劉宏哲修改。
            sbSql.AppendLine(@"select *
                                  from xm_xmxx x
                                  left join (select z.itemcode zitemcode,z.zjze
                                               from xm_xmzj z
                                              where z.rowid in
                                                    (select max(z.rowid) from xm_xmzj z group by z.itemcode)) z
                                    on x.itemcode = z.zitemcode
                                  left join (select b.itemcode bitemcode, sum(b.bfje) bfje
                                               from xm_ss_zjbf b
                                              group by b.itemcode) b
                                    on x.itemcode = b.bitemcode
                                  left join company c
                                    on x.ccode = c.ccode
                                 where x.itemstate != 0
                                   and x.wfstate != 2
                                   and x.itemcode like '{0}%'");
            if (!string.IsNullOrEmpty(xmxx.Xmnd))
            {
                sbSql.AppendLine("and x.xmnd = '{1}'");
            }
            if (!string.IsNullOrEmpty(xmxx.Zjnd))
            {
                sbSql.AppendLine("and x.zjnd = '{2}'");
            }
            sbSql.AppendLine("order by x.itemcode");

            //            sbSql.AppendLine(@"with a as
            //                                         (select j.itemcode, max(j.stage) stage
            //                                            from XM_XMZJ j, xm_xmxx x
            //                                           where j.itemcode = x.itemcode
            //                                             and x.zjpfsj is not null
            //                                             and substr(j.itemcode, 0, 6) like '{0}%'
            //                                             and x.itemstate!=0 and x.wfstate!=2 and x.wfstate!=3");
            //            if (!string.IsNullOrEmpty(xmxx.Xmnd))
            //            {
            //                sbSql.AppendLine("and x.xmnd = '{2}'");
            //            }
            //            if (!string.IsNullOrEmpty(xmxx.Zjnd))
            //            {
            //                sbSql.AppendLine("and x.zjnd = '{3}'");
            //            }
            //            sbSql.AppendLine(@"group by j.itemcode),
            //                                        b as
            //                                         (select j.itemcode, max(j.stage) stage, max(j.xh) xh
            //                                            from xm_xmzj j, A
            //                                           where j.itemcode = A.itemcode
            //                                             and j.stage = A.stage
            //                                           group by j.itemcode)
            //
            //                                        select  x.itemcode,x.itemname,x.ccode,x.lxsj,x.itemstage,j.zjze,sum(bf.bfje) bfje,c.cname
            //                                          from xm_xmzj j, b, a, xm_xmxx x,xm_ss_zjbf bf,company c
            //                                         where j.stage = a.stage
            //                                           and j.xh = b.xh
            //                                           and A.itemcode = j.itemcode
            //                                           and j.itemcode = b.itemcode
            //                                           and j.itemcode = x.itemcode
            //                                           and j.itemcode=bf.itemcode(+)
            //                                            and substr(x.ccode,0,{1})=c.shortccode
            //                                            group by x.itemcode,x.itemname,x.ccode,x.lxsj,j.zjze,c.cname,x.itemstage");
            string sql = string.Format(sbSql.ToString(), xmxx.Ccode, xmxx.Xmnd, xmxx.Zjnd);
            #region 设置参数
            OracleParameter pageSize = new OracleParameter("p_PageSize", OracleType.Number, 10);
            pageSize.Value = xmxx.PageSize;
            OracleParameter pageIndex = new OracleParameter("p_PageIndex", OracleType.Number, 10);
            pageIndex.Value = xmxx.PageIndex;
            OracleParameter sqlSelect = new OracleParameter("p_SqlSelect", OracleType.VarChar, 4000);
            sqlSelect.Value = sql;
            OracleParameter recount = new OracleParameter("p_OutRecordCount", OracleType.Number, 5);
            recount.Direction = ParameterDirection.Output;
            OracleParameter returnCursor = new OracleParameter("p_OutCursor", OracleType.Cursor);
            returnCursor.Direction = ParameterDirection.Output;

            OracleParameter[] oracleParameters = { pageSize, pageIndex, sqlSelect, recount, returnCursor };
            #endregion

            DataTable dt = OracleHelper.ExecuteDataTable("CommonPackages.sp_Page", oracleParameters);

            recordCount = int.Parse(recount.Value.ToString());

            return dt;
        }

       
        /// <summary>
        /// 根据项目编号获得项目信息
        /// </summary>
        /// <param name="itemcode">项目编号</param>
        /// <returns>项目信息实体类</returns>
        public Xm_Xmxx GetXmxxByItemCode(string itemcode)
        {
            string sql = string.Format("select * from xm_xmxx x where x.itemcode='{0}'", itemcode);

            DataTable dt = OracleHelper.ExecuteDataTable(sql);
            Xm_Xmxx xmxx = new Xm_Xmxx();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];

                xmxx.WfState = (WfState)Convert.ToInt32(dr["wfstate"]);
            }

            return xmxx;
        }

        public List<string> GetXmnds(out string defaultXmnd)
        {
            string sql = "select distinct(t.xmnd) from xm_xmxx t order by t.xmnd desc";
            DataTable dt = OracleHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count == 0)
            {
                defaultXmnd = string.Empty;
            }
            else
            {
                defaultXmnd = dt.Rows[0][0].ToString();
            }
            return (from DataRow dr in dt.Rows select dr["XMND"].ToString()).ToList();
        }
    }
}
