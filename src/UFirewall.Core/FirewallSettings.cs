using System;
using System.IO;
using U.Utilities.Web;
using Newtonsoft.Json;

namespace UFirewall
{
    public class FirewallSettings
    {
        /// <summary>
        /// 是否使用远程防火墙（1=remote, 0=local）
        /// </summary>
        public int IsClient { get; set; }

        public int ClientUsedLB { get; set; }

        public string ServerHost { get; set; }

        public string IpBlacklistPath { get; set; }

        public string IpWhitelistPath { get; set; }

        public string ErrorLogPath { get; set; }

        public string VisitLogPath { get; set; }

        public IpRule Rule { get; set; }

        public class IpRule
        {
            /// <summary>
            /// 每多少秒
            /// </summary>
            public int PerSecond { get; set; }

            /// <summary>
            /// 多少请求
            /// </summary>
            public int RequestsCount { get; set; }
        }

        public static FirewallSettings GetSettings()
        {
            FirewallSettings settings = null;
            var filePath = WebHelper.MapPath("/Config/UFirewall/FirewallSettings.json");
            if (File.Exists(filePath))
            {
                var fileData = File.ReadAllText(filePath);
                var jsonObj = JsonConvert.DeserializeObject(fileData);
                settings = JsonConvert.DeserializeObject<FirewallSettings>(jsonObj.ToString());

                //ensure folder already exists
                if (!Directory.Exists(WebHelper.MapPath(settings.ErrorLogPath))) {
                    Directory.CreateDirectory(WebHelper.MapPath(settings.ErrorLogPath));
                }

                if (!Directory.Exists(WebHelper.MapPath(settings.VisitLogPath))) {
                    Directory.CreateDirectory(WebHelper.MapPath(settings.VisitLogPath));
                }

                if (!Directory.Exists(WebHelper.MapPath(settings.IpBlacklistPath)))
                {
                    Directory.CreateDirectory(WebHelper.MapPath(settings.IpBlacklistPath));
                }

                if (!Directory.Exists(WebHelper.MapPath(settings.IpWhitelistPath)))
                {
                    Directory.CreateDirectory(WebHelper.MapPath(settings.IpWhitelistPath));
                }
            }
            else
            {
                throw new Exception("出错了：未找到配置文件" + filePath);
            }

            return settings;
        }
    }
}
