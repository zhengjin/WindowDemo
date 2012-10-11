using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ArcGisDemo
{
    public partial class Demo01 : Form
    {
        public Demo01()
        {
            InitializeComponent();
        }

        private void Demo01_Load(object sender, EventArgs e)
        {
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            DataSet ds = null;
            SqlConnection sqlconn = new SqlConnection("database=JobTest;user id=sa;password=sqlserver; server=dev-db;");
            SqlCommand insertcomm = new SqlCommand("select * from ArcTest", sqlconn);
            sqlconn.Open(); //打开与数据库的连接
            SqlDataReader sqlread = insertcomm.ExecuteReader(); //发送命令到数据库

            while (sqlread.Read()) //前进一条纪录
            {
                int i = sqlread.GetInt32(0); //得到当前行的第0格中的数据
                //Decimal n = sqlread.GetDecimal(1);//得到当前行的第1格中
                double n = sqlread.GetDouble(1);//得到当前行的第1格中
            }
            sqlread.Close();
            sqlconn.Close(); //关闭与数据库的连接
        }
    }
}
