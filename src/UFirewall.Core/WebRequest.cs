using System;
using U.Utilities.Web;

namespace UFirewall
{
    public class WebRequest
    {
        public WebRequest() {
            Url = WebHelper.GetThisPageUrl(true);
            Ip = WebHelper.GetIP();
            UserAgent = WebHelper.GetUserAgent();
            VisitTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public string Url { get; set; }

        public string Ip { get; set; }

        public string UserAgent { get; set; }

        public string VisitTime { get; set; }
    }
}
