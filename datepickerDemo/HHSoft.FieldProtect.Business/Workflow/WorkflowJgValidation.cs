using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity;
using System.Data;
using HHSoft.FieldProtect.Business.ItemManage;

namespace HHSoft.FieldProtect.Business.Workflow
{
    public class WorkflowJgValidation : WorkflowValidation
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
            
            //验证需要的文件是否都已经上传。
            AppendErrorMessage(sb, FileValidation(workflowId, itemCode, WorkFlowNode.JunGong, null));

            AppendErrorMessage(sb, ValidationYsxx(itemCode));

            return sb.ToString();
        }
        /// <summary>
        /// 验证验收表信息
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string ValidationYsxx(string itemCode)
        {
            StringBuilder sb = new StringBuilder();

            //验证验收表信息。
            DataTable dtYS = new BusiItemManage_YS().QueryYsxx(itemCode);            
            condition.Clear();
            condition.Add("YSSQDW", "验收申请单位");
            condition.Add("YSSQSJ", "验收申请时间");

            AppendErrorMessage(sb, dtOperation.ColumnNullCheck(dtYS, condition, NotNullFormat));
            
            return sb.ToString();
        }

        public string CheckJgFile(string workflowId, string itemCode)
        {
            StringBuilder sb = new StringBuilder();

            //验证竣工的文件是否都已经上传。
            List<string> fileCodes = new List<string>();
            fileCodes.Add(((int)FileCode.标段竣工报告).ToString());
            fileCodes.Add(((int)FileCode.项目竣工报告).ToString());
            fileCodes.Add(((int)FileCode.监理总结报告).ToString());
            AppendErrorMessage(sb, FileValidation(workflowId, itemCode, WorkFlowNode.JunGong, fileCodes));

            return sb.ToString();
        }
    }
}
