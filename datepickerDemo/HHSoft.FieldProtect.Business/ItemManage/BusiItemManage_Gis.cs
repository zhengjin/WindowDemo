using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using HHSoft.FieldProtect.DataEntity.GisData;
using HHSoft.FieldProtect.Business.Common;
using HHSoft.FieldProtect.DataAccess;
using HHSoft.FieldProtect.DataEntity;
using System.Data;
using GBArcGis.Base;
using System.Reflection;

namespace HHSoft.FieldProtect.Business.ItemManage
{
    public class BusiItemManage_Gis
    {

        /// <summary>
        /// 获取最新的阶段编号和序号
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public string[] getLastGisPara (string itemCode)
        {
            string[] returnAry = new string[2];
            returnAry[0] = ((int)ItemStage.ShenBo).ToString();
            returnAry[1] = "1";
            string strSql = "select * from v_xm_gisdata where itemcode = '{0}'";
            strSql = string.Format(strSql, itemCode.Trim());
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count > 0)
            {
                returnAry[0] = dt.Rows[0]["stage"].ToString();
                returnAry[1] = dt.Rows[0]["xh"].ToString();
            }
            return returnAry;
        } 
        
        public DataTable QueryData(string itemCode, int stage, int Xh)
        {
            StringBuilder sbSql = new StringBuilder();
            sbSql.AppendLine("select * from gis_data ");
            sbSql.AppendLine("where itemcode = '{0}' and stage = '{1}' and xh = '{2}'");
            string strSql = string.Format(sbSql.ToString(), itemCode, stage.ToString(), Xh.ToString());
            return OracleHelper.ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 获取指定阶段的数据
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="stage"></param>
        /// <param name="Xh"></param>
        /// <returns></returns>
        public Gis_Data<Gis_Dk> GetGisInfo(string itemCode, int stage, int Xh)
        {
            string strSql = "select * from gis_data where ItemCode = '{0}' and stage = '{1}' and xh = '{2}'";
            strSql = string.Format(strSql, itemCode, stage.ToString(), Xh.ToString());
            return this.getGisdata(strSql, OracleHelper.ConnectionString, true);
        }
        /// <summary>
        /// 获取最新的数据
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public Gis_Data<Gis_Dk> GetNewGisInfo(string itemCode, bool allData)
        {
            return GetNewGisInfo(itemCode, OracleHelper.ConnectionString, true);
        }

        /// <summary>
        /// 获取最新的数据
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="conStr"></param>
        /// <returns></returns>
        public Gis_Data<Gis_Dk> GetNewGisInfo(string itemCode, string conStr, bool allData)
        {
            string strSql = "select * from v_xm_gisdata where ItemCode = " + itemCode;
            return this.getGisdata(strSql, conStr, allData);
        }

        private Gis_Data<Gis_Dk> getGisdata(string strSql, string conStr, bool allData)
        {
            Gis_Data<Gis_Dk> dataList = null;
            OracleDbOperation oracleDb = new OracleDbOperation(conStr);
            DataTable dt = oracleDb.ExecuteDataTable(strSql);
            if (dt.Rows.Count == 1)
            {
                dataList = (Gis_Data<Gis_Dk>)new DataTableOperation().ConvertFromDataRowToEntity(dt.Rows[0], typeof(Gis_Data<Gis_Dk>));

                strSql = "select * from gis_dk where gisId = " + dataList.GisId;
                            
                dt = oracleDb.ExecuteDataTable(strSql);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Gis_Dk dk = (Gis_Dk)new DataTableOperation().ConvertFromDataRowToEntity(dt.Rows[i], typeof(Gis_Dk));
                    dk.GeoID = i;

                    ////界址点
                    strSql = "select * from gis_jzd where gisid = '{0}' and dkid = '{1}' order by dkqh,xh ";
                    strSql = string.Format(strSql, dataList.GisId, dk.DkId);
                    DataTable dtJzd = oracleDb.ExecuteDataTable(strSql);

                    string dkqh = string.Empty;
                    Gis_Jzqy jzqy = null;
                    for (int j = 0; j < dtJzd.Rows.Count; j++)
                    {
                        if ((dtJzd.Rows[j]["dkqh"].ToString() != dkqh && jzqy != null) || j == dtJzd.Rows.Count - 1)
                        {
                            dk.RingList.Add(jzqy);
                        }
                        if (dtJzd.Rows[j]["dkqh"].ToString() != dkqh)
                        {
                            jzqy = new Gis_Jzqy();
                            jzqy.GeoID = int.Parse(dtJzd.Rows[j]["dkqh"].ToString());
                        }
                        Gis_Jzd jzd = (Gis_Jzd)new DataTableOperation().ConvertFromDataRowToEntity(dtJzd.Rows[j], typeof(Gis_Jzd));
                        jzd.GeoID = jzd.XH;
                        jzqy.PointList.Add(jzd);

                        dkqh = dtJzd.Rows[j]["dkqh"].ToString();
                    }
                    if (allData)
                    {
                        ////地类图斑
                        strSql = "select * from gis_dltb where gisId = {0} and dkId = '{1}'";
                        strSql = string.Format(strSql, dataList.GisId, dk.DkId);
                        DataTable dtDltb = oracleDb.ExecuteDataTable(strSql);
                        for (int j = 0; j < dtDltb.Rows.Count; j++)
                        {
                            Gis_Dltb dltb = (Gis_Dltb)new DataTableOperation().ConvertFromDataRowToEntity(dtDltb.Rows[j], typeof(Gis_Dltb));
                            dk.DLTBList.Add(dltb);
                        }

                        ////线状地物
                        strSql = "select * from gis_xzdw where gisId = {0} and dkId = '{1}'";
                        strSql = string.Format(strSql, dataList.GisId, dk.DkId);
                        DataTable dtXzdw = oracleDb.ExecuteDataTable(strSql);
                        for (int j = 0; j < dtXzdw.Rows.Count; j++)
                        {
                            Gis_Xzdw xzdw = (Gis_Xzdw)new DataTableOperation().ConvertFromDataRowToEntity(dtXzdw.Rows[j], typeof(Gis_Xzdw));
                            dk.XZDWList.Add(xzdw);
                        }
                    }
                    dataList.DkList.Add(dk);
                }
            }
            return dataList;

        }
        /// <summary>
        /// 保存地块信息
        /// </summary>
        /// <param name="dataList"></param>
        /// <returns></returns>
        public bool SaveGisData(Gis_Data<Gis_Dk> dataList)
        {
            if (dataList == null) return true;

            ArrayList strSql = new ArrayList();
            string tmpSql = string.Empty;
            string gisId = dataList.GisId;
            string dkId = string.Empty;

            Dictionary<string, string> delGisId = new Dictionary<string, string>();
            delGisId.Add("GisId", gisId);

            strSql.Add(SqlBuilder.BuildDeleteSql<Gis_Data<Gis_Dk>>(delGisId));
            strSql.Add(SqlBuilder.BuildDeleteSql<Gis_Dk>(delGisId));
            strSql.Add(SqlBuilder.BuildDeleteSql<Gis_Jzd>(delGisId));
            strSql.Add(SqlBuilder.BuildDeleteSql<Gis_Dltb>(delGisId));
            strSql.Add(SqlBuilder.BuildDeleteSql<Gis_Xzdw>(delGisId));

            //// Gis主表            
            strSql.Add(SqlBuilder.BuildInsertSql(dataList));
            //// Gis地块            
            foreach (Gis_Dk dk in dataList.DkList)
            {
                dkId = Guid.NewGuid().ToString();                
                dk.GisId = gisId;
                dk.DkId = dkId;
                strSql.Add(SqlBuilder.BuildInsertSql(dk));
                ////界址点
                foreach (Gis_Jzqy jzqy in dk.RingList)
                {
                    foreach (Gis_Jzd jzd in jzqy.PointList)
                    {
                        jzd.GisId = gisId;
                        jzd.DkId = dkId;
                        jzd.Dkqh = jzqy.GeoID;
                        jzd.XH = jzd.GeoID;
                        strSql.Add(SqlBuilder.BuildInsertSql(jzd));
                    }
                }
                ////地类图斑                
                foreach (Gis_Dltb tb in dk.DLTBList)
                {
                    tb.GisId = gisId;
                    tb.DkId = dkId;
                    strSql.Add(SqlBuilder.BuildInsertSql(tb));
                }
                ////线状地物
                foreach (Gis_Xzdw dw in dk.XZDWList)
                {
                    dw.GisId = gisId;
                    dw.DkId = dkId;
                    strSql.Add(SqlBuilder.BuildInsertSql(dw));
                }
            }
            return OracleHelper.ExecuteCommand(strSql);
        }



