using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace HHSoft.FieldProtect.DataEntity.Sync
{
    /// <summary>
    /// 同步配置类
    /// </summary>
    public class SyncConfig
    {
        /// <summary>
        /// 同步间隔
        /// </summary>
        public long TimerInterval { get; set; }
        
        /// <summary>
        /// 是否写日志
        /// </summary>
        public bool WriteLog { get; set; }

        /// <summary>
        /// 同步类型(1 增量同步　2 全部同步)
        /// </summary>
        public int SyncType { get; set; }

        /// <summary>
        /// 是否同步Gis数据
        /// </summary>
        public bool SyncGisData { get; set; }

        /// <summary>
        /// Web服务地址
        /// </summary>
        public string ServiceUrl { get; set; }

        /// <summary>
        /// Web服务方法
        /// </summary>
        public string ServiceMethod { get; set; }
        
        /// <summary>
        /// 源数据对象
        /// </summary>
        public BindingList<SyncConfigLocal> LocalData { get; set; }

        /// <summary>
        /// 目标数据对象
        /// </summary>
        public BindingList<SyncConfigTarget> TargetData { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public SyncConfig()
        {
            TimerInterval = 60 * 1000;
            SyncType = 1;
            SyncGisData = false;
            LocalData = new BindingList<SyncConfigLocal>();
            TargetData = new BindingList<SyncConfigTarget>();
        }
    }
}
