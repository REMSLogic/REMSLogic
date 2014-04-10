<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrganizationSummary.ascx.cs" Inherits="Site.App.Controls.Widgets.OrganizationSummary" %>

<%var p = Lib.Data.Provider.FindByUser(Lib.Data.ProviderUser.FindByProfile(Lib.Data.UserProfile.FindByUser(Framework.Security.Manager.GetUser())));%>

<header class="portlet-header">
    <a href="#provider/prescribers/create" class="button" style="float: right; margin-top: -8px; margin-right: -10px;">Add Prescriber</a>
    <h2>Organization Summary</h2>
</header>
<section>
    <table class="full">
        <tbody>
            <tr>
                <td>Total Prescribers</td>
                <td class="ar"><%=p.GetNumPrescribers()%></td>
            </tr>
            <tr>
                <td>Total Facilities</td>
                <td class="ar"><%=p.GetNumFacilities()%></td>
            </tr>
        </tbody>
    </table>
</section>