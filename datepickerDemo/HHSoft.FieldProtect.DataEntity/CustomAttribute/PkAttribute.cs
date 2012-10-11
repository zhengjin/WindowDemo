using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HHSoft.FieldProtect.DataEntity.CustomAttribute
{
    public class PkAttribute : Attribute
    {
        public bool IsPk { get; set; }

        public PkAttribute()
        {
            IsPk = true;
        }
    }
}
