<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="link.ascx.cs" Inherits="Site.App.Views.admin.dsq.link" %>
<%@ Import Namespace="RemsLogic.Model" %>
<%@ Import Namespace="RemsLogic.Model.Compliance" %>

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
            $('#form-text-container').hide();
        }
        else if ($('#form-type').val() == 'url') {
            $('#form-value-holder').show();
            $('#btnUpload').show();
            $('#form-text-container').hide();
        }
        else if ($('#form-type').val() == 'upload') {
            $('#form-text-container').hide();
            $('#form-value-holder').show();
            $('#form-file-container').show();
            $('#btnUpload').show();
        }
        else if ($('#form-type').val() == 'text') {
            $('#form-value-holder').hide();
            $('#btnUpload').hide();
            $('#form-file-container').hide();
            $('#form-text-container').show();
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
        <form class="form has-validation ajax-form" action="/api/Dev/DSQ/Link/Edit?id=<%=Link.Id%>&drug-id=<%=Link.DrugId%>&question-id=<%=Link.QuestionId%>">
            
            <div class="clearfix">
                <label for="form-type" class="form-label">Type <em>*</em></label>
                <div class="form-input">
                    <select id="form-type" name="type">
                        <option value="">Please Select</option>
                        <option value="phone"<%=(Link.LinkType == "phone" ? " selected=\"selected\"" : "") %>>Phone</option>
                        <option value="url"<%=(Link.LinkType == "url" ? " selected=\"selected\"" : "") %>>URL</option>
                        <option value="upload"<%=(Link.LinkType == "upload" ? " selected=\"selected\"" : "") %>>Upload</option>
                        <option value="text" <%=(Link.LinkType == "text"? "selected=\"selected\"" : "")%>>Text</option>
                    </select>
                </div>
            </div>

            <div class="clearfix">
                <label for="form-label" class="form-label">Label <em>*</em></label>
                <div class="form-input"><input type="text" id="form-label" name="label" required="required" placeholder="Enter the label" value="<%=Link.Label%>" /></div>
            </div>

            <div class="clearfix">
                <label for="form-help-text" class="form-label">Help Text <span style="font-style: italic; font-size: 12px; color: #999999">(450 character limit)</span></label>
                <div class="form-input"><input type="text" id="form-help-text" name="help-text" placeholder="Enter the help text" maxlength="450" value="<%=Link.HelpText%>" /></div>
            </div>

            <div class="clearfix">
                <label for="form-help-text" class="form-label">Date <em>*</em></label>
                <div class="form-input"><input type="date" id="form-date" name="date" required="required" m placeholder="Enter the last updated date of the link" value="<%=(Link.Date == null) ? DateTime.Now.ToString("yyyy-MM-dd") : Link.Date.Value.ToString("yyyy-MM-dd")%>" /></div>
            </div>

            <div class="clearfix" id="form-value-holder">
                <label for="form-value" class="form-label">URL</label>
                <div class="form-input">
                    <input type="text" id="form-value" name="value" placeholder="Enter the value" value="<%=Link.Value%>" />
                </div>
            </div>
            
            <div class="clearfix" id="form-text-container" style="display: none;">
                <label for="form-text" class="form-label">Text</label>
                <div class="form-input">
                    <textarea name="text" id="form-text" class="auto-grow" rows="5" cols="40"><%=Link.Value%></textarea>
                </div>
            </div>

            <div class="clearfix" id="form-file-container" style="display: none;">
                <label for="form-file" class="form-label">File Upload</label>
                <div class="form-input"><input type="file" id="form-file" name="file" data-url="/api/Dev/DSQ/Link/Upload" data-update-field="form-link" data-callback="UploadCallBack" data-form-data='{"drug-id": <%=Link.DrugId%>}' placeholder="Select an File" /></div>
            </div>
            
            <div class="clearfix">
                <label for="form-type" class="form-label">EOC <em>*</em></label>
                <div class="form-input">
                    <select id="form-eoc" name="eoc_id">
                        <option value="0">None</option>
                        
                        <%foreach(Eoc eoc in Eocs){%>
                            <option value="<%=eoc.Id%>" <%=(eoc.Id == Link.EocId)? "selected=\"selected\"" : String.Empty%>><%=eoc.DisplayName%></option>
                        <%} %>
                    </select>
                </div>
            </div>
            
            <div class="clearfix">
                <label for="form-required" class="form-label">Is Required <em>*</em></label>
                <div class="form-input">
                    <select id="form-required" name="required">
                        <option value="No">No</option>
                        <option value="Yes" <%=Link.IsRequired? "selected=\"selected\"" : String.Empty %>>Yes</option>
                    </select>
                </div>
            </div>
            
            <div class="clearfix">
                <label for="form-prereq" class="form-label">Has Prerequisite <em>*</em></label>
                <div class="form-input">
                    <select id="form-prereq" name="prereq">
                        <option value="No">No</option>
                        <option value="Yes" <%=Link.HasPrereq? "selected=\"selected\"" : String.Empty %>>Yes</option>
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