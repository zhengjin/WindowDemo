using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using System.Data;
using MyConfiguration.CDService;

namespace MyConfiguration
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MerchantService ms = new MerchantService();
            DataSet ds = new DataSet();
            ds=ms.GetNewMerchant();
            if(ds!=null)
            {
                string code = ds.Tables[0].Rows[0]["MerchantCode"].ToString();
                
                ds.Tables[0].Rows[0]["MerchantPassword"] = "Pwd";
                ds.Tables[0].Rows[0]["MerchantName"] = "Name";
                ds.Tables[0].Rows[0]["MerchantID"] = code;
                ds.Tables[0].Rows[0]["BuildDate"] = DateTime.Now;
                ds.Tables[0].Rows[0]["EffectiveDays"] = 0;
                ds.Tables[0].Rows[0]["Shutoff"] = false;

                string pwd = ds.Tables[0].Rows[0]["MerchantPassword"].ToString();
                string name = ds.Tables[0].Rows[0]["MerchantName"].ToString();
                string id = ds.Tables[0].Rows[0]["MerchantID"].ToString();
                Boolean temp=ms.Join(ds);
            }
        }
    }
}
