using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UFirewall.Web
{
    public partial class SetBlacklist : Infrastructure.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSave.Click += BtnSave_Click;
            if (!IsPostBack) {
                var list = Current.Firewall.IpBlacklist();
                tbBlacklist.Text = list.Concatenate(x => x + ",").Trim();
                tbBlacklist.Text = tbBlacklist.Text.TrimEnd(",");
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Current.Firewall.SetBlacklist(tbBlacklist.Text.Trim());
            ltlMessage.Text = AlertSuccess("保存成功");
        }
    }
}