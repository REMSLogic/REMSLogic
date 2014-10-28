<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ecommerce.aspx.cs" Inherits="Site.Ecommerce" %>

<!DOCTYPE html>
<!--[if IE 7 ]>   <html lang="en" class="ie7 lte8"> <![endif]--> 
<!--[if IE 8 ]>   <html lang="en" class="ie8 lte8"> <![endif]--> 
<!--[if IE 9 ]>   <html lang="en" class="ie9"> <![endif]--> 
<!--[if gt IE 9]> <html lang="en"> <![endif]-->
<!--[if !IE]><!--> <html lang="en"> <!--<![endif]-->
<head>
	<!--[if lte IE 9 ]><meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"><![endif]-->

	<!-- iPad Settings -->
	<meta name="apple-mobile-web-app-capable" content="yes" />
	<meta name="apple-mobile-web-app-status-bar-style" content="black-translucent" /> 
	<meta name="viewport" content="width=device-width, minimum-scale=1.0, maximum-scale=1.0" />
	<!-- Adding "maximum-scale=1" fixes the Mobile Safari auto-zoom bug: http://filamentgroup.com/examples/iosScaleBug/ -->
	<!-- iPad Settings End -->
	<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />

	<title>REMSLogic</title>
	
	<link rel="shortcut icon" href="/favicon.ico">

	<!-- iOS ICONS -->
	<link rel="apple-touch-icon" href="/touch-icon-iphone.png" />
	<link rel="apple-touch-icon" sizes="72x72" href="/touch-icon-ipad.png" />
	<link rel="apple-touch-icon" sizes="114x114" href="/touch-icon-iphone4.png" />
	<link rel="apple-touch-startup-image" href="/touch-startup-image.png">
	<!-- iOS ICONS END -->

	<!-- STYLESHEETS -->
	<link rel="stylesheet" href="/css/reset.css" media="screen" />
	<link rel="stylesheet" href="/css/cupertino/jquery-ui-1.10.3.custom.css" media="screen" />
	<link rel="stylesheet" href="/css/grids.css" media="screen" />
	<link rel="stylesheet" href="/css/ui.css" media="screen" />
	<link rel="stylesheet" href="/css/forms.css" media="screen" />
	<link rel="stylesheet" href="/css/device/general.css" media="screen" />
    <link href="/css/Fonts/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href='http://fonts.googleapis.com/css?family=Open+Sans' rel='stylesheet' type='text/css'>
	<!--[if !IE]><!-->
	<!--<link rel="stylesheet" href="/css/device/tablet.css" media="only screen and (min-width: 768px) and (max-width: 991px)" />
	<link rel="stylesheet" href="/css/device/mobile.css" media="only screen and (max-width: 767px)" />
	<link rel="stylesheet" href="/css/device/wide-mobile.css" media="only screen and (min-width: 480px) and (max-width: 767px)" />-->
	<link rel="stylesheet" href="/css/device/mobile.css" media="only screen and (max-width: 991px)" />
	<link rel="stylesheet" href="/css/device/wide-mobile.css" media="only screen and (min-width: 480px) and (max-width: 991px)" />
	<!--<![endif]-->
	<link rel="stylesheet" href="/css/jquery.uniform.css" media="screen" />
	<link rel="stylesheet" href="/css/jquery.popover.css" media="screen">
	<link rel="stylesheet" href="/css/jquery.itextsuggest.css" media="screen">
	<link rel="stylesheet" href="/css/themes/lightblue/style.css" media="screen" />
	<link rel="stylesheet" media="screen" href="/css/notifications.css" />
    <link href="/css/app-overrides.css" rel="stylesheet" type="text/css" media="screen" />
    <style type="text/css">
         #loading-container
         {
             position: absolute;
             top: 50%;
             left: 50%;
         }
        #loading-content
        {
            width: 800px;
            text-align: center;
            margin-left: -400px;
            height: 50px;
            margin-top: -25px;
            line-height: 50px;
        }
        #loading-content
        {
            font-family: "Helvetica" , "Arial" , sans-serif;
            font-size: 18px;
            color: black;
            text-shadow: 0px 1px 0px white;
        }
        #loading-graphic
        {
            margin-right: 0.2em;
            margin-bottom: -2px;
        }
        #loading
        {
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
        .agree-checkbox {
            text-align: right;
            font-size: 14pt;
            font-weight: bold;
            margin-top: 36px;
        }
    </style>
	<!-- STYLESHEETS END -->
	
	<!-- MAIN JAVASCRIPTS -->

	<!--[if lt IE 9]>
	<script type="text/javascript" src="/js/html5.js"></script>
	<script type="text/javascript" src="/js/selectivizr.js"></script>
	<![endif]-->

	<script type="text/javascript" src="/js/modernizr.js"></script>

	<script type="text/javascript" src="/js/lib/json2.js"></script>
    <script type="text/javascript" src="/js/jquery.min.js"></script>
	<script type="text/javascript" src="/js/jquery-ui-1.10.3.custom.min.js"></script>
	<script type="text/javascript" src="/js/jquery.validate.js"></script>
    <script type="text/javascript" src="/js/jquery.uniform.js"></script>
    <script type="text/javascript" src="/js/jquery.easing.js"></script>
    <script type="text/javascript" src="/js/jquery.ui.totop.js"></script>
    <script type="text/javascript" src="/js/jquery.itextsuggest.js"></script>
    <script type="text/javascript" src="/js/jquery.itextclear.js"></script>
    <script type="text/javascript" src="/js/jquery.hashchange.min.js"></script>
    <script type="text/javascript" src="/js/jquery.drilldownmenu.js"></script>
    <script type="text/javascript" src="/js/jquery.popover.js"></script>

    <!--[if lt IE 9]>
    <script type="text/javascript" src="/js/PIE.js"></script>
    <script type="text/javascript" src="/js/ie.js"></script>
    <![endif]-->

	<script type="text/javascript" src="/js/lib/file-upload/jquery.ui.widget.js"></script>
	<script type="text/javascript" src="/js/jquery.notify.js"></script>
	
	<script type="text/javascript" src="/js/lib/file-upload/jquery.iframe-transport.js"></script>
	<script type="text/javascript" src="/js/lib/file-upload/jquery.fileupload.js"></script>

    <script type="text/javascript" src="/js/global.js"></script>
    <!-- MAIN JAVASCRIPTS END -->

	
</head>
<body>
	<div id="loading"> 

        <script type = "text/javascript">
            document.write("<div id='loading-container'><p id='loading-content'>" +
                           "<img id='loading-graphic' width='16' height='16' src='/images/ajax-loader-abc4ff.gif' /> " +
                           "Loading...</p></div>");
        </script>
    </div>
    <div id="wrapper" class="app-wrap">
                <section id="main-content" class="clearfix" style="margin: 36px;">
					
                </section>

	</div>

	<!-- LOADING SCRIPT -->
    <script type="text/javascript">
        $(window).load(function () {
            $("#loading").fadeOut(function () {
                $(this).remove();
                $('body').removeAttr('style');
            });
        });
    </script>
    <!-- LOADING SCRIPT -->
    
	<!-- GROWL CONTAINER -->
	<div id="container">
		<div id="default-container">
			<h1>#{title}</h1>
			<p>#{text}</p>
		</div>
			
		<div id="sticky-container">
			<a class="ui-notify-close ui-notify-cross" href="#">x</a>
			<h1>#{title}</h1>
			<p>#{text}</p>
		</div>
	</div>
	<script type="text/javascript">
	    (function () {
	        var $container;

	        window.growl_create = function (template, vars, opts) {
	            var default_opts = { speed: 500 };
	            if (template == 'sticky-container')
	                default_opts = { expires: false, speed: 500 }
	            return $container.notify("create", template, vars, $.extend(true, {}, default_opts, opts));
	        }

	        $(function () {
	            $container = $("#container").notify();
	        });
	    })();
	</script>
	<!-- GROWL CONTAINER END -->
</body>
</html>