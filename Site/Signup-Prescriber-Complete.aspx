<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Front.Master" AutoEventWireup="true" Inherits="Site.Signup_Prescriber_Complete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="server">
	<script type="text/javascript">
		$(document).ready(function ()
		{
			setTimeout(function ()
			{
				window.location.href = '/App/';
			}, 5000);
		});
	</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
	<div class="headline">
		<h2>Signup</h2>
	</div>
	<div class="cont_nav"><a href="/">Home</a>&nbsp;/&nbsp;<span>Signup</span></div>
	<div class="content_wrap">
<!-- __________________________________________________ Start Content -->
		<section id="content">
			<div class="entry">
								
				<h4>Account Created</h4>
				<p>
					Thank you for signing up with us. You will now be redirected to your account where you can get started. If you browser does not automatically redirect you in 5 seconds,<br />
					<br />
					<a href="/App/" class="button_small" id="btnSubmit">please click here.</a>
				</p>
				<div class="cl"></div>
			</div>
		</section>
<!-- __________________________________________________ Finish Content -->

<!-- __________________________________________________ Start Sidebar -->
		<section id="sidebar">
			<div class="one_first">
				<aside class="widget widget_custom_tweets_entries">
					<h3 class="widgettitle">Tweets</h3>
					<div id="tweetFeed"></div>
					<script type="text/javascript">
						jQuery(document).ready(function ()
						{
							jQuery('#tweetFeed').jTweetsAnywhere({
								username: 'fdarems',
								searchParams: '',
								count: 5,
								showTweetFeed: {
									showTwitterBird: false
								}
							});
						});
					</script>
				</aside>
			</div>
		</section>
		<div class="cl"></div>
<!-- __________________________________________________ Finish Sidebar -->
	</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="cphFoot" runat="server">
	<script src="/js/jquery.script.js" type="text/javascript"></script>
	<script src="/js/jquery.validationEngine-lang.js" type="text/javascript"></script>
	<script src="/js/jquery.validationEngine.js" type="text/javascript"></script>
	<script src="/js/jquery.prettyPhoto.js" type="text/javascript"></script>
	<script src="/js/jquery.jtweetsanywhere.js" type="text/javascript"></script>
	<script src="/js/jquery.flickrfeed.min.js" type="text/javascript"></script>
	<script src="http://maps.google.com/maps/api/js?sensor=false" type="text/javascript"></script>
	<script src="/js/jquery.gMap.js" type="text/javascript"></script>
</asp:Content>
