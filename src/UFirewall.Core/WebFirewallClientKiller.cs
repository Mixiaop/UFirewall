﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web;
using U.Utilities.Net;

namespace UFirewall
{
    public class WebFirewallClientKiller : IWebFirewallKiller
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
                if (Current.Settings.BlacklistSoaUrl.IsNotNullOrEmpty()) {
                    url = Current.Settings.BlacklistSoaUrl;
                }
                Dictionary<string, string> formData = new Dictionary<string, string>();

                var res = WebRequestHelper.HttpPost(url, formData, null, null, null, null, null, null, 500);

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
