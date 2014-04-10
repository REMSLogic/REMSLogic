<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="list.ascx.cs" Inherits="Site.App.Views.admin.security.groups.list" %>

<!-- DATATABLES CSS -->
<link rel="stylesheet" media="screen" href="/App/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/App/js/lib/datatables/js/jquery.dataTables.js"></script> 
<script type="text/javascript">
	$(window).bind('content-loaded', function ()
	{
		$('#groups-table').dataTable({
			"sPaginationType": "full_numbers",
			"bStateSave": true,
			"iCookieDuration": (60 * 60 * 24 * 30)
		});
	});
</script> 
<!-- DATATABLES CSS END -->
<h1 class="page-title">Groups</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
		<a href="#admin/security/groups/edit" class="button" style="float: right; margin-top: 10px; margin-bottom: 10px;">Add Group</a>
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        <div id="demo" class="clearfix"> 
            <table class="display" id="groups-table"> 
                <thead> 
                    <tr> 
                        <th></th> 
                        <th>Name</th> 
                    </tr> 
                </thead> 
                <tbody>
				<% foreach( var group in this.Groups ) { %>
                    <tr data-id="<%=group.ID%>"> 
						<td><a href="#admin/security/groups/edit?id=<%=group.ID%>" class="button">Edit</a> <a href="/api/Admin/Security/Group/Delete?id=<%=group.ID%>" class="ajax-button button" data-confirmtext="Are you sure you want to delete this group?">Delete</a></td>
						<td><%=group.DisplayName%></td>
                    </tr>
				<% } %>
                </tbody>
            </table>
        </div>
    </div>
</div>