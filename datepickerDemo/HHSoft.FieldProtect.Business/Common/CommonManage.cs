using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity;
using System.Configuration;
using HHSoft.FieldProtect.Framework.Utility;
using HHSoft.FieldProtect.DataEntity.ItemManage;
using System.Web;
using System.Web.UI.WebControls;

namespace HHSoft.FieldProtect.Business.Common
{
    public class CommonManage
    {
        /// <summary>
        /// 获取项目文件的上传目录
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public static string UpLoadPath(string itemCode)
        {
            return System.Web.HttpContext.Current.Server.MapPath(string.Format(@"{0}/{1}/{2}",
                EnumHelper.GetFieldDescription(typeof(FileFolder), (int)FileFolder.Item),
                ConfigurationSettings.AppSettings["CompanyCode"],
                itemCode));
        }

        public static string GetFtpUploadPath(string companyCode)
        {
            return "/" + FileFolder.Item + "/" + companyCode + "/";
        }

        public static string GetLocalMessagePath(string xxbh)
        {
            return EnumHelper.GetFieldDescription(typeof(FileFolder), (int)FileFolder.Message) + "/" + xxbh + "/";
        }

        public static string GetMessageFileUrl(string xxbh, string fileName)
        {
            return "<a href='http://" + HttpContext.Current.Request.Url.Authority + "/" + GetLocalMessagePath(xxbh).TrimStart('~', '/') + fileName + "' target='_blank'>" + fileName + "</a><br>";
        }

        public static string GetFtpMessagePath()
        {
            return "/" + FileFolder.Message + "/";
        }

        public static string GetFtpMessagePath(string xxbh)
        {
            return "/" + FileFolder.Message + "/" + xxbh + "/";
        }

        public static string GetFtpGisQueryPath()
        {
            return "/" + FileFolder.Gis + "/Query/";
        }

        public static string WfInfo
        {
            get
            {
                return "操作成功，下一环节【{0}】 \n处理人信息 \n部门：{1} \n角色：{2} \n人员：{3}";
            }
        }


        public static bool GisEnable
        {
            get
            {
                if (ConfigurationSettings.AppSettings["GisParameter"] != null &&
                    ConfigurationSettings.AppSettings["GisParameter"].Equals("1"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 系统风格
        /// </summary>
        public static SystemStyle SystemStyle
        {
            get
            {
                return (SystemStyle)EnumHelper.StringValueToEnum(typeof(SystemStyle),
                    ConfigurationSettings.AppSettings["SystemStyle"].ToString());
            }
        }

        /// <summary>
        /// Gis　Web服务地址
        /// </summary>
        public static string GisWsUrl
        {
           get
           {
               return ConfigurationSettings.AppSettings["GisWsUrl"];
           }           
        }

        /// <summary>
        /// Gis Web服务方法
        /// </summary>
        public static string[] GisWdMethod
        {
            get
            {
                return ConfigurationSettings.AppSettings["GisWsMethod"].Split('|');
            }
        }


        /// <summary>
        /// 返回Cookies名称
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public static string getSessionName
        {
            get
            {
                return "gisData";
            }
        }

        /// <summary>
        /// 删除条件
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public static Dictionary<string, string> delWhere(string itemCode)
        {
            Dictionary<string, string> deleteWhere = new Dictionary<string, string>();
            deleteWhere.Add("itemcode", itemCode);
            return deleteWhere;
        }

        public static ItemStage nodeToStage(WorkFlowNode node)
        {
            switch (node)
            {
                case WorkFlowNode.TB:                    
                case WorkFlowNode.SX:
                    return ItemStage.ShenBo;
                
                case WorkFlowNode.KY:
                case WorkFlowNode.KYSH:
                    return ItemStage.KeYan;
                
                case WorkFlowNode.GHSJYS:
                case WorkFlowNode.GHSJSH:
                    return ItemStage.GuiHua;
                
                case WorkFlowNode.YSSH:
                case WorkFlowNode.YSTZ:
                    return ItemStage.YuSuan;
                
                case WorkFlowNode.ShiShi:
                    return ItemStage.ShiShi;
                
                case WorkFlowNode.JunGong:
                    return ItemStage.JunGong;
                
                case WorkFlowNode.ChuYan:
                case WorkFlowNode.ZhongYan:
                    return ItemStage.YanShou;

                case WorkFlowNode.JueSuan:
                    return ItemStage.JueSuan;
                
                case WorkFlowNode.GuiDang:
                    return ItemStage.GuiDang;

                default:
                    return ItemStage.ShenBo;
            }
        }

        public static void FormatWh(string strValue, TextBox txt1,TextBox txt2,TextBox txt3)
        {
            string[] ary = strValue.Split('|');
            if (ary.Length == 3)
            {
                txt1.Text = ary[0];
                txt2.Text = ary[1];
                txt3.Text = ary[2];
            }
        }
    }
}
