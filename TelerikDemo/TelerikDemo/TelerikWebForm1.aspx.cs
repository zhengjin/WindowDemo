using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using TelerikDemo;

using System.Linq;

public partial class TelerikWebForm1 : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            DataTable dt = new DataTable();
            DataColumn Payment = new DataColumn("Image");
            Payment.DataType = typeof(byte[]);
            dt.Columns.Add(Payment);
            Payment = new DataColumn("Name");
            Payment.DataType = typeof(string);
            dt.Columns.Add(Payment);
            Payment = new DataColumn("serviceId");
            Payment.DataType = typeof(string);
            dt.Columns.Add(Payment);
            List<Services> serviceList=this.GetChargeItem();
            foreach (var item in serviceList)
            {
                dt.Rows.Add(new object[] { item.Logo, item.ServiceName,item.ServiceID.ToString() });
            }
            thumbRotator.DataSource = dt;
            thumbRotator.DataBind();
        }
    }

    public virtual List<Services> GetChargeItem()
    {
        try
        {
            using (CDProDatabaseEntities objDB = new CDProDatabaseEntities())
            {
                List<Services> chargeItemList = objDB.Services.Where(it => it.Logo != null).ToList();
                if (chargeItemList.Count > 0)
                {
                    return chargeItemList;
                }
            }
        }
        catch (Exception)
        {
            return null;
        }
        return null;
    }
}
