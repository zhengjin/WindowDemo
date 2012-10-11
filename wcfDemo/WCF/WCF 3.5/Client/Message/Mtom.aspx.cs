using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.ServiceModel.Channels;
using System.IO;

public partial class Message_Mtom : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        var proxy = new MessageSvc.Mtom.MtomClient();

        var length = file.PostedFile.ContentLength;
        var bytes = new byte[length];
        file.PostedFile.InputStream.Read(bytes, 0, length);

        try
        {
            proxy.UploadFile(
                txtDestination.Text + Path.GetFileName(file.PostedFile.FileName), 
                bytes);
            Page.ClientScript.RegisterStartupScript(typeof(Page), "js", "alert('上传成功');", true);
        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(typeof(Page), "js", "alert('" + ex.ToString() + "');", true);
        }

        proxy.Close();
    }
}
