using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.CustomAttribute;

namespace HHSoft.FieldProtect.DataEntity.ItemManage
{
    /// <summary>
    /// 项目单位信息
    /// </summary>
    [Serializable]
    public class Xm_Xmdw
    {
        /// <summary>
        /// 项目编号
        /// </summary>
        [Pk]
        public virtual string ItemCode { get; set; }

        /// <summary>
        /// 单位类型
        /// </summary>
        [Pk, EnumValueColumn(true)]
        public virtual ItemCompanyType Type { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 单位编码
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// 联系人员
        /// </summary>
        public virtual string LinkMan { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string LinkPhone { get; set; }
    }
}
