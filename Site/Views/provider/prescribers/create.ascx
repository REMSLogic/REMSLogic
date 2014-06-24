<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="create.ascx.cs" Inherits="Site.App.Views.provider.prescribers.create" %>

<h1 class="page-title">Add Prescriber</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12 facility-add-wrap">
        <a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        <form id="frmEditPrescriber" class="form has-validation ajax-form" action="/api/Provider/Prescribers/Create">
            <div class="clearfix">
                <label for="first-name" class="form-label">First Name</label>
                <div class="form-input"><input type="text" id="first-name" name="first-name" required="required" placeholder="First Name" value="" /></div>
            </div>
            <div class="clearfix">
                <label for="last-name" class="form-label">Last Name</label>
                <div class="form-input"><input type="text" id="last-name" name="last-name" required="required" placeholder="Last Name" value="" /></div>
            </div>
            <div class="clearfix">
                <label for="phone-number" class="form-label">Phone Number</label>
                <div class="form-input"><input type="text" id="phone-number" name="phone-number" placeholder="Phone Number" value="" /></div>
            </div>
            <div class="clearfix">
                <label for="email" class="form-label">Email</label>
                <div class="form-input"><input type="text" id="email" name="email" required="required" placeholder="Email Address for Invite" value="" required /></div>
            </div>
            <div class="clearfix">
                <label for="message" class="form-label">Message</label>
                <div class="form-input"><textarea id="message" name="message" placeholder="Enter a message to send" /></div>
            </div>

            <div class="form-action clearfix">
                <button class="button" type="submit">Send Invite</button>
                <button class="button" type="reset">Reset</button>
            </div>
        </form>
    </div>
</div>