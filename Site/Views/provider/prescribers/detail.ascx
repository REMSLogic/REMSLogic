<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="detail.ascx.cs" Inherits="Site.App.Views.provider.prescribers.detail" %>
<%@ Import Namespace="Lib.Data" %>

<!-- DATATABLES CSS -->
<link rel="stylesheet" media="screen" href="/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/js/lib/datatables/js/jquery.dataTables.js"></script> 
<script type="text/javascript">
	$(window).bind('content-loaded', function ()
	{
		$('#drugs-table').dataTable({
			"sPaginationType": "full_numbers",
			"bStateSave": true,
			"iCookieDuration": (60 * 60 * 24 * 30)
		});

		$('#facilities-table').dataTable({
			"sPaginationType": "full_numbers",
			"bStateSave": true,
			"iCookieDuration": (60 * 60 * 24 * 30)
		});
	});
</script> 
<!-- DATATABLES CSS END -->
<h1 class="page-title"><%=PrescriberProfile.Contact.Name%></h1>
<div class="container_12 clearfix leading">
    <div class="grid_12 facility-add-wrap">
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        <form id="frmEditPrescriber" class="form has-validation ajax-form" action="/api/Provider/Prescribers/Edit?profile-id=<%=(PrescriberProfile.ID.Value)%>">
            <div class="clearfix">
                <label for="first-name" class="form-label">First Name</label>
                <div class="form-input"><input type="text" id="first-name" name="first-name" required="required" placeholder="Enter Prescriber's First Name" value="<%=PrescriberProfile.Contact.FirstName%>" /></div>
            </div>

            <div class="clearfix">
                <label for="last-name" class="form-label">Last Name</label>
                <div class="form-input"><input type="text" id="last-name" name="last-name" required="required" placeholder="Enter Prescriber's Last Name" value="<%=PrescriberProfile.Contact.LastName%>" /></div>
            </div>

            <div class="clearfix">
                <label for="phone" class="form-label">Phone</label>
                <div class="form-input"><input type="text" id="phone" name="phone" required="required" placeholder="Enter Prescriber's Phone" value="<%=PrescriberProfile.Contact.Phone%>" /></div>
            </div>

            <div class="clearfix">
                <label for="fax" class="form-label">Fax</label>
                <div class="form-input"><input type="text" id="fax" name="fax" placeholder="Enter Prescriber's Fax" value="<%=PrescriberProfile.Contact.Fax%>" /></div>
            </div>

            <div class="clearfix">
                <label for="email" class="form-label">Email</label>
                <div class="form-input"><input type="text" id="email" name="email" required="required" placeholder="Enter Prescriber's Email" value="<%=PrescriberProfile.Contact.Email%>" /></div>
            </div>

            <div class="clearfix">
                <label for="street-1" class="form-label">Street 1</label>
                <div class="form-input"><input type="text" id="street-1" name="street-1" required="required" placeholder="Enter Prescriber's Street 1" value="<%=PrescriberProfile.Address.Street1%>" /></div>
            </div>

            <div class="clearfix">
                <label for="street-2" class="form-label">Street 2</label>
                <div class="form-input"><input type="text" id="street-2" name="street-2" placeholder="Enter Prescriber's Street 2" value="<%=PrescriberProfile.Address.Street2%>" /></div>
            </div>

			<div class="clearfix">
                <label for="city" class="form-label">City</label>
                <div class="form-input"><input type="text" id="city" name="city" required="required" placeholder="Enter Prescriber's City" value="<%=PrescriberProfile.Address.City%>" /></div>
            </div>

			<div class="clearfix">
                <label for="state" class="form-label">State</label>
                <div class="form-input"><input type="text" id="state" name="state" required="required" placeholder="Enter Prescriber's State" value="<%=PrescriberProfile.Address.State%>" /></div>
            </div>

			<div class="clearfix">
                <label for="zip" class="form-label">Zip</label>
                <div class="form-input"><input type="text" id="zip" name="zip" required="required" placeholder="Enter Prescriber's Zip" value="<%=PrescriberProfile.Address.Zip%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-facility-id" class="form-label">Primary Facility <em>*</em></label>
                <div class="form-input">
					<select id="form-facility-id" name="facility-id" required="required">
						<option value="">Please Select</option>
						<% foreach( var f in ProviderFacilities ) { %>
						<option value="<%=f.ID.Value %>"<%=((PrescriberProfile.PrimaryFacilityID == f.ID) ? " selected=\"selected\"" : "") %>><%=f.Name %></option>
						<% } %>
					</select>
				</div>
            </div>

            <div class="clearfix">
                <label for="form-npi" class="form-label">NPI Number</label>
                <div class="form-input"><input type="text" id="form-npi" name="npi" placeholder="Enter your NPI Number" value="<%=Prescriber.NpiId%>"/></div>
            </div>

            <div class="clearfix">
                <label for="form-state-id" class="form-label">State ID <em>*</em></label>
                <div class="form-input">
                    <input type="text" id="form-state-id" name="state-id" placeholder="Enter your State Id" value="<%=Prescriber.StateId%>" required="required" />
                </div>
            </div>

            <div class="clearfix">
                <label for="form-state-id" class="form-label">Issuing State <em>*</em></label>
                <div class="form-input">
                    <select id="form-issuer" name="issuer" required="required" >
                    <%foreach(State s in States){%>
                        <option value="<%=s.ID%>" <%=(Prescriber.StateIdIssuer == s.ID)? "selected=\"selected\"" : String.Empty%>><%=s.USPS%></option>
                    <%}%>
                    </select>
                </div>
            </div>

            <div class="clearfix">
                <label for="form-speciality" class="form-label">Speciality <em>*</em></label>
                <div class="form-input">
                    <select id="form-speciality" name="speciality" required="required" >
                    <%foreach(Speciality s in Specialities){%>
                        <option value="<%=s.ID%>" <%=(SpecialityId == s.ID)? "selected=\"selected\"" : String.Empty%>><%=s.Name%></option>
                    <%}%>
                    </select>
                </div>
            </div>

            <div class="clearfix">
                <label for="form-prescriber-type" class="form-label">Prescriber Type <em>*</em></label>
                <div class="form-input">
                    <select id="form-prescriber-type" name="prescriber-type" required="required" >
                    <%foreach(PrescriberType pt in PrescriberTypes){%>
                        <option value="<%=pt.ID%>" <%=(TypeId == pt.ID)? "selected=\"selected\"" : String.Empty%>><%=pt.DisplayName%></option>
                    <%}%>
                    </select>
                </div>
            </div>

            <div class="form-action clearfix">
                <button class="button" type="submit">Save</button>
                <button class="button" type="reset">Reset</button>
            </div>
        </form>
		<div class="detail-sub-section clearfix">
			<h2>Drugs</h2>
			<table class="display" id="drugs-table">
                <thead>
                    <tr>
                        <th></th>
                        <th>Drug Name</th>
						<!--<th>Date Enrolled</th>
						<th>Date Certified</th>-->
                    </tr>
                </thead>
                <tbody>
				<% foreach( var drug in this.Drugs ) { %>
                    <tr data-id="<%=drug.ID%>"> 
						<td><a href="#common/drugs/detail?id=<%=drug.ID%>" class="button">View</a></td>
						<td><%=drug.GenericName%></td>
						<%--<td><%=drug.DateAdded.ToString("MM/dd/yyyy")%></td>
						<td><%=((drug.DateCertified != null && drug.DateCertified.HasValue) ? drug.DateCertified.Value.ToString("MM/dd/yyyy") : "No") %></td>--%>
                    </tr>
				<% } %>
                </tbody>
            </table>
		</div>
		<div class="detail-sub-section clearfix">
			<h2>Facilities</h2>
			<table class="display" id="facilities-table">
                <thead>
                    <tr>
                        <th></th>
                        <th>Name</th>
						<th>Street 1</th>
						<th>City</th>
                    </tr>
                </thead>
                <tbody>
				<% foreach( var facility in this.PrescriberFacilities ) { %>
                    <tr data-id="<%=facility.ID%>"> 
						<td><a href="#hcos/facilities/edit?id=<%=facility.ID%>" class="button">View</a></td>
						<td><%=facility.Name%></td>
						<td><%=facility.PrimaryAddress.Street1%></td>
						<td><%=facility.PrimaryAddress.City%></td>
                    </tr>
				<% } %>
                </tbody>
            </table>
		</div>
	</div>
</div>