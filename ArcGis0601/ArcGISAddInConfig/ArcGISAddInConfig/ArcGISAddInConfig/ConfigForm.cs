using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Data.SqlClient;
using System.Collections;

namespace ArcGISAddInConfig
{
    public partial class ConfigForm : Form
    {
        string strServerName = string.Empty;
        string strAuthentication = string.Empty;
        string strUserName = string.Empty;
        string strPassword = string.Empty;
        string strDatabase = string.Empty;
        public ConfigForm()
        {
            InitializeComponent();
            this.ReadInfo();
        }
        #region Apply 把参数保存在注册表中
        private void but_Apply_Click(object sender, EventArgs e)
        {          

            strServerName = text_ServerName.Text;
            strAuthentication = com_Authentication.Text;
            strUserName = text_UserName.Text;
            strPassword = text_Password.Text;
            strDatabase = com_Database.Text;


            string ConnectString = string.Empty;
            //   sqlconn = new SqlConnection("database=JobTest;user id=sa;password=sqlserver; server=dev-db;");
            ConnectString = "database="+strDatabase+";user id="+strUserName+";password="+strPassword+"; server="+strServerName+";";
            if (this.Connect(ConnectString))
            {
                this.regWrite(strServerName, strAuthentication, strUserName, strPassword, strDatabase);
                //MessageBox.Show(("SQL Server connection Successfully!!"), ("Information"),
                //               MessageBoxButtons.OK, MessageBoxIcon.Information,
                //                   MessageBoxDefaultButton.Button1);
            }
            else
            {
                this.regWrite(strServerName, strAuthentication, strUserName, strPassword, strDatabase);
                MessageBox.Show(("SQL Server connection fail!!"), ("Information"),
                               MessageBoxButtons.OK, MessageBoxIcon.Information,
                                   MessageBoxDefaultButton.Button1);
            }
            
        }
        #endregion

        #region 写入注册表
        public void regWrite(string strServerName, string strAuthentication, string strUserName, string strPassword, string strDatabase)
        {
            //写注册表 regedit
            RegistryKey regWrite;
            //往HKEY_CURRENT_USER主键里的Software子键下写一个名为“Test”的子键 
            //如果Test子键已经存在系统会自动覆盖它 
            regWrite = Registry.CurrentUser.CreateSubKey("Software\\ESRI\\PLUG\\UTP");
            //往UTP子键里添数据项，            
            regWrite.SetValue("strServerName", strServerName);
            regWrite.SetValue("strAuthentication", strAuthentication);
            regWrite.SetValue("strUserName", strUserName);
            regWrite.SetValue("strPassword", strPassword);
            regWrite.SetValue("strDatabase", strDatabase);
            //关闭该对象 
            regWrite.Close();
        }
        #endregion

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
                    text_ServerName.Text = regRead.GetValue("strServerName").ToString();
                }
                if (regRead.GetValue("strAuthentication") != null)
                {
                    com_Authentication.SelectedItem = regRead.GetValue("strAuthentication").ToString();
                }
                if (regRead.GetValue("strUserName") != null)
                {
                    text_UserName.Text = regRead.GetValue("strUserName").ToString();
                }
                if (regRead.GetValue("strPassword") != null)
                {
                    text_Password.Text = regRead.GetValue("strPassword").ToString();
                }
                if (regRead.GetValue("strDatabase") != null)
                {
                    ComBoxBing();
                    //com_Database.SelectedItem = regRead.GetValue("strDatabase").ToString();
                    //com_Database.SelectedValue = regRead.GetValue("strDatabase").ToString();
                    com_Database.Text= regRead.GetValue("strDatabase").ToString();
                }
            }            
        }
        #endregion

        #region 关闭
                
        private void but_Close_Click(object sender, EventArgs e)
        {
            Application.Exit();           
            //if (MessageBox.Show("Are you sure to exit?","Config",
            //            MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            //{
                
            //}
            //else
            //{
            //    Application.Exit();  
            //}
        }
        #endregion

        #region 判断连接是否成功
                
        /// <summary>
        /// 判断连接是否成功
        /// </summary>
        /// <returns>true，成功；false，失败</returns>
        public Boolean Connect(string ConnectString)
        {
            Boolean booSuccess = false;
            using (SqlConnection cn = new SqlConnection(ConnectString))
            {
                try
                {
                    cn.Open();
                    if (cn != null)
                    {
                        booSuccess = true;
                    }
                }
                catch (SqlException)
                {
                    return booSuccess;
                }
            }
            return booSuccess;
        }
        #endregion

        #region 取所有数据库名
        
        /// <summary>
        /// 查询数据，返回SqlDataReader
        /// </summary>
        /// <returns>SqlDataReader</returns>
        public DataSet GetDs(string strSql)
        {
            //获取注册表的信息
           // this.ReadInfo();
            //string strSql = "select * from ArcTest where 1=1" + strAnd + " order by orders";
            DataSet ds = null;
            SqlConnection sqlconn = null;
            try
            {
                if (strAuthentication == "SQL Server Authentication")
                {
                    sqlconn = new SqlConnection("database=master;user id=" + strUserName + ";password=" + strPassword + "; server=" + strServerName + ";");
                }
                else
                {
                    //sqlconn = new SqlConnection("database=master; Integrated Security=SSPI; Persist Security Info=False; server=" + strServerName + ";");
                    sqlconn = new SqlConnection("database=master;user id=" + strUserName + ";password=" + strPassword + "; server=" + strServerName + ";");
                }
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

        #endregion

        private void com_Database_Click(object sender, EventArgs e)
        {
            ComBoxBing();            
        }
        public void ComBoxBing()
        {
            strServerName = text_ServerName.Text;
            strAuthentication = com_Authentication.Text;
            strUserName = text_UserName.Text;
            strPassword = text_Password.Text;
            strDatabase = com_Database.Text;
            
            string ConnectString = string.Empty;
            //   sqlconn = new SqlConnection("database=JobTest;user id=sa;password=sqlserver; server=dev-db;");
            if (strAuthentication == "SQL Server Authentication")
            {
                ConnectString = "database=master;user id=" + strUserName + ";password=" + strPassword + "; server=" + strServerName + ";";
            }
            else
            {
                //ConnectString = "database=master;Integrated Security=SSPI; server=" + strServerName + ";";
                ConnectString = "database=master;user id=" + strUserName + ";password=" + strPassword + "; server=" + strServerName + ";";
            }
            if (this.Connect(ConnectString))
            {
                string strSql = "select name from master..sysdatabases";
                DataSet ds = null;
                ds = this.GetDs(strSql);
                //if (ds != null && ds.Tables[0].Rows.Count > 0)
                //{
                //    com_Database.DataSource = ds.Tables[0];
                //    com_Database.DisplayMember = "name";
                //    com_Database.ValueMember = "name";
                //}
               
                com_Database.Items.Clear();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    com_Database.Items.Insert(i, ds.Tables[0].Rows[i][0].ToString());
                }
                com_Database.Text = strDatabase;
            }
            else
            {
                ////com_Database.Items.Clear();
                //DataSet ds = null;               
               // com_Database.DataSource = Nothing;
                com_Database.Items.Clear();
                //com_Database.Refresh();
                
            }
        }

    }
}
