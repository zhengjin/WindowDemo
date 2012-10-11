using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HHSoft.FieldProtect.DataEntity.CustomAttribute
{
    public class FileNameUrlAttribute : Attribute
    {
        public bool IsFileNameUrlColumn { get; set; }

        public FileNameUrlAttribute()
        {
            IsFileNameUrlColumn = true;
        }
    }
}
