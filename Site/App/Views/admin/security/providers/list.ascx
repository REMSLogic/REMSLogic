<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="list.ascx.cs" Inherits="Site.App.Views.admin.security.providers.list" %>

<!-- DATATABLES CSS -->
<link rel="stylesheet" media="screen" href="/App/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/App/js/lib/datatables/js/jquery.dataTables.js"></script> 
<script type="text/javascript">
	$(window).bind('content-loaded', function () {
		$('#providers-table').dataTable({
			"sPaginationType": "full_numbers",
			"bStateSave": true,
			"iCookieDuration": (60 * 60 * 24 * 30)
		});
	});
</script> 
<!-- DATATABLES CSS END -->
<h1 class="page-title">Providers</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
        <div style="float: right; margin-top: 10px; margin-bottom: 10px;">
            <a href="#admin/security/providers/edit?id=" class="button" style="display: inline-block; margin-right: 6px;">Add HCO</a>
        </div>
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        <div id="demo" class="clearfix"> 
            <table class="display" id="providers-table"> 
                <thead> 
                    <tr>
						<th></th>
                        <th>Name</th> 
                        <th>Contact Name</th>
						<th>Contact Email</th>
						<th>Contact Phone</th>
						<th>Created</th>
                    </tr> 
                </thead> 
                <tbody>
				<%
					foreach( var item in this.Providers )
					{
						var c = item.PrimaryContact;
				%>
                    <tr>
						<td><a href="#admin/security/providers/edit?id=<%=item.ID%>" class="button">Edit</a></td>
						<td><%=item.Name%></td>
						<td><%=c.FirstName%> <%=c.LastName%></td>
						<td><%=c.Email%></td>
						<td><%=c.Phone%></td>
						<td><%=item.Created.ToShortDateString()%></td>
                    </tr>
				<% } %>
                </tbody>
            </table>
        </div>
    </div>
</div>