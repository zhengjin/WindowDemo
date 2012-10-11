using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using ProjectCore;
using ProjectCore.Model;
using ProjectDAL;
using Telerik.Web.UI;

namespace WebApplication1.Authorization
{
    public partial class UserManagerForm : System.Web.UI.Page
    {
        #region 属性
        /// <summary>
        /// 主键值
        /// </summary>
        private string strKeyID
        {
            get
            {
                object o = ViewState["strKeyID"];
                return o == null ? string.Empty : o.ToString();
            }
            set { ViewState["strKeyID"] = value; }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            this.GridViewData();
        }

        #region GridView绑定数据
        /// <summary>
        /// GridView绑定数据
        /// </summary>
        public void GridViewData()
        {
            //实例化EntityDataSourceModel 属性类
            EntityDataSourceModel eModel = new EntityDataSourceModel();
            UserBLL UserBLLs = new UserBLL();
            eModel = UserBLLs.GetEModel();
            EntityDataSourceGridUser.EntitySetName = eModel.EntitySetName;
            if (!string.IsNullOrEmpty(eModel.Where))
            {
                EntityDataSourceGridUser.Where = eModel.Where;
            }
            if (!string.IsNullOrEmpty(eModel.OrderBy))
            {
                EntityDataSourceGridUser.OrderBy = eModel.OrderBy;
            }
            EntityDataSourceGridUser.DefaultContainerName = eModel.DefaultContainerName;
            EntityDataSourceGridUser.ConnectionString = eModel.ConnectionString;
        }
        #endregion

        #region GridView的InsertCommand事件
        /// <summary>
        /// GridView的InsertCommand事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridUser_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem edittedItem = e.Item.OwnerTableView.GetInsertItem();
            //获取控件            
            RadTextBox textUserName = edittedItem.FindControl("textUserName") as RadTextBox;
            RadTextBox textLoginName = edittedItem.FindControl("textLoginName") as RadTextBox;
            RadTextBox textUserCode = edittedItem.FindControl("textUserCode") as RadTextBox;
            RadTextBox textEmail = edittedItem.FindControl("textEmail") as RadTextBox;
            RadTextBox textDesc = edittedItem.FindControl("textDesc") as RadTextBox;
            RadTextBox textLoginPwd = edittedItem.FindControl("textLoginPwd") as RadTextBox;
            
            RadComboBox ComBoxState = edittedItem.FindControl("ComBoxState") as RadComboBox;
            RadComboBox ComBoxRole = edittedItem.FindControl("ComBoxRole") as RadComboBox;
            System.Guid gTemp = System.Guid.NewGuid();
            UserBLL UserBLLs = new UserBLL();
            if (UserBLLs.ExistsLogName(textLoginName.Text))
            {
                //The user already exists
                //该用户已存在
                RadWindowManager1.RadAlert("The user already exists!", 300, 100, "Info", "");
                return;
            }
            //实例化Entity实体
            tblUser UserObj = new tblUser();
            UserObj.UserName = textUserName.Text;
            UserObj.LoginName = textLoginName.Text;
            UserObj.LoginPwd = textLoginPwd.Text;
            UserObj.UserCode = textUserCode.Text;
            UserObj.State = Convert.ToBoolean(ComBoxState.SelectedItem.Value);
            UserObj.Email = textEmail.Text;
            UserObj.Desc = textDesc.Text;
            UserObj.UserID = gTemp;

            UserBLLs.Add(UserObj);
            //创建目录文件夹
            Directory.CreateDirectory(Request.PhysicalApplicationPath + "FileUpLoad/" + gTemp);
            //*************************************************给用户配置角色
            RoleUserBLL Role_UserBLLs = new RoleUserBLL();
            if (ComBoxRole.SelectedItem.Value != string.Empty)
            {

                tblRole_User tblRole_UserObj = new tblRole_User();
                System.Guid gUserID = gTemp;
                System.Guid gRoleID = new Guid(ComBoxRole.SelectedItem.Value);
                tblRole_UserObj.UserID = gUserID;
                tblRole_UserObj.RoleID = gRoleID;
                Role_UserBLLs.Add(tblRole_UserObj);
            }
            //*************************************************给用户配置角色
            this.GridViewData();

        }
        #endregion

