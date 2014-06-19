<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Site.App.Views.common.contacts.edit" %>

<h1 class="page-title">Edit Contact</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
		<form class="form has-validation ajax-form" action="/api/Common/Contact/Edit?id=<%=((item.ID == null) ? 0 : item.ID)%>">
			<input type="hidden" name="back-hash" value="<%=this.BackHash %>" />
			<input type="hidden" name="parent-type" value="<%=this.ParentType %>" />
			<input type="hidden" name="parent-id" value="<%=this.ParentID %>" />

			<div class="clearfix">
                <label for="form-contact-type" class="form-label">Contact Type <em>*</em></label>
                <div class="form-input">
					<select id="form-contact-type" name="contact-type" required="required">
						<option value="">None</option>
						<option value="Administrative"<%=((this.item.ContactType == "Administrative") ? " selected" : "") %>>Administrative</option>
						<option value="Technical"<%=((this.item.ContactType == "Technical") ? " selected" : "") %>>Technical</option>
						<option value="Other"<%=((this.item.ContactType == "Other") ? " selected" : "") %>>Other</option>
					</select>
				</div>
            </div>

			<div class="clearfix">
                <label for="form-prefix" class="form-label">Prefix</label>
                <div class="form-input">
					<select id="form-prefix" name="prefix">
						<option value="">None</option>
						<option value="Dr."<%=((this.item.Prefix == "Dr.") ? " selected" : "") %>>Dr.</option>
						<option value="Mr."<%=((this.item.Prefix == "Mr.") ? " selected" : "") %>>Mr.</option>
						<option value="Mrs."<%=((this.item.Prefix == "Mrs.") ? " selected" : "") %>>Mrs.</option>
						<option value="Miss"<%=((this.item.Prefix == "Miss") ? " selected" : "") %>>Miss</option>
						<option value="Ms."<%=((this.item.Prefix == "Ms.") ? " selected" : "") %>>Ms.</option>
					</select>
				</div>
            </div>

			<div class="clearfix">
                <label for="form-first-name" class="form-label">First Name <em>*</em></label>
                <div class="form-input"><input type="text" id="form-first-name" name="first-name" required="required" placeholder="Enter the first name" value="<%=item.FirstName%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-last-name" class="form-label">Last Name <em>*</em></label>
                <div class="form-input"><input type="text" id="form-last-name" name="last-name" required="required" placeholder="Enter the last name" value="<%=item.LastName%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-postfix" class="form-label">Postfix</label>
                <div class="form-input">
					<select id="form-postfix" name="postfix">
						<option value="">None</option>
						<option value="MD"<%=((this.item.Postfix == "MD") ? " selected" : "") %>>MD</option>
					</select>
				</div>
            </div>

			<div class="clearfix">
                <label for="form-title" class="form-label">Title</label>
                <div class="form-input"><input type="text" id="form-title" name="title" placeholder="Enter the title" value="<%=item.Title%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-email" class="form-label">Email <em>*</em></label>
                <div class="form-input"><input type="email" id="form-email" name="email" required="required" placeholder="Enter the email" value="<%=item.Email%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-phone" class="form-label">Phone <em>*</em></label>
                <div class="form-input"><input type="text" id="form-phone" name="phone" required="required" placeholder="Enter the phone number" value="<%=item.Phone%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-fax" class="form-label">Fax</label>
                <div class="form-input"><input type="text" id="form-fax" name="fax" placeholder="Enter the fax number" value="<%=item.Fax%>" /></div>
            </div>

			<% if( item.ParentType == "prescriber" || item.ParentType == "provider" ) { %>
			<div class="clearfix">
                <label for="form-username" class="form-label">Username <em>*</em></label>
                <div class="form-input"><input type="text" id="form-username" name="username" required="required" placeholder="Enter the username" value="<%=item.Phone%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-password" class="form-label">Password</label>
                <div class="form-input"><input type="password" id="form-password" name="password" placeholder="Enter the password" value="" /></div>
            </div>
			<% } %>

			<div class="form-action clearfix">
                <button class="button" type="submit">Save</button>
                <button class="button" type="reset">Reset</button>
            </div>
		</form>
	</div>
</div>