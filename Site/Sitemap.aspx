<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Front.Master" AutoEventWireup="true" CodeBehind="Sitemap.aspx.cs" Inherits="Site.Sitemap" %>

<asp:Content ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
	<div class="headline">
		<h2>Sitemap</h2>
	</div>
	<div class="cont_nav"><a href="/">Home</a>&nbsp;/&nbsp;<span>Sitemap</span></div>
	<div class="content_wrap nobg">
<!-- __________________________________________________ Start Content -->
		<section id="middle_content">
			<div class="entry">
				<h2>Pages</h2>
				<ul class="sitemap">
					<li><a href="/">Home</a></li>
					<li><a href="#">Information</a>
						<ul>
							<li><a href="/Signup-HealthCare.aspx">Healthcare Organizations / Provider</a></li>
							<li><a href="/Signup-Prescriber.aspx">Prescribers</a></li>
						</ul>
					</li>
					<li><a href="#">Medications</a>
						<ul>
							<li><a href="/Medications-ETASU.aspx">REMS ETASU</a></li>
							<li><a href="/Medications-NonETASU.aspx">REMS NON-ETASU</a></li>
						</ul>
					</li>
					<li><a href="/Reporting.aspx">Reporting</a></li>
					<li><a href="/Contact-Us.aspx"><span>Contact Us</span></a></li>
					<li><a href="/About.aspx"><span>About Us</span></a></li>
					<li><a href="/Privacy.aspx"><span>Privacy Statement</span></a></li>
				</ul>
								
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
