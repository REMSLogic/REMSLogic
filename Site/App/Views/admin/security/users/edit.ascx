<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Site.App.Views.admin.security.users.edit" %>

<!-- DATATABLES CSS -->
<link rel="stylesheet" media="screen" href="/App/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/App/js/lib/datatables/js/jquery.dataTables.js"></script>
<script type="text/javascript" src="/App/js/lib/datatables/js/jquery.datatables.rowReordering.js"></script>
<!-- DATATABLES CSS END -->
<script type="text/javascript" src="/App/js/jquery.autogrowtextarea.js"></script>
<script type="text/javascript">
	$(window).bind('content-loaded', function () {
		$("#form-desc").autoGrow();

		$('#licenses-table').dataTable({
			"sPaginationType": "full_numbers",
			"bStateSave": true,
			"iCookieDuration": (60 * 60 * 24 * 30)
		});
	});
</script> 
<h1 class="page-title">Edit User</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
		<form class="form has-validation ajax-form" action="/api/Admin/Security/User/Edit<%=((user.ID == null) ? "" : "?id="+user.ID)%>">
			
			<div class="clearfix">
                <label for="form-username" class="form-label">Username <em>*</em></label>
                <div class="form-input"><input type="text" id="form-username" name="username" required="required" placeholder="Enter the Username" value="<%=user.Username%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-email" class="form-label">Email <em>*</em></label>
                <div class="form-input"><input type="text" id="form-email" name="email" required="required" placeholder="Enter the Email" value="<%=user.Email%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-password" class="form-label">Password <% if( item.ID == null || item.ID == 0 ) { %><em>*</em><% } %></label>
                <div class="form-input"><input type="password" id="form-password" <% if( item.ID == null || item.ID == 0 ) { %>required="required" <% } %>name="password" placeholder="Enter the Password" value="" /></div>
            </div>

			<div class="clearfix">
                <label for="form-confirm" class="form-label">Confirm Password <% if( item.ID == null || item.ID == 0 ) { %><em>*</em><% } %></label>
                <div class="form-input"><input type="password" id="form-confirm" <% if( item.ID == null || item.ID == 0 ) { %>required="required" <% } %>name="confirm" equalTo="#form-password" placeholder="Confirm the Password" value="" /></div>
            </div>

			<div class="clearfix">
                <label for="form-user-type" class="form-label">User Type <em>*</em></label>
                <div class="form-input">
					<select id="form-user-type" name="user-type" required="required">
						<option value="">Select One</option>
						<%
							foreach( var ut in UserTypes )
							{
								if (ut.Name != "dev" && ut.Name != "admin")
									continue;
						%>
						<option value="<%=ut.ID.Value %>"<%=((ut.ID.Value == item.UserTypeID) ? " selected=\"selected\"" : "") %>><%=ut.DisplayName %></option>
						<%
							}
						%>
					</select>
				</div>
            </div>
			
			<hr />

			<div class="clearfix">
                <label for="form-contact-prefix" class="form-label">Prefix <em>*</em></label>
                <div class="form-input">
					<select id="form-contact-prefix" name="contact-prefix" required="required">
						<option value="">Select One</option>
						<option value="Ms"<%=((contact.Prefix == "Ms") ? " selected=\"selected\"" : "") %>>Ms</option>
						<option value="Mrs"<%=((contact.Prefix == "Mrs") ? " selected=\"selected\"" : "") %>>Mrs</option>
						<option value="Miss"<%=((contact.Prefix == "Miss") ? " selected=\"selected\"" : "") %>>Miss</option>
						<option value="Mr"<%=((contact.Prefix == "Mr") ? " selected=\"selected\"" : "") %>>Mr</option>
						<option value="Dr"<%=((contact.Prefix == "Dr") ? " selected=\"selected\"" : "") %>>Dr</option>
					</select>
				</div>
            </div>

			<div class="clearfix">
                <label for="form-contact-name" class="form-label">Name <em>*</em></label>
                <div class="form-input"><input type="text" id="form-contact-name" name="contact-name" required="required" placeholder="Enter the Contact's Name" value="<%=contact.FirstName + " " + contact.LastName%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-contact-suffix" class="form-label">Suffix</label>
                <div class="form-input">
					<select id="form-contact-suffix" name="contact-suffix">
						<option value="">- N/A -</option>
						<option value="Jr"<%=((contact.Postfix == "Jr") ? " selected=\"selected\"" : "") %>>Jr</option>
						<option value="Sr"<%=((contact.Postfix == "Sr") ? " selected=\"selected\"" : "") %>>Sr</option>
					</select>
				</div>
            </div>

			<div class="clearfix">
                <label for="form-contact-title" class="form-label">Title</label>
                <div class="form-input">
					<select id="form-contact-title" name="contact-title">
						<option value="">- N/A -</option>
						<option value="DDS"<%=((contact.Title == "DDS") ? " selected=\"selected\"" : "") %>>DDS</option>
						<option value="MD"<%=((contact.Title == "MD") ? " selected=\"selected\"" : "") %>>MD</option>
						<option value="PhD"<%=((contact.Title == "PhD") ? " selected=\"selected\"" : "") %>>PhD</option>
					</select>
				</div>
            </div>

			<div class="clearfix">
                <label for="form-contact-phone" class="form-label">Phone <em>*</em></label>
                <div class="form-input"><input type="text" id="form-contact-phone" name="contact-phone" required="required" placeholder="Enter the Contact's Phone" value="<%=contact.Phone%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-contact-fax" class="form-label">Fax</label>
                <div class="form-input"><input type="text" id="form-contact-fax" name="contact-fax" placeholder="Enter the Contact's Fax Number" value="<%=contact.Fax%>" /></div>
            </div>

			<div class="form-action clearfix">
                <button class="button" type="submit">Save</button>
                <button class="button" type="reset">Reset</button>
            </div>
		</form>
	</div>
</div>