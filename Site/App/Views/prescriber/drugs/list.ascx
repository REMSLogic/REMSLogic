<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="list.ascx.cs" Inherits="Site.App.Views.prescriber.drugs.list" %>
<!-- DATATABLES CSS -->
<link rel="stylesheet" media="screen" href="/App/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/App/js/lib/datatables/js/jquery.dataTables.js"></script>
<script type="text/javascript">
    $(window).bind('page-animation-completed', function () {
        $('#drugs-table').dataTable({
            "sPaginationType": "full_numbers",
            "bStateSave": true,
            "iCookieDuration": (60 * 60 * 24 * 30)
        });

        $(".remove-icon").click(function () {
            $doc = $(this);
            var id = parseInt($doc.attr('id').toString().substring(3));
            $listItem = $(eval('drug' + id.toString()));

            if (id) {
                $.ajax({
                    url: "/api/Common/DrugList/RemoveDrugFromList",
                    type: 'POST',
                    dataType: 'json',
                    data: { 'id': id },
                    success: function (response) {
                        $listItem.remove();
                    }
                });
            }
        });

        $(".star-icon").click(function () {
            $doc = $(this);
            var id = parseInt($doc.attr('id').toString().substring(3));
            $listItem = $(eval('fav' + id.toString()));
            if ($listItem.hasClass("fa-star")) { //Remove From My Favorites
                if (id) {
                    $.ajax({
                        url: "/api/Common/DrugList/RemoveDrugFromFavorites",
                        type: 'POST',
                        dataType: 'json',
                        data: { 'id': id },
                        success: function (response) {
                            $listItem.toggleClass("fa-star-o fa-star");
                        }
                    });
                }
            }
            else { //Add to My Favorites
                if (id) {
                    $.ajax({
                        url: "/api/Common/DrugList/AddDrugToFavorites",
                        type: 'POST',
                        dataType: 'json',
                        data: { 'id': id },
                        success: function (response) {
                            $listItem.toggleClass("fa-star-o fa-star");
                        }
                    });
                }
            }
        });

        $('#drugs-filter').keyup(function () {
            UpdateFilter();
        });

        $('.eoc-filter img').click(function () {
            $(this).toggleClass('off');
            UpdateFilter();
        }).each(function () {
            $(this).addClass('off');
        });

        $("#eoc-menu-toggle").click(function () {
            $btn = $(".eoc-menu-clear");
            //getting the next element
            $content = $("#divEoc-filter");
            //open up the content needed - toggle the slide- if visible, slide up, if not slidedown.
            $content.slideToggle(500, function () {
             if ($(this).css('display') == 'block') {
                    $(this).css('display', 'inline-block');
            }
                //execute this after slideToggle is done
                //change text of header based on visibility of content div
                //$header.text(function () {
                //change text based on condition
                //return $content.is(":visible") ? "Collapse" : "Expand";
                //});
                $content.is(":visible") ? $btn.show() : $btn.hide();
            });
        });
    });

    function UpdateFilter() {
        var v = $('#drugs-filter').val().toLowerCase();

        $('#man-drugs li.man-drug').show().each(function () {
            var $this = $(this);

            var drug = $('span.drugName', $this);

            if (drug.html().toLowerCase().indexOf(v) == -1)
                $this.hide();
        });

        $('.eoc-filter img:not(.off)').each(function () {
            $('#man-drugs li.man-drug:not([data-' + $(this).attr('data-eoc') + '])').hide();
        });
    }

    function ClearDrugListFilter() {
        $('.eoc-filter img').each(function () { $(this).addClass('off'); });
        $('#drugs-filter').val('');
        UpdateFilter();
    }

</script>
<!-- DATATABLES CSS END -->
<h1 class="page-title">
    My Drug List</h1>
