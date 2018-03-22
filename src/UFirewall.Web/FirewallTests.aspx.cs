using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace UFirewall.Web
{
    public partial class FirewallTests : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write(JsonConvert.SerializeObject(Current.Settings));
            //Response.Write(JsonConvert.SerializeObject(Current.Request.GetAll()));

            var list = Current.RequestLogger.GetAll().GroupBy(x => x.Ip).OrderByDescending(x => x.Count());
            foreach (var item in list) {
                var a = item.Key;
                var b = item.Count();
                var d = 1;
            }
            var c = 1;
            //Response.Write("hello world");

        }
    }
}