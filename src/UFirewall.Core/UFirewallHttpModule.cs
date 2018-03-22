using System;
using System.Web;

namespace UFirewall
{
    public class UFirewallHttpModule : System.Web.IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += Context_BeginRequest;
            context.Error += Context_Error;
        }

        private void Context_BeginRequest(object sender, EventArgs e)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpContext context = application.Context;
            if (Current.IsClient)
            {
                if (!Current.ClientKiller.Authentication())
                {
                    context.Response.Write("access denied");
                    context.Response.End();
                }
            }
            else {
                if (!Current.Firewall.Authentication()) {
                    context.Response.Write("access denied");
                    context.Response.End();
                }
            }
        }

        private void Context_Error(object sender, EventArgs e)
        {
            
        }

        public virtual void Dispose()
        {
            
        }
    }
}
