using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HHSoft.FieldProtect.DataEntity.CustomAttribute
{
    public class EnumValueColumnAttribute : Attribute
    {
        public bool IsEnumValueColumn { get; set; }

        public EnumValueColumnAttribute(bool isOrNot)
        {
            IsEnumValueColumn = isOrNot;
        }
    }
}
