using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HHSoft.FieldProtect.DataEntity.CustomAttribute
{
    public class QueryAttribute : Attribute
    {
        public string DisplayName { get; set; }
        public int Index { get; set; }
        public bool Default { get; set; }
    }
}
