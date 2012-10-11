using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Web.UI;

namespace HHSoft.FieldProtect.Framework.Control
{
    /// <summary>
    /// RepeaterPlusNone 的摘要说明。
    /// </summary>
    public class HHRepeater : Repeater
    {

        #region 公共属性

        /// <summary>
        /// The template that is used to define what is displayed when no items are found.
        /// </summary>
        [
        Browsable(false),
        DefaultValue(null),
        Description("Defines the ITemplate to be used when no items are defined in the datasource."),
        PersistenceMode(PersistenceMode.InnerProperty),
        ]
        public virtual ITemplate NoneTemplate
        {
            get { return _noneTemplate; }
            set { _noneTemplate = value; }
        }
        private ITemplate _noneTemplate;

        [DefaultValue(true)]
        public virtual bool ShowHeaderFooterOnNone
        {
            get
            {
                Object state = ViewState["ShowHeaderFooterOnNone"];
                if (state != null)
                    return (bool)state;
                return true;
            }
            set { ViewState["ShowHeaderFooterOnNone"] = value; }
        }

        private string imagesorturl = "../../Image/sort.gif";
        /// <summary>
        /// 排序标志图片地址
        /// </summary>
        public string ImageSortUrl
        {
            get { return imagesorturl; }
            set { this.imagesorturl = value; }
        }

        private string imageascurl = "../../Image/sortasc.gif";
        /// <summary>
        /// 升序图片地址
        /// </summary>
        public string ImageAscUrl
        {
            get { return imageascurl; }
            set { this.imageascurl = value; }
        }

        private string imagedescurl = "../../Image/sortdesc.gif";
        /// <summary>
        /// 降序图片地址
        /// </summary>
        public string ImageDescUrl
        {
            get { return imagedescurl; }
            set { this.imagedescurl = value; }
        }

        /// <summary>
        /// 当前排序字段
        /// </summary>
        private string currentExpression
        {
            get
            {
                if (ViewState["CurrentExpression"] != null)
                    return (string)ViewState["CurrentExpression"];
                else
                    return string.Empty;
            }
            set
            {
                ViewState["CurrentExpression"] = value;
            }
        }
        /// <summary>
        /// 当前排序方向
        /// </summary>
        private bool currentDirection
        {
            get
            {
                if (ViewState["CurrentDirection"] != null)
                    return (bool)ViewState["CurrentDirection"];
                else
                    return true;
            }
            set
            {
                ViewState["CurrentDirection"] = value;
            }
        }
      
        #endregion

        #region 重写事件

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
            if ((Items.Count == 0) && (NoneTemplate != null))
            {
                this.Controls.Clear();
                //执行空数据源模板
                RepeaterItem noneItem = new RepeaterItem(-1, ListItemType.Item);
                RepeaterItemEventArgs noneArgs = new RepeaterItemEventArgs(noneItem);
                NoneTemplate.InstantiateIn(noneItem);
                this.OnItemCreated(noneArgs);
                this.Controls.Add(noneItem);
                OnNoneItemsDataBound(noneArgs);                 
                this.ChildControlsCreated = true;
            }
        }

        protected override void OnItemCreated(RepeaterItemEventArgs e)
        {
            //base.OnItemCreated(e);           
            if (e.Item.ItemType == ListItemType.Header)
            {
                foreach (System.Web.UI.Control control in e.Item.Controls)
                {
                    if (control.GetType() == typeof(HHRepeaterHead))
                    {                        
                        HHRepeaterHead header = (HHRepeaterHead)control;
                        if (header.AllowSort)
                        {
                            if (string.IsNullOrEmpty(header.SortExpression))
                                throw new ArgumentNullException("排序列的排列表达式不能为空!");
                            if (currentExpression != string.Empty && currentExpression == header.SortExpression)
                            {
                                string imageurl = string.Empty;
                                string imagealt = string.Empty;
                                if (currentDirection)
                                {
                                    imageurl = imageascurl;
                                    imagealt = "升序";
                                }
                                else
                                {
                                    imageurl = imagedescurl;
                                    imagealt = "降序";
                                }
                                header.Text = string.Format("{0}<img src='{1}' alt='{2}' border='0' width='12' height='12'>", header.Text, imageurl, imagealt);
                            }
                            else
                            {
                                header.Text = string.Format("{0}<img src='{1}' alt='{2}' border='0' width='12' height='12'>", header.Text, imagesorturl, "排序");
                            }
                            header.ToolTip = "点击此处可以对此列排序";
                            header.Click += new EventHandler(header_Click);
                        }
                        else
                        {                            
                            header.OnClientClick = "return false;"; 
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 排序事件
        /// </summary>
        public event DelSort ItemDataSort;

        public delegate void DelSort(object sender, SortArgs e);

        
        /// <summary>
        /// 表头单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void header_Click(object sender, EventArgs e)
        {
            if (ItemDataSort != null)
            {
                ////表头第一次被点击
                if (currentExpression != ((HHRepeaterHead)sender).SortExpression)
                {
                    currentExpression = ((HHRepeaterHead)sender).SortExpression;
                    currentDirection = ((HHRepeaterHead)sender).SortDirection;
                }
                ////表头被连续点击
                else
                {
                    currentDirection = !currentDirection;
                }

                SortArgs Args = null;
    
                if (currentDirection)
                    Args = new SortArgs(((HHRepeaterHead)sender).SortExpression, "Asc");
                else
                    Args = new SortArgs(((HHRepeaterHead)sender).SortExpression, "Desc");
                //this.ItemDataSort(sender, Args);
                ItemDataSort(sender, Args);                
            }
        }
        
        #endregion

        #region 事件声明

        public event RepeaterItemEventHandler NoneItemsDataBound
        {
            add { base.Events.AddHandler(EventNoneItemsDataBound, value); }
            remove { base.Events.RemoveHandler(EventNoneItemsDataBound, value); }
        }

        private static readonly object EventNoneItemsDataBound = new object();
        
        protected virtual void OnNoneItemsDataBound(RepeaterItemEventArgs e)
        {
            RepeaterItemEventHandler handler = (RepeaterItemEventHandler)base.Events[EventNoneItemsDataBound];
            if (handler != null)
                handler(this, e);
        }
             

        public class SortArgs : EventArgs
        {
            public SortArgs(string sortexpression, string sortdirection)
            {
                this.SortExpression = sortexpression;
                this.SortDirection = sortdirection;
            }

            public string SortExpression
            {
                get;
                set;
            }
            public string SortDirection
            {
                get;
                set;
            }
        }

        #endregion

    }
}