        #region GridView 的UpdateCommand 事件
        /// <summary>
        /// GridView 的UpdateCommand 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridUser_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem edittedItem = e.Item as GridEditableItem;
            //获取控件            
            RadTextBox textUserName = edittedItem.FindControl("textUserName") as RadTextBox;
            RadTextBox textLoginName = edittedItem.FindControl("textLoginName") as RadTextBox;
            RadTextBox textUserCode = edittedItem.FindControl("textUserCode") as RadTextBox;
            RadTextBox textEmail = edittedItem.FindControl("textEmail") as RadTextBox;
            RadTextBox textDesc = edittedItem.FindControl("textDesc") as RadTextBox;
            RadComboBox ComBoxState = edittedItem.FindControl("ComBoxState") as RadComboBox;
            RadComboBox ComBoxRole = edittedItem.FindControl("ComBoxRole") as RadComboBox;

            //*************************************************给用户配置角色
            RoleUserBLL Role_UserBLLs = new RoleUserBLL();

            if (ComBoxRole.SelectedItem.Value != string.Empty)
            {
                //删除关系表
                Role_UserBLLs.Delete(this.strKeyID);
                tblRole_User tblRole_UserObj = new tblRole_User();
                System.Guid gUserID = new Guid(this.strKeyID);
                System.Guid gRoleID = new Guid(ComBoxRole.SelectedItem.Value);
                tblRole_UserObj.UserID = gUserID;
                tblRole_UserObj.RoleID = gRoleID;
                Role_UserBLLs.Add(tblRole_UserObj);

            }
            //*************************************************给用户配置角色
            //实例化Entity实体
            UserBLL UserBLLs = new UserBLL();
            tblUser UserObjs;
            UserObjs = UserBLLs.GetByID(this.strKeyID);
            UserObjs.UserName = textUserName.Text;
            UserObjs.LoginName = textLoginName.Text;
            
            UserObjs.UserCode = textUserCode.Text;
            if (ComBoxState.SelectedItem.Value == "True")
            {
                UserObjs.State = true;
            }
            else
            {
                UserObjs.State = false;
            }
            
            UserObjs.Email = textEmail.Text;
            UserObjs.Desc = textDesc.Text;

