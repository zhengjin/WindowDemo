using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Security;

using ProjectCore;
using ProjectDAL;
using Telerik.Web.UI;

namespace CustomerDirect.OnlinePayment.Manager
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        private UserBLL BLL_User;

        protected void Page_Load(object sender, EventArgs e)
        {
            //判断用户是否已经登录
            string LoginID = HttpContext.Current.User.Identity.Name;

            if (string.IsNullOrEmpty(LoginID) || string.IsNullOrWhiteSpace(LoginID))
            {
                FormsAuthentication.RedirectToLoginPage();
                return;
            }
            else
            {

                BLL_User = new UserBLL();
                string strGetuser = BLL_User.GetLoginUserName();
                if (strGetuser != string.Empty)
                {
                    lbl_Welcome.Text = "Welcome," + strGetuser;
                    RadMenuBing();
                }
            }
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
            UserID = HttpContext.Current.User.Identity.Name;


            // UserID = "806858E6-034F-4DE5-A57F-7A3089BFC480";
            using (MWDatabaseEntities MWDB = new MWDatabaseEntities())
            {
                if (!string.IsNullOrEmpty(UserID) && !string.IsNullOrWhiteSpace(UserID))
                {
                    //转换成Guid类型
                    System.Guid UserID_Gu = new Guid(UserID);

                    //查询 tblUser表数据
                    tblUser tblUser = MWDB.tblUser.FirstOrDefault(u => u.UserID == UserID_Gu);
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

                    System.Guid UserID_G = new Guid(RoleID);
                    string NodeLevel = "1";

                    //通过获取的角色id查询绑定横向菜单            
                    var objtbl = MWDB.View_Menu_Permission_Role.Where<View_Menu_Permission_Role>
                        (r => r.RoleID == UserID_G && r.NodeLevel == NodeLevel).Distinct().OrderBy(rr => rr.Seq);

                    if (objtbl != null)
                    {

                        List<MenuDataItem> CurrMenuData = GetMenuData((ObjectQuery)objtbl);

                        //初始化数据绑定字段
                        RadMenuMain.DataTextField = "Text";
                        RadMenuMain.DataFieldID = "ID";
                        RadMenuMain.DataValueField = "ID";
                        RadMenuMain.DataFieldParentID = "ParentID";
                        RadMenuMain.DataNavigateUrlField = "Url";

                        //绑定数据
                        RadMenuMain.DataSource = CurrMenuData;
                        RadMenuMain.DataBind();

                        //设置分隔线
                        foreach (MenuDataItem it in CurrMenuData)
                        {
                            if (it.IsSeparator == "True")
                            {
                                RadMenuItem mi = RadMenuMain.FindItemByValue(it.ID.ToString());
                                mi.IsSeparator = true;
                            }
                        }
                    }
                }
                else
                {
                    FormsAuthentication.RedirectToLoginPage();
                    return;
                }
            }
        }
        #endregion

        protected void lbtnLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/Default.aspx");
        }

        //生成菜单项数据
        private List<MenuDataItem> GetMenuData(ObjectQuery DataSource)
        {
            List<MenuDataItem> menuData = new List<MenuDataItem>();

            int PrevGroupIndex = default(int);
            int CurrGroupIndex = default(int);
            Guid? PrevParent = default(Guid);
            Guid? CurrParent = default(Guid);

            foreach (View_Menu_Permission_Role item in DataSource)
            {

                CurrGroupIndex = item.GroupBy == null ? 0 : (int)item.GroupBy;
                CurrParent = item.ParentNode;

                if (CurrGroupIndex != PrevGroupIndex
                    && CurrParent == PrevParent)
                {
                    menuData.Add(new MenuDataItem(Guid.NewGuid(), item.ParentNode, "", "", "True"));
                }

                menuData.Add(new MenuDataItem(item.MenuID, item.ParentNode, item.MenuName, item.URL, "False"));

                PrevGroupIndex = item.GroupBy == null ? 0 : (int)item.GroupBy;
                PrevParent = item.ParentNode;
            }

            return menuData;
        }
    }
}
