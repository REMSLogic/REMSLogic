<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PrescriberUpdates.ascx.cs" Inherits="Site.App.Controls.Widgets.PrescriberUpdates" %>

<header class="portlet-header">
    <h2>Prescriber Updates</h2>
</header>
<section>
    <div class="provider-prescriber-updates">
    <%if(PrescriberUpdateItems != null && PrescriberUpdateItems.Count > 0){
        foreach (var pu in PrescriberUpdateItems){%>
        <div class="update-row">
            <span><%=pu.DateCreated.ToShortDateString()%></span><span> - <%=pu.Message%></span>
        </div>
        <%}
    }else{%>
        <div class="update-row">
            <h3>No updates</h3>
            <span>No prescribers have updated their drug list recently.</span>
        </div>
    <%}%>
    </div>
</section>