<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Front.Master" AutoEventWireup="true" Inherits="Site.Reporting" %>

<asp:Content ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
	<div class="headline">
		<h2>Reporting</h2>
	</div>
	<div class="cont_nav"><a href="index.html">Home</a>&nbsp;/&nbsp;<span>Reporting</span></div>
	<div class="content_wrap nobg">
<!-- __________________________________________________ Start Content -->
						<section id="middle_content">
							<div class="entry">
								<div class="one_half">
									<img src="images/img/reporting.jpg" alt="" class="fullwidth" />
								</div>
								<div class="one_half">
									<h2>Organization log-in access to</h2>
									<ul>
										<li>Prescriber statistics</li>
										<li>Prescriber enrollment rates</li>
										<li>Certification completion rates</li>
										<li>Compliance rates by drug; by individual prescriber, by REMS program</li>
										<li>Audit dashboards</li>
										<li>Tools for compliance</li>
									</ul>
									<p>These reporting benefits are also included in the individual prescriber sign-up.</p>
								</div>
								<div class="divider"></div>
								
								<div class="cl"></div>
							</div>
						</section>
<!-- __________________________________________________ Finish Content -->
		</div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cphFoot" runat="server">
		<script src="js/jquery.script.js" type="text/javascript"></script>
		<script src="js/jquery.prettyPhoto.js" type="text/javascript"></script>
</asp:Content>