using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using U.Utilities.Web;

namespace UFirewall
{
    public class WebFirewallFileStore : IWebFirewall, IWebFirewallLogger
    {
        public bool Authentication(WebRequest req = null)
        {
            if (req == null)
                req = new WebRequest();

            if (IpBlacklist().Contains(req.Ip)) {
                return false;
            }

            return true;
        }

        public bool IpAddToBlacklist(string ip)
        {
            try
            {
                if (!IpBlacklist().Contains(ip))
                {
                    using (FileStream fs = new FileStream(GetBlacklistPath(), FileMode.Append, FileAccess.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            sw.Write("," + ip);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Current.LogError(ex);
                return false;
            }
        }

        public bool IpAddToWhitelist(string ip)
        {
            return false;
        }

        public bool SetBlacklist(string ips) {
            try
            {
                if (ips.IsNotNullOrEmpty())
                {
                    using (FileStream fs = new FileStream(GetBlacklistPath(), FileMode.Create, FileAccess.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            sw.Write(ips);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Current.LogError(ex);
                return false;
            }
        }

        public bool SetWhitelist(string ips) {
            return false;
        }

        public IList<string> IpBlacklist()
        {
            var list = new List<string>();

            try
            {
                using (FileStream fs = new FileStream(GetBlacklistPath(), FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string content = sr.ReadToEnd();
                        if (content.IsNotNullOrEmpty()) {
                            var ipList = content.Split(",");
                            if (ipList != null) {
                                foreach (var s in ipList) {
                                    if (s.Trim().IsNotNullOrEmpty())
                                        list.Add(s);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Current.LogError(ex);
            }
            return list;
        }

        public IList<string> IpWhitelist()
        {
            var list = new List<string>();
            return list;
        }

        Task AnalysesIpAsync(WebRequest req) {
            if (req != null) {
                var count = GetAll().Where(x => x.Ip == req.Ip &&
                                          x.VisitTime.ToDateTime() >= req.VisitTime.ToDateTime().AddSeconds(-(Current.Settings.Rule.PerSecond)) &&
                                          x.VisitTime.ToDateTime() <= req.VisitTime.ToDateTime()).Count();

                if (count > Current.Settings.Rule.RequestsCount) {
                    IpAddToBlacklist(req.Ip);
                }

            }

            return Task.FromResult(0);
        }

        #region Request
        public void Log(WebRequest req = null)
        {
            if (req == null)
                req = new WebRequest();
            try
            {
                using (FileStream fs = new FileStream(GetFilePath(), FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(JsonConvert.SerializeObject(req));
                    }
                }

                AnalysesIpAsync(req);
            }
            catch (Exception ex) {
                Current.LogError(ex);
            }
        }

        public Task LogAsync(WebRequest req = null)
        {
            Log(req);

            return Task.FromResult(0);
        }

        public IList<WebRequest> GetAll() {
            IList<WebRequest> list = new List<WebRequest>();
            try
            {
                using (FileStream fs = new FileStream(GetFilePath(), FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string row = sr.ReadLine();
                        while (row != null){
                            list.Add(JsonConvert.DeserializeObject<WebRequest>(row));
                            row = sr.ReadLine();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Current.LogError(ex);
            }

            return list;
        }
        #endregion

        string GetFilePath()
        {
            var filePath = WebHelper.MapPath(Current.Settings.VisitLogPath +
                                            (!Current.Settings.VisitLogPath.EndsWith("/") ? "/" : "") +
                                            string.Format("visit_{0}.txt", DateTime.Now.ToString("yyyyMMdd")));
            if (!File.Exists(filePath))
            {
                var file = File.Create(filePath);
                file.Close();
            }

            return filePath;
        }

        string GetBlacklistPath() {
            var filePath = WebHelper.MapPath(Current.Settings.IpBlacklistPath +
                                            (!Current.Settings.IpBlacklistPath.EndsWith("/") ? "/" : "") +
                                            string.Format("blacklist_{0}.txt", DateTime.Now.ToString("yyyyMMdd")));
            if (!File.Exists(filePath))
            {
                var file = File.Create(filePath);
                file.Close();
            }

            return filePath;
        }

        string GetWhitelistPath()
        {
            var filePath = WebHelper.MapPath(Current.Settings.IpWhitelistPath +
                                            (!Current.Settings.IpWhitelistPath.EndsWith("/") ? "/" : "") +
                                            string.Format("whitelist_{0}.txt", DateTime.Now.ToString("yyyyMMdd")));
            if (!File.Exists(filePath))
            {
                var file = File.Create(filePath);
                file.Close();
            }

            return filePath;
        }
    }
}
