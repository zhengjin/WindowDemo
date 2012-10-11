using System.Collections.Generic;
using System.Reflection;
using System.Web;
using JQueryUI.App_Code;
using Newtonsoft.Json;

namespace JQueryUI
{
    /// <summary>
    /// json 的摘要说明
    /// </summary>
    public class json : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Request["Method"] != null)
            {
                string method = context.Request["Method"].ToString();
                MethodInfo methodInfo = this.GetType().GetMethod(method);
                object[] args = new object[1];
                args[0] = context;
                methodInfo.Invoke(this, args);
            }
            else
            {
                context.Response.Write("");   
            }
        }

        public void Init(HttpContext context)
        {
            List<ItemCenterQuery> itemlist = new List<ItemCenterQuery>();

            ItemCenterQuery item = new ItemCenterQuery();
            item.CCode = "21";
            item.CName = "辽宁";
            item.GmZz = 21.1141M;
            item.GmZz_Itemcodes = "2101002121420,2112151455021";
            item.GmNT = 22.1141M;
            item.GmNT_Itemcodes = "2101012120,21121255021";
            item.GmCJ = 23.1141M;
            item.GmCJ_itemcodes = "2101121220,211121255021";

            ItemCenterQuery child = new ItemCenterQuery();
            child.CCode = "2103";
            child.CName = "鞍山";
            child.GmZz = 21.1141M;
            child.GmZz_Itemcodes = "2101002121420,2112151455021";
            child.GmNT = 22.1141M;
            child.GmNT_Itemcodes = "2101012120,21121255021";
            child.GmCJ = 23.1141M;
            child.GmCJ_itemcodes = "2101121220,211121255021";
            child.state = "closed";

            item.children.Add(child);
            itemlist.Add(item);

            context.Response.Write(JsonConvert.SerializeObject(itemlist));
        }

        public void GetChildrenData(HttpContext context)
        {
            List<ItemCenterQuery> itemlist = new List<ItemCenterQuery>();

            ItemCenterQuery childSecond = new ItemCenterQuery();
            childSecond.CCode = "210321";
            childSecond.CName = "台安县";
            childSecond.GmZz = 21.1141M;
            childSecond.GmZz_Itemcodes = "2101002121420,2112151455021";
            childSecond.GmNT = 22.1141M;
            childSecond.GmNT_Itemcodes = "2101012120,21121255021";
            childSecond.GmCJ = 23.1141M;
            childSecond.GmCJ_itemcodes = "2101121220,211121255021";

            itemlist.Add(childSecond);

            context.Response.Write(JsonConvert.SerializeObject(itemlist));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}