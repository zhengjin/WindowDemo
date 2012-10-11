using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using HHSoft.FieldProtect.DataEntity.CustomAttribute;

namespace HHSoft.FieldProtect.Business.Common
{
    public static class SqlBuilder
    {
        /// <summary>
        /// 构建指定表的所有列名和列对象的字典信息。用于提高效率。
        /// </summary>
        /// <param name="tableType">表实体类型。</param>
        /// <returns>列名和列对象的字典。</returns>
        private static Dictionary<string, PropertyInfo> BuildColumnInfos(Type tableType)
        {
            Dictionary<string, PropertyInfo> columnInfos = new Dictionary<string, PropertyInfo>();
            foreach (var propertyInfo in tableType.GetProperties())
            {
                //此处可以考虑过滤非数据库列的属性。
                columnInfos.Add(propertyInfo.Name, propertyInfo);
            }
            return columnInfos;
        }

        /// <summary>
        /// 构建列名列值字典。
        /// </summary>
        /// <typeparam name="T">实体类型。</typeparam>
        /// <param name="columnInfos">列信息字典。</param>
        /// <param name="item">实体对象。</param>
        /// <returns>列名列值字典。</returns>
        private static Dictionary<string, string> BuildColumnNamesAndValues<T>(Dictionary<string, PropertyInfo> columnInfos, T item)
        {
            Dictionary<string, string> columnNamesAndValues = new Dictionary<string, string>();
            foreach (var columnInfo in columnInfos)
            {
                PropertyInfo propertyInfo = columnInfo.Value;

                //判断是否为数据库字段。
                var dbColumnAttributes = propertyInfo.GetCustomAttributes(typeof(IsDbColumnAttribute), true);
                if (dbColumnAttributes.Length == 0 || ((IsDbColumnAttribute)dbColumnAttributes[0]).IsDbColumn)
                {
                    object value = columnInfo.Value.GetValue(item, null);
                    string valueString;
                    if (columnInfo.Value.PropertyType == typeof(DateTime) || columnInfo.Value.PropertyType == typeof(DateTime?))
                    {
                        valueString = value == null ? "'" + string.Empty + "'" : "to_date('" + value.ToString() + "','yyyy-mm-dd hh24:mi:ss')";
                    }
                    else if (columnInfo.Value.PropertyType.IsEnum)
                    {
                        var enumValueAttributes = propertyInfo.GetCustomAttributes(typeof(EnumValueColumnAttribute), true);
                        if (enumValueAttributes.Length == 0 || ((EnumValueColumnAttribute)enumValueAttributes[0]).IsEnumValueColumn)
                        {
                            valueString = value == null ? string.Empty : value.GetHashCode().ToString();
                        }
                        else
                        {
                            valueString = value == null ? string.Empty : value.ToString();
                        }
                        valueString = "'" + valueString + "'";
                    }//判断是否为枚举值字段。
                    else
                    {
                        valueString = "'" + (value == null ? string.Empty : value.ToString().Replace("'", "''")) + "'";
                    }
                    columnNamesAndValues.Add(columnInfo.Key, valueString);
                }
            }
            return columnNamesAndValues;
        }

