<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit-output.ascx.cs" Inherits="Site.App.Views.dev.reports.edit_output" %>

<script type="text/javascript" src="/js/jquery.autogrowtextarea.js"></script>
<script type="text/javascript">
	$(window).bind('content-loaded', function () {
		$('textarea').autoGrow();
	});
</script>
<h1 class="page-title"><%=((this.item.ID.HasValue) ? "Edit " + item.Name : "New Output") %></h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
		<form class="form has-validation ajax-form" action="/api/Dev/ReportOutputs/Edit?id=<%=((item.ID == null) ? 0 : item.ID)%>">
			<input type="hidden" name="report-id" value="<%=item.ReportID %>" />

			<div class="clearfix">
                <label for="form-name" class="form-label">Name <em>*</em></label>
                <div class="form-input"><input type="text" id="form-name" name="name" required="required" placeholder="Enter the name" value="<%=item.Name%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-sql-text" class="form-label">SQL Text <em>*</em></label>
                <div class="form-input form-textarea"><textarea id="form-sql-text" name="sql-text" required="required" rows="5" placeholder="SQL Command"><%=item.SqlText %></textarea></div>
            </div>

			<div class="clearfix">
                <label for="form-sql-type" class="form-label">SQL Type <em>*</em></label>
                <div class="form-input">
					<select id="form-sql-type" name="sql-type" required="required">
						<option value=""></option>
						<option value="text"<%=((item.SqlType == "text") ? " selected=\"selected\"" : "") %>>Text</option>
						<option value="stored-procedure"<%=((item.SqlType == "stored-procedure") ? " selected=\"selected\"" : "") %>>Stored Procedure</option>
					</select>
				</div>
            </div>

			<div class="clearfix">
                <label for="form-type" class="form-label">Type <em>*</em></label>
                <div class="form-input">
					<select id="form-type" name="type" required="required">
						<option value=""></option>
						<option value="1"<%=((item.Type == Lib.Data.ReportOutput.OutputTypes.Table) ? " selected=\"selected\"" : "") %>>Table</option>
						<option value="2"<%=((item.Type == Lib.Data.ReportOutput.OutputTypes.PieChart) ? " selected=\"selected\"" : "") %>>PieChart</option>
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