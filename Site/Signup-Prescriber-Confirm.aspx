<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Front.Master" AutoEventWireup="true" Inherits="Site.Signup_Prescriber_Confirm" %>

<asp:Content ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
	<div class="headline">
		<h2>Signup</h2>
	</div>
	<div class="cont_nav"><a href="/">Home</a>&nbsp;/&nbsp;<span>Signup</span></div>
	<div class="content_wrap">
<!-- __________________________________________________ Start Content -->
		<section id="content">
			<div class="entry">
								
				<h4>Confirm Payment Information</h4>
				<div class="cmsms-form-builder">
					<script type="text/javascript">
						$(document).ready(function ()
						{
							$('#contactform a#btnSubmit').click(function ()
							{
								$('#contactform .loading').animate({ opacity: 1 }, 250);

								$.ajax({
									type: 'POST',
									url: '/API/Site/Signup/Prescriber_Confirm',
									data: {
										confirm: true
									},
									complete: function (data, textStatus, jqXHR)
									{
										$('#contactform .loading').animate({ opacity: 0 }, 250);
									}
								});

								return false;
							});
						});
					</script>
					<div class="form" id="contactform">
						<div class="form_info cmsms_input">
							<label for="txtFName">First Name</label>
							<input type="text" name="txtFName" id="txtFName" value="" size="22" disabled />
						</div>
						<div class="cl"></div>

						<div class="form_info cmsms_input">
							<label for="txtLName">Last Name</label>
							<input type="text" name="txtLName" id="txtLName" value="" size="22" disabled />
						</div>
						<div class="cl"></div>

						<div class="form_info cmsms_input">
							<label for="txtEmail">Email</label>
							<input type="text" name="txtEmail" id="txtEmail" value="" size="22" disabled />
						</div>
						<div class="cl"></div>

						<div class="form_info cmsms_input">
							<label for="txtAddress1">Address 1</label>
							<input type="text" name="txtAddress1" id="txtAddress1" value="" size="22" disabled />
						</div>
						<div class="cl"></div>

						<div class="form_info cmsms_input">
							<label for="txtAddress2">Address 2</label>
							<input type="text" name="txtAddress2" id="txtAddress2" value="" size="22" disabled />
						</div>
						<div class="cl"></div>

						<div class="form_info cmsms_input">
							<label for="txtCity">City</label>
							<input type="text" name="txtCity" id="txtCity" value="" size="22" disabled />
						</div>
						<div class="cl"></div>

						<div class="form_info cmsms_input">
							<label for="txtState">State</label>
							<input type="text" name="txtState" id="txtState" value="" size="22" disabled />
						</div>
						<div class="cl"></div>

						<div class="form_info cmsms_input">
							<label for="txtZip">Zip</label>
							<input type="text" name="txtZip" id="txtZip" value="" size="22" disabled />
						</div>
						<div class="cl"></div>

						<div class="form_info cmsms_input">
							<label for="txtCCNum">Credit Card Number</label>
							<input type="text" name="txtCCNum" id="txtCCNum" value="" size="22" disabled />
						</div>
						<div class="cl"></div>

						<div class="form_info cmsms_input">
							<label for="txtCCType">Credit Card Type</label>
							<input type="text" name="txtCCType" id="txtCCType" value="" size="22" disabled />
						</div>
						<div class="cl"></div>

						<div class="form_info cmsms_input">
							<label for="txtCCName">Name on Credit Card</label>
							<input type="text" name="txtCCName" id="txtCCName" value="" size="22" disabled />
						</div>
						<div class="cl"></div>

						<div class="form_info cmsms_input">
							<label for="txtCCExp">Credit Card Expiration</label>
							<input type="text" name="txtCCExp" id="txtCCExp" value="" size="22" disabled />
						</div>
						<div class="cl"></div>

						<div class="form_info cmsms_input">
							<label for="txtTotal">Total</label>
							<input type="text" name="txtTotal" id="txtTotal" value="" size="22" disabled />
						</div>
						<div class="cl"></div>

						<div>
							<a href="#" class="button_small" id="btnSubmit" tabindex="1">Confirm</a>
							<div class="loading"></div>
						</div>

					</div>
				</div>
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

<asp:Content ContentPlaceHolderID="cphFoot" runat="server">
	<script src="/js/jquery.script.js" type="text/javascript"></script>
	<script src="/js/jquery.validationEngine-lang.js" type="text/javascript"></script>
	<script src="/js/jquery.validationEngine.js" type="text/javascript"></script>
	<script src="/js/jquery.prettyPhoto.js" type="text/javascript"></script>
	<script src="/js/jquery.jtweetsanywhere.js" type="text/javascript"></script>
	<script src="/js/jquery.flickrfeed.min.js" type="text/javascript"></script>
	<script src="http://maps.google.com/maps/api/js?sensor=false" type="text/javascript"></script>
	<script src="/js/jquery.gMap.js" type="text/javascript"></script>
</asp:Content>
