using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XL.Models.Sys;
using XL.ServiceAPI.Sys;

namespace XL.Client.Forms.Sys
{
    public partial class MenuForm : BaseForm
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            BindTree();
        }
        private void BindTree()
        {
            var mf = Utils.GetMainForm();
            if (mf != null)
            {
                var nodes = new List<TreeNode>();
                var ms = mf.Menus.OrderBy(m => m.OrderNum);
                foreach (var tm in ms)
                {
                    if (tm.ParentId != Guid.Empty)
                    {
                        continue;
                    }
                    var tn = new TreeNode();
                    tn.Text = tm.MenuName;
                    tn.Tag = tm;
                    foreach (var sm in ms)
                    {
                        if (tm.Id != sm.ParentId)
                        {
                            continue;
                        }
                        var sn = new TreeNode();
                        sn.Text = sm.MenuName;
                        sn.Tag = sm;
                        tn.Nodes.Add(sn);
                    }
                    tn.Expand();
                    nodes.Add(tn);
                }
                treeView1.Nodes.AddRange(nodes.ToArray());
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var tn = treeView1.SelectedNode;
            var mm = tn.Tag as MenuModel;
            textBox1.Text = mm.MenuName;
            textBox3.Text = mm.Url;
            maskedTextBox1.Text = mm.OrderNum.ToString(); 
            textBox4.Text = mm.MenuDes;
            groupBox2.Text = string.Format("修改菜单-{0}", mm.MenuName);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var tn = treeView1.SelectedNode;
            var mm = tn.Tag as MenuModel;
            mm.MenuName = textBox1.Text.Trim();
            mm.OrderNum = Convert.ToInt32(maskedTextBox1.Text);
            mm.Url = textBox3.Text.Trim();
            mm.MenuDes = textBox4.Text;
            var ch = new Common.ClientFactory<IMenu>();
            ch.CreateClient().EditMenu(mm);            
            ch.Dispose();
            Utils.Alert("修改成功");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
