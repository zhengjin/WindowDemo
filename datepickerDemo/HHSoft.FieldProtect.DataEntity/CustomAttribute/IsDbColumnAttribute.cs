using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HHSoft.FieldProtect.DataEntity.CustomAttribute
{
    public class IsDbColumnAttribute : Attribute
    {
        public bool IsDbColumn { get; set; }

        public IsDbColumnAttribute(bool isOrNot)
        {
            IsDbColumn = isOrNot;
        }
    }
}
