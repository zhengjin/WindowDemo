using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HHSoft.FieldProtect.Framework.Utility;

namespace HHSoftTreeView
{
    
    [ToolboxData("<{0}:HHSoftTreeView CssClass='TreeStyle' ImageSet='XPFileExplorer' ExpandDepth='0' OnClick='OnTreeNodeChecked()' runat=server><SelectedNodeStyle CssClass='SelectedNodeStyle' /> <NodeStyle NodeSpacing='3px' VerticalPadding='3px' /></{0}:HHSoftTreeView >")]
    
    public class HHSoftTreeView : TreeView
    {
        
        private IList<ITreeNode> nodeList;
        private string sCheckedValue = string.Empty;
        private string sRootImageUrl = "../../Image/WebResource.gif";
        private bool bTreeNodeUrl = true;
        private bool bTreeCheckBoxes = false;

        /// <summary>
        /// 获取形成树型结构的List
        /// </summary>
        public IList<ITreeNode> NodeList
        {
            get { return nodeList;}
        }
        
        /// <summary>
        /// 设置或获取选中结点的Value字符串
        /// </summary>        
        [Description("设置或获取选中结点的Value字符串")]
        public string CheckedValue
        {
            get
            {
                return GetNodeCheckedList("Value");
            }
            set
            {
                this.sCheckedValue = value;
            }
        }
        
        /// <summary>
        ///  获取选中结点的Name字符串
        /// </summary>
        public string CheckedName
        {
            get
            {
                return GetNodeCheckedList("Name");
            }
        }

        /// <summary>
        ///  获取选中结点的Value和Name字符串,用|分隔
        /// </summary>
        public string CheckValueAndName
        {
            get
            {
                return GetNodeCheckedList("ValueAndName");
            }
        }
        
        /// <summary>
        /// 根节点的菜单图片
        /// </summary>
        public string RootImageUrl
        {
            set
            {
                this.sRootImageUrl = value; 
            }
        }

        /// <summary>
        /// 节点路径是否起作用，ture 起作用，false 不起作用
        /// </summary>
        public bool IsTreeNodeUrl
        {
            set
            {
                this.bTreeNodeUrl = value;
            }
        }
        
        /// <summary>
        /// 是否显示Checkbox
        /// </summary>
        public bool IsTreeCheckBoxes
        {
            set
            {
                this.bTreeCheckBoxes = value;
            }
        }        

        /// <summary>
        /// 初始化树形的方法
        /// </summary>
        /// <param name="nodeList"></param>
        public void InitTreeView(IList<ITreeNode> nodeList)
        {
            if (nodeList == null)
                throw new ArgumentNullException("初始化树形控件数据源不能为 NUll");

            this.nodeList = nodeList;
            this.Nodes.Clear();
            
            if (bTreeCheckBoxes)
                this.ShowCheckBoxes = TreeNodeTypes.All;

            //按NodeCode升序排序
            var nodeCodeOrderQuery = from item in nodeList
                                                 orderby item.NodeCode ascending
                                                 select item;
            //取出排序后第一个部门编码,既根部门长度
            int RootLen = 0;
            if(nodeCodeOrderQuery.Count<ITreeNode>() >0)
            {
                RootLen = nodeCodeOrderQuery.First<ITreeNode>().NodeCode.Length;
            }

            var rootNodes =
                from item in nodeList
                where item.NodeCode.Length == RootLen
                orderby item.NodeOrderNo ascending
                select item;
   
            foreach (ITreeNode node in rootNodes)
            {
                TreeNode RootNode = new TreeNode();
                RootNode.Text = node.NodeName;
                RootNode.Value = node.NodeCode;
                //RootNode.ShowCheckBox = node.NodeCheckBox;
                RootNode.ImageUrl = sRootImageUrl;
                if (bTreeCheckBoxes&&!string.IsNullOrEmpty(sCheckedValue))
                {
                    string sCheckValue = "," + sCheckedValue + ",";
                    string sNodeValue = "," + node.NodeCode + ",";
                    RootNode.Checked = sCheckValue.IndexOf(sNodeValue) != -1;
                }
                if (bTreeNodeUrl)
                {
                    RootNode.NavigateUrl = "";
                    RootNode.Target = "_self";
                    if (!string.IsNullOrEmpty(node.NodeUrl))
                    {
                        RootNode.NavigateUrl = node.NodeUrl;
                        RootNode.Target = node.NodeUrlTarget;
                    }
                }
                
                //if (bTreeNodeUrl&&!string.IsNullOrEmpty(node.NodeUrl))
                //{
                //    RootNode.NavigateUrl = string.Format(CommonHelper.GetSiteRoot() + node.NodeUrl + "?FunId={0}", node.NodeCode);
                //    RootNode.Target = node.NodeUrlTarget;
                //}
                
                RootNode.SelectAction = TreeNodeSelectAction.SelectExpand;
                this.Nodes.Add(RootNode);
                InitTreeViewSubRoot(RootNode, nodeList);
            }
        }

