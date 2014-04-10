<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Site.App.Views.provider.prescribers.edit" %>

<h1 class="page-title"><%=Prescriber.Profile.PrimaryContact.Name%></h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
        <a class="button" href="#provider/prescribers/list" style="margin-bottom: 10px;">Back</a>
        <form id="frmEditPrescriber" class="form has-validation ajax-form" action="/api/Provider/Prescribers/Edit?id=<%=(Prescriber.ID ?? 0)%>">
            <input type="hidden" name="back-hash" value="<%=BackHash %>" />
            <input type="hidden" name="parent-type" value="<%=ParentType %>" />
            <input type="hidden" name="parent-id" value="<%=ParentId %>" />

            <div class="clearfix">
                <label for="prefix" class="form-label">Prefix</label>
                <div class="form-input"><input type="text" id="prefix" name="prefix" placeholder="Enter Prescriber's Prefix" value="<%=Prescriber.Profile.PrimaryContact.Prefix%>" /></div>
            </div>

            <div class="clearfix">
                <label for="first-name" class="form-label">First Name</label>
                <div class="form-input"><input type="text" id="first-name" name="first-name" required="required" placeholder="Enter Prescriber's First Name" value="<%=Prescriber.Profile.PrimaryContact.FirstName%>" /></div>
            </div>

            <div class="clearfix">
                <label for="last-name" class="form-label">Last Name</label>
                <div class="form-input"><input type="text" id="last-name" name="last-name" required="required" placeholder="Enter Prescriber's Last Name" value="<%=Prescriber.Profile.PrimaryContact.LastName%>" /></div>
            </div>

            <div class="clearfix">
                <label for="postfix" class="form-label">Postfix</label>
                <div class="form-input"><input type="text" id="postfix" name="postfix" placeholder="Enter Prescriber's Postfix" value="<%=Prescriber.Profile.PrimaryContact.Postfix%>" /></div>
            </div>

            <div class="clearfix">
                <label for="title" class="form-label">Title</label>
                <div class="form-input"><input type="text" id="title" name="title" placeholder="Enter Prescriber's Title" value="<%=Prescriber.Profile.PrimaryContact.Title%>" /></div>
            </div>

            <div class="clearfix">
                <label for="phone" class="form-label">Phone</label>
                <div class="form-input"><input type="text" id="phone" name="phone" required="required" placeholder="Enter Prescriber's Phone" value="<%=Prescriber.Profile.PrimaryContact.Phone%>" /></div>
            </div>

            <div class="clearfix">
                <label for="fax" class="form-label">Fax</label>
                <div class="form-input"><input type="text" id="fax" name="fax" placeholder="Enter Prescriber's Fax" value="<%=Prescriber.Profile.PrimaryContact.Fax%>" /></div>
            </div>

            <div class="clearfix">
                <label for="email" class="form-label">Email</label>
                <div class="form-input"><input type="text" id="email" name="email" required="required" placeholder="Enter Prescriber's Email" value="<%=Prescriber.Profile.PrimaryContact.Email%>" /></div>
            </div>

            <div class="clearfix">
                <label for="country" class="form-label">Country</label>
                <div class="form-input"><input type="text" id="country" name="country" required="required" placeholder="Enter Prescriber's Country" value="<%=Prescriber.Profile.PrimaryAddress.Country%>" /></div>
            </div>

            <div class="clearfix">
                <label for="state" class="form-label">State</label>
                <div class="form-input"><input type="text" id="state" name="state" required="required" placeholder="Enter Prescriber's State" value="<%=Prescriber.Profile.PrimaryAddress.State%>" /></div>
            </div>

            <div class="clearfix">
                <label for="zip" class="form-label">Zip</label>
                <div class="form-input"><input type="text" id="zip" name="zip" required="required" placeholder="Enter Prescriber's Zip" value="<%=Prescriber.Profile.PrimaryAddress.Zip%>" /></div>
            </div>

            <div class="clearfix">
                <label for="city" class="form-label">City</label>
                <div class="form-input"><input type="text" id="city" name="city" required="required" placeholder="Enter Prescriber's City" value="<%=Prescriber.Profile.PrimaryAddress.City%>" /></div>
            </div>

            <div class="clearfix">
                <label for="street-1" class="form-label">Street 1</label>
                <div class="form-input"><input type="text" id="street-1" name="street-1" required="required" placeholder="Enter Prescriber's Street 1" value="<%=Prescriber.Profile.PrimaryAddress.Street1%>" /></div>
            </div>

            <div class="clearfix">
                <label for="street-2" class="form-label">Street 2</label>
                <div class="form-input"><input type="text" id="street-2" name="street-2" placeholder="Enter Prescriber's Street 2" value="<%=Prescriber.Profile.PrimaryAddress.Street2%>" /></div>
            </div>

            <div class="form-action clearfix">
                <button class="button" type="submit">Save</button>
                <button class="button" type="reset">Reset</button>
            </div>
        </form>
    </div>
</div>