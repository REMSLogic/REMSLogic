<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Site.App.Views.dev.user_types.edit" %>

<h1 class="page-title">Edit User Type</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
		<form class="form has-validation ajax-form" action="/api/Admin/Security/UserType/Edit?id=<%=((item.ID == null) ? 0 : item.ID)%>">
			
			<div class="clearfix">
                <label for="form-name" class="form-label">Name <em>*</em></label>
                <div class="form-input"><input type="text" id="form-name" name="name" required="required" placeholder="Enter the Name" value="<%=item.Name%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-display-name" class="form-label">Display Name <em>*</em></label>
                <div class="form-input"><input type="text" id="form-display-name" name="display-name" required="required" placeholder="Enter the Display Name" value="<%=item.DisplayName%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-has-contact" class="form-label">Contact Info</label>
                <div class="form-input">
					<select id="form-has-contact" name="has-contact">
						<option value="False"<%=((!item.HasContact) ? " selected=\"selected\"" : "") %>>No</option>
						<option value="True"<%=((item.HasContact) ? " selected=\"selected\"" : "") %>>Yes</option>
					</select>
				</div>
            </div>

			<div class="clearfix">
                <label for="form-has-address" class="form-label">Address Info</label>
                <div class="form-input">
					<select id="form-has-address" name="has-address">
						<option value="False"<%=((!item.HasAddress) ? " selected=\"selected\"" : "") %>>No</option>
						<option value="True"<%=((item.HasAddress) ? " selected=\"selected\"" : "") %>>Yes</option>
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