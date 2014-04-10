<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="list.ascx.cs" Inherits="Site.App.Views.dev.user_types.list" %>

<!-- DATATABLES CSS -->
<link rel="stylesheet" media="screen" href="/App/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/App/js/lib/datatables/js/jquery.dataTables.js"></script> 
<script type="text/javascript">
	$(window).bind('content-loaded', function ()
	{
		$('#user-types-table').dataTable({
			"sPaginationType": "full_numbers",
			"bStateSave": true,
			"iCookieDuration": (60 * 60 * 24 * 30)
		});
	});
</script> 
<!-- DATATABLES CSS END -->
<h1 class="page-title">User Types</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
		<a href="#dev/user-types/edit" class="button" style="float: right; margin-top: 10px; margin-bottom: 10px;">Add User Type</a>
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        <div id="demo" class="clearfix"> 
            <table class="display" id="user-types-table"> 
                <thead> 
                    <tr> 
                        <th></th> 
                        <th>Name</th>
						<th>Contact</th>
						<th>Address</th>
                    </tr> 
                </thead> 
                <tbody>
				<% foreach( var item in this.Items ) { %>
                    <tr data-id="<%=item.ID%>"> 
						<td><a href="#dev/user-types/edit?id=<%=item.ID%>" class="button">Edit</a> <a href="/api/Admin/Security/UserType/Delete?id=<%=item.ID%>" class="ajax-button button" data-confirmtext="Are you sure you want to delete this role?">Delete</a></td>
						<td><%=item.DisplayName%></td>
						<td><%=item.HasContact%></td>
						<td><%=item.HasAddress%></td>
                    </tr>
				<% } %>
                </tbody>
            </table>
        </div>
    </div>
</div>