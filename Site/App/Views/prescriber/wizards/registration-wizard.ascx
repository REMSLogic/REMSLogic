<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="registration-wizard.ascx.cs" Inherits="Site.App.Views.prescriber.wizards.RegistrationWizard" %>
<%@ Import Namespace="Lib.Data" %>
<%@ Import Namespace="RemsLogic.Model" %>

<script type="text/javascript" src="/App/js/passwordStrengthMeter.js"></script>
<script type="text/javascript" src="/App/js/jquery.tools.min.js"></script>
<script type="text/javascript" src="/App/js/jquery.wizard.js"></script>

<h1 class="page-title">Prescriber Registration</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
        <form action="/api/Prescriber/Wizards/FirstLogin?id=<%=PrescriberProfile.ID%>" class="wizard form ajax-form" novalidate>
            <nav>
                <ul class="clearfix">
                    <li class="active">Step <strong>1</strong></li>
                    <li>Step <strong>2</strong></li>
                    <li>Step <strong>3</strong></li>
                    <li>Step <strong>4</strong></li>
                    <li>Step <strong>5</strong></li>
                    <li>Step <strong>6</strong></li>
                </ul>
            </nav>
            <div class="items" >
                <!-- page1 -->
                <section>
                    <header>
                        <h2>
                            <strong>Step 1: </strong> Terms of Service
                            <em>You must agree to the terms of service below</em>
                        </h2>
                    </header>

                    <section>
                        <div style="overflow-y: scroll; height: 250px; margin-bottom: 20px; border: 1px solid rgb(204, 204, 204); padding: 10px;">
                            <h1>Terms of Service for REMS Logic&trade;</h1>
							<h2>Introduction</h2>
							<p>Welcome to REMS Logic&trade;. This website is owned and operated by REMS Logic, LLC. By visiting our website and accessing the information, resources, services, products, and tools we provide, you understand and agree to accept and adhere to the following terms and conditions as stated in this policy (hereafter referred to as 'User Agreement'), along with the terms and conditions as stated in our Privacy Policy (please refer to the Privacy Policy section below for more information).</p>
							<p>This agreement is in effect as of August 1, 2013.</p>
							<p>We reserve the right to change this User Agreement from time to time without notice. You acknowledge and agree that it is your responsibility to review this User Agreement periodically to familiarize yourself with any modifications. Your continued use of this site after such modifications will constitute acknowledgment and agreement of the modified terms and conditions.</p>
							<h2>Responsible Use and Conduct</h2>
							<p>By visiting our website and accessing the information, resources, services, products, and tools we provide for you, either directly or indirectly (hereafter referred to as 'Resources'), you agree to use these Resources only for the purposes intended as permitted by (a) the terms of this User Agreement, and (b) applicable laws, regulations and generally accepted online practices or guidelines.</p>
							<p>Wherein, you understand that:</p>
							<ul>
								<li>In order to access our Resources, you may be required to provide certain information about yourself (such as identification, contact details, etc.) as part of the registration process, or as part of your ability to use the Resources. You agree that any information you provide will always be accurate, correct, and up to date.</li>
								<li>You are responsible for maintaining the confidentiality of any login information associated with any account you use to access our Resources. Accordingly, you are responsible for all activities that occur under your account/s.</li>
								<li>Accessing (or attempting to access) any of our Resources by any means other than through the means we provide, is strictly prohibited. You specifically agree not to access (or attempt to access) any of our Resources through any automated, unethical or unconventional means.</li>
								<li>Engaging in any activity that disrupts or interferes with our Resources, including the servers and/or networks to which our Resources are located or connected, is strictly prohibited.</li>
								<li>Attempting to copy, duplicate, reproduce, sell, trade, or resell our Resources is strictly prohibited.</li>
								<li>You are solely responsible any consequences, losses, or damages that we may directly or indirectly incur or suffer due to any unauthorized activities conducted by you, as explained above, and may incur criminal or civil liability.</li>
								<li>We may provide various open communication tools on our website, such as blog comments, blog posts, public chat, forums, message boards, newsgroups, product ratings and reviews, various social media services, etc. You understand that generally we do not pre-screen or monitor the content posted by users of these various communication tools, which means that if you choose to use these tools to submit any type of content to our website, then it is your personal responsibility to use these tools in a responsible and ethical manner. By posting information or otherwise using any open communication tools as mentioned, you agree that you will not upload, post, share, or otherwise distribute any content that:
									<ul>
										<li>Is illegal, threatening, defamatory, abusive, harassing, degrading, intimidating, fraudulent, deceptive, invasive, racist, or contains any type of suggestive, inappropriate, or explicit language;</li>
										<li>Infringes on any trademark, patent, trade secret, copyright, or other proprietary right of any party;</li>
										<li>Contains any type of unauthorized or unsolicited advertising;</li>
										<li>Impersonates any person or entity, including any REMS Logic&trade; employees or representatives.</li>
									</ul>
									We have the right at our sole discretion to remove any content that, we feel in our judgment does not comply with this User Agreement, along with any content that we feel is otherwise offensive, harmful, objectionable, inaccurate, or violates any 3rd party copyrights or trademarks. We are not responsible for any delay or failure in removing such content. If you post content that we choose to remove, you hereby consent to such removal, and consent to waive any claim against us.</li>
								<li>We do not assume any liability for any content posted by you or any other 3rd party users of our website. However, any content posted by you using any open communication tools on our website, provided that it doesn't violate or infringe on any 3rd party copyrights or trademarks, becomes the property of REMS Logic, LLC, and as such, gives us a perpetual, irrevocable, worldwide, royalty-free, exclusive license to reproduce, modify, adapt, translate, publish, publicly display and/or distribute as we see fit. This only refers and applies to content posted via open communication tools as described, and does not refer to information that is provided as part of the registration process, necessary in order to use our Resources. All information provided as part of our registration process is covered by our privacy policy.</li>
								<li>You agree to indemnify and hold harmless REMS Logic, LLC and its parent company and affiliates, and their directors, officers, managers, employees, donors, agents, and licensors, from and against all losses, expenses, damages and costs, including reasonable attorneys' fees, resulting from any violation of this User Agreement or the failure to fulfill any obligations relating to your account incurred by you or any other person using your account. We reserve the right to take over the exclusive defense of any claim for which we are entitled to indemnification under this User Agreement. In such event, you shall provide us with such cooperation as is reasonably requested by us.</li>
							</ul>
							<h2>Privacy</h2>
							<p>Your privacy is very important to us, which is why we've created a separate Privacy Policy in order to explain in detail how we collect, manage, process, secure, and store your private information. Our privacy policy is included under the scope of this User Agreement. To read our privacy policy in its entirety, click here.</p>
							<h2>Limitation of Warranties</h2>
							<p>By using our website, you understand and agree that all Resources we provide are "as is" and "as available". This means that we do not represent or warrant to you that:</p>
							<ul>
								<li>the use of our Resources will meet your needs or requirements.</li>
								<li>the use of our Resources will be uninterrupted, timely, secure or free from errors.</li>
								<li>the information obtained by using our Resources will be accurate or reliable, and</li>
								<li>any defects in the operation or functionality of any Resources we provide will be repaired or corrected.
									<p>Furthermore, you understand and agree that:</p>
								</li>
								<li>any content downloaded or otherwise obtained through the use of our Resources is done at your own discretion and risk, and that you are solely responsible for any damage to your computer or other devices for any loss of data that may result from the download of such content.</li>
								<li>no information or advice, whether expressed, implied, oral or written, obtained by you from REMS Logic, LLC or through any Resources we provide shall create any warranty, guarantee, or conditions of any kind, except for those expressly outlined in this User Agreement.</li>
							</ul>
							<h2>Limitation of Liability</h2>
							<p>In conjunction with the Limitation of Warranties as explained above, you expressly understand and agree that any claim against us shall be limited to the amount you paid, if any, for use of products and/or services. REMS Logic, LLC will not be liable for any direct, indirect, incidental, consequential or exemplary loss or damages which may be incurred by you as a result of using our Resources, or as a result of any changes, data loss or corruption, cancellation, loss of access, or downtime to the full extent that applicable limitation of liability laws apply.</p>
							<h2>Copyrights/Trademarks</h2>
							<p>All content and materials available on REMS Logic&trade;, including but not limited to text, graphics, website name, code, images and logos are the intellectual property of REMS Logic, LLC, and are protected by applicable copyright and trademark law. Any inappropriate use, including but not limited to the reproduction, distribution, display or transmission of any content on this site is strictly prohibited, unless specifically authorized by REMS Logic, LLC.</p>
							<h2>Termination of Use</h2>
							<p>You agree that we may, at our sole discretion, suspend or terminate your access to all or part of our website and Resources with or without notice and for any reason, including, without limitation, breach of this User Agreement. Any suspected illegal, fraudulent or abusive activity may be grounds for terminating your relationship and may be referred to appropriate law enforcement authorities. Upon suspension or termination, your right to use the Resources we provide will immediately cease, and we reserve the right to remove or delete any information that you may have on file with us, including any account or login information.</p>
							<h2>Governing Law</h2>
							<p>This website is controlled by REMS Logic, LLC from our offices located in the state of OH, USA. It can be accessed by most countries around the world. As each country has laws that may differ from those of OH, by accessing our website, you agree that the statutes and laws of OH, without regard to the conflict of laws and the United Nations Convention on the International Sales of Goods, will apply to all matters relating to the use of this website and the purchase of any products or services through this site.</p>
							<p>Furthermore, any action to enforce this User Agreement shall be brought in the federal or state courts located in USA, OH You hereby agree to personal jurisdiction by such courts, and waive any jurisdictional, venue, or inconvenient forum objections to such courts.</p>
							<h2>Guarantee</h2>
							<p>UNLESS OTHERWISE EXPRESSED, REMS Logic, LLC EXPRESSLY DISCLAIMS ALL WARRANTIES AND CONDITIONS OF ANY KIND, WHETHER EXPRESS OR IMPLIED, INCLUDING, BUT NOT LIMITED TO THE IMPLIED WARRANTIES AND CONDITIONS OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT.</p>
							<h2>Contact Information</h2>
							<p>If you have any questions or comments about these our Terms of Service as outlined above, you can contact us at:</p>
							<p>REMS Logic, LLC<br />
							P.O. Box 602<br />
							Chagrin Falls, OH 44022<br />
							USA<br />
							www.remslogic.com</p>
                        </div>
                        <input type="checkbox" name="agree-to-terms" required="required" /> I agree to the terms of service.
                    </section>
                    
                    <footer class="clearfix">
                        <button type="button" class="next fr tos-proceed-button" >Proceed &raquo;</button>
                    </footer>
                </section>
                
                <!-- page 2 -->
                <section>
                    <header>
                        <h2>
                            <strong>Step 2:</strong> Facility Information
                            <em>Select your primary facility</em>
                        </h2>
                    </header>
                    
                    <section>
                        <div class="grid_12">
                            <div class="form">
                                <div class="clearfix">
                                    <label class="form-label">Name</label>
                                    <div class="form-input">
                                        <select id="form-facility-id" name="facility-id" required="required" >
                                        <%foreach(Facility f in Facilities){%>
                                            <option value="<%=f.Id%>"><%=f.Name%></option>
                                        <%}%>
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </section>

                    <footer class="clearfix">
                        <button type="button" class="prev fl">&laquo; Back</button>
                        <button type="button" class="next fr">Proceed &raquo;</button>
                    </footer>
                </section>

                <!-- page3 -->
                <section>
                    <header>
                        <h2>
                            <strong>Step 3:</strong> Create your password
                            <em>Enter your password:</em>
                        </h2>
                    </header>
                
                    <section>
                        <div class="grid_12">
                            <div class="form">
                                <div class="clearfix">
                                    <label for="form-new-password" class="form-label">New Password <em>*</em></label>
                                    <div class="form-input"><input type="password" id="form-new-password" name="new-password" placeholder="Enter your new password" value="" required="required" /></div>
                                </div>

                                <div class="clearfix">
                                    <label for="form-confirm-password" class="form-label">Confirm Password <em>*</em></label>
                                    <div class="form-input"><input type="password" id="form-confirm-password" name="confirm-password" placeholder="Confirm your new password" value="" required="required" data-indicator="pwindicator" /></div>
                                </div>
                                
                                <div id="pwindicator">
                                    <label class="form-label">Password Strength</label>
                                    <div class="form-input"><div id="result" class="form-info">&nbsp;</div></div>
                                </div>
                            </div>
                        </div>
                    </section>
                
                    <footer class="clearfix">
                        <button type="button" class="prev fl">&laquo; Back</button>
                        <button type="button" class="next fr">Proceed &raquo;</button>
                    </footer>
                </section>
                
                <!-- page4 -->
                <section>
                    <header>
                        <h2>
                            <strong>Step 4:</strong> Video
                            <em>Please take two minutes and watch the following video:</em>
                        </h2>
                    </header>
                
                    <section>
                          <video id="training_video_1" class="video-js vjs-default-skin" controls preload="none" width="100%" height="600"
                              poster="../App/Videos/training-1-cover.png"
                              data-setup="{}">
                            <source src="../App/Videos/training-1.mp4" type='video/mp4' />
                            <p class="vjs-no-js">To view this video please enable JavaScript, and consider upgrading to a web browser that <a href="http://videojs.com/html5-video-support/" target="_blank">supports HTML5 video</a></p>
                          </video>
                        <input type="checkbox" name="watched-video" required="required" /> I watched the complete video.
                    </section>
                
                    <footer class="clearfix">
                        <button type="button" class="prev fl">&laquo; Back</button>
                        <button type="button" class="next fr video-proceed-button">Proceed &raquo;</button>
                    </footer>
                </section>
                
                <!-- page5 -->
                <section>
                    <header>
                        <h2>
                            <strong>Step 5:</strong> Personal Information
                            <em>Tell us about yourself</em>
                        </h2>
                    </header>
                    
                    <section>
                        <!-- Contact Information -->
                        <h3>Contact Information</h3>
                        <div class="grid_12" style="margin-bottom: 20px;">
                            <div class="form">
                                <div class="clearfix">
                                    <label for="form-prefix" class="form-label">Prefix</label>
                                    <div class="form-input"><input type="text" id="form-prefix" name="prefix" placeholder="" value="" /></div>
                                </div>

                                <div class="clearfix">
                                    <label for="form-first-name" class="form-label">First Name <em>*</em></label>
                                    <div class="form-input"><input type="text" id="form-first-name" name="first-name" placeholder="Enter your first name" value="<%=Contact.FirstName%>" required="required" /></div>
                                </div>
                                
                                <div class="clearfix">
                                    <label for="form-last-name" class="form-label">Last Name <em>*</em></label>
                                    <div class="form-input"><input type="text" id="form-last-name" name="last-name" placeholder="Enter your last name" value="<%=Contact.LastName%>" required="required" /></div>
                                </div>
                                
                                <div class="clearfix">
                                    <label for="form-postfix" class="form-label">Postfix</label>
                                    <div class="form-input"><input type="text" id="form-postfix" name="postifx" placeholder="" value="" /></div>
                                </div>
                                
                                <div class="clearfix">
                                    <label for="form-title" class="form-label">Title</label>
                                    <div class="form-input"><input type="text" id="form-title" name="title" placeholder="Enter your title" value="" /></div>
                                </div>
                                
                                <div class="clearfix">
                                    <label for="form-email" class="form-label">Email Address <em>*</em></label>
                                    <div class="form-input"><input type="email" id="form-email" name="email" placeholder="Enter your email address" value="<%=PrescriberProfile.Contact.Email%>" required="required" /></div>
                                </div>
                                
                                <div class="clearfix">
                                    <label for="form-phone" class="form-label">Phone Number</label>
                                    <div class="form-input"><input type="text" id="form-phone" name="phone" placeholder="Enter your phone number" value="<%=Contact.Phone%>" /></div>
                                </div>
                                
                                <div class="clearfix">
                                    <label for="form-fax" class="form-label">Fax Number</label>
                                    <div class="form-input"><input type="text" id="form-fax" name="fax" placeholder="Enter your fax number" value="<%=Contact.Fax%>" /></div>
                                </div>
                            </div>
                        </div>

                        <!-- Address Information -->
                        
                        <h3>Address</h3>
                        <div class="grid_12">
                            <div class="form">
                                <div class="clearfix">
                                    <label for="form-street-1" class="form-label">Street 1 <em>*</em></label>
                                    <div class="form-input"><input type="text" id="form-street-1" name="street-1" placeholder="Enter your street" value="<%=Address.Street1%>" required="required" /></div>
                                </div>

                                <div class="clearfix">
                                    <label for="form-street-2" class="form-label">Street 2</label>
                                    <div class="form-input"><input type="text" id="form-street-2" name="street-2" placeholder="" value="<%=Address.Street2%>" /></div>
                                </div>
                                
                                <div class="clearfix">
                                    <label for="form-city" class="form-label">City <em>*</em></label>
                                    <div class="form-input"><input type="text" id="form-city" name="city" placeholder="Enter your city" value="<%=Address.City%>" required="required" /></div>
                                </div>
                                
                                <div class="clearfix">
                                    <label for="form-state" class="form-label">State <em>*</em></label>
                                    <div class="form-input"><input type="text" id="form-state" name="state" placeholder="Enter your state" value="<%=Address.State%>" required="required" /></div>
                                </div>
                                
                                <div class="clearfix">
                                    <label for="form-zip" class="form-label">Zip <em>*</em></label>
                                    <div class="form-input"><input type="text" id="form-zip" name="zip" placeholder="Enter your zip code" value="<%=Address.Zip%>" required="required" /></div>
                                </div>
                                
                                <div class="clearfix">
                                    <label for="form-country" class="form-label">Country <em>*</em></label>
                                    <div class="form-input"><input type="text" id="form-country" name="country" placeholder="Enter your country" value="<%=Address.Country%>" required="required" /></div>
                                </div>
                            </div>
                        </div>
                    </section>
                    
                    <footer class="clearfix">
                        <button type="button" class="prev fl">&laquo; Back</button>
                        <button type="button" class="next fr">Proceed &raquo;</button>
                    </footer>
                </section>

                <!-- page6 -->
                <section>
                    <header>
                        <h2>
                            <strong>Step 6:</strong> Prescriber Details
                            <em>Please provide your Prescriber details</em>
                        </h2>
                    </header>
                    
                    <section>
                        <div class="grid_12">
                            <div class="form">
                                <div class="clearfix">
                                    <label for="form-prescriber-type" class="form-label">Prescriber Type <em>*</em></label>
                                    <div class="form-input">
                                        <select id="form-prescriber-type" name="prescriber-type" required="required" >
                                        <%foreach(PrescriberType pt in PrescriberTypes){%>
                                            <option><%=pt.DisplayName%></option>
                                        <%}%>
                                        </select>
                                    </div>
                                </div>
                                
                                <div class="clearfix">
                                    <label for="form-prescriber-speciality" class="form-label">Speciality <em>*</em></label>
                                    <div class="form-input">
                                        <select id="form-prescriber-speciality" name="prescriber-speciality" required="required" >
                                        <%foreach(Speciality s in Specialities){%>
                                            <option value="<%=s.ID%>"><%=s.Name%></option>
                                        <%}%>
                                        </select>
                                    </div>
                                </div>

                                <div class="clearfix">
                                    <label for="form-npi" class="form-label">NPI Number</label>
                                    <div class="form-input"><input type="text" id="form-npi" name="npi" placeholder="Enter your NPI Number" value=""/></div>
                                </div>
                                
                                <div class="clearfix">
                                    <label for="form-state-id" class="form-label">State ID <em>*</em></label>
                                    <div class="form-input">
                                        <input type="text" id="form-state-id" name="state-id" placeholder="Enter your State Id" value="" required="required" />
                                    </div>
                                </div>
                                
                                <div class="clearfix">
                                    <label for="form-state-id" class="form-label">Issuing State <em>*</em></label>
                                    <div class="form-input">
                                        <select id="form-issuer" name="issuer" required="required" >
                                        <%foreach(State s in States){%>
                                            <option value="<%=s.ID%>"><%=s.USPS%></option>
                                        <%}%>
                                        </select>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </section>

                    <footer class="clearfix">
                        <button type="button" class="prev fl">&laquo; Back</button>
                        <button type="submit" class="next fr">Submit &raquo;</button>
                    </footer>
                </section>
            </div>
        </form>
    </div>
</div>

<script type="text/javascript">
    $(window).bind('content-loaded', function () {

        try {
            $('.wizard').wizard();
            // MJL 2014-01-13 - the below trigger seems to fix a formatting issue.
            //$('.wizard').trigger('resize');
        } catch (ex) { }

        $('input[name="watched-video"]').change(function () {
            if ($('input[name="watched-video"]').is(':checked'))
                $('.video-proceed-button').removeAttr('disabled');
            else
                $('.video-proceed-button').attr('disabled', 'disabled');
        });

        $('input[name="agree-to-terms"]').change(function () {
            if ($('input[name="agree-to-terms"]').is(':checked'))
                $('.tos-proceed-button').removeAttr('disabled');
            else
                $('.tos-proceed-button').attr('disabled', 'disabled');
        });

        $('#form-new-password').keyup(function () {
            $('#result').html(passwordStrength($('#form-new-password').val(), ""));
        });
    });

    $(window).bind('page-animation-completed', function () {
        $('.wizard').trigger('resize');
        $('#training_video_1').height($('#training_video_1').width() * 0.6136);
    });
</script>