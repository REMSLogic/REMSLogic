<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="view.ascx.cs" Inherits="Site.App.Views.hcos.facilities.view" %>

<!-- DATATABLES CSS -->
<link rel="stylesheet" media="screen" href="/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/js/lib/datatables/js/jquery.dataTables.js"></script>
<script type="text/javascript">
	$(window).bind('content-loaded', function () {
		$('#prescribers-table').dataTable({
			"sPaginationType": "full_numbers",
			"bStateSave": true,
			"iCookieDuration": (60 * 60 * 24 * 30)
		});

		$(".email-dialog-button").each(function () {
			$(this).click(function () {
				var id = $(this).attr('data-id');

				$('#prescriber-email-modal').dialog("open");

				return false;
			});
		});

		$('#prescriber-email-modal').dialog({
			modal: true,
			height: 230,
			width: 350,
			autoOpen: false,
			buttons: {
				"Send": function () {
					$('#prescriber-email-modal').dialog("close");
				},
				"Cancel": function () {
					$('#prescriber-email-modal').dialog("close");
				}
			}
		});
	});
</script>
<!-- DATATABLES CSS END -->
<h1 class="page-title"><%=item.Name%></h1>
<div class="container_12 clearfix leading">
	<div class="grid_12">
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
		<div class="form">
			<div class="clearfix">
				<label for="form-facility-name" class="form-label">Facility Name</label>
				<div class="form-input"><span class="form-info"><%=item.Name%></span></div>
			</div>

			<div class="clearfix">
				<label for="form-street1" class="form-label">Street 1</label>
				<div class="form-input"><span class="form-info"><%=item.PrimaryAddress.Street1%></span></div>
			</div>

			<div class="clearfix">
				<label for="form-street2" class="form-label">Street 2</label>
				<div class="form-input"><span class="form-info"><%=item.PrimaryAddress.Street2%></span></div>
			</div>

			<div class="clearfix">
				<label for="form-city" class="form-label">City</label>
				<div class="form-input"><span class="form-info"><%=item.PrimaryAddress.City%></span></div>
			</div>

			<div class="clearfix">
				<label for="form-state" class="form-label">State</label>
				<div class="form-input"><span class="form-info"><%=item.PrimaryAddress.State%></span></div>
			</div>

			<div class="clearfix">
				<label for="form-zip" class="form-label">Zip</label>
				<div class="form-input"><span class="form-info"><%=item.PrimaryAddress.Zip%></span></div>
			</div>
		</div>
	</div>
	<div class="container_12 clearfix leading">
		<div class="grid_12">
			<h1>Prescribers</h1>
			<div id="demo" class="clearfix"> 
				<table class="display" id="prescribers-table"> 
					<thead> 
						<tr>
							<th></th>
							<th>Name</th> 
							<th>Email</th>
							<th>Phone</th>
							<th>Enrolled Drugs</th>
							<th>Percent Certified</th>
						</tr> 
					</thead> 
					<tbody>
						<% foreach( var i in this.profiles ) {
								if( i.Prescriber == null )
									continue;
						%>
						<tr data-id="<%=i.ID%>">
							<td>
								<a href="#provider/prescribers/detail?id=<%=i.ID%>" class="button">Detail</a>
								<a href="/api/Provider/Prescribers/Delete?id=<%=i.ID%>" class="ajax-button button" data-confirmtext="Are you sure you want to delete this facility?">Delete</a>
								<a href="#" class="button email-dialog-button" data-id="<%=i.ID%>">Email</a>
							</td>
							<td><%=i.Prescriber.Profile.PrimaryContact.Name%></td>
							<td><%=i.Prescriber.Profile.PrimaryContact.Email%></td>
							<td><%=i.Prescriber.Profile.PrimaryContact.Phone%></td>
							<td class="ar"><%=i.Prescriber.GetNumSelectedDrugs()%></td>
							<td class="ar"><%=((i.Prescriber.GetNumSelectedDrugs() <= 0) ? "0.00" : ((((float)i.Prescriber.GetNumCertifiedDrugs()) / ((float)i.Prescriber.GetNumSelectedDrugs())) * 100.0f).ToString("#.00"))%>%</td>
						</tr>
						<% } %>
					</tbody>
				</table>
			</div>
		</div>
	</div>
</div>
