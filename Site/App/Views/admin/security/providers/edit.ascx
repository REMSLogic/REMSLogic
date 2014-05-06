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
<h1 class="page-title"><%=((Organization.Id != null) ? "Edit "+Organization.Name : "Add Provider") %></h1>
<div class="container_12 clearfix leading">
    <div class="grid_12 facility-add-wrap">
        <a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        
        <h2>HCO Details</h2>
        <form class="form has-validation ajax-form" style="margin-bottom: 32px;" action="/api/Admin/Security/Provider/Edit?id=<%=((Organization.Id == null) ? 0 : Organization.Id)%>">
            <div class="clearfix">
                <label for="form-name" class="form-label">Name <em>*</em></label>
                <div class="form-input"><input type="text" id="form-name" name="name" required="required" placeholder="Enter the Name" value="<%=Organization.Name%>" /></div>
            </div>
            
            <div class="clearfix">
                <label for="form-name" class="form-label">Primary Facility Name <em>*</em></label>
                <div class="form-input"><input type="text" id="form-fac-name" name="fac-name" required="required" placeholder="Enter the Name" value="<%=Organization.PrimaryFacility.Name%>" /></div>
            </div>
            
            <div class="clearfix">
                <label for="form-facility-size" class="form-label">Primary Facility Size <em>*</em></label>
                <div class="form-input">
                    <select id="form-facility-size" name="facility-size" required="required">
                        <option value=""<%=((string.IsNullOrEmpty(Organization.PrimaryFacility.BedSize) || Organization.PrimaryFacility.BedSize == "") ? " selected" : "") %>>Please Select</option>
                        <option value="100 or less"<%=((Organization.PrimaryFacility.BedSize == "100 or less") ? " selected" : "") %>>100 or less beds</option>
                        <option value="101-300"<%=((Organization.PrimaryFacility.BedSize == "101-300") ? " selected" : "") %>>101-300</option>
                        <option value="301-500"<%=((Organization.PrimaryFacility.BedSize == "301-500") ? " selected" : "") %>>301-500</option>
                        <option value="501-900"<%=((Organization.PrimaryFacility.BedSize == "501-900") ? " selected" : "") %>>501-900</option>
                        <option value="901 or more"<%=((Organization.PrimaryFacility.BedSize == "901 or more") ? " selected" : "") %>>901 or more beds</option>
                    </select>
                </div>
            </div>
            
            <div class="clearfix">
                <label for="form-name" class="form-label">Street 1 <em>*</em></label>
                <div class="form-input">
                    <input type="text" id="form-fac-street1" name="fac-street1" required="required" placeholder="Street 1" 
                        value="<%=Organization.PrimaryFacility.Address.Street1%>" />
                </div>
            </div>
            
            <div class="clearfix">
                <label for="form-name" class="form-label">Street 2 <em>*</em></label>
                <div class="form-input">
                    <input type="text" id="form-fac-street2" name="fac-street2" required="required" placeholder="Street 2" 
                        value="<%=Organization.PrimaryFacility.Address.Street2%>" />
                </div>
            </div>
            
            <div class="clearfix">
                <label for="form-name" class="form-label">City <em>*</em></label>
                <div class="form-input">
                    <input type="text" id="form-fac-city" name="fac-city" required="required" placeholder="City" 
                        value="<%=Organization.PrimaryFacility.Address.City%>" />
                </div>
            </div>
            
            <div class="clearfix">
                <label for="form-name" class="form-label">State <em>*</em></label>
                <div class="form-input">
                    <input type="text" id="form-fac-state" name="fac-state" required="required" placeholder="State" 
                        value="<%=Organization.PrimaryFacility.Address.State%>" />
                </div>
            </div>
            
            <div class="clearfix">
                <label for="form-name" class="form-label">Zip Code <em>*</em></label>
                <div class="form-input">
                    <input type="text" id="form-fac-zip" name="fac-zip" required="required" placeholder="Zip Code" 
                        value="<%=Organization.PrimaryFacility.Address.Zip%>" />
                </div>
            </div>
            
            <div class="clearfix">
                <label for="form-name" class="form-label">Country <em>*</em></label>
                <div class="form-input">
                    <input type="text" id="form-fac-country" name="fac-country" required="required" placeholder="Country" 
                        value="<%=Organization.PrimaryFacility.Address.Country%>" />
                </div>
            </div>

            <div class="form-action clearfix">
                <button class="button" type="submit">Save</button>
                <button class="button" type="reset">Reset</button>
            </div>
        </form>

		<% if(Organization.Id > 0 ) { %>
		<a href="#hcos/facilities/edit?id=0&provider-id=<%=Organization.Id%>" class="button" style="float: right; margin-top: 10px; margin-bottom: 10px;">Add Facility</a>
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
				<% foreach( var facility in Organization.Facilities ) {	%>
                    <tr data-id="<%=facility.Id%>">
						<td><a href="#hcos/facilities/edit?id=<%=facility.Id%>&provider-id=<%=Organization.Id%>" class="button">Edit</a> <a href="/api/HCOs/Facility/Delete?id=<%=facility.Id%>" class="ajax-button button" data-confirmtext="Are you sure you want to delete this facility?">Delete</a></td>
						<td><%=facility.Name%></td>
						<td><%=facility.Address.City%></td>
						<td><%=facility.Address.State%></td>
                    </tr>
				<% } %>
                </tbody>
            </table>
        </div>
        
		<a href="#admin/security/providers/edit-user?provider-user-id=0&organization-id=<%=Organization.Id%>" class="button" style="float: right; margin-top: 10px; margin-bottom: 10px;">Add User</a>
		<h2>Administrative Users</h2>
		<div class="clearfix">
            <table class="display provider-edit-dt">
                <thead>
                    <tr>
                        <th></th>
                        <th>Facility</th>
						<th>Name</th>
                        <th>Phone</th>
						<th>Email</th>
                    </tr>
                </thead>
                <tbody>
				<%foreach( var user in AdministrativeUsers ) {
					var contact = user.Profile.PrimaryContact;
				%>
                    <tr data-id="<%=user.ID%>">
						<td><a href="#admin/security/providers/edit-user?provider-user-id=<%=user.ID%>&organization-id=<%=Organization.Id%>" class="button">Edit</a> <a href="/api/Admin/Security/ProviderUser/Delete?id=<%=user.ID%>" class="ajax-button button" data-confirmtext="Are you sure you want to delete this user?">Delete</a></td>
                        <td><%=user.Facility.Name%></td>
						<td><%=contact.LastName + ", " + contact.FirstName%></td>
						<td><%=contact.Phone%></td>
						<td><%=contact.Email%></td>
                    </tr>
				<% } %>
                </tbody>
            </table>
        </div>

		<a href="#admin/security/prescribers/edit?prescriber-profile-id=0&provider-id=<%=Organization.Id%>" class="button" style="float: right; margin-top: 10px; margin-bottom: 10px;">Add Prescribers</a>
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
						<td><a href="#admin/security/prescribers/edit?prescriber-profile-id=<%=profile.ID.Value%>&provider-id=<%=Organization.Id%>" class="button">Edit</a> <a href="/api/Admin/security/Prescriber/Delete?id=<%=profile.ID%>" class="ajax-button button" data-confirmtext="Are you sure you want to delete this profile?">Delete</a></td>
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
        <form class="form has-validation ajax-form file-upload-form" action="/api/Admin/Security/Provider/UploadPrescribers?id=<%=Organization.Id%>" enctype="multipart/form-data">
            <div class="clearfix">
                <label for="form-prescribers-csv" class="form-label">Prescriber CSV File <em>*</em></label>
                <div class="form-input"><input type="file" id="form-prescribers-csv" data-url="/api/Admin/Security/Provider/UploadPrescribers?id=<%=Organization.Id%>"  name="prescribers-csv" value="" /></div>
            </div>
            <div class="clearfix">
                <label for="form-facilities-csv" class="form-label">Facility CSV File <em>*</em></label>
                <div class="form-input"><input type="file" id="form-facilities-csv" data-url="/api/Admin/Security/Provider/UploadFacilities?id=<%=Organization.Id%>"  name="facilities-csv" value="" /></div>
            </div>
            <div class="clearfix">
                <label for="form-users-csv" class="form-label">Administrative Users CSV File <em>*</em></label>
                <div class="form-input"><input type="file" id="form-users-csv" data-url="/api/Admin/Security/Provider/UploadUsers?id=<%=Organization.Id%>"  name="users-csv" value="" /></div>
            </div>
        </form>
        <% } %>
    </div>
</div>