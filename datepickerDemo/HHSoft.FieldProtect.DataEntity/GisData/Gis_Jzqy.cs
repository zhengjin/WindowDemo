using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.Gt.Geometry.Base;
using HHSoft.FieldProtect.DataEntity.CustomAttribute;

namespace HHSoft.FieldProtect.DataEntity.GisData
{
    /// <summary>
    /// GIS界址点区域
    /// </summary>
    [Serializable]
    public class Gis_Jzqy : IGeoRing<Gis_Jzd>, IGeoPolyline<Gis_Jzd>, IGeomety
    {
        public Gis_Jzqy()
        {
            this.PointList = new List<Gis_Jzd>();
        }
        #region IGeomety 成员
        /// <summary>
        /// 界址点区域编号
        /// </summary>
        public int GeoID { get; set; }
        #endregion

        #region IGeoPolyline<JZD> 成员
        /// <summary>
        /// 界址点集合
        /// </summary>
        [IsDbColumn(false)]
        public List<Gis_Jzd> PointList { get; set; }
        #endregion
    }
}
