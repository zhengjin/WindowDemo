using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.Business.Common;
using HHSoft.FieldProtect.DataAccess;
using HHSoft.FieldProtect.DataEntity.Sync;
using HHSoft.FieldProtect.FrameWork.Utility;
using HHSoft.FieldProtect.DataEntity.ItemManage;
using HHSoft.FieldProtect.DataEntity;
using System.Data;

namespace HHSoft.FieldProtect.Business.Sync
{
    public class SyncManage
    {
        /// <summary>
        /// 获取新建文件名称。
        /// </summary>
        /// <param name="directoryInfo">文件夹信息对象。</param>
        /// <returns>新建文件名称。</returns>
        public string GetNewFileName(DirectoryInfo directoryInfo)
        {
            int newFileIndex = 1;
            var fileNames = (from file in directoryInfo.GetFiles()
                             select int.Parse(file.Name.Substring(0, file.Name.LastIndexOf('.')))).ToList();
            if (fileNames.Count > 0)
            {
                newFileIndex = fileNames.Max() + 1;
            }
            return newFileIndex + ".txt";
        }

        /// <summary>
        /// 获取同步记录文件夹路径。
        /// </summary>
        /// <param name="systemFolderPath">系统文件夹路径。</param>
        /// <param name="code">行政区编号。</param>
        /// <returns>同步记录文件夹路径。</returns>
        public string GetRecordFolder(string systemFolderPath, string code)
        {
            return systemFolderPath + code + @"\";
        }

        /// <summary>
        /// 获取最后同步时间。
        /// </summary>
        /// <param name="systemFolderPath">系统文件夹路径。</param>
        /// <param name="code">行政区编号。</param>
        /// <returns>最后同步时间。</returns>
        public DateTime GetLastTime(string systemFolderPath, string code)
        {
            string folderPath = GetRecordFolder(systemFolderPath, code);
            DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
            return directoryInfo.CreationTime;
        }

        /// <summary>
        /// 设置最后同步时间。
        /// </summary>
        /// <param name="systemFolderPath">系统文件夹路径。</param>
        /// <param name="code">行政区编号。</param>
        /// <param name="time">最后同步时间。</param>
        public void SetLastTime(string systemFolderPath, string code, DateTime time)
        {
            string folderPath = GetRecordFolder(systemFolderPath, code);
            DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
            directoryInfo.CreationTime = time;
        }


        public SyncDataLog GetSyncLogInfo(OracleDbOperation oracleDb, string code)
        {
            SyncDataLog dataInfo = null;
            string strSql = "select * from syncdataLog where ccode = '{0}' and syncType = {1} order by syncdate desc";
            strSql = string.Format(strSql, code, ((int)SyncDataType.System).ToString());
            DataTable dt = oracleDb.ExecuteDataTable(strSql);            
            if (dt.Rows.Count > 0)
            {
                dataInfo = (SyncDataLog)new DataTableOperation().ConvertFromDataRowToEntity(dt.Rows[0], typeof(SyncDataLog));
            }
            return dataInfo;
        }

        /// <summary>
        /// 插入失败的记录
        /// </summary>
        /// <param name="localDb"></param>
        /// <param name="targetConfig"></param>
        /// <param name="dataType"></param>
        public SyncDataLog InsertSyncLog(OracleDbOperation oracleDb, SyncConfigTarget targetConfig, SyncDataType dataType, string fileName)
        {
            SyncDataLog syncLog = new SyncDataLog();
            syncLog.LogId = Guid.NewGuid().ToString();
            syncLog.CCode = targetConfig.Code;
            syncLog.CName = targetConfig.Name;
            syncLog.Synctype = dataType;
            syncLog.SyncDate = System.DateTime.Now;
            syncLog.FileName = fileName;
            syncLog.Succ = 1;
            string strSql = SqlBuilder.BuildInsertSql(syncLog);
            oracleDb.ExecuteNonQuery(strSql, null);
            return syncLog;
        }

        public void UpdateSyncLog(OracleDbOperation oracleDb, SyncDataLog syncLog)
        {
            syncLog.Succ = 0;
            string strSql = SqlBuilder.BuildUpdateSql(syncLog);
            oracleDb.ExecuteNonQuery(strSql, null);
        }

        /// <summary>
        /// 同步。
        /// </summary>
        /// <param name="setting">同步配置。</param>
        /// <param name="log">日志信息。</param>
        /// <returns>同步是否成功。</returns>
        public bool Sync(SyncConfig syncConfig, SyncConfigTarget targetConfig, SyncConfigLocal localConfig, ref StringBuilder sbLog)
        {
            bool result = false;

            LogOperation.Append(sbLog, string.Format("开始同步数据：{0}({1})  开始时间：{2}", targetConfig.Name, targetConfig.Code, DateTime.Now));

            OracleDbOperation localDb = new OracleDbOperation(localConfig.DbConfig.ConnStr);
            FtpHelper localFtp = new FtpHelper(localConfig.FtpConfig.Ip, localConfig.FtpConfig.Port, localConfig.FtpConfig.Username, localConfig.FtpConfig.Password);

            OracleDbOperation targetDb = new OracleDbOperation(targetConfig.DbConfig.ConnStr);
            FtpHelper targetFtp = new FtpHelper(targetConfig.FtpConfig.Ip, targetConfig.FtpConfig.Port, targetConfig.FtpConfig.Username, targetConfig.FtpConfig.Password);
            try
            {
                targetDb.BeginTransaction();
                localDb.BeginTransaction();

                List<SyncData> syncDataList = new List<SyncData>();
                ////系统数据
                syncDataList.Add(new SyncDataSystemInfo(targetDb, localDb, targetConfig.Code, localConfig.Code, targetFtp, localFtp, sbLog));
                ////项目数据
                syncDataList.Add(new SyncDataItem(syncConfig, targetDb, localDb, targetConfig.Code, localConfig.Code, targetFtp, localFtp, sbLog));
                ////流程数据
                syncDataList.Add(new SyncDataWorkflow(targetDb, localDb, targetConfig.Code, localConfig.Code, targetFtp, localFtp, sbLog));
                ////Syncs.Add(new SyncXtxx(targetDbOperation, localDbOperation, targetSetting.Code, localSetting.Code, targetFtpOperation, localFtpOperation, sbLog));

                foreach (var syncData in syncDataList)
                {
                    syncData.Sync();
                }

                targetDb.Commit();
                localDb.Commit();
                result = true;
            }
            catch (Exception e)
            {
                targetDb.Rollback();
                localDb.Rollback();
                LogOperation.WriteExceptionLog(sbLog, e);
            }

            sbLog.AppendLine();
            sbLog.AppendLine("结束时间：" + DateTime.Now);
            return result;
        }
    }
}
