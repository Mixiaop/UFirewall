using System.Collections.Generic;
using System.Threading.Tasks;

namespace UFirewall
{
    public interface IWebRequestLogger {
        void Log(WebRequest req = null);

        Task LogAsync(WebRequest req = null);

        IList<WebRequest> GetAll();
    }

    public interface IWebFirewallVerifier {
        bool Authentication(WebRequest req = null);
    }

    public interface IWebFirewall : IWebFirewallVerifier
    {
        bool IpAddToWhitelist(string ip);

        bool IpAddToBlacklist(string ip);

        bool SetBlacklist(string ips);

        bool SetWhitelist(string ips);

        IList<string> IpWhitelist();

        IList<string> IpBlacklist();
    }
}
