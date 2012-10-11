using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using GBArcGis.Base;
using HHSoft.FieldProtect.Business.Common;
using HHSoft.FieldProtect.Business.ItemManage;
using HHSoft.FieldProtect.DataAccess;
using HHSoft.FieldProtect.DataEntity;
using HHSoft.FieldProtect.DataEntity.GisData;
using HHSoft.FieldProtect.DataEntity.Sync;
using HHSoft.FieldProtect.Framework.Utility;
using HHSoft.FieldProtect.FrameWork.Utility;
using Newtonsoft.Json;

namespace HHSoft.FieldProtect.Business.Sync
{
    public class SyncDataItem : SyncData
    {
        private SyncConfig syncConfig;

        

        public SyncDataItem(SyncConfig SyncConfig, OracleDbOperation targetDb, OracleDbOperation localDb,
            string targetCode, string localCode, FtpHelper targetFtp, FtpHelper localFtp, StringBuilder sbLog)
            : base(targetDb, localDb, targetCode, localCode, targetFtp, localFtp, sbLog)
        {
            this.syncConfig = SyncConfig;
        }

        private bool ChangeTbzt(DataRow dr)
        {
            if (dr["ITEMSTATE"].ToString() == ((int)ItemState.Ending).ToString())
            {
                dr["TBZT"] = "1";

                return true;
            }
            return false;
        }

