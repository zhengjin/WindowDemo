using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HHSoft.FieldProtect.Business.ItemManage;
using HHSoft.FieldProtect.Business.Common;
using HHSoft.FieldProtect.DataEntity;

namespace HHSoft.FieldProtect.Business.Workflow
{
    public class WorkflowJsValidation : WorkflowValidation
    {
        public override string Validation(string workflowId, string itemCode, WfResult wfResult)
        {
            StringBuilder sb = new StringBuilder();

            //验证决算信息。

            AppendErrorMessage(sb, MoneyValidation(itemCode, "实际投资信息", WorkFlowNode.JueSuan, 1));

            DataTable dtJsxx = new BusiItemManage_JS().QueryJsxx(itemCode);
            AppendErrorMessage(sb, dtOperation.RowNullCheck(dtJsxx, "决算信息", NotNullFormat));
            condition.Clear();
            condition.Add("SJZTZ", "资金总额");
            condition.Add("MJTZ", "亩均投资");
            condition.Add("SJGM", "建设规模");
            condition.Add("JSDW", "决算单位");
            condition.Add("SCDW", "决算审查单位");
            condition.Add("SCDASJ", "审查定案时间");
            condition.Add("SCDAWH", "审查定案文号");


            AppendErrorMessage(sb, dtOperation.ColumnNullCheck(dtJsxx, condition, NotNullFormat));

            //验证需要的文件是否都已经上传。
            if (wfResult == WfResult.Agree) AppendErrorMessage(sb, FileValidation(workflowId, itemCode, WorkFlowNode.JueSuan, null));
            return sb.ToString();
        }
    }
}
