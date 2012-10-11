using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HHSoft.FieldProtect.DataEntity;
using HHSoft.FieldProtect.Business.ItemManage;
using HHSoft.FieldProtect.Business.Common;

namespace HHSoft.FieldProtect.Business.Workflow
{
    public class WorkflowCyValidation : WorkflowValidation
    {
        /// <summary>
        /// 验证方法。
        /// </summary>
        /// <param name="workflowId">流程Id。</param>
        /// <param name="itemCode">项目编号。</param>
        /// <returns>错误信息。</returns>
        public override string Validation(string workflowId, string itemCode, WfResult wfResult)
        {
            StringBuilder sb = new StringBuilder();

            DataTable dtYsxx = new BusiItemManage_YS().QueryYsxx(itemCode);
            AppendErrorMessage(sb, dtOperation.RowNullCheck(dtYsxx, "初验信息", NotNullFormat));
            condition.Clear();
            condition.Add("CYDW", "初验信息-初验单位");
            condition.Add("CYSJ", "初验信息-初验时间");
            
            AppendErrorMessage(sb, dtOperation.ColumnNullCheck(dtYsxx, condition, NotNullFormat));

            //验证需要的文件是否都已经上传。
            if (wfResult == WfResult.Agree) AppendErrorMessage(sb, FileValidation(workflowId, itemCode, WorkFlowNode.ChuYan, null));

            return sb.ToString();
        }
    }
}
