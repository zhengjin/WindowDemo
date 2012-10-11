using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HHSoft.FieldProtect.DataEntity.CustomAttribute
{
    public class ViewInDataViewAttribute : Attribute
    {
        public string DisplayName { get; set; }
        public int Index { get; set; }
        public string DisplayNameGroups { get; set; }
        public int MaxLength { get; set; }
    }
}
