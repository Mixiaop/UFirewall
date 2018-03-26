<%@ Page Title="" Language="C#" MasterPageFile="~/Head.Master" AutoEventWireup="true" CodeBehind="VisitTop.aspx.cs" Inherits="UFirewall.Web.VisitTop" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <%
        var blacklist = UFirewall.Current.Firewall.IpBlacklist();
        var list = UFirewall.Current.RequestLogger.GetAll();
        var result = list.GroupBy(x => x.Ip).OrderByDescending(x => x.Count()).Take(40);
        %>
    <div class="js-refresh" data-name="logs">
    <div class="content">
        <div class="col-lg-12">
            <div class="underline-nav">
                <nav class="underline-nav-body">
                    <a href="VisitLogs.aspx" class="underline-nav-item" aria-selected="false" role="tab" title="Stars">今日实时访问 (<%= list.Count()  %>)</a>
                    <a href="#" class="underline-nav-item selected" aria-selected="false" role="tab" title="Stars">今日访问Top</a>
                    <% if (UFirewall.Current.Settings.IsBlacklistServer == 1)
                             { %>
                    
                    <a href="javascript:;" onclick="page.open('SetBlacklist.aspx',800,600)" class="underline-nav-item" aria-selected="false" role="tab" title="Stars">设置黑名单</a>
                    <%}%>
                </nav>
            </div>
        </div>
    </div>
    <div class="clearfix"></div>
    <div class="content" >
        <div class="block">
            <div class="block-content table-responsive">
                <table class="table table-hover js-dataTable-full">
                    <thead>
                        <tr>
                            <th class="text-center" width="10%">Ip</th>
                            <th class="text-center">Count</th>
                        </tr>
                    </thead>
                    
                    <% foreach(var req in result){ %>
                    <tr>
                        <td ><a href="javascript:;" onclick="page.open('/VisitLogs.aspx?ip=<%= req.Key %>')"><%= req.Key %></a> <% if(blacklist.Contains(req.Key)){ %><label class="label bg-black">b</label><%} %></td>
                        <td class="text-center"><%= req.Count() %></td>
                    </tr>
                    <%} %>
                </table>
            </div>
        </div>
    </div>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
    <script>
        var vc = {};
        vc.options = {
            refreshTime: 2 * 1000
        };
        if (isNaN(vc.options.refreshTime)) {
            vc.options.refreshTime = 0;
        }

        vc.modules = {};

        vc.modules.refresh = function () {
            var _refreshData = function () {

                if (vc.options.refreshTime > 0) {
                    $.ajax(window.location.href, {
                        cache: false
                    }).done(function (html) {
                        var $res = $(html);
                        $('.js-refresh[data-name]').html($res.find('.js-refresh[data-name]').html());
                        if (vc.options.refreshTime > 0) {
                            setTimeout(_refreshData, vc.options.refreshTime);
                        }
                    })
                        .fail(function () {
                            console.log('Failed to refresh', this, arguments);
                        });
                }

            }

            return {
                init: function () { _refreshData(); }
            }
        }();


        vc.modules.refresh.init();

      
    </script>
</asp:Content>
