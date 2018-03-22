<%@ Page Title="" Language="C#" MasterPageFile="~/Head.Master" AutoEventWireup="true" CodeBehind="AllVisitLogs.aspx.cs" Inherits="UFirewall.Web.AllVisitLogs" %>
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
                    <a href="#" class="underline-nav-item selected" aria-selected="false" role="tab" title="Stars"><%= getIp %></a>
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
                            <th class="text-center">Url</th>
                            <th class="text-center">UserAgent</th>
                        </tr>
                    </thead>
                    <% if(getIp.IsNotNullOrEmpty()){ %>
                    <% foreach(var req in list.Where(x=>x.Ip == getIp)){ %>
                    <tr>
                        <td class="text-center"><%= req.VisitTime.ToDateTime().ToString("HH:mm:ss") %></td>
                        <% if (!getIp.IsNotNullOrEmpty())
                            { %>
                        <td class="text-center"><a href="javascript:;" onclick="window.location.href='/VisitLogs.aspx?ip=<%= req.Ip %>'"><%= req.Ip %></a></td>
                        <%} %>
                        <td ><%= req.Url %></td>
                        <td class="text-center"><%= req.UserAgent %></td>
                    </tr>
                    <%} %>
                    <%} %>
                </table>
                
            </div>
        </div>
    </div>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="server">
</asp:Content>
