using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HHSoft.FieldProtect.DataEntity.ItemManage
{
    /// <summary>
    /// 项目界址点
    /// </summary>
    [Serializable]
    public class Xm_Jzd
    {
        /// <summary>
        /// 地块Id
        /// </summary>
        public string Dkid { get; set; }

        /// <summary>
        /// 界址点Id
        /// </summary>
        public string JzdId { get; set; }

        /// <summary>
        /// 界址点编号
        /// </summary>
        public string JzdBh { get; set; }

        /// <summary>
        /// 地块圈号
        /// </summary>
        public string Dkqh { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string X { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public string Y { get; set; }
    }
}
