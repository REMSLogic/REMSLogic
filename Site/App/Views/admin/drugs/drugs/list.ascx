<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="list.ascx.cs" Inherits="Site.App.Views.admin.drugs.drugs.list" %>

<% long ts = (long)(DateTime.Now - new DateTime(1970,1,1)).TotalSeconds; %>
<!-- DATATABLES CSS -->
<link rel="stylesheet" media="screen" href="/App/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/App/js/lib/datatables/js/jquery.dataTables.js"></script> 
<script type="text/javascript">
	$(window).bind('content-loaded', function () {
		$('#drugs-table-<%=ts %>').dataTable({
			"sPaginationType": "full_numbers",
			"bStateSave": true,
			"iCookieDuration": (60 * 60 * 24 * 30)
		});
	});
</script> 
<!-- DATATABLES CSS END -->
<h1 class="page-title"><%=this.Title %></h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
		<a href="#admin/dsq/edit" class="button" style="float: right; margin-bottom: 10px;">Add Drug</a>
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        <div id="demo" class="clearfix">
            <table class="display admin-drugs-list" id="drugs-table-<%=ts %>">
                <thead>
                    <tr>
                        <th></th>
                        <th>Drug Name</th>
						<th>Formulations</th>
						<th>Pending Changes</th>
						<th>Last Updated</th>
                    </tr>
                </thead>
                <tbody>
				<% foreach( var drug in this.Drugs ) { %>
                    <tr data-id="<%=drug.ID%>"> 
						<td>
							<% if( !Pending ) { %>
							<a href="#common/drugs/detail?id=<%=drug.ID%>" class="button">View</a>
							<a href="#admin/dsq/edit?id=<%=drug.ID%>" class="button">Edit</a>
							<a href="/api/Admin/Drugs/Drug/Delete?id=<%=drug.ID%>" class="ajax-button button" data-confirmtext="Are you sure you want to delete this Drug?">Delete</a></td>
							<% } else { %>
							<a href="#admin/dsq/edit?id=<%=drug.ID%>" class="button">Review</a>
							<% } %>
						<td><%=drug.GenericName%></td>
						<td><%=drug.GetNumFormulations()%></td>
						<td><%=(Lib.Systems.Drugs.HasPendingChanges(drug) ? "Yes" : "-") %></td>
						<td><%=drug.Updated.ToShortDateString() %></td>
                    </tr>
				<% } %>
                </tbody>
            </table>
        </div>
    </div>
</div>