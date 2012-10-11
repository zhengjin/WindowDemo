using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HHSoftTreeView
{
    /// <summary>
    /// HHTreeView控件使用的树形节点接口
    /// </summary>
    public interface ITreeNode 
    {
        /// <summary>
        /// 节点编码(用于拓扑关系)
        /// </summary>
        string NodeCode { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        string NodeName { get; set; }

        /// <summary>
        /// 节点链接
        /// </summary>
        string NodeUrl { get; set;}

        /// <summary>
        /// 节点链接Target
        /// </summary>
        string NodeUrlTarget { get; set;}

        /// <summary>
        /// 节点排序
        /// </summary>
        int NodeOrderNo { get; set; }

        /// <summary>
        /// 是否有选择框
        /// </summary>
        bool NodeCheckBox { get; set; }
    }

}
