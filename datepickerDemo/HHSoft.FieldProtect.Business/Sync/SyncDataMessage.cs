using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataAccess;
using System.Data.OracleClient;
using System.Data;
using HHSoft.FieldProtect.Business.PersonalManage;
using HHSoft.FieldProtect.FrameWork.Utility;
using System.Configuration;
using HHSoft.FieldProtect.Business.Common;
using HHSoft.FieldProtect.DataEntity;

namespace HHSoft.FieldProtect.Business.Sync
{
    public class SyncDataMessage : SyncData
    {
        public SyncDataMessage(OracleDbOperation targetDbOperation, OracleDbOperation localDbOperation, string targetCode,
            string localCode, FtpHelper targetFtpOperation, FtpHelper localFtpOperation, StringBuilder sbLog)
            : base(targetDbOperation, localDbOperation, targetCode, localCode, targetFtpOperation, localFtpOperation, sbLog)
        {

        }

        public override void Sync()
        {
            MessageManage messageManage = new MessageManage();
            Action<DataRow> actionAddLocalMessageFile = new Action<DataRow>((dr) =>
            {
                string xxbh = dr["xxbh"].ToString();
                string messageFtpPath = CommonManage.GetFtpMessagePath();
                LogOperation.Append(sbLog, "从" + targetCode + "到" + localCode + "同步系统消息（" + xxbh + "）的文件。");
                targetFtp.CopyDirectory(messageFtpPath, xxbh, localFtp, messageFtpPath, xxbh, null);
            });
            Action<DataRow> actionAddTargetMessageFile = new Action<DataRow>((dr) =>
            {
                string xxbh = dr["xxbh"].ToString();
                string messageFtpPath = CommonManage.GetFtpMessagePath();
                LogOperation.Append(sbLog, "从" + localCode + "到" + targetCode + "同步系统消息（" + xxbh + "）的文件。");
                localFtp.CopyDirectory(messageFtpPath, xxbh, targetFtp, messageFtpPath, xxbh, null);
            });
            Action<DataRow> actionDeleteLocalMessageFile = new Action<DataRow>((dr) =>
            {
                string xxbh = dr["xxbh"].ToString();
                LogOperation.Append(sbLog, "删除" + localCode + "的系统消息（" + xxbh + "）的文件。");
                localFtp.DeleteDirectory(CommonManage.GetFtpMessagePath(xxbh));
            });
            Action<DataRow> actionDeleteTargetMessageFile = new Action<DataRow>((dr) =>
            {
                string xxbh = dr["xxbh"].ToString();
                LogOperation.Append(sbLog, "删除" + targetCode + "的系统消息（" + xxbh + "）的文件。");
                targetFtp.DeleteDirectory(CommonManage.GetFtpMessagePath(xxbh));
            });

            LogOperation.WriteTitle(sbLog, "同步系统消息开始（从" + targetCode + "到" + localCode + "）。");
            SyncXxbh(targetDb, localDb, localCode, actionAddLocalMessageFile,
                actionDeleteTargetMessageFile, actionDeleteLocalMessageFile);
            LogOperation.WriteTitle(sbLog, "同步系统消息结束（从" + targetCode + "到" + localCode + "）。");

            LogOperation.WriteTitle(sbLog, "同步系统消息开始（从" + localCode + "到" + targetCode + "）。");
            SyncXxbh(localDb, targetDb, targetCode, actionAddTargetMessageFile,
                actionDeleteLocalMessageFile, actionDeleteTargetMessageFile);
            LogOperation.WriteTitle(sbLog, "同步系统消息结束（从" + localCode + "到" + targetCode + "）。");
        }

