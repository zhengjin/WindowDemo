using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HHSoft.FieldProtect.Business.ItemManage;
using HHSoft.FieldProtect.DataEntity;
using HHSoft.FieldProtect.Business.Common;

namespace HHSoft.FieldProtect.Business.Workflow
{
    public class WorkflowZyValidation : WorkflowValidation
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
            AppendErrorMessage(sb, dtOperation.RowNullCheck(dtYsxx, "终验信息", NotNullFormat));
            condition.Clear();
            condition.Add("ZYDW", "终验信息-终验单位");
            condition.Add("ZYSJ", "终验信息-终验时间");
            condition.Add("JSFHDW", "终验信息-复核单位");
            condition.Add("JSFHSJ", "终验信息-复核时间");
            AppendErrorMessage(sb, dtOperation.ColumnNullCheck(dtYsxx, condition, NotNullFormat));

            if (wfResult == WfResult.Agree)
            {
                if (string.IsNullOrEmpty(dtYsxx.Rows[0]["YSWH"].ToString()))
                {
                    AppendErrorMessage(sb, "\"终验信息-验收文号\"必须填写");
                }
                else
                {
                    string[] strAry = dtYsxx.Rows[0]["YSWH"].ToString().Split('|');
                    foreach (string s in strAry)
                    {
                        if (string.IsNullOrEmpty(s))
                        {
                            AppendErrorMessage(sb, "\"终验信息-验收文号\"填写不完整");
                            break;
                        }
                    }
                }
            }


            ////工程信息
            AppendErrorMessage(sb, GcxxValidation(itemCode, ItemStage.YanShou, 1));

            //验证需要的文件是否都已经上传。
            if (wfResult == WfResult.Agree) AppendErrorMessage(sb, FileValidation(workflowId, itemCode, WorkFlowNode.ZhongYan, null));
            
            return sb.ToString();
        }
    }
}
