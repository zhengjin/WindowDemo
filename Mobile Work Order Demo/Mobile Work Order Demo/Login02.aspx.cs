using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DAL;

namespace Mobile_Work_Order_Demo
{
    public partial class Login02 : System.Web.UI.Page
    {
       UnicornDBEntities UnicornDB = new UnicornDBEntities();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            RadMenuBing();
        }

        #region 绑定RadMenu1数据， 横向菜单
        /// <summary>
        /// 绑定RadMenu1数据， 横向菜单
        /// </summary>
        public void RadMenuBing()
        {
            string UserID = string.Empty;
            string RoleID = string.Empty;
            //获取登陆ID
          
               // UserID = Session["UserID"].ToString();
                UserID = "A917BBA9-601C-4105-BBA6-BED06181F1AB";
           
            if (UserID != string.Empty)
            {
                //转换成Guid类型
                System.Guid UserID_Gu = new Guid(UserID);
                //查询 tblUser表数据
                tblUser tblUser = UnicornDB.tblUser.FirstOrDefault(u => u.UserID == UserID_Gu);
                if (tblUser != null)
                {
                    //加载tblRole_User的数据
                    tblUser.tblRole_User.Load();
                    foreach (tblRole_User item in tblUser.tblRole_User)
                    {
                        item.tblRoleReference.Load();
                        //得到登陆用户对应的角色id
                        RoleID = item.tblRole.RoleID.ToString();
                    }
                }
            }
            System.Guid UserID_G = new Guid(RoleID);
            //通过获取的角色id查询绑定横向菜单
            var objtbl = UnicornDB.View_Menu_Permission_Role.Where<View_Menu_Permission_Role>(r => r.RoleID == UserID_G).OrderBy(rr => rr.MenuPermissionID);
            if (objtbl != null)
            {
                RadMenu1.DataSource = objtbl;
                //显示列
                RadMenu1.DataTextField = "MenuName";
                //一级编码
                RadMenu1.DataFieldID = "MenuPermissionID";
                //Url路径
                RadMenu1.DataNavigateUrlField = "URL";
                //一级编码
                RadMenu1.DataValueField = "MenuPermissionID";
                //二级编码
                RadMenu1.DataFieldParentID = "ParentNode";
                RadMenu1.DataBind();
                //设置二级节点的Target属性。
                for (int i = 0; i < RadMenu1.Items.Count; i++)
                {
                    for (int j = 0; j < RadMenu1.Items[i].Items.Count; j++)
                    {
                        RadMenu1.Items[i].Items[j].Target = "Main";
                        for (int n = 0; n < RadMenu1.Items[i].Items[j].Items.Count; n++)
                        {
                            RadMenu1.Items[i].Items[j].Items[n].Target = "Main";
                        }
                    }
                }
            }
        }
        #endregion

    }
}