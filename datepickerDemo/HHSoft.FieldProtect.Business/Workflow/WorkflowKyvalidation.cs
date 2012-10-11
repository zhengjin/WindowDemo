using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity;
using HHSoft.FieldProtect.Business.ItemManage;
using System.Data;
using HHSoft.FieldProtect.Business.Common;

namespace HHSoft.FieldProtect.Business.Workflow
{
    public class WorkflowKyvalidation : WorkflowValidation
    {
        public override string Validation(string workflowId, string itemCode, WfResult wfResult)
        {
            StringBuilder sb = new StringBuilder();

            DataTable dtKy = new BusiItemManage_KY().QueryData(itemCode);

            AppendErrorMessage(sb, dtOperation.RowNullCheck(dtKy, "可研基本信息", NotNullFormat));
            condition.Clear();
            condition.Add("ITEMNAME", "项目名称");
            condition.Add("XMLX", "项目类型");
            condition.Add("ZJND", "新增费使用年度");
            condition.Add("JSGQ", "建设工期");
            AppendErrorMessage(sb, dtOperation.ColumnNullCheck(dtKy, condition, NotNullFormat));

            DataTable dtGis = new BusiItemManage_Gis().QueryData(itemCode, (int)ItemStage.KeYan, 1);

            AppendErrorMessage(sb, dtOperation.RowNullCheck(dtGis, "Gis基本信息", NotNullFormat));

            condition.Clear();
            condition.Add("ADDRESS", "项目区行政位置");
            condition.Add("QSJD", "大地坐标-起始经度");
            condition.Add("JZJD", "大地坐标-截止经度");
            condition.Add("QSWD", "大地坐标-起始纬度");
            condition.Add("JZWD", "大地坐标-截止纬度");
            condition.Add("GM", "项目建设规模");
            condition.Add("XZGDMJ", "新增耕地面积");
            condition.Add("MONEY", "资金估算");


            condition.Add("TFTBH", "图幅号(图斑号)");
            condition.Add("DLMJ", "地类");
            condition.Add("QSXZMJ", "权属情况");

            AppendErrorMessage(sb, dtOperation.ColumnNullCheck(dtGis, condition, NotNullFormat));


            DataTable dtKydw = new BusiItemManage().QueryXmdw(itemCode, ItemCompanyType.KY);
            AppendErrorMessage(sb, dtOperation.RowNullCheck(dtKydw, "可研编制单位", NotNullFormat));
            condition.Clear();
            condition.Add("NAME", "可研编制单位-单位名称");
            condition.Add("CODE", "可研编制单位-机构代码");
            condition.Add("LINKPHONE", "可研编制单位-联系电话");
            condition.Add("LINKMAN", "可研编制单位-联系人员");
            AppendErrorMessage(sb, dtOperation.ColumnNullCheck(dtKydw, condition, NotNullFormat));

            AppendErrorMessage(sb, FileValidation(workflowId, itemCode, WorkFlowNode.KY, null));
            return sb.ToString();
        }
    }
}
