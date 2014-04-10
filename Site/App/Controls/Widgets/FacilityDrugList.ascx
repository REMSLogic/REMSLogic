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
var p = Lib.Data.Provider.FindByUser(Lib.Data.ProviderUser.FindByProfile(Lib.Data.UserProfile.FindByUser(Framework.Security.Manager.GetUser())));
var drugs = Lib.Data.Drug.FindByEoc("pharmacy-requirements");
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
        <div class="fac-filter-item">
            <img src="/App/images/icons/ETASU.png" alt="ETASU" data-eoc="etasu" />
            <p class="label">ETASU</p>
            <p class="label">&nbsp;</p>
        </div>
        <div class="fac-filter-item">
            <img src="/App/images/icons/FP EN.png" alt="Facility/Pharmacy Enrollment" data-eoc="facility-pharmacy-enrollment" />
            <p class="label">FACILITY</p>
            <p class="label">ENROLLMENT</p>
        </div>
        <div class="fac-filter-item">
            <img src="/App/images/icons/PAEN.png" alt="Patient Enrollment" data-eoc="patient-enrollment" />
            <p class="label">PATIENT</p>
            <p class="label">ENROLLMENT</p>
        </div>
        <div class="fac-filter-item">
            <img src="/App/images/icons/PREN.png" alt="Prescriber Enrollment" data-eoc="prescriber-enrollment" />
            <p class="label">PRESCRIBER</p>
            <p class="label">ENROLLMENT</p>
        </div>
        <div class="fac-filter-item">
            <img src="/App/images/icons/EDUCRT.png" alt="Education/Certification" data-eoc="education-training" />
            <p class="label">EDUCATION &amp;</p>
            <p class="label">CERTIFICATION</p>
        </div>
        <div class="fac-filter-item">
            <img src="/App/images/icons/MON.png" alt="Monitoring" data-eoc="monitoring-management" />
            <p class="label">MONITORING</p>
            <p class="label">&nbsp;</p>
        </div>
        <div class="fac-filter-item">
            <img src="/App/images/icons/MG.png" alt="Medication Guide" data-eoc="medication-guide" />
            <p class="label">MEDICATION</p>
            <p class="label">GUIDE</p>
        </div>
        <div class="fac-filter-item">
            <img src="/App/images/icons/IC.png" alt="Informed Consent" data-eoc="informed-consent" />
            <p class="label">INFORMED</p>
            <p class="label">CONSENT</p>
        </div>
        <div class="fac-filter-item">
            <img src="/App/images/icons/FD.png" alt="Forms and Documents" data-eoc="forms-documents" />
            <p class="label">FORMS &amp;</p>
            <p class="label">DOCUMENTS</p>
        </div>
        <div class="fac-filter-item">
            <img src="/App/images/icons/PR.png" alt="Pharmacy Requirements" data-eoc="pharmacy-requirements" />
            <p class="label">PHARMACY</p>
            <p class="label">REQUIREMENTS</p>
        </div>
    </div>
