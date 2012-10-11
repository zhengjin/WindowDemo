using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.CustomAttribute;
using System.ComponentModel;

namespace HHSoft.FieldProtect.DataEntity.PersonalManage
{
    /// <summary>
    /// 系统信息结构体。
    /// </summary>
    [Serializable]
    public class XTXX
    {
        /// <summary>
        /// 信息编号
        /// </summary>
        [Pk]
        public string XXBH { get; set; }
        /// <summary>
        /// 消息标题
        /// </summary>
        public string XXBT { get; set; }
        /// <summary>
        /// 信息内容
        /// </summary>
        public string XXNR { get; set; }
        /// <summary>
        /// 信息附件
        /// </summary>
        public string XXFJ { get; set; }
        /// <summary>
        /// 发送人
        /// </summary>
        public string FSR { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime FSSJ { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public string XXLX { get; set; }
        /// <summary>
        /// 发送者IP地址
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 发送人删除状态。
        /// </summary>
        public string FSRSC { get; set; }
    }
}
