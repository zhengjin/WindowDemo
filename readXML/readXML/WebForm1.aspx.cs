using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Xml;

namespace readXML
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string xmlPath = Server.MapPath("/MyTest1.xml");

            using (XmlTextReader reader = new XmlTextReader(xmlPath))
            {
                while(reader.Read())
                {
                    //节点开始
                    if(reader.NodeType==XmlNodeType.Element)
                    {
                        string elementName = reader.Name;
                        if (elementName == "tblCustomer")
                        {
                            string CustomerID = reader["CustomerID"];
                            string AcctNum = reader["AcctNum"];
                            string PremiseID = reader["PremiseID"];
                            string TotalDue = reader["TotalDue"];
                        }
                    }
                }
            }
        }
    }
}