        public override void Sync()
        {            

            LogOperation.WriteTitle(sbLog, "-------------------------------------同步项目信息开始-------------------------------------");

            LogOperation.WriteTitle(sbLog, string.Format("同步数据方式：{0}", syncConfig.SyncType.Equals(1) ? "增量同步" : "全部同步"));

            Action<DataRow, DataRow> checkTbzt = (drA, drB) =>
            {
                ChangeTbzt(drA);
                if (ChangeTbzt(drB))
                {                    
                    LogOperation.Append(sbLog, "标记成已同步项目：" + drB["ITEMCODE"].ToString() + "");
                }
            };

            //在目标数据库中查询需要同步的项目信息。
            string strWhere = string.Empty;
            if (syncConfig.SyncType == 1) strWhere = "and TBZT != 1";
            
            string queryTargetUnSyncXmxx = string.Format("select * from xm_xmxx where 1=1 {0} order by itemCode", strWhere);
            DataTable dtXmxxTarget = targetDb.ExecuteDataTable(queryTargetUnSyncXmxx, "xm_xmxx", null);

            int rowCount = dtXmxxTarget.Rows.Count;
            if (rowCount > 0)
            {
                string[] itemCodes = new string[rowCount];
                string[] conditonItemCodes = new string[rowCount];
                for (int i = 0; i < rowCount; i++)
                {
                    string itemCode = dtXmxxTarget.Rows[i]["ITEMCODE"].ToString();
                    conditonItemCodes[i] = "'" + itemCode + "'";
                    itemCodes[i] = itemCode;
                }
                string itemCodeConditionString = " itemcode in (" + string.Join(",", conditonItemCodes) + ")";

                #region 同步项目信息

                LogOperation.WriteSyncTableTitle(sbLog, "xm_xmxx", SyncType.Comparer, itemCodeConditionString);
                SyncByComparer("xm_xmxx", itemCodeConditionString, new List<string>() { "ITEMCODE" }, true,
                    (dr) =>
                    {
                        LogOperation.Append(sbLog, "添加 项目编号：" + dr["ITEMCODE"]);
                    },
                    (dr) =>
                    {
                        LogOperation.Append(sbLog, "删除 项目编号：" + dr["ITEMCODE"]);
                    },
                    (drTarget, drLocal) =>
                    {
                        LogOperation.Append(sbLog, "更新 项目编号：" + drTarget["ITEMCODE"]);
                    }, checkTbzt);

                #endregion

                #region 同步项目相关信息
                
                List<string> dkxxIds = new List<string>();
                SyncByAddAfterDelete("gis_data", itemCodeConditionString, deleteAction,
                    (dr) => { dkxxIds.Add("'" + dr["GisId"] + "'"); }, totalAction);

                if (dkxxIds.Count > 0)
                {
                    string dkjzdConditionString = " GisId in (" + string.Join(",", dkxxIds.ToArray()) + ")";
                    
                    SyncByAddAfterDelete("gis_dk", dkjzdConditionString, deleteAction, null, totalAction);
                    
                    SyncByAddAfterDelete("gis_jzd", dkjzdConditionString, deleteAction, null, totalAction);
                    
                    SyncByAddAfterDelete("gis_dltb", dkjzdConditionString, deleteAction, null, totalAction);
                    
                    SyncByAddAfterDelete("gis_xzdw", dkjzdConditionString, deleteAction, null, totalAction);
                }

                string xzfConditionString = "XZDM = " + targetCode;

                SyncByAddAfterDelete("xm_xzf", xzfConditionString, deleteAction, null, totalAction);

                //SyncByAddAfterDelete("xm_xmxx", itemCodeConditionString, deleteAction, null, totalAction);
                
                SyncByAddAfterDelete("xm_sb_jbxx", itemCodeConditionString, deleteAction, null, totalAction);
                
                SyncByAddAfterDelete("xm_ky_jbxx", itemCodeConditionString, deleteAction, null, totalAction);

                SyncByAddAfterDelete("xm_ghsjysxx", itemCodeConditionString, deleteAction, null, totalAction);

                SyncByAddAfterDelete("xm_gcxx", itemCodeConditionString, deleteAction, null, totalAction);

                SyncByAddAfterDelete("xm_gdxx", itemCodeConditionString, deleteAction, null, totalAction);

                SyncByAddAfterDelete("xm_jsxx", itemCodeConditionString, deleteAction, null, totalAction);

                SyncByAddAfterDelete("xm_ss_bgxx", itemCodeConditionString, deleteAction, null, totalAction);

                SyncByAddAfterDelete("xm_ss_gcjl_jlht", itemCodeConditionString, deleteAction, null, totalAction);

                SyncByAddAfterDelete("xm_ss_gcjl_jlry", itemCodeConditionString, deleteAction, null, totalAction);

                SyncByAddAfterDelete("xm_ss_jdgz_xmbb", itemCodeConditionString, deleteAction, null, totalAction);

                SyncByAddAfterDelete("xm_ss_zjbf", itemCodeConditionString, deleteAction, null, totalAction);

                SyncByAddAfterDelete("xm_ss_ztb_jbxx", itemCodeConditionString, deleteAction, null, totalAction);

                SyncByAddAfterDelete("xm_ss_ztb_sght", itemCodeConditionString, deleteAction, null, totalAction);
                
                SyncByAddAfterDelete("xm_ss_ztb_zbqk", itemCodeConditionString, deleteAction, null, totalAction);

                SyncByAddAfterDelete("xm_xmdw", itemCodeConditionString, deleteAction, null, totalAction);
               
                SyncByAddAfterDelete("xm_xmzj", itemCodeConditionString, deleteAction, null, totalAction);

                SyncByAddAfterDelete("xm_ysxx", itemCodeConditionString, deleteAction, null, totalAction);
               
                SyncByAddAfterDelete("wf_instance", itemCodeConditionString, deleteAction, null, totalAction);

                SyncByAddAfterDelete("item_file", itemCodeConditionString, deleteAction, null, totalAction);

                //清除垃圾文件。
                LogOperation.WriteTitle(sbLog, "--------开始检查项目垃圾文件--------");
                foreach (var itemCode in itemCodes)
                {
                    string filePath = CommonManage.GetFtpUploadPath(targetCode) + itemCode;
                    string itemFileTableName = "item_file";
                    string queryFileSql = "select * from " + itemFileTableName + " where itemcode = '" + itemCode + "'";
                    DataTable dtItemFile = targetDb.ExecuteDataTable(queryFileSql, itemFileTableName, null);
                    if (dtItemFile.Rows.Count > 0)
                    {
                        List<string> exceptions = (from DataRow dr in dtItemFile.Rows select dr["FILENAME"].ToString()).ToList();
                        targetFtp.ClearDirectory(itemCode, filePath, exceptions, deleteFileAction);
                    }
                }
                LogOperation.WriteTitle(sbLog, "--------结束检查项目垃圾文件--------");

                //LogOperation.WriteSyncTableTitle(sbLog, "item_file", SyncType.AddAfterDelete, itemCodeConditionString);
               

                LogOperation.WriteTitle(sbLog, "--------开始同步项目文件(FTP)--------");
                
                foreach (var itemCode in itemCodes)
                {
                    string filePath = CommonManage.GetFtpUploadPath(targetCode);
                    try
                    {
                        targetFtp.CopyDirectory(filePath, itemCode, localFtp, filePath, itemCode, ftpCopyAction);
                    }
                    catch(Exception ex)
                    {
                        LogOperation.Append(sbLog, string.Format("异常  项目编号:{0}; 原因：{1}", itemCode, ex.Message));
                        continue;                         
                    }
                    //string gisPath = CommonManage.GetFtpGisQueryPath() + itemCode + ".xml";
                    //targetFtpOperation.CopyFile(gisPath, localFtpOperation, gisPath);
                }

                LogOperation.WriteTitle(sbLog, "--------结束同步项目文件(FTP)--------");               

                #endregion
                ////同步GIS数据
                if (this.syncConfig.SyncGisData)
                {
                    LogOperation.WriteTitle(sbLog, "--------开始调用GIS服务--------");

                    foreach (var itemCode in itemCodes)
                    {
                        ////得到连接字符串
                        Gis_Data<Gis_Dk> gisdata = new BusiItemManage_Gis().GetNewGisInfo(itemCode, targetDb.ConnectionStr, false);

                        IGBXM<GBDK> gisEntity = new BusiItemManage_Gis().Conversion(gisdata);
                        gisEntity.Ccode = itemCode.Substring(0, 6);

                        string wsUrl = this.syncConfig.ServiceUrl;
                        string method = this.syncConfig.ServiceMethod;
                        object[] args = new object[1];
                        args[0] = JsonConvert.SerializeObject(gisEntity);
                        try
                        {
                            bool succ = (bool)WebServiceHelper.InvokeWebService(wsUrl, method, args);
                            if (!succ)
                            {
                                LogOperation.Append(sbLog, "服务返回失败：" + itemCode);
                            }
                        }
                        catch(Exception ex)
                        {
                            LogOperation.Append(sbLog, string.Format("异常  项目编号:{0}; 原因：{1}", itemCode, ex.Message));
                            continue;
                        }
                    }

                    LogOperation.WriteTitle(sbLog, "--------结束调用GIS服务--------");
                }
            }

            LogOperation.WriteTitle(sbLog, "-------------------------------------同步项目信息结束-------------------------------------");
        }
    }
}
