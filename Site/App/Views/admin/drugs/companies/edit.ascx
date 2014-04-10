<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Site.App.Views.admin.drugs.companies.edit" %>

<!-- DATATABLES CSS -->
<link rel="stylesheet" media="screen" href="/App/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/App/js/lib/datatables/js/jquery.dataTables.js"></script> 
<script type="text/javascript">
	$(window).bind('content-loaded', function () {
		$('#users-table').dataTable({
			"sPaginationType": "full_numbers",
			"bStateSave": true,
			"iCookieDuration": (60 * 60 * 24 * 30)
		});
	});
</script> 
<!-- DATATABLES CSS END -->
<h1 class="page-title">Edit Company</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
		<form class="form has-validation ajax-form" action="/api/Admin/Drugs/Company/Edit?id=<%=((item.ID == null) ? 0 : item.ID)%>">
			
			<div class="clearfix">
                <label for="form-name" class="form-label">Name <em>*</em></label>
                <div class="form-input"><input type="text" id="form-name" name="name" required="required" placeholder="Enter the Name" value="<%=item.Name%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-website" class="form-label">Website</label>
                <div class="form-input"><input type="url" id="form-website" name="website" placeholder="Enter the Web URL" value="<%=item.Website%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-phone" class="form-label">Phone</label>
                <div class="form-input"><input type="text" id="form-phone" name="phone" placeholder="Enter the Phone Number" value="<%=item.Phone%>" /></div>
            </div>

			<div class="form-action clearfix">
                <button class="button" type="submit">Save</button>
                <button class="button" type="reset">Reset</button>
            </div>
		</form>
		<% if( this.item.ID.HasValue && this.item.ID.Value > 0 ) { %>
		<a href="#admin/drugs/companies/edit-user?parent-id=<%=this.item.ID %>" class="button" style="float: right; margin-top: 10px; margin-bottom: 10px;">Add User</a>
		<h2>Users</h2>
		<div class="clearfix">
            <table class="display" id="users-table">
                <thead>
                    <tr>
                        <th></th>
						<th>Name</th>
                        <th>Phone</th>
						<th>Email</th>
                    </tr>
                </thead>
                <tbody>
				<% foreach( var user in this.Users ) {
					var contact = user.Profile.PrimaryContact;
				%>
                    <tr data-id="<%=user.ID%>">
						<td><a href="#admin/drugs/companies/edit-user?id=<%=user.ID%>&parent-id=<%=this.item.ID %>" class="button">Edit</a> <a href="/api/Admin/Security/DrugCompanyUser/Delete?id=<%=user.ID%>" class="ajax-button button" data-confirmtext="Are you sure you want to delete this user?">Delete</a></td>
						<td><%=contact.LastName + ", " + contact.FirstName%></td>
						<td><%=contact.Phone%></td>
						<td><%=contact.Email%></td>
                    </tr>
				<% } %>
                </tbody>
            </table>
        </div>
		<% } %>
	</div>
</div>