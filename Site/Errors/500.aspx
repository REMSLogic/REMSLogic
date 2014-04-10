<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Front.Master" AutoEventWireup="true" CodeBehind="500.aspx.cs" Inherits="Site.Errors._500" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
	<div class="content_wrap nobg">
<!-- __________________________________________________ Start Content -->
		<section id="middle_content">
			<div class="entry">
				<div class="error">
					<h2>Error</h2>
					<h4>We're sorry, but an error has occured. It has been logged and we have been notified. We apologize for any inconvience.<br /><br />You may <asp:HyperLink NavigateUrl="~/Contact-Us.aspx">contact us</asp:HyperLink> if you have any questions.</h4>
					<div class="search_line">
						<form action="#">
							<p>
								<input type="text" name="search" id="search" placeholder="Enter Keywords..." value="" />
								<input type="submit" value="" />
							</p>
						</form>
					</div>
					<a href="/Sitemap.aspx" class="button_small"><span>Sitemap</span></a>
				</div>
				<div class="cl"></div>
			</div>
		</section>
<!-- __________________________________________________ Finish Content -->
	</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphFoot" runat="server">
	<script src="/js/jquery.script.js" type="text/javascript"></script>
	<script src="/js/jquery.prettyPhoto.js" type="text/javascript"></script>
</asp:Content>
