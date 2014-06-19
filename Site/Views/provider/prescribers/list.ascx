<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="list.ascx.cs" Inherits="Site.App.Views.provider.prescribers.list" %>
<%@ Import Namespace="Lib.Data" %>

<!-- DATATABLES CSS -->
<link rel="stylesheet" media="screen" href="/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/js/lib/datatables/js/jquery.dataTables.js"></script> 
<script type="text/javascript">
    var distLists = <%=DistributionLists%>;
    var dataTable;

    function buildList() {
        var nodes = $('#prescribers-table').dataTable().fnGetNodes();
        var count = nodes.length;
        var ids = new Array();

        for (var i = 0; i < count; i++) {
            if ($($(nodes[i]).find(':checkbox')[0]).prop('checked'))
                ids.push(parseInt($(nodes[i]).attr('data-id')));
        }

        return ids;
    }

    function setupTypeFilter(){
        var typeOptions = <%=GetPrescriberTypes()%>;

        var typeLabel = $("<label style=\"margin-right: 8px;\">").html("Type:");
        var typeSelect = $("<select style=\"margin-left: 4px; width: 80px;\">");

        $.each(typeOptions, function(index, value){
            typeSelect.append($('<option></option>').val(value).html(index));
        });

        typeLabel.append(typeSelect);
        typeLabel.prependTo('#prescribers-table_filter');
        typeSelect.change(function(){
            $('#prescribers-table').dataTable().fnFilter($(this).val(), 2);
        });
    }

    function setupSpecialtyFilter(){
        var typeOptions = <%=GetPrescriberSpecialties()%>;

        var label = $("<label style=\"margin-right: 8px;\">").html("Spec.:");
        var select = $("<select style=\"margin-left: 4px; width: 80px;\">");

        $.each(typeOptions, function(index, value){
            select.append($('<option></option>').val(value).html(index));
        });

        label.append(select);
        label.prependTo('#prescribers-table_filter');
        select.change(function(){
            $('#prescribers-table').dataTable().fnFilter($(this).val(), 3);
        });
    }

    function setupSelectAll(){
        var button = $("<input type=\"checkbox\" style=\"margin-left: 8px; margin-right: 32px;\">");
        var label = $("<span style=\"margin-left: 8px;\">All:</span>");

        label.appendTo('#prescribers-table_filter');
        button.appendTo('#prescribers-table_filter');
        button.click(function(){
            var nodes = $('#prescribers-table').dataTable().fnGetFilteredNodes();
            var count = nodes.length;
            var checked = $(this).prop('checked');

            for (var i = 0; i < count; i++) {
                $($(nodes[i]).find('.checker > span')[0]).attr('class', checked? 'checked' : '');
                $($(nodes[i]).find(':checkbox')[0]).prop('checked', checked);
            }
        });
    }

    function setupDistributionListTools(){
        // setup the dialog
        setupDistListDialogControls();
        initDistListDialog();

        // create more room for the additional controls
        $('#prescribers-table_length').css('padding', '3px 0 13px 0');
        $('#prescribers-table_length').css('width', '132px');
        $('#prescribers-table_filter').css('width','auto');
        $('#prescribers-table_filter').css('position','absolute');
        $('#prescribers-table_filter').css('right','0');
        $('#prescribers-table_filter').css('float','none');


        // add the additional controls
        setupSpecialtyFilter();
        setupTypeFilter();
        setupSelectAll();

        // configure the button to show the dialog
        $("#distribution-list-button").click(function () {
            $('#distribution-list-modal').dialog("open");
            return false;
        });
    }

    function setupDistListDialogControls() {
        $('#existing-list-name').autocomplete({
            source: distLists,
            minLength: 0,
            focus: function (event, ui) {
                $('#existing-list-name').val(ui.item.label);
                event.preventDefault();
            },
            select: function (event, ui) {
                event.preventDefault();

                $('#existing-list-name').val(ui.item.label);
                $('#existing-list-id').val(ui.item.value);
            }
        }).focus(function () {
            $(this).data("uiAutocomplete").search('');
        });

        $('#edit-list-name').autocomplete({
            source: distLists,
            minLength: 0,
            focus: function (event, ui) {
                $('#edit-list-name').val(ui.item.label);
                event.preventDefault();
            },
            select: function (event, ui) {
                event.preventDefault();

                $('#edit-list-name').val(ui.item.label);
                $('#edit-list-id').val(ui.item.value);
            }
        }).focus(function () {
            $(this).data("uiAutocomplete").search('');
        });

        $('#create-dist-list').click(function(){
            $.ajax({
                    url: '/api/Notifications/CreatePrescriberDistributionList',
                    data: {list_name: $('#new-list-name').val(), prescriber_ids: buildList()},
                    cache: false,
                })
                .always(function(){
                    $('#new-list-name').val('');
                    $('#distribution-list-modal').dialog('close');
                });
        });

        $('#add-to-dist-list').click(function(){
            $.ajax({
                    url: '/api/Notifications/AddPrescribersToDistributionList',
                    data: {list_id: $('#existing-list-id').val(), prescriber_ids: buildList()},
                    cache: false,
                })
                .always(function(){
                    $('#existing-list-name').val('');
                    $('#existing-list-id').val(0);
                    $('#distribution-list-modal').dialog('close');
                });
        });

        $('#edit-dist-list').click(function(){
            $('#distribution-list-modal').dialog("close");
            window.location = '#provider/dist-list/edit?id='+$('#edit-list-id').val();
        });
    }

    function initDistListDialog(){
        $('#distribution-list-modal').dialog({
            modal: true,
            width: 400,
            autoOpen: false,
            buttons: {
                "Cancel": function () {
                    $('#distribution-list-modal').dialog("close");
                }
            }
        });
    }

    $(window).bind('content-loaded', function () {
        $.fn.dataTableExt.oApi.fnGetFilteredNodes = function ( oSettings )
        {
            var anRows = [];
            for ( var i=0, iLen=oSettings.aiDisplay.length ; i<iLen ; i++ )
            {
                var nRow = oSettings.aoData[ oSettings.aiDisplay[i] ].nTr;
                anRows.push( nRow );
            }
            return anRows;
        };

        dataTable = $('#prescribers-table').dataTable({
            "sPaginationType": "full_numbers",
            "bStateSave": true,
            "iCookieDuration": (60 * 60 * 24 * 30)
        });

        $(".email-dialog-button").each(function () {
            $(this).click(function () {
                var id = $(this).attr('data-id');

                $('#prescriber-email-modal').dialog("open");

                return false;
            });
        });

        $('#prescriber-email-modal').dialog({
            modal: true,
            height: 230,
            width: 350,
            autoOpen: false,
            buttons: {
                "Send": function () {
                    $('#prescriber-email-modal').dialog("close");
                },
                "Cancel": function () {
                    $('#prescriber-email-modal').dialog("close");
                }
            }
        });

        setTimeout(function () {
            setupDistributionListTools();
        }, 100);
    });
