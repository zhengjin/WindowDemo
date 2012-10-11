using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity;
using System.Data;
using HHSoft.FieldProtect.Business.ItemManage;

namespace HHSoft.FieldProtect.Business.Workflow
{
    public class WorkflowYsTzvalidation : WorkflowValidation
    {
        public override string Validation(string workflowId, string itemCode, WfResult wfResult)
        {
            StringBuilder sb = new StringBuilder();

            AppendErrorMessage(sb, GcxxValidation(itemCode, ItemStage.GuiHua, 1));

            AppendErrorMessage(sb, MoneyValidation(itemCode, "项目预算", WorkFlowNode.YSTZ, 1));

            return sb.ToString();
        }
    }
}
