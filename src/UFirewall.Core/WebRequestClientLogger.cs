using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using U.Utilities.Net;

namespace UFirewall
{
    public class WebRequestClientLogger  : IWebRequestLogger
    {
        public void Log(WebRequest req = null) {
            if (req == null)
                req = new WebRequest();

            if(Current.Settings.ServerHost.IsNotNullOrEmpty())
            {
                if (Current.Settings.ClientUsedLB == 1)
                {
                    req.Ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                }

                var url = Current.Settings.ServerHost + (Current.Settings.ServerHost.EndsWith("/") ? "" : "/") + "UFirewall/Stat.ashx";
                Dictionary<string, string> formData = new Dictionary<string, string>();
                formData.Add("url", req.Url);
                formData.Add("ip", req.Ip);
                formData.Add("userAgent", req.UserAgent);
                formData.Add("visitTime", req.VisitTime);

                WebRequestHelper.HttpPost(url, formData);
            }
        }

        public Task LogAsync(WebRequest req = null) {
            Log(req);
            return Task.FromResult(0);
        }

        public IList<WebRequest> GetAll() {
            throw new NotImplementedException();
        }
    }
}