</script> 
<!-- DATATABLES CSS END -->
<h1 class="page-title">Prescribers</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12 pro-prescriber-wrap">
        <div style="float: right; margin-top: 10px; margin-bottom: 10px;">
            <a href="#provider/prescribers/create" class="button" style="display: inline-block; margin-right: 6px;">Add Prescriber</a>
            <a href="" class="button" id="distribution-list-button" style="display: inline-block;">Distribution Lists</a>
        </div>
        <a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        <div id="demo" class="clearfix"> 
            <table class="display" id="prescribers-table"> 
                <thead> 
                    <tr>
                        <th></th>
                        <th>Last Name</th> 
                        <th>First Name</th>
                        <th>Type</th>
                        <th>Specialty</th>
                        <th>Facility</th>
                        <th></th>
                    </tr> 
                </thead> 
                <tbody>
                    <% foreach( var i in this.Prescribers ) { 
                        var prescriberProfile = GetPrescriberProfile(i);%>
                        
                        <% if(prescriberProfile != null){%>
                        <tr data-id="<%=prescriberProfile.ID%>"> 
                            <td>
                                <a href="#provider/prescribers/detail?id=<%=i.ID%>" class="button">Detail</a>
                                <a href="/api/Provider/Prescribers/Delete?id=<%=prescriberProfile.ID%>" class="ajax-button button" data-confirmtext="Are you sure you want to delete this facility?">Delete</a>
                                <a href="#" class="button email-dialog-button" data-id="<%=i.ID%>">Email</a>
                            </td>
                            <td><%=i.Profile.PrimaryContact.LastName%></td>
                            <td><%=i.Profile.PrimaryContact.FirstName %></td>
                            <td><%=GetPrescriberType(prescriberProfile)%></td>
                            <td><%=GetPrescriberSpecialty(i)%></td>
                            <td><%=GetPrescriberFacilityName(prescriberProfile)%></td>
                            <td><input type="checkbox" name="cb<%=i.ID%>" id="cb<%=i.ID%>" /></td>
                        </tr>
                        <%} %>
                    <% } %>
                </tbody>
            </table>
        </div>
    </div>
</div>
<div id="prescriber-email-modal" title="Send Email">
	<p>Please enter your message below:</p>
	<textarea rows="5" cols="40"></textarea>
</div>
<div id="distribution-list-modal" title="Distribution Lists">
    <div style="padding: 8px; text-align: left;">
        <p style="font-weight: bold;">Create New List</p>
        <label for="new-list-name" style="width: 70px; display: inline-block; text-align: right;">List Name:</label>
        <input type="text" id="new-list-name" name="new-list-name" style="width: 200px;" />
        <input type="button" value="Create" style="width: 60px;" id="create-dist-list" />

        <p style="margin-top: 24px; font-weight: bold;">Add to an Existing List</p>
        <label for="existing-list-name" style="width: 70px; display: inline-block; text-align: right;">Select List:</label>
        <input type="text" id="existing-list-name" name="existing-list-name" style="width: 200px;" />
        <input type="hidden" id="existing-list-id" name="existing-list-id"/>
        <input type="button" value="Add" id="add-to-dist-list" style="width: 60px;"/>
        
        <p style="margin-top: 24px; font-weight: bold;">Edit an Existing List</p>
        <label for="edit-list-name" style="width: 70px; display: inline-block; text-align: right;">Select List:</label>
        <input type="text" id="edit-list-name" name="edit-list-name" style="width: 200px;" />
        <input type="hidden" id="edit-list-id" name="edit-list-id"/>
        <input type="button" value="Edit" id="edit-dist-list" style="width: 60px;"/>
    </div>
</div>