using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity;
using System.Data;
using HHSoft.FieldProtect.Business.ItemManage;

namespace HHSoft.FieldProtect.Business.Workflow
{
    public class WorkflowSbValidation : WorkflowValidation
    {
        public override string Validation(string workflowId, string itemCode, WfResult wfResult)
        {
            StringBuilder sb = new StringBuilder();

            DataTable dtSb = new BusiItemManage_SB().QueryData(itemCode);

            condition.Clear();
            condition.Add("ITEMNAME", "项目名称");            
            condition.Add("XMLX", "项目类型");
            condition.Add("SHENG", "项目所在地－省");
            condition.Add("SHI", "项目所在地－市");
            condition.Add("XIAN", "项目所在地－县");
            condition.Add("ZJND", "新增费使用年度");
            AppendErrorMessage(sb, dtOperation.ColumnNullCheck(dtSb, condition, NotNullFormat));

            DataTable dtGis = new BusiItemManage_Gis().QueryData(itemCode, (int)ItemStage.ShenBo, 1);

            AppendErrorMessage(sb, dtOperation.RowNullCheck(dtGis, "Gis基本信息", NotNullFormat));

            condition.Clear();
            condition.Add("ADDRESS", "项目区行政位置");
            condition.Add("QSJD", "大地坐标-起始经度");
            condition.Add("JZJD", "大地坐标-截止经度");
            condition.Add("QSWD", "大地坐标-起始纬度");
            condition.Add("JZWD", "大地坐标-截止纬度");
            condition.Add("GM", "项目建设规模");
            condition.Add("XZGDMJ", "新增耕地面积");
            condition.Add("MONEY", "资金概算");


            condition.Add("TFTBH", "图幅号(图斑号)");           
            condition.Add("DLMJ", "地类");
            condition.Add("QSXZMJ", "权属情况");

            AppendErrorMessage(sb, dtOperation.ColumnNullCheck(dtGis, condition, NotNullFormat));


            //验证需要的文件是否都已经上传。
            AppendErrorMessage(sb, FileValidation(workflowId, itemCode, WorkFlowNode.TB, null));            

            return sb.ToString();

        }
    }
}
