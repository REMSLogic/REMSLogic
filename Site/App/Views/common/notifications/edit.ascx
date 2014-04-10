<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Site.App.Views.common.notifications.edit" %>

<script type="text/javascript" src="/App/js/jquery.autogrowtextarea.js"></script>

<script type = "text/javascript">
    $(document).ready(function () {
        //var data = "Core Selectors Attributes Traversing Manipulation CSS Events Effects Ajax Utilities".split(" ");
        //var data = [{ label: "asdf", value: 1 }, { label: "aasdf", value: 2 }, { label: "aaasdf", value: 3}];
        var data = <%= SendToList %>;
        $('#send-to').autocomplete({
            source: data,
            select: function (event, ui) {
                event.preventDefault();
                $('#send-to').val(ui.item.label);
                $('#send-to-id').val(ui.item.value);
            }/*,
            open: function (event, ui) {
                $('.ui-autocomplete').prepend('<li class="ui-menu-item"><a class="ui-corner-all ui-add-new">All Prescribers</a></li>');
            }*/
        });

        $('#notification-body').autoGrow();
    });
</script>

<h1 class="page-title">Send Notification</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
		<form class="form has-validation ajax-form" action="/api/Notifications/Edit?id=<%=(Item.ID ?? 0)%>">
			<input type="hidden" name="back-hash" value="<%=this.BackHash %>" />
			<input type="hidden" name="parent-type" value="<%=this.ParentType %>" />
			<input type="hidden" name="parent-id" value="<%=this.ParentID %>" />

			<div class="clearfix">
			    <label for="send-to" class="form-label">Send To <em>*</em></label>
                <input type="hidden" id="send-to-id" name="send-to-id" />
                <div class="form-input"><input type="text" id="send-to" required="required" placeholder="Send To" /></div>
			</div>

            <div class="clearfix">
                <label for="notification-body" class="form-label">About you <em>*</em></label>
                <div class="form-input form-textarea"><textarea id="notification-body" name="notification-body" required="required" rows="5" placeholder="Enter your message..."></textarea></div>
            </div>

            <div class="form-action clearfix">
                <button class="button" type="submit">Send</button>
                <button class="button" type="reset">Reset</button>
            </div>
		</form>
	</div>
</div>