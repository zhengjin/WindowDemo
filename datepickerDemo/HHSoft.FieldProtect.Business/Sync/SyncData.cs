using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using HHSoft.FieldProtect.Business.Common;
using HHSoft.FieldProtect.DataAccess;
using HHSoft.FieldProtect.FrameWork.Utility;

namespace HHSoft.FieldProtect.Business.Sync
{
    /// <summary>
    /// 数据同步抽象类
    /// </summary>
    public abstract class SyncData
    {
        protected OracleDbOperation targetDb;
        protected OracleDbOperation localDb;
        protected string targetCode;
        protected string localCode;
        protected FtpHelper targetFtp;
        protected FtpHelper localFtp;
        protected StringBuilder sbLog;        
        protected DataTableOperation dtOperation;
        
        protected Action deleteAction;
        protected Action<string, int> totalAction;
        protected Action<string, string> deleteFileAction;
        protected Action<string, int> ftpCopyAction;

        protected SyncData(OracleDbOperation targetDb, OracleDbOperation localDb, string targetCode,
            string localCode, FtpHelper targetFtp, FtpHelper localFtp, StringBuilder sbLog)
        {
            this.targetDb = targetDb;
            this.localDb = localDb;
            this.targetCode = targetCode;
            this.localCode = localCode;
            this.targetFtp = targetFtp;
            this.localFtp = localFtp;
            this.sbLog = sbLog;
            
            dtOperation = new DataTableOperation();

            deleteAction = () =>
            {
                LogOperation.Append(sbLog, "删除成功");
            };
            totalAction = (tableName, count) =>
            {
                LogOperation.Append(sbLog, "统计：" + tableName + "表共同步了" + count + "条数据");
            };
            deleteFileAction = (itemCode,fileName) =>
            {                
                LogOperation.Append(sbLog, string.Format("项目编号：{0}, 删除文件：{1}", itemCode, fileName));
            };

            ftpCopyAction = (itemCode, fileCount) =>
            {
                LogOperation.Append(sbLog, string.Format("项目编号：{0}, 拷贝文件数：{1}", itemCode, fileCount.ToString()));
            };
        }

        protected void Copy(DataRow drSource, DataRow drTarget)
        {
            foreach (DataColumn column in drSource.Table.Columns)
            {
                string columnName = column.ColumnName;
                if (drTarget.Table.Columns.Contains(columnName))
                {
                    drTarget[columnName] = drSource[columnName];
                }
            }
        }

        protected void SyncByAddAfterDelete(string tableName, string condition, Action deleteAction, Action<DataRow> actionDetail, Action<string, int> actionCompleted)
        {
            SyncByAddAfterDelete(targetDb, localDb, tableName, condition, deleteAction, actionDetail, actionCompleted);
        }

        protected void SyncByAddAfterDelete(OracleDbOperation sourceDbOperation, OracleDbOperation targetDbOperation,
            string tableName, string condition, Action deleteAction, Action<DataRow> actionDetail, Action<string, int> actionCompleted)
        {
            string whereCondition = string.IsNullOrEmpty(condition) ? string.Empty : " where " + condition;
            string selectNoneSql = "select * from " + tableName + " where 1 <> 1";
            string selectSql = "select * from " + tableName + whereCondition;
            string deleteSql = "delete from " + tableName + whereCondition;
            targetDbOperation.ExecuteNonQuery(deleteSql, null);
            DataTable dtSource = sourceDbOperation.ExecuteDataTable(selectSql, tableName, null);
            DataTable dtTarget = targetDbOperation.ExecuteDataTable(selectNoneSql, tableName, null);
            foreach (DataRow drSource in dtSource.Rows)
            {
                DataRow newTargetDr = dtTarget.NewRow();
                Copy(drSource, newTargetDr);
                dtTarget.Rows.Add(newTargetDr);
                if (actionDetail != null)
                {
                    actionDetail(newTargetDr);
                }                
            }
            targetDbOperation.UpdateDataTable(dtTarget);
            if (actionCompleted != null)
            {
                actionCompleted(tableName, dtTarget.Rows.Count);
            }
        }

