<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="list.ascx.cs" Inherits="Site.App.Views.admin.security.users.list" %>

<!-- DATATABLES CSS -->
<link rel="stylesheet" media="screen" href="/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/js/lib/datatables/js/jquery.dataTables.js"></script> 
<script type="text/javascript">
	$(window).bind('content-loaded', function () {
		$('.admin-users-table').dataTable({
			"sPaginationType": "full_numbers",
			"bStateSave": true,
			"iCookieDuration": (60 * 60 * 24 * 30)
		});
	});
</script>
<!-- DATATABLES CSS END -->
<h1 class="page-title">Users</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
		<a href="#admin/security/users/edit" class="button" style="float: right; margin-top: 10px; margin-bottom: 10px;">Add User</a>
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        <div id="demo" class="clearfix"> 
            <table class="display admin-users-table"> 
                <thead> 
                    <tr> 
                        <th></th> 
                        <th>Name</th> 
                        <th>Email</th>
						<th>Phone</th>
						<th>Created</th>
                    </tr> 
                </thead> 
                <tbody>
				<%
					foreach( var p in this.Items )
					{
						var c = p.PrimaryContact;
				%>
                    <tr data-id="<%=p.ID%>"> 
						<td><a href="#admin/security/users/edit?id=<%=p.ID%>" class="button">Edit</a> <a href="/api/Admin/Security/User/Delete?id=<%=p.ID%>" class="ajax-button button" data-confirmtext="Are you sure you want to delete this user?">Delete</a></td>
						<td><%=c.Name%></td>
						<td><%=c.Email%></td>
						<td><%=c.Phone%></td>
						<td><%=p.Created.ToShortDateString()%></td>
                    </tr>
				<% } %>
                </tbody>
            </table>
        </div>
    </div>
</div>