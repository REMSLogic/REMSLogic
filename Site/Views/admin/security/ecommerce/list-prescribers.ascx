<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="list-prescribers.ascx.cs" Inherits="Site.Views.admin.security.ecommerce.list_prescribers" %>

<%@ Import Namespace="Lib.Data" %>
<%@ Import Namespace="RemsLogic.Model.Ecommerce" %>
<!-- DATATABLES CSS -->
<link rel="stylesheet" media="screen" href="/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/js/lib/datatables/js/jquery.dataTables.js"></script> 
<script type="text/javascript">
	$(window).bind('content-loaded', function () {
		$('#prescribers-table').dataTable({
			"sPaginationType": "full_numbers",
			"bStateSave": true,
			"iCookieDuration": (60 * 60 * 24 * 30)
		});
	});
</script> 
<!-- DATATABLES CSS END -->
<h1 class="page-title">E-Commerce Prescribers</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
        <div style="float: right; margin-top: 10px; margin-bottom: 10px;">
            <a href="#admin/security/ecommerce/edit-prescriber?prescriber-profile-id=0" class="button" style="display: inline-block; margin-right: 6px;">Add E-Commerce User</a>
        </div>        
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        <div id="demo" class="clearfix"> 
            <table class="display" id="prescribers-table"> 
                <thead> 
                    <tr>
						<th></th>
                        <th>Name</th> 
						<th>Email</th>
						<th>Phone</th>
                        <th>Created</th>
                        <th>Expires</th>
                        <th>Account Status</th>
                        <th>Enabled</th>
                    </tr> 
                </thead> 
                <tbody>
				<%foreach( PrescriberProfile prescriberProfile in PrescriberProfiles ){
                    Account account = GetAccountByPrescriberPrfile(prescriberProfile);%>
                    <tr>
						<td><a href="#admin/security/ecommerce/edit-prescriber?prescriber-profile-id=<%=prescriberProfile.ID.Value%>" class="button">Edit</a></td>
						<td><%=prescriberProfile.Contact.Name%></td>
						<td><%=prescriberProfile.Contact.Email%></td>
						<td><%=prescriberProfile.Contact.Phone%></td>
                        <td><%=account.CreatedAt.ToShortDateString() %></td>
                        <td><%=account.ExpiresOn.ToShortDateString() %></td>
                        <td><%=(account.ExpiresOn >= DateTime.Now? "Active" : "Expired")%></td>
                        <td><%=(account.IsEnabled? "Yes" : "No") %></td>
                    </tr>
				<% } %>
                </tbody>
            </table>
        </div>
    </div>
</div>