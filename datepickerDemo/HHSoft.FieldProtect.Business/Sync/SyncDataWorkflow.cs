using System.Collections.Generic;
using System.Text;
using HHSoft.FieldProtect.Business.Common;
using HHSoft.FieldProtect.DataAccess;
using HHSoft.FieldProtect.DataEntity;
using HHSoft.FieldProtect.FrameWork.Utility;

namespace HHSoft.FieldProtect.Business.Sync
{
    public class SyncDataWorkflow : SyncData
    {
        public SyncDataWorkflow(OracleDbOperation targetDbOperation, OracleDbOperation localDbOperation, string targetCode,
            string localCode, FtpHelper targetFtpOperation, FtpHelper localFtpOperation, StringBuilder sbLog)
            : base(targetDbOperation, localDbOperation, targetCode, localCode, targetFtpOperation, localFtpOperation, sbLog)
        {

        }

        public override void Sync()
        {
            LogOperation.WriteTitle(sbLog, "-------------------------------------同步流程信息开始-------------------------------------");

            string companyCodeConditionString = "ccode = '" + targetCode + "'";
            List<string> workflowId = new List<string>();
            LogOperation.WriteSyncTableTitle(sbLog, "wf_workflow", SyncType.AddAfterDelete, companyCodeConditionString);
            SyncByAddAfterDelete("wf_workflow", companyCodeConditionString, deleteAction,
                (dr) =>
                {
                    LogOperation.Append(sbLog, "流程ID：" + dr["FLOWID"] + "。");
                    workflowId.Add("'" + dr["FLOWID"] + "'");
                }, totalAction);

            string workflowConditionString = "flowid in (" + string.Join(",", workflowId.ToArray()) + ")";

            LogOperation.WriteSyncTableTitle(sbLog, "wf_node", SyncType.AddAfterDelete, workflowConditionString);
            SyncByAddAfterDelete("wf_node", workflowConditionString, deleteAction,
                (dr) =>
                {
                    LogOperation.Append(sbLog, "流程ID：" + dr["FLOWID"] + "，" + "环节ID" + dr["NODEID"] + "。");
                }, totalAction);

            LogOperation.WriteSyncTableTitle(sbLog, "wf_file", SyncType.Comparer, string.Empty);
            SyncByComparer("wf_file", string.Empty, new List<string>() { "FILECODE" }, false,
                (dr) =>
                {
                    LogOperation.Append(sbLog, "添加 文档编码：" + dr["FILECODE"]);
                }, (dr) =>
                {
                    LogOperation.Append(sbLog, "删除 文档编码：" + dr["FILECODE"]);
                }, (drTarget, drLocal) =>
                {
                    LogOperation.Append(sbLog, "更新 文档编码：" + drTarget["FILECODE"]);
                });

            LogOperation.WriteTitle(sbLog, "-------------------------------------同步流程信息结束-------------------------------------");
        }
    }
}
