<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyDrugList.ascx.cs" Inherits="Site.App.Controls.Widgets.MyDrugList" %>

<script type="text/javascript">
    $(window).bind('page-animation-completed', function () {
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
    <img class="eoc-menu-toggle" style="float: right; margin-top: 0px; margin-right: -10px;" src="/images/dashboard-icon.png" alt="Filter Menu" width="24" height="24"/>
    <h2>My Favorite Drugs</h2>
</header>
<section>
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

    <%if (drugs == null || drugs.Drugs.Count == 0)
    {%>
        <div class="no-data">
            You do not have any favorites.  Please visit the Manage Drugs page to add drugs to your favorites list.
        </div>
        
    <%}else{%>
    <ul id="drug-list-widget" class="dashboard-drugs mydrug-list">
        <%foreach (var pd in drugs.Drugs){
            var pd_drug = new Lib.Data.Drug(pd.Id);%>
        <li class="ui-state-default" data-drug-id="<%=pd_drug.ID %>"<%=GetEOCData(pd_drug) %>>
            <%if(pd.PercentComplete >= 1){ %>
            <div class="compliance-holder green-alert" id="<%=pd_drug.ID %>">
                <span class="number-alert green-number"><%=String.Format("{0}/{1}", pd.UserEocsCount, pd.DrugEocsCount)%></span><span class="task-alert">Compliant</span>
            </div>
            <span class="compliance-green compliance-expand"></span>

            <% } else if(pd.PercentComplete > 0){ %>
            <div class="compliance-holder yellow-alert" id="<%=pd_drug.ID %>">
                <span class="number-alert yellow-number"><%=String.Format("{0}/{1}", pd.UserEocsCount, pd.DrugEocsCount)%></span><span class="task-alert">Compliant</span>
            </div>
            <span class="compliance-yellow compliance-expand"></span>

            <% } else { %>
            <div class="compliance-holder red-alert" id="<%=pd_drug.ID %>">
                <span class="number-alert red-number"><%=String.Format("{0}/{1}", pd.UserEocsCount, pd.DrugEocsCount)%></span><span class="task-alert">Compliant</span>
            </div>
            <span class="compliance-red compliance-expand"></span>
            <% } %>

            <div class="drugName">
                <a href="#common/drugs/detail?id=<%=pd_drug.ID %>"><span class="name"><%=pd_drug.GenericName %></span></a>
            </div>
        </li>
        <% } }%>
    </ul>
</section>