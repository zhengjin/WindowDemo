using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DAL;
using System.Data;
using Telerik.Web.UI;

namespace Mobile_Work_Order_Demo
{
    public partial class Login : System.Web.UI.Page
    {
        #region 属性
        /// <summary>
        /// 排序
        /// </summary>
        private string OrderBy
        {
            get
            {
                object o = ViewState["OrderBy"];
                return o == null ? string.Empty : o.ToString();
            }
            set { ViewState["OrderBy"] = value; }
        }
        /// <summary>
        /// 查询条件
        /// </summary>
        private string SearchStr
        {
            get
            {
                object o = ViewState["SearchStr"];
                return o == null ? string.Empty : o.ToString();
            }
            set { ViewState["SearchStr"] = value; }
        }
        #endregion

       UnicornDBEntities UnicornDB = new UnicornDBEntities();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.OrderBy = "it.UserName desc";
                this.DataBing("");
                RadFilterEqualToFilterExpression<string> expr1 = new RadFilterEqualToFilterExpression<string>("UserName");
                //expr1.Value = "10248";
                RadFilter1.RootGroup.Expressions.Add(expr1);
                
            }
            
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

        public void DataBing(string strWhere)
        {
            List<tblUser> objList = null;
            objList = this.GetActionLog(strWhere, this.OrderBy);
            RadGrid1.DataSource = objList;
            RadGrid1.DataBind();
        }

        public virtual List<tblUser> GetActionLog(string searchStr, string orderBy)
        {
            string strWhere = " it.UserID is not null " + searchStr;
            List<tblUser> query = null;
            try
            {

                query = UnicornDB.tblUser.Where(strWhere).OrderBy(orderBy).ToList();
            }
            catch (EntitySqlException)
            {
                throw;
            }
            return query;
        }

        protected void RadGrid1_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            this.DataBing("");
        }

        protected void RadGrid1_PageSizeChanged(object sender, Telerik.Web.UI.GridPageSizeChangedEventArgs e)
        {           
            this.DataBing("");
        }

        protected void LinkButton5_Click(object sender, EventArgs e)
        {
            
            //RadFilterEqualToFilterExpression<string> expr1 = new RadFilterEqualToFilterExpression<string>("LoginName");
            RadFilterEqualToFilterExpression<string> expr1 = new RadFilterEqualToFilterExpression<string>("LoginName");
            string str= expr1.Value;
            string str1 = expr1.FieldName;
            string str2 = expr1.ToString();
            string ss = RadFilter1.ApplyButtonText;
            string ssss = RadFilter1.BetweenDelimeterText;
            string strds= RadFilter1.FieldEditors[0].DisplayName;
            string strds2 = RadFilter1.FieldEditors[0].FieldName;
          
        }


    }
}