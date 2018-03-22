using System.Web;
using U.Utilities.Web;

namespace UFirewall.Web.UFirewall
{
    /// <summary>
    /// <script src="http://localhost/UFirewall/Stat.ashx"></script>
    /// </summary>
    public class Stat : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            var url = WebHelper.GetString("url");
            var ip = WebHelper.GetString("ip");
            var visitTime = WebHelper.GetString("visitTime");
            var userAgent = WebHelper.GetString("userAgent");


            var req = new WebRequest();
            if (url.IsNotNullOrEmpty()) {
                req.Url = url;
            }
            if (ip.IsNotNullOrEmpty()) {
                req.Ip = ip;
            }
            if (visitTime.IsNotNullOrEmpty()) {
                req.VisitTime = visitTime;
            }
            if (userAgent.IsNotNullOrEmpty()) {
                req.UserAgent = userAgent;
            }

            Current.RequestLogger.Log(req);
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