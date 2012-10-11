using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI;

namespace HHSoft.FieldProtect.Business.Template
{
    public class GridViewItemCodeTemplate : ITemplate
    {
        private string contentColumnName;
        private string separator;
        private QueryType queryType;

        public GridViewItemCodeTemplate(QueryType queryType, string contentColumnName, string separator)
        {
            this.queryType = queryType;
            this.contentColumnName = contentColumnName;
            this.separator = separator;
        }

        #region ITemplate 成员

        public void InstantiateIn(Control container)
        {
            Literal link = new Literal();
            link.DataBinding += (sender, e) =>
            {
                DataControlFieldCell cell = (DataControlFieldCell)container;
                GridViewRow gr = (GridViewRow)container.Parent;
                DataRowView dr = (DataRowView)gr.DataItem;
                string content = dr[contentColumnName].ToString();
                string[] items = content.Split(new string[] { separator }, StringSplitOptions.None);
                if (items.Length == 1)
                {
                    link.Text = items[0];
                }                
                else if (items.Length >= 2)
                {
                    string display = items[0];
                    string itemCodeString = items[1];
                    if (string.IsNullOrEmpty(display) || display == "0")
                    {
                        link.Text = display;
                    }
                    else
                    {
                        cell.ToolTip = itemCodeString;
                        if (this.queryType == QueryType.ZjQuery)
                        {
                            link.Text = "<a href=\"#\" onclick=\"XmxxTotalOpenList('" + itemCodeString + "')\">" + display + "</a>";
                        }
                        if (this.queryType == QueryType.GcxxQuery)
                        {
                            link.Text = "<a href=\"#\" onclick=\"GcxxDetail('" + itemCodeString + "','" + items[2] + "')\">" + display + "</a>";
                        }                        
                    }                   
                }
            };
            container.Controls.Add(link);
        }

        #endregion




    }

    /// <summary>
    /// 查询类型
    /// </summary>
    public enum QueryType
    {
        /// <summary>
        /// 进度、资金查询
        /// </summary>
        ZjQuery = 1,
        /// <summary>
        /// 工程信息查询
        /// </summary>
        GcxxQuery = 2
    }
}
