<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit-filter.ascx.cs" Inherits="Site.App.Views.dev.reports.edit_filter" %>

<script type="text/javascript" src="/js/jquery.autogrowtextarea.js"></script>
<script type="text/javascript">
	$(window).bind('content-loaded', function () {
		$('textarea').autoGrow();
	});
</script>
<h1 class="page-title"><%=((this.item.ID.HasValue) ? "Edit " + item.DisplayName : "New Output") %></h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
		<form class="form has-validation ajax-form" action="/api/Dev/ReportFilters/Edit?id=<%=((item.ID == null) ? 0 : item.ID)%>">
			<input type="hidden" name="report-id" value="<%=item.ReportID %>" />

			<div class="clearfix">
                <label for="form-display-name" class="form-label">Display Name <em>*</em></label>
                <div class="form-input"><input type="text" id="form-display-name" name="display-name" required="required" placeholder="Enter the display name" value="<%=item.DisplayName%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-placeholder-text" class="form-label">Placeholder Text</label>
                <div class="form-input"><input type="text" id="form-placeholder-text" name="placeholder-text" placeholder="Enter the placeholder text" value="<%=item.PlaceholderText%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-help-text" class="form-label">Help Text</label>
                <div class="form-input"><input type="text" id="form-help-text" name="help-text" placeholder="Enter the help text" value="<%=item.HelpText%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-parameter-name" class="form-label">Parameter Name <em>*</em></label>
                <div class="form-input"><input type="text" id="form-parameter-name" name="parameter-name" required="required" placeholder="Enter the parameter name" value="<%=item.ParameterName%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-type" class="form-label">Type <em>*</em></label>
                <div class="form-input">
					<select id="form-type" name="type" required="required">
						<option value=""></option>
						<option value="1"<%=((item.Type == Lib.Data.ReportFilter.FilterTypes.String) ? " selected=\"selected\"" : "") %>>String</option>
						<option value="2"<%=((item.Type == Lib.Data.ReportFilter.FilterTypes.Integer) ? " selected=\"selected\"" : "") %>>Integer</option>
						<option value="3"<%=((item.Type == Lib.Data.ReportFilter.FilterTypes.DateTime) ? " selected=\"selected\"" : "") %>>DateTime</option>
					</select>
				</div>
            </div>

			<div class="clearfix">
                <label for="form-parameters" class="form-label">Parameters</label>
                <div class="form-input form-textarea"><textarea id="form-parameters" name="parameters" rows="5" placeholder="UI Parameters"><%=item.Parameters%></textarea></div>
            </div>

			<div class="form-action clearfix">
                <button class="button" type="submit">Save</button>
                <button class="button" type="reset">Reset</button>
            </div>
		</form>
	</div>
</div>