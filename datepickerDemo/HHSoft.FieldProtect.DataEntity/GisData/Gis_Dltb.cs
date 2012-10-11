using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.Gt.Geometry.DataRule;
using HHSoft.FieldProtect.DataEntity.CustomAttribute;

namespace HHSoft.FieldProtect.DataEntity.GisData
{
    /// <summary>
    /// 地类图斑类
    /// </summary>
    public class Gis_Dltb : IDLTB
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
        /// 要素代码
        /// </summary>
        public string ysdm { get; set; }

        /// <summary>
        /// 图斑预编号
        /// </summary>
        public string tbybh { get; set; }

        /// <summary>
        /// 图斑编号
        /// </summary>
        public string tbbh { get; set; }
         
        /// <summary>
        /// 地类编码
        /// </summary>
        public string dlbm { get; set; } 
        
        /// <summary>
        /// 地类名称
        /// </summary>
        public string dlmc { get; set; }

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
        /// 权属单位代码
        /// </summary>
        public string qsdwdm { get; set; } 
        
        /// <summary>
        /// 权属单位名称
        /// </summary>
        public string qsdwmc { get; set; }
       
        /// <summary>
        /// 坐落单位代码
        /// </summary>
        public string zldwdm { get; set; }
     
        /// <summary>
        /// 坐落单位名称
        /// </summary>
        public string zldwmc { get; set; }
        
        /// <summary>
        /// 耕地类型
        /// </summary>
        public string gdlx { get; set; }
          
        /// <summary>
        /// 扣除类型
        /// </summary>
        public string kclx { get; set; }

        /// <summary>
        /// 地类备注
        /// </summary>
        public string dlbz { get; set; }

        /// <summary>
        /// 耕地坡度级
        /// </summary>
        public string pdjb { get; set; }

        /// <summary>
        /// 扣除地类编码
        /// </summary>
        public string kcdlbm { get; set; }

        /// <summary>
        /// 扣除地类系数
        /// </summary>
        public decimal tkxs { get; set; }

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
        /// 分类代码
        /// </summary>
        public string fldm { get; set; }

        /// <summary>
        /// 建设情况
        /// </summary>
        public string gkjsqk { get; set; }
        
        /// <summary>
        /// 更新日期
        /// </summary>
        public string gxrq { get; set; }
       
        /// <summary>
        /// 海岛
        /// </summary>
        public string hd { get; set; }
        
        /// <summary>
        /// 建立日期
        /// </summary>
        public string jlrq { get; set; }

        /// <summary>
        /// 零星地物面积
        /// </summary>
        public decimal lxdwmj { get; set; }
        
        /// <summary>
        /// 图斑面积
        /// </summary>
        public decimal pcmj { get; set; }

        /// <summary>
        /// 批准文号
        /// </summary>
        public string pzwh { get; set; }

        /// <summary>
        /// 所属权地籍号
        /// </summary>
        public string qszdh { get; set; }       


        /// <summary>
        /// 实际用途
        /// </summary>
        public string sjyt { get; set; }

        /// <summary>
        /// 所属区域
        /// </summary>
        public string ssqy { get; set; }
        
        /// <summary>
        /// 所在图幅号
        /// </summary>
        public string sztfh { get; set; }
   
        /// <summary>
        /// 图斑地类面积
        /// </summary>
        public decimal tbdlmj { get; set; }
       
        /// <summary>
        /// 椭球面积
        /// </summary>
        public decimal tbmj { get; set; }

        /// <summary>
        /// 扣除地类面积
        /// </summary>
        public decimal tkmj { get; set; }

        /// <summary>
        /// 线状地物面积
        /// </summary>
        public decimal xzdwmj { get; set; }
        
        /// <summary>
        /// 行政区代码
        /// </summary>
        public string xzqdm { get; set; }

        /// <summary>
        /// 行政区名称
        /// </summary>
        public string xzqmc { get; set; }
        
        /// <summary>
        /// 原地类编码
        /// </summary>
        public string ydlbm { get; set; }

        /// <summary>
        /// 专项地类
        /// </summary>
        public string zxdl { get; set; }

        /// <summary>
        /// SOAP序列化字符串ArcGIS几何对象
        /// </summary>
        [IsDbColumn(false)]
        public string shape { get; set; }

        /// <summary>
        /// Shape对象面积
        /// </summary>
        [IsDbColumn(false)]
        public double ShapeArea { get; set; }

        /// <summary>
        /// shape对象长度
        /// </summary>
        [IsDbColumn(false)]
        public double ShapeLin { get; set; }
    }
}
