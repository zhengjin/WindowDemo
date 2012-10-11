using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HHSoft.FieldProtect.DataEntity.SysManage;
using System.Data.OracleClient;
using HHSoft.FieldProtect.DataAccess;
using HHSoft.FieldProtect.DataEntity.ItemManage;

namespace HHSoft.FieldProtect.Business.ItemManage
{
    public class BusiItemQuery
    {
        public BusiItemQuery() { }

        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <param name="query"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public DataTable QueryItemInfo(Xm_Query query, out int recordCount)
        {

            #region 设置参数

            OracleParameter allowPager = new OracleParameter("IsPager", OracleType.Number, 10);
            allowPager.Value = query.IsPager;
            OracleParameter sortExpression = new OracleParameter("SortExpression", OracleType.VarChar, 100);
            sortExpression.Value = query.SortExpression;
            OracleParameter sortDirection = new OracleParameter("SortDirection", OracleType.VarChar, 100);
            sortDirection.Value = query.SortDirection;
            OracleParameter ShortCode = new OracleParameter("ShortCode", OracleType.VarChar, 100);
            ShortCode.Value = string.IsNullOrEmpty(query.CCode) ? string.Empty : query.CCode;

            OracleParameter ItemCode = new OracleParameter("ItemCode", OracleType.VarChar, 1000);
            ItemCode.Value = string.IsNullOrEmpty(query.ItemCode) ? string.Empty : query.ItemCode;

            OracleParameter ItemName = new OracleParameter("ItemName", OracleType.VarChar, 100);
            ItemName.Value = string.IsNullOrEmpty(query.ItemName) ? string.Empty : query.ItemName;

            OracleParameter ItemStage = new OracleParameter("ItemStage", OracleType.VarChar, 100);
            ItemStage.Value = string.IsNullOrEmpty(query.Stage) ? string.Empty : query.Stage;

            OracleParameter StageType = new OracleParameter("StageType", OracleType.Number, 10);
            StageType.Value = query.StageType;

            OracleParameter ItemState = new OracleParameter("ItemState", OracleType.VarChar, 100);
            ItemState.Value = string.IsNullOrEmpty(query.ItemState) ? string.Empty : query.ItemState;

            OracleParameter WfState = new OracleParameter("WfState", OracleType.VarChar, 100);
            WfState.Value = string.IsNullOrEmpty(query.WfState) ? string.Empty : query.WfState;

            OracleParameter pageSize = new OracleParameter("PageSize", OracleType.Number, 10);
            pageSize.Value = query.PageSize;
            OracleParameter pageIndex = new OracleParameter("PageIndex", OracleType.Number, 10);
            pageIndex.Value = query.PageIndex;
            OracleParameter recount = new OracleParameter("RecordCount", OracleType.Number, 5);
            recount.Direction = ParameterDirection.Output;
            OracleParameter returnCursor = new OracleParameter("ReturnCursor", OracleType.Cursor);
            returnCursor.Direction = ParameterDirection.Output;

            OracleParameter[] oracleParameters = { allowPager, sortExpression, sortDirection, ShortCode, ItemCode, ItemName, StageType, ItemStage, ItemState, WfState, pageSize, pageIndex, recount, returnCursor };
  
            #endregion

            DataTable dt = OracleHelper.ExecuteDataTable("QueryPackages.ItemInfoQuery", oracleParameters);
            if (query.IsPager == 1)
            {
                recordCount = int.Parse(recount.Value.ToString());                
            }
            else
            {
                recordCount = 0;
            }
            return dt;
        }       

        public DataTable QueryItemInfo(List<string> itemCodes, int index, int size, out int recordCount)
        {
            string strWhere = " where a.itemcode in (" + string.Join(",", (from record in itemCodes select "'" + record + "'").ToArray()) + ")";
            string strSql = "select * from xm_xmxx a left join Company b on a.CCode = b.CCode " + strWhere;

            #region 设置参数
            OracleParameter pageSize = new OracleParameter("p_PageSize", OracleType.Number, 10);
            pageSize.Value = size;
            OracleParameter pageIndex = new OracleParameter("p_PageIndex", OracleType.Number, 10);
            pageIndex.Value = index;
            OracleParameter sqlSelect = new OracleParameter("p_SqlSelect", OracleType.VarChar, 4000);
            sqlSelect.Value = strSql;
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
        /// 查询项目明细(工程信息统计)
        /// </summary>
        /// <param name="query"></param>
        /// <param name="itemCodes"></param>
        /// <returns></returns>
        public DataTable QueryItemInfo(Xm_Query query)
        {
            string strSql = "select a.*,b.{0} from xm_xmxx a left join v_xm_gcxx b on a.itemcode = b.itemcode"
                + " where a.itemcode in ({1}) and b.{0} is not null order by a.{2} {3}";
            strSql = string.Format(strSql, query.QueryItem, query.ItemCode, query.SortExpression, query.SortDirection);
            return OracleHelper.ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 返回用户可以收回的项目
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public DataTable GetItemUndoData(string userId)
        {
            string strSql = "select * from xm_xmxx tab0 inner join (select a.flowid, a.itemcode, a.enddate"
                      + " from (select flowid,itemCode,max(orderno) orderno, max(enddate) enddate from wf_instance"
                      + " where state = 1 and userId = {0} group by flowid, itemCode) a"
                      + " left join (select flowid, itemCode, max(orderno) orderno from wf_instance"
                      + " where state = 0 group by flowid, itemCode) b "
                      + " on a.flowid = b.flowid and a.itemcode = b.itemcode"
                      + " where a.orderno + 1 = b.orderno) tab1 on tab0.flowid = tab1.flowid and tab0.itemcode = tab1.itemcode"
                      + " where tab0.wfstate = 0 and itemState = 1 order by tab1.enddate desc";
            strSql = string.Format(strSql, userId);
            return OracleHelper.ExecuteDataTable(strSql);
        }
    }

}
