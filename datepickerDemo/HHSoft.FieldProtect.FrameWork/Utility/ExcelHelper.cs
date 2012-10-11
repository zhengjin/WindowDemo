using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHSoft.FieldProtect.Framework.Control;

namespace HHSoft.FieldProtect.Framework.Utility
{
    /// <summary>
    /// Excel帮助类
    /// </summary>
    public class ExcelHelper
    {
        /// <summary>
        /// 定义数字型列的样式,防止数字导出后变成科学计数法
        /// </summary>
        private static string style = @"<style> .text { mso-number-format:\@; } </style> ";

        /// <summary>
        /// 定义输入Excel的文件名称
        /// </summary>
        private static string fileName
        {
            get
            {
                return string.Format("{0}.xls", DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString()
                    + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString());

            }
        }

        /// <summary>
        /// 数字列的数组
        /// </summary>
        private static int[] numberCell = null;

        /// <summary>
        /// 设置数字型列的数组
        /// </summary>
        public static int[] NumberCell
        {
            get
            {
                return numberCell;
            }

            set
            {
                numberCell = value;
            }
        }

        /// <summary>
        /// 导出数据到Excel
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="dataList">IList</param>
        /// <param name="fields">要导出的字段</param>
        /// <param name="headTexts">字段对应显示的名称</param>
        /// <param name="title">表头</param>
        /// <param name="footer">表尾</param>
        public static void ExportToExcel<T>(IList<T> dataList, string[] fields, string[] headTexts, string title, string[] footer)
        {
            GridView gvw = new GridView();

            gvw.RowDataBound += new GridViewRowEventHandler(Gvw_RowDataBound);
            int colCount, i;
            ////如果筛选的字段和对应的列头名称个数相对的情况下只导出指定的字段
            if (fields.Length != 0 && fields.Length == headTexts.Length)
            {
                colCount = fields.Length;
                gvw.AutoGenerateColumns = false;

                for (i = 0; i < colCount; i++)
                {
                    BoundField bf = new BoundField();
                    bf.DataField = fields[i];
                    bf.HeaderText = headTexts[i];
                    gvw.Columns.Add(bf);
                }
            }
            else
            {
                gvw.AutoGenerateColumns = true;
            }

            SetStype(gvw);
            gvw.DataSource = dataList;
            gvw.DataBind();

            ExportToExcel(gvw, title, footer);
        }

        /// <summary>
        /// 导出数据到Excel
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="dataList">dataList</param>
        /// <param name="fields">要导出的字段</param>
        /// <param name="headTexts">字段对应显示的名称</param>
        public static void ExportToExcel<T>(IList<T> dataList, string[] fields, string[] headTexts)
        {
            ExportToExcel(dataList, fields, headTexts, string.Empty, null);
        }

