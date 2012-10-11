using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using HHSoft.FieldProtect.Business.Common;
using HHSoft.FieldProtect.DataAccess;
using HHSoft.FieldProtect.DataEntity;
using HHSoft.FieldProtect.FrameWork.Utility;

namespace HHSoft.FieldProtect.Business.Sync
{
    public class SyncDataSystemInfo : SyncData
    {
        public SyncDataSystemInfo(OracleDbOperation targetDbOperation, OracleDbOperation localDbOperation, string targetCode,
            string localCode, FtpHelper targetFtpOperation, FtpHelper localFtpOperation, StringBuilder sbLog)
            : base(targetDbOperation, localDbOperation, targetCode, localCode, targetFtpOperation, localFtpOperation, sbLog)
        {

        }

        public override void Sync()
        {
            LogOperation.WriteTitle(sbLog, "-------------------------------------同步系统信息开始-------------------------------------");

            Action<DataRow> companyDetailAction = (dr) =>
            {
                LogOperation.Append(sbLog, "单位编码：" + dr["CCODE"]);
            };
            Action<DataRow> departmentDetailAction = (dr) =>
            {
                LogOperation.Append(sbLog, "部门编码：" + dr["DEPTCODE"]);
            };
            Action<DataRow> userDetailAction = (dr) =>
            {
                LogOperation.Append(sbLog, "用户ID：" + dr["USERID"]);
            };

            string targetCompanyCodeConditionString = "ccode like '" + targetCode.Substring(0, 4) + "%'";

            LogOperation.Append(sbLog, "同步系统信息（从" + targetCode + "到" + localCode + "）。");

            LogOperation.WriteSyncTableTitle(sbLog, "company", SyncType.Comparer, targetCompanyCodeConditionString);
            SyncByComparer(targetDb, localDb, "company", targetCompanyCodeConditionString, new List<string>() { "CCODE" }, true,
                 (dr) =>
                 {
                     LogOperation.Append(sbLog, "添加行政区单位！单位编码：" + dr["CCODE"]);
                 },
                 (dr) =>
                 {
                     LogOperation.Append(sbLog, "删除行政区单位！单位编码：" + dr["CCODE"]);
                 },
                 (drTarget, drLocal) =>
                 {
                     LogOperation.Append(sbLog, "更新行政区单位！单位编码：" + drTarget["CCODE"]);
                 });

            LogOperation.WriteSyncTableTitle(sbLog, "department", SyncType.AddAfterDelete, targetCompanyCodeConditionString);
            SyncByAddAfterDelete(targetDb, localDb, "department", targetCompanyCodeConditionString, deleteAction, departmentDetailAction, totalAction);

            LogOperation.WriteSyncTableTitle(sbLog, "users", SyncType.AddAfterDelete, targetCompanyCodeConditionString);
            SyncByAddAfterDelete(targetDb, localDb, "users", targetCompanyCodeConditionString, deleteAction, userDetailAction, totalAction);

            //string localCompanyCodeConditionString = "ccode like '" + localCode.Substring(0, 4) + "%'";

            //LogOperation.Append(sbLog, "同步系统信息（从" + localCode + "到" + targetCode + "）。");

            //LogOperation.WriteSyncTableTitle(sbLog, "company", SyncType.AddAfterDelete, localCompanyCodeConditionString);
            //SyncByComparer(localDb, targetDb, "company", localCompanyCodeConditionString, new List<string>() { "CCODE" }, true,
            //     (dr) =>
            //     {
            //         LogOperation.Append(sbLog, "添加行政区单位！单位编码：" + dr["CCODE"]);
            //     },
            //     (dr) =>
            //     {
            //         LogOperation.Append(sbLog, "删除行政区单位！单位编码：" + dr["CCODE"]);
            //     },
            //     (drTarget, drLocal) =>
            //     {
            //         LogOperation.Append(sbLog, "更新行政区单位！单位编码：" + drTarget["CCODE"]);
            //     });

            //LogOperation.WriteSyncTableTitle(sbLog, "department", SyncType.AddAfterDelete, localCompanyCodeConditionString);
            //SyncByAddAfterDelete(localDb, targetDb, "department", localCompanyCodeConditionString, deleteAction, departmentDetailAction, totalAction);

            //LogOperation.WriteSyncTableTitle(sbLog, "users", SyncType.AddAfterDelete, localCompanyCodeConditionString);
            //SyncByAddAfterDelete(localDb, targetDb, "users", localCompanyCodeConditionString, deleteAction, userDetailAction, totalAction);

            LogOperation.WriteTitle(sbLog, "-------------------------------------同步系统信息结束-------------------------------------");
        }
    }
}
