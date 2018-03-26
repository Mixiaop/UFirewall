<%@ Page Title="" Language="C#" MasterPageFile="~/Head.Master" AutoEventWireup="true" CodeBehind="VisitLogs.aspx.cs" Inherits="UFirewall.Web.VisitLogs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <% 
        var getIp = U.Utilities.Web.WebHelper.GetString("ip");
        var list = UFirewall.Current.RequestLogger.GetAll().OrderByDescending(x => x.VisitTime);%>
    <div class="js-refresh" data-name="logs">
    <div class="content">
        <div class="col-lg-12">
            <div class="underline-nav">
                <nav class="underline-nav-body">
                    <% if(getIp.IsNotNullOrEmpty()){ %>
                    <a href="#" class="underline-nav-item selected" aria-selected="false" role="tab" title="Stars"><%= getIp %></a>
                    <%}else{ %>
                    <a href="#" class="underline-nav-item selected" aria-selected="false" role="tab" title="Stars">今日实时访问 (<%= list.Count()  %>)</a>
                    <a href="VisitTop.aspx" class="underline-nav-item" aria-selected="false" role="tab" title="Stars">今日访问Top</a>
                    <% if (UFirewall.Current.Settings.IsBlacklistServer == 1)
                             { %>
                    <a href="javascript:;" onclick="page.open('SetBlacklist.aspx',800,600)" class="underline-nav-item" aria-selected="false" role="tab" title="Stars">设置黑名单</a>
                    <%}
                             } %>
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
                            <th class="text-center" width="10%">Time</th>
                            <% if (!getIp.IsNotNullOrEmpty())
                                { %>
                            <th class="text-center" width="15%">Ip</th>
                            <%} %>
                            <th class="text-center" width="30%">Url</th>
                            <th class="text-center">UserAgent</th>
                        </tr>
                    </thead>
                    
                    <% foreach(var req in (getIp.IsNotNullOrEmpty()?list.Where(x=>x.Ip == getIp).Take(20):list.Take(20))){ %>
                    <tr>
                        <td class="text-center"><%= req.VisitTime.ToDateTime().ToString("HH:mm:ss") %></td>
                        <% if (!getIp.IsNotNullOrEmpty())
                            { %>
                        <td class="text-center"><a href="javascript:;" onclick="page.open('/VisitLogs.aspx?ip=<%= req.Ip %>')"><%= req.Ip %></a></td>
                        <%} %>
                        <td ><%= req.Url %></td>
                        <td class="text-center"><%= req.UserAgent %></td>
                    </tr>
                    <%} %>
                </table>
                <% if (getIp.IsNotNullOrEmpty())
                    { %>
                <a href="javascript:;" onclick="window.location.href='AllVisitLogs.aspx?ip=<%= getIp %>'" class="btn-block btn">查看全部</a>
                <%} %>
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
