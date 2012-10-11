using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HHSoft.FieldProtect.DataEntity.ItemManage
{
    [Serializable]
    public class Xm_Query
    {
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string CCode { get; set; }
        public string Stage { get; set; }
        public string ItemState { get; set; }
        public string WfState { get; set; }

        private int stageType = 0;
        /// <summary>
        /// 阶段属性(0:全部　1:审批)
        /// </summary>
        public int StageType
        {
            get { return stageType; }
            set { stageType = value; }
        }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortExpression { get; set; }

        /// <summary>
        /// 排序方向
        /// </summary>
        public string SortDirection { get; set; }

        /// <summary>
        /// 查询项
        /// </summary>
        public string QueryItem { get; set; }

        private int pageIndex = 1;
        /// <summary>
        /// 当前页面
        /// </summary>
        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }
        private int pageSize = 10;
        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        private int keyQIsPager = 1;
        /// <summary>
        /// 是否分页
        /// </summary>
        public int IsPager
        {
            get { return keyQIsPager; }
            set { keyQIsPager = value; }
        }
    }


}
