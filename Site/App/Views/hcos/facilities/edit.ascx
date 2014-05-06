<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Site.App.Views.hcos.facilities.edit" %>

<h1 class="page-title"><%=((Facility == null || string.IsNullOrEmpty(Facility.Name)) ? "Create Facility" : "Edit " + Facility.Name) %></h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        <form class="form has-validation ajax-form" action="/api/HCOs/Facility/Edit?id=<%=Facility.Id%>&provider-id=<%=OrganizationId%>">
            <div class="clearfix">
                <label for="form-facility-name" class="form-label">Name <em>*</em></label>
                <div class="form-input">
                    <input type="text" id="form-facility-name" name="facility-name" required="required" placeholder="Enter Facility Name" 
                        value="<%=Facility.Name%>" />
                </div>
            </div>
            
            <div class="clearfix">
                <label for="form-facility-size" class="form-label">Primary Facility Size <em>*</em></label>
                <div class="form-input">
                    <select id="form-facility-size" name="facility-size" required="required">
                        <option value=""<%=((string.IsNullOrEmpty(Facility.BedSize) || Facility.BedSize == "") ? " selected" : "") %>>Please Select</option>
                        <option value="100 or less"<%=((Facility.BedSize == "100 or less") ? " selected" : "") %>>100 or less beds</option>
                        <option value="101-300"<%=((Facility.BedSize == "101-300") ? " selected" : "") %>>101-300</option>
                        <option value="301-500"<%=((Facility.BedSize == "301-500") ? " selected" : "") %>>301-500</option>
                        <option value="501-900"<%=((Facility.BedSize == "501-900") ? " selected" : "") %>>501-900</option>
                        <option value="901 or more"<%=((Facility.BedSize == "901 or more") ? " selected" : "") %>>901 or more beds</option>
                    </select>
                </div>
            </div>

            <div class="clearfix">
                <label for="form-street1" class="form-label">Street 1 <em>*</em></label>
                <div class="form-input">
                    <input type="text" id="form-street1" name="street1" required="required" placeholder="Enter Facilities' Street 1" 
                        value="<%=Facility.Address.Street1%>" />
                </div>
            </div>

            <div class="clearfix">
                <label for="form-street2" class="form-label">Street 2</label>
                <div class="form-input">
                    <input type="text" id="form-street2" name="street2" placeholder="Enter Facilities' Street 2" 
                        value="<%=Facility.Address.Street2%>" />
                </div>
            </div>

            <div class="clearfix">
                <label for="form-city" class="form-label">City <em>*</em></label>
                <div class="form-input">
                    <input type="text" id="form-city" name="city" required="required" placeholder="Enter Facilities' City" 
                        value="<%=Facility.Address.City%>" />
                </div>
            </div>

            <div class="clearfix">
                <label for="form-state" class="form-label">State <em>*</em></label>
                <div class="form-input">
                    <input type="text" id="form-state" name="state" required="required" placeholder="Enter Facilities' State" 
                        value="<%=Facility.Address.State%>" />
                </div>
            </div>

            <div class="clearfix">
                <label for="form-zip" class="form-label">Zip <em>*</em></label>
                <div class="form-input">
                    <input type="text" id="form-zip" name="zip" required="required" placeholder="Enter Facilities' Zip" 
                        value="<%=Facility.Address.Zip%>" />
                </div>
            </div>
            
            <div class="clearfix">
                <label for="form-country" class="form-label">Country <em>*</em></label>
                <div class="form-input">
                    <input type="text" id="form-country" name="country" required="required" placeholder="Enter Facilities' Country" 
                        value="<%=Facility.Address.Country%>" />
                </div>
            </div>

            <div class="form-action clearfix">
                <button class="button" type="submit">Save</button>
                <button class="button" type="reset">Reset</button>
            </div>
        </form>
	</div>
</div>