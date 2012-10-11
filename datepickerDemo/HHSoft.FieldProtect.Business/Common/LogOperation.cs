using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity;
using HHSoft.FieldProtect.Framework.Utility;

namespace HHSoft.FieldProtect.Business.Common
{
    public static class LogOperation
    {
        public static void WriteTitle(StringBuilder sbLog, string title)
        {
            sbLog.AppendLine();
            sbLog.AppendLine(title);
            sbLog.AppendLine();
        }

        public static void Append(StringBuilder sbLog, string content)
        {
            sbLog.AppendLine(content);
        }

        public static void WriteExceptionLog(StringBuilder sbLog, Exception e)
        {
            sbLog.AppendLine("出现异常：");
            sbLog.AppendLine(e.Message);
            sbLog.AppendLine(e.Source);
            sbLog.AppendLine(e.StackTrace);
        }

        public static void WriteSyncTableTitle(StringBuilder sbLog, string tableName, SyncType type, string condition)
        {
            sbLog.AppendLine(string.Format("同步表：{0}; 同步方式：{1}; 同步条件：{2};", tableName, EnumHelper.GetFieldDescription(typeof(SyncType), (int)type), condition));
        }
    }
}
