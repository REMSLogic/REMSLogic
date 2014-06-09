<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FacilityDrugList.ascx.cs" Inherits="Site.App.Controls.Widgets.FacilityDrugList" %>

<script type="text/javascript">
    $(window).bind('content-loaded', function () {
        $(".compliance-expand").click(function () {
            $btn = $(this);
            $detail = $(eval('com' + $btn.attr('id').toString().substring(3)));
            $detail.animate({ width: 'toggle' }, 500);
        });
    });
</script>
<%
    var drugs = GetDrugList();
%>

<header class="portlet-header">
    <img class="fac-menu-toggle" style="float: right; margin-top: 0px; margin-right: -10px;" src="/App/images/dashboard-icon.png" alt="Filter Menu" width="24" height="24"/>
    <h2>Facility Drug List</h2>
</header>
<section>
    <div id="divFac-filter" class="fac-filter">
        <div class="filter-input mydruglist-inputwrap">
            <input id="drugs-filter" type="text" value="" size="50" placeholder="Filter Drugs" />
            <a class="fac-menu-clear clear-btn" onclick="ClearDrugListFilter();" >Clear Filter</a>
        </div>

        <%foreach(var eoc in Eocs){
            if(DisplayEoc(eoc)){%>
            <div class="fac-filter-item">
                <img src="<%=eoc.LargeIcon%>" alt="<%=eoc.DisplayName %>" data-eoc="<%=eoc.Name %>" />
                <%
                    var words = eoc.ShortDisplayName.ToUpperInvariant().Split(' ');
                    var result = String.Join("", words.Select(x => String.Format("<p class=\"label\">{0}</p>", x)));
                %>
                <%=result%>
            </div>
        <%}} %>
    </div>    
    <%if (drugs == null || drugs.Drugs.Count == 0)
    {%>
        <div class="no-data">
            You do not have any drugs on your list. Please visit the Manage Drugs page to add drugs to your facility drug list.
        </div>
        
    <%}else{%>
    <ul id="drug-list-widget" class="dashboard-drugs mydrug-list">
        <%foreach (var pd in drugs.Drugs){
            var pd_drug = new Lib.Data.Drug(pd.Id);%>
        <li class="ui-state-default" data-drug-id="<%=pd_drug.ID %>"<%=GetEOCData(pd_drug) %>>
            <div class="drugName">
                <a href="#common/drugs/detail?id=<%=pd_drug.ID %>"><span class="name"><%=pd_drug.GenericName %></span></a>
            </div>
        </li>
        <% } }%>
    </ul>
</section>