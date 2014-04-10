<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="list.ascx.cs" Inherits="Site.App.Views.admin.tasks.list" %>

<!-- DATATABLES CSS -->
<link rel="stylesheet" media="screen" href="/App/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/App/js/lib/datatables/js/jquery.dataTables.js"></script> 
<script type="text/javascript">
    $(window).bind('content-loaded', function () {
        $('#task-table').dataTable({
            "sPaginationType": "full_numbers",
            "bStateSave": true,
            "iCookieDuration": (60 * 60 * 24 * 30)
        });

        $('.run-button').click(function (event) {
            var $this = $(this);
            var id = parseInt($this.attr('data-id'));

            if (id) {
                $.ajax({
                    url: '/api/Admin/Tasks/Run',
                    data: 'task_id=' + encodeURIComponent(id),
                    cache: false,
                    success: function (response) {
                    }
                });
            }

            event.preventDefault();
            return false;
        });

        $('#run-all-button').click(function (event) {
            $.ajax({
                url: '/api/Admin/Tasks/RunAll',
                cache: false,
            });
        });
    });
</script> 
<!-- DATATABLES CSS END -->
<h1 class="page-title">Tasks</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
        <input type="button" id="run-all-button" style="float: right; margin-top: 10px; margin-bottom: 10px;" value="Run All" />
        <a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        <div id="demo" class="clearfix">
            <%if(Tasks != null){ %> 
            <table class="display" id="task-table"> 
                <thead> 
                    <tr>
                        <th>Runner</th>
                        <th>State</th> 
                        <th>Created At</th>
                        <th>Scheduled Run Date</th>
                        <th></th>
                    </tr> 
                </thead> 
                <tbody>
                <% foreach(var task in Tasks) { %>
                    <tr data-id="<%=task.ID%>">
                        <td><%=task.Runner%></td> 
                        <td><%=task.State%></td>
                        <td><%=task.CreatedAt.ToShortDateString()%></td>
                        <td><%=task.RunAt.ToShortDateString()%></td>
                        <td><input data-id="<%=task.ID%>"  type="button" value="Run" class="run-button" /></td>
                    </tr>
                <% } %>
                </tbody>
            </table>
            <%} %>
        </div>
    </div>
</div>