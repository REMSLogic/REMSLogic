<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="list.ascx.cs" Inherits="Site.App.Views.dev.specialities.list" %>

<!-- DATATABLES CSS -->
<link rel="stylesheet" media="screen" href="/App/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/App/js/lib/datatables/js/jquery.dataTables.js"></script> 
<script type="text/javascript">
    $(window).bind('content-loaded', function () {
        $('#specialities-table').dataTable({
            "sPaginationType": "full_numbers",
            "bStateSave": true,
            "iCookieDuration": (60 * 60 * 24 * 30)
        });
    });
</script> 
<!-- DATATABLES CSS END -->
<h1 class="page-title">Roles</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
        <a href="#dev/specialities/edit" class="button" style="float: right; margin-top: 10px; margin-bottom: 10px;">Add Speciality</a>
        <a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        <div id="demo" class="clearfix"> 
            <table class="display" id="specialities-table"> 
                <thead> 
                    <tr> 
                        <th></th> 
                        <th>Name</th> 
                        <th>Code</th>
                    </tr> 
                </thead> 
                <tbody>
                <% foreach( var item in this.Items ) { %>
                    <tr data-id="<%=item.ID%>"> 
                        <td><a href="#dev/specialities/edit?id=<%=item.ID%>" class="button">Edit</a> <a href="/api/Admin/Security/Speciality/Delete?id=<%=item.ID%>" class="ajax-button button" data-confirmtext="Are you sure you want to delete this role?">Delete</a></td>
                        <td><%=item.Name%></td>
                        <td><%=item.Code %></td>
                    </tr>
                <% } %>
                </tbody>
            </table>
        </div>
    </div>
</div>