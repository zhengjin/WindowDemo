using System;

using System.Linq;
using System.Collections.Generic;
using WCF_Demo;
using WCF_Demo.UTPDataProxyServiceReference;
using System.Data.Services.Client;

public partial class Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadUtilityCompanyList();
        }
    }

    private void LoadUtilityCompanyList()
    {
        using (WCFDemoDBEntities objDB = new WCFDemoDBEntities())
        {
            List<UtilityCompany> uclist = objDB.UtilityCompany.OrderBy("it.CompanyName").ToList<UtilityCompany>();

            if (uclist != null)
            {
                cboCompany.DataValueField = "ID";
                cboCompany.DataTextField = "CompanyName";

                cboCompany.DataSource = uclist;
                cboCompany.DataBind();
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string UCUri = GetUCUriByID(Guid.Parse(cboCompany.SelectedValue));

        if (UCUri != string.Empty)
        {
            Customer c = GetCustomerbyAccount(txtAccountNum.Text, UCUri);

            if (c != null)
            {
                txtCustomerName.Text = c.CustomerName;
                txtCurrentDue.Text = c.CurrentDue.ToString();
                txtTotalDue.Text = c.TotalDue.ToString();
                txtLastName.Text = c.LastName;
                txtZip.Text = c.Zip;
            }
        }
    }

    private string GetUCUriByID(Guid UCID)
    {
        using (WCFDemoDBEntities objDB = new WCFDemoDBEntities())
        {
            UtilityCompany uc = objDB.UtilityCompany.FirstOrDefault<UtilityCompany>(u => u.ID == UCID);

            if (uc != null)
                return uc.WCFUri;
            else
                return string.Empty;
        }
    }

    private Customer GetCustomerbyAccount(string AcctNum, string UCUri)
    {
        Uri RemoteUri = new Uri(UCUri);
        Customer tmpCustomer = new Customer();

        UTPDataEntities objRemoteDB = new UTPDataEntities(RemoteUri);
        
        DataServiceQuery<View_CustomerInformation> selectCustList =
            objRemoteDB.View_CustomerInformation.AddQueryOption("$filter", "AcctNum eq '" + AcctNum + "'");
        try
            {

                foreach (View_CustomerInformation vc in selectCustList)
                {
                    tmpCustomer.CustomerName = vc.FirstName;
                    tmpCustomer.LastName = vc.LastName;
                    tmpCustomer.CurrentDue = Convert.ToDecimal(vc.CurrDue);
                    tmpCustomer.TotalDue =Convert.ToDecimal(vc.TotalDue);
                }

                return tmpCustomer;
            }
            catch (Exception ex)
            {
                return null;
            }
        
    }

    protected void rbtnSave_Click(object sender, EventArgs e)
    {

    }
}
