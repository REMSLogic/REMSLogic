<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyDrugList.ascx.cs" Inherits="Site.App.Controls.Widgets.MyDrugList" %>

<script type="text/javascript">
    $(window).bind('content-loaded', function () {
        $(".compliance-expand").click(function () {
            $btn = $(this);
            $detail = $(eval('com' + $btn.attr('id').toString().substring(3)));

            $detail.animate({ width: 'toggle' }, 500);
            //            if ($btn.attr('src') === '/App/images/Warning_Arrow_Down.png') {
            //                $detail.animate({ width: "20px" }, 500);
            //                //$btn.attr('src', '/App/images/Warning_Arrow_Up.png');
            //            }
            //            else {
            //                $detail.animate({ width: "0px" }, 500);
            //                //$btn.attr('src', "/App/images/Warning_Arrow_Down.png");
            //            }
        });

        //		$("#drug-list-widget").sortable({
        //			placeholder: "sortable-placeholder",
        //			start: function (event, ui)
        //			{
        //				ui.item.startPos = ui.item.index();
        //			},
        //			stop: function handleDrop(event, ui)
        //			{
        //				var drugId = $(ui.item[0]).attr('data-drug-id');

        //				$.ajax({
        //					url: "/api/Prescriber/Drug/Reorder",
        //					type: 'POST',
        //					dataType: 'json',
        //					data: {
        //						'id': drugId,
        //						'fromPosition': ui.item.startPos + 1,
        //						'toPosition': ui.item.index() + 1
        //					}
        //				});
        //			}
        //		}).disableSelection();
    });
</script>

<%
    //var drugs = Lib.Data.PrescriberDrug.FindByPrescriber(Lib.Data.Prescriber.FindByProfile(Lib.Data.UserProfile.FindByUser(Framework.Security.Manager.GetUser())));
    //var drugs = Lib.Systems.Security.GetCurrentPrescriber().GetDrugInfo();
    var drugs = GetDrugList();
%>

<header class="portlet-header">
    <%--<a href="#prescriber/drugs/select" class="add-drugs-icon" style="float: right; margin-top: -4px; margin-right: -10px;"><img src="/App/images/dashboard-icon.png" alt="Dashboard-icon" width="24" height="24" /></a>--%>
    <img class="eoc-menu-toggle" style="float: right; margin-top: 0px; margin-right: -10px;" src="/App/images/dashboard-icon.png" alt="Filter Menu" width="24" height="24"/>
    <h2>My Drug List</h2>
