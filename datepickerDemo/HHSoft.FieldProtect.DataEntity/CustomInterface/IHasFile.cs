using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.ItemManage;

namespace HHSoft.FieldProtect.DataEntity.CustomInterface
{
    public interface IHasFile
    {
        string FileNameColumnName { get; }
        List<Item_File> Files { get; set; }
        List<string> DelFiles { get; set; }
    }
}
