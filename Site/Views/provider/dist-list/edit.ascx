<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Site.App.Views.provider.dist_list.edit" %>

<!-- DATATABLES CSS -->
<link rel="stylesheet" media="screen" href="/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/js/lib/datatables/js/jquery.dataTables.js"></script> 
<script type="text/javascript">
    var dataTable;

    function buildList() {
        var nodes = $('#dist-list-table').dataTable().fnGetNodes();
        var count = nodes.length;
        var ids = new Array();

        for (var i = 0; i < count; i++) {
            if ($($(nodes[i]).find(':checkbox')[0]).prop('checked'))
                ids.push(parseInt($(nodes[i]).attr('data-id')));
        }

        return ids;
    }

    function setupTypeFilter(){
        var typeOptions = {
            Any: '',
            Doctor: 'Doctor',
            Nurse: 'Nurse'
        };

        var typeLabel = $("<label style=\"margin-right: 8px;\">").html("Type:");
        var typeSelect = $("<select style=\"margin-left: 4px; width: 80px;\">");

        $.each(typeOptions, function(index, value){
            typeSelect.append($('<option></option>').val(value).html(index));
        });

        typeLabel.append(typeSelect);
        typeLabel.prependTo('#dist-list-table_filter');
        typeSelect.change(function(){
            $('#dist-list-table').dataTable().fnFilter($(this).val(), 4);
        });
    }

    function setupSelectAll(){
        var button = $("<input type=\"checkbox\" style=\"margin-left: 8px; margin-right: 17px;\">");

        button.appendTo('#dist-list-table_filter');
        button.click(function(){
            var nodes = $('#dist-list-table').dataTable().fnGetFilteredNodes();
            var count = nodes.length;
            var checked = $(this).prop('checked');

            for (var i = 0; i < count; i++) {
                $($(nodes[i]).find('.checker > span')[0]).attr('class', checked? 'checked' : '');
                $($(nodes[i]).find(':checkbox')[0]).prop('checked', checked);
            }
        });
    }

    function setupDistributionListTools() {
        $('#save-changes').click(function(){
            $.ajax({
                    url: '/api/Notifications/UpdateDistributionList',
                    data: {list_id: <%=DistributionList.ID%>, prescriber_ids: buildList()},
                    cache: false,
                })
                .always(function(){
                    $('#existing-list-name').val('');
                    $('#existing-list-id').val(0);
                    $('#distribution-list-modal').dialog('close');
                });
        });

        // create more room for the additional controls
        $('#dist-list-table_filter').css('width','60%');

        // add the additional controls
        setupTypeFilter();
        setupSelectAll();
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

        dataTable = $('#dist-list-table').dataTable({
            "sPaginationType": "full_numbers",
            "bStateSave": true,
            "iCookieDuration": (60 * 60 * 24 * 30)
        });

        setupDistributionListTools();
    });
</script> 
<!-- DATATABLES CSS END -->
<h1 class="page-title">Edit Distribution List <%=DistributionList.Name %></h1>
<div class="container_12 clearfix leading">
    <div class="grid_12 facility-add-wrap">
        <div style="float: right; margin-top: 10px; margin-bottom: 10px;">
            <input id="save-changes" name="save-changes" type="button" style="display: inline-block; margin-right: 6px;" value="Save Changes" />
            <a href="#provider/prescribers/list" class="button" id="distribution-list-button" style="display: inline-block;">Cancel</a>
        </div>
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        <div id="demo" class="clearfix"> 
            <table class="display" id="dist-list-table"> 
                <thead> 
                    <tr>
                        <th>Name</th> 
						<th>Email</th>
						<th>Phone</th>
                        <th>Type</th>
                        <th></th>
                    </tr> 
                </thead> 
                <tbody>
					<% foreach( var i in this.Prescribers ) { %>
                    <tr data-id="<%=i.ID%>"> 
						<td><%=i.Profile.PrimaryContact.Name%></td>
						<td><%=i.Profile.PrimaryContact.Email%></td>
						<td><%=i.Profile.PrimaryContact.Phone%></td>
                        <td><%=GetPrescriberType(i)%></td>
                        <td><input type="checkbox" name="cb<%=i.ID%>" id="cb<%=i.ID%>" checked="checked" /></td>
                    </tr>
					<% } %>
                </tbody>
            </table>
        </div>
    </div>
</div>