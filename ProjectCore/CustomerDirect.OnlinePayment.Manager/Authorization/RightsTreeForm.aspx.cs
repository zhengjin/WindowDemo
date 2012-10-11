using System;
using System.Collections.Generic;
using System.Linq;

using System.Data.Objects;
using ProjectCore;
using ProjectDAL;
using Telerik.Web.UI;

public partial class RightsTreeForm : System.Web.UI.Page 
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
        //this.strKeyID = Request["RoleID"];
        if (!IsPostBack)
        {
            //this.strKeyID = "339E18D5-8BE8-448B-8623-48CF7A873DAA";
            this.strKeyID = Request["RoleID"];
            if (this.strKeyID != string.Empty)
            {
                this.RadTreeData();
                this.Resume(this.strKeyID);
                this.GetRole();
            }
        }
    }

    public void GetRole()
    {
        RoleBLL RoleBLLs = new RoleBLL();
        tblRole tblRoleObj;
        tblRoleObj = RoleBLLs.GetByID(this.strKeyID);
        lbl_RoleNames.Text = tblRoleObj.RoleName;

    }

    #region RadTree 绑定数据
    /// <summary>
    /// RadTreeView 绑定数据
    /// </summary>
    public void RadTreeData()
    {
        using (MWDatabaseEntities MWDB = new MWDatabaseEntities())
        {
            var objtbl = MWDB.tblMenuRight.Where<tblMenuRight>(m => m.MenuID != null).OrderBy(rr => rr.Seq);
            RadTreeView_Rigth.DataSource = objtbl;
            RadTreeView_Rigth.DataTextField = "MenuName";
            RadTreeView_Rigth.DataValueField = "MenuID";
            RadTreeView_Rigth.DataFieldID = "MenuID";
            RadTreeView_Rigth.DataFieldParentID = "ParentNode";
            RadTreeView_Rigth.DataBind();
        }
    }
    #endregion

    #region 重置方法
    public void Resume(string strRoleID)
    {
        if (strRoleID != string.Empty)
        {
            IList<RadTreeNode> nodeCollection = RadTreeView_Rigth.CheckedNodes;
            //循环所有节点把CheckBox设为 false
            foreach (RadTreeNode node in nodeCollection)
            {
                node.Checked = false;
            }
            using (MWDatabaseEntities MWDB = new MWDatabaseEntities())
            {
                //根据选中ListBox的值查询tblRole表
                System.Guid gu = new Guid(strRoleID);
                tblRole objRole = MWDB.tblRole.First(r => r.RoleID == gu);
                if (objRole != null)
                {
                    //必须调用 Load方法，把数据先加载上去  
                    ObjectQuery<tblMenu_Permission_Role> tblMenu_Permission_Roles = MWDB.tblMenu_Permission_Role.Where(" it.RoleID=Guid '" + strRoleID + "'");
                    foreach (tblMenu_Permission_Role item in tblMenu_Permission_Roles)
                    {
                        RadListBoxItem objItem = new RadListBoxItem();
                        item.tblMenuRightReference.Load();
                        objItem.Text = item.RoleID.ToString();
                        tblMenuRight objMenut = MWDB.tblMenuRight.First(m => m.MenuID == item.MenuID);
                        if (objMenut != null)
                        {
                            RadTreeNode node = RadTreeView_Rigth.FindNodeByValue(item.MenuID.ToString());

                            if (node != null)
                            {
                                //判断是否有子节点如果有返回True ，否则返回False
                                //如果有子节点就不能选中当前的节点，如果选中的话他会自动选中所有子节点
                                if (!node.HasControls())
                                {
                                    node.Checked = true;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    #endregion

    #region 保存权限
    protected void Save(string strRoleID)
    {
        if (strRoleID != string.Empty)
        {
            using (MWDatabaseEntities MWDB = new MWDatabaseEntities())
            {
                System.Guid gu = new Guid(strRoleID);
                tblRole objRole = MWDB.tblRole.First(r => r.RoleID == gu);
                if (objRole != null)
                {
                    MenuPermissionRoleBLL Menu_Permission_RoleBLLs = new MenuPermissionRoleBLL();
                    Menu_Permission_RoleBLLs.DeleteT(strRoleID);

                    IList<RadTreeNode> nodeCollection = RadTreeView_Rigth.CheckedNodes;
                    //删除                    
                    //循环选中的节点并获取值
                    foreach (RadTreeNode node in nodeCollection)
                    {
                        if (node.Value != string.Empty)
                        {
                            tblMenu_Permission_Role tblp = new tblMenu_Permission_Role();
                            System.Guid guMenuID = new Guid(node.Value);
                            tblp.MenuID = guMenuID;
                            tblp.RoleID = gu;
                            Menu_Permission_RoleBLLs.Add(tblp);
                        }
                    }
                }
            }
        }
    }
    #endregion

    protected void butSave_Click(object sender, EventArgs e)
    {
        this.Save(this.strKeyID);
        ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CancelEdit();", true);
    }

    protected void butCancel_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(Page.GetType(), "mykey", "CancelEdit();", true);
    }
}
