using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HHSoft.FieldProtect.DataEntity.WorkFlow
{
    public class Wfinstance:IComparable
    {
        public Wfinstance() { }

        public virtual string ID { get; set; }
        public virtual string FlowId { get; set; }
        public virtual string ItemCode { get; set; }
        public virtual int Orderno { get; set; }
        public virtual string NodeId { get; set; }
        public virtual string PerNode { get; set; }
        public virtual string NextNode { get; set; }
        public virtual string UserId { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Result { get; set; }
        public virtual string ResultDesc { get; set; }
        public virtual string BeginDate { get; set; }
        public virtual string EndDate { get; set; }
        public virtual string State { get; set; }

        #region IComparable 成员

        public int CompareTo(object obj)
        {
            return this.Orderno.CompareTo(((Wfinstance)obj).Orderno);
        }

        #endregion
    }
}