        /// <summary>
        /// 检测对象属性是否是主键。
        /// </summary>
        /// <param name="propertyInfo">属性信息对象。</param>
        /// <returns>是否是主键。</returns>
        private static bool CheckPk(PropertyInfo propertyInfo)
        {
            var attributes = propertyInfo.GetCustomAttributes(typeof(PkAttribute), true);
            if (attributes.Length == 0 || !((PkAttribute)attributes[0]).IsPk)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 构建Insert Sql语句。
        /// </summary>
        /// <typeparam name="T">要插入实体对象的类型。</typeparam>
        /// <param name="item">要插入的实体对象。</param>
        /// <returns>Insert Sql语句。</returns>
        public static string BuildInsertSql<T>(T item)
        {
            Type tableType = item.GetType();
            Dictionary<string, PropertyInfo> columnInfos = BuildColumnInfos(tableType);

            //获取列名与值得字典。为后期拼接Sql语句使用。
            Dictionary<string, string> columnNamesAndValues = BuildColumnNamesAndValues(columnInfos, item);

            //拼接Sql语句。
            StringBuilder sbInsert = new StringBuilder();
            sbInsert.AppendLine("insert into");
            sbInsert.AppendLine(tableType.Name.Split('`')[0]);
            sbInsert.AppendLine("(" + string.Join(",", columnNamesAndValues.Keys.ToArray()) + ")");
            sbInsert.AppendLine("values");
            sbInsert.AppendLine("(" + string.Join(",", columnNamesAndValues.Values.ToArray()) + ")");
            return sbInsert.ToString();
        }

        /// <summary>
        /// 构建Update Sql语句。
        /// </summary>
        /// <typeparam name="T">要更新实体对象的类型。</typeparam>
        /// <param name="item">要插入的实体对象。</param>
        /// <returns>Update Sql语句。</returns>
        public static string BuildUpdateSql<T>(T item)
        {
            Type tableType = item.GetType();
            Dictionary<string, PropertyInfo> columnInfos = BuildColumnInfos(tableType);

            //获取列名与值得字典。为后期拼接Sql语句使用。
            Dictionary<string, string> columnNamesAndValues = BuildColumnNamesAndValues(columnInfos, item);

            StringBuilder sbSet = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();
            sbWhere.AppendLine("1=1");
            foreach (var columnNameAndValue in columnNamesAndValues)
            {
                if (CheckPk(columnInfos[columnNameAndValue.Key]))
                {
                    if (columnNameAndValue.Value == "''")
                    {
                        sbWhere.AppendLine("and " + columnNameAndValue.Key + " is null");
                    }
                    else
                    {
                        sbWhere.AppendLine("and " + columnNameAndValue.Key + "=" + columnNameAndValue.Value);
                    }
                }
                else
                {
                    if (sbSet.Length != 0)
                    {
                        sbSet.Append(",");
                    }
                    sbSet.AppendLine(columnNameAndValue.Key + "=" + columnNameAndValue.Value);
                }
            }

            //拼接Sql语句。
            StringBuilder sbUpdate = new StringBuilder();
            sbUpdate.AppendLine("update " + tableType.Name + " set");
            sbUpdate.AppendLine(sbSet.ToString());
            sbUpdate.AppendLine("where");
            sbUpdate.AppendLine(sbWhere.ToString());
            return sbUpdate.ToString();
        }

        /// <summary>
        /// 构建Delete Sql语句。
        /// </summary>
        /// <typeparam name="T">要删除实体对象的类型。</typeparam>
        /// <param name="item">要插入的实体对象。</param>
        /// <returns>Delete Sql语句。</returns>
        public static string BuildDeleteSql<T>(T item)
        {
            Type tableType = item.GetType();
            Dictionary<string, PropertyInfo> columnInfos = BuildColumnInfos(tableType);

            //获取列名与值得字典。为后期拼接Sql语句使用。
            Dictionary<string, string> columnNamesAndValues = BuildColumnNamesAndValues(columnInfos, item);

            StringBuilder sbWhere = new StringBuilder("1=1");
            foreach (var columnNameAndValue in columnNamesAndValues)
            {
                if (CheckPk(columnInfos[columnNameAndValue.Key]))
                {
                    sbWhere.AppendLine("and " + columnNameAndValue.Key + "=" + columnNameAndValue.Value);
                }
            }

            StringBuilder sbDelete = new StringBuilder();
            sbDelete.AppendLine("delete from " + tableType.Name);
            sbDelete.AppendLine("where " + sbWhere.ToString());
            return sbDelete.ToString();
        }

        /// <summary>
        /// 构建Delete Sql语句。
        /// </summary>
        /// <typeparam name="T">要删除实体对象的类型。</typeparam>
        /// <param name="whereConditions">Where条件字典。</param>
        /// <returns>Delete Sql语句。</returns>
        public static string BuildDeleteSql<T>(Dictionary<string, string> whereConditions)
        {
            Type tableType = typeof(T);
            Dictionary<string, PropertyInfo> columnInfos = BuildColumnInfos(tableType);

            StringBuilder sbWhere = new StringBuilder("1=1");
            foreach (var whereCondition in whereConditions)
            {
                sbWhere.AppendLine("and " + whereCondition.Key + "='" + whereCondition.Value + "'");
            }

            StringBuilder sbDelete = new StringBuilder();
            sbDelete.AppendLine("delete from " + tableType.Name.Split('`')[0]);
            sbDelete.AppendLine("where " + sbWhere.ToString());
            return sbDelete.ToString();
        }
    }
}
