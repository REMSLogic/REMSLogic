<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Site.App.Views.dev.eocs.edit" %>

<script type="text/javascript">
(function ($) {
	var user_types_selected = [];

	/*$(window).bind('content-loaded', function () {
		$('input[name="chk-user-types"]').change(function () {

			var val = parseInt($(this).val());

			if ($(this).is(':checked'))
				user_types_selected.push(val);
			else
				user_types_selected.remove(val);

			UpdateHiddenField();
		});

		UpdateHiddenField();
	});*/

	function UpdateHiddenField() {
		var str = '';
		for (i = 0; i < user_types_selected.length; i++)
			str += ',' + user_types_selected[i].toString();

		str = str.substr(1);

		$('#form-user-types').val(str);
	}

})(jQuery);
</script>
<h1 class="page-title"><%=((item.ID != null) ? "Edit " + item.DisplayName : "Add EOC") %></h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
		<form class="form has-validation ajax-form" action="/api/Dev/Eoc/Edit?id=<%=((item.ID == null) ? 0 : item.ID)%>">
			
			<div class="clearfix">
                <label for="form-name" class="form-label">Name <em>*</em></label>
                <div class="form-input"><input type="text" id="form-name" name="name" required="required" placeholder="Enter the Name" value="<%=item.Name%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-display-name" class="form-label">Display Name <em>*</em></label>
                <div class="form-input"><input type="text" id="form-display-name" name="display-name" required="required" placeholder="Enter the Display Name" value="<%=item.DisplayName%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-user-types" class="form-label">For User Types <em>*</em></label>
                <div class="form-input">
					<div class="checkgroup wide">
					<% foreach( var userType in UserTypes ) { %>
						<label><input type="checkbox" name="user-types[]" value="<%=userType.ID.Value %>"<%=(HasUserType(userType.ID.Value)) ? " checked" : "" %> /> <%=userType.DisplayName %></label>
					<% } %>
					</div>
				</div>
            </div>

			<div class="form-action clearfix">
                <button class="button" type="submit">Save</button>
                <button class="button" type="reset">Reset</button>
            </div>
		</form>
	</div>
</div>