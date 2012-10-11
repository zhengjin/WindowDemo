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
    /// GIS界址点实体类
    /// </summary>
    [Serializable]
    public class Gis_Jzd : IGeoPoint, IGeomety, IJZD
    {
        /// <summary>
        /// GisId
        /// </summary>
        [Pk]
        public string GisId { get; set; }
        /// <summary>
        /// DkId
        /// </summary>
        public string DkId { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int XH { get; set; }       

        /// <summary>
        /// 界址点区域编号
        /// </summary>
        public int Dkqh { get; set; }

        #region IGeomety 成员
        /// <summary>
        /// GeoID
        /// </summary>
        [IsDbColumn(false)]
        public int GeoID { get; set; }

        #endregion

        #region IGeoPoint 成员
        /// <summary>
        /// X坐标
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// Y坐标
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// Z坐标
        /// </summary>
        [IsDbColumn(false)]
        public double Z { get; set; }

        #endregion

        #region IJZD 成员
        /// <summary>
        /// 界址点编号
        /// </summary>
        public string Jzdbh { get; set; }

        #endregion
    }
}
