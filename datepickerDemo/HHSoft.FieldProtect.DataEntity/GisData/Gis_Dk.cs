using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GBArcGis.Base;
using HHSoft.Gt.Geometry.Base;
using HHSoft.FieldProtect.DataEntity.CustomAttribute;

namespace HHSoft.FieldProtect.DataEntity.GisData
{
    /// <summary>
    /// Gis地块实体类
    /// </summary>
    [Serializable]
    public class Gis_Dk : IGeoPolygon<Gis_Jzqy>, IGBDK<Gis_Dltb, Gis_Xzdw>
    {
        public Gis_Dk()
        {
            this.RingList = new List<Gis_Jzqy>();
            this.DLTBList = new List<Gis_Dltb>();
            this.XZDWList = new List<Gis_Xzdw>();
        }
        /// <summary>
        /// DkId
        /// </summary>        
        public string DkId { get; set; }

        /// <summary>
        /// GisId
        /// </summary>
        [Pk]
        public string GisId { get; set; }

        /// <summary>
        /// 权属性质代码
        /// </summary>
        public string Qsxzdm { get; set; }

        /// <summary>
        /// 地类编码
        /// </summary>
        public string Dlbm { get; set; }

        /// <summary>
        /// 地类名称
        /// </summary>
        public string Dlmc { get; set; }

        #region IGeoPolygon<JZQY> 成员

        /// <summary>
        /// 界址点环的集合
        /// </summary>
        [IsDbColumn(false)]
        public List<Gis_Jzqy> RingList { get; set; }

        #endregion

        #region IGeomety 成员
        /// <summary>
        /// 标识
        /// </summary>
        [IsDbColumn(false)]
        public int GeoID { get; set; }

        #endregion

        #region IGBDK<DLTB,XZDW> 成员

        /// <summary>
        /// 地块编号
        /// </summary>
        public string Dkbh { get; set; }

        /// <summary>
        /// 地块名称
        /// </summary>
        public string Dkmc { get; set; }

        /// <summary>
        /// 图形类型
        /// </summary>
        public string Txlx { get; set; }

        /// <summary>
        /// 界址点数
        /// </summary>
        public string Jzds { get; set; }

        /// <summary>
        /// 套环级别
        /// </summary>
        public string Thjb { get; set; }

        /// <summary>
        /// 实测面积
        /// </summary>
        public double Scmj { get; set; }

        /// <summary>
        /// 计算面积
        /// </summary>
        public double Jsmj { get; set; }

        /// <summary>
        /// 实测图幅号
        /// </summary>
        public string ScTfh { get; set; }

        /// <summary>
        /// 计算图幅号
        /// </summary>
        public string JsTfh { get; set; }
        
        /// <summary>
        /// 地块用途
        /// </summary>
        public string Dkyt { get; set; }

        /// <summary>
        /// ????
        /// </summary>
        [IsDbColumn(false)]
        public string Dktf { get; set; }

        /// <summary>
        /// 地类图斑集合
        /// </summary>
        [IsDbColumn(false)]
        public List<Gis_Dltb> DLTBList { get; set; }
        
        /// <summary>
        /// 线状地物集合
        /// </summary>
        [IsDbColumn(false)]
        public List<Gis_Xzdw> XZDWList { get; set; }

        #endregion

        
    }
}
