using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.OracleClient;
using System.Data;
using System.Configuration;
using System.Reflection;
using log4net;

namespace HHSoft.FieldProtect.DataAccess
{
    /// <summary>
    /// Oracle数据库工具类
    /// </summary>
    public class OracleHelper
    {
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());////为参数提供缓存集合

        private static string connString = ConfigurationSettings.AppSettings["ConnectionString"];////数据库连接字符串

        private static ILog logger = LogManager.GetLogger("dataLog");

        public static string ConnectionString
        {
            get
            {
                return connString;
            }
        }


        #region  ExecuteDataTable,返回数据集

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="CommandText"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string CommandText)
        {
            return ExecuteDataTable(CommandType.Text, CommandText, null);
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="spName">存储过程名称</param>
        /// <param name="myParams">存储过程参数</param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string spName, OracleParameter[] myParams)
        {
            return ExecuteDataTable(CommandType.StoredProcedure, spName, myParams);
        }

        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="CommandText"></param>
        /// <param name="myParams"></param>
        /// <returns></returns>
        private static DataTable ExecuteDataTable(CommandType commandType,
             string CommandText, OracleParameter[] myParams)
        {
            using (OracleConnection conn = new OracleConnection(connString))
            {                
                //创建一个新的command			
                OracleCommand cmd = new OracleCommand();
                //调用PrepareCommand方法来构造该command
                PrepareCommand(cmd, conn, null, commandType, CommandText, myParams);
                //创建一个适配器
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                //创建一个dataTable
                DataTable dt = new DataTable();
                try
                {
                    //填充该dataTable
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    logger.Info(CommandText);
                    throw ex;
                }
                //返回dataTable
                return dt;               
                
            }
        }

        #endregion

        #region ExecuteCommand,执行command命令

        /// <summary>
        /// 带事务处理的SQL操作,重载,单个SQL语句
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        /// <param name="commandTextArray">要执行的SQL语句</param>
        /// <returns>布尔值，成功返回TRUE,失败返回FALSE</returns>
        public static bool ExecuteCommand(string commandText)
        {
            string[] TempArr = new string[1];
            TempArr[0] = commandText;
            return ExecuteCommand(TempArr);
        }

        /// <summary>
        /// 带事务处理的SQL操作
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        /// <param name="commandTextArray">要执行的SQL语句数组</param>
        /// <returns>布尔值，成功返回TRUE,失败返回FALSE</returns>
        public static bool ExecuteCommand(ArrayList commandTextArray)
        {
            string[] arySql = (string[])commandTextArray.ToArray(typeof(string));
            return ExecuteCommand(arySql);
        }

        /// <summary>
        /// 带事务处理的SQL操作
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        /// <param name="commandTextArray">要执行的SQL语句数组</param>
        /// <returns>布尔值，成功返回TRUE,失败返回FALSE</returns>
        public static bool ExecuteCommand(string[] commandTextArray)
        {            
            OracleConnection myConnection = new OracleConnection(connString);
            myConnection.Open();

            OracleCommand myCommand = new OracleCommand();
            OracleTransaction myTrans;

            myTrans = myConnection.BeginTransaction();
            myCommand.Connection = myConnection;
            myCommand.Transaction = myTrans;

            try
            {
                for (int i = commandTextArray.GetLowerBound(0); i <= commandTextArray.GetUpperBound(0); i++)
                {
                    if (commandTextArray.GetValue(i) != null)
                    {
                        myCommand.CommandText = commandTextArray.GetValue(i).ToString();
                        myCommand.ExecuteNonQuery();
                    }
                }
                myTrans.Commit();
                return true;
            }
            catch(Exception ex)
            {
                logger.Error(ex.Message);
                logger.Info(myCommand.CommandText);
                myTrans.Rollback();
                return false;
            }
            finally
            {
                myConnection.Close();
                myConnection = null;
            }
        }

        #endregion ExecuteCommand,执行command命令

        #region ExecuteReader,返回DataReader

        /// <summary>
        /// 执行Sql语句
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">存储过程或语句</param>
        /// <param name="commandParameters">命令参数</param>
        /// <returns></returns>
        public static OracleDataReader ExecuteReader(string cmdText)
        {
            OracleCommand cmd = new OracleCommand();
            OracleConnection conn = new OracleConnection(connString);
            OracleDataReader rdr = null;
            try
            {
                PrepareCommand(cmd, conn, null, CommandType.Text, cmdText, null);

                rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;

            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="myParams"></param>
        /// <returns></returns>
        public static OracleDataReader ExecuteReader(string spName, OracleParameter[] myParams)
        {
            OracleCommand cmd = new OracleCommand();
            OracleConnection conn = new OracleConnection(connString);
            try
            {
                PrepareCommand(cmd, conn, null, CommandType.StoredProcedure, spName, myParams);

                OracleDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;

            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
            finally
            {
                conn.Close();
                conn = null;
            }
        }

        #endregion

        #region 构造适配器
        /// <summary>
        /// 适配Command对象
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="trans">Oracle事务对象</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="cmdText">Sql或存储过程名</param>
        /// <param name="commandParameters">存储过程参数</param>
        private static void PrepareCommand(OracleCommand cmd, OracleConnection conn, OracleTransaction trans,
            CommandType cmdType, string cmdText, OracleParameter[] commandParameters)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;

            if (trans != null)
                cmd.Transaction = trans;

            if (commandParameters != null)
            {
                foreach (OracleParameter parm in commandParameters)
                    cmd.Parameters.Add(parm);
            }

        }
        #endregion

        /// <summary>
        /// 缓存参数集合
        /// </summary>
        /// <param name="cacheKey">Key value to look up the parameters</param>
        /// <param name="commandParameters">Actual parameters to cached</param>
        public static void CacheParameters(string cacheKey, params OracleParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// 从缓存中获取参数集合
        /// </summary>
        /// <param name="cacheKey">Key to look up the parameters</param>
        /// <returns></returns>
        public static OracleParameter[] GetCachedParameters(string cacheKey)
        {
            OracleParameter[] cachedParms = (OracleParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            // If the parameters are in the cache
            OracleParameter[] clonedParms = new OracleParameter[cachedParms.Length];

            // return a copy of the parameters
            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (OracleParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }


        /// <summary>
        /// Converter to use boolean data type with Oracle
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns></returns>
        public static string OraBit(bool value)
        {
            if (value)
                return "Y";
            else
                return "N";
        }

        /// <summary>
        /// Converter to use boolean data type with Oracle
        /// </summary>
        /// <param name="value">Value to convert</param>
        /// <returns></returns>
        public static bool OraBool(string value)
        {
            if (value.Equals("Y"))
                return true;
            else
                return false;
        }
    }
}
