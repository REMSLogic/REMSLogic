<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="prescriber-summary.ascx.cs" Inherits="Site.App.Views.provider.reports.prescriber_summary" %>

<!-- DATATABLES CSS -->
<link rel="stylesheet" media="screen" href="/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/js/lib/datatables/js/jquery.dataTables.js"></script> 
<script type="text/javascript">
	$(window).bind('content-loaded', function ()
	{
		$('#prescribers-table').dataTable({
			"bInfo": false,
			"bFilter": false,
			"bPaginate": false,
			"bStateSave": true,
			"iCookieDuration": (60 * 60 * 24 * 30)
		});
	});
</script> 
<!-- DATATABLES CSS END -->
<h1 class="page-title">Prescribers</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12 facility-add-wrap">
		<a href="#provider/reports/list" class="button" style="float: left; margin-top: 10px; margin-bottom: 10px;">Back</a>
		<a href="/Page.aspx?v=provider/reports/prescriber-summary&export=csv" class="button" style="float: right; margin-top: 10px; margin-bottom: 10px;">Export to CSV</a>
        <div id="demo" class="clearfix"> 
            <table class="display" id="prescribers-table"> 
                <thead> 
                    <tr>
                        <th>Name</th> 
						<th>Email</th>
						<th>Phone</th>
						<th>Enrolled Drugs</th>
						<th>Percent Certified</th>
                    </tr> 
                </thead> 
                <tbody>
					<% foreach( var i in this.Prescribers ) { %>
                    <tr data-id="<%=i.ID%>"> 
						<td><%=i.FirstName + " " + i.LastName%></td>
						<td><%=i.Email%></td>
						<td><%=i.Phone%></td>
						<td class="ar"><%=i.GetNumSelectedDrugs()%></td>
						<td class="ar"><%=((i.GetNumSelectedDrugs() <= 0) ? "0.00" : ((((float)i.GetNumCertifiedDrugs()) / ((float)i.GetNumSelectedDrugs())) * 100.0f).ToString("#0.00"))%>%</td>
                    </tr>
					<% } %>
                </tbody>
            </table>
        </div>
    </div>
</div>