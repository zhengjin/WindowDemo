using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XL.Models.Sys;

namespace XL.DataAccess.Sys
{
    public class LogDA
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(Guid Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from LogInfo");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16)};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(LogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into LogInfo(");
            strSql.Append("Id,LogName,LogInfo,IP,HostName,LogType,LogTime)");
            strSql.Append(" values (");
            strSql.Append("@Id,@LogName,@LogInfo,@IP,@HostName,@LogType,@LogTime)");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@LogName", SqlDbType.VarChar,250),
					new SqlParameter("@LogModel", SqlDbType.VarChar),
					new SqlParameter("@IP", SqlDbType.VarChar,150),
					new SqlParameter("@HostName", SqlDbType.VarChar,150),
					new SqlParameter("@LogType", SqlDbType.Int,4),
					new SqlParameter("@LogTime", SqlDbType.DateTime)};
            parameters[0].Value = Guid.NewGuid();
            parameters[1].Value = model.LogName;
            parameters[2].Value = model.LogInfo;
            parameters[3].Value = model.IP;
            parameters[4].Value = model.HostName;
            parameters[5].Value = model.LogType;
            parameters[6].Value = model.LogTime;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(LogModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update LogInfo set ");
            strSql.Append("LogName=@LogName,");
            strSql.Append("LogInfo=@LogInfo,");
            strSql.Append("IP=@IP,");
            strSql.Append("HostName=@HostName,");
            strSql.Append("LogType=@LogType,");
            strSql.Append("LogTime=@LogTime");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@LogName", SqlDbType.VarChar,250),
					new SqlParameter("@LogInfo", SqlDbType.VarChar),
					new SqlParameter("@IP", SqlDbType.VarChar,150),
					new SqlParameter("@HostName", SqlDbType.VarChar,150),
					new SqlParameter("@LogType", SqlDbType.Int,4),
					new SqlParameter("@LogTime", SqlDbType.DateTime),
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16)};
            parameters[0].Value = model.LogName;
            parameters[1].Value = model.LogInfo;
            parameters[2].Value = model.IP;
            parameters[3].Value = model.HostName;
            parameters[4].Value = model.LogType;
            parameters[5].Value = model.LogTime;
            parameters[6].Value = model.Id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(Guid Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from LogInfo ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16)};
            parameters[0].Value = Id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from LogInfo ");
            strSql.Append(" where Id in (" + Idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public LogModel GetModel(Guid Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,LogName,LogInfo,IP,HostName,LogType,LogTime from LogInfo ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16)};
            parameters[0].Value = Id;

            LogModel model = new LogModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"] != null && ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = new Guid(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LogName"] != null && ds.Tables[0].Rows[0]["LogName"].ToString() != "")
                {
                    model.LogName = ds.Tables[0].Rows[0]["LogName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LogModel"] != null && ds.Tables[0].Rows[0]["LogModel"].ToString() != "")
                {
                    model.LogInfo = ds.Tables[0].Rows[0]["LogInfo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["IP"] != null && ds.Tables[0].Rows[0]["IP"].ToString() != "")
                {
                    model.IP = ds.Tables[0].Rows[0]["IP"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HostName"] != null && ds.Tables[0].Rows[0]["HostName"].ToString() != "")
                {
                    model.HostName = ds.Tables[0].Rows[0]["HostName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["LogType"] != null && ds.Tables[0].Rows[0]["LogType"].ToString() != "")
                {
                    model.LogType = int.Parse(ds.Tables[0].Rows[0]["LogType"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LogTime"] != null && ds.Tables[0].Rows[0]["LogTime"].ToString() != "")
                {
                    model.LogTime = DateTime.Parse(ds.Tables[0].Rows[0]["LogTime"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Id,LogName,LogInfo,IP,HostName,LogType,LogTime ");
            strSql.Append(" FROM LogInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" Id,LogName,LogInfo,IP,HostName,LogType,LogTime ");
            strSql.Append(" FROM LogInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM LogInfo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.Id desc");
            }
            strSql.Append(")AS Row, T.*  from LogInfo T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}
