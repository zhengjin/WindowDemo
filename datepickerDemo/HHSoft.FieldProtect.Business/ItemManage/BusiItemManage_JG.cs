using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.ItemManage;
using HHSoft.FieldProtect.DataEntity;
using System.Collections;
using HHSoft.FieldProtect.DataAccess;
using HHSoft.FieldProtect.Business.Common;

namespace HHSoft.FieldProtect.Business.ItemManage
{
    public class BusiItemManage_JG
    {
        public bool UpdateTjcy(ActionEnum action, Xm_Ysxx ysxx, List<Item_File> fileInfos)
        {
            ArrayList sqls = new ArrayList();
            string tmpSql;

            switch (action)
            {
                case ActionEnum.Insert:
                    sqls.Add(SqlBuilder.BuildInsertSql(ysxx));
                    break;
                case ActionEnum.Update:
                    sqls.Add(SqlBuilder.BuildUpdateSql(ysxx));
                    break;
            }

            //清空项目所有的文件。
            tmpSql = "delete from item_file where itemcode = '{0}' and nodeid = '{1}' and filecode not in ('24','25','26')";
            tmpSql = string.Format(tmpSql, ysxx.ITEMCODE, (int)WorkFlowNode.JunGong);
            sqls.Add(tmpSql);

            //插入基本信息的文件。
            foreach (Item_File fileInfo in fileInfos)
            {
                sqls.Add(SqlBuilder.BuildInsertSql(fileInfo));
            }

            return OracleHelper.ExecuteCommand(sqls);
        }

        public bool UpdateJgData(string itemCode, List<Item_File> fileInfos)
        {
            ArrayList sqls = new ArrayList();
            string tmpSql;

            //清空项目所有的文件。
            tmpSql = "delete from item_file where itemcode = '{0}' and nodeid = '{1}' and filecode in ('24','25','26')";
            tmpSql = string.Format(tmpSql, itemCode, (int)WorkFlowNode.JunGong);
            sqls.Add(tmpSql);

            //插入基本信息的文件。
            foreach (Item_File fileInfo in fileInfos)
            {
                sqls.Add(SqlBuilder.BuildInsertSql(fileInfo));
            }

            return OracleHelper.ExecuteCommand(sqls);
        }

        public bool SaveItem(Xm_Ysxx ysxx, List<Item_File> itemFile)
        {
            ArrayList strSql = new ArrayList();
            string tmpSql = string.Empty;
            ////预算信息
            strSql.Add(SqlBuilder.BuildDeleteSql<Xm_Ysxx>(CommonManage.delWhere(ysxx.ITEMCODE)));
            strSql.Add(SqlBuilder.BuildInsertSql(ysxx));
            ////文件
            new BusiItemManage().SaveItemFile(ysxx.ITEMCODE, WorkFlowNode.JunGong, itemFile, ref strSql);

            return OracleHelper.ExecuteCommand(strSql);
        }
    }
}
