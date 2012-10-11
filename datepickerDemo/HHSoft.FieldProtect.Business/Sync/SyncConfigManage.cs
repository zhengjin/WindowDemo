using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.SysManage;
using Microsoft.Win32;
using HHSoft.FieldProtect.DataEntity;
using HHSoft.FieldProtect.FrameWork.Utility;
using HHSoft.FieldProtect.DataEntity.Sync;

namespace HHSoft.FieldProtect.Business.Sync
{
    public class SyncConfigManage
    {
        private RegeditHelper regHelper;

        public SyncConfigManage()
        {
            regHelper = new RegeditHelper();
        }

        /// <summary>
        /// 读
        /// </summary>
        /// <returns></returns>
        public SyncConfig Read()
        {
            SyncConfig SyncSetting = new SyncConfig();
            RegistryKey root = GetRoot();
            if (regHelper.KeyExists(root, SysConfig.SyncServiceName))
            {
                RegistryKey serviceKey = root.OpenSubKey(SysConfig.SyncServiceName);
                SyncSetting.TimerInterval = regHelper.GetValue<int>(serviceKey, SysConfig.RegistryTimerInterval);                
                SyncSetting.WriteLog = regHelper.GetValue<bool>(serviceKey, SysConfig.RegistryWriteLog);
                SyncSetting.SyncType = regHelper.GetValue<int>(serviceKey, SysConfig.RegistrySyncType);
                SyncSetting.SyncGisData = regHelper.GetValue<bool>(serviceKey, SysConfig.RegistrySyncGisData);
                SyncSetting.ServiceUrl = regHelper.GetValue<string>(serviceKey, SysConfig.RegistryServiceUrl);
                SyncSetting.ServiceMethod = regHelper.GetValue<string>(serviceKey, SysConfig.RegistryServiceMethod);
                
                RegistryKey localKey = serviceKey.OpenSubKey(SysConfig.RegistryLocals);
                foreach (var keyName in localKey.GetSubKeyNames())
                {
                    RegistryKey localCodeKey = localKey.OpenSubKey(keyName);
                    SyncConfigLocal localSetting = new SyncConfigLocal()
                    {
                        Code = keyName,
                        Name = regHelper.GetValue<string>(localCodeKey, SysConfig.RegistryName),
                        DbConfig = new DbConfig()
                        {
                            Ip = regHelper.GetValue<string>(localCodeKey, SysConfig.RegistryDbIp),
                            Name = regHelper.GetValue<string>(localCodeKey, SysConfig.RegistryDbName),
                            Username = regHelper.GetValue<string>(localCodeKey, SysConfig.RegistryDbUsername),
                            Password = regHelper.GetValue<string>(localCodeKey, SysConfig.RegistryDbPassword)
                        },
                        FtpConfig = new FtpConfig()
                        {
                            Ip = regHelper.GetValue<string>(localCodeKey, SysConfig.RegistryFtpIp),
                            Port = regHelper.GetValue<int>(localCodeKey, SysConfig.RegistryFtpPort),
                            Username = regHelper.GetValue<string>(localCodeKey, SysConfig.RegistryFtpUsername),
                            Password = regHelper.GetValue<string>(localCodeKey, SysConfig.RegistryFtpPassword)
                        }
                    };
                    SyncSetting.LocalData.Add(localSetting);
                }
                RegistryKey targetKey = serviceKey.OpenSubKey(SysConfig.RegistryTargets);
                foreach (var keyName in targetKey.GetSubKeyNames())
                {
                    RegistryKey targetCodeKey = targetKey.OpenSubKey(keyName);
                    SyncConfigTarget targetSetting = new SyncConfigTarget()
                    {
                        Code = keyName,
                        Name = regHelper.GetValue<string>(targetCodeKey, SysConfig.RegistryName),
                        DbConfig = new DbConfig()
                        {
                            Ip = regHelper.GetValue<string>(targetCodeKey, SysConfig.RegistryDbIp),
                            Name = regHelper.GetValue<string>(targetCodeKey, SysConfig.RegistryDbName),
                            Username = regHelper.GetValue<string>(targetCodeKey, SysConfig.RegistryDbUsername),
                            Password = regHelper.GetValue<string>(targetCodeKey, SysConfig.RegistryDbPassword)
                        },
                        FtpConfig = new FtpConfig()
                        {
                            Ip = regHelper.GetValue<string>(targetCodeKey, SysConfig.RegistryFtpIp),
                            Port = regHelper.GetValue<int>(targetCodeKey, SysConfig.RegistryFtpPort),
                            Username = regHelper.GetValue<string>(targetCodeKey, SysConfig.RegistryFtpUsername),
                            Password = regHelper.GetValue<string>(targetCodeKey, SysConfig.RegistryFtpPassword)
                        },
                        Jg = regHelper.GetValue<string>(targetCodeKey, SysConfig.RegistryJg),
                        Time = regHelper.GetValue<string>(targetCodeKey, SysConfig.RegistryTime)
                    };
                    SyncSetting.TargetData.Add(targetSetting);
                }
            }
            return SyncSetting;
        }

