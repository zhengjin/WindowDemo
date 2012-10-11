using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using HHSoft.FieldProtect.DataAccess;
using HHSoft.FieldProtect.DataEntity.PersonalManage;
using System.Data.OracleClient;
using HHSoft.FieldProtect.DataEntity.SysManage;

namespace HHSoft.FieldProtect.Business.PersonalManage
{
    /// <summary>
    /// 个人中心查询类
    /// </summary>
    public class PersonalCenterQuery
    {
        /// <summary>
        /// 根据用户和时间查询处理过的项目信息
        /// </summary>
        /// <param name="query">个人中心查询类</param>
        /// <returns></returns>
        public DataTable GetItemInstace(CenterQuery query, out int recordCount)
        {
            string sql = string.Format(@"select a.*,b.enddate from xm_xmxx a
                   left join (select flowid, itemCode, max(enddate) enddate from wf_instance
                   where state = 1 and userId = {0} group by flowid, itemCode) b
                   on a.flowid = b.flowid and a.itemcode = b.itemcode 
                   where a.ItemState in (1,2) and b.enddate >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                   and b.enddate <= to_date('{2}','yyyy-mm-dd hh24:mi:ss')
                   order by b.enddate desc", query.UserID, 
                                           query.BeginDate.ToString(), query.EndDate.ToString());
            
            #region 设置参数
            OracleParameter pageSize = new OracleParameter("p_PageSize", OracleType.Number, 10);
            pageSize.Value = query.PageSize;
            OracleParameter pageIndex = new OracleParameter("p_PageIndex", OracleType.Number, 10);
            pageIndex.Value = query.PageIndex;
            OracleParameter sqlSelect = new OracleParameter("p_SqlSelect", OracleType.VarChar, 4000);
            sqlSelect.Value = sql;
            OracleParameter recount = new OracleParameter("p_OutRecordCount", OracleType.Number, 5);
            recount.Direction = ParameterDirection.Output;
            OracleParameter returnCursor = new OracleParameter("p_OutCursor", OracleType.Cursor);
            returnCursor.Direction = ParameterDirection.Output;

            OracleParameter[] oracleParameters = { pageSize, pageIndex, sqlSelect, recount, returnCursor };
            #endregion

            DataTable dt = OracleHelper.ExecuteDataTable("CommonPackages.sp_Page", oracleParameters);
            recordCount = int.Parse(recount.Value.ToString());
            return dt;
        }

        public DataTable GetMessages(Dictionary<string, string> conditions)
        {
            StringBuilder sbWhere = new StringBuilder();
            foreach (var condition in conditions)
            {
                sbWhere.AppendLine(condition.Key + "'" + condition.Value + "' and");
            }

            string strSql = "select xxbh,min(fssj) fssj,min(XXBT) XXBT,min(XXNR) XXNR,min(FSR) FSR,wm_concat(JSR) JSR,min(YDZT) YDZT from XTXX a left join xtxxjs b on a.xxbh = b.jsxxbh where {0} 1=1 group by xxbh order by fssj DESC";
            strSql = string.Format(strSql, sbWhere.ToString());
            return OracleHelper.ExecuteDataTable(strSql);
        }


        /// <summary>
        /// 获取用户的代办数量
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public DataTable GetUserDB(LoginUser user)
        {
            string strSql = string.Empty;
            
            ////角色条件
            string strRoleWhere = string.Empty;
            string[] roleAry = user.RoleId.Split(new char[] { ',' });
            for (int i = 0; i < roleAry.Length; i++)
            {
                if (roleAry[i] != string.Empty)
                {
                    strRoleWhere += string.Format(" or instr(',' || NodeRoleId || ',', ',{0},') <> 0", roleAry[i]);
                }
            }
            ////部门条件
            string strDeptWhere = string.Empty;
            if (!string.IsNullOrEmpty(user.DepartCode))
                strDeptWhere = string.Format(" or instr(',' || NodeDepartCode || ',', ',{0},') <> 0", user.DepartCode);

            strSql = "select count(1) num, b.nodeid, c.functioncode, c.functionurl from wf_instance a"
                 + " left join wf_node b on a.nodeid = b.nodeid and a.flowid = b.flowid"
                 + " left join function c  on b.functioncode = c.functioncode"
                 + " left join xm_xmxx d on a.itemcode = d.itemcode"
                 + " where substr(a.itemcode, 0, {0}) = '{1}' and a.state = 0 and d.wfstate = 0"
                 + " and (instr(',' || b.NodeUserId || ',', ',{2},') <> 0 {3} {4})"
                 + " group by b.nodeid, c.functioncode, c.functionurl";
            strSql = string.Format(strSql, user.CompanyShortCode.Length, user.CompanyShortCode, user.UserId, strRoleWhere, strDeptWhere);

            DataTable dt = OracleHelper.ExecuteDataTable(strSql);

            return dt;
        }


