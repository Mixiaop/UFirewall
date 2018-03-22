using System;

namespace UFirewall.Web.Infrastructure
{
    public class PageBase : System.Web.UI.Page
    {
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);


        }

        #region Method
        #region Alert

        public string AlertSuccess(string message, string title = "", int timeoutByClose = 0)
        {
            return GetMessage(1, title, message, timeoutByClose);
        }

        public string AlertError(string message, string title = "", int timeoutByClose = 0)
        {
            return GetMessage(2, title, message, timeoutByClose);
        }

        /// <summary>
        /// 获取消息HTML
        /// </summary>
        /// <param name="type">1-成功,2-错误,3-警告</param>
        /// <param name="message"></param>
        /// <param name="timeClose">1秒=1000</param>
        /// <returns></returns>
        private string GetMessage(int type, string title, string message, int timeClose)
        {
            string html = "";
            switch (type)
            {
                case 1:
                    html = "<div class=\"alert alert-success\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\">&times;</button>" + (title != "" ? "<strong>" + title + "</strong> " : "") + "" + message + "</div>";
                    break;
                case 2:
                    html = "<div class=\"alert alert-danger\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\">&times;</button>" + (title != "" ? "<strong>" + title + "</strong> " : "") + "" + message + "</div>";
                    break;
                case 3:
                    html = "<div class=\"alert alert-warning\"><button type=\"button\" class=\"close\" data-dismiss=\"alert\">&times;</button>" + (title != "" ? "<strong>" + title + "</strong> " : "") + "" + message + "</div>";
                    break;
            }

            if (timeClose > 0)
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "alert-hidden", "<script>setTimeout(function(){$('.alert').remove();}," + timeClose + ");</script>");
            }

            return html;
        }
        #endregion

        /// <summary>
        /// 客服端延时跳转
        /// </summary>
        /// <param name="url">跳转地址</param>
        /// <param name="time">时间1000:1秒</param>
        public void RedirectByTime(string url, int time)
        {
            this.ClientScript.RegisterStartupScript(this.GetType(), "RedirectByTime", "<script>setTimeout(function(){window.location='" + url + "';}," + time + ");</script>");
        }

        protected void RegisterScripts(string key, string scripts)
        {
            this.ClientScript.RegisterStartupScript(GetType(), key, scripts, true);
        }

        /// <summary>
        /// 替换Url参数值
        /// </summary>
        /// <param name="param">参数,例：node=</param>
        /// <param name="rep">替换字符串,例：node=1</param>
        /// <param name="url">Url地址字符串</param>
        /// <returns>string</returns>
        protected string ChangeUrlParam(string param, string rep, string url)
        {
            var szUrl = url;
            if (szUrl.IndexOf(param) == -1)
            {
                if (szUrl.IndexOf("?") == -1)
                {
                    szUrl += "?";
                }
                else
                {
                    szUrl += "&";
                }
                szUrl += rep;
            }
            else
            {
                szUrl = System.Text.RegularExpressions.Regex.Replace(szUrl, param + @"[\d]{0,10}", rep);
            }
            return szUrl;
        }
        #endregion
    }
}