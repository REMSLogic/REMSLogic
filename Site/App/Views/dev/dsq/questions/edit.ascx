<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Site.App.Views.dev.dsq.questions.edit" %>

<script type="text/javascript" src="/App/js/jquery.autogrowtextarea.js"></script>
<script type="text/javascript">
	$(window).bind('content-loaded', function () {
		$('.autogrow textarea').autoGrow();

		$('#form-field-type').change(function () {
			UpdateAnswersDiv();
		});

		UpdateAnswersDiv();
	});

	function UpdateAnswersDiv() {
		var val = $('#form-field-type').val();

		$('.answers-div').hide();
		$('.eoc-div').hide();

		if (val == 'Checkbox List' || val == 'Radio Buttons' || val == 'DropDown')
			$('.answers-div').show();
		else if (val == 'EOC')
			$('.eoc-div').show();
	}
</script>
<h1 class="page-title">Edit Question</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
		<form class="form has-validation ajax-form" action="/api/Dev/DSQ/Question/Edit?id=<%=((item.ID == null) ? 0 : item.ID)%>">
			
			<div class="clearfix">
                <label for="form-section-id" class="form-label">Section <em>*</em></label>
                <div class="form-input">
					<select id="form-section-id" name="section-id" required="required">
						<option value="">Please Select</option>
						<%
							foreach( var s in Lib.Data.DSQ.Section.FindAll() )
							{
						%>
						<option value="<%=s.ID.Value %>"<%=((s.ID.Value == item.SectionID) ? " selected=\"selected\"" : "") %>><%=s.Name %></option>
						<%
							}
						%>
					</select>
				</div>
            </div>

			<div class="clearfix">
                <label for="form-parent-id" class="form-label">Parent Question</label>
                <div class="form-input">
					<select id="form-parent-id" name="parent-id">
						<option value="">None</option>
						<%
							foreach( var q in Lib.Data.DSQ.Question.FindPossibleParents() )
							{
								if (q.ID.HasValue && q.ID == item.ID)
									continue;
						%>
						<option value="<%=q.ID.Value %>"<%=((item.ParentID.HasValue && q.ID.Value == item.ParentID.Value) ? " selected=\"selected\"" : "") %>><%=q.Text %></option>
						<%
							}
						%>
					</select>
				</div>
            </div>

			<div class="clearfix">
                <label for="form-parent-checks" class="form-label">Parent Checks</label>
                <div class="form-input form-textarea"><textarea id="form-parent-checks" name="parent-checks" rows="5" cols="40" placeholder="Parent validation (Javascript)"><%=item.ParentChecks %></textarea></div>
            </div>

			<div class="clearfix">
                <label for="form-text" class="form-label">Text <em>*</em></label>
                <div class="form-input"><input type="text" id="form-text" name="text" required="required" placeholder="Enter the Text" value="<%=item.Text%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-viewtext" class="form-label">View Text</label>
                <div class="form-input"><input type="text" id="form-viewtext" name="viewtext" placeholder="Enter the View-only Text" value="<%=item.viewText%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-subtext" class="form-label">SubText</label>
                <div class="form-input"><input type="text" id="form-subtext" name="name" placeholder="Enter the Sub Text" value="<%=item.SubText%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-help-text" class="form-label">Help Text</label>
                <div class="form-input"><input type="text" id="form-help-text" name="help-text" placeholder="Enter the help Text" value="<%=item.HelpText%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-dev-name" class="form-label">Code Safe Name</label>
                <div class="form-input"><input type="text" id="form-dev-name" name="dev-name" placeholder="Enter a code safe name (a-z, A-Z, 0-9, -, _)" value="<%=item.DevName%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-required" class="form-label">Required</label>
                <div class="form-input"><div class="checkgroup"><input type="checkbox" id="form-required" value="1"<%=(item.Required) ? " checked=\"checked\"" : "" %> /></div></div>
            </div>

			<div class="clearfix">
                <label for="form-order" class="form-label">Order <em>*</em></label>
                <div class="form-input"><input type="text" id="form-order" name="order" required="required" placeholder="Enter the Order" value="<%=item.Order%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-field-type" class="form-label">Field Type <em>*</em></label>
                <div class="form-input">
					<select id="form-field-type" name="field-type" required="required">
						<option value="">Please Select</option>
						<%
							foreach (var ft in Lib.Data.DSQ.Question.GetValidFieldTypes())
							{
						%>
						<option value="<%=ft %>"<%=((ft == item.FieldType) ? " selected=\"selected\"" : "") %>><%=ft %></option>
						<%
							}
						%>
					</select>
				</div>
            </div>

			<div class="clearfix eoc-div">
                <label for="form-eoc" class="form-label">EOC</label>
                <div class="form-input">
					<select id="form-eoc" name="eoc" required="required">
						<option value="">Please Select</option>
						<%
							foreach (var eoc in Lib.Data.Eoc.FindAll())
							{
						%>
						<option value="<%=eoc.ID.Value %>"<%=((eoc.ID.Value.ToString() == item.Answers) ? " selected=\"selected\"" : "") %>><%=eoc.DisplayName %></option>
						<%
							}
						%>
					</select>
				</div>
            </div>

			<div class="clearfix answers-div">
                <label for="form-answers" class="form-label">Answers</label>
                <div class="form-input form-textarea autogrow"><textarea id="form-answers" name="answers" rows="5" cols="40" placeholder="Allowed Answers"><%=item.Answers %></textarea></div>
            </div>

			<div class="clearfix answers-div">
                <label for="form-hide-answers" class="form-label">Hide for Answers</label>
                <div class="form-input form-textarea autogrow"><textarea id="form-hide-answers" name="hide-answers" rows="5" cols="40" placeholder="Answers that cause this answer to hide"><%=item.HideForAnswers %></textarea></div>
            </div>

			<div class="clearfix answers-div">
                <label for="form-show-children-answers" class="form-label">Show Children for Answers</label>
                <div class="form-input form-textarea autogrow"><textarea id="form-show-children-answers" name="show-children-answers" rows="5" cols="40" placeholder="Answers that cause children to show even if this question is hidden"><%=item.HideForAnswers %></textarea></div>
            </div>

			<div class="clearfix">
                <label for="form-hide-roles" class="form-label">Hide for Roles</label>
                <div class="form-input form-textarea autogrow"><textarea id="form-hide-roles" name="hide-roles" rows="5" cols="40" placeholder="Roles that can not see this answer"><%=item.HideForRoles %></textarea></div>
            </div>

			<div class="clearfix">
                <label for="form-show-roles" class="form-label">Show for Roles</label>
                <div class="form-input form-textarea autogrow"><textarea id="form-show-roles" name="show-roles" rows="5" cols="40" placeholder="Roles that can see this answer"><%=item.ShowForRoles %></textarea></div>
            </div>

			<div class="form-action clearfix">
                <button class="button" type="submit">Save</button>
                <button class="button" type="reset">Reset</button>
            </div>
		</form>
	</div>
</div>