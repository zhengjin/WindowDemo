using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HHSoft.FieldProtect.DataAccess;
using HHSoft.FieldProtect.DataEntity.ItemManage;
using System.Collections;
using HHSoft.FieldProtect.DataEntity;
using HHSoft.FieldProtect.Business.Common;

namespace HHSoft.FieldProtect.Business.ItemManage
{
    public class BusiItemManage_SS
    {
        /// <summary>
        /// 获取招投标基本信息
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public Xm_Ss_Ztb_Jbxx GetItemInfo(string itemCode)
        {
            Xm_Ss_Ztb_Jbxx ItemInfo = null;
            string strSql = "select * from xm_ss_ztb_jbxx where itemcode = '{0}'";
            strSql = string.Format(strSql, itemCode);
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count > 0)
            {
                ItemInfo = (Xm_Ss_Ztb_Jbxx)new DataTableOperation().ConvertFromDataRowToEntity(dt.Rows[0], typeof(Xm_Ss_Ztb_Jbxx));
            }
            return ItemInfo;
        }

        public string GetMoneyByCode(string itemCode)
        {
            string strValue = string.Empty;
            string strSql = "select * from v_xm_zfzj where itemcode = '{0}'";
            strSql = string.Format(strSql, itemCode);
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count > 0)
            {
                strValue = dt.Rows[0]["zjze"].ToString();
            }
            return strValue;
        }

        public DataTable QueryZtbData(string itemCode)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("select a.* from xm_ss_ztb_jbxx a");
            sbSql.AppendLine("where a.itemcode = '{0}'");
            string strSql = string.Format(sbSql.ToString(), itemCode);
            return OracleHelper.ExecuteDataTable(strSql);
        } 

        public DataTable QueryFile(string itemCode, WorkFlowNode wfNode, string fileCode)
        {
            string strSql = " select a.*, decode(a.filecode, 0, '其它文件', b.filename) FileType"
                        + " from item_file a  left join wf_file b on a.filecode = b.filecode"
                        + " Where a.ItemCode = '{0}' and a.NodeId = '{1}' and a.filecode = '{2}'";
            strSql = string.Format(strSql, itemCode, (int)wfNode, fileCode);
            return OracleHelper.ExecuteDataTable(strSql);
        }

        public string QueryXmmc(string itemCode)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("select a.itemname from xm_xmxx a");
            sbSql.AppendLine("where a.itemcode = '{0}'");
            string strSql = string.Format(sbSql.ToString(), itemCode);
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            return dt.Rows[0][0].ToString();
        }

        public DataTable QueryZtbZbqk(string itemCode)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("select a.* from xm_ss_ztb_zbqk a");
            sbSql.AppendLine("where a.itemcode = '{0}'");
            string strSql = string.Format(sbSql.ToString(), itemCode);
            return OracleHelper.ExecuteDataTable(strSql);
        }

        public DataTable QueryZtbSght(string itemCode)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("select a.* from xm_ss_ztb_sght a");
            sbSql.AppendLine("where a.itemcode = '{0}'");
            string strSql = string.Format(sbSql.ToString(), itemCode);
            return OracleHelper.ExecuteDataTable(strSql);
        }

        public DataTable QueryXmxx(string itemCode)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("select a.* from xm_xmxx a");
            sbSql.AppendLine("where a.itemcode = '{0}'");
            string strSql = string.Format(sbSql.ToString(), itemCode);
            return OracleHelper.ExecuteDataTable(strSql);
        }

        public DataTable QueryGcjlJlry(string itemCode)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("select a.* from xm_ss_gcjl_jlry a");
            sbSql.AppendLine("where a.itemcode = '{0}'");
            string strSql = string.Format(sbSql.ToString(), itemCode);
            return OracleHelper.ExecuteDataTable(strSql);
        }

        public DataTable QueryGcjlJlht(string itemCode)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("select a.* from xm_ss_gcjl_jlht a");
            sbSql.AppendLine("where a.itemcode = '{0}'");
            string strSql = string.Format(sbSql.ToString(), itemCode);
            return OracleHelper.ExecuteDataTable(strSql);
        }

        public DataTable QueryJzxx(string itemCode)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("select a.* from xm_ss_jdgz_jzxx a");
            sbSql.AppendLine("where a.itemcode = '{0}'");
            string strSql = string.Format(sbSql.ToString(), itemCode);
            return OracleHelper.ExecuteDataTable(strSql);
        }

        public DataTable QueryJzxxXmbb(string itemCode, JdgzReportType type)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("select a.* from xm_ss_jdgz_xmbb a");
            sbSql.AppendLine("where a.itemcode = '{0}' and a.bblx = '{1}'");
            string strSql = string.Format(sbSql.ToString(), itemCode, type.ToString());
            return OracleHelper.ExecuteDataTable(strSql);
        }

        public DataTable QueryBgxx(string itemCode, string xh)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("select a.* from xm_ss_bgxx a");
            sbSql.AppendLine("where a.itemcode = '{0}'");
            if (!string.IsNullOrEmpty(xh))
            {
                sbSql.AppendLine("and a.xh = " + xh);
            }
            sbSql.AppendLine("order by xh");
            string strSql = string.Format(sbSql.ToString(), itemCode);
            return OracleHelper.ExecuteDataTable(strSql);
        }

        public DataTable QueryZjbf(string itemCode)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("select a.* from xm_ss_zjbf a");
            sbSql.AppendLine("where a.itemcode = '{0}'");
            string strSql = string.Format(sbSql.ToString(), itemCode);
            return OracleHelper.ExecuteDataTable(strSql);
        }

        public bool HaveUncompletedBgxx(DataTable dt)
        {
            bool result = false;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["COMPLETED"].ToString() != dr["BGNR"].ToString())
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool SaveSSdate(string itemCode, string type, string date)
        {
            ArrayList sSqlList = new ArrayList(); 

            string strSql = "select * from xm_ss_ztb_jbxx where itemcode = '" + itemCode + "'";
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count == 1)
            {
                strSql = "update xm_ss_ztb_jbxx set {0} = to_date('{1}','yyyy-MM-dd') where itemCode = '{2}'";                
            }
            else
            {
                strSql = "Insert Into xm_ss_ztb_jbxx(itemCode,{0}) values ('{2}',to_date('{1}','yyyy-MM-dd'))";
            }
            strSql = string.Format(strSql, type, date, itemCode);
            sSqlList.Add(strSql);

            if (type.ToLower() == "kbsj" || type.ToLower() == "kgsj" || type.ToLower() == "jgsj")
            {
                strSql = "update xm_xmxx set {0} = to_date('{1}','yyyy-MM-dd') where itemcode = '{2}'";
                strSql = string.Format(strSql, type, date, itemCode);
                sSqlList.Add(strSql);
            }

            return OracleHelper.ExecuteCommand(sSqlList);
        }


        public bool HaveUncompletedBgxx(string itemCode)
        {
            DataTable dt = QueryBgxx(itemCode, null);
            return HaveUncompletedBgxx(dt);
        }

        public int QueryNextBgXh(string itemCode)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("select max(xh) from xm_ss_bgxx a");
            sbSql.AppendLine("where a.itemcode = '{0}'");
            string strSql = string.Format(sbSql.ToString(), itemCode);
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count == 0)
            {
                return 1;
            }
            else
            {
                DataRow dr = dt.Rows[0];
                string xhString = dr[0].ToString();
                if (string.IsNullOrEmpty(xhString))
                {
                    return 1;
                }
                else
                {
                    int xh = int.Parse(xhString);
                    return xh + 1;
                }
            }
        }

        public bool AddBgxx(Xm_Ss_Bgxx bgxx, List<Item_File> fileInfos)
        {
            ArrayList sqls = new ArrayList();            
            sqls.Add(SqlBuilder.BuildInsertSql(bgxx));

            //插入基本信息的文件。
            foreach (Item_File fileInfo in fileInfos)
            {
                sqls.Add(SqlBuilder.BuildInsertSql(fileInfo));
            }

            return OracleHelper.ExecuteCommand(sqls);
        }

        public bool AddCddw(Xm_Xmdw cddw)
        {
            ArrayList sqls = new ArrayList();
            sqls.Add(SqlBuilder.BuildInsertSql(cddw));
            return OracleHelper.ExecuteCommand(sqls);
        }

        public void SaveItem_SS(Xm_Ss_Ztb_Jbxx ztbJbxx,
            List<Xm_Ss_Ztb_Zbqk> zbqks, List<Xm_Ss_Ztb_Sght> sghts,
            List<Xm_Ss_Gcjl_Jlry> jlrys, List<Xm_Ss_Gcjl_Jlht> jlhts,
            List<Xm_Ss_Jdgz_Xmbb> xmybs, List<Xm_Ss_Jdgz_Xmbb> xmjbs,
            List<Xm_Ss_Zjbf> zjbfs,ref ArrayList strSql)
        {

            Dictionary<string, string> deleteWhere = new Dictionary<string, string>();
            deleteWhere.Add("itemcode", ztbJbxx.ItemCode);

            ////招投标基本记录。
            strSql.Add(SqlBuilder.BuildDeleteSql<Xm_Ss_Ztb_Jbxx>(deleteWhere));
            strSql.Add(SqlBuilder.BuildInsertSql(ztbJbxx));

            ////中标情况记录。
            strSql.Add(SqlBuilder.BuildDeleteSql<Xm_Ss_Ztb_Zbqk>(deleteWhere));
            foreach (var zbqk in zbqks)
            {
                strSql.Add(SqlBuilder.BuildInsertSql(zbqk));
            }

            ////施工合同记录。
            strSql.Add(SqlBuilder.BuildDeleteSql<Xm_Ss_Ztb_Sght>(deleteWhere));
            foreach (var sght in sghts)
            {
                strSql.Add(SqlBuilder.BuildInsertSql(sght));
            }

            ////监理人员记录。
            strSql.Add(SqlBuilder.BuildDeleteSql<Xm_Ss_Gcjl_Jlry>(deleteWhere));
            foreach (var jlry in jlrys)
            {
                strSql.Add(SqlBuilder.BuildInsertSql(jlry));
            }

            ////监理合同记录。
            strSql.Add(SqlBuilder.BuildDeleteSql<Xm_Ss_Gcjl_Jlht>(deleteWhere));
            foreach (var jlht in jlhts)
            {
                strSql.Add(SqlBuilder.BuildInsertSql(jlht));
            }

            ////项目报表记录。
            strSql.Add(SqlBuilder.BuildDeleteSql<Xm_Ss_Jdgz_Xmbb>(deleteWhere));            
            foreach (var xmbb in xmybs)
            {
                strSql.Add(SqlBuilder.BuildInsertSql(xmbb));
            }
            foreach (var xmbb in xmjbs)
            {
                strSql.Add(SqlBuilder.BuildInsertSql(xmbb));
            }

            ////资金拨付记录
            strSql.Add(SqlBuilder.BuildDeleteSql<Xm_Ss_Zjbf>(deleteWhere));
            foreach (var zjbf in zjbfs)
            {
                strSql.Add(SqlBuilder.BuildInsertSql(zjbf));
            }
        }

        public bool UpdateZtbData(ActionEnum action, Xm_Ss_Ztb_Jbxx ztbJbxx, Xm_Xmdw xmdw, List<Xm_Ss_Ztb_Zbqk> zbqks, List<Xm_Ss_Ztb_Sght> sghts, List<Item_File> fileInfos)
        {
            List<string> sgdwNames = (from record in sghts
                                      select record.CBF).Distinct().ToList();
            List<Xm_Xmdw> sgdws = new List<Xm_Xmdw>();
            foreach (var item in sgdwNames)
            {
                sgdws.Add(new Xm_Xmdw() { ItemCode = ztbJbxx.ItemCode, Type = ItemCompanyType.SG, Name = item });
            }

            ArrayList sqls = new ArrayList();
            string tmpSql;
            switch (action)
            {
                case ActionEnum.Insert:
                    sqls.Add(SqlBuilder.BuildInsertSql(ztbJbxx));
                    sqls.Add(SqlBuilder.BuildInsertSql(xmdw));
                    break;
                case ActionEnum.Update:
                    sqls.Add(SqlBuilder.BuildUpdateSql(ztbJbxx));
                    sqls.Add(SqlBuilder.BuildUpdateSql(xmdw));

                    //清空项目所有的文件。
                    tmpSql = "delete from item_file where itemcode = '{0}' and nodeid = '{1}' and filecode in ('16','17','18','19')";
                    tmpSql = string.Format(tmpSql, ztbJbxx.ItemCode, (int)WorkFlowNode.ShiShi);
                    sqls.Add(tmpSql);

                    //清空所有中标情况记录。
                    tmpSql = "delete from xm_ss_ztb_zbqk where itemcode = '{0}'";
                    tmpSql = string.Format(tmpSql, ztbJbxx.ItemCode);
                    sqls.Add(tmpSql);

                    //清空所有施工合同记录。
                    tmpSql = "delete from xm_ss_ztb_sght where itemcode = '{0}'";
                    tmpSql = string.Format(tmpSql, ztbJbxx.ItemCode);
                    sqls.Add(tmpSql);

                    //清空所有施工单位记录。
                    tmpSql = "delete from xm_xmdw where itemcode = '{0}' and TYPE = '{1}'";
                    tmpSql = string.Format(tmpSql, ztbJbxx.ItemCode, (int)ItemCompanyType.SG);
                    sqls.Add(tmpSql);
                    break;
            }

            tmpSql = "update xm_xmxx set KBSJ = to_date('{0}','yyyy-mm-dd hh24:mi:ss') where ITEMCODE = '{1}'";
            tmpSql = string.Format(tmpSql, ztbJbxx.KBSJ, ztbJbxx.ItemCode);
            sqls.Add(tmpSql);

            //插入基本信息的文件。
            foreach (Item_File fileInfo in fileInfos)
            {
                sqls.Add(SqlBuilder.BuildInsertSql(fileInfo));
            }
            foreach (var zbqk in zbqks)
            {
                sqls.Add(SqlBuilder.BuildInsertSql(zbqk));
            }
            foreach (var sght in sghts)
            {
                sqls.Add(SqlBuilder.BuildInsertSql(sght));
            }
            foreach (var sgdw in sgdws)
            {
                sqls.Add(SqlBuilder.BuildInsertSql(sgdw));
            }
            return OracleHelper.ExecuteCommand(sqls);
        }

        public bool UpdateJlData(ActionEnum action, Xm_Xmdw xmdw, List<Xm_Ss_Gcjl_Jlry> jlrys, List<Xm_Ss_Gcjl_Jlht> jlhts, List<Item_File> jlhtFiles)
        {
            ArrayList sqls = new ArrayList();
            string tmpSql;
            switch (action)
            {
                case ActionEnum.Insert:
                    sqls.Add(SqlBuilder.BuildInsertSql(xmdw));
                    break;
                case ActionEnum.Update:
                    sqls.Add(SqlBuilder.BuildUpdateSql(xmdw));

                    //清空项目所有的文件。
                    tmpSql = "delete from item_file where itemcode = '{0}' and nodeid = '{1}' and filecode in ('{2}')";
                    tmpSql = string.Format(tmpSql, xmdw.ItemCode, (int)WorkFlowNode.ShiShi, "20");
                    sqls.Add(tmpSql);

                    Dictionary<string, string> delJlInfoCondition = new Dictionary<string, string>();
                    delJlInfoCondition.Add("itemcode", xmdw.ItemCode);

                    //清空所有监理人员记录。
                    sqls.Add(SqlBuilder.BuildDeleteSql<Xm_Ss_Gcjl_Jlry>(delJlInfoCondition));

                    //清空所有监理合同记录。
                    sqls.Add(SqlBuilder.BuildDeleteSql<Xm_Ss_Gcjl_Jlht>(delJlInfoCondition));
                    break;
            }

            //插入基本信息的文件。
            foreach (Item_File fileInfo in jlhtFiles)
            {
                sqls.Add(SqlBuilder.BuildInsertSql(fileInfo));
            }
            foreach (var jlry in jlrys)
            {
                sqls.Add(SqlBuilder.BuildInsertSql(jlry));
            }
            foreach (var jlht in jlhts)
            {
                sqls.Add(SqlBuilder.BuildInsertSql(jlht));
            }
            return OracleHelper.ExecuteCommand(sqls);
        }

        public bool UpdateJdData(ActionEnum action, Xm_Ss_Ztb_Jbxx jzxx, List<Xm_Ss_Jdgz_Xmbb> xmybs, List<Xm_Ss_Jdgz_Xmbb> xmjbs, List<Item_File> files)
        {
            ArrayList sqls = new ArrayList();
            string tmpSql;
            switch (action)
            {
                case ActionEnum.Insert:
                    sqls.Add(SqlBuilder.BuildInsertSql(jzxx));
                    break;
                case ActionEnum.Update:
                    sqls.Add(SqlBuilder.BuildUpdateSql(jzxx));

                    //清空项目所有的文件。
                    tmpSql = "delete from item_file where itemcode = '{0}' and nodeid = '{1}' and filecode in ('{2}')";
                    tmpSql = string.Format(tmpSql, jzxx.ItemCode, (int)WorkFlowNode.ShiShi, "21");
                    sqls.Add(tmpSql);

                    //清空所有项目报表记录。
                    Dictionary<string, string> xmbbCondition = new Dictionary<string, string>();
                    xmbbCondition.Add("itemcode", jzxx.ItemCode);
                    sqls.Add(SqlBuilder.BuildDeleteSql<Xm_Ss_Jdgz_Xmbb>(xmbbCondition));
                    break;
            }

            tmpSql = "update xm_xmxx set KGSJ = to_date('{0}','yyyy-mm-dd hh24:mi:ss'),JGSJ = to_date('{1}','yyyy-mm-dd hh24:mi:ss') where ITEMCODE = '{2}'";
            tmpSql = string.Format(tmpSql, jzxx.KGSJ, jzxx.JGSJ, jzxx.ItemCode);
            sqls.Add(tmpSql);

            //插入基本信息的文件。
            foreach (Item_File fileInfo in files)
            {
                sqls.Add(SqlBuilder.BuildInsertSql(fileInfo));
            }
            foreach (var xmbb in xmybs.Union(xmjbs))
            {
                sqls.Add(SqlBuilder.BuildInsertSql(xmbb));
            }
            return OracleHelper.ExecuteCommand(sqls);
        }

        public bool UpdateBfData(string itemCode, List<Xm_Ss_Zjbf> zjbfs)
        {
            ArrayList sqls = new ArrayList();

            //清空所有资金拨付记录。
            Dictionary<string, string> zjbfDelCondition = new Dictionary<string, string>();
            zjbfDelCondition.Add("ITEMCODE", itemCode);
            sqls.Add(SqlBuilder.BuildDeleteSql<Xm_Ss_Zjbf>(zjbfDelCondition));

            foreach (var zjbf in zjbfs)
            {
                sqls.Add(SqlBuilder.BuildInsertSql(zjbf));
            }
            return OracleHelper.ExecuteCommand(sqls);
        }

        public bool UpdateBgData(string itemCode, List<Xm_Ss_Bgxx> bgxxs)
        {
            ArrayList sqls = new ArrayList();

            //清空所有变更信息记录。
            Dictionary<string, string> bgxxDelCondition = new Dictionary<string, string>();
            bgxxDelCondition.Add("ITEMCODE", itemCode);
            sqls.Add(SqlBuilder.BuildDeleteSql<Xm_Ss_Bgxx>(bgxxDelCondition));

            foreach (var zjbf in bgxxs)
            {
                sqls.Add(SqlBuilder.BuildInsertSql(zjbf));
            }
            return OracleHelper.ExecuteCommand(sqls);
        }

        public bool UpdateBgxxData(Xm_Ss_Bgxx bgxx, ActionEnum gcxxAction, Xm_Gcxx gcxx, Xm_Xmzj xmzjgs, List<Item_File> files)
        {
            ArrayList sqls = new ArrayList();
            string tmpSql;

            sqls.Add(SqlBuilder.BuildUpdateSql(bgxx));

            if (gcxx != null)
            {
                switch (gcxxAction)
                {
                    case ActionEnum.Insert:
                        tmpSql = "delete from xm_gcxx where itemcode = '{0}' and stage = '{1}' and xh >= {2}";
                        tmpSql = string.Format(tmpSql, gcxx.ItemCode, (int)gcxx.Stage, gcxx.Xh);
                        sqls.Add(tmpSql);
                        sqls.Add(SqlBuilder.BuildInsertSql(gcxx));
                        break;
                    case ActionEnum.Update:
                        sqls.Add(SqlBuilder.BuildUpdateSql(gcxx));
                        break;
                }
            }

            if (xmzjgs != null)
            {
                tmpSql = "delete from xm_xmzj where itemcode = '{0}' and stage = '{1}' and xh = '{2}'";
                tmpSql = string.Format(tmpSql, xmzjgs.ItemCode, (int)xmzjgs.Stage, xmzjgs.Xh);
                sqls.Add(tmpSql);
                sqls.Add(SqlBuilder.BuildInsertSql(xmzjgs));

                //清空项目所有的文件。
                tmpSql = "delete from item_file where itemcode = '{0}' and stage = '{1}' and xh = '{2}' and filecode in ('{3}')";
                tmpSql = string.Format(tmpSql, xmzjgs.ItemCode, (int)xmzjgs.Stage, xmzjgs.Xh, (int)FileCode.预算补充文件);
                sqls.Add(tmpSql);

                //插入基本信息的文件。
                foreach (Item_File fileInfo in files)
                {
                    sqls.Add(SqlBuilder.BuildInsertSql(fileInfo));
                }
            }

            return OracleHelper.ExecuteCommand(sqls);
        }
    }
}
