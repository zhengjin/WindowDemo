using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity;
using HHSoft.FieldProtect.DataEntity.WorkFlow;
using HHSoft.FieldProtect.Business.ItemManage;
using HHSoft.FieldProtect.DataEntity.ItemManage;
using HHSoft.FieldProtect.Framework.Utility;
using System.Data;
using HHSoft.FieldProtect.DataAccess;
using HHSoft.FieldProtect.Business.Common;
using System.Reflection;

namespace HHSoft.FieldProtect.Business.Workflow
{
    public abstract class WorkflowValidation
    {
        protected string NotNullFormat = "\"{0}\"必须填写！";
        protected Dictionary<string, string> condition;
        protected DataTableOperation dtOperation;

        public WorkflowValidation()
        {
            condition = new Dictionary<string, string>();
            dtOperation = new DataTableOperation();
        }

        public static WorkflowValidation Build(WorkFlowNode node)
        {
            switch (node)
            {
                case WorkFlowNode.Begin:
                    return new WorkflowSbValidation();
                case WorkFlowNode.TB:
                    return new WorkflowSbValidation();
                case WorkFlowNode.KY:
                    return new WorkflowKyvalidation();
                case WorkFlowNode.GHSJYS:
                    return new WorkflowGhvalidation();
                case WorkFlowNode.YSTZ:
                    return new WorkflowYsTzvalidation();
                case WorkFlowNode.ShiShi:
                    return new WorkflowSsValidation();
                case WorkFlowNode.JunGong:
                    return new WorkflowJgValidation();
                case WorkFlowNode.ChuYan:
                    return new WorkflowCyValidation();
                case WorkFlowNode.ZhongYan:
                    return new WorkflowZyValidation();
                case WorkFlowNode.JueSuan:
                    return new WorkflowJsValidation();
                case WorkFlowNode.GuiDang:
                    return new WorkflowGdValidation();
                default:
                    throw new Exception("未找到验证类。节点ID：" + node);
            }
        }

        /// <summary>
        /// 追加错误信息。
        /// </summary>
        /// <param name="sb">错误信息对象。</param>
        /// <param name="errorMessage">错误信息。</param>
        protected void AppendErrorMessage(StringBuilder sb, string errorMessage)
        {
            if (!string.IsNullOrEmpty(errorMessage))
            {
                sb.AppendLine(errorMessage);
            }
        }

        /// <summary>
        /// 验证文件是否都上传。
        /// </summary>
        /// <param name="workflowId">流程Id。</param>
        /// <param name="itemCode">项目编号。</param>
        /// <param name="node">节点。</param>
        /// <param name="fileCodes">是否制定验证文件的类型。</param>
        /// <returns>错误信息。</returns>
        protected string FileValidation(string workflowId, string itemCode, WorkFlowNode node, List<string> fileCodes)
        {
            WfNode nodeInfo = this.GetNodeInfo(workflowId, ((int)node).ToString());
            List<string> nodeFileCodes = nodeInfo.NodeFileCode.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (fileCodes == null)
            {
                fileCodes = nodeFileCodes;
            }
            else
            {
                fileCodes = fileCodes.Intersect(nodeFileCodes).ToList();
            }
            string format = "\"{0}\"必须上传！";
            StringBuilder sb = new StringBuilder();
            List<Item_File> fileInfos = new BusiItemManage().GetItemFile(itemCode, node);
            foreach (var fileInfo in fileInfos)
            {
                fileCodes.Remove(fileInfo.FileCode);
            }
            if (fileCodes.Count != 0)
            {
                foreach (var fileCode in fileCodes)
                {
                    sb.AppendLine(string.Format(format, EnumHelper.GetFieldDescription(typeof(FileCode), int.Parse(fileCode))));
                }
            }
            return sb.ToString();
        }

        protected string MoneyValidation(string itemCode, string dispalyName, WorkFlowNode node, int xh)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dtMoney = new BusiItemManage().GetItemMoneyDt(itemCode, (int)node, xh.ToString());
            AppendErrorMessage(sb, dtOperation.RowNullCheck(dtMoney, dispalyName, NotNullFormat));
            condition.Clear();
            condition.Add("ZJZE", dispalyName);
            AppendErrorMessage(sb, dtOperation.ColumnNullCheck(dtMoney, condition, NotNullFormat));

