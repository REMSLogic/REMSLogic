<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Front.Master" AutoEventWireup="true" CodeBehind="Signup-Prescriber.aspx.cs" Inherits="Site.Signup_Prescriber" %>

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
				<h4>Create Prescriber Account</h4>
				<div class="cmsms-form-builder">
					<script type="text/javascript">
						$(document).ready(function () {
							$('#contactform').validationEngine('init');

							$('#contactform a#btnSubmit').click(function () {
								var form_builder_url = '/API/Site/Signup/Prescriber';

								$('#contactform .loading').animate({ opacity: 1 }, 250);

								if ($('#contactform').validationEngine('validate')) {
									$.ajax({
										type: 'POST',
										url: form_builder_url,
										data: {
											fname: $('#txtFName').val(),
											lname: $('#txtLName').val(),
											username: $('#txtUsername').val(),
											password: $('#txtPassword').val(),
											email: $('#txtEmail').val(),
											company: $('#txtCompany').val(),
											street1: $('#txtAddress1').val(),
											street2: $('#txtAddress2').val(),
											city: $('#txtCity').val(),
											state: $('#txtState').val(),
											zip: $('#txtZip').val(),
											phone: $('#txtPhone').val(),
											npiid: $('#txtNPIID').val()
										},
										complete: function (data, textStatus, jqXHR) {
											$('#contactform .loading').animate({ opacity: 0 }, 250);

											$('#txtFName').val('');
											$('#txtLName').val('');
											$('#txtUsername').val('');
											$('#txtPassword').val('');
											$('#txtConfirmPassword').val('');
											$('#txtEmail').val('');
											$('#txtCompany').val('');
											$('#txtAddress1').val('');
											$('#txtAddress2').val('');
											$('#txtCity').val('');
											$('#txtState').val('');
											$('#txtZip').val('');
											$('#txtPhone').val('');
											$('#txtNPIID').val('');
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
							<label for="txtFName">First Name<span class="color_3"> *</span></label>
							<input type="text" name="txtFName" id="txtFName" value="" size="22" tabindex="1" class="validate[required,minSize[3],maxSize[255]]"/>
						</div>
						<div class="cl"></div>

						<div class="form_info cmsms_input">
							<label for="txtLName">Last Name<span class="color_3"> *</span></label>
							<input type="text" name="txtLName" id="txtLName" value="" size="22" tabindex="2" class="validate[required,minSize[3],maxSize[255]]"/>
						</div>
						<div class="cl"></div>

						<div class="form_info cmsms_input">
							<label for="txtUsername">Username<span class="color_3"> *</span></label>
							<input type="text" name="txtUsername" id="txtUsername" value="" size="22" tabindex="3" class="validate[required,minSize[3],maxSize[255]]"/>
						</div>
						<div class="cl"></div>

						<div class="form_info cmsms_input">
							<label for="txtPassword">Password<span class="color_3"> *</span></label>
							<input type="password" name="txtPassword" id="txtPassword" value="" size="22" tabindex="4" class="validate[required,minSize[3],maxSize[255]]"/>
						</div>
						<div class="cl"></div>

						<div class="form_info cmsms_input">
							<label for="txtConfirmPassword">Confirm Password<span class="color_3"> *</span></label>
							<input type="password" name="txtConfirmPassword" id="txtConfirmPassword" value="" size="22" tabindex="5" class="validate[required,minSize[3],maxSize[255],equals[txtPassword]]"/>
						</div>
						<div class="cl"></div>

						<div class="form_info cmsms_input">
							<label for="txtEmail">Email<span class="color_3"> *</span></label>
							<input type="text" name="txtEmail" id="txtEmail" value="" size="22" tabindex="6" class="validate[required,custom[email]]" />
						</div>
						<div class="cl"></div>

						<div class="form_info cmsms_input">
							<label for="txtConfirmEmail">Confirm Email<span class="color_3"> *</span></label>
							<input type="text" name="txtConfirmEmail" id="txtConfirmEmail" value="" size="22" tabindex="7" class="validate[required,custom[email],equals[txtEmail]]" />
						</div>
						<div class="cl"></div>

						<div class="form_info cmsms_input">
							<label for="txtCompany">Company Name</label>
							<input type="text" name="txtCompany" id="txtCompany" value="" size="22" tabindex="8" class="validate[minSize[3],maxSize[255]]"/>
						</div>
						<div class="cl"></div>

						<div class="form_info cmsms_input">
							<label for="txtAddress1">Address 1<span class="color_3"> *</span></label>
							<input type="text" name="txtAddress1" id="txtAddress1" value="" size="22" tabindex="9" class="validate[required,minSize[3],maxSize[255]]"/>
						</div>
						<div class="cl"></div>

						<div class="form_info cmsms_input">
							<label for="txtAddress2">Address 2</label>
							<input type="text" name="txtAddress2" id="txtAddress2" value="" size="22" tabindex="10" class="validate[minSize[3],maxSize[255]]"/>
						</div>
						<div class="cl"></div>

						<div class="form_info cmsms_input">
							<label for="txtCity">City<span class="color_3"> *</span></label>
							<input type="text" name="txtCity" id="txtCity" value="" size="22" tabindex="11" class="validate[required,minSize[3],maxSize[255]]"/>
						</div>
						<div class="cl"></div>

						<div class="form_info cmsms_input">
							<label for="txtState">State<span class="color_3"> *</span></label>
							<input type="text" name="txtState" id="txtState" value="" size="22" tabindex="12" class="validate[required,minSize[2],maxSize[255]]"/>
						</div>
						<div class="cl"></div>

						<div class="form_info cmsms_input">
							<label for="txtZip">Zip<span class="color_3"> *</span></label>
							<input type="text" name="txtZip" id="txtZip" value="" size="22" tabindex="13" class="validate[required,minSize[5],maxSize[20]]"/>
						</div>
						<div class="cl"></div>

						<div class="form_info cmsms_input">
							<label for="txtPhone">Phone</label>
							<input type="text" name="txtPhone" id="txtPhone" value="" size="22" tabindex="14" class="validate[minSize[10],maxSize[25]]"/>
						</div>
						<div class="cl"></div>

						<div class="form_info cmsms_input">
							<label for="txtNPIID">NPI ID<span class="color_3"> *</span></label>
							<input type="text" name="txtNPIID" id="txtNPIID" value="" size="22" tabindex="13" class="validate[required,minSize[1],maxSize[255]]"/>
						</div>
						<div class="cl"></div>

						<div>
							<a href="#" class="button_small" id="btnSubmit" tabindex="15">Sign Up</a>
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