        //public string ConversionJson(Gis_Data<Gis_Dk> gisData)
        //{
        //    if (string.IsNullOrEmpty(JsonStr)) return string.Empty;
        //    Gis_Data<Gis_Dk> gisData = JsonConvert.DeserializeObject<Gis_Data<Gis_Dk>>(JsonStr);
        //    if (gisData.DkList.Count == 0) return string.Empty; ////手工录入方式
        //    IGBXM<GBDK> gisEntity = this.Conversion(gisData);
        //    return JsonConvert.SerializeObject(gisEntity);
        //}


        /// <summary>
        /// Mis对象转换成Gis对象
        /// </summary>
        /// <param name="SourceXM"></param>
        /// <returns></returns>
        public IGBXM<GBDK> Conversion(IGBXM<Gis_Dk> SourceXM)
        {
            IGBXM<GBDK> result = new GBXM<GBDK>();

            foreach (PropertyInfo pi in typeof(IGBXM<GBDK>).GetProperties())
            {
                switch (pi.Name.ToLower())
                {
                    case "dklist":
                        List<Gis_Dk> dklist = (List<Gis_Dk>)SourceXM.GetType().GetProperty(pi.Name.ToString()).GetValue(SourceXM, null);
                        List<GBDK> newdklist = new List<GBDK>();
                        foreach (Gis_Dk d in dklist)
                        {
                            newdklist.Add(ConversionDK(d));
                        }

                        pi.SetValue(result, newdklist, null);
                        break;
                    default:
                        pi.SetValue(result, SourceXM.GetType().GetProperty(pi.Name.ToString()).GetValue(SourceXM, null), null);
                        break;
                }
            }
            return result;
        }

