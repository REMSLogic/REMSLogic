<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Site.App.Views.hcos.facilities.edit" %>

<h1 class="page-title"><%=((item == null || string.IsNullOrEmpty(item.Name)) ? "Create Facility" : "Edit " + item.Name) %></h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        <form class="form has-validation ajax-form" action="/api/HCOs/Facility/Edit?id=<%=item.ID%>&provider-id=<%=this.ProviderID%>">
            <div class="clearfix">
                <label for="form-facility-name" class="form-label">Name <em>*</em></label>
                <div class="form-input"><input type="text" id="form-facility-name" name="facility-name" required="required" placeholder="Enter Facility Name" value="<%=item.Name%>" /></div>
            </div>

            <div class="clearfix">
                <label for="form-street1" class="form-label">Street 1 <em>*</em></label>
                <div class="form-input"><input type="text" id="form-street1" name="street1" required="required" placeholder="Enter Facilities' Street 1" value="<%=address.Street1%>" /></div>
            </div>

            <div class="clearfix">
                <label for="form-street2" class="form-label">Street 2</label>
                <div class="form-input"><input type="text" id="form-street2" name="street2" placeholder="Enter Facilities' Street 2" value="<%=address.Street2%>" /></div>
            </div>

            <div class="clearfix">
                <label for="form-city" class="form-label">City <em>*</em></label>
                <div class="form-input"><input type="text" id="form-city" name="city" required="required" placeholder="Enter Facilities' City" value="<%=address.City%>" /></div>
            </div>

            <div class="clearfix">
                <label for="form-state" class="form-label">State <em>*</em></label>
                <div class="form-input"><input type="text" id="form-state" name="state" required="required" placeholder="Enter Facilities' State" value="<%=address.State%>" /></div>
            </div>

            <div class="clearfix">
                <label for="form-zip" class="form-label">Zip <em>*</em></label>
                <div class="form-input"><input type="text" id="form-zip" name="zip" required="required" placeholder="Enter Facilities' Zip" value="<%=address.Zip%>" /></div>
            </div>

            <div class="form-action clearfix">
                <button class="button" type="submit">Save</button>
                <button class="button" type="reset">Reset</button>
            </div>
        </form>
	</div>
</div>