            UserBLLs.Update(UserObjs);
            this.GridViewData();
        }
        #endregion



        #region 删除事件
        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridUser_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            //获取Grid的主键值
            string GridKey = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["UserID"].ToString();
            UserBLL UserBLLs = new UserBLL();
            string strGetuserID = HttpContext.Current.User.Identity.Name;
            if (strGetuserID == GridKey)
            {
                //当前用户不能删除！ 警告
                string strInfo = GetGlobalResourceObject("en_US", "UserManagerForm_lblDesc_DeleteInfo").ToString();
                RadWindowManager1.RadAlert(strInfo, 300, 100, "Warn", "");
                return;
            }
            if (GridKey != string.Empty)
            {              
                tblUser UserObjs;
                UserObjs = UserBLLs.GetByID(GridKey);
                if (UserObjs.UserName == "admin")
                {
                    //管理员不能删除！ 警告Admin Can't delete!
                    string strInfo = GetGlobalResourceObject("en_US", "UserManagerForm_lblDesc_DeleteInfoAdmin").ToString();
                    RadWindowManager1.RadAlert(strInfo, 300, 100, "Warn", "");
                    return;
                }
                UserBLLs.Delete(GridKey);
                RoleUserBLL Role_UserBLLs = new RoleUserBLL();
                //删除关系表
                Role_UserBLLs.Delete(GridKey);
            }
            this.GridViewData();
        }
        #endregion

        protected void GridUser_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                //插入状态
                if (e.Item.OwnerTableView.IsItemInserted)
                {
                    //***********************************Role 下拉列表加载数据
                    RoleBLL RoleBLLs = new RoleBLL();
                    List<View_Role> query = null;
                    query = RoleBLLs.GetAll("", " it.RoleName desc");
                    if (query != null && query.Count() > 0)
                    {
                        GridEditFormItem edittedItem = (GridEditFormItem)e.Item;
                        RadComboBox ComBoxRole = edittedItem.FindControl("ComBoxRole") as RadComboBox;
                        ComBoxRole.DataSource = query;
                        ComBoxRole.DataTextField = "RoleName";
                        ComBoxRole.DataValueField = "RoleID";
                        ComBoxRole.DataBind();
                    }
                    //***********************************Role 下拉列表加载数据
                }
                //编辑状态
                else
                {
                    //获取Grid的主键值
                    this.strKeyID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["UserID"].ToString();
                    //判断主键是否为空
                    if (this.strKeyID != string.Empty)
                    {
                        //装换Guid类型
                        System.Guid Guid = new Guid(this.strKeyID);
                        //实例化Entity实体
                        using (MWDatabaseEntities MWDB = new MWDatabaseEntities())
                        {
                            //通过主键查询数据表获取外键字段 control
                            tblUser tblUsers = MWDB.tblUser.FirstOrDefault(d => d.UserID == Guid);

                            if (tblUsers != null)
                            {
                                //加载d_DicType

                                GridEditFormItem edittedItem = (GridEditFormItem)e.Item;

                                //获取控件
                                RadTextBox textUserName = edittedItem.FindControl("textUserName") as RadTextBox;
                                RadTextBox textLoginName = edittedItem.FindControl("textLoginName") as RadTextBox;
                                RadTextBox textUserCode = edittedItem.FindControl("textUserCode") as RadTextBox;
                                RadTextBox textEmail = edittedItem.FindControl("textEmail") as RadTextBox;
                                RadTextBox textDesc = edittedItem.FindControl("textDesc") as RadTextBox;

                                RadComboBox ComBoxState = edittedItem.FindControl("ComBoxState") as RadComboBox;
                                RadComboBox ComBoxRole = edittedItem.FindControl("ComBoxRole") as RadComboBox;
                                RoleBLL RoleBLLs = new RoleBLL();
                                //***********************************Role 下拉列表加载数据
                                List<View_Role> query = null;
                                query = RoleBLLs.GetAll("", " it.RoleName desc");
                                if (query != null && query.Count() > 0)
                                {
                                    ComBoxRole.DataSource = query;
                                    ComBoxRole.DataTextField = "RoleName";
                                    ComBoxRole.DataValueField = "RoleID";
                                    ComBoxRole.DataBind();
                                }
                                //***********************************Role 下拉列表加载数据

                                RoleUserBLL Role_UserBLLs = new RoleUserBLL();
                                tblRole_User tblRole_UserObj = null;
                                tblRole_UserObj = Role_UserBLLs.GetByID(this.strKeyID);
                                if (tblRole_UserObj != null)
                                {
                                    ComBoxRole.SelectedIndex = ComBoxRole.Items.IndexOf(ComBoxRole.Items.FindItemByValue(tblRole_UserObj.RoleID.ToString(), true));
                                }
                                //为控件赋值
                                textUserName.Text = tblUsers.UserName;
                                textLoginName.Text = tblUsers.LoginName;

                                textUserCode.Text = tblUsers.UserCode;
                                textEmail.Text = tblUsers.Email;
                                textDesc.Text = tblUsers.Desc;
                                //下拉列表编辑时选择
                                ComBoxState.SelectedIndex = ComBoxState.Items.IndexOf(ComBoxState.Items.FindItemByValue(tblUsers.State.ToString()));

                            }
                        }
                    }
                }
            }
        }

        protected void GridUser_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                HyperLink editLink = (HyperLink)e.Item.FindControl("EditLink");
                editLink.Attributes["href"] = "#";
                editLink.Attributes["onclick"] = String.Format("return ShowEditForm('{0}','{1}');", e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["UserID"], e.Item.ItemIndex);
            }
        }
    }
}