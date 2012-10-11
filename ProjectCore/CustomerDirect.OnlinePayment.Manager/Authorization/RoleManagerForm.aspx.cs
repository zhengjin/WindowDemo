using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProjectCore;
using ProjectCore.Model;
using ProjectDAL;
using Telerik.Web.UI;

namespace WebApplication1.Authorization
{
    public partial class RoleManagerForm : System.Web.UI.Page
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
            RoleBLL RoleBLLs = new RoleBLL();
            eModel = RoleBLLs.GetEModel();
            EntityDataSourceRole.EntitySetName = eModel.EntitySetName;
            if (!string.IsNullOrEmpty(eModel.Where))
            {
                EntityDataSourceRole.Where = eModel.Where;
            }
            if (!string.IsNullOrEmpty(eModel.OrderBy))
            {
                EntityDataSourceRole.OrderBy = eModel.OrderBy;
            }
            EntityDataSourceRole.DefaultContainerName = eModel.DefaultContainerName;
            EntityDataSourceRole.ConnectionString = eModel.ConnectionString;
        }
        #endregion

        #region GridView的InsertCommand事件

        /// <summary>
        /// GridView的InsertCommand事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridRole_InsertCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            GridEditableItem edittedItem = e.Item.OwnerTableView.GetInsertItem();
            //获取控件            
            RadTextBox textRoleName = edittedItem.FindControl("txtRoleName") as RadTextBox;
            RadTextBox textRoleCode = edittedItem.FindControl("textRoleCode") as RadTextBox;
            RadTextBox textDesc = edittedItem.FindControl("textDesc") as RadTextBox;
            RadComboBox ComBoxState = edittedItem.FindControl("ComBoxState") as RadComboBox;

            //实例化Entity实体
            tblRole roleObj = new tblRole();
            roleObj.RoleName = textRoleName.Text;
            roleObj.RoleCode = textRoleCode.Text;
            roleObj.Desc = textDesc.Text;
            roleObj.State = Convert.ToDecimal(ComBoxState.SelectedItem.Value);
            RoleBLL RoleBLLs = new RoleBLL();
            RoleBLLs.Add(roleObj);
            this.GridViewData();
        }
        #endregion

        #region GridView 的UpdateCommand 事件

        /// <summary>
        /// GridView 的UpdateCommand 事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridRole_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            //获取控件            
            RadTextBox textRoleName = editedItem.FindControl("txtRoleName") as RadTextBox;
            RadTextBox textRoleCode = editedItem.FindControl("textRoleCode") as RadTextBox;
            RadTextBox textDesc = editedItem.FindControl("textDesc") as RadTextBox;
            RadComboBox ComBoxState = editedItem.FindControl("ComBoxState") as RadComboBox;
            //实例化Entity实体

            RoleBLL RoleBLLs = new RoleBLL();
            tblRole roleObj;
            roleObj = RoleBLLs.GetByID(this.strKeyID);
            roleObj.RoleName = textRoleName.Text;
            roleObj.RoleCode = textRoleCode.Text;
            roleObj.Desc = textDesc.Text;
            roleObj.State = Convert.ToDecimal(ComBoxState.SelectedItem.Value);

            RoleBLLs.Update(roleObj);
            this.GridViewData();
        }
        #endregion

        #region 删除事件

        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridRole_DeleteCommand(object sender, GridCommandEventArgs e)
        {
            //获取Grid的主键值
            string GridKey = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["RoleID"].ToString();
            RoleUserBLL Role_UserBLLs = new RoleUserBLL();
            tblRole_User tblRole_Users = Role_UserBLLs.GetByRoleID(GridKey);
            if (tblRole_Users != null)
            {
                if (tblRole_Users.RoleID != null)
                {
                    //正在使用不能删除！ 警告Admin Can't delete!
                    string strInfo = GetGlobalResourceObject("en_US", "RoleManagerForm_DeleteInfo").ToString();
                    RadWindowManager1.RadAlert(strInfo, 300, 100, "Warn", "");
                    return;
                }
            }
            RoleBLL RoleBLLs = new RoleBLL();
            if (GridKey != string.Empty)
            {
                RoleBLLs.Delete(GridKey);
            }
            this.GridViewData();
        }
        #endregion

        #region GridRole_ItemDataBound事件

        protected void GridRole_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridEditFormItem && e.Item.IsInEditMode)
            {
                //编辑状态
                if (!e.Item.OwnerTableView.IsItemInserted)
                {
                    //获取Grid的主键值
                    this.strKeyID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["RoleID"].ToString();
                    //判断主键是否为空
                    if (this.strKeyID != string.Empty)
                    {
                        //装换Guid类型
                        System.Guid Guid = new Guid(this.strKeyID);
                        //实例化Entity实体
                        using (MWDatabaseEntities MWDB = new MWDatabaseEntities())
                        {
                            //通过主键查询数据表获取外键字段
                            tblRole tblRoles = MWDB.tblRole.FirstOrDefault(d => d.RoleID == Guid);
                            if (tblRoles != null)
                            {
                                //加载d_DicType
                                //tbldicinfo.d_DicTypeReference.Load();
                                GridEditFormItem formItem = (GridEditFormItem)e.Item;
                                //string StrDicTypeID = tblRoles.RoleName;
                                //获取控件
                                RadTextBox textRoleName = formItem.FindControl("txtRoleName") as RadTextBox;
                                RadTextBox textRoleCode = formItem.FindControl("textRoleCode") as RadTextBox;
                                RadTextBox textDesc = formItem.FindControl("textDesc") as RadTextBox;
                                RadComboBox ComBoxState = formItem.FindControl("ComBoxState") as RadComboBox;
                                //为控件赋值
                                textRoleName.Text = tblRoles.RoleName;
                                textRoleCode.Text = tblRoles.RoleCode;
                                textDesc.Text = tblRoles.Desc;

                                //下拉列表编辑时选择
                                ComBoxState.SelectedIndex = ComBoxState.Items.IndexOf(ComBoxState.Items.FindItemByValue(tblRoles.State.ToString()));
                            }
                        }
                    }
                }
            }
        }
        #endregion

        protected void RadWindow1_Disposed(object sender, EventArgs e)
        {
            UserListDialog.VisibleOnPageLoad = false;
            UserListDialog.Dispose();
        }

        protected void GridRole_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                HyperLink editLink = (HyperLink)e.Item.FindControl("EditLink");
                editLink.Attributes["href"] = "#";
                editLink.Attributes["onclick"] = String.Format("return ShowEditForm('{0}','{1}');", e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["RoleID"], e.Item.ItemIndex);
            }
        }
    }
}