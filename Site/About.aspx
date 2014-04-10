<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Front.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Site.About" %>

<asp:Content ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
	<div class="headline">
		<h2>About Us</h2>
	</div>
	<div class="cont_nav"><a href="index.html">Home</a>&nbsp;/&nbsp;<span>About Us</span></div>
	<div class="content_wrap nobg">
<!-- __________________________________________________ Start Content -->
		<section id="middle_content">
			<div class="entry">
				<div class="one_half">
									<img src="images/img/about_1.jpg" alt="" class="fullwidth" />
				</div>
				<div class="one_half">
									<h2>Why use this service?</h2>
									<p>FDA risk evaluation mitigation strategies (REMS) promote safe management of those medications that have a higher risk of harm compared to other medications.</p>
									<p>We understand that REMS programs are confusing. That is why we have created this service.  From here, prescribers will find concise, easy to understand REMS information, tracking reports and tools to manage certification and compliance.</p>
									<p>REMSLogic keeps the process simple by guiding you through what it is you need to know.  Having all this information in one place makes this site your single source for REMS compliance.</p>
				</div>
				<div class="divider"></div>
				<h2>Our Process</h2>
				<div class="one_fourth">
					<div class="colored_banner glow_blue">
						<span class="icon_banner icon_1"></span>
						<h3>Research</h3>
					</div>
					<br />
									<p>We have done all the research for you.  This website maintains a current list of requirements for each REMS program or medication.</p>
				</div>
				<div class="one_fourth">
					<div class="colored_banner glow_green">
						<span class="icon_banner icon_2"></span>
						<h3>Design</h3>
					</div>
					<br />
									<p>Customize management of the REMS medications you prescribe.  Choose how frequently you want to receive updates.</p>
				</div>
				<div class="one_fourth">
					<div class="colored_banner glow_yellow">
						<span class="icon_banner icon_3"></span>
						<h3>Deliver</h3>
					</div>
					<br />
									<p>Track your status with REMS educations and certifications.  Obtain all required forms (medication guides, patient informed consent, medication use checklists).  Print reports and dashboards.</p>
				</div>
				<div class="one_fourth">
					<div class="colored_banner glow_red">
						<span class="icon_banner icon_4"></span>
						<h3>Certify</h3>
					</div>
					<br />
									<p>Obtain certifications and renewal information in one location.   From here you can stay up-to-date with all the changes related to the REMS medications you prescribe.</p>
				</div>
								<!--
				<div class="divider"></div>
								
				<h2>Meet Our Team</h2>
				<div class="one_third">
					<h4>Marie Link, CEO</h4>
					<a href="#">Twitter</a> | <a href="#">Facebook</a>
					<div class="cl"></div>
					<br />
									<p><img class="fullwidth" src="images/img/about_2.png" alt="Marie Link" /></p>
					<p>Lorem ipsum dolor sit amet, consectetur adipiscing eliter. Nam risus nisl, porttitor et fermentum sem, sit amet ipite odio suscipit id.</p>
				</div>
				<div class="one_third">
					<h4>Lynn Webster, MD</h4>
					<a href="#">Twitter</a> | <a href="#">Facebook</a>
					<div class="cl"></div>
					<br />
									<p><img class="fullwidth" src="images/img/about_3.png" alt="" /></p>
					<p>Lorem ipsum dolor sit amet, consectetur adipiscing eliter. Nam risus nisl, porttitor et fermentum sem, sit amet ipite odio suscipit id.</p>
				</div>
				<div class="one_third">
					<h4>Janet Woodcock, FDA</h4>
					<a href="#">Facebook</a> | <a href="#">Twitter</a>
					<div class="cl"></div>
					<br />
									<p><img class="fullwidth" src="images/img/about_4.png" alt="" /></p>
					<p>director of FDA’s Center for Drug Evaluation and Research</p>
				</div>
								-->
				<div class="cl"></div>
			</div>
		</section>
<!-- __________________________________________________ Finish Content -->
	</div>
</asp:Content>

<asp:Content ContentPlaceHolderID="cphFoot" runat="server">
	<script src="/js/jquery.script.js" type="text/javascript"></script>
	<script src="/js/jquery.prettyPhoto.js" type="text/javascript"></script>
</asp:Content>
