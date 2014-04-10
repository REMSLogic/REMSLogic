<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Site.App.Views.admin.dsq.edit" %>

<!-- DATATABLES CSS -->
<link rel="stylesheet" media="screen" href="/App/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/App/js/lib/datatables/js/jquery.dataTables.js"></script> 
<script type="text/javascript" src="/App/js/jquery.autogrowtextarea.js"></script>
<script type="text/javascript">
	var default_section = <%=(this.SectionIndex >= 0) ? this.SectionIndex.ToString() : "null" %>;
</script>
<script type="text/javascript" src="/App/js/dsq.js"></script>

<h1 class="page-title"><%=((item.ID == null) ? "Create Drug" : "Edit " + item.GenericName)%></h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
		<form id="frmEditDrug" class="has-validation dsq-form" action="/api/Dev/DSQ/Drug/Edit?id=<%=((item == null || item.ID == null) ? 0 : item.ID)%>">

			<div class="clearfix">
				<site:DSQ ID="dsqView" GeneralInfoControlPath="~/App/Controls/DSQ/GeneralInfo.ascx" runat="server" />
			</div>

			<div class="form leading">
				<div class="form-action clearfix">
					<%	if( version != null && version.Status == "Pending" ) { %>
					<%		if( Framework.Security.Manager.HasRole("drug_version_approve") ) { %>
					<input type="hidden" id="form-drug-id" name="drug-id" value="<%=item.ID.Value %>" />
					<input type="hidden" id="form-drug-version-id" name="drug-version-id" value="<%=version.ID.Value %>" />
					<button class="button" type="button" id="btnApproveVersion">Approve Changes</button>
					<button class="button" type="button" id="btnDenyVersion">Deny Changes</button>
					<% } else { %>
					<!-- Pending Drug updates, but user has no permission to approve/deny changes -->
					<%		} %>
					<%	} else { %>
					<input type="hidden" id="form-new-version" name="new-version" value="false" />
					<input type="hidden" id="form-message" name="message" value="" />
					<%		if( Framework.Security.Manager.HasRole("drug_version_approve") ) { %>
					<button class="button" type="submit ajax">Save</button>
					<button class="button" type="button" id="btnNewVersion">Save new Version</button>
					<%		} else { %>
					<button class="button" type="button" id="btnNewVersion">Submit new Version</button>
					<%		} %>
					<button class="button" type="reset">Reset</button>
					<%	} %>
				</div>
			</div>
		</form>
	</div>
</div>
<div id="dialog-confirm" title="New Version" style="display: none;">
    <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>Please enter a message describing the new version below:</p>
	<textarea id="modal-message" name="message" rows="5" cols="34"></textarea>
</div>
<div id="dialog-approve-confirm" title="Approve Changes" style="display: none;">
    <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>You may enter a message below in regards to approving this version.</p>
	<textarea id="modal-approve-message" name="message" rows="5" cols="34"></textarea>
</div>
<div id="dialog-deny-confirm" title="Deny Changes" style="display: none;">
    <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>Please enter a message describing why these changes are being denied:</p>
	<textarea id="modal-deny-message" name="message" rows="5" cols="34"></textarea>
</div>