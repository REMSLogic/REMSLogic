<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit-provider.ascx.cs" Inherits="Site.Views.admin.security.ecommerce.edit_provider" %>
<%@ Import Namespace="Lib.Data" %>

<h1 class="page-title">Edit Provider User</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
		<form class="form has-validation ajax-form" action="/api/Admin/Security/Ecommerce/EditProvider?provider-user-id=<%=((ProviderUser.ID == null) ? 0 : ProviderUser.ID)%>">
			<div class="clearfix">
                <label for="form-first-name" class="form-label">First Name <em>*</em></label>
                <div class="form-input">
                    <input type="text" id="form-first-name" name="first-name" required="required" placeholder="Enter the first name" 
                        value="<%=Contact.FirstName%>" />
                </div>
            </div>

			<div class="clearfix">
                <label for="form-last-name" class="form-label">Last Name <em>*</em></label>
                <div class="form-input">
                    <input type="text" id="form-last-name" name="last-name" required="required" placeholder="Enter the last name" 
                        value="<%=Contact.LastName%>" />
                </div>
            </div>

			<div class="clearfix">
                <label for="form-email" class="form-label">Email <em>*</em></label>
                <div class="form-input">
                    <input type="text" id="form-email" name="email" required="required" placeholder="Enter the email" 
                        value="<%=Contact.Email%>" />
                </div>
            </div>

			<div class="clearfix">
                <label for="form-phone" class="form-label">Phone <em>*</em></label>
                <div class="form-input">
                    <input type="text" id="form-phone" name="phone" required="required" placeholder="Enter the phone" 
                        value="<%=Contact.Phone%>" />
                </div>
            </div>

			<div class="clearfix">
                <label for="form-username" class="form-label">Username <em>*</em></label>
                <div class="form-input">
                    <input type="text" id="form-username" name="username" required="required" placeholder="Enter the Username" 
                        value="<%=User.Username%>" />
                </div>
            </div>

			<div class="clearfix">
                <label for="form-password" class="form-label">New Password<% if( !User.ID.HasValue ) { %> <em>*</em><% } %></label>
                <div class="form-input"><input type="password" id="form-password" name="password"<% if( !User.ID.HasValue ) { %> required="required"<% } %> placeholder="Enter the Password" /></div>
            </div>

			<div class="clearfix">
                <label for="form-confirm-password" class="form-label">Confirm Password<% if( !User.ID.HasValue ) { %> <em>*</em><% } %></label>
                <div class="form-input">
                    
                    <input type="password" id="form-confirm-password" name="confirm-password"<% if( !User.ID.HasValue ) { %> required="required"<% } %> placeholder="Confirm the Password" />
                </div>
            </div>

			<div class="clearfix">
                <label for="form-street" class="form-label">Street <em>*</em></label>
                <div class="form-input">
                    <input type="text" id="form-street" name="street" required="required" placeholder="Enter the Street Address" 
                        value="<%=Address.Street1%>" />
                </div>
            </div>

			<div class="clearfix">
                <label for="form-street-2" class="form-label">Street 2</label>
                <div class="form-input">
                    <input type="text" id="form-street-2" name="street-2" placeholder="" 
                        value="<%=Address.Street2%>" />
                </div>
            </div>

			<div class="clearfix">
                <label for="form-city" class="form-label">City <em>*</em></label>
                <div class="form-input">
                    <input type="text" id="form-city" name="city" required="required" placeholder="Enter the City" 
                        value="<%=Address.City%>" />
                </div>
            </div>

			<div class="clearfix">
                <label for="form-state" class="form-label">State <em>*</em></label>
                <div class="form-input">
                    <input type="text" id="form-state" name="state" required="required" placeholder="Enter the State" 
                        value="<%=Address.State%>" />
                </div>
            </div>

			<div class="clearfix">
                <label for="form-zip" class="form-label">Zip <em>*</em></label>
                <div class="form-input">
                    <input type="text" id="form-zip" name="zip" required="required" placeholder="Enter the Zip" 
                        value="<%=Address.Zip%>" />
                </div>
            </div>
 
			<div class="clearfix">
                <label for="form-expires-on" class="form-label">Expiration Date: <em>*</em></label>
                <div class="form-input">
                    <input type="date" id="form-expires-on" name="expires-on" required="required" placeholder="Enter Expriation Date" 
                        value="<%=Account.ExpiresOn.ToShortDateString()%>" />
                </div>
            </div>
            
			<div class="clearfix">
                <label for="form-is-enabled" class="form-label">Is Enabled <em>*</em></label>
                <div class="form-input">
                    <select id="form-is-enabled" name="is-enabled" required="required">
                        <option value="yes" <%=((Account.IsEnabled) ? " selected=\"selected\"" : "") %>>Yes</option>
                        <option value="no" <%=((!Account.IsEnabled) ? " selected=\"selected\"" : "") %>>No</option>
                    </select>
                </div>
            </div>

			<div class="form-action clearfix">
                <button class="button" type="submit">Save</button>
                <button class="button" type="reset">Reset</button>
            </div>
		</form>
	</div>
</div>