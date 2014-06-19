<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GeneralInfo.ascx.cs" Inherits="Site.App.Controls.DSQ.GeneralInfo" %>

<div class="clearfix">
    <label for="form-generic-name" class="form-label">Drug Name <em>*</em></label>
    <div class="form-input"><input type="text" id="form-generic-name" name="generic-name" required="required" placeholder="Enter the Drug's generic name" value="<%=item.GenericName%>" /></div>
</div>

<div class="clearfix">
    <label for="form-rems-reason" class="form-label">REMS Program Purpose</label>
    <div class="form-input"><input type="text" id="form-rems-reason" name="rems-reason" placeholder="Enter the reason this is a REMS Drug" value="<%=item.RemsReason%>" /></div>
</div>

<div class="clearfix">
    <label for="form-indication" class="form-label">Indication</label>
    <div class="form-input"><input type="text" id="form-indication" name="indication" placeholder="Enter the Drug's Indication" value="<%=item.Indication%>" /></div>
</div>

<div class="clearfix">
    <label for="form-class-id" class="form-label">Class</label>
    <div class="form-input">
		<select id="form-class-id" name="class-id">
			<option value="0">Non-ETASU</option>
			<option value="1"<%=((item.ClassID == 1) ? " selected" : "") %>>ETASU</option>
		</select>
	</div>
</div>

<div class="clearfix">
    <label for="form-system-id" class="form-label">Shared System</label>
    <div class="form-input">
		<select id="form-system-id" name="system-id">
			<option value="">None</option>
			<% foreach(var s in this.Systems) { %>
			<option value="<%=s.ID %>"<%=((item.SystemID == s.ID.Value) ? " selected=\"selected\"" : "") %>><%=s.Name %></option>
			<% } %>
		</select>
	</div>
</div>

<% if( item.ID.HasValue ) { %>
<div class="clearfix form-padding">
	<a href="#admin/dsq/formulation?drug-id=<%=this.item.ID %>" class="button" style="float: right; margin-top: 10px; margin-bottom: 10px;">Add Formulation</a>
	<h2>Formulations</h2>
    <table class="display formulations-table">
        <thead>
            <tr>
                <th></th>
				<th>Formulation</th>
                <th>Brand Name</th>
				<th>Company</th>
				<th>Company Phone</th>
            </tr>
        </thead>
        <tbody>
		<% foreach( var f in this.Formulations ) {
			var formulation = f.Formulation;
			var company = f.DrugCompany;
		%>
            <tr data-id="<%=f.ID%>">
				<td><a href="#admin/dsq/formulation?id=<%=f.ID%>&drug-id=<%=this.item.ID %>" class="button">Edit</a> <a href="/api/Admin/Drugs/DrugFormulation/Delete?id=<%=f.ID%>" class="ajax-button button" data-confirmtext="Are you sure you want to delete this drug formulation?">Delete</a>
				</td>
				<td><%=formulation.Name%></td>
				<td><%=f.BrandName%></td>
				<td><%=company.Name%></td>
				<td><%=f.DrugCompanyPhone%></td>
            </tr>
		<% } %>
        </tbody>
    </table>
</div>

<% } %>
<div class="clearfix">
    <label for="form-rems-website" class="form-label">REMS Program Website</label>
    <div class="form-input"><input type="text" id="form-rems-website" name="rems-website" placeholder="Enter the URL for the main REMS Program Website" value="<%=item.RemsProgramUrl%>" /></div>
</div>

<div class="clearfix">
    <label for="form-fda-number" class="form-label">FDA Application number</label>
    <div class="form-input"><input type="text" id="form-fda-number" name="fda-number" placeholder="Enter the Drug's FDA Application Number" value="<%=item.FdaApplicationNumber%>" /></div>
</div>

<div class="clearfix">
    <label for="form-rems-approved" class="form-label">FDA REMS Approved On</label>
    <div class="form-input"><input type="date" id="form-rems-approved" name="rems-approved" value="<%=((item.RemsApproved == DateTime.MinValue || item.RemsApproved == null) ? "" : this.Server.HtmlEncode(item.RemsApproved.Value.ToString("yyyy-MM-dd")))%>" /></div>
</div>

<div class="clearfix">
    <label for="form-rems-updated" class="form-label">FDA REMS Updated On</label>
    <div class="form-input"><input type="date" id="form-rems-updated" name="rems-updated" value="<%=((item.RemsUpdated == null) ? "" : this.Server.HtmlEncode(item.RemsUpdated.Value.ToString("yyyy-MM-dd")))%>" /></div>
</div>

<div class="clearfix">
    <label for="form-active" class="form-label">Active</label>
    <div class="form-input">
		<select id="form-active" name="active">
			<option value="true"<%=((this.item.Active) ? " selected=\"selected\"" : "") %>>Yes</option>
			<option value="false"<%=((!this.item.Active) ? " selected=\"selected\"" : "") %>>No</option>
		</select>
	</div>
</div>

<% if( item.ID.HasValue ) { %>
<div class="clearfix form-padding">
	<h2>Versions</h2>
    <table class="display versions-table">
        <thead>
            <tr>
				<th>#</th>
				<th>Date</th>
                <th>User</th>
				<th>Status</th>
				<th>Message</th>
            </tr>
        </thead>
        <tbody>
		<% foreach( var v in this.Versions ) {
		%>
            <tr data-id="<%=v.ID%>">
				<td><%=v.Version %></td>
				<td><%=v.Updated.ToShortDateString()%></td>
				<td><%=Lib.Systems.Security.GetProfileForUser(v.UpdatedBy).PrimaryContact.Name%></td>
				<td><%=v.Status %></td>
				<td><%=v.Message%></td>
            </tr>
		<% } %>
        </tbody>
    </table>
</div>

<% } %>