<div class="container_12 clearfix">
    <div class="grid_12 portlet manage-drug-list leading">
        <header>
			<h2 class="title-header">My Drug List</h2>
                <div class="drug-list-options">
                    <%--<span class="search-input-drugs"><input type="text" name="Search" placeholder="Search..." rel="" class="fieldBlurred"></span>--%>
                    <span class="add-drugs-btn-2" id="eoc-menu-toggle"> <img class="eoc-menu-toggle" style="float: right; margin-top: 0px; margin-right: 0px;" src="/App/images/dashboard-icon.png" alt="Filter Menu" width="24" height="24"/></span>
                    <span class="add-drugs-btn">
                         <a href="#prescriber/drugs/select" class="grey-btn"><i class="fa fa-plus-circle pad-right"></i>Add Drugs</a>
                    </span>
                </div>
		</header>
       <%-- <a class="prev-btn back-button" href="#" style="margin-bottom: 10px;"><i class="fa fa-caret-left pad-right"></i>Previous</a>
        <a class="next-btn" href="#" style="margin-bottom: 10px;">More<i class="fa fa-caret-right pad-left"></i></a>--%>
        <div id="divEoc-filter" class="eoc-filter">
            <div class="filter-input mydruglist-inputwrap">
                    <input id="drugs-filter" type="text" value="" size="50" placeholder="Filter Drugs" />
                    <a class="eoc-menu-clear clear-btn" onclick="ClearDrugListFilter();" >Clear Filter</a>
            </div>

            <%foreach(var eoc in Eocs){
                if(DisplayEoc(eoc)){%>
                <div class="eoc-filter-item">
                    <img src="<%=eoc.LargeIcon%>" alt="<%=eoc.DisplayName %>" data-eoc="<%=eoc.Name %>" />
                    <%
                        var words = eoc.ShortDisplayName.ToUpperInvariant().Split(' ');
                        var result = String.Join("", words.Select(x => String.Format("<p class=\"label\">{0}</p>", x)));
                    %>
                    <%=result%>
                </div>
            <%}} %>
		</div>

        <div id="demo" class="clearfix">
            <%if (this.Drugs.Drugs == null)
            {%>
                <div class="drugName"><span class="name">You do not have any drugs</span></div>
            <%}else{%>
            <ul id="man-drugs" class="drug-content-wrap">
            <% foreach (var drug in this.Drugs.Drugs) { 
                   var pd_drug = new Lib.Data.Drug(drug.Id);%>
                <li class="man-drug" id="drug<%=drug.Id %>" data-drug-id="<%=drug.Id %>"<%=GetEOCData(pd_drug) %>>
                    <span class="drugName" style="display: none"><%=drug.DrugName %></span>
                    <ul class="clearfix" data-id="<%=drug.Id%>">
                        <li class="one"><a href="#common/drugs/detail?id=<%=drug.Id%>"><%=drug.DrugName%></a></li>
                        <li class="two"><strong>Last Update:</strong> <%=pd_drug.Updated.ToShortDateString() %></li>
                        <li class="three star-icon" id="str<%=drug.Id%>"><i id="fav<%=drug.Id%>" class="fa <%=drug.IsFav ? "fa-star" : "fa-star-o"%>"></i></li>
                        <li class="four remove-icon" id="rmv<%=drug.Id%>"><i class="fa fa-times-circle"></i></li>
                    </ul>
                </li>
            <% } %>
            </ul>
            <%}%>
        </div>
    </div>
</div>

<!-- OLD STUFF -->
<%--<table class="display" id="drugs-table">
                   <thead>
                    <tr>
                        <th>
                        </th>
                        <th>
                            Drug Name
                        </th>
                        <th>
                            Last Updated
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <% foreach (var drug in this.Drugs)
                       { %>
                    <tr data-id="<%=drug.ID%>">
                        <td>
                            <a href="#common/drugs/detail?id=<%=drug.ID%>" class="button">View</a>
                        </td>
                        <td>
                            <%=drug.GenericName%>
                        </td>
                        <td>
                           Last Update: <%=drug.Updated.ToShortDateString() %>
                        </td>
                    </tr>
                    <% } %>
                </tbody>
            </table>--%>