            if (dtMoney.Rows.Count > 0)
            {
                if (FormatMoney(dtMoney.Rows[0]["ZJZE"].ToString()) !=
                    FormatMoney(dtMoney.Rows[0]["fee1"].ToString()) + FormatMoney(dtMoney.Rows[0]["fee2"].ToString()) +
                    FormatMoney(dtMoney.Rows[0]["fee3"].ToString()) + FormatMoney(dtMoney.Rows[0]["fee4"].ToString()) +
                    FormatMoney(dtMoney.Rows[0]["fee5"].ToString()) + FormatMoney(dtMoney.Rows[0]["fee6"].ToString()) +
                    FormatMoney(dtMoney.Rows[0]["fee7"].ToString()) + FormatMoney(dtMoney.Rows[0]["fee8"].ToString()))
                {
                    AppendErrorMessage(sb, "资金总额与来源情况不相符!");
                }

                if (FormatMoney(dtMoney.Rows[0]["ZJZE"].ToString()) !=
                    FormatMoney(dtMoney.Rows[0]["sbgzf"].ToString()) + FormatMoney(dtMoney.Rows[0]["tdpzgcf"].ToString()) +
                    FormatMoney(dtMoney.Rows[0]["ntslgcf"].ToString()) + FormatMoney(dtMoney.Rows[0]["dlgcf"].ToString()) +
                    FormatMoney(dtMoney.Rows[0]["qtgcf"].ToString()) + FormatMoney(dtMoney.Rows[0]["bkyjf"].ToString()) +
                    FormatMoney(dtMoney.Rows[0]["qtfy"].ToString()))
                {
                    AppendErrorMessage(sb, "资金总额与使用情况不相符!");
                }
            }

            return sb.ToString();
        }


        private decimal FormatMoney(string strValue)
        {
            return decimal.Parse(string.IsNullOrEmpty(strValue) ? "0" : strValue);
        }

        protected string GcxxValidation(string itemCode, ItemStage stage, int xh)
        {
            Xm_Gcxx gcInfo = new BusiItemManage().GetItemGcxx(itemCode, stage, xh.ToString());
            StringBuilder sb = new StringBuilder();
            if (gcInfo == null)
            {
                sb.AppendLine(string.Format(NotNullFormat, "项目规模"));
                sb.AppendLine(string.Format(NotNullFormat, "成效信息"));
                sb.AppendLine(string.Format(NotNullFormat, "工程量信息"));
            }
            else
            {
                bool gmFlag = true;
                bool cxFlag = true;
                bool gclFlag = true;
                foreach (PropertyInfo pi in gcInfo.GetType().GetProperties())
                {                    
                    if (pi.Name.ToLower().StartsWith("gm"))
                    {
                        if (!string.IsNullOrEmpty(pi.GetValue(gcInfo, null).ToString()))
                        {
                            gmFlag = false;
                        }
                    }
                    if (pi.Name.ToLower().StartsWith("cx"))
                    {
                        if (!string.IsNullOrEmpty(pi.GetValue(gcInfo, null).ToString()))
                        {
                            cxFlag = false;
                        }
                    }
                    if (pi.Name.ToLower().StartsWith("gc"))
                    {
                        if (!string.IsNullOrEmpty(pi.GetValue(gcInfo, null).ToString()))
                        {
                            gclFlag = false;
                        }
                    }
                }
                if (gmFlag) sb.AppendLine(string.Format(NotNullFormat, "项目规模"));
                if (cxFlag) sb.AppendLine(string.Format(NotNullFormat, "成效信息"));
                if (gclFlag) sb.AppendLine(string.Format(NotNullFormat, "工程量信息"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 验证方法。
        /// </summary>
        /// <param name="workflowId">流程Id。</param>
        /// <param name="itemCode">项目编号。</param>
        /// <returns>错误信息。</returns>
        public abstract string Validation(string workflowId, string itemCode, WfResult wfResult);

        private WfNode GetNodeInfo(string workFlowId, string nodeId)
        {
            WfNode node = null;
            string strSql = "select * from wf_node where FlowId = {0} and NodeId = {1}";
            strSql = string.Format(strSql, workFlowId, nodeId);
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count == 1)
            {
                node = new WfNode();
                node.NodeId = dt.Rows[0]["nodeId"].ToString();
                node.NodeDesc = dt.Rows[0]["NodeDesc"].ToString();
                node.Stage = dt.Rows[0]["Stage"].ToString();
                node.PerNode = dt.Rows[0]["Pernode"].ToString();
                node.NextNode = dt.Rows[0]["NextNode"].ToString();
                node.NodeLevel = dt.Rows[0]["NodeLevel"].ToString();
                node.TimeOut = dt.Rows[0]["TimeOut"].ToString();
                node.NodeDepartCode = dt.Rows[0]["NodeDepartCode"].ToString();
                node.NodeRoleId = dt.Rows[0]["NodeRoleId"].ToString();
                node.NodeUserId = dt.Rows[0]["NodeUserId"].ToString();
                node.NodeFileCode = dt.Rows[0]["NodeFileCode"].ToString();
                node.NodeType = (NodeType)EnumHelper.StringValueToEnum(typeof(NodeType), dt.Rows[0]["NodeType"].ToString());
                node.NotifyType = dt.Rows[0]["NotifyType"].ToString();
                node.BeginText = dt.Rows[0]["notifyBeginText"].ToString();
                node.NextText = dt.Rows[0]["notifyNextText"].ToString();
                node.HistoryText = dt.Rows[0]["notifyHistoryText"].ToString();
            }
            return node;
        }
    }
}
