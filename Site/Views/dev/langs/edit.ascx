<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Site.App.Views.dev.langs.edit" %>

<h1 class="page-title">Edit Language</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
		<form class="form has-validation ajax-form" action="/api/Admin/Language/Edit?id=<%=((item.ID == null) ? 0 : item.ID)%>">
			
			<div class="clearfix">
                <label for="form-name" class="form-label">Name <em>*</em></label>
                <div class="form-input"><input type="text" id="form-name" name="name" required="required" placeholder="Enter the Name" value="<%=item.Name%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-code" class="form-label">Language Code <em>*</em></label>
                <div class="form-input"><input type="text" id="form-code" name="code" required="required" placeholder="Enter the Language Code" value="<%=item.Code%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-enabled-icon" class="form-label">Enabled Icon <em>*</em></label>
                <div class="form-input"><input type="text" id="form-enabled-icon" name="enabled-icon" required="required" placeholder="Enter the URL" value="<%=item.EnabledIcon%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-disabled-icon" class="form-label">Disabled Icon <em>*</em></label>
                <div class="form-input"><input type="text" id="form-disabled-icon" name="disabled-icon" required="required" placeholder="Enter the URL" value="<%=item.DisabledIcon%>" /></div>
            </div>

			<div class="form-action clearfix">
                <button class="button" type="submit">Save</button>
                <button class="button" type="reset">Reset</button>
            </div>
		</form>
	</div>
</div>