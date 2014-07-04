<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="list.ascx.cs" Inherits="Site.Views.admin.video_links.list" %>

<% long ts = (long)(DateTime.Now - new DateTime(1970,1,1)).TotalSeconds; %>
<!-- DATATABLES CSS -->
<link rel="stylesheet" media="screen" href="/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/js/lib/datatables/js/jquery.dataTables.js"></script> 
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
<h1 class="page-title">Video Links</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
        <a href="#admin/video-links/edit" class="button" style="float: right; margin-bottom: 10px;">Add Link</a>
        <a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        <div id="demo" class="clearfix">
            <table class="display admin-drugs-list" id="drugs-table-<%=ts %>">
                <thead>
                    <tr>
                        <th></th>
                        <th>Created For</th>
                        <th>Link</th>
                        <th>Expiration Date</th>
                    </tr>
                </thead>
                <tbody>
                <% foreach(var link in Links ) { %>
                    <tr data-id="<%=link.Id%>"> 
                        <td>
                            <a href="#adming/video-link/delete?id=<%=link.Id%>" class="button">Delete</a>
                        </td>
                        <td><%=link.CreatedFor%></td>
                        <td><%=Request.Url.GetLeftPart(UriPartial.Authority)%><%=link.Url%>?token=<%=link.Token%></td>
                        <td><%=link.ExpirationDate.Date.ToShortDateString()%></td>
                    </tr>
                <% } %>
                </tbody>
            </table>
        </div>
    </div>
</div>