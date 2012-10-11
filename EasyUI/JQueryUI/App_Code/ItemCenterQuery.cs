
using System.Collections.Generic;
namespace JQueryUI.App_Code
{
    public class ItemCenterQuery
    {
        public ItemCenterQuery()
        {
            children = new List<ItemCenterQuery>();
        }

        /// <summary>
        /// 组织机构编码
        /// </summary>
        public string CCode { get; set; }

        /// <summary>
        /// 组织机构名称
        /// </summary>
        public string CName { get; set; }

        /// <summary>
        /// 项目类别_土地整治(公顷)
        /// </summary>
        public decimal? GmZz { get; set; }

        /// <summary>
        /// 项目类别_土地整治(项目编号)
        /// </summary>
        public string GmZz_Itemcodes { get; set; }
        
        /// <summary>
        ///  项目类别_标准农田(公顷)
        /// </summary>
        public decimal? GmNT { get; set; }

        /// <summary>
        /// 项目类别_标准农田(项目编号)
        /// </summary>
        public string GmNT_Itemcodes { get; set; }

        /// <summary>
        /// 项目类别_拆旧复垦(公顷)
        /// </summary>
        public decimal? GmCJ { get; set; }

        /// <summary>
        /// 立项个数(项目编号)
        /// </summary>
        public string GmCJ_itemcodes { get; set; }

        /// <summary>
        /// 子数据
        /// </summary>
        public List<ItemCenterQuery> children { get; set; }

        public string state { set; get; }
    }
}