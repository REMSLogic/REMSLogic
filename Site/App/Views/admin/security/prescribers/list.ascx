<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="list.ascx.cs" Inherits="Site.App.Views.admin.security.prescribers.list" %>

<!-- DATATABLES CSS -->
<link rel="stylesheet" media="screen" href="/App/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/App/js/lib/datatables/js/jquery.dataTables.js"></script> 
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
<h1 class="page-title">Prescribers</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        <div id="demo" class="clearfix"> 
            <table class="display" id="prescribers-table"> 
                <thead> 
                    <tr>
						<th></th>
                        <th>Name</th> 
						<th>Email</th>
						<th>Phone</th>
                    </tr> 
                </thead> 
                <tbody>
				<%foreach( var profile in PrescriberProfiles ){%>
                    <tr>
						<td><a href="#admin/security/prescribers/edit?prescriber-profile-id=<%=profile.ID.Value%>&provider-id=<%=profile.OrganizationId%>" class="button">Edit</a></td>
						<td><%=profile.Contact.Name%></td>
						<td><%=profile.Contact.Email%></td>
						<td><%=profile.Contact.Phone%></td>
                    </tr>
				<% } %>
                </tbody>
            </table>
        </div>
    </div>
</div>