        /// <summary>
        /// 写
        /// </summary>
        /// <param name="sysConfig"></param>
        public void Write(SyncConfig sysConfig)
        {
            RegistryKey root = GetRoot();
            if (regHelper.KeyExists(root, SysConfig.SyncServiceName))
            {
                root.DeleteSubKeyTree(SysConfig.SyncServiceName);
            }
            RegistryKey serviceKey = root.CreateSubKey(SysConfig.SyncServiceName);
            serviceKey.SetValue(SysConfig.RegistryTimerInterval, sysConfig.TimerInterval);           
            serviceKey.SetValue(SysConfig.RegistryWriteLog, sysConfig.WriteLog);            
            serviceKey.SetValue(SysConfig.RegistrySyncType, sysConfig.SyncType);

            serviceKey.SetValue(SysConfig.RegistrySyncGisData, sysConfig.SyncGisData); 
            serviceKey.SetValue(SysConfig.RegistryServiceUrl, sysConfig.ServiceUrl);
            serviceKey.SetValue(SysConfig.RegistryServiceMethod, sysConfig.ServiceMethod);
            
            RegistryKey localKey = serviceKey.CreateSubKey(SysConfig.RegistryLocals);

            foreach (var localSetting in sysConfig.LocalData)
            {
                RegistryKey localCodeKey = localKey.CreateSubKey(localSetting.Code);
                localCodeKey.SetValue(SysConfig.RegistryName, localSetting.Name);
                if (localSetting.DbConfig != null)
                {
                    regHelper.SetValue(localCodeKey, SysConfig.RegistryDbIp, localSetting.DbConfig.Ip);
                    regHelper.SetValue(localCodeKey, SysConfig.RegistryDbName, localSetting.DbConfig.Name);
                    regHelper.SetValue(localCodeKey, SysConfig.RegistryDbUsername, localSetting.DbConfig.Username);
                    regHelper.SetValue(localCodeKey, SysConfig.RegistryDbPassword, localSetting.DbConfig.Password);
                }
                if (localSetting.FtpConfig != null)
                {
                    regHelper.SetValue(localCodeKey, SysConfig.RegistryFtpIp, localSetting.FtpConfig.Ip);
                    regHelper.SetValue(localCodeKey, SysConfig.RegistryFtpPort, localSetting.FtpConfig.Port);
                    regHelper.SetValue(localCodeKey, SysConfig.RegistryFtpUsername, localSetting.FtpConfig.Username);
                    regHelper.SetValue(localCodeKey, SysConfig.RegistryFtpPassword, localSetting.FtpConfig.Password);
                }
            }
            RegistryKey targetKey = serviceKey.CreateSubKey(SysConfig.RegistryTargets);
            foreach (var targetSetting in sysConfig.TargetData)
            {
                RegistryKey targetCodeKey = targetKey.CreateSubKey(targetSetting.Code);
                targetCodeKey.SetValue(SysConfig.RegistryName, targetSetting.Name);
                if (targetSetting.DbConfig != null)
                {
                    regHelper.SetValue(targetCodeKey, SysConfig.RegistryDbIp, targetSetting.DbConfig.Ip);
                    regHelper.SetValue(targetCodeKey, SysConfig.RegistryDbName, targetSetting.DbConfig.Name);
                    regHelper.SetValue(targetCodeKey, SysConfig.RegistryDbUsername, targetSetting.DbConfig.Username);
                    regHelper.SetValue(targetCodeKey, SysConfig.RegistryDbPassword, targetSetting.DbConfig.Password);
                }
                if (targetSetting.DbConfig != null)
                {
                    regHelper.SetValue(targetCodeKey, SysConfig.RegistryFtpIp, targetSetting.FtpConfig.Ip);
                    regHelper.SetValue(targetCodeKey, SysConfig.RegistryFtpPort, targetSetting.FtpConfig.Port);
                    regHelper.SetValue(targetCodeKey, SysConfig.RegistryFtpUsername, targetSetting.FtpConfig.Username);
                    regHelper.SetValue(targetCodeKey, SysConfig.RegistryFtpPassword, targetSetting.FtpConfig.Password);
                }
                if (!string.IsNullOrEmpty(targetSetting.Jg))
                {
                    regHelper.SetValue(targetCodeKey, SysConfig.RegistryJg, targetSetting.Jg);
                }
                if (!string.IsNullOrEmpty(targetSetting.Time))
                {
                    regHelper.SetValue(targetCodeKey, SysConfig.RegistryTime, targetSetting.Time);
                }
            }
        }

