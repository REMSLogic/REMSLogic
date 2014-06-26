<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="list.ascx.cs" Inherits="Site.App.Views.hcos.facilities.list" %>

<!-- DATATABLES CSS -->
<link rel="stylesheet" media="screen" href="/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/js/lib/datatables/js/jquery.dataTables.js"></script>
<script type="text/javascript">
	$(window).bind('content-loaded', function () {
		$('#facilities-table').dataTable({
			"sPaginationType": "full_numbers",
			"bStateSave": true,
			"iCookieDuration": (60 * 60 * 24 * 30)
		});
	});
</script>
<!-- DATATABLES CSS END -->
<h1 class="page-title">Facilities</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12 facility-add-wrap">
        <a href="#hcos/facilities/edit?provider-id=<%=Provider.ID.Value %>" class="button" style="float: right; margin-top: 10px; margin-bottom: 10px;">Add Facility</a>
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        <div id="demo" class="clearfix"> 
            <table class="display" id="facilities-table"> 
                <thead> 
                    <tr>
						<th></th>
                        <th>Name</th> 
						<th>Street1</th>
						<th>Street2</th>
						<th>City</th>
						<th>State</th>
						<th>Zip</th>
                    </tr> 
                </thead> 
                <tbody>
					<% foreach( var i in this.Facilities ) { %>
                    <tr data-id="<%=i.Id%>"> 
						<td>
						    <a href="#hcos/facilities/edit?id=<%=i.Id%>&provider-id=<%=OrganizationId%>" class="button">Edit</a>
                            <% if (Framework.Security.Manager.HasRole("view_admin", true)){ %>
                                <a href="/api/HCOs/Facility/Delete?id=<%=i.Id%>" class="ajax-button button" data-confirmtext="Are you sure you want to delete this facility?">Delete</a>
                            <% } %>
						</td>
						<td><%=i.Name%></td>
						<td><%=i.Address.Street1%></td>
						<td><%=i.Address.Street2%></td>
						<td><%=i.Address.City%></td>
						<td><%=i.Address.State%></td>
						<td><%=i.Address.Zip%></td>
                    </tr>
					<% } %>
                </tbody>
            </table>
        </div>
    </div>
</div>
