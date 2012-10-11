using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.Gt.Geometry.DataRule;
using HHSoft.FieldProtect.DataEntity.CustomAttribute;

namespace HHSoft.FieldProtect.DataEntity.GisData
{
    /// <summary>
    /// 线状地物类
    /// </summary>
    public class Gis_Xzdw:IXZDW
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
        /// 变更编号
        /// </summary>
        public string bgbh { get; set; }
        
        /// <summary>
        /// 变更记录号
        /// </summary>
        public string bgjlh { get; set; }
    
        /// <summary>
        /// 变更日期
        /// </summary>
        public DateTime bgrq { get; set; }
 
        /// <summary>
        /// 标识码
        /// </summary>
        [IsDbColumn(false)]
        public decimal bsm { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }
        
        /// <summary>
        /// 长度
        /// </summary>
        public decimal cd { get; set; }
       
        /// <summary>
        /// 地类编码
        /// </summary>
        public string dlbm { get; set; }
     
        /// <summary>
        /// 地类名称
        /// </summary>
        public string dlmc { get; set; }
      
        /// <summary>
        /// 分类代码
        /// </summary>
        public string fldm { get; set; }
        
        /// <summary>
        /// 更新日期
        /// </summary>
        public string gxrq { get; set; }
        
        /// <summary>
        /// 建立日期
        /// </summary>
        public string jlrq { get; set; }
        
        /// <summary>
        /// 扣除比例
        /// </summary>
        public decimal kcbl { get; set; }
     
        /// <summary>
        /// 扣除图斑编号1
        /// </summary>
        public string kctbbh1 { get; set; }

        /// <summary>
        /// 扣除图斑编号2
        /// </summary>
        public string kctbbh2 { get; set; }
       
        /// <summary>
        /// 扣除图斑单位代码1
        /// </summary>
        public string kctbdwdm1 { get; set; }

        /// <summary>
        /// 扣除图斑单位代码2
        /// </summary>
        public string kctbdwdm2 { get; set; }
       
        /// <summary>
        /// 宽度
        /// </summary>
        public decimal kd { get; set; }
        
        /// <summary>
        /// 权属单位代码1
        /// </summary>
        public string qsdwdm1 { get; set; }

        /// <summary>
        /// 权属单位代码2
        /// </summary>
        public string qsdwdm2 { get; set; }

        /// <summary>
        /// 权属单位名称1
        /// </summary>
        public string qsdwmc1 { get; set; }
        
        /// <summary>
        /// 权属单位名称2
        /// </summary>
        public string qsdwmc2 { get; set; }

        /// <summary>
        /// 权属性质
        /// </summary>
        public string qsxz { get; set; }


        /// <summary>
        /// 权属性质(新)
        /// </summary>
        [IsDbColumn(false)]
        public string newqsxz
        {
            get
            {
                if (int.Parse(qsxz) <= 20)
                {
                    return ((int)QSXZ.GY).ToString();
                }
                else
                {
                    return ((int)QSXZ.JT).ToString();
                }
            }
        }
      
        /// <summary>
        /// 所属区域
        /// </summary>
        public string ssqy { get; set; }
        
        /// <summary>
        /// 线状地物编号
        /// </summary>
        public string xzdwbh { get; set; }
       
        /// <summary>
        /// 线状地物名称
        /// </summary>
        public string xzdwmc { get; set; }

        /// <summary>
        /// 线状地物面积
        /// </summary>
        public decimal xzdwmj { get; set; }
     
        /// <summary>
        /// 线状地物预编号
        /// </summary>
        public string xzdwybh { get; set; }
      
        /// <summary>
        /// 行政区代码
        /// </summary>
        public string xzqdm { get; set; }

        /// <summary>
        /// 扣除行政区代码1
        /// </summary>
        public string xzqdm1 { get; set; }
        
        /// <summary>
        /// 扣除行政区代码2
        /// </summary>
        public string xzqdm2 { get; set; }
        
        /// <summary>
        /// 原地类编码
        /// </summary>
        public string ydlbm { get; set; }

        /// <summary>
        /// 要素代码
        /// </summary>
        public string ysdm { get; set; }

        /// <summary>
        /// 左扣系数
        /// </summary>
        public decimal zkxs { get; set; }

        /// <summary>
        /// 坐落单位代码1
        /// </summary>
        public string zldwdm1 { get; set; }

        /// <summary>
        /// 坐落单位代码2
        /// </summary>
        public string zldwdm2 { get; set; }

        /// <summary>
        /// SOAP序列化字符串ArcGIS几何对象
        /// </summary>
        [IsDbColumn(false)]
        public string shape { get; set; }

        /// <summary>
        /// shape对象长度
        /// </summary>
        [IsDbColumn(false)]
        public double ShapeLin { get; set; }
    }
}
