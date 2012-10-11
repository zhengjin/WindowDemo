using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using WebDemoProject;
using System.Data.Objects;
using System.Linq;

public partial class Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ServiceDirectDBEntities objDB = new ServiceDirectDBEntities();

            ObjectQuery SizeList = objDB.tblSize.Where("it.IsDeleted=false and it.SizeType='GAUGE'");
            ObjectQuery ModelList = objDB.tblModel.Where("it.IsDeleted=false and it.ModelType='GAUGE'");
            ObjectQuery MfrList = objDB.tblManufacturer.Where("it.IsDeleted=false and it.MfrType='GAUGE'");

            foreach (tblSize objSize in SizeList)
            {
                RadComboBoxItem item = new RadComboBoxItem();

                item.Value = objSize.SizeID.ToString();
                item.Text = objSize.Size;

                cboSize.Items.Add(item);
            }

            foreach (tblModel objModel in ModelList)
            {
                RadComboBoxItem item = new RadComboBoxItem();

                item.Value = objModel.ModelID.ToString();
                item.Text = objModel.Model;

                cboModel.Items.Add(item);
            }

            foreach (tblManufacturer objManufacturer in MfrList)
            {
                RadComboBoxItem item = new RadComboBoxItem();

                item.Value = objManufacturer.MfrID.ToString();
                item.Text = objManufacturer.MfrName;

                cboManufacturer.Items.Add(item);
            }

            dtpLastCalibrationDate.SelectedDate = DateTime.Now.Date;
        }
    }

     protected void RadButton5_Click1(object sender, EventArgs e) 
    {
        ServiceDirectDBEntities objDB = new ServiceDirectDBEntities();

        tblGauge objGauge = objDB.tblGauge.First<tblGauge>(g => g.GaugeCode == txtGaugeCode.Text);

        if (objGauge != null)
        {

            cboSize.SelectedValue = objGauge.SizeID.ToString();
            cboModel.SelectedValue = objGauge.ModelID.ToString();
            cboManufacturer.SelectedValue = objGauge.MfrID.ToString();

            dtpLastCalibrationDate.SelectedDate = objGauge.LastCalibrationDate;
            hfGaugeID.Value = objGauge.GaugeID.ToString();
        }
    }

    protected void RadButton4_Click(object sender, EventArgs e)
    {
        txtGaugeCode.Text = "";
        dtpLastCalibrationDate.SelectedDate = DateTime.Now.Date;
    }

    protected void RadButton3_Click(object sender, EventArgs e)
    {
        ServiceDirectDBEntities objDB = new ServiceDirectDBEntities();
        tblGauge objGauge;

        if (hfGaugeID.Value != string.Empty)
        {
            objGauge = objDB.tblGauge.First<tblGauge>(g => g.GaugeID == Guid.Parse(hfGaugeID.Value));
        }
        else
        {
            objGauge = objDB.tblGauge.CreateObject();
        }

        if (objGauge.GaugeID==Guid.Empty)
        {
            objGauge.GaugeID = Guid.NewGuid();
            objGauge.GaugeCode = txtGaugeCode.Text;
            objGauge.SizeID =Guid.Parse(cboSize.SelectedValue);
            objGauge.ModelID = Guid.Parse(cboModel.SelectedValue);
            objGauge.MfrID = Guid.Parse(cboManufacturer.SelectedValue);
            objGauge.LastCalibrationDate = dtpLastCalibrationDate.SelectedDate;

            objGauge.LastModifiedDate = DateTime.Now;
            objGauge.CreatedDate = DateTime.Now;

            objDB.tblGauge.AddObject(objGauge);
        }
        else
        {
            objGauge.GaugeCode = txtGaugeCode.Text;
            objGauge.SizeID = Guid.Parse(cboSize.SelectedValue);
            objGauge.ModelID = Guid.Parse(cboModel.SelectedValue);
            objGauge.MfrID = Guid.Parse(cboManufacturer.SelectedValue);
            objGauge.LastCalibrationDate = dtpLastCalibrationDate.SelectedDate;

            objGauge.LastModifiedDate = DateTime.Now;

        }

        objDB.SaveChanges();
    }

    protected void btnNewGauge_Click(object sender, EventArgs e)
    {
        hfGaugeID.Value = string.Empty;

        txtGaugeCode.Text = string.Empty;
    }

    protected void btnPrintGauge_Click(object sender, EventArgs e)
    {
        Response.Redirect("GaugeReport.aspx");
    }

}
