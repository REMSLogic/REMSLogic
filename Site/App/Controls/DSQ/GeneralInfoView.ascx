<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GeneralInfoView.ascx.cs" Inherits="Site.App.Controls.DSQ.GeneralInfoView" %>

<div class="clearfix">
    <label for="form-generic-name" class="form-label">Drug Name</label>
    <div class="form-input"><span class="form-info"><%=item.GenericName%></span></div>
</div>

<div class="clearfix">
    <label for="form-rems-reason" class="form-label">REMS Program Purpose</label>
    <div class="form-input"><span class="form-info"><%=item.RemsReason%></span></div>
</div>

<div class="clearfix">
    <label for="form-indication" class="form-label">Indication</label>
    <div class="form-input"><span class="form-info"><%=item.Indication%></span></div>
</div>

<div class="clearfix">
    <label for="form-class-id" class="form-label">Class</label>
    <div class="form-input">
		<span class="form-info"><%=((item.ClassID == 1) ? "ETASU" : "Non-ETASU") %></span>
	</div>
</div>

<% if( this.TheSystem != null && !string.IsNullOrEmpty(this.TheSystem.Name) ) { %>
<div class="clearfix">
    <label for="form-system-id" class="form-label">Shared System</label>
    <div class="form-input">
		<span class="form-info"><%=this.TheSystem.Name%></span>
	</div>
</div>
<% } %>

<!--
<div class="clearfix">
    <label for="form-class-id" class="form-label">FDA Updated On</label>
    <div class="form-input">
		<span class="form-info"><%=((item.RemsUpdated == null) ? item.Updated.ToShortDateString() : item.RemsUpdated.Value.ToShortDateString())%></span>
	</div>
</div>
-->

<div class="clearfix form-padding" style="padding-bottom: 1em;">
	<h2>Formulations</h2>
    <table class="display" id="formulations-table-view">
        <thead>
            <tr>
                <th></th>
				<th>Brand Name</th>
				<th>Formulation</th>
				<th>Company</th>
				<th>Company Phone</th>
            </tr>
        </thead>
        <tbody>
		<% foreach( var f in this.Formulations ) {
			var formulation = f.Formulation;
			var company = f.DrugCompany;
		%>
            <tr>
                <td><a href="#common/drugs/companies/detail?id=<%=company.ID%>&formulationId=<%=f.ID%>" class="button" style="margin: 6px 0;">View Company</a></td>
				<td><%=f.BrandName%></td>
				<td><%=formulation.Name%></td>
				<td><%=company.Name%></td>
				<td><%=f.DrugCompanyPhone%></td>
            </tr>
		<% } %>
        </tbody>
    </table>
</div>

<%if(!String.IsNullOrEmpty(item.RemsProgramUrl)){%>
<div class="clearfix">
    <label for="form-rems-url" class="form-label">REMS Program Website</label>
    <div class="form-input"><span class="form-info"><a href="<%=item.RemsProgramUrl%>" target="_blank">View</a></span></div>
</div>
<%}%>
<!--
<div class="clearfix form-padding versions-container">
	<h2 class="click-to-expand">Versions <em>(click to expand)</em></h2>
	<div class="versions-table">
		<% foreach( var v in this.Versions ) {
		%>
			<div>
				<span class="version-info">Version #<%=v.Version %> - Updated <%=v.Updated.ToShortDateString()%></span>
				<p><%=v.Message%></p>
			</div>
		<% } %>
	</div>
</div>

<div class="clearfix">
    <label for="form-eocs" class="form-label">EOCs</label>
    <div class="form-info drug-view-eocs">
		<% if( UserType == "prescriber" ) { %>
		<% if( item.HasEoc("patient-enrollment") ) { %>
		<span class="eoc-icon eoc-icon-patient-enrollment<%=(EocIsCertified("patient-enrollment") ? "" : " disabled") %>">
			<a href="/api/App/Users/Certified?id=<%=item.ID %>&eoc_name=patient-enrollment" class="ajax-button" onclick="return $(this).parent().is('.disabled');"><img src="/App/images/icons/PAEN.png" alt="Patient Enrollment" /></a>
		</span>
		<% } %>
		<% if( item.HasEoc("prescriber-enrollment") ) { %>
		<span class="eoc-icon eoc-icon-prescriber-enrollment<%=(EocIsCertified("prescriber-enrollment") ? "" : " disabled") %>">
			<a href="/api/App/Users/Certified?id=<%=item.ID %>&eoc_name=prescriber-enrollment" class="ajax-button" onclick="return $(this).parent().is('.disabled');"><img src="/App/images/icons/PREN.png" alt="Prescriber Enrollment" /></a>
		</span>
		<% } %>
		<% if( item.HasEoc("education-training") ) { %>
		<span class="eoc-icon eoc-icon-education-training<%=(EocIsCertified("education-training") ? "" : " disabled") %>">
			<a href="/api/App/Users/Certified?id=<%=item.ID %>&eoc_name=education-training" class="ajax-button" onclick="return $(this).parent().is('.disabled');"><img src="/App/images/icons/EDUCRT.png" alt="Education/Certification" /></a>
		</span>
		<% } %>
		<% } else if( UserType == "provider" ) { %>
		<% if( item.HasEoc("facility-pharmacy-enrollment") ) { %>
		<span class="eoc-icon eoc-icon-facility-pharmacy-enrollment<%=(EocIsCertified("facility-pharmacy-enrollment") ? "" : " disabled") %>">
			<a href="/api/App/Users/Certified?id=<%=item.ID %>&eoc_name=facility-pharmacy-enrollment" class="ajax-button" onclick="return $(this).parent().is('.disabled');"><img src="/App/images/icons/FP EN.png" alt="Facility/Pharmacy Enrollment" /></a>
		</span>
		<% } %>
		<% if( item.HasEoc("pharmacy-requirements") ) { %>
		<span class="eoc-icon eoc-icon-pharmacy-requirements<%=(EocIsCertified("pharmacy-requirements") ? "" : " disabled") %>">
			<a href="/api/App/Users/Certified?id=<%=item.ID %>&eoc_name=pharmacy-requirements" class="ajax-button" onclick="return $(this).parent().is('.disabled');"><img src="/App/images/icons/PR.png" alt="Pharmacy Requirements" /></a>
		</span>
		<% } %>
		<% } %>
	</div>
</div>
-->