<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Site.App.Views.dev.specialities.edit" %>

<h1 class="page-title">Edit Speciality</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
        <a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        <form class="form has-validation ajax-form" action="/api/Admin/Security/Speciality/Edit?id=<%=((Item.ID == null) ? 0 : Item.ID)%>">
            
            <div class="clearfix">
                <label for="form-name" class="form-label">Name <em>*</em></label>
                <div class="form-input"><input type="text" id="form-name" name="name" required="required" placeholder="Enter the Name" value="<%=Item.Name%>" /></div>
            </div>

            <div class="clearfix">
                <label for="form-code" class="form-label">Code <em>*</em></label>
                <div class="form-input"><input type="text" id="form-code" name="code" required="required" placeholder="Enter the Code" value="<%=Item.Code%>" /></div>
            </div>

            <div class="form-action clearfix">
                <button class="button" type="submit">Save</button>
                <button class="button" type="reset">Reset</button>
            </div>
        </form>
    </div>
</div>