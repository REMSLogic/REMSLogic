<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Front.Master" AutoEventWireup="true" CodeBehind="Contact-Us.aspx.cs" Inherits="Site.Contact_Us" %>

<asp:Content ContentPlaceHolderID="cphHead" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="cphBody" runat="server">
	<div class="headline">
		<h2>Contact Us</h2>
	</div>
	<div class="cont_nav"><a href="/">Home</a>&nbsp;/&nbsp;<span>Contact Us</span></div>
	<div class="content_wrap">
<!-- __________________________________________________ Start Content -->
		<section id="content">
			<div class="entry">
								
				<h4>Send us a message</h4>
				<div class="cmsms-form-builder">
					<div class="box success_box" style="display:none;">
						<table>
							<tr>
								<td>&nbsp;</td>
								<td>Thank You!<br>Your message has been sent successfully.</td>
							</tr>
						</table>
					</div>
					<script type="text/javascript">
						$(document).ready(function () {
							$('#contactform').validationEngine('init');

							$('#contactform a#contact_form_formsend').click(function () {
								var form_builder_url = '/API/Site/ContactUs/Submit';

								$('#contactform .loading').animate({ opacity: 1 }, 250);

								if ($('#contactform').validationEngine('validate')) {
									$.ajax({
										type: 'POST',
										url: form_builder_url,
										data: {
											name: $('#contact_name').val(),
											email: $('#contact_email').val(),
											subject: $('#contact_subject').val(),
											message: $('#contact_message').val()
										},
										complete: function (data, textStatus, jqXHR) {
											$('#contactform .loading').animate({ opacity: 0 }, 250);

											//document.getElementById('contactform').reset();

											$('#contact_name').val('');
											$('#contact_email').val('');
											$('#contact_subject').val('');
											$('#contact_message').val('');

											$('#contactform').parent().find('.box').hide();
											$('#contactform').parent().find('.success_box').fadeIn('fast');
											$('html, body').animate({ scrollTop: jQuery('#contactform').offset().top - 100 }, 'slow');
											$('#contactform').parent().find('.success_box').delay(5000).fadeOut(1000);
										}
									});

									return false;
								} else {
									jQuery('#contactform .loading').animate({ opacity: 0 }, 250);

									return false;
								}
							});
						});
					</script>
					<div class="form" id="contactform">
						<div class="form_info cmsms_input">
							<label for="contact_name">Name<span class="color_3"> *</span></label>
							<input type="text" name="contact_name" id="contact_name" value="" size="22" tabindex="3" class="validate[required,minSize[3],maxSize[100],custom[onlyLetterSp]]"/>
						</div>
						<div class="cl"></div>
						<div class="form_info cmsms_input">
							<label for="contact_email">Email<span class="color_3"> *</span></label>
							<input type="text" name="contact_email" id="contact_email" value="" size="22" tabindex="4" class="validate[required,custom[email]]" />
						</div>
						<div class="cl"></div>
						<div class="form_info cmsms_input">
							<label for="contact_subject">Subject<span class="color_3"> *</span></label>
							<input type="text" name="contact_subject" id="contact_subject" value="" size="22" tabindex="6" class="validate[required,minSize[3],maxSize[100]]" />
						</div>
						<div class="cl"></div>
						<div class="form_info cmsms_textarea">
							<label for="contact_message">Message<span class="color_3"> *</span></label>
							<textarea name="contact_message" id="contact_message" cols="28" rows="6" tabindex="7" class="validate[required,minSize[3]]" ></textarea>
						</div>
						<div>
							<a href="#" class="button_small" id="contact_form_formsend" tabindex="8">Send a message</a>
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
						jQuery(document).ready(function () {
							jQuery('#tweetFeed').jTweetsAnywhere({
								username: 'fdarems',
								searchParams: 'q=FDAMedWatch',
								count: 3,
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