</header>
<section>
    <div id="divEoc-filter" class="eoc-filter">
        <div class="filter-input mydruglist-inputwrap">
            <input id="drugs-filter" type="text" value="" size="50" placeholder="Filter Drugs" />
            <a class="eoc-menu-clear clear-btn" onclick="ClearDrugListFilter();" >Clear Filter</a>
        </div>
        <div class="eoc-filter-item">
            <img src="/App/images/icons/ETASU.png" alt="ETASU" data-eoc="etasu" />
            <p class="label">ETASU</p>
            <p class="label">&nbsp;</p>
        </div>
        <div class="eoc-filter-item">
            <img src="/App/images/icons/FP EN.png" alt="Facility/Pharmacy Enrollment" data-eoc="facility-pharmacy-enrollment" />
            <p class="label">FACILITY</p>
            <p class="label">ENROLLMENT</p>
        </div>
        <div class="eoc-filter-item">
            <img src="/App/images/icons/PAEN.png" alt="Patient Enrollment" data-eoc="patient-enrollment" />
            <p class="label">PATIENT</p>
            <p class="label">ENROLLMENT</p>
        </div>
        <div class="eoc-filter-item">
            <img src="/App/images/icons/PREN.png" alt="Prescriber Enrollment" data-eoc="prescriber-enrollment" />
            <p class="label">PRESCRIBER</p>
            <p class="label">ENROLLMENT</p>
        </div>
        <div class="eoc-filter-item">
            <img src="/App/images/icons/EDUCRT.png" alt="Education/Certification" data-eoc="education-training" />
            <p class="label">EDUCATION &amp;</p>
            <p class="label">CERTIFICATION</p>
        </div>
        <div class="eoc-filter-item">
            <img src="/App/images/icons/MON.png" alt="Monitoring" data-eoc="monitoring-management" />
            <p class="label">MONITORING</p>
            <p class="label">&nbsp;</p>
        </div>
        <div class="eoc-filter-item">
            <img src="/App/images/icons/MG.png" alt="Medication Guide" data-eoc="medication-guide" />
            <p class="label">MEDICATION</p>
            <p class="label">GUIDE</p>
        </div>
        <div class="eoc-filter-item">
            <img src="/App/images/icons/IC.png" alt="Informed Consent" data-eoc="informed-consent" />
            <p class="label">INFORMED</p>
            <p class="label">CONSENT</p>
        </div>
        <div class="eoc-filter-item">
            <img src="/App/images/icons/FD.png" alt="Forms and Documents" data-eoc="forms-documents" />
            <p class="label">FORMS &amp;</p>
            <p class="label">DOCUMENTS</p>
        </div>
        <div class="eoc-filter-item">
            <img src="/App/images/icons/PR.png" alt="Pharmacy Requirements" data-eoc="pharmacy-requirements" />
            <p class="label">PHARMACY</p>
            <p class="label">REQUIREMENTS</p>
        </div>
    </div>

    <%--
    <div class="widgetTitle">
        <span class="drugName">Drug</span>
        <span class="enrolled">Enrolled</span>
        <span class="actions">Actions</span>
    </div>
    --%>
    <ul id="drug-list-widget" class="dashboard-drugs mydrug-list">
        <%foreach (var pd in drugs.Drugs){
        var pd_drug = new Lib.Data.Drug(pd.Id);%>
        <li class="ui-state-default" data-drug-id="<%=pd_drug.ID %>"<%=GetEOCData(pd_drug) %>>
            <%if(pd.PercentComplete >= 1){ %>
            <div class="compliance-holder green-alert" id="<%=pd_drug.ID %>">
                <%--<img class="compliance-icon" src="/App/images/Warning_Green_Check.png" alt="Compliance Details" />--%>
                <span class="number-alert green-number"><%=(pd.PercentComplete * 100).ToString("0")%></span><span class="task-alert">% Compliant</span>
            </div>
            <span class="compliance-green compliance-expand"></span>

            <% } else if(pd.PercentComplete >= 0.33){ %>
            <div class="compliance-holder yellow-alert" id="<%=pd_drug.ID %>">
                <%--<img class="compliance-icon" src="/App/images/Warning_Yellow_Exclimation.png" alt="Compliance Details" />--%>
                <span class="number-alert yellow-number"><%=(pd.PercentComplete * 100).ToString("0")%></span><span class="task-alert">% Compliant</span>
            </div>
            <span class="compliance-yellow compliance-expand"></span>

            <% } else { %>
            <div class="compliance-holder red-alert" id="<%=pd_drug.ID %>">
                <%--<img class="compliance-icon" src="/App/images/Warning_Red_Exclimation.png" alt="Compliance Details" />--%>
                <span class="number-alert red-number"><%=(pd.PercentComplete * 100).ToString("0")%></span><span class="task-alert">% Compliant</span>
            </div>
            <span class="compliance-red compliance-expand"></span>
            <% } %>

            <div class="drugName">
                <a href="#common/drugs/detail?id=<%=pd_drug.ID %>"><span class="name"><%=pd_drug.GenericName %></span></a>
            </div>
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
            </span>
            <span class="other">
                <span class="actions-lbl">Actions</span>
                <span class="actions"><a class="button" href="#common/drugs/detail?id=<%=pd_drug.ID %>">View</a>&nbsp;<a class="button" href="#">Remove</a></span>
                <span class="enrolled-lbl">Date Enrolled</span>
                <span class="enrolled"><%=(pd.DateAdded == null ? "" : pd.DateAdded.ToShortDateString()) %></span>
            </span>
            --%>
        </li>
        <% } %>
    </ul>
</section>