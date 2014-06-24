<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="landing-page.ascx.cs" Inherits="Site.App.Views.prescriber.wizards.landing_page" %>

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

<h1 class="page-title">Setup Prescriber Profile</h1>
<div class="container_12 clearfix leading">
    <div class="login-box" style="top: 225px;">
    <section class="login-box-top">
        <header>
            <h2 class="logo ac">FoodPing Login</h2>
        </header>
        <section style="padding-top: 24px;">
            Add this prescriber profile to an existing REMS Logic account:
        </section>
        <section>
            <form id="form" class="has-validation ajax-form" action="/api/Prescriber/Wizards/AttachProfile?id=<%=PrescriberProfile.ID%>" style="margin-top: 30px;" >
                <div class="user-pass">
                    <input type="text" id="username" class="full" value="" name="username" required="required" placeholder="Username" />
                    <input type="password" id="password" class="full" value="" name="password" required="required" placeholder="Password" />
                </div>
                <p class="clearfix" style="text-align: right;">
                    <button type="submit" style="margin-right: 8px;">Add Profile</button>
                    <a class="button" href="#prescriber/wizards/registration-wizard?token=<%=Token%>&reset=<%=Reset%>">Register</a>
                </p>
            </form>
        </section>
    </section>
    </div>
</div>