<%--<ul class="drug-list-dc">
<% foreach( var d in Lib.Systems.Lists.GetDrugsWithPharmacyRequirements() ) { %>
    <li><a href="#common/drugs/detail?id=<%=d.ID.Value %>" class="button">View</a><%=d.GenericName %></li>
<% } %>
</ul>--%>
<ul id="fac-drug-list-widget" class="dashboard-drugs mydrug-list">
        <%foreach (var pd in Lib.Systems.Lists.GetDrugsWithPharmacyRequirements())
          {
            var pd_drug = new Lib.Data.Drug(pd.ID);
            Random rndm = new Random();
            double percentComplete = rndm.NextDouble();
              %>
        <li class="ui-state-default" data-drug-id="<%=pd_drug.ID %>"<%=GetEOCData(pd_drug) %>>
            <%--<%if(pd.PercentComplete >= 1){ %>--%>
            <%if(percentComplete >= .9){ %>
            <div class="compliance-holder green-alert" id="<%=pd_drug.ID %>">
                <%--<span class="number-alert green-number"><%=(pd.PercentComplete * 100).ToString("0")%></span><span class="task-alert">% Compliant</span>--%>
                <span class="number-alert green-number"><%=(percentComplete * 100).ToString("0")%></span><span class="task-alert">% Compliant</span>
            </div>
            <span class="compliance-green compliance-expand"></span>

            <%--<% } else if(pd.PercentComplete >= 0.33){ %>--%>
            <% } else if(percentComplete >= 0.33){ %>
            <div class="compliance-holder yellow-alert" id="<%=pd_drug.ID %>">
                <%--<span class="number-alert "><%=(pd.PercentComplete * 100).ToString("0")%></span><span class="task-alert">% Compliant</span>--%>
                <span class="number-alert yellow-number"><%=(percentComplete * 100).ToString("0")%></span><span class="task-alert">% Compliant</span>
            </div>
            <span class="compliance-yellow compliance-expand"></span>

            <% } else { %>
            <div class="compliance-holder red-alert" id="<%=pd_drug.ID %>">
                <%--<span class="number-alert "><%=(pd.PercentComplete * 100).ToString("0")%></span><span class="task-alert">% Compliant</span>--%>
                <span class="number-alert red-number"><%=(percentComplete * 100).ToString("0")%></span><span class="task-alert">% Compliant</span>
            </div>
            <span class="compliance-red compliance-expand"></span>
            <% } %>

            <div class="drugName">
                <a href="#common/drugs/detail?id=<%=pd_drug.ID %>"><span class="name"><%=pd_drug.GenericName %></span></a>
            </div>
            </li>
        <% } %>
    </ul>
</section>
<%--<ul class="dashboard-drugs">
    <%
    foreach (var pd in Lib.Systems.Lists.GetDrugsWithPharmacyRequirements()){
    var pd_drug = new Lib.Data.Drug(pd.ID);%>
    <li class="ui-state-default" data-drug-id="<%=pd_drug.ID %>"<%=GetEOCData(pd_drug) %>>
        <div class="drugName">
            <a href="#common/drugs/detail?id=<%=pd_drug.ID %>"><span class="name"><%=pd_drug.GenericName %></span></a>
        </div>--%>
        <%--
        <span class="eoc-icons">
            <% if( pd_drug.HasEoc("etasu") ) { %>
            <span class="eoc-icon eoc-icon-etasu">
                <a href="#common/drugs/detail?id=<%=pd_drug.ID %>"><img src="/App/images/icons/ETASU.png" alt="ETASU" /></a>
            </span>
            <% } %>
            <% if( pd_drug.HasEoc("facility-pharmacy-enrollment") ) { %>
            <span class="eoc-icon eoc-icon-facility-pharmacy-enrollment">
                <a href="#common/drugs/detail?id=<%=pd_drug.ID %>"><img src="/App/images/icons/FP EN.png" alt="Facility/Pharmacy Enrollment" /></a>
            </span>
            <% } %>
            <% if( pd_drug.HasEoc("patient-enrollment") ) { %>
            <span class="eoc-icon eoc-icon-patient-enrollment">
                <a href="#common/drugs/detail?id=<%=pd_drug.ID %>"><img src="/App/images/icons/PAEN.png" alt="Patient Enrollment" /></a>
            </span>
            <% } %>
            <% if( pd_drug.HasEoc("prescriber-enrollment") ) { %>
            <span class="eoc-icon eoc-icon-prescriber-enrollment">
                <a href="#common/drugs/detail?id=<%=pd_drug.ID %>"><img src="/App/images/icons/PREN.png" alt="Prescriber Enrollment" /></a>
            </span>
            <% } %>
            <% if( pd_drug.HasEoc("education-training") ) { %>
            <span class="eoc-icon eoc-icon-education-training">
                <a href="#common/drugs/detail?id=<%=pd_drug.ID %>"><img src="/App/images/icons/EDUCRT.png" alt="Education/Certification" /></a>
            </span>
            <% } %>
            <% if( pd_drug.HasEoc("monitoring-management") ) { %>
            <span class="eoc-icon eoc-icon-monitoring-management">
                <a href="#common/drugs/detail?id=<%=pd_drug.ID %>"><img src="/App/images/icons/MON.png" alt="Monitoring" /></a>
            </span>
            <% } %>
            <% if( pd_drug.HasEoc("medication-guide") ) { %>
            <span class="eoc-icon eoc-icon-medication-guide">
                <a href="#common/drugs/detail?id=<%=pd_drug.ID %>"><img src="/App/images/icons/MG.png" alt="Medication Guide" /></a>
            </span>
            <% } %>
            <% if( pd_drug.HasEoc("informed-consent") ) { %>
            <span class="eoc-icon eoc-icon-informed-consent">
                <a href="#common/drugs/detail?id=<%=pd_drug.ID %>"><img src="/App/images/icons/IC.png" alt="Informed Consent" /></a>
            </span>
            <% } %>
            <% if( pd_drug.HasEoc("forms-documents") ) { %>
            <span class="eoc-icon eoc-icon-forms-documents">
                <a href="#common/drugs/detail?id=<%=pd_drug.ID %>"><img src="/App/images/icons/FD.png" alt="Forms and Documents" /></a>
            </span>
            <% } %>
            <% if( pd_drug.HasEoc("pharmacy-requirements") ) { %>
            <span class="eoc-icon eoc-icon-pharmacy-requirements">
                <a href="#common/drugs/detail?id=<%=pd_drug.ID %>"><img src="/App/images/icons/PR.png" alt="Pharmacy Requirements" /></a>
            </span>
            <% } %>
        </span>--%>
        <%--
        <span class="other">
            <span class="actions-lbl">Actions</span>
            <span class="actions"><a class="button" href="#common/drugs/detail?id=<%=pd_drug.ID %>">View</a></span>
        </span>
        --%>
    <%--</li>
    <% } %>
</ul>--%>