        /// <summary>
        /// 导出数据到Excel
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="dataList">dataList</param>
        /// <param name="fields">要导出的字段</param>
        /// <param name="headTexts">字段对应显示的名称</param>
        /// <param name="title">表头</param>
        public static void ExportToExcel<T>(IList<T> dataList, string[] fields, string[] headTexts, string title)
        {
            ExportToExcel(dataList, fields, headTexts, title, null);
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="dataList">DataTable</param>
        /// <param name="fields">要导出的字段</param>
        /// <param name="headTexts">字段对应显示的名称</param>
        /// <param name="title">表头</param>
        /// <param name="footer">表尾</param>
        public static void ExportToExcel(DataTable dataList, string[] fields, string[] headTexts, string title, string[] footer)
        {
            GridView gvw = new GridView();

            gvw.RowDataBound += new GridViewRowEventHandler(Gvw_RowDataBound);
            int colCount, i;
            ////如果筛选的字段和对应的列头名称个数相对的情况下只导出指定的字段
            if (fields.Length != 0 && fields.Length == headTexts.Length)
            {
                colCount = fields.Length;
                gvw.AutoGenerateColumns = false;

                for (i = 0; i < colCount; i++)
                {
                    BoundField bf = new BoundField();
                    bf.DataField = fields[i];
                    bf.HeaderText = headTexts[i];
                    gvw.Columns.Add(bf);
                }
            }
            else
            {
                gvw.AutoGenerateColumns = true;
            }

            SetStype(gvw);
            gvw.DataSource = dataList;
            gvw.DataBind();

            ExportToExcel(gvw, title, footer);
        }

        /// <summary>
        /// 导出数据到Excel
        /// </summary>
        /// <param name="dataList">DataTable Data</param>
        /// <param name="fields">要导出的字段</param>
        /// <param name="headTexts">字段对应显示的名称</param>
        /// <param name="title">标题</param>
        public static void ExportToExcel(DataTable dataList, string[] fields, string[] headTexts, string title)
        {
            ExportToExcel(dataList, fields, headTexts, title, null);
        }

        /// <summary>
        /// 导出数据到Excel
        /// </summary>
        /// <param name="dataList">DataTable Data</param>
        /// <param name="fields">要导出的字段</param>
        /// <param name="headTexts">字段对应显示的名称</param>
        public static void ExportToExcel(DataTable dataList, string[] fields, string[] headTexts)
        {
            ExportToExcel(dataList, fields, headTexts, string.Empty, null);
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="gv">GridView</param>
        public static void GridViewToExcel(GridView gv)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Charset = "GB2312";
            HttpContext.Current.Response.Write("<meta http-equiv=Content-Type content=text/html;charset=GB2312>");
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            HttpContext.Current.Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlTextWriter = new System.Web.UI.HtmlTextWriter(stringWriter);
            gv.RenderControl(htmlTextWriter);
            HttpContext.Current.Response.Output.Write(stringWriter.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="gv">HHGridView</param>
        public static void HHGridViewToExcel(HHGridView gv)
        {
            HHGridViewToExcel(gv, string.Empty);
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="gv">HHGridView</param>
        /// <param name="title">表头</param>
        public static void HHGridViewToExcel(HHGridView gv, string title)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Charset = "GB2312";
            HttpContext.Current.Response.Write("<meta http-equiv=Content-Type content=text/html;charset=GB2312>");
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            HttpContext.Current.Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlTextWriter = new System.Web.UI.HtmlTextWriter(stringWriter);
            gv.RenderControl(htmlTextWriter);
            if (!string.IsNullOrEmpty(title))
            {
                HttpContext.Current.Response.Write("<b><center><font size=3 face=Verdana>" + title + "</font></center></b>");
            }

            HttpContext.Current.Response.Output.Write(stringWriter.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="dg">DataGrid</param>
        public static void DataGridToExcel(DataGrid dg)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Charset = "GB2312";
            HttpContext.Current.Response.Write("<meta http-equiv=Content-Type content=text/html;charset=GB2312>");
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            HttpContext.Current.Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlTextWriter = new System.Web.UI.HtmlTextWriter(stringWriter);
            dg.RenderControl(htmlTextWriter);
            HttpContext.Current.Response.Output.Write(stringWriter.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="dg">DataGrid</param>
        public static void DataGridToExcel(DataGrid dg,string title,string footer)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Charset = "GB2312";
            HttpContext.Current.Response.Write("<meta http-equiv=Content-Type content=text/html;charset=GB2312>");
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            HttpContext.Current.Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlTextWriter = new System.Web.UI.HtmlTextWriter(stringWriter);
            dg.RenderControl(htmlTextWriter);

            if (!string.IsNullOrEmpty(title))
            {
                HttpContext.Current.Response.Write("<b><center><font size=3 face=Verdana>" + title + "</font></center></b>");
            }

            HttpContext.Current.Response.Write(stringWriter.ToString());
            if (footer != null)
            {
                HttpContext.Current.Response.Write("<br><font size=2>" + footer.ToString() + "</font>");
            }
    
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="re">Repeater</param>
        public static void RepeatToExcel(Repeater re)
        {
            RepeatToExcel(re, string.Empty);
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        /// <param name="re">Repeater</param>
        /// <param name="title">表头</param>
        public static void RepeatToExcel(Repeater re, string title)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Charset = "GB2312";
            HttpContext.Current.Response.Write("<meta http-equiv=Content-Type content=text/html;charset=GB2312>");
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            HttpContext.Current.Response.ContentType = "application/vnd.xls";
            System.IO.StringWriter stringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htmlTextWriter = new System.Web.UI.HtmlTextWriter(stringWriter);
            re.RenderControl(htmlTextWriter);
            if (!string.IsNullOrEmpty(title))
            {
                HttpContext.Current.Response.Write("<b><center><font size=3 face=Verdana>" + title + "</font></center></b>");
            }

            HttpContext.Current.Response.Output.Write(stringWriter.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 绑定实际
        /// </summary>
        /// <param name="sender">对象</param>
        /// <param name="e">参数</param>
        protected static void Gvw_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow && numberCell != null)
            {
                for (int i = 0; i < numberCell.Length; i++)
                {
                    e.Row.Cells[numberCell[i]].Attributes.Add("class", "text");
                }
            }
        }

        /// <summary>
        /// 设置样式
        /// </summary>
        /// <param name="gvw">GridView</param>
        private static void SetStype(GridView gvw)
        {
            gvw.Font.Name = "Verdana";
            gvw.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
            ////gvw.HeaderStyle.BackColor = System.Drawing.Color.LightCyan;
            gvw.HeaderStyle.ForeColor = System.Drawing.Color.Black;
            gvw.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
            gvw.HeaderStyle.Wrap = false;
            gvw.HeaderStyle.Font.Bold = true;
            gvw.HeaderStyle.Font.Size = 10;
            gvw.RowStyle.Font.Size = 10;
        }

        /// <summary>
        /// 导出GridView中的数据到Excel
        /// </summary>
        /// <param name="gvw">GridView</param>
        /// <param name="title">表头</param>
        /// <param name="footer">表尾</param>
        private static void ExportToExcel(GridView gvw, string title, string[] footer)
        {
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Charset = "GB2312";
            HttpContext.Current.Response.Write("<meta http-equiv=Content-Type content=text/html;charset=GB2312>");
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            HttpContext.Current.Response.ContentType = "application/vnd.xls";
            StringWriter tw = new System.IO.StringWriter();
            HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
            gvw.RenderControl(hw);
            HttpContext.Current.Response.Write(style);
            if (!string.IsNullOrEmpty(title))
            {
                HttpContext.Current.Response.Write("<b><center><font size=3 face=Verdana>" + title + "</font></center></b>");
            }

            HttpContext.Current.Response.Write(tw.ToString());
            if (footer != null)
            {
                for (int i = 0; i < footer.Length; i++)
                {
                    HttpContext.Current.Response.Write("<br><font size=2>" + footer[i].ToString() + "</font>");
                }
            }

            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Close();
            HttpContext.Current.Response.End();
            gvw.Dispose();
            tw.Dispose();
            hw.Dispose();
            gvw = null;
            tw = null;
            hw = null;
        }
    }
}
