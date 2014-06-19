<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="list.ascx.cs" Inherits="Site.App.Views.admin.drugs.formulations.list" %>

<!-- DATATABLES CSS -->
<link rel="stylesheet" media="screen" href="/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/js/lib/datatables/js/jquery.dataTables.js"></script> 
<script type="text/javascript">
	$(window).bind('content-loaded', function () {
		$('#formulations-table').dataTable({
			"sPaginationType": "full_numbers",
			"bStateSave": true,
			"iCookieDuration": (60 * 60 * 24 * 30)
		});
	});
</script> 
<!-- DATATABLES CSS END -->
<h1 class="page-title">Formulations</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
		<a href="#admin/drugs/formulations/edit" class="button" style="float: right; margin-top: 10px; margin-bottom: 10px;">Add Formulation</a>
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        <div id="demo" class="clearfix"> 
            <table class="display" id="formulations-table"> 
                <thead> 
                    <tr> 
                        <th></th> 
                        <th>Name</th>
                    </tr> 
                </thead> 
                <tbody>
				<% foreach( var item in this.Items ) { %>
                    <tr data-id="<%=item.ID%>"> 
						<td><a href="#admin/drugs/formulations/edit?id=<%=item.ID%>" class="button">Edit</a> <a href="/api/Admin/Drugs/Formulation/Delete?id=<%=item.ID%>" class="ajax-button button" data-confirmtext="Are you sure you want to delete this formulation?">Delete</a></td>
						<td><%=item.Name%></td>
                    </tr>
				<% } %>
                </tbody>
            </table>
        </div>
    </div>
</div>