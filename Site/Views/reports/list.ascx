<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="list.ascx.cs" Inherits="Site.App.Views.reports.list" %>

<!-- DATATABLES CSS -->
<link rel="stylesheet" media="screen" href="/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/js/lib/datatables/js/jquery.dataTables.js"></script> 
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

<h1 class="page-title">Reports</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
        <a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        <div id="demo" class="clearfix"> 
            <table class="display" id="providers-table"> 
                <thead> 
                    <tr>
                        <th></th>
                        <th>Name</th> 
                    </tr> 
                </thead> 
                <tbody>
                <%foreach(var item in items){%>
                    <tr>
                        <td>
                            <a class="button" href="#reports/view?id=<%=item.ID.Value %>">Run</a>
                        </td>
                        <td><%=item.Name%></td>
                    </tr>
                <%}%>
                </tbody>
            </table>
        </div>
    </div>
</div>