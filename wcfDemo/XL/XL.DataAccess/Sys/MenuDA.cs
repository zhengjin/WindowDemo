using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using XL.Models.Sys;

namespace XL.DataAccess.Sys
{
    public class MenuDA
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(Guid Id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from MenuInfo");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16)};
            parameters[0].Value = Id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(MenuModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MenuInfo(");
            strSql.Append("Id,MenuName,ParentId,Url,MenuDes)");
            strSql.Append(" values (");
            strSql.Append("@Id,@MenuName,@ParentId,@Url,@MenuDes)");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@MenuName", SqlDbType.VarChar,150),
					new SqlParameter("@ParentId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@Url", SqlDbType.VarChar,350),
					new SqlParameter("@MenuDes", SqlDbType.VarChar)};
            parameters[0].Value = Guid.NewGuid();
            parameters[1].Value = model.MenuName;
            parameters[2].Value = Guid.NewGuid();
            parameters[3].Value = model.Url;
            parameters[4].Value = model.MenuDes;

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
        public bool Update(MenuModel model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MenuInfo set ");
            strSql.Append("MenuName=@MenuName,");
            strSql.Append("ParentId=@ParentId,");
            strSql.Append("Url=@Url,");
            strSql.Append("MenuDes=@MenuDes");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@MenuName", SqlDbType.VarChar,150),
					new SqlParameter("@ParentId", SqlDbType.UniqueIdentifier,16),
					new SqlParameter("@Url", SqlDbType.VarChar,350),
					new SqlParameter("@MenuDes", SqlDbType.VarChar),
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16)};
            parameters[0].Value = model.MenuName;
            parameters[1].Value = model.ParentId;
            parameters[2].Value = model.Url;
            parameters[3].Value = model.MenuDes;
            parameters[4].Value = model.Id;

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
            strSql.Append("delete from MenuInfo ");
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
            strSql.Append("delete from MenuInfo ");
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
        /// 得到所有对象
        /// </summary>
        public List<MenuModel> GetAllModels()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from MenuInfo ");
            var result = new List<MenuModel>();
            var dr = DbHelperSQL.ExecuteReader(strSql.ToString());
            while(dr.Read())
            {
                MenuModel model = new MenuModel();
                if (dr["Id"] != DBNull.Value)
                {
                    model.Id = Guid.Parse(dr["Id"].ToString());
                }
                if (dr["MenuName"] != null && dr["MenuName"].ToString() != "")
                {
                    model.MenuName = dr["MenuName"].ToString();
                }
                if (dr["ParentId"] != null && dr["ParentId"].ToString() != "")
                {
                    model.ParentId = new Guid(dr["ParentId"].ToString());
                }
                if (dr["Url"] != null && dr["Url"].ToString() != "")
                {
                    model.Url = dr["Url"].ToString();
                }
                if (dr["MenuDes"] != null && dr["MenuDes"].ToString() != "")
                {
                    model.MenuDes = dr["MenuDes"].ToString();
                }
                if (dr["OrderNum"] != DBNull.Value)
                {
                    model.OrderNum = Convert.ToInt32(dr["OrderNum"]);
                }
                result.Add(model);
            }
            return result;
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public MenuModel GetModel(Guid Id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 Id,MenuName,ParentId,Url,MenuDes from MenuInfo ");
            strSql.Append(" where Id=@Id ");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.UniqueIdentifier,16)};
            parameters[0].Value = Id;

            MenuModel model = new MenuModel();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Id"] != null && ds.Tables[0].Rows[0]["Id"].ToString() != "")
                {
                    model.Id = new Guid(ds.Tables[0].Rows[0]["Id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["MenuName"] != null && ds.Tables[0].Rows[0]["MenuName"].ToString() != "")
                {
                    model.MenuName = ds.Tables[0].Rows[0]["MenuName"].ToString();
                }
                if (ds.Tables[0].Rows[0]["ParentId"] != null && ds.Tables[0].Rows[0]["ParentId"].ToString() != "")
                {
                    model.ParentId = new Guid(ds.Tables[0].Rows[0]["ParentId"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Url"] != null && ds.Tables[0].Rows[0]["Url"].ToString() != "")
                {
                    model.Url = ds.Tables[0].Rows[0]["Url"].ToString();
                }
                if (ds.Tables[0].Rows[0]["MenuDes"] != null && ds.Tables[0].Rows[0]["MenuDes"].ToString() != "")
                {
                    model.MenuDes = ds.Tables[0].Rows[0]["MenuDes"].ToString();
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
            strSql.Append("select Id,MenuName,ParentId,Url,MenuDes ");
            strSql.Append(" FROM MenuInfo ");
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
            strSql.Append(" Id,MenuName,ParentId,Url,MenuDes ");
            strSql.Append(" FROM MenuInfo ");
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
            strSql.Append("select count(1) FROM MenuInfo ");
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
            strSql.Append(")AS Row, T.*  from MenuInfo T ");
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
