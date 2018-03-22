using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web;
using U.Utilities.Net;

namespace UFirewall
{
    public class WebFirewallClientVerifier : IWebFirewallVerifier
    {
        public bool Authentication(WebRequest req = null)
        {
            if (req == null)
                req = new WebRequest();

            if (Current.Settings.ServerHost.IsNotNullOrEmpty())
            {
                if (Current.Settings.ClientUsedLB == 1)
                {
                    req.Ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                }

                var url = Current.Settings.ServerHost + (Current.Settings.ServerHost.EndsWith("/") ? "" : "/") + "UFirewall/IpBlacklist.ashx";
                Dictionary<string, string> formData = new Dictionary<string, string>();

                var res = WebRequestHelper.HttpPost(url, formData);

                var blacklist = JsonConvert.DeserializeObject<List<string>>(res);
                if (blacklist != null && blacklist.Count > 0 && (blacklist.Contains(req.Ip)))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
