using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HHSoft.FieldProtect.Framework.Utility;
using HHSoft.FieldProtect.DataAccess;
using HHSoft.FieldProtect.DataEntity;
using System.Collections;
using HHSoft.FieldProtect.DataEntity.ItemManage;
using HHSoft.FieldProtect.Business.Common;

namespace HHSoft.FieldProtect.Business.ItemManage
{
    public class BusiItemManage_SB
    {
        public BusiItemManage_SB() { }

        /// <summary>
        /// 根据环节获取项目数据
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public DataTable GetItemData(string cCode, string nodeId)
        {
            string shortCode = CommonHelper.GetShortCode(cCode);
            string strSql = string.Format(@"select a.*,b.xmlx,g.*,c.begindate,b.jsgm,b.jzgdmj,
               decode(d.nodeid, null, 0, 1) Kystate,
               decode(e.nodeid, null, 0, 1) Jgstate,
               decode(f.nodeid, null, 0, 1) Cystate,
               decode(h.nodeid, null, 0, 1) Ghstate,
               decode(i.nodeid, null, 0, 1) Ysstate
               from xm_xmxx a
               left join xm_sb_jbxx b on a.ItemCode = b.ItemCode
               left join xm_ky_jbxx g on a.ItemCode = g.ItemCode
               left join wf_instance c on a.itemcode = c.itemcode and a.flowid = c.flowid  and a.nodeid = c.nodeid and c.state = 0
               left join (select flowid, itemCode, nodeId from wf_instance where state = 1 and nodeid = {2} group by flowid, itemCode, nodeId) d
               on a.itemcode = d.itemcode and a.flowid = d.flowid
               left join (select flowid, itemCode, nodeId from wf_instance where state = 1 and nodeid = {3} group by flowid, itemCode, nodeId) e
               on a.itemcode = e.itemcode and a.flowid = e.flowid
               left join (select flowid, itemCode, nodeId from wf_instance where state = 1 and nodeid = {4} group by flowid, itemCode, nodeId) f
               on a.itemcode = f.itemcode and a.flowid = f.flowid
               left join (select flowid, itemCode, nodeId from wf_instance where state = 1 and nodeid = {5} group by flowid, itemCode, nodeId ) h
               on a.itemcode = h.itemcode and a.flowid = h.flowid
               left join (select flowid, itemCode, nodeId from wf_instance where state = 1 and nodeid = {6} group by flowid, itemCode, nodeId ) i
               on a.itemcode = i.itemcode and a.flowid = i.flowid
               where a.ccode like '{1}%' and a.nodeid in ({0}) 
               order by c.begindate desc, a.jlsj desc",
               nodeId, shortCode, ((int)WorkFlowNode.KYSH).ToString(), ((int)WorkFlowNode.ChuYan).ToString(), ((int)WorkFlowNode.ZhongYan).ToString(),
               ((int)WorkFlowNode.GHSJSH).ToString(), ((int)WorkFlowNode.YSSH).ToString());
            return OracleHelper.ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 获取申报项目数据
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public Xm_Sb_Jbxx GetItemInfo(string itemCode)
        {
            Xm_Sb_Jbxx ItemInfo = null;
            string strSql = "select * from xm_sb_jbxx a left join xm_xmzj b "
                       + " on a.itemcode = b.itemcode and b.stage = {1}"
                       + " where a.itemcode = '{0}'";
            strSql = string.Format(strSql, itemCode, ((int)ItemStage.ShenBo).ToString());
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            
            if (dt.Rows.Count == 1)
            {
                ItemInfo = (Xm_Sb_Jbxx)new DataTableOperation().ConvertFromDataRowToEntity(dt.Rows[0], typeof(Xm_Sb_Jbxx));
                ItemInfo.Xmzj = (Xm_Xmzj)new DataTableOperation().ConvertFromDataRowToEntity(dt.Rows[0], typeof(Xm_Xmzj));
                
                //ItemInfo.Zjze = dt.Rows[0]["Zjze"].ToString();
                //ItemInfo.Sbgzf = dt.Rows[0]["Sbgzf"].ToString();
                //ItemInfo.Tdpzgcf = dt.Rows[0]["Tdpzgcf"].ToString();
                //ItemInfo.Ntslgcf = dt.Rows[0]["Ntslgcf"].ToString();
                //ItemInfo.Dlgcf = dt.Rows[0]["Dlgcf"].ToString();
                //ItemInfo.Qtgcf = dt.Rows[0]["Qtgcf"].ToString();
                //ItemInfo.Bkyjf = dt.Rows[0]["Bkyjf"].ToString();
                //ItemInfo.Qtfy = dt.Rows[0]["Qtfy"].ToString();      
            }
            return ItemInfo;
        }

        /// <summary>
        /// 获取项目编号
        /// </summary>
        /// <param name="cCode"></param>
        /// <returns></returns>
        public string GetNodeCode(string cCode)
        {
            string strSql = "select nvl(max(ItemCode),{0}000) + 1 from xm_xmxx where ItemCode like '{0}%'";
            strSql = string.Format(strSql, cCode + System.DateTime.Now.ToString("yyyy"));
            return OracleHelper.ExecuteDataTable(strSql).Rows[0][0].ToString();
        }

        /// <summary>
        /// 更新申报时间
        /// </summary>
        /// <param name="itemCode"></param>
        public void UpdateItemInfo(string itemCode, string flowId)
        {
            string strSql = "update xm_xmxx set Sbsj = to_date('{1}','yyyy-mm-dd'),FlowId = '{2}' where itemcode = '{0}'";
            strSql = string.Format(strSql, itemCode, System.DateTime.Now.ToString("yyyy-MM-dd"), flowId);
            OracleHelper.ExecuteCommand(strSql);
        }
        /// <summary>
        /// 筛选退回的特殊处理
        /// </summary>
        /// <param name="itemCode"></param>
        public void UpdateItemStage(string itemCode)
        {
            string strSql = "update xm_xmxx set ItemStage = '',ItemState = {0},FlowId = '' where itemcode in ({1})";
            strSql = string.Format(strSql, ((int)ItemState.Beginning).ToString(), itemCode);
            OracleHelper.ExecuteCommand(strSql);
        }

        /// <summary>
        /// 保存筛选时间
        /// </summary>
        /// <param name="itemKy"></param>
        /// <returns></returns>
        public bool SaveItem_SBInfo(string itemCode, string sxdw, string sxsj)
        {
            string strSql = "update xm_sb_jbxx set Sxdw = '{1}', Sxsj = to_date('{2}','yyyy-mm-dd') where itemCode in ({0})";
            strSql = string.Format(strSql, itemCode, sxdw, sxsj);

            return OracleHelper.ExecuteCommand(strSql);
        }


        public bool SaveItem(Xm_Xmxx itemInfo, Xm_Sb_Jbxx itemSb,
            List<Item_File> itemFile, Xm_Xmzj itemZj, Xm_Gcxx itemGcxx)
        {
            ArrayList strSql = new ArrayList();
            string tmpSql = string.Empty;
            //// 项目主表
            strSql.Add(SqlBuilder.BuildDeleteSql<Xm_Xmxx>(CommonManage.delWhere(itemInfo.ItemCode)));
            strSql.Add(SqlBuilder.BuildInsertSql(itemInfo));
            ////项目申报信息
            strSql.Add(SqlBuilder.BuildDeleteSql<Xm_Sb_Jbxx>(CommonManage.delWhere(itemSb.ItemCode)));
            strSql.Add(SqlBuilder.BuildInsertSql(itemSb));
            ////文件
            new BusiItemManage().SaveItemFile(itemInfo.ItemCode, WorkFlowNode.TB, itemFile, ref strSql);
            ////资金
            new BusiItemManage().SaveItemMoney(itemInfo.ItemCode, WorkFlowNode.TB, itemZj, ref strSql);
            ////工程
            new BusiItemManage().SaveItemGcxx(itemInfo.ItemCode, itemGcxx, ref strSql);
            ////GIS信息

            return OracleHelper.ExecuteCommand(strSql);
        }

        public bool DeleteItem(string itemCode)
        {
            ArrayList strSql = new ArrayList();
            string tmpSql = string.Empty;
            tmpSql = "delete from xm_xmxx where ItemCode in ({0})";
            tmpSql = string.Format(tmpSql, itemCode);
            strSql.Add(tmpSql);

            tmpSql = "delete from xm_sb_jbxx where ItemCode in ({0})";
            tmpSql = string.Format(tmpSql, itemCode);
            strSql.Add(tmpSql);

            tmpSql = "delete from xm_xmzj where ItemCode in ({0})";
            tmpSql = string.Format(tmpSql, itemCode);
            strSql.Add(tmpSql);

            tmpSql = "delete from item_file where itemcode in ({0})";
            tmpSql = string.Format(tmpSql, itemCode);
            strSql.Add(tmpSql);

            tmpSql = "delete from xm_gcxx where itemcode in ({0})";
            tmpSql = string.Format(tmpSql, itemCode);
            strSql.Add(tmpSql);

            tmpSql = "delete from xm_xmdw where itemcode in ({0})";
            tmpSql = string.Format(tmpSql, itemCode);
            strSql.Add(tmpSql);

            tmpSql = "delete from wf_instance where itemcode in ({0})";
            tmpSql = string.Format(tmpSql, itemCode);
            strSql.Add(tmpSql);

            tmpSql = "delete from gis_data where itemcode in ({0})";
            tmpSql = string.Format(tmpSql, itemCode);
            strSql.Add(tmpSql);

            return OracleHelper.ExecuteCommand(strSql);
        }

        public DataTable QueryData(string itemCode)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("select a.* from xm_sb_jbxx a");
            sbSql.AppendLine("where a.itemcode = '{0}'");
            string strSql = string.Format(sbSql.ToString(), itemCode);
            return OracleHelper.ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 放入备选库
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public bool DelBx(string itemCode)
        {
            ArrayList strSql = new ArrayList();
            string tmpSql = string.Empty;

            tmpSql = "update xm_xmxx set ItemState = 0, wfState = 0, FlowId = '' where ItemCode = '" + itemCode + "'";            
            strSql.Add(tmpSql);

            tmpSql = "delete from wf_instance where Itemcode = '" + itemCode + "'";
            strSql.Add(tmpSql);

            return OracleHelper.ExecuteCommand(strSql);
        }
    }
}
