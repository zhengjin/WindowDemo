using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HHSoft.FieldProtect.FrameWork.Utility;

namespace HHSoft.FieldProtect.Business.Common
{
    public class DataTableOperation
    {
        /// <summary>
        /// 给DataTable添加Guid列并赋值。
        /// </summary>
        /// <param name="dt">DataTable。</param>
        /// <param name="columnName">列名。</param>
        public void AddGuidColumnAndGuidValue(DataTable dt, string columnName)
        {
            dt.Columns.Add(columnName);
            foreach (DataRow dr in dt.Rows)
            {
                dr[columnName] = Guid.NewGuid();
            }
        }

        /// <summary>
        /// 把DataRow转成实体对象。
        /// </summary>
        /// <param name="dr">DataRow。</param>
        /// <param name="type">实体类型。</param>
        /// <returns>实体对象。</returns>
        public object ConvertFromDataRowToEntity(DataRow dr, Type type)
        {
            object result = Activator.CreateInstance(type);
            foreach (var propertyInfo in type.GetProperties())
            {
                if (dr.Table.Columns.Contains(propertyInfo.Name))
                {
                    propertyInfo.SetValue(result, TypeConvert.Convert(dr[propertyInfo.Name], propertyInfo.PropertyType), null);
                }
            }
            return result;
        }

        /// <summary>
        /// 把DataTable转成实体集合。
        /// </summary>
        /// <typeparam name="T">实体类型。</typeparam>
        /// <param name="dt">DataTable。</param>
        /// <returns>实体集合。</returns>
        public List<T> ConvertFromDataTableToEntities<T>(DataTable dt)
        {
            List<T> result = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                Type itemType = typeof(T);
                T t = (T)Activator.CreateInstance(itemType);
                foreach (var propertyInfo in itemType.GetProperties())
                {
                    string columnName = propertyInfo.Name;
                    if (dt.Columns.Contains(columnName))
                    {
                        propertyInfo.SetValue(t, TypeConvert.Convert(dr[columnName], propertyInfo.PropertyType), null);
                    }
                }
                result.Add(t);
            }
            return result;
        }

        /// <summary>
        /// DataTable空值检测。
        /// </summary>
        /// <param name="dt">DataTable。</param>
        /// <param name="columnInfos">列名和中文名字典。</param>
        /// <param name="errorFormat">错误提示格式。</param>
        /// <returns>错误信息。</returns>
        public string ColumnNullCheck(DataTable dt, Dictionary<string, string> columnInfos, string errorFormat)
        {
            StringBuilder sbError = new StringBuilder();
            foreach (var columnInfo in columnInfos)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    object value = dr[columnInfo.Key];
                    if (value == null || string.IsNullOrEmpty(value.ToString().Trim()))
                    {
                        sbError.AppendLine(string.Format(errorFormat, columnInfo.Value));
                        break;
                    }
                }
            }
            return sbError.ToString();
        }

        public Dictionary<string, List<string>> BuildGcxxColumnGroups()
        {
            Dictionary<string, List<string>> groups = new Dictionary<string, List<string>>();
            groups.Add("建设规模", new List<string>() { "GM_ZGM", "GM_KFGM", "GM_FKGM", "GM_ZLGM", "GM_JBNTZLGM", "GM_XZGDMJ", "GM_XZQTNYDMJ", "GM_ZLQJSYDMJ", "GM_ZLHJSYDMJ", "GM_JSJSYDZJGDMJ" });
            groups.Add("成效信息", new List<string>() { "CX_ZGNTGGMJ", "CX_XGNTFLMJ", "CX_JSQGDDJ", "CX_JSHGDDJ", "CX_XZLSCN", "CX_RJXZNSR", "CX_JSCZSL", "CX_JGZXCSL", "CX_SYRS" });
            groups.Add("工程量信息", new List<string>() { "GC_PZMJ", "GC_WTSF", "GC_TTSF", "GC_YSTSF", "GC_PSGQ", "GC_PSGD", "GC_GGGQ", "GC_GGGD", "GC_PGLYGQ", "GC_PGLYGD", "GC_QIAO", "GC_HAN", "GC_ZHA", "GC_FANG", "GC_TJ", "GC_JDJ", "GC_GJ", "GC_SB", "GC_GYX", "GC_DYX", "GC_BYQ", "GC_SNG", "GC_MG", "GC_TJL", "GC_SCL", "GC_FHLMJ", "GC_ZS" });
            return groups;
        }

        public string ColumnNotNullCheck(DataTable dt, Dictionary<string, List<string>> columnInfoGroups, string errorFormat)
        {
            StringBuilder sbError = new StringBuilder();
            foreach (var columnInfoGroup in columnInfoGroups)
            {
                bool groupNotNull = false;
                foreach (var columnInfo in columnInfoGroup.Value)
                {
                    bool columnNotNull = false;
                    foreach (DataRow dr in dt.Rows)
                    {
                        object value = dr[columnInfo];
                        if (value != null && !string.IsNullOrEmpty(value.ToString()))
                        {
                            columnNotNull = true;
                            break;
                        }
                    }
                    if (columnNotNull)
                    {
                        groupNotNull = true;
                        break;
                    }
                }
                if (!groupNotNull)
                {
                    sbError.AppendLine(string.Format(errorFormat, columnInfoGroup.Key));
                }
            }
            return sbError.ToString();
        }

        /// <summary>
        /// DataTable空检测。
        /// </summary>
        /// <param name="dt">DataTable。</param>
        /// <param name="checkItemName">检测对象名称。</param>
        /// <param name="errorFormat">错误提示格式。</param>
        /// <returns>错误信息。</returns>
        public string RowNullCheck(DataTable dt, string checkItemName, string errorFormat)
        {
            string error = string.Empty;
            if (dt.Rows.Count == 0)
            {
                error = string.Format(errorFormat, checkItemName);
            }
            return error;
        }

        public void ClearDbDataTable(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                dr.Delete();
            }
        }

        public void DeleteDbDataRow(DataRow dr, Action<DataRow> action)
        {
            dr.Delete();
            if (action != null)
            {
                action(dr);
            }
        }

        public void DeleteDbDataRows(IEnumerable<DataRow> drs, Action<DataRow> action)
        {
            foreach (DataRow dr in drs)
            {
                DeleteDbDataRow(dr, action);
            }
        }

        public void AddDbDataRow(DataTable dt, DataRow dr, Action<DataRow> action)
        {
            dt.Rows.Add(dr);
            if (action != null)
            {
                action(dr);
            }
        }
    }
}
