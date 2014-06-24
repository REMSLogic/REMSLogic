<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="link.ascx.cs" Inherits="Site.App.Views.admin.drugs.drugs.link" %>

<script type="text/javascript">
	$(window).bind('content-loaded', function ()
	{
		$('#btnUpload').click(function ()
		{
			$('#form-file-container').show();
		});
	});

	function UploadCallBack()
	{
		$('#form-file-container').hide();
	}
</script>
<h1 class="page-title">Edit Link</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
		<form class="form has-validation ajax-form" action="/api/Admin/Drugs/Link/Edit?id=<%=((item.ID == null) ? 0 : item.ID)%>&drug-id=<%=this.Drug.ID %>">
			
			<div class="clearfix">
                <label for="form-type" class="form-label">Type <em>*</em></label>
                <div class="form-input">
					<select id="form-type" name="type">
						<option value="">None</option>
                        <% foreach( var type in LinkTypes ) { %>
                        <option value="<%=type %>"<%=((!string.IsNullOrEmpty(this.item.Type) && this.item.Type.ToLower() == type.ToLower()) ? " selected" : "") %>><%=type%></option>
                        <% } %>
					</select>
				</div>
            </div>

			<div class="clearfix">
                <label for="form-text" class="form-label">Text <em>*</em></label>
                <div class="form-input"><textarea id="form-text" name="text" required="required" placeholder="Enter the text"><%=item.Text%></textarea></div>
            </div>

			<div class="clearfix">
                <label for="form-help-text" class="form-label">Help Text</label>
                <div class="form-input"><input type="text" id="form-help-text" name="help-text" required="required" placeholder="Enter the help text" value="<%=item.HelpText%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-help-text" class="form-label">Date <em>*</em></label>
                <div class="form-input"><input type="date" id="form-date" name="date" required="required" placeholder="Enter the last updated date of the link" value="<%=(item.Date == null) ? DateTime.Now.ToShortDateString() : item.Date.Value.ToShortDateString()%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-link" class="form-label">URL</label>
                <div class="form-input has-button">
					<input type="url" id="form-link" name="link" placeholder="Enter the URL" value="<%=item.Link%>" />
					<button class="button" id="btnUpload" type="button">Upload</button>
				</div>
            </div>

			<div class="clearfix" id="form-file-container" style="display: none;">
                <label for="form-file" class="form-label">File Upload</label>
                <div class="form-input"><input type="file" id="form-file" name="file" data-url="/api/Admin/Files/Upload" data-update-field="form-link" data-callback="UploadCallBack" data-form-data='{"parent-type": "drug", "parent-id": <%=this.Drug.ID%>}' placeholder="Select an File" /></div>
            </div>

			<div class="form-action clearfix">
                <button class="button" type="submit">Save</button>
                <button class="button" type="reset">Reset</button>
            </div>
		</form>
	</div>
</div>