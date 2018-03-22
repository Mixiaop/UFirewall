using System.Collections.Generic;
using System.Threading.Tasks;

namespace UFirewall
{
    public interface IWebFirewallLogger {
        void Log(WebRequest req = null);

        Task LogAsync(WebRequest req = null);

        IList<WebRequest> GetAll();
    }

    public interface IWebFirewallKiller {
        bool Authentication(WebRequest req = null);
    }

    public interface IWebFirewall : IWebFirewallKiller
    {
        bool IpAddToWhitelist(string ip);

        bool IpAddToBlacklist(string ip);

        bool SetBlacklist(string ips);

        bool SetWhitelist(string ips);

        IList<string> IpWhitelist();

        IList<string> IpBlacklist();
    }
}
