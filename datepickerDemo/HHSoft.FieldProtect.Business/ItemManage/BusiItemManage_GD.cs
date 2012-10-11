using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.ItemManage;
using System.Collections;
using HHSoft.FieldProtect.DataAccess;
using HHSoft.FieldProtect.Business.Common;
using System.Data;
using HHSoft.FieldProtect.DataEntity;

namespace HHSoft.FieldProtect.Business.ItemManage
{
    public class BusiItemManage_GD
    {
        public DataTable QueryGdxx(string itemCode)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("select a.* from xm_gdxx a");
            sbSql.AppendLine("where a.itemcode = '{0}'");
            string strSql = string.Format(sbSql.ToString(), itemCode);
            return OracleHelper.ExecuteDataTable(strSql);
        }

        public bool UpdateGdxxData(ActionEnum action, Xm_Gdxx gdxx)
        {
            ArrayList sqls = new ArrayList();
            string tmpSql;

            switch (action)
            {
                case ActionEnum.Insert:
                    sqls.Add(SqlBuilder.BuildInsertSql(gdxx));
                    break;
                case ActionEnum.Update:
                    sqls.Add(SqlBuilder.BuildUpdateSql(gdxx));
                    break;
            }

            tmpSql = "update xm_xmxx set GDSJ = to_date('{0}','yyyy-mm-dd hh24:mi:ss') where ITEMCODE = '{1}'";
            tmpSql = string.Format(tmpSql, gdxx.GDSJ, gdxx.ITEMCODE);
            sqls.Add(tmpSql);

            return OracleHelper.ExecuteCommand(sqls);
        }
    }
}