        /// <summary>
        /// 根据组织机构短编码获得新增费的总和
        /// </summary>
        /// <param name="shotCode">组织结构短编码</param>
        /// <returns>新增费总和</returns>
        public double GetSumXzfByShotCode(string shotCode, string zjnd)
        {
            string sql = string.Format("select nvl(sum(zjse),0) num from xm_xzf  where xzdm like '{0}%' and zjnd='{1}'", shotCode, zjnd);

            DataTable dt = OracleHelper.ExecuteDataTable(sql);

            return Convert.ToDouble(dt.Rows[0]["num"].ToString());
        }

        /// <summary>
        /// 根据组织机构短编码获得立项资金总和
        /// </summary>
        /// <param name="shotCode">组织结构短编码</param>
        /// <returns>立项资金总和</returns>
        /// <remarks>已经作废</remarks>
        public double GetSumLXZJ(string shotCode)
        {
            string sql = string.Format(@"with a as
                                         (select j.itemcode, max(j.stage) stage
                                            from XM_XMZJ j, xm_xmxx x
                                           where j.itemcode = x.itemcode
                                             and x.lxsj is not null and substr(j.itemcode,0,6) like '{0}%'
                                           group by j.itemcode),
                                        b as
                                         (select j.itemcode, max(j.stage) stage, max(j.xh) xh
                                            from xm_xmzj j, A
                                           where j.itemcode = A.itemcode
                                             and j.stage = A.stage
                                           group by j.itemcode)

                                        select sum(j.zjze) num
                                          from xm_xmzj j, A, B
                                         where j.itemcode = A.itemcode
                                           and j.stage = A.stage
                                           and j.itemcode = B.itemcode
                                           and j.xh = B.Xh
                                        ", shotCode);

            DataTable dt = OracleHelper.ExecuteDataTable(sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                return string.IsNullOrEmpty(dt.Rows[0]["num"].ToString()) ? 0 : Convert.ToDouble(dt.Rows[0]["num"].ToString());
            }

            return 0;
        }

        /// <summary>
        /// 根据组织机构短编码获取立项数量和开工数量
        /// </summary>
        /// <param name="shotCode">组织机构短编码</param>
        /// <param name="LxCount">立项数量</param>
        /// <param name="KgCount">开工数量</param>
        public void GetLxAndKgCount(string shotCode, ref double LxCount, ref double KgCount, string zjnd)
        {
            string sql = string.Format(@"
                                        with a as
                                         (select count(0) lxCount
                                            from xm_xmxx x
                                           where x.lxsj is not null
                                             and substr(x.itemcode, 0, 6) like '{0}%' and x.zjnd='{1}' and x.itemstate='1' )
                                        , b as(select count(0) kgCount
                                          from xm_xmxx x
                                         where x.kgsj is not null
                                           and substr(x.itemcode, 0, 6) like '{0}%' and x.zjnd='{1}' and x.itemstate='1' )

                                        select * from A,B", shotCode, zjnd);

            DataTable dt = OracleHelper.ExecuteDataTable(sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                LxCount = Convert.ToDouble(dt.Rows[0]["lxcount"]);
                KgCount = Convert.ToDouble(dt.Rows[0]["kgcount"]);
            }
        }

        /// <summary>
        /// 根据组织机构短编码获取拨付资金总和
        /// </summary>
        /// <param name="shotCode">组织机构短编码</param>
        /// <returns>拨付资金总和</returns>
        /// <remarks>已经作废</remarks>
        public double GetBfZjByShotCode(string shotCode)
        {
            string sql = string.Format(@"select sum(t.bfje) num from xm_ss_zjbf t where substr(t.itemcode,0,6) like '{0}%'", shotCode);

            DataTable dt = OracleHelper.ExecuteDataTable(sql);

            if (dt != null && dt.Rows.Count > 0)
            {
                return string.IsNullOrEmpty(dt.Rows[0]["num"].ToString()) ? 0 : Convert.ToDouble(dt.Rows[0]["num"].ToString());
            }

            return 0;
        }

        /// <summary>
        /// 根据组织机构短编码获取各市或各区县立项资金总和
        /// </summary>
        /// <param name="shortcode">组织机构短编码</param>
        /// <returns>数据集</returns>
        public DataTable GetAllCompanyLxzj(string shortcode, string zjnd)
        {
            string sql = string.Format(@"with a as
                                     (select j.itemcode, max(j.stage) stage
                                        from XM_XMZJ j, xm_xmxx x
                                       where j.itemcode = x.itemcode
                                         and x.lxsj is not null
                                         and substr(j.itemcode, 0, 6) like '{0}%' and x.zjnd='{2}' and x.itemstate!=0 and x.wfstate!=2 and x.wfstate!=3
                                       group by j.itemcode),
                                    b as
                                     (select j.itemcode, max(j.stage) stage, max(j.xh) xh
                                        from xm_xmzj j, A
                                       where j.itemcode = A.itemcode
                                         and j.stage = A.stage
                                       group by j.itemcode),
                                    c as
                                     (select *
                                        from company c
                                       where length(c.shortccode) = {1}
                                         and c.shortccode like '{0}%'),

                                    d as(select sum(j.zjze) num,C.shortccode,C.cname
                                      from xm_xmzj j, A, B,C
                                     where j.itemcode = A.itemcode
                                       and j.stage = A.stage
                                       and j.itemcode = B.itemcode
                                       and j.xh = B.Xh and substr(j.itemcode, 0,{1})=C.shortccode(+)
                                       group by C.shortccode,C.cname)
                                       
                                       
                                     select C.shortccode,D.num,C.cname from C,D where C.shortccode=d.shortccode(+) order by C.shortccode", shortcode, shortcode.Length + 2, zjnd);
            DataTable dt = OracleHelper.ExecuteDataTable(sql);

            return dt;

        }

        public List<ItemQuery> GetItemQueryData(string shortcode, string zjnd)
        {
            List<ItemQuery> dataList = new List<ItemQuery>();
            OracleParameter oraShortcCode = new OracleParameter("ShortcCode", OracleType.VarChar, 20);
            oraShortcCode.Value = shortcode;
            OracleParameter oraZjnd = new OracleParameter("Zjnd", OracleType.VarChar, 20);
            oraZjnd.Value = zjnd;
            OracleParameter oraXmnd = new OracleParameter("Xmnd", OracleType.VarChar, 20);
            oraXmnd.Value = string.Empty;
            OracleParameter returnCursor = new OracleParameter("ReturnCursor", OracleType.Cursor);
            returnCursor.Direction = ParameterDirection.Output;

            OracleParameter[] oracleParameters = { oraShortcCode, oraZjnd, oraXmnd, returnCursor };

            DataTable dt = OracleHelper.ExecuteDataTable("QueryPackages.ItemZjQuery", oracleParameters);

            
            foreach(DataRow dr in dt.Rows)
            {
                ItemQuery data = new ItemQuery();
                data.CCode = dr["ccode"].ToString();
                data.CName = dr["cname"].ToString();
                data.YsMoney = Convert.ToDouble(dr["zjze"]);
                data.zjzeitemcodes = dr["zjzeitemcodes"].ToString();
                data.BfMoney = Convert.ToDouble(dr["bfje"]);
                data.bfjeitemcodes = dr["bfjeitemcodes"].ToString();
                data.LxNum = Convert.ToDouble(dr["lxNum"]);
                data.lxitemcodes = dr["lxitemcodes"].ToString();
                data.KgNum = Convert.ToDouble(dr["kgNum"]);
                data.kgitemcodes = dr["kgitemcodes"].ToString();
                data.YsNum = Convert.ToDouble(dr["ysNum"]);
                data.ysitemcodes = dr["ysitemcodes"].ToString();


                dataList.Add(data);
            }
            return dataList;
        }
       

        /// <summary>
        /// 根据组织机构短编码获取各市或各区县拨付资金
        /// </summary>
        /// <param name="shortcode">组织机构短编码</param>
        /// <returns>数据集</returns>
        public DataTable GetAllCompanyBfzj(string shortcode, string zjnd)
        {
            string sql = string.Format(@"with a as
                                        (select sum(t.bfje) num,c.shortccode,c.cname
                                          from xm_ss_zjbf t, company c,xm_xmxx x
                                         where substr(t.itemcode, 0, 6) like '{0}%'
                                         and substr(t.itemcode,0,{1})=c.shortccode and t.itemcode=x.itemcode and x.zjnd='{2}'  and x.itemstate!=0 and x.wfstate!=2 and x.wfstate!=3
                                        group by c.shortccode,c.cname),
                                        b as(
                                          select * from company c where length(c.shortccode)={1} and c.shortccode like '{0}%'
                                        )

                                        select a.num,b.cname,b.shortccode from a,b where b.shortccode=a.shortccode(+) order by b.shortccode", shortcode, shortcode.Length + 2, zjnd);

            DataTable dt = OracleHelper.ExecuteDataTable(sql);

            return dt;
        }

        /// <summary>
        /// 根据组织机构短编码获取各城市或区县项目进展情况
        /// </summary>
        /// <param name="shortCode">组织机构短编码</param>
        /// <returns>数据集</returns>
        public DataTable GetAllCompanyXmJz(string shortCode, string zjnd)
        {
            string sql = string.Format(@"with a as
                                         (select count(x.lxsj) lxcount,
                                                 count(x.kgsj) kgcount,
                                                 count(x.yssj) ysCount,
                                                 substr(x.ccode,0,{1}) ccode
                                            from xm_xmxx x
                                          where x.zjnd='{2}' and x.itemstate='1' 
                                           group by substr(x.ccode, 0, {1})),
                                        b as
                                         (select *
                                            from company c
                                           where length(c.shortccode) = {1}
                                             and c.shortccode like '{0}%')
                                             
                                         select a.lxcount,a.kgcount,a.yscount,b.cname,b.shortccode from a,b where b.shortccode=a.ccode(+)
                                        ", shortCode, shortCode.Length + 2, zjnd);

            return OracleHelper.ExecuteDataTable(sql);
        }

        
    }
}