        private void SyncXxbh(OracleDbOperation sourceDbOperation, OracleDbOperation targetDbOperation, string targetCode,
            Action<DataRow> actionAddTargetMessageFile, Action<DataRow> actionDeleteSourceMessageFile, Action<DataRow> actionDeleteTargetMessageFile)
        {
            string sql;
            List<OracleParameter> parameters = new List<OracleParameter>();
            DataTable dt;

            //用指定的行政区代码去系统信息接收表中查询所有与接收人行政区代码相等的记录，即需要同步的信息编号。
            sql = "select distinct(jsxxbh) from xtxxjs where jsrxzdm = :jsrxzdm";
            parameters.Clear();
            parameters.Add(new OracleParameter("jsrxzdm", targetCode));
            dt = sourceDbOperation.ExecuteDataTable(sql, "temp", parameters);
            var xxbhs = (from DataRow dr in dt.Rows
                         select "'" + dr[0] + "'").ToArray();

            if (xxbhs.Length > 0)
            {
                //分别取源系统和目标系统取出需要同步的信息接收数据。
                string xxbhsCondition = string.Join(",", xxbhs);
                sql = "select * from xtxxjs where jsxxbh in (" + xxbhsCondition + ")";
                DataTable dtSourceXtxxjs = sourceDbOperation.ExecuteDataTable(sql, "xtxxjs", null);
                DataTable dtTargetXtxxjs = targetDbOperation.ExecuteDataTable(sql, "xtxxjs", null);

                string xtxxCondition = "xxbh in (" + xxbhsCondition + ")";

                //进行比对同步。
                LogOperation.WriteSyncTableTitle(sbLog, "xtxx", SyncType.Comparer, xtxxCondition);
                SyncByComparer(sourceDbOperation, targetDbOperation, "xtxx", xtxxCondition, new List<string>() { "xxbh" }, true,
                    (dr) =>
                    {
                        string xxbh = dr["xxbh"].ToString();
                        LogOperation.Append(sbLog, "添加目标系统信息！信息编号：" + xxbh);
                        foreach (var needAddXtxxjs in dtSourceXtxxjs.Select("jsxxbh = '" + xxbh + "' and jsrxzdm = '" + targetCode + "'"))
                        {
                            DataRow newXtxxjs = dtTargetXtxxjs.NewRow();
                            Copy(needAddXtxxjs, newXtxxjs);
                            dtTargetXtxxjs.Rows.Add(newXtxxjs);
                        }
                        if (actionAddTargetMessageFile != null)
                        {
                            actionAddTargetMessageFile(dr);
                        }
                    },
                    (dr) =>
                    {
                        LogOperation.Append(sbLog, "删除垃圾系统信息！信息编号：" + dr["XXBH"]);
                    },
                    (drSource, drTarget) =>
                    {
                        string xxbh = drSource["XXBH"].ToString();
                        LogOperation.Append(sbLog, "更新系统信息！信息编号：" + xxbh);

                        if (actionAddTargetMessageFile != null)
                        {
                            actionAddTargetMessageFile(drSource);
                        }

                        //判断是否需要删除邮件。
                        if (drSource["FSRSC"].ToString() == "1")
                        {
                            var querySource = from DataRow dr in dtSourceXtxxjs.Rows
                                              where dr["JSXXBH"].ToString() == xxbh
                                              select dr;
                            var queryTarget = from DataRow dr in dtTargetXtxxjs.Rows
                                              where dr["JSXXBH"].ToString() == xxbh
                                              select dr;
                            bool needDelete = true;
                            foreach (var itemSource in querySource)
                            {
                                string jsr = itemSource["JSR"].ToString();
                                var queryTargetJsr = (from dr in queryTarget
                                                      where dr["JSR"].ToString() == jsr
                                                      select dr).FirstOrDefault();
                                if (queryTargetJsr == null)
                                {
                                    if (itemSource["JSRSC"].ToString() == "0")
                                    {
                                        needDelete = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (queryTargetJsr["JSRSC"].ToString() == "0")
                                    {
                                        needDelete = false;
                                        break;
                                    }
                                }
                            }

                            //删除邮件。
                            if (needDelete)
                            {
                                dtOperation.DeleteDbDataRow(drSource, (dr) =>
                                {
                                    dtOperation.DeleteDbDataRows(querySource, null);
                                    LogOperation.Append(sbLog, "删除源系统信息，因为收发双方都已删除此信息！信息编号：" + xxbh);
                                    actionDeleteSourceMessageFile(drSource);
                                });
                                dtOperation.DeleteDbDataRow(drTarget, (dr) =>
                                {
                                    dtOperation.DeleteDbDataRows(queryTarget, null);
                                    LogOperation.Append(sbLog, "删除目标系统信息，因为收发双方都已删除此信息！信息编号：" + xxbh);
                                    actionDeleteSourceMessageFile(drTarget);
                                });
                            }
                        }
                    });

                //保存信息接收信息。
                sourceDbOperation.UpdateDataTable(dtSourceXtxxjs);
                targetDbOperation.UpdateDataTable(dtTargetXtxxjs);
            }
        }
    }
}
