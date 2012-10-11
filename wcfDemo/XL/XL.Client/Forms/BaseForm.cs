using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XL.Models.Sys;

namespace XL.Client
{
    public partial class BaseForm : Form
    {
        /// <summary>
        /// 菜单数据
        /// </summary>
        public MenuModel FormMenu { get; set; }
        /// <summary>
        /// tab按钮
        /// </summary>
        public TabBTN FormTabBTN { get; set; }
        /// <summary>
        /// 子菜单
        /// </summary>
        public Label SubMenu { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseForm()
        {
            InitializeComponent();
            this.TopLevel = false;
        }
        /// <summary>
        /// 窗体关闭（子窗体内调用close方法与点击TAB按钮的close按钮都会触发此事件）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseForm_FormClosed(object sender, FormClosedEventArgs e)
        {            
        }
        /// <summary>
        /// 窗体加载；创建tab按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BaseForm_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// tab按钮选中事件；
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void tbn_OnSelect(object sender, EventArgs e)
        {
            this.Show();
        }
        /// <summary>
        /// tab按钮关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void tbn_OnClose(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BaseForm_VisibleChanged(object sender, EventArgs e)
        {
            if (Utils.IsInDesignMode())
            {
                return;
            }
            this.VisibleChanged -= new EventHandler(BaseForm_VisibleChanged);
            var mf = Utils.GetMainForm();
            if (this.Visible)
            {
                foreach (var hf in mf.FormHistory)
                {
                    if (hf.FormMenu.Url.Equals(this.FormMenu.Url))
                    {
                        continue;
                    }
                    if (hf.Visible)
                    {
                        hf.Hide();
                    }
                }
                FormTabBTN.SelectMe();
                mf.FormHistory.Remove(this);
                mf.FormHistory.Insert(0, this);
                mf.MainContainerP.Controls.Clear();
                mf.MainContainerP.Controls.Add(this);
                SubMenu.BackColor = SystemColors.Info;
                //TODO:系统名称可以做到数据库里去
                mf.Text = string.Format("XXX管理系统-{0}", FormMenu.MenuName);
            }
            else
            {                
                FormTabBTN.DisSelectMe();
                SubMenu.BackColor = Color.Transparent;
            }
            this.VisibleChanged += new EventHandler(BaseForm_VisibleChanged);
        }

        private void BaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.VisibleChanged -= new EventHandler(BaseForm_VisibleChanged);
            var mf = Utils.GetMainForm();
            mf.FormHistory.Remove(this);
            this.SubMenu.BackColor = Color.Transparent;
            if (mf.FormHistory.Count > 0)
            {
                mf.FormHistory[0].Show();
            }
            foreach (var f in mf.FormHistory)
            {
                if (f.FormTabBTN.Left > FormTabBTN.Left)
                {
                    f.FormTabBTN.Left -= (FormTabBTN.Width + 6);
                }
            }
            mf.TabContainerP.Controls.Remove(FormTabBTN);
        }
    }
}
