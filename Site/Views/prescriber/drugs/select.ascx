<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="select.ascx.cs" Inherits="Site.App.Views.prescriber.drugs.select" %>
<script type="text/javascript" src="/js/views/prescriber/drugs/select.js"></script>
<h1 class="page-title">
    Drug Management</h1>
<div class="container_12 clearfix">
    <div class="grid_12 manage-drug-list">
        
        <section class="portlet grid_12 docs leading">
        <a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
			<section>
				<div class="grid_6 portlet drug-select">
					<header>
						<h2 class="title-header">Drugs Available</h2>
                          <div class="eoc-menu">
                            <span id="eoc-menu-toggle-avail">
                                <img src="/images/dashboard-icon.png" alt="Dashboard-icon" width="24" height="24" class="eoc-menu-toggle" style="padding-top: 8px;" />
                            </span>
                        </div>
					</header>
					<div class="filter">

				        <div id="divEoc-filter-avail" class="eoc-filter eoc-filter-avail">
                            <div class="filter-input mydruglist-inputwrap">
                                 <input id="available-drugs-filter" type="text" value="" size="50" placeholder="Filter Drugs" />
                                 <a class="eoc-menu-clear-avail clear-btn" onclick="ClearAvailDrugListFilter();" >Clear Filter</a>
                            </div>
                            <div style="clear:both;"></div>

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
					</div>
                    <br />

					<div>
						<ul id="available-drugs" class="connectedDrugSelection sortable-drugs addDrugList">
							<% foreach (var d in AvailableDrugs){ %>
							<li class="ui-state-default" id="drug<%=d.ID %>" data-drug-id="<%=d.ID %>"<%=GetEOCData(d) %>>
                                <span class="selectdrug-icon"><i id="add<%=d.ID %>" class="fa fa-plus-circle selectDrug-add drugIcon"></i></span>
								<a href="#common/drugs/detail?id=<%=d.ID %>"><span class="drugName wide"><%=d.GenericName %><span></span></span></a>
							</li>
							<% } %>
						</ul>
					</div>
				</div>
				<div class="grid_6 portlet drug-select">
					<header>
						<h2 class="title-header">My Drug List</h2>
                          <div class="eoc-menu">
                            <span id="eoc-menu-toggle-my">
                               <img src="/images/dashboard-icon.png" alt="Dashboard-icon" width="24" height="24" class="eoc-menu-toggle" style="padding-top: 8px;" />
                            </span>
                        </div>
					</header>
					<div class="filter">
				        <div id="divEoc-filter-my" class="eoc-filter eoc-filter-my">
                         <div class="filter-input mydruglist-inputwrap">
                                 <input id="my-drugs-filter" type="text" value="" size="50" placeholder="Filter Drugs">
                                 <a class="eoc-menu-clear-my clear-btn" onclick="ClearMyDrugListFilter();" >Clear Filter</a>
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
					</div>
                    <br />

					<div>
						<ul id="selected-drugs" class="connectedDrugSelection sortable-drugs addDrugList">
							<% foreach (var d in SelectedDrugs) { %>
							<li class="ui-state-default" id="drug<%=d.ID %>" data-drug-id="<%=d.ID %>"<%=GetEOCData(d) %>>
                                <span class="selectdrug-icon"><i id="rmv<%=d.ID %>" class="fa fa-times-circle myDrug-remove drugIcon"></i></span>
                                <a href="#common/drugs/detail?id=<%=d.ID %>"><span class="drugName wide"><%=d.GenericName %><span></span></span></a>
							</li>
							<% } %>
						</ul>
					</div>
				</div>
				<div class="clear"></div>
    		</section>
		</section>
    </div>
</div>
<script type="text/javascript">
    $(window).bind('content-loaded', function () {
        $("#eoc-menu-toggle-avail").click(function () {
            $header = $(this);
            $btn = $(".eoc-menu-clear-avail");
            //getting the next element
            $content = $("#divEoc-filter-avail");
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

        $("#eoc-menu-toggle-my").click(function () {
            $header = $(this);
            $btn = $(".eoc-menu-clear-my");
            //getting the next element
            $content = $("#divEoc-filter-my");
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
</script>