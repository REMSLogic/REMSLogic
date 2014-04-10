<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Front.Master" AutoEventWireup="true" Inherits="Site.Signup_Prescriber_Step2" %>

<asp:Content ContentPlaceHolderID="cphHead" runat="server">
	<script type="text/javascript" src="/App/js/global.js"></script>
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
				<h4>Enter Payment Details</h4>
				<div class="cmsms-form-builder">
					<script type="text/javascript">
						$(document).ready(function ()
						{
							$('#contactform').validationEngine('init');

							$('#contactform a#btnSubmit').click(function ()
							{
								var form_builder_url = '/API/Site/Signup/Prescriber_Step2';

								$('#contactform .loading').animate({ opacity: 1 }, 250);

								if ($('#contactform').validationEngine('validate'))
								{
									$.ajax({
										type: 'POST',
										url: form_builder_url,
										data: {
											'cc-num': $('#cc-num').val(),
											'cc-type': $('#cc-type').val(),
											'cc-name': $('#cc-name').val(),
											'cc-exp-month': $('#cc-exp-month').val(),
											'cc-exp-year': $('#cc-exp-year').val(),
											'cc-cvv': $('#cc-cvv').val()
										},
										complete: function (data, textStatus, jqXHR)
										{
											$('#contactform .loading').animate({ opacity: 0 }, 250);

											$('#cc-num').val('');
											$('#cc-type').val('');
											$('#cc-name').val('');
											$('#cc-exp-month').val('');
											$('#cc-exp-year').val('');
											$('#cc-cvv').val('');
										}
									});

									return false;
								} else
								{
									jQuery('#contactform .loading').animate({ opacity: 0 }, 250);

									return false;
								}
							});
						});
					</script>
					<div class="form" id="contactform">
						
						<div class="form_info cmsms_input">
							<label for="cc-num">Credit Card Number<span class="color_3"> *</span></label>
							<input type="text" name="cc-num" id="cc-num" value="" size="22" tabindex="1" class="validate[required,minSize[12],maxSize[20]]"/>
						</div>
						<div class="cl"></div>

						<div class="form_info cmsms_input">
							<label for="cc-type">Credit Card Type<span class="color_3"> *</span></label>
							<select id="cc-type" name="cc-type">
								<option value="Visa">Visa</option>
								<option value="MasterCard">MasterCard</option>
								<option value="Discover">Discover</option>
								<option value="Amex">American Express</option>
							</select>
						</div>
						<div class="cl"></div>

						<div class="form_info cmsms_input">
							<label for="cc-name">Name on Credit Card<span class="color_3"> *</span></label>
							<input type="text" name="cc-name" id="cc-name" value="" size="22" tabindex="3" class="validate[required,minSize[3],maxSize[255]]"/>
						</div>
						<div class="cl"></div>

						<div class="form_info cmsms_input">
							<label for="cc-exp-month">Credit Card Expiration<span class="color_3"> *</span></label>
							<select id="cc-exp-month" name="cc-exp-month" style="width: 239px;">
								<option value="1">January</option>
								<option value="2">February</option>
								<option value="3">March</option>
								<option value="4">April</option>
								<option value="5">May</option>
								<option value="6">June</option>
								<option value="7">July</option>
								<option value="8">August</option>
								<option value="9">September</option>
								<option value="10">October</option>
								<option value="11">November</option>
								<option value="12">December</option>
							</select>
							<select id="cc-exp-year" name="cc-exp-year" style="width: 145px;">
							<% for(int i=DateTime.Now.Year;i<DateTime.Now.Year+10;i++) { %>
								<option value="<%=i%>"><%=i%></option>
							<% } %>
							</select>
						</div>
						<div class="cl"></div>

						<div class="form_info cmsms_input">
							<label for="cc-cvv">CVV Code<span class="color_3"> *</span></label>
							<input type="text" name="cc-cvv" id="cc-cvv" value="" size="22" maxlength="4" tabindex="5" class="validate[required,minSize[3],maxSize[4]]"/>
						</div>
						<div class="cl"></div>

						<div class="form_info cmsms_input">
							<label for="txtTotal">Total</label>
							<input type="text" name="txtTotal" id="txtTotal" value="" size="22" disabled />
						</div>
						<div class="cl"></div>

						<div>
							<a href="#" class="button_small" id="btnSubmit" tabindex="6">Next</a>
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
</asp:Content>
