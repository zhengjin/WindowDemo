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
    public class WorkflowGdValidation : WorkflowValidation
    {
        public override string Validation(string workflowId, string itemCode, WfResult wfResult)
        {
            StringBuilder sb = new StringBuilder();

            //验证归档信息。
            DataTable dtZtb = new BusiItemManage_GD().QueryGdxx(itemCode);
            AppendErrorMessage(sb, dtOperation.RowNullCheck(dtZtb, "归档信息", NotNullFormat));

            return sb.ToString();
        }
    }
}
