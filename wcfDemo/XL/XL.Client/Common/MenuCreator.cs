using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XL.Models.Sys;
using XL.ServiceAPI.Sys;
using System.Windows.Forms;
using System.Drawing;

namespace XL.Client
{
    partial class MainForm
    {
        /// <summary>
        /// 菜单缓存
        /// </summary>
        public List<MenuModel> Menus;
        /// <summary>
        /// 初始化菜单
        /// </summary>
        private void InitMenu()
        {
            PrepareMenus();
            CreateTopMenu();
        }
        /// <summary>
        /// 从WCF获取所有菜单
        /// </summary>
        private void PrepareMenus()
        {
            var factory = new Common.ClientFactory<IMenu>();
            try
            {
                var client = factory.CreateClient();
                Menus = client.GetAllMenu();
            }
            catch (Exception ex)
            {
                Utils.OnException(ex);
            }
            factory.Dispose();
        }
        /// <summary>
        /// 创建顶部菜单
        /// </summary>
        private void CreateTopMenu()
        {
            var tmms = (from v in Menus where v.ParentId == Guid.Empty orderby v.OrderNum select v).ToList();
            for (var i = 0; i < tmms.Count; i++)
            {
                var ctl = CreateOneTopMenu(tmms[i], i);
                TopMenuP.Controls.Add(ctl);
            }
        }
        /// <summary>
        /// 创建子菜单
        /// </summary>
        /// <param name="tm"></param>
        private void CreateSubMenu(MenuModel tm)
        {
            SubHeaderLB.Text = tm.MenuName;
            SubMenuP.Controls.Clear();
            var smms = (from v in Menus where v.ParentId == tm.Id orderby v.OrderNum select v).ToList();
            for (var i = 0; i < smms.Count; i++)
            {
                var ctl = CreateOneSubMenu(smms[i], i);
                SubMenuP.Controls.Add(ctl);
            }
        }
        /// <summary>
        /// 创建一个顶部菜单
        /// </summary>
        /// <param name="m"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private Control CreateOneTopMenu(MenuModel m, int index)
        {
            var tm = new Label();
            tm.Width = 68;
            tm.Height = 40;
            tm.Text = m.MenuName;
            tm.TextAlign = ContentAlignment.MiddleCenter;
            tm.BackColor = Color.Transparent;
            tm.Left = index * 68 + index * 12 + 12;
            tm.Top = 9;
            tm.Cursor = Cursors.Hand;
            tm.Tag = m;
            tm.MouseEnter += new EventHandler(tm_MouseEnter);
            tm.MouseLeave += new EventHandler(tm_MouseLeave);
            tm.MouseUp += new MouseEventHandler(tm_MouseUp);
            return tm;
        }
        /// <summary>
        /// 创建一个子菜单
        /// </summary>
        /// <param name="m"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private Control CreateOneSubMenu(MenuModel m, int index)
        {
            var sm = new Label();
            sm.Width = SubHeaderLB.Width;
            sm.Height = 27;
            sm.Text = m.MenuName;
            sm.TextAlign = ContentAlignment.MiddleCenter;
            sm.BackColor = Color.Transparent;
            sm.Top = index * 27 + index * 9 + 9;
            sm.Left = 0;
            sm.Anchor = (System.Windows.Forms.AnchorStyles)(AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            sm.Cursor = Cursors.Hand;
            sm.Tag = m;
            sm.MouseEnter += new EventHandler(sm_MouseEnter);
            sm.MouseLeave += new EventHandler(sm_MouseLeave);
            sm.MouseUp += new MouseEventHandler(sm_MouseUp);
            return sm;
        }
        /// <summary>
        /// 创建一个业务窗体;包括tab按钮
        /// </summary>
        /// <param name="m"></param>
        private BaseForm CreateOneForm(MenuModel m)
        {
            var ass = this.GetType().Assembly;
            var url = string.Format("XL.Client.Forms.{0}", m.Url);
            BaseForm f = null;
            try
            {
                f = ass.CreateInstance(url) as BaseForm;
            }
            catch
            {
                Utils.Alert("没有与此菜单相关的业务窗体");
                return null;
            }
            f.Dock = DockStyle.Fill;
            f.FormMenu = m;
            var tabBtn = new TabBTN();
            tabBtn.OnClose += new EventHandler(f.tbn_OnClose);
            tabBtn.OnSelect += new EventHandler(f.tbn_OnSelect);
            tabBtn.Caption = m.MenuName;
            int left = 6;
            var tabCount = TabContainerP.Controls.Count;
            if (tabCount > 0)
            {
                var lastTab = TabContainerP.Controls[tabCount - 1];
                left += (lastTab.Left + lastTab.Width);
            }
            tabBtn.Left = left;
            TabContainerP.Controls.Add(tabBtn);
            f.FormTabBTN = tabBtn;
            return f;
        }
        /// <summary>
        /// 判断是否为打开的窗口
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private BaseForm FindOpenedForm(string url)
        {
            foreach (var f in FormHistory)
            {
                var ft = f.GetType().Name;
                if (url.EndsWith(ft))
                {
                    return f;
                }
            }
            return null;
        }
        /// <summary>
        /// 子菜单弹起事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void sm_MouseUp(object sender, MouseEventArgs e)
        {
            var lb = sender as Label;
            var m = lb.Tag as MenuModel;
            if (string.IsNullOrEmpty(m.Url))
            {
                Utils.Alert("没有与此菜单相关的业务窗体");
                return;
            }
            BaseForm f = null;
            foreach(var hf in FormHistory)
            {
                if (hf.FormMenu.Url.Equals(m.Url))
                {
                    f = hf;
                    break;
                }
            }
            if (f == null)
            {
                f = CreateOneForm(m);
                f.SubMenu = lb;
            }
            if (f != null&&!f.Visible)
            {
                f.Show();
            }
        }
        /// <summary>
        /// 子菜单滑出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void sm_MouseLeave(object sender, EventArgs e)
        {
            var lb = sender as Label;            
            if (FormHistory.Count > 0)
            {
                if (FormHistory[0].FormMenu.Equals(lb.Tag))
                {
                    return;
                }
            }
            lb.BackColor = Color.Transparent;
        }
        /// <summary>
        /// 子菜单滑入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void sm_MouseEnter(object sender, EventArgs e)
        {
            var lb = sender as Label;
            if (FormHistory.Count > 0)
            {
                if (FormHistory[0].FormMenu.Equals(lb.Tag))
                {
                    return;
                }
            }
            lb.BackColor = SystemColors.Info;
        }
        /// <summary>
        /// 顶部菜单鼠标滑出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tm_MouseLeave(object sender, EventArgs e)
        {
            var lb = sender as Label;
            lb.BackColor = Color.Transparent;
            lb.ForeColor = SystemColors.ControlText;
        }
        /// <summary>
        /// 顶部菜单鼠标滑入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tm_MouseEnter(object sender, EventArgs e)
        {
            var lb = sender as Label;
            lb.BackColor = Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(96)))), ((int)(((byte)(130)))));
            lb.ForeColor = Color.White;
        }
        /// <summary>
        /// 顶部菜单鼠标弹起
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tm_MouseUp(object sender, MouseEventArgs e)
        {
            var lb = sender as Label;
            var m = lb.Tag as MenuModel;
            CreateSubMenu(m);
        }
    }
}
