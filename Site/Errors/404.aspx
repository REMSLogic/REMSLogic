<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Front.Master" AutoEventWireup="true" CodeBehind="404.aspx.cs" Inherits="Site.Errors._404" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
	<div class="content_wrap nobg">
<!-- __________________________________________________ Start Content -->
		<section id="middle_content">
			<div class="entry">
				<div class="error">
					<h2>404</h2>
					<h4>We're sorry, but the page you were looking for doesn't exist.</h4>
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
