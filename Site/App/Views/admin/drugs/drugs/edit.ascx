<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Site.App.Views.admin.drugs.drugs.edit" %>

<!-- DATATABLES CSS -->
<link rel="stylesheet" media="screen" href="/App/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/App/js/lib/datatables/js/jquery.dataTables.js"></script> 
<script type="text/javascript" src="/App/js/lib/datatables/js/jquery.datatables.rowReordering.js"></script>
<script type="text/javascript">
	$(window).bind('content-loaded', function ()
	{
		$('#links-table').dataTable({
			"bPaginate": false,
			"aoColumnDefs": [
					{ "bSortable": false, "aTargets": [0], "sWidth": "106px" },
					{ "iDataSort": 1, "aTargets": [1], "sWidth": "20px" },
					{ "bSortable": false, "aTargets": [2] },
					{ "bSortable": false, "aTargets": [3], "sWidth": "350px" }
				],
			"bStateSave": true,
			"iCookieDuration": (60 * 60 * 24 * 30)
		}).rowReordering({
			sURL: "/api/Admin/Drugs/Link/Reorder",
			sRequestType: "POST",
			iIndexColumn: 1
		});

		$('#btnNewVersion').click(function ()
		{
			$("#dialog-confirm").dialog({
				resizable: false,
				height: 240,
				modal: true,
				buttons: {
					"Confirm": function ()
					{
						$('#form-new-version').val('true');
						$('#form-message').val($('#modal-message').val());
						$(this).dialog("close");

						$('#frmEditDrug').submit();
					},
					"Cancel": function ()
					{
						$(this).dialog("close");
					}
				}
			});
		});
	});
</script> 
<!-- DATATABLES CSS END -->
<h1 class="page-title"><%=((item.ID == null) ? "Create Drug" : "Edit " + item.GenericName)%></h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
		<form id="frmEditDrug" class="form has-validation ajax-form" action="/api/Admin/Drugs/Drug/Edit?id=<%=((item.ID == null) ? 0 : item.ID)%>">
			
			<div class="clearfix">
                <label for="form-generic-name" class="form-label">Drug Name <em>*</em></label>
                <div class="form-input"><input type="text" id="form-generic-name" name="generic-name" required="required" placeholder="Enter the Drug's generic name" value="<%=item.GenericName%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-brand-name" class="form-label">Brand Name <em>*</em></label>
                <div class="form-input"><input type="text" id="form-brand-name" name="brand-name" required="required" placeholder="Enter the Drug's brand name" value="<%=item.BrandName%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-formulation" class="form-label">Formulation <em>*</em></label>
                <div class="form-input"><input type="text" id="form-formulation" name="formulation" required="required" placeholder="Enter the Drug's formulation" value="<%=item.Formulation%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-company-id" class="form-label">Company/Sponsor <em>*</em></label>
                <div class="form-input">
					<select id="form-company-id" name="company-id" required="required">
						<option value="">Please select</option>
						<% foreach(var c in this.Companies) { %>
						<option value="<%=c.ID %>"<%=((item.CompanyID == c.ID.Value) ? " selected=\"selected\"" : "") %>><%=c.Name %></option>
						<% } %>
					</select>
				</div>
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

			<div class="clearfix">
                <label for="form-inpatient-requirements" class="form-label">Inpatient Requirements</label>
                <div class="form-input"><div class="checkgroup" ><input type="checkbox" id="form-inpatient-requirements" name="inpatient-requirements" value="true"<%=((item.InpatientRequirements) ? " checked=\"checked\"" : "") %> /></div></div>
            </div>

			<div class="clearfix">
                <label for="form-outpatient-requirements" class="form-label">Outpatient Requirements</label>
                <div class="form-input"><div class="checkgroup" ><input type="checkbox" id="form-outpatient-requirements" name="outpatient-requirements" value="true"<%=((item.OutpatientRequirements) ? " checked=\"checked\"" : "") %> /></div></div>
            </div>

			<div class="clearfix">
                <label for="form-fda-number" class="form-label">FDA Application number</label>
                <div class="form-input"><input type="text" id="form-fda-number" name="fda-number" placeholder="Enter the Drug's FDA Application Number" value="<%=item.FdaApplicationNumber%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-rems-approved" class="form-label">Date REMS Approved</label>
                <div class="form-input"><input type="date" id="form-rems-approved" name="rems-approved" value="<%=((item.RemsApproved == DateTime.MinValue || item.RemsApproved == null) ? "" : this.Server.HtmlEncode(item.RemsApproved.Value.ToString("yyyy-MM-dd")))%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-rems-updated" class="form-label">Last REMS Updated Date</label>
                <div class="form-input"><input type="date" id="form-rems-updated" name="rems-updated" value="<%=((item.RemsUpdated == null) ? "" : this.Server.HtmlEncode(item.RemsUpdated.Value.ToString("yyyy-MM-dd")))%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-active" class="form-label">Active</label>
                <div class="form-input">
					<select id="form-active" name="active">
						<option value="true">Yes</option>
						<option value="false">No</option>
					</select>
				</div>
            </div>

			<div class="form-action clearfix">
				<input type="hidden" id="form-new-version" name="new-version" value="false" />
				<input type="hidden" id="form-message" name="message" value="" />
                <button class="button" type="submit">Save</button>
				<button class="button" type="button" id="btnNewVersion">Save new Version</button>
                <button class="button" type="reset">Reset</button>
            </div>
		</form>
		<% if( this.item.ID.HasValue && this.item.ID.Value > 0 ) { %>
		<a href="#admin/drugs/drugs/link?drug-id=<%=this.item.ID %>" class="button" style="float: right; margin-top: 10px; margin-bottom: 10px;">Add Link</a>
		<h2>Links</h2>
		<div id="demo" class="clearfix">
            <table class="display" id="links-table">
                <thead>
                    <tr>
                        <th></th>
						<th></th>
                        <th>Type</th>
						<th>Text</th>
                    </tr>
                </thead>
                <tbody>
				<% foreach( var link in this.Links ) { %>
                    <tr data-id="<%=link.ID%>" data-position="<%=link.Order%>" id="<%=link.ID%>">
						<td><a href="#admin/drugs/drugs/link?id=<%=link.ID%>&drug-id=<%=this.item.ID %>" class="button">Edit</a> <a href="/api/Admin/Drugs/Link/Delete?id=<%=link.ID%>" class="ajax-button button" data-confirmtext="Are you sure you want to delete this link?">Delete</a></td>
						<td><%=link.Order%></td>
						<td><%=link.Type%></td>
						<td><%=link.Text%></td>
                    </tr>
				<% } %>
                </tbody>
            </table>
        </div>
		<% } %>
	</div>
</div>
<div id="dialog-confirm" title="New Version" style="display: none;">
    <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>Please enter a message describing the new version below:</p>
	<textarea id="modal-message" name="message" rows="5" cols="34"></textarea>
</div>