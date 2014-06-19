<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="delete.ascx.cs" Inherits="Site.App.Views.provider.prescribers.delete" %>

<h1 class="page-title">Delete <%=Prescriber.Profile.PrimaryContact.Name%></h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
        <a class="button" href="#provider/prescribers/list" style="margin-bottom: 10px;">Back</a>
        <form id="frmEditPrescriber" class="form has-validation ajax-form" action="/api/Provider/Prescribers/Delete?id=<%=(Prescriber.ID ?? 0)%>">
            <div>Are you sure you want to delete this Prescriber?</div>
            <a class="button" href="#provider/prescribers/list" style="margin-bottom: 10px;">No</a>
            <button class="button" type="submit">Yes</button>
        </form>
    </div>
</div>