using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.ItemManage;
using HHSoft.FieldProtect.Framework.Utility;

namespace HHSoft.FieldProtect.DataEntity.WorkFlow
{
    [Serializable]
    public class WfNode
    {
        private string flowid;
        private string nodeid;
        private string nodename;
        private string nodedesc;
        private string nodelevel;
        private string stage;
        private string pernode;
        private string nextnode;
        private string nodedepartcode;
        private string noderoleid;
        private string nodeuserid;
        private string nodefilecode;
        private NodeType nodetype;
        private string timeout;
        private string notifyType;
        private string begintext;
        private string nexttext;
        private string historytext;
        
        private IList<Item_Menu> itemmenu = new List<Item_Menu>();

        public WfNode() { }

        public WfNode(WorkFlowNode node)
        {
            this.nodeid = ((int)node).ToString();
            this.setItemMenu(node);
        }

        private void setItemMenu(WorkFlowNode node)
        {
            switch (node)
            {
                case WorkFlowNode.Begin:
                    this.stage = ((int)ItemStage.ShenBo).ToString();
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SB, true, true));
                    break;
                case WorkFlowNode.TB:
                    this.stage = ((int)ItemStage.ShenBo).ToString();
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SB, true, true));
                    break;
                case WorkFlowNode.SX:
                    this.stage = ((int)ItemStage.ShenBo).ToString();
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SB, true, true));
                    break;
                case WorkFlowNode.KY:
                    this.stage = ((int)ItemStage.KeYan).ToString();
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SB, true, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_KY, true, true));
                    break;
                case WorkFlowNode.KYSH:
                    this.stage = ((int)ItemStage.KeYan).ToString();
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SB, true, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_KY, true, true));
                    break;
                case WorkFlowNode.GHSJYS:
                    this.stage = ((int)ItemStage.GuiHua).ToString();
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SB, true, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_KY, true, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_GHYS, true, true));
                    break;
                case WorkFlowNode.GHSJSH:
                    this.stage = ((int)ItemStage.GuiHua).ToString();
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SB, true, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_KY, true, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_GHYS, true, true));
                    break;
                case WorkFlowNode.YSSH:
                    this.stage = ((int)ItemStage.YuSuan).ToString();
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SB, true, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_KY, true, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_GHYS, true, true));
                    break;
                case WorkFlowNode.YSTZ:
                    this.stage = ((int)ItemStage.YuSuan).ToString();
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SB, true, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_KY, true, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_GHYS, true, true));
                    break;
                case WorkFlowNode.ShiShi:
                    this.stage = ((int)ItemStage.ShiShi).ToString();
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SB, false, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_KY, false, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_GHYS, false, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SS, true, true));
                    //itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SS_ZTB, true, false));
                    //itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SS_JL, true, false));
                    //itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SS_JD, true, false));
                    //itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SS_BG, true, false));
                    //itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SS_BF, true, false));
                    break;
                case WorkFlowNode.JunGong:
                    this.stage = ((int)ItemStage.JunGong).ToString();
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SB, false, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_KY, false, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_GHYS, false, false));
                    //itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SS_ZTB, true, false));
                    //itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SS_JL, true, false));
                    //itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SS_JD, true, false));
                    //itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SS_BG, true, false));
                    //itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SS_BF, true, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SS, true, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_JG, true, true));
                    break;
                case WorkFlowNode.ChuYan:
                case WorkFlowNode.ZhongYan:
                    this.stage = ((int)ItemStage.YanShou).ToString();
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SB, false, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_KY, false, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_GHYS, false, false));
                    //itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SS_ZTB, false, false));
                    //itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SS_JL, false, false));
                    //itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SS_JD, false, false));
                    //itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SS_BG, false, false));
                    //itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SS_BF, false, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SS, true, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_JG, true, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_YS, true, true));
                    break;
                case WorkFlowNode.JueSuan:
                    this.stage = ((int)ItemStage.JueSuan).ToString();
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SB, false, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_KY, false, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_GHYS, false, false));
                    //itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SS_ZTB, false, false));
                    //itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SS_JL, false, false));
                    //itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SS_JD, false, false));
                    //itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SS_BG, false, false));
                    //itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SS_BF, false, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SS, false, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_JG, false, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_YS, false, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_JS, true, true));
                    break;
                case WorkFlowNode.End:
                    this.stage = ((int)ItemStage.GuiDang).ToString();
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SB, false, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_KY, false, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_GHYS, false, false));
                    //itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SS_ZTB, false, false));
                    //itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SS_JL, false, false));
                    //itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SS_JD, false, false));
                    //itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SS_BG, false, false));
                    //itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SS_BF, false, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_SS, false, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_JG, false, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_YS, false, false));
                    itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_JS, false, false));
                    //itemmenu.Add(this.getMenu(ItemMenu.ItemInfo_GD, true, true));
                    break;
            }
        }

        private Item_Menu getMenu(ItemMenu menu, bool isVisable, bool isDefault)
        {
            return new Item_Menu(EnumHelper.GetFieldDescription(typeof(ItemMenu), (int)menu), string.Format("{0}.aspx", menu.ToString()), isVisable, isDefault);
        }

        public void AddMenu(ItemMenu menu, bool isVisable, bool isDefault)
        {
            itemmenu.Insert(0, getMenu(menu, isVisable, isDefault));
        }

        /// <summary>
        /// 流程ID
        /// </summary>
        public virtual string FlowId
        {
            get { return flowid; }
            set { flowid = value; }
        }

        /// <summary>
        /// 环节ID
        /// </summary>
        public virtual string NodeId
        {
            get { return nodeid; }
            set { nodeid = value; }
        }

        /// <summary>
        /// 环节名称
        /// </summary>
        public virtual string NodeName
        {
            get { return EnumHelper.GetFieldDescription(typeof(WorkFlowNode), int.Parse(nodeid)); }
        }

        /// <summary>
        /// 环节枚举
        /// </summary>
        public virtual WorkFlowNode WorkFlowNode
        {
            get { return (WorkFlowNode)EnumHelper.IntValueToEnum(typeof(WorkFlowNode), int.Parse(nodeid)); }
        }

        /// <summary>
        /// 环节描述
        /// </summary>
        public virtual string NodeDesc
        {
            get { return nodedesc; }
            set { nodedesc = value; }
        }

        /// <summary>
        /// 环节权限
        /// </summary>
        public virtual string NodeLevel
        {
            get { return nodelevel; }
            set { nodelevel = value; }
        }

        /// <summary>
        /// 所属阶段
        /// </summary>
        public virtual string Stage
        {
            get { return stage; }
            set { stage = value; }
        }

        /// <summary>
        /// 上一环节
        /// </summary>
        public virtual string PerNode
        {
            get { return pernode; }
            set { pernode = value; }
        }

        /// <summary>
        /// 下一环节
        /// </summary>
        public virtual string NextNode
        {
            get { return nextnode; }
            set { nextnode = value; }
        }

        /// <summary>
        /// 环节部门权限
        /// </summary>
        public virtual string NodeDepartCode
        {
            get { return nodedepartcode; }
            set { nodedepartcode = value; }
        }

        /// <summary>
        /// 环节角色权限
        /// </summary>
        public virtual string NodeRoleId
        {
            get { return noderoleid; }
            set { noderoleid = value; }
        }

        /// <summary>
        /// 环节用户权限
        /// </summary>
        public virtual string NodeUserId
        {
            get { return nodeuserid; }
            set { nodeuserid = value; }
        }

        /// <summary>
        /// 环节文档权限
        /// </summary>
        public virtual string NodeFileCode
        {
            get { return nodefilecode; }
            set { nodefilecode = value; }
        }

        /// <summary>
        /// 环节类型
        /// </summary>
        public virtual NodeType NodeType
        {
            get { return nodetype; }
            set { nodetype = value; }
        }

        /// <summary>
        /// 超时时间
        /// </summary>
        public virtual string TimeOut
        {
            get { return timeout; }
            set { timeout = value; }
        }

        /// <summary>
        /// 通知类型
        /// </summary>
        public virtual string NotifyType
        {
            get { return notifyType; }
            set { notifyType = value; }
        }

        /// <summary>
        /// 通知模板：任务创建人
        /// </summary>
        public virtual string BeginText
        {
            get { return begintext; }
            set { begintext = value; }
        }

        /// <summary>
        /// 通知模板：下一处理人
        /// </summary>
        public virtual string NextText
        {
            get { return nexttext; }
            set { nexttext = value; }
        }

        /// <summary>
        /// 通知模板：历史处理人
        /// </summary>
        public virtual string HistoryText
        {
            get { return historytext; }
            set { historytext = value; }
        }

        public virtual string FunctionCode
        {
            get;
            set;
        }

        /// <summary>
        /// 环节对应的Tab页集合
        /// </summary>
        public virtual IList<Item_Menu> Item_Menu
        {
            get { return itemmenu; }
            set { itemmenu = value; }
        }

    }
}
