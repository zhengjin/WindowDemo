using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XL.ServiceAPI.Staff;
using XL.Models;

namespace XL.Client
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            var factory = new Common.ClientFactory<ILogin>();
            UserModel CurUser = null;
            try
            {
                var client = factory.CreateClient();
                CurUser = client.Login(UserNameTB.Text.Trim(), PassWordTB.Text.Trim());
            }
            catch (Exception ex)
            {
                Utils.OnException(ex);
            }
            factory.Dispose();
            if (CurUser == null) 
            {
                Utils.Alert("用户名或者密码错误;请重新登录!");
                return;
            }
            CacheStrategy.CurUser = CurUser;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
