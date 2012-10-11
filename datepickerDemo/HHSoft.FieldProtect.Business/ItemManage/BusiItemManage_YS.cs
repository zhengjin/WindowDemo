using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.ItemManage;
using System.Collections;
using HHSoft.FieldProtect.Business.Common;
using HHSoft.FieldProtect.DataAccess;
using HHSoft.FieldProtect.DataEntity;
using System.Data;
using HHSoft.FieldProtect.Framework.Utility;

namespace HHSoft.FieldProtect.Business.ItemManage
{
    public class BusiItemManage_YS
    {
        public DataTable QueryYsxx(string itemCode)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("select a.* from xm_ysxx a");
            sbSql.AppendLine("where a.itemcode = '{0}'");
            string strSql = string.Format(sbSql.ToString(), itemCode);
            return OracleHelper.ExecuteDataTable(strSql);
        }

        public string QueryYsReturnMessage(string itemCode, string flowId)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("select a.* from wf_instance a");
            sbSql.AppendLine("where a.itemcode = '{0}'");
            sbSql.AppendLine("and a.flowid = '{1}'");
            sbSql.AppendLine("order by a.orderno desc");
            string strSql = string.Format(sbSql.ToString(), itemCode, flowId);
            DataTable dtInstance = OracleHelper.ExecuteDataTable(strSql);
            if (dtInstance.Rows.Count >= 2)
            {
                DataRow drInstance = dtInstance.Rows[1];
                if (drInstance["RESULT"].ToString() == "2")
                {
                    StringBuilder sbReturnMessage = new StringBuilder();
                    DataTable dtYsxx = QueryYsxx(itemCode);
                    if (dtYsxx.Rows.Count > 0)
                    {
                        DataRow drYsxx = dtYsxx.Rows[0];
                        string returnDepartment = null;
                        string returnTime = null;
                        int nodeId = int.Parse(drInstance["NODEID"].ToString());
                        if (nodeId == (int)WorkFlowNode.ChuYan)
                        {
                            returnDepartment = drYsxx["CYDW"].ToString();
                            returnTime = drYsxx["CYSJ"].ToString();
                        }
                        else if (nodeId == (int)WorkFlowNode.ZhongYan)
                        {
                            returnDepartment = drYsxx["ZYDW"].ToString();
                            returnTime = drYsxx["ZYSJ"].ToString();
                        }
                        sbReturnMessage.Append("退回单位：" + returnDepartment + "\\r\\n");
                        sbReturnMessage.Append("退回时间：" + returnTime + "\\r\\n");
                        sbReturnMessage.Append("退回意见：" + drInstance["RESULTDESC"].ToString());
                        return sbReturnMessage.ToString();
                    }
                }
            }
            return null;
        }        

        public bool UpdateGcxx(ActionEnum action, Xm_Gcxx gcxx)
        {
            ArrayList sqls = new ArrayList();

            switch (action)
            {
                case ActionEnum.Insert:
                    sqls.Add(SqlBuilder.BuildInsertSql(gcxx));
                    break;
                case ActionEnum.Update:
                    sqls.Add(SqlBuilder.BuildUpdateSql(gcxx));
                    break;
            }

            return OracleHelper.ExecuteCommand(sqls);
        }

        public void UpdateYsxx(Xm_Ysxx ysxx, WorkFlowNode node, ref ArrayList strSql)
        {
            string tmpSql = string.Empty;
            if (node == WorkFlowNode.JunGong)
            {
                tmpSql = "delete from xm_ysxx where Itemcode = {0}";
                tmpSql = string.Format(tmpSql, ysxx.ITEMCODE);
                strSql.Add(tmpSql);
                tmpSql = "Insert Into xm_ysxx (itemCode,Yssqsj,Yssqdw) values ({0},to_date('{1}','yyyy-mm-dd'),'{2}')";
                tmpSql = string.Format(tmpSql, ysxx.ITEMCODE, ysxx.YSSQSJ.Value.ToString("yyyy-MM-dd"), ysxx.YSSQDW);
                strSql.Add(tmpSql);
            }
            if (node == WorkFlowNode.ChuYan)
            {
                tmpSql = "Update xm_ysxx set cydw = '{1}', cysj = to_date('{2}','yyyy-mm-dd') Where ItemCode = {0}";
                tmpSql = string.Format(tmpSql, ysxx.ITEMCODE, ysxx.CYDW, ysxx.CYSJ.Value.ToString("yyyy-MM-dd"));
                strSql.Add(tmpSql);
            }

            if (node == WorkFlowNode.ZhongYan)
            {
                tmpSql = "Update xm_ysxx set zydw = '{1}', zysj = to_date('{2}','yyyy-mm-dd'),yswh = '{3}',jsfhsj = to_date('{4}','yyyy-mm-dd') Where ItemCode = {0}";
                tmpSql = string.Format(tmpSql, ysxx.ITEMCODE, ysxx.ZYDW, ysxx.ZYSJ.Value.ToString("yyyy-MM-dd"), ysxx.YSWH, ysxx.JSFHSJ.Value.ToString("yyyy-MM-dd"));
                strSql.Add(tmpSql);

                tmpSql = "update xm_xmxx set YSSJ = to_date('{1}','yyyy-mm-dd') where ITEMCODE = '{0}'";
                tmpSql = string.Format(tmpSql, ysxx.ITEMCODE, ysxx.ZYSJ.Value.ToString("yyyy-MM-dd"));
                strSql.Add(tmpSql);
            }
        }

        public bool UpdateYsxx(Xm_Ysxx ysxx, Xm_Xmdw xmdw, List<Item_File> fileInfos, WorkFlowNode ysType)
        {
            ArrayList sqls = new ArrayList();
            Dictionary<string, string> deleteCondition = new Dictionary<string, string>();
            string tmpSql;

            sqls.Add(SqlBuilder.BuildUpdateSql(ysxx));

            tmpSql = "update xm_xmxx set YSWH = '{0}' where ITEMCODE = '{1}'";
            tmpSql = string.Format(tmpSql, ysxx.YSWH, ysxx.ITEMCODE);
            sqls.Add(tmpSql);

            if (xmdw != null)
            {
                //清空技术复合单位记录。
                deleteCondition.Clear();
                deleteCondition.Add("itemcode", ysxx.ITEMCODE);
                deleteCondition.Add("TYPE", ((int)ItemCompanyType.JSFH).ToString());
                sqls.Add(SqlBuilder.BuildDeleteSql<Xm_Xmdw>(deleteCondition));

                //添加技术复合单位记录。
                sqls.Add(SqlBuilder.BuildInsertSql(xmdw));
            }

            //清空项目所有的文件。
            tmpSql = "delete from item_file where itemcode = '{0}' and nodeid = '{1}'";
            tmpSql = string.Format(tmpSql, ysxx.ITEMCODE, (int)ysType);
            sqls.Add(tmpSql);

            //插入基本信息的文件。
            foreach (Item_File fileInfo in fileInfos)
            {
                sqls.Add(SqlBuilder.BuildInsertSql(fileInfo));
            }

            return OracleHelper.ExecuteCommand(sqls);
        }

        public bool UpdateYsxx(Xm_Ysxx ysxx)
        {
            ArrayList sqls = new ArrayList();
            string tmpSql;

            tmpSql = "update xm_xmxx set YSSJ = to_date('{0}','yyyy-mm-dd hh24:mi:ss') where ITEMCODE = '{1}'";
            tmpSql = string.Format(tmpSql, ysxx.ZYSJ, ysxx.ITEMCODE);
            sqls.Add(tmpSql);

            sqls.Add(SqlBuilder.BuildUpdateSql(ysxx));
            return OracleHelper.ExecuteCommand(sqls);
        }

        public bool DelYsxx(string itemCode, WorkFlowNode ysType)
        {
            ArrayList sqls = new ArrayList();
            string tmpSql;
            Dictionary<string, string> delCondition = new Dictionary<string, string>();

            delCondition.Clear();
            delCondition.Add("itemcode", itemCode);
            sqls.Add(SqlBuilder.BuildDeleteSql<Xm_Ysxx>(delCondition));

            delCondition.Clear();
            delCondition.Add("itemcode", itemCode);
            delCondition.Add("xh", "1");
            delCondition.Add("stage", ((int)ItemStage.YanShou).ToString());
            sqls.Add(SqlBuilder.BuildDeleteSql<Xm_Gcxx>(delCondition));

            //清空项目所有的文件。
            tmpSql = "delete from item_file where itemcode = '{0}' and nodeid = '{1}'";
            tmpSql = string.Format(tmpSql, itemCode, (int)ysType);
            sqls.Add(tmpSql);

            if (ysType == WorkFlowNode.ChuYan)
            {
                //清空项目所有的文件。
                tmpSql = "delete from item_file where itemcode = '{0}' and nodeid = '{1}' and filecode not in ('24','25','26')";
                tmpSql = string.Format(tmpSql, itemCode, (int)WorkFlowNode.JunGong);
                sqls.Add(tmpSql);
            }

            return OracleHelper.ExecuteCommand(sqls);
        }

        public Xm_Ysxx GetItemInfo(string itemCode)
        {
            Xm_Ysxx ItemInfo = new Xm_Ysxx();
            string strSql = "select * from xm_ysxx where itemcode = '{0}'";
            strSql = string.Format(strSql, itemCode);
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count > 0)
            {
                ItemInfo = (Xm_Ysxx)new DataTableOperation().ConvertFromDataRowToEntity(dt.Rows[0], typeof(Xm_Ysxx));
            }
            return ItemInfo;
        }
        /// <summary>
        /// 保存验收信息
        /// </summary>
        /// <param name="node"></param>
        /// <param name="ysxx"></param>
        /// <param name="itemFile"></param>
        /// <param name="xmdw"></param>
        /// <param name="gcxx"></param>
        /// <returns></returns>
        public bool SaveItem(WorkFlowNode node, Xm_Ysxx ysxx, List<Item_File> itemFile, Xm_Xmdw xmdw, Xm_Gcxx gcxx)
        {
            ArrayList strSql = new ArrayList();
            string tmpSql = string.Empty;
            ////文件
            new BusiItemManage().SaveItemFile(ysxx.ITEMCODE, node, itemFile, ref strSql);
            ////预算信息
            strSql.Add(SqlBuilder.BuildUpdateSql(ysxx));
            
            if (node == WorkFlowNode.ZhongYan)
            {
                ////工程信息
                new BusiItemManage().SaveItemGcxx(ysxx.ITEMCODE, gcxx, ref strSql);
                ////单位信息
                new BusiItemManage().SaveItemCompany(ysxx.ITEMCODE, ItemCompanyType.JSFH, xmdw, ref strSql);
                ////更新主表信息(验收时间、验收文号)
                tmpSql = "update xm_xmxx set YSSJ = to_date('{1}','yyyy-mm-dd'), YsWh = '{2}' where ITEMCODE = '{0}'";
                tmpSql = string.Format(tmpSql, ysxx.ITEMCODE,
                    ysxx.ZYSJ.HasValue ? ysxx.ZYSJ.Value.ToString("yyyy-MM-dd") : string.Empty, ysxx.YSWH);
                strSql.Add(tmpSql);
            }
            return OracleHelper.ExecuteCommand(strSql);
        }
    }
}
