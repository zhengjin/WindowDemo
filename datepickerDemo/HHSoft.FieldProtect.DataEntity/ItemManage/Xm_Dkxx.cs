using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace HHSoft.FieldProtect.DataEntity.ItemManage
{
    /// <summary>
    /// 项目地块信息
    /// </summary>
    [Serializable]
    public class Xm_Dkxx
    {
        /// <summary>
        /// 实例化
        /// </summary>
        public Xm_Dkxx()
        {
            JzdList = new List<Xm_Jzd>();
        }

        #region 公用信息
        /// <summary>
        /// 坐标系
        /// </summary>
        public string Zbx { get; set; }
        /// <summary>
        /// 几度分带
        /// </summary>
        public string Jdfd { get; set; }
        /// <summary>
        /// 投影类型
        /// </summary>
        public string Tylx { get; set; }
        /// <summary>
        /// 计量单位
        /// </summary>
        public string Jldw { get; set; }
        /// <summary>
        /// 带号
        /// </summary>
        public string Dh { get; set; }
        /// <summary>
        /// 精度
        /// </summary>
        public string Jd { get; set; }
        /// <summary>
        /// 转换参数
        /// </summary>
        public string Zhcs { get; set; }

        #endregion

        /// <summary>
        /// 项目编码
        /// </summary>
        public string Dkid { get; set; }

        /// <summary>
        /// 项目编码
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// 项目阶段
        /// </summary>
        public ItemStage Stage { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public string Xh { get; set; }

        /// <summary>
        /// 地块编号
        /// </summary>
        public string Dkbh { get; set; }

        /// <summary>
        /// 地块名称
        /// </summary>
        public string Dkmc { get; set; }

        /// <summary>
        /// 经度范围（最小）
        /// </summary>
        public string X_Min { get; set; }

        /// <summary>
        /// 经度范围（最大）
        /// </summary>
        public string X_Max { get; set; }

        /// <summary>
        /// 纬度范围（最小）
        /// </summary>
        public string Y_Min { get; set; }

        /// <summary>
        /// 纬度范围（最大）
        /// </summary>
        public string Y_Max { get; set; }

        /// <summary>
        /// 图幅号
        /// </summary>
        public string Tfh { get; set; }

        /// <summary>
        /// 实测地块面积
        /// </summary>
        public string Scdkmj { get; set; }

        /// <summary>
        /// 计算地块面积
        /// </summary>
        public string Jsdkmj { get; set; }

        /// <summary>
        /// 图形类型
        /// </summary>
        public string Txlx { get; set; }

        /// <summary>
        /// 权属性质
        /// </summary>
        public string Qsxz { get; set; }

        /// <summary>
        /// 界址点数
        /// </summary>
        public string Jzds { get; set; }

        /// <summary>
        /// 套环级别
        /// </summary>
        public string Thjb { get; set; }

        /// <summary>
        /// 要素ID
        /// </summary>
        public string Ysid { get; set; }

        /// <summary>
        /// 地类编码
        /// </summary>
        public string Dlbm { get; set; }

        /// <summary>
        /// 地类名称
        /// </summary>
        public string Dlmc { get; set; }

        /// <summary>
        /// 错误信息集合
        /// </summary>
        public List<string> ErrorList { get; set; }
        
        /// <summary>
        /// 提示信息集合
        /// </summary>
        public List<string> WarnList { get; set; }        

        /// <summary>
        /// 界址点信息
        /// </summary>
        public List<Xm_Jzd> JzdList { get; set; }
    }

}
