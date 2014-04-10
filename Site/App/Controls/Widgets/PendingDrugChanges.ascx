<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PendingDrugChanges.ascx.cs" Inherits="Site.App.Controls.Widgets.PendingDrugChanges" %>

<% var pending_changes = Lib.Data.Drug.FindPending(); %>

<header class="portlet-header">
    <h2>Pending Drug Changes</h2>
</header>
<section>
<% if (pending_changes == null || pending_changes.Count <= 0)
{ %>
    <h5>No Pending Changes</h5>
<% }
else
{ %>
    <ul class="dashboard-links">
    <% foreach (var d in pending_changes)
{ %>
        <li><a href="#admin/dsq/edit?id=<%=d.ID.Value %>" class="button">View</a> <%=d.GenericName %></li>
    <% } %>
    </ul>
<% } %>
</section>