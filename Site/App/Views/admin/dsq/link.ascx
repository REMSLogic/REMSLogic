<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="link.ascx.cs" Inherits="Site.App.Views.admin.dsq.link" %>
<%@ Import Namespace="RemsLogic.Model" %>

<script type="text/javascript">
	$(window).bind('content-loaded', function () {
		$('#btnUpload').click(function () {
			$('#form-file-container').show();
		});

		$('#form-type').change(function () {
			UpdateElements();
		});

		UpdateElements();
	});

	function UpdateElements() {
		$('#form-value-holder').hide();
		$('#form-value-holder label.form-label').html('URL <em>*</em>');
		$('#form-file-container').hide();
		$('#btnUpload').hide();

		if ($('#form-type').val() == 'phone') {
			$('#form-value-holder').show();
			$('#form-value-holder label.form-label').html('Phone <em>*</em>');
		}
		else if ($('#form-type').val() == 'url') {
			$('#form-value-holder').show();
			$('#btnUpload').show();
		}
		else if ($('#form-type').val() == 'upload') {
			$('#form-value-holder').show();
			$('#form-file-container').show();
			$('#btnUpload').show();
		}
	}

	function UploadCallBack() {
		$('#form-file-container').hide();
	}
</script>
<h1 class="page-title">Edit Link</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
		<form class="form has-validation ajax-form" action="/api/Dev/DSQ/Link/Edit?id=<%=((item.ID == null) ? 0 : item.ID)%>&drug-id=<%=this.item.DrugID %>&question-id=<%=this.item.QuestionID %>">
			
			<div class="clearfix">
                <label for="form-type" class="form-label">Type <em>*</em></label>
                <div class="form-input">
					<select id="form-type" name="type">
						<option value="">Please Select</option>
                        <option value="phone"<%=((!string.IsNullOrEmpty(item.Value) && !item.Value.StartsWith("http")) ? " selected=\"selected\"" : "") %>>Phone</option>
						<option value="url"<%=((!string.IsNullOrEmpty(item.Value) && item.Value.StartsWith("http")) ? " selected=\"selected\"" : "") %>>URL</option>
						<option value="upload">Upload</option>
					</select>
				</div>
            </div>

			<div class="clearfix">
                <label for="form-label" class="form-label">Label <em>*</em></label>
                <div class="form-input"><input type="text" id="form-label" name="label" required="required" placeholder="Enter the label" value="<%=item.Label%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-help-text" class="form-label">Help Text <span style="font-style: italic; font-size: 12px; color: #999999">(450 character limit)</span></label>
                <div class="form-input"><input type="text" id="form-help-text" name="help-text" placeholder="Enter the help text" maxlength="450" value="<%=item.HelpText%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-help-text" class="form-label">Date <em>*</em></label>
                <div class="form-input"><input type="date" id="form-date" name="date" required="required" m placeholder="Enter the last updated date of the link" value="<%=(item.Date == null) ? DateTime.Now.ToShortDateString() : item.Date.Value.ToShortDateString()%>" /></div>
            </div>

			<div class="clearfix" id="form-value-holder">
                <label for="form-value" class="form-label">URL</label>
                <div class="form-input has-button">
					<input type="text" id="form-value" name="value" placeholder="Enter the value" value="<%=item.Value%>" />
					<button class="button" id="btnUpload" type="button">Upload</button>
				</div>
            </div>

			<div class="clearfix" id="form-file-container" style="display: none;">
                <label for="form-file" class="form-label">File Upload</label>
                <div class="form-input"><input type="file" id="form-file" name="file" data-url="/api/Dev/DSQ/Link/Upload" data-update-field="form-link" data-callback="UploadCallBack" data-form-data='{"drug-id": <%=this.item.DrugID%>}' placeholder="Select an File" /></div>
            </div>
            
            <div class="clearfix">
                <label for="form-type" class="form-label">EOC <em>*</em></label>
                <div class="form-input">
                    <select id="form-eoc" name="eoc_id">
                        <option value="None">None</option>
                        
                        <%foreach(Eoc eoc in Eocs){%>
                            <option value="<%=eoc.Id%>"><%=eoc.DisplayName%></option>
                        <%} %>
                    </select>
                </div>
            </div>

			<div class="form-action clearfix">
                <button class="button" type="submit">Save</button>
                <button class="button" type="reset">Reset</button>
            </div>
		</form>
	</div>
</div>