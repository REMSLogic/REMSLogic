<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="detail.ascx.cs" Inherits="Site.App.Views.common.drugs.detail" %>

<!-- DATATABLES CSS -->
<link rel="stylesheet" media="screen" href="/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/js/lib/datatables/js/jquery.dataTables.js"></script> 
<script type="text/javascript" src="/js/dsq-view.js"></script> 
<h1 class="page-title"><%=item.GenericName%></h1>
<div class="container_12 clearfix leading">
	<div class="grid_12 portlet">
        <a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        <header>
			<h2 class="title-header"><%=item.GenericName%></h2>
        </header>
		
		<div class="dsq-form" >
			<div class="clearfix">
				<site:DSQView ID="dsqView" GeneralInfoControlPath="~/Controls/DSQ/GeneralInfoView.ascx" runat="server" />
				
				<!--
				<section class="dsq-accordion accordion ui-accordion ui-widget ui-helper-reset" role="tablist">
                  <span id="ctl00_dsqView"></span> 
                <header id="ui-accordion-1-header-1" class="ui-accordion-header ui-helper-reset ui-state-default ui-corner-all ui-accordion-icons" role="tab" aria-controls="ui-accordion-1-panel-1" 
                        aria-selected="false" tabindex="-1">
                    <h2>Enrollment</h2>
                </header>
                <section id="ui-accordion-1-panel-1" class="clearfix ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom"
                style="display: none;" aria-labelledby="ui-accordion-1-header-1" role="tabpanel" aria-expanded="false" aria-hidden="true">
                    <div class="dvform">
                        <div class="dvform-row" data-id="6">
                            <div class="dvform-label">
                                <span class="dvform-label-parent">Facility enrollment</span>
                                <span class="dvform-label-required">REQUIRED</span>
                            </div>
                            <div class="dvform-input">
                                <div class="dvform-info">
                                    <div class="dvform-input-pin">
                                        <a class="ajax-button link-add-button-a" href="/api/List/FormsAndDocuments/AddItem?id=53">
                                            <img class="dvform-input-img" src="/images/navicons/101.png" alt="Add to Documents List"/>
                                        </a>
                                    </div>
                                    <div class="dvform-input-link">
                                        <a class="ajax-button" target="_blank" href="http://www.tracleerrems.com/docs/TAP_Hospital_Certification_Form.pdf">
                                            <img class="dvform-input-img" src="/images/navicons/100.png" alt="External Link"/>
                                        </a>
                                    </div>
                                    <div class="dvform-input-lbl">
                                        <a class="dvform-input-lbl-blue" target="_blank" href="http://www.tracleerrems.com/docs/TAP_Hospital_Certification_Form.pdf">
                                            Facility Enrollment
                                        </a>
                                    </div>
                                </div>
                                <div class="dvform-info">
                                    <div class="dvform-input-phone">
                                        <img class="dvform-input-img" src="/images/navicons/75.png" alt="Phone Number"/>
                                        <span class="dvform-input-pn">+1 (216) 555-1212</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="dvform-row" data-id="7" data-parent-checks="Yes" data-parent-id="6">
                            <div class="dvform-label compliance-yellow">
                                <span class="dvform-label-child">Yes, I acknowledge that I am aware of the facility enrollment requirement for this REMS medication</span>
                            </div>
                            <div class="dvform-input">
                                <div class="dvform-info">
                                    <div class="dvform-input-compliance">
                                        <img class="dvform-input-icon" src="/images/Warning_Yellow_Exclimation.png" alt="Compliance Warning" />
                                    </div>
                                    <div class="dvform-input-button">
                                        <a href="/api/Prescriber/Drug/Certified?id=85&amp;eoc_name=facility-pharmacy-enrollment" class="button dvform-input-btn" onclick="">YES</a>
                                    </div>
                                    <div class="dvform-input-lbl">
                                        <span class="dvform-input-lbl-yellow">Reviewed Requirements?</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="dvform-row" data-id="8" data-parent-checks="Yes" data-parent-id="6">
                            <div class="dvform-label">
                                <span class="dvform-label-child">Frequency of Facility Enrollment Renewal</span>
                            </div>
                            <div class="dvform-input">
                                <div class="dvform-info">
                                    <span class="dvform-input-lbl">3 years</span>
                                </div>
                            </div>
                        </div>
                        <div class="dvform-row" data-id="9">
                            <div class="dvform-label">
                                <span class="dvform-label-parent">Patient enrollment</span>
                            </div>
                            <div class="dvform-input">
                                <div class="dvform-info">
                                    <div class="dvform-input-pin">
                                        <a class="ajax-button link-add-button-a" href="/api/List/FormsAndDocuments/AddItem?id=55">
                                            <img class="dvform-input-img" src="/images/navicons/101.png" alt="Add to Documents List"/>
                                        </a>
                                    </div>
                                    <div class="dvform-input-link">
                                        <a class="ajax-button" target="_blank" href="http://www.tracleerrems.com/docs/Tracleer_Enrollment_Form.pdf">
                                            <img class="dvform-input-img" src="/images/navicons/100.png" alt="External Link"/>
                                        </a>
                                    </div>
                                    <div class="dvform-input-lbl">
                                        <a class="dvform-input-lbl-blue" target="_blank" href="http://www.tracleerrems.com/docs/Tracleer_Enrollment_Form.pdf">
                                            Patient Enrollment
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="dvform-row" data-id="10" data-parent-checks="Yes" data-parent-id="9">
                            <div class="dvform-label">
                                <span class="dvform-label-child">Yes, I acknowledge that I am aware of the patient enrollment requirement for this REMS medication</span>
                            </div>
                            <div class="dvform-input">
                                <div class="dvform-info">
                                    <div class="dvform-input-compliance">
                                        <img class="dvform-input-icon" src="/images/Warning_Green_Check.png" alt="Compliance" />
                                    </div>
                                    <span class="dvform-input-lbl">Compliant as of 2014-01-27  23:30:08</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
                <header id="ui-accordion-1-header-2" class="ui-accordion-header ui-helper-reset ui-state-default ui-corner-all ui-accordion-icons" role="tab" aria-controls="ui-accordion-1-panel-2" 
                        aria-selected="false" tabindex="-1">
                    <h2>Education/Training and Monitoring</h2>
                </header>
                <section id="ui-accordion-1-panel-2" class="clearfix ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom" style="display: none;" aria-labelledby="ui-accordion-1-header-2"                                    role="tabpanel" aria-expanded="false" aria-hidden="true">
                    <div class="dvform">
                        <div class="dvform-row" data-id="27">
                            <div class="dvform-label">
                                <span class="dvform-label-parent">Education/Training/Certification</span>
                                <span class="dvform-label-required">REQUIRED</span>
                            </div>
                            <div class="dvform-input">
                                <div class="dvform-info">
                                    <div class="dvform-input-pin">
                                        <a class="ajax-button link-add-button-a" href="/api/List/FormsAndDocuments/AddItem?id=58">
                                            <img class="dvform-input-img" src="/images/navicons/101.png" alt="Add to Documents List"/>
                                        </a>
                                    </div>
                                    <div class="dvform-input-link">
                                        <a class="ajax-button" target="_blank" href="http://www.tracleerrems.com/docs/Prescriber_Certification_Form.pdf">
                                            <img class="dvform-input-img" src="/images/navicons/100.png" alt="External Link"/>
                                        </a>
                                    </div>
                                    <div class="dvform-input-lbl">
                                        <a class="dvform-input-lbl-blue" target="_blank" href="http://www.tracleerrems.com/docs/Prescriber_Certification_Form.pdf">
                                            Prescriber Certification 
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="dvform-row" data-id="28" data-parent-checks="Yes" data-parent-id="27">
                            <div class="dvform-label compliance-yellow">
                                <span class="dvform-label-child">Yes, I acknowledge that I am aware of the education/training requirement for this REMS medication</span>
                            </div>
                            <div class="dvform-input">
                                <div class="dvform-info">
                                    <div class="dvform-input-compliance">
                                        <img class="dvform-input-icon" src="/images/Warning_Yellow_Exclimation.png" alt="Compliance Warning" />
                                    </div>
                                    <div class="dvform-input-button">
                                        <a href="/api/Prescriber/Drug/Certified?id=85&amp;eoc_name=education-training" class="button dvform-input-btn" onclick="">YES</a>
                                    </div>
                                    <div class="dvform-input-lbl">
                                        <span class="dvform-input-lbl-yellow">Reviewed Updated Requirements?</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="dvform-row" data-id="29" data-parent-checks="Yes" data-parent-id="27">
                            <div class="dvform-label">
                                <span class="dvform-label-child">Is an education training post test available</span>
                            </div>
                            <div class="dvform-input">
                                <div class="dvform-info">
                                    <span class="dvform-input-lbl">No</span>
                                </div>
                            </div>
                        </div>
                        <div class="dvform-row" data-id="30" data-parent-checks="Yes" data-parent-id="27">
                            <div class="dvform-label">
                                <span class="dvform-label-child">Is there a certificate or confirmation document provided upon completion of training</span>
                            </div>
                            <div class="dvform-input">
                                <div class="dvform-info">
                                    <span class="dvform-input-lbl">No</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
                <header id="ui-accordion-1-header-3" class="ui-accordion-header ui-helper-reset ui-state-default ui-corner-all ui-accordion-icons" role="tab" aria-controls="ui-accordion-1-panel-3" 
                        aria-selected="false" tabindex="-1">
                    <h2>Forms and Documents</h2>
                </header>
                <section id="ui-accordion-1-panel-3" class="clearfix ui-accordion-content ui-helper-reset ui-widget-content ui-corner-bottom" style="display: none;" aria-labelledby="ui-accordion-1-header-3"  
                         role="tabpanel" aria-expanded="false" aria-hidden="true">
                    <div class="dvform">
                        <div class="dvform-row" data-id="38">
                            <div class="dvform-label">
                                <span class="dvform-label-parent">Medication Guide</span>
                                <span class="dvform-label-required">REQUIRED</span>
                            </div>
                            <div class="dvform-input">
                                <div class="dvform-info">
                                    <div class="dvform-input-pin">
                                        <a class="ajax-button link-add-button-a" href="/api/List/FormsAndDocuments/AddItem?id=60">
                                            <img class="dvform-input-img" src="/images/navicons/101.png" alt="Add to Documents List"/>
                                        </a>
                                    </div>
                                    <div class="dvform-input-link">
                                        <a class="ajax-button" target="_blank" href="http://www.tracleerrems.com/docs/Tracleer_Medication_Guide.pdf">
                                            <img class="dvform-input-img" src="/images/navicons/100.png" alt="External Link"/>
                                        </a>
                                    </div>
                                    <div class="dvform-input-lbl">
                                        <a class="dvform-input-lbl-blue" target="_blank" href="http://www.tracleerrems.com/docs/Tracleer_Medication_Guide.pdf">
                                            Medication Guide 
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="dvform-row" data-id="39">
                            <div class="dvform-label">
                                <span class="dvform-label-parent">Informed consent required prior to first dose</span>
                                <span class="dvform-label-required">REQUIRED</span>
                            </div>
                            <div class="dvform-input">
                                <div class="dvform-info">
                                    <div class="dvform-input-pin">
                                        <a class="ajax-button link-add-button-a" href="/api/List/FormsAndDocuments/AddItem?id=65">
                                            <img class="dvform-input-img" src="/images/navicons/101.png" alt="Add to Documents List"/>
                                        </a>
                                    </div>
                                    <div class="dvform-input-link">
                                        <a class="ajax-button" target="_blank" href="http://www.tracleerrems.com/docs/Tracleer_Enrollment_Form.pdf">
                                            <img class="dvform-input-img" src="/images/navicons/100.png" alt="External Link"/>
                                        </a>
                                    </div>
                                    <div class="dvform-input-lbl">
                                        <a class="dvform-input-lbl-blue" target="_blank" href="http://www.tracleerrems.com/docs/Tracleer_Enrollment_Form.pdf">
                                            Patient Enrollment
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="dvform-row" data-id="40" data-parent-checks="Yes" data-parent-id="39">
                            <div class="dvform-label compliance-yellow">
                                <span class="dvform-label-child">Yes, I acknowledge that I am aware of the informed consent requirement for this REMS medication</span>
                            </div>
                            <div class="dvform-input">
                                <div class="dvform-info">
                                    <div class="dvform-input-compliance">
                                        <img class="dvform-input-icon" src="/images/Warning_Yellow_Exclimation.png" alt="Compliance Warning" />
                                    </div>
                                    <div class="dvform-input-button">
                                        <a href="/api/Prescriber/Drug/Certified?id=85&amp;eoc_name=informed-consent" class="button dvform-input-btn" onclick="">YES</a>
                                    </div>
                                    <div class="dvform-input-lbl">
                                        <span class="dvform-input-lbl-yellow">Reviewed Requirements?</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="dvform-row" data-id="41">
                            <div class="dvform-label">
                                <span class="dvform-label-parent">Counseling Document</span>
                                <span class="dvform-label-required">REQUIRED</span>
                            </div>
                            <div class="dvform-input">
                                <div class="dvform-info">
                                    <div class="dvform-input-pin">
                                        <a class="ajax-button link-add-button-a" href="/api/List/FormsAndDocuments/AddItem?id=67">
                                            <img class="dvform-input-img" src="/images/navicons/101.png" alt="Add to Documents List"/>
                                        </a>
                                    </div>
                                    <div class="dvform-input-link">
                                        <a class="ajax-button" target="_blank" href="http://www.tracleerrems.com/docs/Patient_Essentials_Guide.pdf">
                                            <img class="dvform-input-img" src="/images/navicons/100.png" alt="External Link"/>
                                        </a>
                                    </div>
                                    <div class="dvform-input-lbl">
                                        <a class="dvform-input-lbl-blue" target="_blank" href="http://www.tracleerrems.com/docs/Patient_Essentials_Guide.pdf">
                                            Counseling Document 
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="dvform-row" data-id="42">
                            <div class="dvform-label">
                                <span class="dvform-label-parent">Medication Brochure</span>
                                <span class="dvform-label-required">REQUIRED</span>
                            </div>
                            <div class="dvform-input">
                                <div class="dvform-info">
                                    <div class="dvform-input-pin">
                                        <a class="ajax-button link-add-button-a" href="/api/List/FormsAndDocuments/AddItem?id=68">
                                            <img class="dvform-input-img" src="/images/navicons/101.png" alt="Add to Documents List"/>
                                        </a>
                                    </div>
                                    <div class="dvform-input-link">
                                        <a class="ajax-button" target="_blank" href="http://www.tracleerrems.com/docs/Tracleer_Patient_Brochure.pdf">
                                            <img class="dvform-input-img" src="/images/navicons/100.png" alt="External Link"/>
                                        </a>
                                    </div>
                                    <div class="dvform-input-lbl">
                                        <a class="dvform-input-lbl-blue" target="_blank" href="http://www.tracleerrems.com/docs/Tracleer_Patient_Brochure.pdf">
                                            Medication Brochure 
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="dvform-row" data-id="43">
                            <div class="dvform-label">
                                <span class="dvform-label-parent">Prescribing Info</span>
                            </div>
                            <div class="dvform-input">
                                <div class="dvform-info">
                                    <div class="dvform-input-pin">
                                        <a class="ajax-button link-add-button-a" href="/api/List/FormsAndDocuments/AddItem?id=69">
                                            <img class="dvform-input-img" src="/images/navicons/101.png" alt="Add to Documents List"/>
                                        </a>
                                    </div>
                                    <div class="dvform-input-link">
                                        <a class="ajax-button" target="_blank" href="http://www.tracleerrems.com/docs/Tracleer_Full_Prescribing_Information.pdf">
                                            <img class="dvform-input-img" src="/images/navicons/100.png" alt="External Link"/>
                                        </a>
                                    </div>
                                    <div class="dvform-input-lbl">
                                        <a target="_blank" href="http://www.tracleerrems.com/docs/Tracleer_Full_Prescribing_Information.pdf">
                                            Prescribing Information 
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="dvform-row" data-id="44">
                            <div class="dvform-label">
                                <span class="dvform-label-parent">Additional Info</span>
                            </div>
                            <div class="dvform-input">
                                <div class="dvform-info">
                                    <div class="dvform-input-pin">
                                        <a class="ajax-button link-add-button-a" href="/api/List/FormsAndDocuments/AddItem?id=71">
                                            <img class="dvform-input-img" src="/images/navicons/101.png" alt="Add to Documents List"/>
                                        </a>
                                    </div>
                                    <div class="dvform-input-link">
                                        <a class="ajax-button" target="_blank" href="http://www.tracleerrems.com/docs/Tracleer_Prescriber_Guide.pdf">
                                            <img class="dvform-input-img" src="/images/navicons/100.png" alt="External Link"/>
                                        </a>
                                    </div>
                                    <div class="dvform-input-lbl">
                                        <a target="_blank" href="http://www.tracleerrems.com/docs/Tracleer_Prescriber_Guide.pdf">
                                            Prescriber Guide 
                                        </a>
                                    </div>
                                </div>
                                <div class="dvform-info">
                                    <div class="dvform-input-pin">
                                        <a class="ajax-button link-add-button-a" href="/api/List/FormsAndDocuments/AddItem?id=72">
                                            <img class="dvform-input-img" src="/images/navicons/101.png" alt="Add to Documents List"/>
                                        </a>
                                    </div>
                                    <div class="dvform-input-link">
                                        <a class="ajax-button" target="_blank" href="http://www.tracleerrems.com/docs/Tracleer_Prescriber_Guide.pdf">
                                            <img class="dvform-input-img" src="/images/navicons/100.png" alt="External Link"/>
                                        </a>
                                    </div>
                                    <div class="dvform-input-lbl">
                                        <a target="_blank" href="http://www.tracleerrems.com/docs/Tracleer_Prescriber_Guide.pdf">
                                            Prescriber Guide 2
                                        </a>
                                    </div>
                                </div>
                                <div class="dvform-info">
                                    <div class="dvform-input-pin">
                                        <a class="ajax-button link-add-button-a" href="/api/List/FormsAndDocuments/AddItem?id=73">
                                            <img class="dvform-input-img" src="/images/navicons/101.png" alt="Add to Documents List"/>
                                        </a>
                                    </div>
                                    <div class="dvform-input-link">
                                        <a class="ajax-button" target="_blank" href="http://www.tracleerrems.com/docs/Tracleer_Prescriber_Guide.pdf">
                                            <img class="dvform-input-img" src="/images/navicons/100.png" alt="External Link"/>
                                        </a>
                                    </div>
                                    <div class="dvform-input-lbl">
                                        <a target="_blank" href="http://www.tracleerrems.com/docs/Tracleer_Prescriber_Guide.pdf">
                                            Prescriber Guide 3
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
                </section>
				-->
			</div>
		</div>
	</div>
</div>
