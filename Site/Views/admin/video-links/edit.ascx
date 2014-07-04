<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Site.Views.admin.video_links.edit" %>

<h1 class="page-title">Add Video Link</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        <form class="form has-validation ajax-form" action="/api/Admin/RestrictedLinks/Edit?id=<%=Link.Id%>">
            
            <div class="clearfix">
                <label for="form-url" class="form-label">Url <em>*</em></label>
                <div class="form-input">
                    <input type="text" id="form-url" name="url" required="required" value="/Video.aspx" />
                </div>
            </div>
            
            <div class="clearfix">
                <label for="form-token" class="form-label">Token <em>*</em></label>
                <div class="form-input">
                    <input type="text" id="form-token" name="token" required="required" value="<%=Guid.NewGuid()%>" />
                </div>
            </div>

            <div class="clearfix">
                <label for="form-expiration-date" class="form-label">Expiration Date</label>
                <div class="form-input">
                    <input type="text" id="form-expiration-date" name="expiration-date" value="<%=DateTime.Now.AddDays(14).Date.ToShortDateString()%>" />
                </div>
            </div>

            <div class="clearfix">
                <label for="form-created-for" class="form-label">Created For <em>*</em></label>
                <div class="form-input">
                    <input type="text" id="form-created-for" name="created-for" required="required" placeholder="Description of who this is for"  />
                </div>
            </div>

            <div class="form-action clearfix">
                <button class="button" type="submit">Save</button>
                <button class="button" type="reset">Reset</button>
            </div>
        </form>
	</div>
</div>