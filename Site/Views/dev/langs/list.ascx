<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="list.ascx.cs" Inherits="Site.App.Views.dev.langs.list" %>

<!-- DATATABLES CSS -->
<link rel="stylesheet" media="screen" href="/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/js/lib/datatables/js/jquery.dataTables.js"></script> 
<script type="text/javascript">
	$(window).bind('content-loaded', function () {
		$('#langs-table').dataTable({
			"sPaginationType": "full_numbers",
			"bStateSave": true,
			"iCookieDuration": (60 * 60 * 24 * 30)
		});
	});
</script> 
<!-- DATATABLES CSS END -->
<h1 class="page-title">Languages</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
		<a href="#dev/langs/edit" class="button" style="float: right; margin-top: 10px; margin-bottom: 10px;">Add Language</a>
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        <div id="demo" class="clearfix"> 
            <table class="display" id="langs-table"> 
                <thead> 
                    <tr> 
                        <th style="width: 100px;"></th> 
                        <th>Name</th>
						<th>Icons</th>
                    </tr> 
                </thead> 
                <tbody>
				<% foreach( var item in this.Items ) { %>
                    <tr data-id="<%=item.ID%>"> 
						<td><a href="#dev/langs/edit?id=<%=item.ID%>" class="button">Edit</a> <a href="/api/Admin/Language/Delete?id=<%=item.ID%>" class="ajax-button button" data-confirmtext="Are you sure you want to delete this language?">Delete</a></td>
						<td><%=item.Name%></td>
						<td><img src="<%=item.EnabledIcon%>" style="width: 16px; height: 10px;" /></td>
                    </tr>
				<% } %>
                </tbody>
            </table>
        </div>
    </div>
</div>