﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="list.ascx.cs" Inherits="Site.App.Views.admin.security.prescribers.list" %>

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
						<th>Created</th>
                    </tr> 
                </thead> 
                <tbody>
				<%
					foreach( var item in this.Prescribers )
					{
						var p = item.Profile;
						var c = p.PrimaryContact;
				%>
                    <tr>
						<td><a href="#admin/security/prescribers/edit?id=<%=item.ID%>" class="button">Edit</a></td>
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