<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="select.ascx.cs" Inherits="Site.App.Views.prescriber.drugs.select" %>
<script type="text/javascript" src="/App/js/views/prescriber/drugs/select.js"></script>
<h1 class="page-title">
    Drug Management</h1>
<div class="container_12 clearfix">
    <div class="grid_12 manage-drug-list">
        
        <section class="portlet grid_12 docs leading">
        <a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
    		<%--<header>
    			<h2>Drug Selection</h2>
    		</header>--%>
			<section>
             <%--   <div class="grid_12">
                    To create your unique drug list, drag and drop drugs from the "Available Drugs" column (left) into the "My Drug List" column (right).
                </div>--%>
				<div class="grid_6 portlet drug-select">
					<header>
						<h2 class="title-header">Drugs Available</h2>
                          <div class="eoc-menu">
                            <span id="eoc-menu-toggle-avail">
                                <img src="/App/images/dashboard-icon.png" alt="Dashboard-icon" width="24" height="24" class="eoc-menu-toggle" style="padding-top: 8px;" />
                                <%--<img id="eoc-menu-toggle-avail" class="eoc-menu-toggle" src="/App/images/navicons/27.png" alt="Filter Menu" />--%>
                                
                            </span>
                        </div>
					</header>
					<div class="filter">
						
						<%--<div class="eoc-filter eoc-filter-avail">
							<img src="/App/images/icons/ETASU.png" alt="ETASU" data-eoc="etasu" />
							<img src="/App/images/icons/FP EN.png" alt="Facility/Pharmacy Enrollment" data-eoc="facility-pharmacy-enrollment" />
							<img src="/App/images/icons/PAEN.png" alt="Patient Enrollment" data-eoc="patient-enrollment" />
							<img src="/App/images/icons/PREN.png" alt="Prescriber Enrollment" data-eoc="prescriber-enrollment" />
							<img src="/App/images/icons/EDUCRT.png" alt="Education/Certification" data-eoc="education-training" />
							<img src="/App/images/icons/MON.png" alt="Monitoring" data-eoc="monitoring-management" />
							<img src="/App/images/icons/MG.png" alt="Medication Guide" data-eoc="medication-guide" />
							<img src="/App/images/icons/IC.png" alt="Informed Consent" data-eoc="informed-consent" />
							<img src="/App/images/icons/FD.png" alt="Forms and Documents" data-eoc="forms-documents" />
							<img src="/App/images/icons/PR.png" alt="Pharmacy Requirements" data-eoc="pharmacy-requirements" />
						</div>--%>

				        <div id="divEoc-filter-avail" class="eoc-filter eoc-filter-avail">
                            <div class="filter-input mydruglist-inputwrap">
                                 <input id="available-drugs-filter" type="text" value="" size="50" placeholder="Filter Drugs" />
                                 <a class="eoc-menu-clear-avail clear-btn" onclick="ClearAvailDrugListFilter();" >Clear Filter</a>
                            </div>
                            <div style="clear:both;"></div>
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
					</div>
                    <br />

					<div>
						<%--<div class="widgetTitle">
							<span class="drugName wide">Drug</span>
							<span class="class">Class</span>
						</div>--%>
						<ul id="available-drugs" class="connectedDrugSelection sortable-drugs addDrugList">
							<% foreach (var d in AvailableDrugs)
          { %>
							<li class="ui-state-default" id="drug<%=d.ID %>" data-drug-id="<%=d.ID %>"<%=GetEOCData(d) %>>
                                <span class="selectdrug-icon"><i id="add<%=d.ID %>" class="fa fa-plus-circle selectDrug-add drugIcon"></i></span>
								<a href="#common/drugs/detail?id=<%=d.ID %>"><span class="drugName wide"><%=d.GenericName %><span></span></span></a>
								<%--<span class="class"><%=((d.ClassID != 1) ? "Non-ETASU" : "ETASU") %></span>--%>
								<!--<span class="eoc-icons">
									<span class="eoc-icon eoc-icon-etasu">
										<% if( d.HasEoc("etasu") ) { %>
										<img src="/App/images/icons/ETASU.png" alt="ETASU" />
										<% } %>
									</span>
									<span class="eoc-icon eoc-icon-facility-pharmacy-enrollment">
										<% if( d.HasEoc("facility-pharmacy-enrollment") ) { %>
										<img src="/App/images/icons/FP EN.png" alt="Facility/Pharmacy Enrollment" />
										<% } %>
									</span>
									<span class="eoc-icon eoc-icon-patient-enrollment">
										<% if( d.HasEoc("patient-enrollment") ) { %>
										<img src="/App/images/icons/PAEN.png" alt="Patient Enrollment" />
										<% } %>
									</span>
									<span class="eoc-icon eoc-icon-prescriber-enrollment">
										<% if( d.HasEoc("prescriber-enrollment") ) { %>
										<img src="/App/images/icons/PREN.png" alt="Prescriber Enrollment" />
										<% } %>
									</span>
									<span class="eoc-icon eoc-icon-education-training">
										<% if( d.HasEoc("education-training") ) { %>
										<img src="/App/images/icons/EDUCRT.png" alt="Education/Certification" />
										<% } %>
									</span>
									<span class="eoc-icon eoc-icon-monitoring-management">
										<% if( d.HasEoc("monitoring-management") ) { %>
										<img src="/App/images/icons/MON.png" alt="Monitoring" />
										<% } %>
									</span>
									<span class="eoc-icon eoc-icon-medication-guide">
										<% if( d.HasEoc("medication-guide") ) { %>
										<img src="/App/images/icons/MG.png" alt="Medication Guide" />
										<% } %>
									</span>
									<span class="eoc-icon eoc-icon-informed-consent">
										<% if( d.HasEoc("informed-consent") ) { %>
										<img src="/App/images/icons/IC.png" alt="Informed Consent" />
										<% } %>
									</span>
									<span class="eoc-icon eoc-icon-forms-documents">
										<% if( d.HasEoc("forms-documents") ) { %>
										<img src="/App/images/icons/FD.png" alt="Forms and Documents" />
										<% } %>
									</span>
									<span class="eoc-icon eoc-icon-pharmacy-requirements">
										<% if( d.HasEoc("pharmacy-requirements") ) { %>
										<img src="/App/images/icons/PR.png" alt="Pharmacy Requirements" />
										<% } %>
									</span>
								</span>-->
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
                               <img src="/App/images/dashboard-icon.png" alt="Dashboard-icon" width="24" height="24" class="eoc-menu-toggle" style="padding-top: 8px;" />
                               <%-- <img id="eoc-menu-toggle-my" class="eoc-menu-toggle" src="/App/images/navicons/27.png" alt="Filter Menu" />--%>
                            </span>
                        </div>
					</header>
					<div class="filter">
						<%--<div class="eoc-filter eoc-filter-my">
							<img src="/App/images/icons/ETASU.png" alt="ETASU" data-eoc="etasu" />
							<img src="/App/images/icons/FP EN.png" alt="Facility/Pharmacy Enrollment" data-eoc="facility-pharmacy-enrollment" />
							<img src="/App/images/icons/PAEN.png" alt="Patient Enrollment" data-eoc="patient-enrollment" />
							<img src="/App/images/icons/PREN.png" alt="Prescriber Enrollment" data-eoc="prescriber-enrollment" />
							<img src="/App/images/icons/EDUCRT.png" alt="Education/Certification" data-eoc="education-training" />
							<img src="/App/images/icons/MON.png" alt="Monitoring" data-eoc="monitoring-management" />
							<img src="/App/images/icons/MG.png" alt="Medication Guide" data-eoc="medication-guide" />
							<img src="/App/images/icons/IC.png" alt="Informed Consent" data-eoc="informed-consent" />
							<img src="/App/images/icons/FD.png" alt="Forms and Documents" data-eoc="forms-documents" />
							<img src="/App/images/icons/PR.png" alt="Pharmacy Requirements" data-eoc="pharmacy-requirements" />
						</div>--%>

				        <div id="divEoc-filter-my" class="eoc-filter eoc-filter-my">
                         <div class="filter-input mydruglist-inputwrap">
                                 <input id="my-drugs-filter" type="text" value="" size="50" placeholder="Filter Drugs">
                                 <a class="eoc-menu-clear-my clear-btn" onclick="ClearMyDrugListFilter();" >Clear Filter</a>
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
					</div>
                    <br />

					<div>
					<%--	<div class="widgetTitle">
							<span class="drugName wide">Drug</span>
							<span class="class">Class</span>
						</div>--%>
						<ul id="selected-drugs" class="connectedDrugSelection sortable-drugs addDrugList">
							<% foreach (var d in SelectedDrugs)
          { %>
							<li class="ui-state-default" id="drug<%=d.ID %>" data-drug-id="<%=d.ID %>"<%=GetEOCData(d) %>>
                                <span class="selectdrug-icon"><i id="rmv<%=d.ID %>" class="fa fa-times-circle myDrug-remove drugIcon"></i></span>
                                <a href="#common/drugs/detail?id=<%=d.ID %>"><span class="drugName wide"><%=d.GenericName %><span></span></span></a>
								<%--<span class="class"><%=((d.ClassID != 1) ? "Non-ETASU" : "ETASU") %></span>--%>
								<!--<span class="eoc-icons">
									<span class="eoc-icon eoc-icon-etasu">
										<% if( d.HasEoc("etasu") ) { %>
										<img src="/App/images/icons/ETASU.png" alt="ETASU" />
										<% } %>
									</span>
									<span class="eoc-icon eoc-icon-facility-pharmacy-enrollment">
										<% if( d.HasEoc("facility-pharmacy-enrollment") ) { %>
										<img src="/App/images/icons/FP EN.png" alt="Facility/Pharmacy Enrollment" />
										<% } %>
									</span>
									<span class="eoc-icon eoc-icon-patient-enrollment">
										<% if( d.HasEoc("patient-enrollment") ) { %>
										<img src="/App/images/icons/PAEN.png" alt="Patient Enrollment" />
										<% } %>
									</span>
									<span class="eoc-icon eoc-icon-prescriber-enrollment">
										<% if( d.HasEoc("prescriber-enrollment") ) { %>
										<img src="/App/images/icons/PREN.png" alt="Prescriber Enrollment" />
										<% } %>
									</span>
									<span class="eoc-icon eoc-icon-education-training">
										<% if( d.HasEoc("education-training") ) { %>
										<img src="/App/images/icons/EDUCRT.png" alt="Education/Certification" />
										<% } %>
									</span>
									<span class="eoc-icon eoc-icon-monitoring-management">
										<% if( d.HasEoc("monitoring-management") ) { %>
										<img src="/App/images/icons/MON.png" alt="Monitoring" />
										<% } %>
									</span>
									<span class="eoc-icon eoc-icon-medication-guide">
										<% if( d.HasEoc("medication-guide") ) { %>
										<img src="/App/images/icons/MG.png" alt="Medication Guide" />
										<% } %>
									</span>
									<span class="eoc-icon eoc-icon-informed-consent">
										<% if( d.HasEoc("informed-consent") ) { %>
										<img src="/App/images/icons/IC.png" alt="Informed Consent" />
										<% } %>
									</span>
									<span class="eoc-icon eoc-icon-forms-documents">
										<% if( d.HasEoc("forms-documents") ) { %>
										<img src="/App/images/icons/FD.png" alt="Forms and Documents" />
										<% } %>
									</span>
									<span class="eoc-icon eoc-icon-pharmacy-requirements">
										<% if( d.HasEoc("pharmacy-requirements") ) { %>
										<img src="/App/images/icons/PR.png" alt="Pharmacy Requirements" />
										<% } %>
									</span>
								</span>-->
							</li>
							<% } %>
						</ul>
						<%--<p>You can re-order your drugs by dragging and dropping them within this list. You might want to put your most prescribed drugs first.</p>--%>
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