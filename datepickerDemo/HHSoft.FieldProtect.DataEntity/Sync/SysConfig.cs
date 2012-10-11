using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HHSoft.FieldProtect.DataEntity.Sync
{
    /// <summary>
    /// 常量参数类。
    /// </summary>
    public class SysConfig:BaseConfig
    {
        /// <summary>
        /// 系统名称。
        /// </summary>
        public const string SystemName = "耕地保护系统";

        /// <summary>
        /// 同步日志信息文件夹名称。
        /// </summary>
        public const string LogFolderName = "Logs";

        /// <summary>
        /// 自定义同步命令。
        /// </summary>
        public const int CommandSync = 135;

        /// <summary>
        /// 自定义刷新命令。
        /// </summary>
        public const int CommandRefresh = 136;

        /// <summary>
        /// 自定义同步消息队列路径。
        /// </summary>
        public const string SyncMessageQueuePath = @".\Private$\MQSync";

        /// <summary>
        /// 同步服务名称。
        /// </summary>
        public const string SyncServiceName = "耕地保护数据同步服务";

        /// <summary>
        /// TimerInterval注册表键。
        /// </summary>
        public const string RegistryTimerInterval = "TimerInterval";

        /// <summary>
        /// WriteLog注册表键。
        /// </summary>
        public const string RegistryWriteLog = "WriteLog";

        /// <summary>
        /// 同步类型。
        /// </summary>
        public const string RegistrySyncType = "SyncType";

        /// <summary>
        /// 是否同步Gis。
        /// </summary>
        public const string RegistrySyncGisData = "SyncGisData";

        /// <summary>
        /// Web服务地址。
        /// </summary>
        public const string RegistryServiceUrl = "ServiceUrl";

        /// <summary>
        /// Web服务方法。
        /// </summary>
        public const string RegistryServiceMethod = "ServiceMethod";

        /// <summary>
        /// Locals注册表键。
        /// </summary>
        public const string RegistryLocals = "Locals";

        /// <summary>
        /// Targets注册表键。
        /// </summary>
        public const string RegistryTargets = "Targets";

        /// <summary>
        /// Name注册表键。
        /// </summary>
        public const string RegistryName = "Name";

        /// <summary>
        /// DbIp注册表键。
        /// </summary>
        public const string RegistryDbIp = "DbIp";

        /// <summary>
        /// DbName注册表键。
        /// </summary>
        public const string RegistryDbName = "DbName";

        /// <summary>
        /// DbUsername注册表键。
        /// </summary>
        public const string RegistryDbUsername = "DbUsername";

        /// <summary>
        /// DbPassword注册表键。
        /// </summary>
        public const string RegistryDbPassword = "DbPassword";

        /// <summary>
        /// FtpIp注册表键。
        /// </summary>
        public const string RegistryFtpIp = "FtpIp";

        /// <summary>
        /// FtpPort注册表键。
        /// </summary>
        public const string RegistryFtpPort = "FtpPort";

        /// <summary>
        /// FtpUsername注册表键。
        /// </summary>
        public const string RegistryFtpUsername = "FtpUsername";

        /// <summary>
        /// FtpPassword注册表键。
        /// </summary>
        public const string RegistryFtpPassword = "FtpPassword";

        /// <summary>
        /// Jg注册表键。
        /// </summary>
        public const string RegistryJg = "Jg";

        /// <summary>
        /// Time注册表键。
        /// </summary>
        public const string RegistryTime = "Time";

        /// <summary>
        /// 项目列表配置文件Web路径。
        /// </summary>
        public const string ItemListColumnConfigWebPath = "~/App_Data/ItemListColumnConfig.xml";
    }
}
