<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyDrugs.ascx.cs" Inherits="Site.App.Controls.Widgets.MyDrugs" %>

<%
    var user = Framework.Security.Manager.GetUser();
    var userProfile = Lib.Data.UserProfile.FindByUser(user);
    var drugCompanyUser = Lib.Data.DrugCompanyUser.FindByProfile(userProfile);
    
    var drugs = drugCompanyUser != null
        ? Lib.Data.Drug.FindAll(true, drugCompanyUser.DrugCompanyID)
        : new List<Lib.Data.Drug>();
%>

<header class="portlet-header">
    <h2>My Drugs</h2>
</header>
<section>
    <ul class="drug-list-dc">
    <% foreach (var d in drugs){ %>
        <li><a href="#admin/dsq/edit?id=<%=d.ID.Value %>" class="button">Edit</a><%=d.GenericName %></li>
    <% } %>
    </ul>
</section>