        private void InitTreeViewSubRoot(TreeNode pNode, IList<ITreeNode> nodeList)
        {
            IEnumerable<ITreeNode> subNodes = null;
           
            subNodes =
                from item in nodeList
                where item.NodeCode.Length == pNode.Value.Length + 2 &&
                item.NodeCode.Substring(0, pNode.Value.Length) == pNode.Value
                orderby item.NodeOrderNo ascending
                select item;
          
            if (subNodes.Count<ITreeNode>() == 0)
                return;

            foreach (ITreeNode node in subNodes)
            {
                TreeNode SubNode = new TreeNode();
                SubNode.Text = node.NodeName;
                SubNode.Value = node.NodeCode;
                //SubNode.ShowCheckBox = node.NodeCheckBox;

                if (bTreeCheckBoxes && !string.IsNullOrEmpty(sCheckedValue))
                {
                    string sCheckValue = "," + sCheckedValue + ",";
                    string sNodeValue = "," + node.NodeCode + ",";
                    SubNode.Checked = sCheckValue.IndexOf(sNodeValue) != -1;
                }
                if (bTreeNodeUrl)
                {
                    SubNode.NavigateUrl = "";
                    SubNode.Target = "_self";
                    if (!string.IsNullOrEmpty(node.NodeUrl))
                    {
                        SubNode.NavigateUrl = node.NodeUrl;
                        SubNode.Target = node.NodeUrlTarget;
                    }
                }                  
                //if (bTreeNodeUrl && !string.IsNullOrEmpty(node.NodeUrl))
                //{
                //    SubNode.NavigateUrl = string.Format(CommonHelper.GetSiteRoot() + node.NodeUrl + "?FunId={0}", node.NodeCode);
                //    SubNode.Target = node.NodeUrlTarget;
                //}
                //else
                //{
                //    SubNode.NavigateUrl = "";
                //    SubNode.Target = "_self";
                //}

                SubNode.SelectAction = TreeNodeSelectAction.SelectExpand;
                pNode.ChildNodes.Add(SubNode);

                if (IsHasChildren(SubNode, nodeList))                            
                    InitTreeViewSubRoot(SubNode, nodeList);                   

            }
        }

        private bool IsHasChildren(TreeNode Node, IList<ITreeNode> nodeList)
        {
            IEnumerable<ITreeNode> ChildNodes =
                    from item in nodeList
                    where item.NodeCode.Length == Node.Value.Length + 2 &&
                    item.NodeCode.Substring(0, Node.Value.Length) == Node.Value
                    select item;

            if (ChildNodes.Count<ITreeNode>() != 0)
                return true;
            else
                return false;
        }
        
        private string GetNodeCheckedList(string sReturnType)
        {
            string sNodeCheckValueList = string.Empty;
            string sNodeCheckNameList = string.Empty;

            GetSelectedNode(null, ref sNodeCheckValueList, ref sNodeCheckNameList);

            if (sNodeCheckValueList != string.Empty)
                sNodeCheckValueList = sNodeCheckValueList.Substring(0, sNodeCheckValueList.Length - 1);
            if (sNodeCheckNameList != string.Empty)
                sNodeCheckNameList = sNodeCheckNameList.Substring(0, sNodeCheckNameList.Length - 1);

            if (sNodeCheckValueList == string.Empty && sNodeCheckNameList == string.Empty)
            {
                return string.Empty;
            }
            else
            {
                if (sReturnType.ToUpper() == "VALUE")
                {
                    return sNodeCheckValueList;
                }
                if (sReturnType.ToUpper() == "NAME")
                {
                    return sNodeCheckNameList;
                }
                if (sReturnType.ToUpper() == "VALUEANDNAME")
                {
                    return sNodeCheckValueList + "|" + sNodeCheckNameList;
                }                
            }
            return string.Empty;
        }

        private void GetSelectedNode(TreeNode pNode, ref string sNodeCheckValueList, ref string sNodeCheckNameList)
        {                      
            if (pNode == null)
            {
                foreach (TreeNode node in this.Nodes)
                {
                    if (node.Checked == true)
                    {
                        sNodeCheckValueList += node.Value + ",";
                        sNodeCheckNameList += node.Text + ",";               
                    }
                    GetSelectedNode(node, ref sNodeCheckValueList, ref sNodeCheckNameList);
                }
            }
            else
            {
                foreach (TreeNode node in pNode.ChildNodes)
                {
                    if (node.Checked == true)
                    {
                        sNodeCheckValueList += node.Value + ",";
                        sNodeCheckNameList += node.Text + ",";
                    }
                    GetSelectedNode(node, ref sNodeCheckValueList, ref sNodeCheckNameList);
                }
            }          
        }

        /// <summary>
        /// 清空节点
        /// </summary>
        public void ClearNodes()
        {
            if (this.Nodes != null)
            {
                this.Nodes.Clear();
            }
        }
    }
}
