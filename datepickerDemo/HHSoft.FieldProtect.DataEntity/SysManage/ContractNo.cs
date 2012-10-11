using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.CustomAttribute;

namespace HHSoft.FieldProtect.DataEntity.SysManage
{
    /// <summary>
    /// 电子监管号
    /// </summary>
    [Serializable]    
    public class ContractNo
    {
        public string CONTRACTNO { get; set; }
        public string HTBD { get; set; }
        public string ITEMCODE { get; set; }
        public string ITEMNAME { get; set; }
        public string CBF { get; set; }
        public DateTime QDSJ { get; set; }
        public double HTJE { get; set; }
        public string CCODE { get; set; }

        [EnumValueColumn(true)]
        public ContractState STATE { get; set; }

        [Pk]
        public string DZJGH { get; set; }
    }
}