        /// <summary>
        /// 获取根节点
        /// </summary>
        /// <returns></returns>
        private RegistryKey GetRoot()
        {
            return Registry.LocalMachine.CreateSubKey("SOFTWARE").CreateSubKey(SysConfig.SystemName);
        }
        
        /// <summary>
        /// 获取某一行政区的数据库连接
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public DbConfig GetDbSetting(string code)
        {
            DbConfig result = null;
            RegistryKey root = GetRoot();
            if (regHelper.KeyExists(root, SysConfig.SyncServiceName))
            {
                RegistryKey serviceKey = root.OpenSubKey(SysConfig.SyncServiceName);
                RegistryKey localKey = serviceKey.OpenSubKey(SysConfig.RegistryLocals);
                foreach (var keyName in localKey.GetSubKeyNames())
                {
                    if (keyName == code)
                    {
                        RegistryKey codeKey = localKey.OpenSubKey(keyName);
                        result = new DbConfig()
                        {
                            Ip = regHelper.GetValue<string>(codeKey, SysConfig.RegistryDbIp),
                            Name = regHelper.GetValue<string>(codeKey, SysConfig.RegistryDbName),
                            Username = regHelper.GetValue<string>(codeKey, SysConfig.RegistryDbUsername),
                            Password = regHelper.GetValue<string>(codeKey, SysConfig.RegistryDbPassword)
                        };
                        break;
                    }
                }
                if (result == null)
                {
                    RegistryKey targetKey = serviceKey.OpenSubKey(SysConfig.RegistryTargets);
                    foreach (var keyName in targetKey.GetSubKeyNames())
                    {
                        if (keyName == code)
                        {
                            RegistryKey codeKey = targetKey.OpenSubKey(keyName);
                            result = new DbConfig()
                            {
                                Ip = regHelper.GetValue<string>(codeKey, SysConfig.RegistryDbIp),
                                Name = regHelper.GetValue<string>(codeKey, SysConfig.RegistryDbName),
                                Username = regHelper.GetValue<string>(codeKey, SysConfig.RegistryDbUsername),
                                Password = regHelper.GetValue<string>(codeKey, SysConfig.RegistryDbPassword)
                            };
                            break;
                        }
                    }
                }
            }
            return result;
        }
    }
}
