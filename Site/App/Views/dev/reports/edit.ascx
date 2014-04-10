<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Site.App.Views.dev.reports.edit" %>

<!-- DATATABLES CSS -->
<link rel="stylesheet" media="screen" href="/App/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/App/js/lib/datatables/js/jquery.dataTables.js"></script> 
<script type="text/javascript">
	$(window).bind('content-loaded', function () {
		$('.report-edit-dt').dataTable({
			"sPaginationType": "full_numbers",
			"bStateSave": true,
			"iCookieDuration": (60 * 60 * 24 * 30)
		});
	});
</script> 
<!-- DATATABLES CSS END -->
<h1 class="page-title"><%=((item.ID == null) ? "Create Report" : "Edit "+item.Name)%></h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
		<form class="form has-validation ajax-form" action="/api/Dev/Reports/Edit?id=<%=((item.ID == null) ? 0 : item.ID)%>">
			
			<div class="clearfix">
                <label for="form-name" class="form-label">Name <em>*</em></label>
                <div class="form-input"><input type="text" id="form-name" name="name" required="required" placeholder="Enter the Name" value="<%=item.Name%>" /></div>
            </div>

			<div class="clearfix answers-div">
                <label for="form-roles" class="form-label">For Roles <em>*</em></label>
                <div class="form-input form-textarea autogrow"><textarea id="form-roles" required="required" name="roles" rows="5" placeholder="Roles that can see this report"><%=item.ForRoles%></textarea></div>
            </div>

			<div class="form-action clearfix">
                <button class="button" type="submit">Save</button>
                <button class="button" type="reset">Reset</button>
            </div>
		</form>

		<% if( item.ID != null ) { %>
		<a href="#dev/reports/edit-filter?report-id=<%=this.item.ID %>" class="button" style="float: right; margin-top: 10px; margin-bottom: 10px;">Add Filter</a>
		<h2>Filters</h2>
		<div class="clearfix">
            <table id="filters-table" class="display report-edit-dt">
                <thead>
                    <tr>
                        <th></th>
						<th>Name</th>
                        <th>Type</th>
                    </tr>
                </thead>
                <tbody>
				<% foreach( var filter in this.item.GetFilters() ) {	%>
                    <tr data-id="<%=filter.ID%>">
						<td><a href="#dev/reports/edit-filter?id=<%=filter.ID%>" class="button">Edit</a> <a href="/api/Dev/ReportFilters/Delete?id=<%=filter.ID%>" class="ajax-button button" data-confirmtext="Are you sure you want to delete this filter?">Delete</a></td>
						<td><%=filter.DisplayName%></td>
						<td><%=filter.Type%></td>
                    </tr>
				<% } %>
                </tbody>
            </table>
        </div>
		<a href="#dev/reports/edit-output?report-id=<%=this.item.ID %>" class="button" style="float: right; margin-top: 10px; margin-bottom: 10px;">Add Output</a>
		<h2>Outputs</h2>
		<div class="clearfix">
            <table id="outputs-table" class="display report-edit-dt">
                <thead>
                    <tr>
                        <th></th>
						<th>Name</th>
                        <th>Type</th>
                    </tr>
                </thead>
                <tbody>
				<% foreach( var output in this.item.GetOutputs() ) {	%>
                    <tr data-id="<%=output.ID%>">
						<td><a href="#dev/reports/edit-output?id=<%=output.ID%>" class="button">Edit</a> <a href="/api/Dev/ReportOutputs/Delete?id=<%=output.ID%>" class="ajax-button button" data-confirmtext="Are you sure you want to delete this output?">Delete</a></td>
						<td><%=output.Name%></td>
						<td><%=output.Type%></td>
                    </tr>
				<% } %>
                </tbody>
            </table>
        </div>
		<% } %>
	</div>
</div>