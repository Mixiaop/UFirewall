using System;
using System.IO;

namespace UFirewall
{
    public class Current
    {
        private static IWebFirewall _firewall  = new WebFirewallFileStore();
        public static IWebFirewall Firewall => _firewall;

        private static IWebRequestLogger _request = new WebFirewallFileStore();
        public static IWebRequestLogger RequestLogger => _request;

        private static IWebFirewallVerifier _verifier = new WebFirewallClientVerifier();
        public static IWebFirewallVerifier ClientVerifier => _verifier;

        public static FirewallSettings Settings => FirewallSettings.GetSettings();

        public static void LogError(Exception ex) {
            string path = U.Utilities.Web.WebHelper.MapPath(Path.Combine(Settings.ErrorLogPath, string.Format("error_{0}.txt", DateTime.Now.ToString("yyyyMMdd"))));
            FileInfo file = new FileInfo(path);
            if (!file.Exists)
            {
                FileStream fs;
                fs = File.Create(path);
                fs.Close();
            }

            using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "   " + ex.Message);
                    sw.WriteLine(ex.StackTrace.ToString());
                    sw.WriteLine();
                }
            }
        }
    }
}
