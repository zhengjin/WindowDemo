using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using HHSoft.FieldProtect.DataEntity.ItemManage;
using HHSoft.FieldProtect.Business.Common;
using HHSoft.FieldProtect.DataAccess;
using HHSoft.FieldProtect.DataEntity;
using System.Data;

namespace HHSoft.FieldProtect.Business.ItemManage
{
    public class BusiItemManage_JS
    {
        public DataTable QueryJsxx(string itemCode)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("select a.* from xm_jsxx a");
            sbSql.AppendLine("where a.itemcode = '{0}'");
            string strSql = string.Format(sbSql.ToString(), itemCode);
            return OracleHelper.ExecuteDataTable(strSql);
        }

        public bool UpdateJsxxData(ActionEnum action, Xm_Jsxx jsxx, List<Item_File> fileInfos)
        {
            ArrayList sqls = new ArrayList();
            string tmpSql;
            switch (action)
            {
                case ActionEnum.Insert:
                    sqls.Add(SqlBuilder.BuildInsertSql(jsxx));
                    break;
                case ActionEnum.Update:
                    sqls.Add(SqlBuilder.BuildUpdateSql(jsxx));

                    //清空项目所有的文件。
                    tmpSql = "delete from item_file where itemcode = '{0}' and nodeid = '{1}'";
                    tmpSql = string.Format(tmpSql, jsxx.ITEMCODE, (int)WorkFlowNode.JueSuan);
                    sqls.Add(tmpSql);
                    break;
            }

            tmpSql = "update xm_xmxx set JSSJ = to_date('{0}','yyyy-mm-dd hh24:mi:ss') where ITEMCODE = '{1}'";
            tmpSql = string.Format(tmpSql, jsxx.SCDASJ, jsxx.ITEMCODE);
            sqls.Add(tmpSql);

            //插入文件。
            foreach (Item_File fileInfo in fileInfos)
            {
                sqls.Add(SqlBuilder.BuildInsertSql(fileInfo));
            }

            return OracleHelper.ExecuteCommand(sqls);
        }


        public Xm_Jsxx GetItemInfo(string itemCode)
        {
            Xm_Jsxx ItemInfo = null;
            string strSql = "select * from Xm_Jsxx where itemcode = '{0}'";
            strSql = string.Format(strSql, itemCode);
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count > 0)
            {
                ItemInfo = (Xm_Jsxx)new DataTableOperation().ConvertFromDataRowToEntity(dt.Rows[0], typeof(Xm_Jsxx));
            }
            return ItemInfo;
        }

        public bool SaveItem(Xm_Jsxx jsxx, Xm_Xmzj itemZj, List<Item_File> itemFile)
        {
            ArrayList strSql = new ArrayList();
            string tmpSql = string.Empty;
            
            ////项目决算
            strSql.Add(SqlBuilder.BuildDeleteSql<Xm_Jsxx>(CommonManage.delWhere(jsxx.ITEMCODE)));
            strSql.Add(SqlBuilder.BuildInsertSql(jsxx));
            ////资金
            new BusiItemManage().SaveItemMoney(jsxx.ITEMCODE, WorkFlowNode.JueSuan, itemZj, ref strSql);
            ////文件
            new BusiItemManage().SaveItemFile(jsxx.ITEMCODE, WorkFlowNode.JueSuan, itemFile, ref strSql);
            ////更新主表
            if (jsxx.SCDASJ.HasValue)
            {
                tmpSql = "update xm_xmxx set JSSJ = to_date('{0}','yyyy-mm-dd') where ITEMCODE = '{1}'";
                tmpSql = string.Format(tmpSql, jsxx.SCDASJ.Value.ToString("yyyy-MM-dd"), jsxx.ITEMCODE);
                strSql.Add(tmpSql);
            }

            return OracleHelper.ExecuteCommand(strSql);
        }
    }
}
