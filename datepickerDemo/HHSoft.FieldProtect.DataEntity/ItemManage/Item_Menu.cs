using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HHSoft.FieldProtect.DataEntity.ItemManage
{
    /// <summary>
    /// 环节菜单
    /// </summary>
    [Serializable]
    public class Item_Menu
    {
        public Item_Menu(string menuname, string menuurl,bool visable, bool isdefault)
        {            
            this.MenuName = menuname;
            this.MenuUrl = menuurl;
            this.IsVisable = visable;
            this.IsDefault = isdefault;            
        }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public virtual string MenuName
        {
            get;
            set;
        }

        /// <summary>
        /// 菜单地址
        /// </summary>
        public virtual string MenuUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 是否默认菜单
        /// </summary>
        public virtual bool IsDefault
        {
            get;
            set;
        }

        /// <summary>
        /// 默认是否显示
        /// </summary>
        public virtual bool IsVisable
        {
            get;
            set;
        }
    }
}
