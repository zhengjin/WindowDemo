using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.ItemManage;
using System.Collections;
using System.Data;
using HHSoft.FieldProtect.Framework.Utility;
using HHSoft.FieldProtect.DataAccess;
using HHSoft.FieldProtect.DataEntity;
using HHSoft.FieldProtect.Business.Common;

namespace HHSoft.FieldProtect.Business.ItemManage
{
    public class BusiItemManage_GHYS
    {
        public BusiItemManage_GHYS() { }


        /// <summary>
        /// 根据环节获取项目数据
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public DataTable GetItemData(string cCode, string nodeId)
        {
            string shortCode = CommonHelper.GetShortCode(cCode);
            string strSql = string.Empty;

            strSql = "select a.*, b.address, b.jsgm, b.tzgs, b.mjtz, d.nodeid ghNode, e.nodeid ysNode from xm_xmxx a"
            + " left join xm_ky_jbxx b on a.itemcode = b.itemcode"
            + " left join wf_instance c"
            + " on a.itemcode = c.itemcode and a.nodeid = c.nodeid and c.state = 0"
            + " left join (select flowid, itemCode, nodeId from wf_instance where state = 1 and nodeid = {2} group by flowid, itemCode, nodeId ) d"
            + " on a.itemcode = d.itemcode and a.flowid = d.flowid"
            + " left join (select flowid, itemCode, nodeId from wf_instance where state = 1 and nodeid = {3} group by flowid, itemCode, nodeId ) e"
            + " on a.itemcode = e.itemcode and a.flowid = e.flowid"
            + " where a.ccode like '{1}%' and a.nodeid in ({0})"
            + " order by c.begindate desc";
            strSql = string.Format(strSql, nodeId, shortCode,
                ((int)WorkFlowNode.GHSJSH).ToString(), ((int)WorkFlowNode.YSSH).ToString());
            return OracleHelper.ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 根据环节获取项目数据
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public DataTable GetItemInfo(string cCode, string nodeId)
        {
            string shortCode = CommonHelper.GetShortCode(cCode);
            string strSql = string.Empty;

            strSql = " select * from xm_xmxx a left join xm_ghsjysxx b on a.ItemCode = b.ItemCode"
                   + " left join xm_gcxx c on a.itemcode = c.itemcode and c.stage = {2}"
                   + " left join wf_instance d on a.itemcode = d.itemcode and a.nodeid = d.nodeid and d.state = 0"
                   + " where a.ccode like '{1}%' and a.nodeid in ({0})";
            strSql = string.Format(strSql, nodeId, shortCode, ((int)ItemStage.GuiHua).ToString());
            return OracleHelper.ExecuteDataTable(strSql);
        }


        /// <summary>
        /// 获取规划设计信息
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public Xm_GhsjYsxx GetItemInfo(string itemCode)
        {
            Xm_GhsjYsxx ItemInfo = null;
            string strSql = "select * from Xm_GhsjYsxx a "
               + " left join xm_xmzj b on a.itemcode = b.itemcode and b.stage = {1}"
               + " left join xm_xmdw c on a.itemcode = c.itemcode and c.type = {2}"
               + " where a.itemcode = '{0}'";
            strSql = string.Format(strSql, itemCode, ((int)ItemStage.GuiHua).ToString(), ((int)ItemCompanyType.GH).ToString());
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count > 0)
            {
                ItemInfo = (Xm_GhsjYsxx)new DataTableOperation().ConvertFromDataRowToEntity(dt.Rows[0], typeof(Xm_GhsjYsxx));
                ItemInfo.Xmzj = (Xm_Xmzj)new DataTableOperation().ConvertFromDataRowToEntity(dt.Rows[0], typeof(Xm_Xmzj));
                ItemInfo.GhDw = (Xm_Xmdw)new DataTableOperation().ConvertFromDataRowToEntity(dt.Rows[0], typeof(Xm_Xmdw));
            }
            return ItemInfo;
        }

        /// <summary>
        /// 保存规划设计信息
        /// </summary>
        /// <param name="itemGHYS"></param>
        /// <param name="strSql"></param>
        public void SaveItem_GHSJ(Xm_GhsjYsxx itemGHYS, ref ArrayList strSql)
        {
            string tmpSql = string.Empty;

            tmpSql = "delete from xm_ghsjysxx where itemcode = '{0}'";
            tmpSql = string.Format(tmpSql, itemGHYS.ItemCode);
            strSql.Add(tmpSql);
        }               
 

