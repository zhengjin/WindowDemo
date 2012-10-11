using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.Business.ItemManage;
using System.Data;
using HHSoft.FieldProtect.DataEntity;

namespace HHSoft.FieldProtect.Business.Workflow
{
    public class WorkflowGhvalidation : WorkflowValidation
    {
        public override string Validation(string workflowId, string itemCode, WfResult wfResult)
        {
            StringBuilder sb = new StringBuilder();


            DataTable dtGh = new BusiItemManage_GHYS().QueryData(itemCode);
            AppendErrorMessage(sb, dtOperation.RowNullCheck(dtGh, "规划预算基本信息", NotNullFormat));

            DataTable dtGhdw = new BusiItemManage().QueryXmdw(itemCode, ItemCompanyType.GH);
            AppendErrorMessage(sb, dtOperation.RowNullCheck(dtGhdw, "规划设计单位", NotNullFormat));

            condition.Clear();
            condition.Add("NAME", "规划设计单位-单位名称");
            condition.Add("CODE", "规划设计单位-机构代码");
            condition.Add("LINKPHONE", "规划设计单位-联系电话");
            condition.Add("LINKMAN", "规划设计单位-联系人员");
            AppendErrorMessage(sb, dtOperation.ColumnNullCheck(dtGhdw, condition, NotNullFormat));

            AppendErrorMessage(sb, GcxxValidation(itemCode, ItemStage.GuiHua, 1));

            AppendErrorMessage(sb, MoneyValidation(itemCode, "项目预算", WorkFlowNode.GHSJYS, 1));
                  
            AppendErrorMessage(sb, FileValidation(workflowId, itemCode, WorkFlowNode.GHSJYS, null));
           
            return sb.ToString();
        }
    }
}
