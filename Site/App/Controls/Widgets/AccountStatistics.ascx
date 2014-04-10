<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccountStatistics.ascx.cs" Inherits="Site.App.Controls.Widgets.AccountStatistics" %>

<header class="portlet-header">
    <h2>Account Statistics</h2>
</header>
<section>
    <table class="full">
        <tbody>
            <tr>
                <td>Total Prescribers</td>
                <td class="ar"><%=Lib.Data.Prescriber.GetTotalCount() %></td>
            </tr>
            <tr>
                <td>Recent Prescriber Signups</td>
                <td class="ar"><%=Lib.Data.Prescriber.GetRecentCount() %></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="ar">&nbsp;</td>
            </tr>
            <tr>
                <td>Total Providers</td>
                <td class="ar"><%=Lib.Data.Provider.GetTotalCount() %></td>
            </tr>
            <tr>
                <td>Recent Provider Signups</td>
                <td class="ar"><%=Lib.Data.Provider.GetRecentCount() %></td>
            </tr>
        </tbody>
    </table>
</section>