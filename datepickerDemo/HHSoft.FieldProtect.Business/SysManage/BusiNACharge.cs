using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHSoft.FieldProtect.DataEntity.SysManage;
using System.Data.OracleClient;
using HHSoft.FieldProtect.DataAccess;
using System.Data;
using HHSoft.FieldProtect.DataEntity;
using HHSoft.FieldProtect.Framework.Utility;
namespace HHSoft.FieldProtect.Business.SysManage
{
    public class BusiNACharge
    {
        /// <summary>
        /// 根据资金年度取得记录
        /// </summary>
        /// <param name="zjnd"></param>
        /// <returns></returns>
        public DataTable GetXZFByZJND(string zjnd)
        {
            string strSql = "select * from XM_XZF where ZJND = '{0}'";
            strSql = string.Format(strSql, zjnd);
            return OracleHelper.ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 根据ID获取记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable GetXZFById(string id)
        {
            string strSql = "select * from XM_XZF where XZFID = '{0}'";
            strSql = string.Format(strSql, id);
            return OracleHelper.ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 判断是否存在相同资金年度的记录
        /// </summary>
        /// <param name="zjnd">资金年度</param>
        /// <returns>bool</returns>
        public bool ZJNDCheck(string zjnd, string xmdm)
        {
            string strSql = "select * from XM_XZF where ZJND = '{0}' and XZDM = '{1}'";
            strSql = string.Format(strSql, zjnd, xmdm);
            DataTable dt = OracleHelper.ExecuteDataTable(strSql);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable GetNAChargeByXDWH(string xdwh)
        {
            string strSql = "select * from XM_XZF where xdwh = '{0}'";
            strSql = string.Format(strSql, xdwh);
            return OracleHelper.ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 将当天的资金年度改为费当前
        /// </summary>
        /// <param name="dqnd"></param>
        /// <returns></returns>
        public bool UpdateDQND()
        {
            string strSql = "Update XM_XZF Set DQND = '0' Where DQND = '1'";
            strSql = string.Format(strSql);
            return OracleHelper.ExecuteCommand(strSql);

        }

        public string ValidateDelete(string XzfId)
        {
            string strSql = string.Empty;
            strSql = "select Count(*) from xm_xmxx where Zjnd  in (select zjnd from XM_XZF where xzfid = " + XzfId + ")";
            if (OracleHelper.ExecuteDataTable(strSql).Rows[0][0].ToString() != "0")
            {
                return "1";////资金年度下存在项目
            }
            return "0";
        }

        /// <summary>
        /// 新增费信息增删改
        /// </summary>
        /// <param name="xzf"></param>
        /// <returns></returns>
        public bool NACharge(XM_XZF xzf)
        {
            string strSql = string.Empty;
            switch (xzf.Action)
            {
                case ActionEnum.Insert:
                    if (xzf.DQND == "1")
                    {
                        UpdateDQND();
                    }
                    strSql = "Insert into XM_XZF(XZFID,XZDM,ZJND,ZJSE,XDWH,XDSJ,DQND) Values ('{1}'||{0},'{1}','{2}','{3}','{4}',to_date('{5}','yyyy-mm-dd hh24:mi:ss'),'{6}')";
                    strSql = string.Format(strSql, "SEQ_XZF.Nextval", xzf.XZDM, xzf.ZJND, xzf.ZJSE, xzf.XDWH, xzf.XDSJ, xzf.DQND);

                    break;
                case ActionEnum.Update:
                    if (xzf.DQND == "1")
                    {
                        UpdateDQND();
                    }
                    strSql = "Update XM_XZF Set ZJSE = '{1}',XDWH = '{2}',XDSJ=to_date('{3}','yyyy-mm-dd hh24:mi:ss'),DQND='{4}' Where ZJND = '{0}'";
                    strSql = string.Format(strSql, xzf.ZJND, xzf.ZJSE, xzf.XDWH, xzf.XDSJ, xzf.DQND);
                    break;
                case ActionEnum.Delete:
                    strSql = "delete from XM_XZF Where XZFID = '{0}'";
                    strSql = string.Format(strSql, xzf.ZJND);
                    break;
            }
            return OracleHelper.ExecuteCommand(strSql);
        }


        /// <summary>
        /// 获得新增费的集合
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public IList<XM_XZF> GetNACharge(XM_XZF xzf)
        {
            IList<XM_XZF> datalist = new List<XM_XZF>();
            string strWhere = string.Empty;
            if (!string.IsNullOrEmpty(xzf.XZDM)) strWhere += "XZDM = '" + xzf.XZDM + "' And ";
            if (!string.IsNullOrEmpty(xzf.ZJND)) strWhere += "ZJND = '" + xzf.ZJND + "' And ";
            if (!string.IsNullOrEmpty(xzf.DQND)) strWhere += "DQND = '" + xzf.DQND + "' And ";


            string strSql = "select * from XM_XZF where {0}  1 = 1  order by dqnd desc,zjnd";
            strSql = string.Format(strSql, strWhere);
            IDataReader dr = OracleHelper.ExecuteReader(strSql);
            while (dr.Read())
            {
                XM_XZF XZFEntity = new XM_XZF();
                XZFEntity.XZFID = Convert.ToInt32(dr["XZFID"]);
                XZFEntity.XZDM = dr["XZDM"].ToString();
                XZFEntity.ZJND = dr["ZJND"].ToString();
                XZFEntity.ZJSE = Convert.ToDecimal(dr["ZJSE"]);
                XZFEntity.XDWH = dr["XDWH"].ToString();
                XZFEntity.XDSJ = Convert.ToDateTime(dr["XDSJ"]);
                XZFEntity.DQND = dr["DQND"].ToString();
                datalist.Add(XZFEntity);
            }
            dr.Close();
            return datalist;
        }

        /// <summary>
        /// 根据区域代码获得新增费年度列表
        /// </summary>
        /// <param name="cCode">区域代码</param>
        /// <returns>新增费年度列表</returns>
        public List<string> GetNAByCcode(string cCode, ref string defaultZjnd)
        {
            List<string> datalist = new List<string>();

            string shortCode = cCode;
            string zjndDe = "";

            if (shortCode.Length == 6)
            {
                shortCode = shortCode.Substring(0, 4);
            }

            string sql = string.Format("select distinct t.zjnd,t.dqnd from xm_xzf t where t.xzdm like '{0}%' order by t.zjnd", shortCode);


            IDataReader dr = OracleHelper.ExecuteReader(sql);


            while (dr.Read())
            {

                string zjnd = dr["ZJND"].ToString();
                if (cCode.Length == 4 && dr["DQND"].ToString() == "1")
                {
                    zjndDe = zjnd;
                }
                if (!datalist.Contains(zjnd))
                    datalist.Add(zjnd);
            }
            dr.Close();
            defaultZjnd = zjndDe;
            return datalist;

        }

        /// <summary>
        /// 获取新增费年度列表
        /// </summary>
        /// <param name="cCode"></param>
        /// <returns></returns>
        public List<string> GetNAYearList(string cCode)
        {
            List<string> yearlist = new List<string>();
            string queryCode = cCode;
            if (queryCode.Length.Equals(6))
            {
                queryCode = CommonHelper.GetSHICode(cCode);
            }
            string strSql = "select distinct zjnd from xm_xzf  where xzdm like '{0}%' order by zjnd";
            strSql = string.Format(strSql, queryCode);

            IDataReader dr = OracleHelper.ExecuteReader(strSql);
            while (dr.Read())
            {
                if (!yearlist.Contains(dr["ZJND"].ToString()))
                    yearlist.Add(dr["ZJND"].ToString());
            }
            dr.Close();
            return yearlist;
        }

        public List<string> GetZjnds(out string defaultZjnd)
        {
            string sql = "select distinct(t.zjnd) from xm_xzf t order by t.zjnd desc";
            DataTable dt = OracleHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count == 0)
            {
                defaultZjnd = string.Empty;
            }
            else
            {
                defaultZjnd = dt.Rows[0][0].ToString();
            }
            return (from DataRow dr in dt.Rows select dr["zjnd"].ToString()).ToList();
        }
    }
}
