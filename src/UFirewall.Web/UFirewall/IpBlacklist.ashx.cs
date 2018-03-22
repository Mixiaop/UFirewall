using Newtonsoft.Json;
using System.Web;

namespace UFirewall.Web.UFirewall
{
    public class IpBlacklist : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Write(JsonConvert.SerializeObject(Current.Firewall.IpBlacklist()));
            context.Response.End();
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