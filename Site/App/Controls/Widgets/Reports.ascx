<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Reports.ascx.cs" Inherits="Site.App.Controls.Widgets.Reports" %>

<%
var reports = Lib.Systems.Reports.GetMyReports();
%>

<header class="portlet-header">
    <h2>Reports</h2>
</header>
<section>
    <%if( reports.Count > 0){%>
    <ul class="dashboard-links">
        <% foreach (var r in reports){ %>
        <li><a href="#reports/view?id=<%=r.ID.Value %>" class="button">View</a> <%=r.Name %></li>
        <% } %>
    </ul>
    <%}else{ %>
    <span>You do not have any reports</span>
    <%} %>
</section>