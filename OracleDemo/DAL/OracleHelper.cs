using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Collections;
using System.Configuration;

/******************************************************************************************************
 * 开发者：
 * 建立时间：
 * 进度描述：
 * 修订描述：
 * 修改者：
 * 修改时间：
 * 详细功能描述：oracle数据库访问操作类
 * ****************************************************************************************************/
namespace Dosoft.DAL
{
    /// <summary>
    /// oracle数据库访问操作类
    /// </summary>
    public abstract class OracleHelper
    {

        // 定义数据库连接字符串
        public static readonly string connectionString = ConfigurationManager.ConnectionStrings["apps"].ConnectionString;
        
        //Create a hashtable for the parameter cached
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string SQLString)
        {
            if (SQLString != null && SQLString.Trim() != "")
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        connection.Open();
                        OracleDataAdapter command = new OracleDataAdapter(SQLString, connection);
                        command.Fill(ds, "ds");
                    }
                    catch (System.Data.OracleClient.OracleException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                    return ds;
                }
            }
            else
            {
                return null;
            }
        }

       
    }
}
