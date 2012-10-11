using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Win32;

namespace ArcGISAddInShowMap
{
    public class DBClass
    {
        string strServerName = string.Empty;
        string strAuthentication = string.Empty;
        string strUserName = string.Empty;
        string strPassword = string.Empty;
        string strDatabase = string.Empty;
        public DBClass()
        { }

        /// <summary>
        /// 查询数据，返回SqlDataReader
        /// </summary>
        /// <returns>SqlDataReader</returns>
        public DataSet GetDs(string strSql)
        {
            //获取注册表的信息
            this.ReadInfo();
            //string strSql = "select * from ArcTest where 1=1" + strAnd + " order by orders";
            DataSet ds = null;
            SqlConnection sqlconn = null;
            try
            {
                sqlconn = new SqlConnection("database=" + strDatabase + ";user id=" + strUserName + ";password=" + strPassword + "; server=" + strServerName + ";");
                //sqlconn = new SqlConnection("database=" + strDatabase + "; server=" + strServerName + ";");
                //database=Database;user id=sa;password=sqlserver; server=192.0.0.206;
                //打开与数据库的连接 
                sqlconn.Open();

                //创建DataAdapter数据适配器实例
                SqlDataAdapter da = new SqlDataAdapter(strSql, sqlconn);

                //创建DataSet实例
                ds = new DataSet();

                //使用DataAdapter的Fill方法(填充)，调用SELECT命令            
                da.Fill(ds, "ds");
            }
            catch (Exception)
            {
                return null;
               // throw;
            }
            finally
            {
                //关闭与数据库的连接
                sqlconn.Close();
            }
            return ds;
        }

        #region 读取注册表
        private void ReadInfo()
        {
            RegistryKey regRead;
            //读取HKEY_CURRENT_USER主键里的Software子键下名为“Test”的子键 
            regRead = Registry.CurrentUser.OpenSubKey("Software\\ESRI\\PLUG\\UTP", true);
            if (regRead != null)   //如果该子键不存在 
            {
                if (regRead.GetValue("strServerName") != null)
                {
                    strServerName = regRead.GetValue("strServerName").ToString();
                }
                if (regRead.GetValue("strAuthentication") != null)
                {
                    strAuthentication = regRead.GetValue("strAuthentication").ToString();
                }
                if (regRead.GetValue("strUserName") != null)
                {
                    strUserName = regRead.GetValue("strUserName").ToString();
                }
                if (regRead.GetValue("strPassword") != null)
                {
                    strPassword = regRead.GetValue("strPassword").ToString();
                }
                if (regRead.GetValue("strDatabase") != null)
                {
                    strDatabase = regRead.GetValue("strDatabase").ToString();
                }
            }
        }
        #endregion


    }
}
