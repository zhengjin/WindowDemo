using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HHSoft.FieldProtect.Framework.Control
{
    [ToolboxData("<{0}:HHGridView AutoGenerateColumns='False' Width='100%' HorizontalAlign='Left' runat='server'> <RowStyle CssClass='RowStyle' Height='24px' /> <FooterStyle BackColor='LightGray' BorderStyle='Solid' BorderWidth='1px' /><SelectedRowStyle CssClass='SelectedRowStyle' /><HeaderStyle CssClass='HeaderStyle' Height='24px' /><AlternatingRowStyle CssClass='AlternateRowStyle' /></{0}:HHGridView >")]

    public class HHGridView : GridView
    {
        public HHGridView()
            : base()
        {

        }
        private int[] mergecolumn = null;


        public int[] MergeColumn
        {
            get { return mergecolumn; }
            set { mergecolumn = value; }
        }

        /// <summary>
        /// 绑定数据源后分组合并
        /// </summary>
        public void DataBindAndGroup()
        {
            this.DataBind();
            if (mergecolumn != null)
                GroupRows(mergecolumn);
            //else
            //    GroupGridViewRows();
        }

        ///// <summary>
        ///// 分组
        ///// </summary>
        //private void GroupGridViewRows()
        //{
        //    foreach (DataControlField field in this.Columns)
        //    {
        //        GroupBoundField gfield = field as GroupBoundField;
        //        if (gfield != null && gfield.IsGroup)
        //        {
        //            this.GroupRows(this.Columns.IndexOf(gfield));
        //        }
        //    }
        //}
        /// <summary>
        /// 合并GridView列中相同的行(Add By Caochen)
        /// </summary>
        /// <param name="MergeColumnStr">需要合并列的字符串(1,2,3)</param>
        private void GroupRows(int[] MergeColumn)
        {
            int i = 0, rowSpanNum = 1;

            while (i < this.Rows.Count - 1)
            {
                GridViewRow gvr = this.Rows[i];

                for (++i; i < this.Rows.Count; i++)
                {
                    GridViewRow gvrNext = this.Rows[i];

                    string PreText = string.Empty;
                    string NextText = string.Empty;

                    foreach (int mergecol in MergeColumn)
                    {
                        PreText += gvr.Cells[mergecol].Text.Trim();
                        NextText += gvrNext.Cells[mergecol].Text.Trim();
                    }

                    if (PreText == NextText)
                    {
                        foreach (int mergecol in MergeColumn)
                        {
                            gvrNext.Cells[mergecol].Visible = false;
                        }
                        rowSpanNum++;
                    }
                    else
                    {
                        foreach (int mergecol in MergeColumn)
                        {
                            gvr.Cells[mergecol].RowSpan = rowSpanNum;
                        }
                        rowSpanNum = 1;
                        break;
                    }

                    if (i == this.Rows.Count - 1)
                    {
                        foreach (int mergecol in MergeColumn)
                        {
                            gvr.Cells[mergecol].RowSpan = rowSpanNum;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 合并GridView列中相同的行
        /// </summary>
        /// <param name="CellNum">需要合并列的索引</param>
        private void GroupRows(int CellNum)
        {
            int i = 0, rowSpanNum = 1;

            while (i < this.Rows.Count - 1)
            {
                GridViewRow gvr = this.Rows[i];

                for (++i; i < this.Rows.Count; i++)
                {
                    GridViewRow gvrNext = this.Rows[i];

                    if (gvr.Cells[CellNum].Text == gvrNext.Cells[CellNum].Text)
                    {
                        gvrNext.Cells[CellNum].Visible = false;
                        rowSpanNum++;
                    }
                    else
                    {
                        gvr.Cells[CellNum].RowSpan = rowSpanNum;
                        rowSpanNum = 1;
                        break;
                    }

                    if (i == this.Rows.Count - 1)
                    {
                        gvr.Cells[CellNum].RowSpan = rowSpanNum;
                    }
                }
            }
        }

        /// <summary>
        /// 合并GridView列中相同的行
        /// </summary>        
        /// <param name="CellNum">需要合并列的索引</param>
        /// <param name="ConditionCellNum">条件列的索引</param>
        private void GroupRows(int CellNum, int ConditionCellNum)
        {
            int i = 0, rowSpanNum = 1;
            while (i < this.Rows.Count - 1)
            {
                GridViewRow gvr = this.Rows[i];
                for (++i; i < this.Rows.Count; i++)
                {
                    GridViewRow gvrNext = this.Rows[i];
                    if (gvr.Cells[CellNum].Text + gvr.Cells[ConditionCellNum].Text == gvrNext.Cells[CellNum].Text + gvrNext.Cells[ConditionCellNum].Text)
                    {
                        gvrNext.Cells[CellNum].Visible = false;
                        rowSpanNum++;
                    }
                    else
                    {
                        gvr.Cells[CellNum].RowSpan = rowSpanNum;
                        rowSpanNum = 1;
                        break;
                    }

                    if (i == this.Rows.Count - 1)
                    {
                        gvr.Cells[CellNum].RowSpan = rowSpanNum;
                    }
                }
            }
        }
    }
}
