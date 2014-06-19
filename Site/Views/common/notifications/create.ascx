<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="create.ascx.cs" Inherits="Site.App.Views.common.notifications.edit" %>
<%@ Import Namespace="Lib.Data" %>

<script type="text/javascript" src="/js/jquery.autogrowtextarea.js"></script>
<link rel="stylesheet" media="screen" href="/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/js/lib/datatables/js/jquery.dataTables.js"></script>

<script type = "text/javascript">
    function buildToList() {
        var sendToList = '';

        $.each($('#to-table tr'), function (index, value) {
            var id = $(value).attr('data-id');
            var type = $(value).attr('data-type');

            if (sendToList !== '')
                sendToList += ',';

            sendToList += '{"RecipientType":' + type + ',"Id":' + id + '}';
        });

        $('#send-to-id').val('[' + sendToList + ']');

        alert($('#send-to-id').val());
    }

    $(window).bind('content-loaded', function () {
        $('#send-to').autocomplete({
            source: '/api/Notifications/SendToAutocomplete',
            disabled: false,
            focus: function (event, ui) {
                $('#send-to').val(ui.item.label);
                event.preventDefault();
            },
            select: function (event, ui) {
                event.preventDefault();
                $('#to-list').css('display', 'block');
                $('#send-to').val('');
                $('#send-to-id').val(ui.item.value.id);

                var $table = $('#to-table');
                var $row = $('<tr data-id="' + ui.item.value.id + '" data-type="' + ui.item.value.type + '"><td>' + ui.item.label + '</td></tr>');
                var $cell = $('<td style="text-align: right;"></td>');
                var $button = $('<input type="button" value="Remove" style="margin-bottom: 8px;"></input>');

                $button.click(function () {
                    $($(this).parents('tr')[0]).remove();

                    if ($('#to-list').find('tr').length == 0)
                        $('#to-list').css('display', 'none');
                });

                $cell.append($button);
                $row.append($cell);
                $table.append($row);
            }
        });

        $('#notification-body').autoGrow();

        $('#link').autocomplete({
            focus: function (event, ui) {
                $('#link').val(ui.item.label);
                event.preventDefault();
            },
            select: function (event, ui) {
                event.preventDefault();
                $('#link').val(ui.item.label);
                $('#link-value').val(ui.item.value);
            }
        });

        $('#link-type').change(function (d) {
            var type = $('#link-type').val();

            if (type === 'none') {
                $('#link').prop('disabled', 'disabled').val('');
                $('#link').autocomplete({ disabled: true });
            }
            else {
                $('#link').prop('disabled', '').val('');
                $('#link').autocomplete({
                    source: '/api/Notifications/LinkAutocomplete?link_type=' + type,
                    disabled: false
                });
            }
        });


    });
</script>

<h1 class="page-title">Send Notification</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
        <a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        <form class="form has-validation ajax-form" action="/api/Notifications/Create" onsubmit="JavaScript: buildToList();">
            <input type="hidden" name="back-hash" value="<%=BackHash %>" />
            <input type="hidden" name="parent-type" value="<%=ParentType %>" />
            <input type="hidden" name="parent-id" value="<%=ParentID %>" />

            <div class="clearfix">
                <label for="send-to" class="form-label">Send To <em>*</em></label>
                <input type="hidden" id="send-to-id" name="send-to-id" />
                <div class="form-input">
                    <div><input type="text" id="send-to" placeholder="Send To" /></div>
                    <div id="to-list" style="background-color: #ffffff; border-top: 1px dashed #DDDDDD; display: none; margin-top: 47px;padding: 16px;">
                        <table id="to-table" style="width: 100%;">
                        </table>
                    </div>
                </div>
            </div>

            <div class="clearfix">
                <label for="is-important" class="form-label">Is Important</label>
                <div class="form-input"><div class="checkgroup"><input type="checkbox" id="is-important" name="is-important"/></div></div>
            </div>

            <div class="clearfix">
                <label for="notification-body" class="form-label">Message <em>*</em></label>
                <div class="form-input form-textarea"><textarea id="notification-body" name="notification-body" required="required" rows="5" placeholder="Enter your message..."></textarea></div>
            </div>

            <div class="clearfix">
                <label for="link-type" class="form-label">Link Type</label>
                <div class="form-input"><select id="link-type" name="link-type"><option selected="selected" value="none">No Link</option><option value="drug">Drug</option><option value="company">Drug Company</option><option value="external">External</option></select></div>
            </div>

            <div class="clearfix">
                <label for="link" class="form-label">Link</label>
                <input type="hidden" id="link-value" name="link-value" />
                <div class="form-input"><input type="text" id="link" name="link" placeholder="Related Link" disabled="disabled" /></div>
            </div>

            <div class="form-action clearfix">
                <button class="button" type="submit">Send</button>
                <button class="button" type="reset">Reset</button>
            </div>
        </form>
    </div>
</div>