        /// <summary>
        /// 更新规划设计审核信息
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <returns></returns>
        public bool UpdateGhSH(Xm_GhsjYsxx itemInfo)
        {
            string strSql = "update xm_ghsjysxx set GHPFDW = '{2}', GHPFSJ = to_date('{1}','yyyy-mm-dd') where itemCode in ({0})";
            strSql = string.Format(strSql, itemInfo.ItemCode, itemInfo.Ghpfsj.Value.ToString("yyyy-MM-dd"), itemInfo.Ghpfdw);
            return OracleHelper.ExecuteCommand(strSql);
        }
        /// <summary>
        /// 更新预算审核信息
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <returns></returns>
        public bool UpdateYsSH(Xm_GhsjYsxx itemInfo, bool updateMain)
        {
            ArrayList strSql = new ArrayList();
            string tmpSql = string.Empty;
            tmpSql = "update xm_ghsjysxx set YSPFDW = '{3}', YSPFSJ = to_date('{1}','yyyy-mm-dd'), YSXDWH = '{2}', SFTZ = {4} where itemCode in ({0})";
            tmpSql = string.Format(tmpSql, itemInfo.ItemCode, itemInfo.Yspfsj.Value.ToString("yyyy-MM-dd"), itemInfo.Ysxdwh, itemInfo.Yspfdw, itemInfo.Sftz.ToString());
            strSql.Add(tmpSql);
            if (updateMain)
            {
                tmpSql = "update xm_xmxx set ZJPFSJ = to_date('{1}','yyyy-mm-dd'), ZJPFWH = '{2}' where itemCode in ({0})";
                tmpSql = string.Format(tmpSql, itemInfo.ItemCode, itemInfo.Yspfsj.Value.ToString("yyyy-MM-dd"), itemInfo.Ysxdwh);
                strSql.Add(tmpSql);
            }
            return OracleHelper.ExecuteCommand(strSql);
        }

        public void InsertYs(string itemCode)
        {
            string[] strSql = new string[2];
            strSql[0] = "delete from xm_xmzj where itemCode in ({0}) and stage = {1}";
            strSql[0] = string.Format(strSql[0], itemCode, ((int)ItemStage.YuSuan).ToString());
            strSql[1] = "insert into xm_xmzj "
                + " select itemcode, {2}, {3}, xh, zjze, sbgzf, tdpzgcf, ntslgcf, dlgcf, qtgcf, bkyjf, qtfy, "
                + " fee1, fee2, fee3, fee4, fee5, fee6, fee7, fee8 from xm_xmzj where itemCode in ({0}) and stage = {1}";
            strSql[1] = string.Format(strSql[1], itemCode, ((int)ItemStage.GuiHua).ToString(),
                ((int)ItemStage.YuSuan).ToString(), ((int)WorkFlowNode.YSTZ).ToString());
            OracleHelper.ExecuteCommand(strSql);
        }

        public DataTable QueryData(string itemCode)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("select a.* from xm_ghsjysxx a");
            sbSql.AppendLine("where a.itemcode = '{0}'");
            string strSql = string.Format(sbSql.ToString(), itemCode);
            return OracleHelper.ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 获取预算审批单位
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string GetYsDw(string itemCode)
        {
            string dwName = string.Empty;
            string strSql = "select YsPfdw from xm_ghsjysxx where ItemCode = '{0}'";
            strSql = string.Format(strSql, itemCode);
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count == 1)
            {
                dwName = dt.Rows[0][0].ToString();
            }
            return dwName;
        }

        public bool SaveItem(Xm_GhsjYsxx itemGhys,List<Item_File> itemFile, 
            Xm_Xmzj itemZj, Xm_Gcxx itemGcxx, Xm_Xmdw itemDw)
        {
            ArrayList strSql = new ArrayList();
            string tmpSql = string.Empty;

            ////项目规划设计信息
            strSql.Add(SqlBuilder.BuildDeleteSql<Xm_GhsjYsxx>(CommonManage.delWhere(itemGhys.ItemCode)));
            strSql.Add(SqlBuilder.BuildInsertSql(itemGhys));
            ////文件
            new BusiItemManage().SaveItemFile(itemGhys.ItemCode, WorkFlowNode.GHSJYS, itemFile, ref strSql);
            ////资金
            new BusiItemManage().SaveItemMoney(itemGhys.ItemCode, WorkFlowNode.GHSJYS, itemZj, ref strSql);
            ////工程
            new BusiItemManage().SaveItemGcxx(itemGhys.ItemCode, itemGcxx, ref strSql);
            ////单位
            new BusiItemManage().SaveItemCompany(itemGhys.ItemCode, ItemCompanyType.GH, itemDw, ref strSql);            

            return OracleHelper.ExecuteCommand(strSql);
        }
    }
}
