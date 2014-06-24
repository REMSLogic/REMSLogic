<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="archive.ascx.cs" Inherits="Site.App.Views.common.notifications.archive" %>

<!-- DATATABLES CSS -->
<link rel="stylesheet" media="screen" href="/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/js/lib/datatables/js/jquery.dataTables.js"></script> 
<script type="text/javascript">
    $(window).bind('content-loaded', function () {
        $('#notifications-table').dataTable({
            "sPaginationType": "full_numbers",
            "bStateSave": true,
            "iCookieDuration": (60 * 60 * 24 * 30)
        });
    });
</script> 
<!-- DATATABLES CSS END -->
<h1 class="page-title">Notification Archive</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        <div id="demo" class="clearfix">
            <%if(Notifications != null){ %> 
            <table class="display" id="notifications-table"> 
                <thead> 
                    <tr>
                        <th></th>
                        <th>Important</th>
                        <th>Message</th> 
                        <th>Link</th>
						<th>Date Sent</th>
                        <th></th>
                    </tr> 
                </thead> 
                <tbody>
				<% foreach(var notification in Notifications) { %>
                    <tr data-id="<%=notification.ID%>">
                        <td>
                            <a href="/api/Notifications/Delete?id=<%=notification.ID%>" class="ajax-button button" data-confirmtext="Are you sure you want to delete this Notification?">Delete</a>
                        </td>
                        <td><%=((String.IsNullOrEmpty(notification.Important) || notification.Important == "no")? "No" : "Yes")%></td> 
						<td><%=notification.Message%></td>
						<td style="text-align: center;">
                            <%if(!String.IsNullOrEmpty(notification.Link)){ %>
                                <a class="button" href="<%=notification.Link%>" target="<%=(notification.LinkType == "link")? "_blank" : "" %>">View More</a>
                            <%}%>
                        </td>
						<td><%=notification.Sent.ToShortDateString()%></td>
                        <th><input type="checkbox" /></th>
                    </tr>
				<% } %>
                </tbody>
            </table>
            <%} %>
        </div>
    </div>
</div>