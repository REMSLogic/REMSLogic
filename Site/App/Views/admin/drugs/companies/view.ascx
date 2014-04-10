<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="view.ascx.cs" Inherits="Site.App.Views.admin.drugs.companies.view" %>

<h1 class="page-title"><%=item.Name%></h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
		<div class="form">
			
			<div class="clearfix">
                <label for="form-name" class="form-label">Name <em>*</em></label>
                <div class="form-input"><span class="form-info"><%=item.Name%></span></div>
            </div>

			<div class="clearfix">
                <label for="form-website" class="form-label">Website</label>
                <div class="form-input"><span class="form-info"><%=item.Website%></span></div>
            </div>

			<div class="clearfix">
                <label for="form-phone" class="form-label">Phone</label>
                <div class="form-input"><span class="form-info"><%=item.Phone%></span></div>
            </div>
		</div>
	</div>
</div>