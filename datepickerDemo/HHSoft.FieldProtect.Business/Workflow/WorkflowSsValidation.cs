using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity;
using System.Data;
using HHSoft.FieldProtect.Business.ItemManage;
using HHSoft.FieldProtect.Business.Common;

namespace HHSoft.FieldProtect.Business.Workflow
{
    public class WorkflowSsValidation : WorkflowValidation
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

            //验证承担单位信息。
            AppendErrorMessage(sb, ValidationCddw(itemCode));

            //验证招投标信息。
            AppendErrorMessage(sb, ValidationZtb(workflowId, itemCode, false));

            //验证工程监理信息。
            AppendErrorMessage(sb, ValidationJl(workflowId, itemCode));

            //验证变更信息。
            AppendErrorMessage(sb, ValidationBg(itemCode));

            //验证进度跟踪信息。
            //AppendErrorMessage(sb, ValidationJzxx(itemCode));

            //验证需要的文件是否都已经上传。
            AppendErrorMessage(sb, FileValidation(workflowId, itemCode, WorkFlowNode.ShiShi, null));

            return sb.ToString();
        }

        /// <summary>
        /// 验证招投标。
        /// </summary>
        /// <param name="workflowId">流程Id。</param>
        /// <param name="itemCode">项目编号。</param>
        /// <param name="validationFile">是否验证文件。</param>
        /// <returns>错误信息。</returns>
        public string ValidationZtb(string workflowId, string itemCode, bool validationFile)
        {
            StringBuilder sb = new StringBuilder();

            //验证招投标信息。
            DataTable dtZtb = new BusiItemManage_SS().QueryZtbData(itemCode);
            AppendErrorMessage(sb, dtOperation.RowNullCheck(dtZtb, "招投标信息-基本信息", NotNullFormat));
            condition.Clear();
            condition.Add("KBSJ", "招投标信息-开标时间");
            condition.Add("GGSJ", "招投标信息-公告时间");
            condition.Add("KGSJ", "工程进度跟踪-开工时间");
            condition.Add("JGSJ", "工程进度跟踪-完工时间");
            condition.Add("ZBRY", "招投标信息-基本信息-招标人员");
            condition.Add("JBRY", "招投标信息-基本信息-监标人员");
            AppendErrorMessage(sb, dtOperation.ColumnNullCheck(dtZtb, condition, NotNullFormat));

            if (validationFile)
            {
                //验证需要的文件是否都已经上传。
                List<string> fileCodes = new List<string>();
                fileCodes.Add(((int)FileCode.标书).ToString());
                fileCodes.Add(((int)FileCode.招标公告).ToString());
                AppendErrorMessage(sb, FileValidation(workflowId, itemCode, WorkFlowNode.ShiShi, fileCodes));
            }

            //验证招标代理机构。
            DataTable dtZbdljg = new BusiItemManage().QueryXmdw(itemCode, ItemCompanyType.ZB);
            AppendErrorMessage(sb, dtOperation.RowNullCheck(dtZbdljg, "招投标信息-招标代理机构", NotNullFormat));
            condition.Clear();
            condition.Add("NAME", "招投标信息-招标代理机构-机构名称");
            condition.Add("CODE", "招投标信息-招标代理机构-机构代码");
            condition.Add("LINKPHONE", "招投标信息-招标代理机构-联系电话");
            condition.Add("LINKMAN", "招投标信息-招标代理机构-联系人员");
            AppendErrorMessage(sb, dtOperation.ColumnNullCheck(dtZbdljg, condition, NotNullFormat));

            //验证中标情况。
            DataTable dtZbqk = new BusiItemManage_SS().QueryZtbZbqk(itemCode);
            AppendErrorMessage(sb, dtOperation.RowNullCheck(dtZbqk, "招投标信息-中标情况", NotNullFormat));

            //验证施工合同。
            DataTable dtSght = new BusiItemManage_SS().QueryZtbSght(itemCode);
            AppendErrorMessage(sb, dtOperation.RowNullCheck(dtSght, "招投标信息-施工合同", NotNullFormat));

            return sb.ToString();
        }

        /// <summary>
        /// 验证工程监理信息。
        /// </summary>
        /// <param name="workflowId">流程Id。</param>
        /// <param name="itemCode">项目编号。</param>
        /// <returns>错误信息。</returns>
        public string ValidationJl(string workflowId, string itemCode)
        {
            StringBuilder sb = new StringBuilder();

            //验证监理单位。
            DataTable dtJldw = new BusiItemManage().QueryXmdw(itemCode, ItemCompanyType.JL);
            AppendErrorMessage(sb, dtOperation.RowNullCheck(dtJldw, "工程监理-监理单位", NotNullFormat));
            condition.Clear();
            condition.Add("NAME", "工程监理信息-监理单位-机构名称");
            condition.Add("CODE", "工程监理信息-监理单位-机构代码");
            condition.Add("LINKPHONE", "工程监理信息-监理单位-联系电话");
            condition.Add("LINKMAN", "工程监理信息-监理单位-联系人员");
            AppendErrorMessage(sb, dtOperation.ColumnNullCheck(dtJldw, condition, NotNullFormat));

            //验证监理人员。
            DataTable dtJlry = new BusiItemManage_SS().QueryGcjlJlry(itemCode);
            AppendErrorMessage(sb, dtOperation.RowNullCheck(dtJlry, "工程监理-监理人员", NotNullFormat));

            //验证监理合同。
            DataTable dtJlht = new BusiItemManage_SS().QueryGcjlJlht(itemCode);
            AppendErrorMessage(sb, dtOperation.RowNullCheck(dtJlht, "工程监理-监理合同", NotNullFormat));

            return sb.ToString();
        }

        public string ValidationBg(string itemCode)
        {
            StringBuilder sb = new StringBuilder();

            //验证变更是否都完成。
            bool have = new BusiItemManage_SS().HaveUncompletedBgxx(itemCode);
            if (have)
            {
                AppendErrorMessage(sb, "项目变更未完成！");
            }

            return sb.ToString();
        }

        public string ValidationJzxx(string itemCode)
        {
            StringBuilder sb = new StringBuilder();

            //验证进度跟踪是否填写，并且是否完工。
            DataTable dtJzxx = new BusiItemManage_SS().QueryJzxx(itemCode);
            AppendErrorMessage(sb, dtOperation.RowNullCheck(dtJzxx, "进度跟踪", NotNullFormat));
            condition.Clear();
            condition.Add("WGSJ", "进度跟踪-是否完工");
            AppendErrorMessage(sb, dtOperation.ColumnNullCheck(dtJzxx, condition, NotNullFormat));

            return sb.ToString();
        }

        public string ValidationCddw(string itemCode)
        {
            StringBuilder sb = new StringBuilder();

            //验证承担单位是否填写。
            DataTable dtCddw = new BusiItemManage().QueryXmdw(itemCode, ItemCompanyType.CD);
            AppendErrorMessage(sb, dtOperation.RowNullCheck(dtCddw, "承担单位信息", NotNullFormat));

            return sb.ToString();
        }
    }
}
