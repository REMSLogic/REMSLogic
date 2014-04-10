<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Site.App.Views.admin.security.providers.edit" %>

<!-- DATATABLES CSS -->
<link rel="stylesheet" media="screen" href="/App/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/App/js/lib/datatables/js/jquery.dataTables.js"></script> 
<script type="text/javascript">
	$(window).bind('content-loaded', function () {
		$('.provider-edit-dt').dataTable({
			"sPaginationType": "full_numbers",
			"bStateSave": true,
			"iCookieDuration": (60 * 60 * 24 * 30)
		});

		$('#upload-hco-prescribers').click(function () {
			$('#upload-prescribers-modal').dialog({
				modal: true,
				width: 500
			});

			return false;
		});
	});
</script> 
<!-- DATATABLES CSS END -->
<h1 class="page-title"><%=((Provider.ID != null) ? "Edit "+Provider.Name : "Add Provider") %></h1>
<div class="container_12 clearfix leading">
    <div class="grid_12 facility-add-wrap">
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        <h2>Provider Details</h2>
		<form class="form has-validation ajax-form" style="margin-bottom: 32px;" action="/api/Admin/Security/Provider/Edit?id=<%=((Provider.ID == null) ? 0 : Provider.ID)%>">
			
			<div class="clearfix">
                <label for="form-name" class="form-label">Name <em>*</em></label>
                <div class="form-input"><input type="text" id="form-name" name="name" required="required" placeholder="Enter the Name" value="<%=Provider.Name%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-facility-size" class="form-label">Facility Size <em>*</em></label>
                <div class="form-input">
					<select id="form-facility-size" name="facility-size" required="required">
						<option value=""<%=((string.IsNullOrEmpty(Provider.FacilitySize) || Provider.FacilitySize == "") ? " selected" : "") %>>Please Select</option>
						<option value="100 or less"<%=((Provider.FacilitySize == "100 or less") ? " selected" : "") %>>100 or less beds</option>
						<option value="101-300"<%=((Provider.FacilitySize == "101-300") ? " selected" : "") %>>101-300</option>
						<option value="301-500"<%=((Provider.FacilitySize == "301-500") ? " selected" : "") %>>301-500</option>
						<option value="501-900"<%=((Provider.FacilitySize == "501-900") ? " selected" : "") %>>501-900</option>
						<option value="901 or more"<%=((Provider.FacilitySize == "901 or more") ? " selected" : "") %>>901 or more beds</option>
					</select>
				</div>
            </div>

			<div class="form-action clearfix">
                <button class="button" type="submit">Save</button>
                <button class="button" type="reset">Reset</button>
            </div>
		</form>

		<% if( this.Provider.ID.HasValue && this.Provider.ID.Value > 0 ) { %>
		<a href="#hcos/facilities/edit?provider-id=<%=this.Provider.ID %>" class="button" style="float: right; margin-top: 10px; margin-bottom: 10px;">Add Facility</a>
		<h2>Facilities</h2>
		<div class="clearfix">
            <table id="facilities-table" class="display provider-edit-dt">
                <thead>
                    <tr>
                        <th></th>
						<th>Facility Name</th>
                        <th>City</th>
						<th>State</th>
                    </tr>
                </thead>
                <tbody>
				<% foreach( var facility in this.Facilities ) {	%>
                    <tr data-id="<%=facility.ID%>">
						<td><a href="#hcos/facilities/edit?id=<%=facility.ID%>" class="button">Edit</a> <a href="/api/HCOs/Facility/Delete?id=<%=facility.ID%>" class="ajax-button button" data-confirmtext="Are you sure you want to delete this facility?">Delete</a></td>
						<td><%=facility.Name%></td>
						<td><%=facility.PrimaryAddress.City%></td>
						<td><%=facility.PrimaryAddress.State%></td>
                    </tr>
				<% } %>
                </tbody>
            </table>
        </div>
		<a href="#admin/security/providers/edit-user?parent-id=<%=this.Provider.ID %>" class="button" style="float: right; margin-top: 10px; margin-bottom: 10px;">Add User</a>
		<h2>Administrative Users</h2>
		<div class="clearfix">
            <table class="display provider-edit-dt">
                <thead>
                    <tr>
                        <th></th>
						<th>Name</th>
                        <th>Phone</th>
						<th>Email</th>
                    </tr>
                </thead>
                <tbody>
				<% foreach( var user in this.AdministrativeUsers ) {
					var contact = user.Profile.PrimaryContact;
				%>
                    <tr data-id="<%=user.ID%>">
						<td><a href="#admin/security/providers/edit-user?id=<%=user.ID%>&parent-id=<%=this.Provider.ID %>" class="button">Edit</a> <a href="/api/Admin/Security/ProviderUser/Delete?id=<%=user.ID%>" class="ajax-button button" data-confirmtext="Are you sure you want to delete this user?">Delete</a></td>
						<td><%=contact.LastName + ", " + contact.FirstName%></td>
						<td><%=contact.Phone%></td>
						<td><%=contact.Email%></td>
                    </tr>
				<% } %>
                </tbody>
            </table>
        </div>
		<a href="#" class="button" id="upload-hco-prescribers" style="float: right; margin-top: 10px; margin-bottom: 10px;">Add Prescribers</a>
		<h2>Prescribers</h2>
		<div class="clearfix">
            <table class="display provider-edit-dt">
                <thead>
                    <tr>
                        <th></th>
						<th>Name</th>
                        <th>Email</th>
						<th>Expiration</th>
                    </tr>
                </thead>
                <tbody>
				<% foreach( var profile in this.Prescribers ) { %>
                    <tr data-id="<%=profile.ID%>">
						<td><a href="#admin/security/prescribers/edit?id=<%=profile.PrescriberID%>" class="button">Edit</a> <a href="/api/Admin/Profile/Delete?id=<%=profile.ID%>" class="ajax-button button" data-confirmtext="Are you sure you want to delete this profile?">Delete</a></td>
						<td><%=profile.Contact.LastName + ", " + profile.Contact.FirstName%></td>
						<td><%=profile.Contact.Email%></td>
						<td><%=profile.Prescriber.GetCurrentExpirationDate().ToShortDateString()%></td>
                    </tr>
				<% } %>
                </tbody>
            </table>
        </div>
        <%if(PendingInvites.Count > 0){%>
        <h3>Pending Prescriber Invites</h3>
        <div>
            <table class="display provider-edit-dt">
                <thead>
                    <tr>
                        <th></th>
                        <th>Name</th>
                        <th>Email</th>
                    </tr>
                </thead>
                <tbody>
                <% foreach(var profile in PendingInvites) { %>
                    <tr data-id="<%=profile.ID%>">
                        <td><a target="_blank" href="/App/SignUp.aspx#prescriber/wizards/landing-page?token=<%=profile.Guid%>" class="button">Enroll</a></td>
                        <td><%=profile.Contact.LastName + ", " + profile.Contact.FirstName%></td>
                        <td><%=profile.Contact.Email%></td>
                    </tr>
                <% } %>
                </tbody>
            </table>
        </div>
        <%}%>
        <h1>Import Data</h1>
        <form class="form has-validation ajax-form file-upload-form" action="/api/Admin/Security/Provider/UploadPrescribers?id=<%=((Provider.ID == null) ? 0 : Provider.ID)%>" enctype="multipart/form-data">
            <div class="clearfix">
                <label for="form-prescribers-csv" class="form-label">Prescriber CSV File <em>*</em></label>
                <div class="form-input"><input type="file" id="form-prescribers-csv" data-url="/api/Admin/Security/Provider/UploadPrescribers?id=<%=((Provider.ID == null) ? 0 : Provider.ID)%>"  name="prescribers-csv" value="" /></div>
            </div>
            <div class="clearfix">
                <label for="form-facilities-csv" class="form-label">Facility CSV File <em>*</em></label>
                <div class="form-input"><input type="file" id="form-facilities-csv" data-url="/api/Admin/Security/Provider/UploadFacilities?id=<%=((Provider.ID == null) ? 0 : Provider.ID)%>"  name="facilities-csv" value="" /></div>
            </div>
            <div class="clearfix">
                <label for="form-users-csv" class="form-label">Administrative Users CSV File <em>*</em></label>
                <div class="form-input"><input type="file" id="form-users-csv" data-url="/api/Admin/Security/Provider/UploadUsers?id=<%=((Provider.ID == null) ? 0 : Provider.ID)%>"  name="users-csv" value="" /></div>
            </div>
        </form>
        <% } %>
    </div>
</div>