        protected void SyncByComparer(string tableName, string condition, List<string> Pks, bool isDelete,
            Action<DataRow> actionAdd, Action<DataRow> actionDelete, Action<DataRow, DataRow> actionModify)
        {
            SyncByComparer(targetDb, localDb, tableName, condition, Pks, isDelete, actionAdd, actionDelete, actionModify, null);
        }

        protected void SyncByComparer(string tableName, string condition, List<string> Pks, bool isDelete,
            Action<DataRow> actionAdd, Action<DataRow> actionDelete, Action<DataRow, DataRow> actionModify, Action<DataRow, DataRow> actionDetail)
        {
            SyncByComparer(targetDb, localDb, tableName, condition, Pks, isDelete, actionAdd, actionDelete, actionModify, actionDetail);
        }

        protected void SyncByComparer(OracleDbOperation sourceDbOperation, OracleDbOperation targetDbOperation,
            string tableName, string condition, List<string> Pks, bool isDelete,
            Action<DataRow> actionAdd, Action<DataRow> actionDelete, Action<DataRow, DataRow> actionModify)
        {
            SyncByComparer(sourceDbOperation, targetDbOperation, tableName, condition, Pks, isDelete, actionAdd, actionDelete, actionModify, null);
        }

        protected void SyncByComparer(OracleDbOperation sourceDbOperation, OracleDbOperation targetDbOperation,
            string tableName, string condition, List<string> Pks, bool isDelete,
            Action<DataRow> actionAdd, Action<DataRow> actionDelete, Action<DataRow, DataRow> actionModify, Action<DataRow, DataRow> actionDetail)
        {
            string whereCondition = string.IsNullOrEmpty(condition) ? string.Empty : " where " + condition;
            string orderCondition = string.Empty;
            if (Pks.Count > 0) orderCondition = " order by " + string.Join(",", Pks.ToArray());
            string selectSql = "select * from " + tableName + whereCondition + orderCondition;
            DataTable dtSource = sourceDbOperation.ExecuteDataTable(selectSql, tableName, null);
            DataTable dtTarget = targetDbOperation.ExecuteDataTable(selectSql, tableName, null);
            List<DataRow> needDeletes = new List<DataRow>();
            if (isDelete)
            {
                foreach (DataRow drTarget in dtTarget.Rows)
                {
                    needDeletes.Add(drTarget);
                }
            }
            foreach (DataRow drSource in dtSource.Rows)
            {
                DataRow drTarget = null;
                foreach (DataRow drTargetTemp in dtTarget.Rows)
                {
                    bool equals = true;
                    foreach (var pk in Pks)
                    {
                        if (drSource[pk].ToString() != drTargetTemp[pk].ToString())
                        {
                            equals = false;
                            break;
                        }
                    }
                    if (equals)
                    {
                        drTarget = drTargetTemp;
                    }
                }
                if (drTarget == null)
                {
                    DataRow newDrTarget = dtTarget.NewRow();
                    Copy(drSource, newDrTarget);
                    dtTarget.Rows.Add(newDrTarget);
                    if (actionAdd != null)
                    {
                        actionAdd(newDrTarget);
                    }
                    if (actionDetail != null)
                    {
                        actionDetail(drSource, newDrTarget);
                    }
                }
                else
                {
                    Copy(drSource, drTarget);
                    if (actionModify != null)
                    {
                        actionModify(drSource, drTarget);
                    }
                    if (actionDetail != null)
                    {
                        actionDetail(drSource, drTarget);
                    }
                    if (isDelete)
                    {
                        needDeletes.Remove(drTarget);
                    }
                }
            }
            if (isDelete)
            {
                dtOperation.DeleteDbDataRows(needDeletes, actionDelete);
            }
            sourceDbOperation.UpdateDataTable(dtSource);
            targetDbOperation.UpdateDataTable(dtTarget);
        }
                
        public abstract void Sync();
    }
}
