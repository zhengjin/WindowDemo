using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.OracleClient;

namespace HHSoft.FieldProtect.DataAccess
{
    /// <summary>
    /// 数据库操作类
    /// </summary>
    /// <typeparam name="DataAdapter"></typeparam>
    /// <typeparam name="CommandBuilder"></typeparam>
    /// <typeparam name="Parameter"></typeparam>
    public abstract class DbOperation<DataAdapter, CommandBuilder, Parameter>
        where DataAdapter : DbDataAdapter, new()
        where CommandBuilder : DbCommandBuilder, new()
        where Parameter : DbParameter
    {
        protected DbConnection con;
        protected DbTransaction tran;
        protected string connStr = string.Empty;

        public DbOperation(string conStr)
        {
            this.con = new OracleConnection(conStr);
            this.connStr = conStr;
        }

        /// <summary>
        /// 返回连接字符串
        /// </summary>
        public string ConnectionStr
        {
            get
            {
                return this.connStr;
            }
        }

        /// <summary>
        /// 构造Commond对象
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private DbCommand PrepareCommand(string cmdText, List<Parameter> parameters)
        {
            if (con.State != ConnectionState.Open)
                con.Open();
            DbCommand cmd = con.CreateCommand();
            cmd.CommandText = cmdText;           
            
            if (tran != null) cmd.Transaction = tran;

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
            return cmd;  
        }

        
        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="tableName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string strSql, string tableName, List<Parameter> parameters)
        {
            DbCommand cmd = PrepareCommand(strSql, parameters);
            DataAdapter da = new DataAdapter();
            da.SelectCommand = cmd;
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            DataTable dt = new DataTable() {TableName = tableName };            
            da.Fill(dt);
            return dt;
        }

        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string strSql)
        {
            return ExecuteDataTable(strSql, string.Empty, null);
        }
        

        /// <summary>
        /// 执行Sql语句，返回影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string strSql, List<Parameter> parameters)
        {
            DbCommand cmd = PrepareCommand(strSql, parameters);
            int result = cmd.ExecuteNonQuery();
            return result;
        }

        /// <summary>
        /// 更新DataTable
        /// </summary>
        /// <param name="dt"></param>
        public void UpdateDataTable(DataTable dt)
        {
            string selectCommand = "select * from " + dt.TableName;
            DataAdapter da = new DataAdapter();
            da.SelectCommand = PrepareCommand(selectCommand, null);
            CommandBuilder cb = new CommandBuilder();
            cb.DataAdapter = da;
            if (tran != null)
            {
                da.InsertCommand = cb.GetInsertCommand();
                da.InsertCommand.Transaction = tran;
                if (dt.PrimaryKey.Length > 0)
                {
                    da.UpdateCommand = cb.GetUpdateCommand();
                    da.UpdateCommand.Transaction = tran;
                    da.DeleteCommand = cb.GetDeleteCommand();
                    da.DeleteCommand.Transaction = tran;
                }
            }
            da.Update(dt);
        }

        /// <summary>
        /// 测试连接
        /// </summary>
        /// <returns></returns>
        public bool TestConn()
        {
            try
            {
                con.Open();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                con.Close();
                //con.Dispose();
            }
        }

        #region 事务

        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTransaction()
        {            
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            tran = con.BeginTransaction();
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            tran.Commit();
            tran.Dispose();
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            tran.Rollback();
            tran.Dispose();
        }
        #endregion

    }
}
