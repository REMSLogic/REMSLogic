<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Site.App.Login" %>

<!DOCTYPE html>
<!--[if IE 7 ]>   <html lang="en" class="ie7 lte8"> <![endif]-->
<!--[if IE 8 ]>   <html lang="en" class="ie8 lte8"> <![endif]-->
<!--[if IE 9 ]>   <html lang="en" class="ie9"> <![endif]-->
<!--[if gt IE 9]> <html lang="en"> <![endif]-->
<!--[if !IE]><!-->
<html lang="en">
<!--<![endif]-->
<head>
    <!--[if lte IE 9 ]><meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"><![endif]-->

    <!-- iPad Settings -->
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" />
    <meta name="viewport" content="width=device-width, minimum-scale=1.0, maximum-scale=1.0" />
    <!-- Adding "maximum-scale=1" fixes the Mobile Safari auto-zoom bug: http://filamentgroup.com/examples/iosScaleBug/ -->
    <!-- iPad End -->
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />

    <title>REMSLogic - Login</title>

    <!-- iOS ICONS -->
    <link rel="apple-touch-icon" href="touch-icon-iphone.png" />
    <link rel="apple-touch-icon" sizes="72x72" href="touch-icon-ipad.png" />
    <link rel="apple-touch-icon" sizes="114x114" href="touch-icon-iphone4.png" />
    <link rel="apple-touch-startup-image" href="touch-startup-image.png">
    <!-- iOS ICONS END -->

    <!-- STYLESHEETS -->

    <link rel="stylesheet" media="screen" href="css/reset.css" />
    <link rel="stylesheet" media="screen" href="css/grids.css" />
    <link rel="stylesheet" media="screen" href="css/style.css" />
    <link rel="stylesheet" media="screen" href="css/ui.css" />
    <link rel="stylesheet" media="screen" href="css/jquery.uniform.css" />
    <link rel="stylesheet" media="screen" href="css/forms.css" />
    <link rel="stylesheet" media="screen" href="css/themes/lightblue/style.css" />
    <link rel="stylesheet" media="screen" href="css/cupertino/jquery-ui-1.10.3.custom.min.css" />

    <style type="text/css">
        #loading-container {
            position: absolute;
            top: 50%;
            left: 50%;
        }

        #loading-content {
            width: 800px;
            text-align: center;
            margin-left: -400px;
            height: 50px;
            margin-top: -25px;
            line-height: 50px;
        }

        #loading-content {
            font-family: "Helvetica", "Arial", sans-serif;
            font-size: 18px;
            color: black;
            text-shadow: 0px 1px 0px white;
        }

        #loading-graphic {
            margin-right: 0.2em;
            margin-bottom: -2px;
        }

        #loading {
            background-color: #abc4ff;
            background-image: -moz-radial-gradient(50% 50%, ellipse closest-side, #abc4ff, #87a7ff 100%);
            background-image: -webkit-radial-gradient(50% 50%, ellipse closest-side, #abc4ff, #87a7ff 100%);
            background-image: -o-radial-gradient(50% 50%, ellipse closest-side, #abc4ff, #87a7ff 100%);
            background-image: -ms-radial-gradient(50% 50%, ellipse closest-side, #abc4ff, #87a7ff 100%);
            background-image: radial-gradient(50% 50%, ellipse closest-side, #abc4ff, #87a7ff 100%);
            height: 100%;
            width: 100%;
            overflow: hidden;
            position: absolute;
            left: 0;
            top: 0;
            z-index: 99999;
        }

		.tos-scroll
		{
			padding: 0 1em;
		}

		.tos-scroll h1
		{
			font-size: 16pt;
			line-height: 16pt;
			font-weight: bold;
			margin: 0;
		}

		.tos-scroll h2
		{
			font-size: 14pt;
			line-height: 14pt;
			margin: 0;
		}

		.tos-scroll p
		{
			margin: 1em 0;
		}

		.tos-scroll ul
		{
			list-style-type: lower-alpha;
			margin-bottom: 1em;
		}

		.tos-scroll li
		{
		}

		.tos-scroll ul ul
		{
			list-style-type: lower-roman;
			margin-bottom: 0;
		}

		.tos-scroll ul ul ul
		{
			list-style-type: decimal;
		}
    </style>

    <!-- STYLESHEETS END -->

    <!--[if lt IE 9]>
	<script src="js/html5.js"></script>
	<script type="text/javascript" src="js/selectivizr.js"></script>
	<![endif]-->

    <script type="text/javascript" src="/js/modernizr.js"></script>

    <!-- MAIN JAVASCRIPTS -->
    <script src="js/jquery.min.js"></script>

    <!-- <script type="text/javascript" src="/js/jquery.tools.min.js"></script> -->
    <script type="text/javascript" src="/js/jquery.validate.js"></script>
    <script type="text/javascript" src="js/jquery.uniform.min.js"></script>
    <script type="text/javascript" src="js/jquery.easing.js"></script>
    <script type="text/javascript" src="js/jquery.ui.totop.js"></script>
    <script type="text/javascript" src="js/jquery.ui.dialog.min.js"></script>
    <!--[if lt IE 9]>
    <script type="text/javascript" src="js/PIE.js"></script>
    <script type="text/javascript" src="js/ie.js"></script>
    <![endif]-->

    <script type="text/javascript" src="js/global.js"></script>
    <!-- MAIN JAVASCRIPTS END -->

    <!-- LOADING SCRIPT -->
    <script>
        $(window).load(function () {
            $("#loading").fadeOut(function () {
                $(this).remove();
                $('body').removeAttr('style');
            });
        });
        $(function () {
            $("#loginDialog").dialog({
                autoOpen: false,
                height: 300,
                width: 350,
                modal: true,
                show: {
                    effect: "fade",
                    duration: 1000
                },
                hide: {
                    effect: "fade",
                    duration: 1000
                }
            });

            $("#loginOpener").click(function () {
                $("#loginDialog").dialog("open");
            });
        });
    </script>
    <!-- LOADING SCRIPT -->
</head>
<body class="login" style="overflow: hidden;">
    <div id="loading">

        <script type="text/javascript">
            document.write("<div id='loading-container'><p id='loading-content'>" +
                           "<img id='loading-graphic' width='16' height='16' src='images/ajax-loader-abc4ff.gif' /> " +
                           "Loading...</p></div>");
        </script>

    </div>

    <div class="login-box">
        <section class="login-box-top">
            <header>
                <h2 class="logo ac">FoodPing Login</h2>
            </header>
            <section>
                <form id="form" class="has-validation" action="/Login.aspx<%=string.IsNullOrEmpty(Request.Url.Fragment) ? "" : "?frag="+Request.Url.Fragment.Substring(1) %>" method="post" style="margin-top: 30px">
                    <% if (!string.IsNullOrEmpty(msg))
                       { %>
                    <div class="error">
                        <%=msg %>
                    </div>
                    <% } %>
                    <div class="user-pass">
                        <input type="text" id="username" class="full" value="" name="username" required="required" placeholder="Username" />
                        <input type="password" id="password" class="full" value="" name="password" required="required" placeholder="Password" />
                    </div>
                    <p class="clearfix">
                        <span class="fl" style="line-height: 23px;">
                            <label class="choice" for="tos">
                                <input type="checkbox" id="tos" class="" value="1" name="tos" />
                                I agree to the <a href="#" id="loginOpener">Terms and Conditions</a>
                           
                            </label>
                        </span>
                        <span class="fl" style="line-height: 23px;">
                            <label class="choice" for="remember">
                                <input type="checkbox" id="remember" class="" value="1" name="remember" />
                                Keep me logged in
                           
                            </label>
                        </span>

                        <button class="fr blue-btn" type="submit">Login</button>
                    </p>
                </form>
            </section>
        </section>
    </div>
    <div id="loginDialog" title="Terms and Conditions" class="tos-scroll">
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
</body>
</html>
