using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using HHSoft.FieldProtect.DataEntity;
using HHSoft.FieldProtect.DataEntity.ItemManage;
using HHSoft.FieldProtect.DataAccess;
using System.Data;
using HHSoft.FieldProtect.Business.Common;

namespace HHSoft.FieldProtect.Business.ItemManage
{
    public class BusiItemManage_KY
    {
        public BusiItemManage_KY() { }

        /// <summary>
        /// 保存可研信息
        /// </summary>
        /// <param name="node"></param>
        /// <param name="itemKy"></param>
        /// <param name="itemFileList"></param>
        /// <returns></returns>
        public void SaveItem_KY(Xm_Ky_Jbxx itemKy, ref ArrayList strSql)
        {            
            string tmpSql = string.Empty;

            tmpSql = "delete from xm_ky_jbxx where itemcode = '{0}'";
            tmpSql = string.Format(tmpSql, itemKy.ItemCode);
            strSql.Add(tmpSql);

            //tmpSql = " insert into xm_ky_jbxx (itemcode, address, qsjd_d, qsjd_f, qsjd_m, "
            //+ " jzjd_d, jzjd_f, jzjd_m, qswd_d, qswd_f, qswd_m, jzwd_d, jzwd_f, jzwd_m, dmlx, xmlx, "
            //+ " tdlylx, tdqsqk, jsgm, jzgdmj, jsgq, tzgs, mjtz) values"
            //+ " ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}',"
            //+ " '{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}')";
            //tmpSql = string.Format(tmpSql, itemKy.ItemCode, itemKy.Address, itemKy.Qsjd_D,
            //    itemKy.Qsjd_F, itemKy.Qsjd_M, itemKy.Jzjd_D, itemKy.Jzjd_F, itemKy.Jzjd_M, itemKy.Qswd_D, itemKy.Qswd_F,
            //    itemKy.Qswd_M, itemKy.Jzwd_D, itemKy.Jzwd_F, itemKy.Jzwd_M, itemKy.Dmlx, itemKy.Xmlx, itemKy.Tdlylx,
            //    itemKy.Tdqsqk, itemKy.Jsgm, itemKy.Jzgdmj, itemKy.Jsgq, itemKy.Tzgs, itemKy.Mjtz);
            strSql.Add(tmpSql);      
        }

        /// <summary>
        /// 保存项目立项数据
        /// </summary>
        /// <param name="itemKy"></param>
        /// <returns></returns>
        public bool SaveItem_KYInfo(Xm_Ky_Jbxx itemKy)
        {            
            ArrayList strSql = new ArrayList();
            string tmpSql = string.Empty;
            tmpSql = "update xm_xmxx set xmpc = '{1}', Lxsj = to_date('{2}','yyyy-mm-dd'), Lxwh = '{3}',ItemType = '{4}' where itemCode in ({0})";
            tmpSql = string.Format(tmpSql, itemKy.ItemCode, itemKy.Xmpc, itemKy.Lxsj.Value.ToString("yyyy-MM-dd"), itemKy.Lxwh, itemKy.ItemType);
            strSql.Add(tmpSql);

            tmpSql = "update xm_ky_jbxx set xmpc = '{1}', Lxsj = to_date('{2}','yyyy-mm-dd'), Lxwh = '{3}',Lxdw = '{4}',ItemType = '{5}' where itemCode in ({0})";
            tmpSql = string.Format(tmpSql, itemKy.ItemCode, itemKy.Xmpc, itemKy.Lxsj.Value.ToString("yyyy-MM-dd"), itemKy.Lxwh, itemKy.Lxdw, itemKy.ItemType);
            strSql.Add(tmpSql);

            return OracleHelper.ExecuteCommand(strSql);
        }

        /// <summary>
        /// 获取可研信息
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public Xm_Ky_Jbxx GetItemInfo(string itemCode)
        {
            Xm_Ky_Jbxx ItemInfo = null;            
            string strSql = "select * from xm_ky_jbxx a "
               + " left join xm_xmzj b on a.itemcode = b.itemcode and b.stage = {1}"
               + " left join xm_xmdw c on a.itemcode = c.itemcode and c.type = {2}"
               + " where a.itemcode = '{0}'";
            strSql = string.Format(strSql, itemCode, ((int)ItemStage.KeYan).ToString(), ((int)ItemCompanyType.KY).ToString());
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count > 0)
            {
                ItemInfo = (Xm_Ky_Jbxx)new DataTableOperation().ConvertFromDataRowToEntity(dt.Rows[0], typeof(Xm_Ky_Jbxx));
                ItemInfo.Xmzj = (Xm_Xmzj)new DataTableOperation().ConvertFromDataRowToEntity(dt.Rows[0], typeof(Xm_Xmzj));
                ItemInfo.KyDw = (Xm_Xmdw)new DataTableOperation().ConvertFromDataRowToEntity(dt.Rows[0], typeof(Xm_Xmdw));
            }
            return ItemInfo;
        }


        public DataTable QueryData(string itemCode)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("select a.* from xm_ky_jbxx a");
            sbSql.AppendLine("where a.itemcode = '{0}'");
            string strSql = string.Format(sbSql.ToString(), itemCode);
            return OracleHelper.ExecuteDataTable(strSql);
        }

        public bool SaveItem(Xm_Xmxx itemInfo, Xm_Ky_Jbxx itemKy,
           List<Item_File> itemFile, Xm_Xmzj itemZj, Xm_Gcxx itemGcxx,Xm_Xmdw itemDw)
        {
            ArrayList strSql = new ArrayList();
            string tmpSql = string.Empty;
            //// 项目主表            
            strSql.Add(SqlBuilder.BuildUpdateSql(itemInfo));
            ////项目可研信息
            strSql.Add(SqlBuilder.BuildDeleteSql<Xm_Ky_Jbxx>(CommonManage.delWhere(itemKy.ItemCode)));
            strSql.Add(SqlBuilder.BuildInsertSql(itemKy));
            ////文件
            new BusiItemManage().SaveItemFile(itemInfo.ItemCode, WorkFlowNode.KY, itemFile, ref strSql);
            ////资金
            new BusiItemManage().SaveItemMoney(itemInfo.ItemCode, WorkFlowNode.KY, itemZj, ref strSql);
            ////工程
            new BusiItemManage().SaveItemGcxx(itemInfo.ItemCode, itemGcxx, ref strSql);
            ////单位
            new BusiItemManage().SaveItemCompany(itemInfo.ItemCode, ItemCompanyType.KY, itemDw, ref strSql);
            ////GIS信息

            return OracleHelper.ExecuteCommand(strSql);
        }

    }
}
