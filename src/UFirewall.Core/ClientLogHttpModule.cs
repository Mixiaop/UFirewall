using System;
using System.Web;
using U.Utilities.Web;

namespace UFirewall
{
    public class ClientLogHttpModule : System.Web.IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += Context_BeginRequest;
            context.Error += Context_Error;
        }

        private void Context_BeginRequest(object sender, EventArgs e)
        {
            try
            {
                HttpApplication application = (HttpApplication)sender;
                HttpContext context = application.Context;
                var url = WebHelper.GetThisPageUrl(true);
                if (!url.ToLower().Contains(".css") &&
                    !url.ToLower().Contains(".js") &&
                    !url.ToLower().Contains(".jpg") &&
                    !url.ToLower().Contains(".jpeg") &&
                    !url.ToLower().Contains(".gif") &&
                    !url.ToLower().Contains(".png") &&
                    !url.ToLower().Contains(".ashx") &&
                    !url.ToLower().Contains(".woff") &&
                    !url.ToLower().Contains(".ttf") &&
                    !url.ToLower().Contains(".ico"))
                {

                    if (Current.IsClient)
                    {
                        new WebRequestClientLogger().LogAsync();
                    }
                    else
                    {
                        new WebFirewallFileStore().LogAsync();
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void Context_Error(object sender, EventArgs e)
        {

        }

        public virtual void Dispose()
        {

        }
    }
}
