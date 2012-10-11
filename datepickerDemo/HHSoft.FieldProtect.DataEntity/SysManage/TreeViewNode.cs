using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HHSoft.FieldProtect.DataEntity.SysManage
{
    public class TreeViewNode
    {
        public string id { get; set; }
        public string text { get; set; }
        public string value { get; set; }
        public bool showcheck { get; set; }
        public bool isexpand { get; set; }
        public int checkstate { get; set; }
        public bool hasChildren { get; set; }
        public bool complete { get; set; }
        public string ServerXzdm { get; set; }
        public List<TreeViewNode> ChildNodes { get; set; }

        public TreeViewNode()
        {
            ChildNodes = new List<TreeViewNode>();
            hasChildren = false;
            complete = true;
        }
    }
}