        /// <summary>
        /// 客户端地块对象转换到服务地块
        /// </summary>
        /// <param name="SourceXM"></param>
        /// <returns></returns>
        private GBDK ConversionDK(Gis_Dk SourceXM)
        {
            GBDK result = new GBDK();
            foreach (PropertyInfo pi in typeof(GBDK).GetProperties())
            {
                switch (pi.Name.ToLower())
                {
                    case "":

                        break;
                    case "ringlist":
                        List<Gis_Jzqy> tmplist = (List<Gis_Jzqy>)SourceXM.GetType().GetProperty(pi.Name.ToString()).GetValue(SourceXM, null);
                        List<JZQY> newdklist = new List<JZQY>();
                        foreach (Gis_Jzqy d in tmplist)
                        {
                            newdklist.Add(ConversionJzqy(d));
                        }

                        pi.SetValue(result, newdklist, null);
                        break;
                    case "dltblist":
                        List<Gis_Dltb> tblist = (List<Gis_Dltb>)SourceXM.GetType().GetProperty(pi.Name.ToString()).GetValue(SourceXM, null);
                        List<DLTB> newtblist = new List<DLTB>();
                        foreach (Gis_Dltb d in tblist)
                        {
                            newtblist.Add(ConversionDltb(d));
                        }

                        pi.SetValue(result, newtblist, null);
                        break;
                    case "xzdwlist":
                        List<Gis_Xzdw> dwlist = (List<Gis_Xzdw>)SourceXM.GetType().GetProperty(pi.Name.ToString()).GetValue(SourceXM, null);
                        List<XZDW> newdwlist = new List<XZDW>();
                        foreach (Gis_Xzdw d in dwlist)
                        {
                            newdwlist.Add(ConversionXzdw(d));
                        }

                        pi.SetValue(result, newdwlist, null);
                        break;
                    default:
                        pi.SetValue(result, SourceXM.GetType().GetProperty(pi.Name.ToString()).GetValue(SourceXM, null), null);
                        break;
                }
            }
            return result;
        }

        /// <summary>
        /// 界址区域对象
        /// </summary>
        /// <param name="SourceXm"></param>
        /// <returns></returns>
        private JZQY ConversionJzqy(Gis_Jzqy SourceXm)
        {
            JZQY result = new JZQY();
            foreach (PropertyInfo pi in typeof(JZQY).GetProperties())
            {
                if (pi.Name.ToLower() == "pointlist")
                {
                    List<Gis_Jzd> tmplist = (List<Gis_Jzd>)SourceXm.GetType().GetProperty(pi.Name.ToString()).GetValue(SourceXm, null);
                    List<JZD> newdklist = new List<JZD>();
                    foreach (Gis_Jzd d in tmplist)
                    {
                        newdklist.Add(ConversionJzd(d));
                    }

                    pi.SetValue(result, newdklist, null);

                }
                else
                {
                    pi.SetValue(result, SourceXm.GetType().GetProperty(pi.Name.ToString()).GetValue(SourceXm, null), null);
                }
            }
            return result;
        }

        /// <summary>
        /// 界址点对象
        /// </summary>
        /// <param name="SourceXm"></param>
        /// <returns></returns>
        private JZD ConversionJzd(Gis_Jzd SourceXm)
        {
            JZD result = new JZD();
            foreach (PropertyInfo pi in typeof(JZD).GetProperties())
            {
                pi.SetValue(result, SourceXm.GetType().GetProperty(pi.Name.ToString()).GetValue(SourceXm, null), null);
            }
            return result;
        }

        /// <summary>
        /// 地类图斑对象
        /// </summary>
        /// <param name="SourceXm"></param>
        /// <returns></returns>
        private DLTB ConversionDltb(Gis_Dltb SourceXm)
        {
            DLTB result = new DLTB();
            foreach (PropertyInfo pi in typeof(DLTB).GetProperties())
            {
                pi.SetValue(result, SourceXm.GetType().GetProperty(pi.Name.ToString()).GetValue(SourceXm, null), null);
            }
            return result;
        }

        /// <summary>
        /// 现状地物对象
        /// </summary>
        /// <param name="SourceXm"></param>
        /// <returns></returns>
        private XZDW ConversionXzdw(Gis_Xzdw SourceXm)
        {
            XZDW result = new XZDW();
            foreach (PropertyInfo pi in typeof(XZDW).GetProperties())
            {
                pi.SetValue(result, SourceXm.GetType().GetProperty(pi.Name.ToString()).GetValue(SourceXm, null), null);
            }
            return result;
        }

    }
}
