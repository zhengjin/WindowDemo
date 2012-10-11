using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.SysManage;
using HHSoft.FieldProtect.FrameWork.Utility;
using System.Xml.Linq;
using System.Web;
using HHSoft.FieldProtect.DataEntity;
using HHSoft.FieldProtect.DataEntity.Sync;

namespace HHSoft.FieldProtect.Business.Common
{
    public class ItemListOperation
    {
        public List<ItemListColumn> QueryConfig(string name)
        {
            XDocument document = XDocument.Load(HttpContext.Current.Server.MapPath(SysConfig.ItemListColumnConfigWebPath));
            XDocumentOperation documentOperation = new XDocumentOperation();
            return documentOperation.ConvertFromElementsToEntities<ItemListColumn>(document.Root.Element(name).Elements());
        }
    }
}
