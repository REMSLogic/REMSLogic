<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ComplianceStatus.ascx.cs" Inherits="Site.App.Controls.Widgets.ComplianceStatus" %>

<script type="text/javascript">
    $(window).bind('content-loaded', function () {
        var pb_val = parseFloat($('#edu-progress-bar').attr('data-value'));
        $('#edu-progress-bar').radialProgressBar({
            color: ((pb_val >= 0.33) ? ((pb_val >= 1.0) ? 'green' : '#FAA61A') : 'red'),
            fill: false,
            showPercentage: true,
            value: pb_val
        });

        $('.edu-drug-list').each(function () {
            var $this = $(this);

            $('h5', $this).css('cursor', 'pointer').click(function () {
                $('ul', $this).toggle();
            });

            $('ul', $this).hide();
        });
    });
</script>

<%
var complete_drugs = new List<Lib.Data.Prescriber.PresciberDrugInfo>();
var in_progress_drugs = new List<Lib.Data.Prescriber.PresciberDrugInfo>();
var not_started_drugs = new List<Lib.Data.Prescriber.PresciberDrugInfo>();

var pdis = Lib.Systems.Security.GetCurrentPrescriber().GetDrugInfo();
float total_percent = 0.0f;

foreach (var pdi in pdis)
{
    var percent = pdi.PercentComplete;

    if (percent <= 0.0f)
        not_started_drugs.Add(pdi);
    else if (percent >= 100.0f)
        complete_drugs.Add(pdi);
    else
        in_progress_drugs.Add(pdi);
    total_percent += percent;
}

total_percent = total_percent / ((float)pdis.Count);
%>

<header class="portlet-header">
    <h2>Compliance Status</h2>
</header>
<section class="border-wrap">
    <div class="progress-bar">
        <div id="edu-progress-bar" data-value="<%=total_percent %>"></div>
        <div class="edu-progress-bar-info"><h4>Percent Complete</h4></div>
        <div class="clear-both"></div>
    </div>
    <%if (complete_drugs.Count > 0){ %>
    <div class="edu-drug-list">
        <h5>Completed (<%=complete_drugs.Count%>)</h5>
        <ul class="drug-list-dc">
        <% foreach (var pdi in complete_drugs){ %>
            <li class="forms-list"><a href="#common/drugs/detail?id=<%=pdi.DrugID %>" class="button">View</a><span class="forms-text drugName"><%=pdi.DrugName %></span></li>
        <%} %>
        </ul>
    </div>
    <%} 
      
    if (in_progress_drugs.Count > 0){ %>
    <div class="edu-drug-list">
        <h5>In-Progress (<%=in_progress_drugs.Count%>)</h5>
        <ul class="drug-list-dc">
        <%foreach (var pdi in in_progress_drugs){ %>
            <li class="forms-list"><a href="#common/drugs/detail?id=<%=pdi.DrugID %>" class="button">View</a><span class="forms-text drugName"><%=pdi.DrugName %></span></li>
        <% } %>
        </ul>
    </div>
    <% } 
       
    if (not_started_drugs.Count > 0){ %>
    <div class="edu-drug-list">
        <h5>Not Started (<%=not_started_drugs.Count%>)</h5>
        <ul class="drug-list-dc">
        <% foreach (var pdi in not_started_drugs){ %>
            <li class="forms-list"><a href="#common/drugs/detail?id=<%=pdi.DrugID %>" class="button">View</a><span class="forms-text drugName"><%=pdi.DrugName %></span></li>
        <% } %>
        </ul>
    </div>
    <%} %>
</section>