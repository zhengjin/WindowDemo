using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using Telerik.Web.UI;
using TelerikDemo;

namespace CustomerDirect.OnlinePayment.Manager.Maintenance
{
    public partial class AddService : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            const long MaxTotalBytes = 548576; // 500kbs
            long totalBytes;
            if (AsyncUpload1.UploadedFiles.Count > 0)
            {
                UploadedFile file = AsyncUpload1.UploadedFiles[0];
                totalBytes = file.InputStream.Length;

                if (totalBytes < MaxTotalBytes)
                {
                    byte[] fileData = new byte[totalBytes];
                    file.InputStream.Read(fileData, 0, (int)totalBytes);
                    Services serviceObj = new Services();

                    serviceObj.ServiceID = Guid.NewGuid();
                    serviceObj.ServiceName = rtxtMerchantName.Text.Trim();
                    serviceObj.Description = rtxtRegCode.Text.Trim();
                    serviceObj.Logo = fileData;

                    this.AddServiceObj(serviceObj);
                }
                else
                {
                    //大于1MB
                    
                    return;
                }
            }
            else
            {
                //大于1MB
                
                return;
            }
        }

        protected int AddServiceObj(Services serviceObj)
        {
            serviceObj.CreatedDate = DateTime.Now;
            serviceObj.LastModifiedDate = DateTime.Now;
            serviceObj.IsDeleted = false;
            int counts = 0;
            try
            {
                CDProDatabaseEntities MWDB = new CDProDatabaseEntities();
                MWDB.Services.AddObject(serviceObj);
                counts = MWDB.SaveChanges();
                if (counts > 0)
                {
                    string userHostAddress = HttpContext.Current.Request.UserHostAddress;
                    
                    return 0;
                }
            }
            catch (Exception ex)
            {
                string userHostAddress = HttpContext.Current.Request.UserHostAddress;
                
                return -1;
            }
            return 1;
        }
    }
}