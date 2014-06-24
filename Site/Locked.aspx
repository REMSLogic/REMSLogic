<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Locked.aspx.cs" Inherits="Site.App.Locked" %>

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
        <section>
            <!-- Sidebar -->
            <aside>
                <nav class="profile">
                    <ul>
                        <li>
                            <a href="#account/profile" title="Account"><i class="fa fa-user-md fa-fw app-icon-nav"></i><span><%=Framework.Security.Manager.GetUser().Username %></span></a>
                            <a href="/Logout.aspx" title="Logout" class="button icon-with-text logout">Logout</a>
                        </li>
                    </ul>
                </nav>
                <nav class="drilldownMenu clearfix east-app">
                    <h1>
                        <span class="title">Main Menu</span>
                        <button title="Go Back" class="back">Back</button>
						<div class="clear"></div>
                    </h1>
                    <div class="clearfix" id="searchform">
                        <div class="searchcontainer">
                            <div class="searchbox" onclick="$(this).find('input').focus();">
                                <input type="text" name="q" id="q" autocomplete="off" placeholder="Search...">
                            </div>
                            <input type="button" value="Cancel" />
                        </div>
                        <div class="search_results"></div>
                    </div>
                    <ul class="tlm">
						<li class="current"><a href="#dashboard" title="Dashboard" onclick="return showDisabledGrowl();"><img src="/images/navicons/81.png" alt=""/><span>Dashboard</span></a></li>
						<li><a href="#prescriber/drugs/list" title="Drug List" onclick="return showDisabledGrowl();"><img src="/images/navicons/15.png" alt=""/><span>My Drug List</span></a></li>
						<li><a href="#account/prefs" title="Preferences" onclick="return showDisabledGrowl();"><img src="/images/navicons/19.png" alt=""/><span>Preferences</span></a></li>
						<li><a href="#account/profile" title="Account" onclick="return showDisabledGrowl();"><img src="/images/navicons/111.png" alt=""/><span>Account</span></a></li>
						
                    </ul>
                </nav>
				<!--<div id="help-icons-sidebar">
                    <h1>Icon Meanings</h1>
                    <ul>
                        <li><img src="/images/icons/ETASU.png" alt="ETASU" /> ETASU</li>
						<li><img src="/images/icons/FP EN.png" alt="FP/EN" /> Facility/Pharmacy Enrollment</li>
						<li><img src="/images/icons/PAEN.png" alt="PAEN" /> Patient Enrollment</li>
						<li><img src="/images/icons/PREN.png" alt="PAEN" /> Prescriber Enrollment</li>
						<li><img src="/images/icons/EDUCRT.png" alt="EDUCRT" /> Education/Certification</li>
						<li><img src="/images/icons/MON.png" alt="MON" /> Monitoring</li>
                        <li><img src="/images/icons/MG.png" alt="MG" /> Medication Guide</li>
                        <li><img src="/images/icons/IC.png" alt="IC" /> Informed Consent</li>
                        <li><img src="/images/icons/FD.png" alt="F&amp;D" /> Forms and Documents</li>
                        <li><img src="/images/icons/PR.png" alt="PR" /> Pharmacy Requirements</li>
                    </ul>
                </div>-->
            </aside>

            <!-- Sidebar End -->

            <section>
                <div class="main-content-header">
                </div>
                <section id="main-content" class="clearfix">
					
                </section>
                <footer class="clearfix">
                    <div class="container_12">
                        <div class="grid_12">
                            
                        </div>
                    </div>
                </footer>
            </section>

            <!-- Main Section End -->
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

	<!-- POPOVERS SETUP-->
	<div id="notifications-popover" class="popover">
        <header class="clearfix">
            <div style="float: left; padding-left: 8px;">Notifications</div>
            <div style="float: right; padding-right: 8px;"><a class="button" href="#common/notifications/archive" />View Archive</a></div>
        </header>
        <section>
            <div class="content">
                <nav>
                    <ul>
						<% if (this.notifications.Count > 0)
         { %>
						<% foreach (var n in this.notifications)
         { %>
						<li class="<%=(n.read) ? "read" : "new" %>" data-id="<%=n.notification.ID %>">
						    <div class="content">
						    <div class="message"><%=n.notification.Message %></div>
                            <div class="controls">
                                <% if(!String.IsNullOrEmpty(n.notification.Link)){ %>
                                <a class="button" style="display: inline-block; position: absolute; top: 0; right: 74px;" href="<%=n.notification.Link%>" target="<%=(n.notification.LinkType == "link")? "_blank" : "" %>">View More</a>
                                <% } %>
                                <input data-id="<%=n.notification.ID%>" type="button" value="Archive" style="position: absolute; top: 0; right: 0;" />
                            </div>
                            </div>
						</li>
						<% } %>
						<% } else { %>
						<li class="read">No new notifications.</li>
						<% } %>
                        <!--<li class="new"><span class="avatar"></span>John Doe created a new project</li>>-->
                    </ul>
                </nav>
            </div>
        </section>
    </div>
    <!-- POPOVERS SETUP END-->

	<script type="text/javascript">
	    $(document).ready(function () {
	        $('#notifications-button').popover('#notifications-popover', { preventRight: true });

	        $('#notifications-popover li div.controls input').click(function (event) {
	            var $this = $(this);
	            var li = $this.closest('li');
	            var id = parseInt($this.attr('data-id'));

	            if (id) {
	                $.ajax({
	                    url: '/api/Notifications/Archive',
	                    data: 'id=' + encodeURIComponent(id),
	                    cache: false,
	                    success: function (response) {
	                        li.remove();
	                    }
	                });
	            }

	            event.preventDefault();
	            return false;
	        });

	        $('#notifications-popover li.new').click(function () {
	            var $this = $(this);
	            var id = parseInt($this.attr('data-id'));

	            if (id) {
	                $.ajax({
	                    url: '/api/Notifications/MarkRead',
	                    data: 'id=' + encodeURIComponent(id),
	                    cache: false,
	                    success: function (response) {
	                        $this.removeClass('new').addClass('read');

	                        var s = $('#notifications-button .message-count');
	                        var cnt = parseInt(s.text());

	                        if (cnt > 1) {
	                            s.text(cnt - 1);
	                        }
	                        else {
	                            s.remove();
	                        }
	                    }
	                });
	            }
	        });

	        /**
	        * setup search
	        */
	        var last_q;
	        function textSearch(q) {
	            if (!q || q.trim() == '' || q == last_q)
	                return;

	            last_q = q;
	            $('#searchform .searchbox a').fadeOut()
	            $.ajax({
	                url: '/api/Search/DoSearch',
	                data: 'q=' + encodeURIComponent(q),
	                cache: false,
	                success: function (response) {
	                    $('.search_results').html(response);
	                }
	            });
	        }

	        // Set iTextSuggest
	        $('#searchform .searchbox').length && $('#searchform .searchbox').find('input[type=text]').iTextClear().iTextSuggest({
	            url: '/api/Search/Suggest',
	            onKeydown: function (query) {
	                textSearch(query);
	            },
	            onChange: function (query) {
	                textSearch(query);
	            },
	            onSelect: function (query) {
	                textSearch(query);
	            },
	            onSubmit: function (query) {
	                textSearch(query);
	            },
	            onEmpty: function () {
	                $('.search_results').html('');
	            }
	        }).focus(function () {
	            $('#wrapper > section > aside > nav > ul').fadeOut(function () {
	                $('#searchform .search_results').show();
	            });
	            $(this).parents('#searchform .searchbox').animate({ marginRight: 70 }).next().fadeIn();
	        });

	        $('#searchform .searchcontainer').find('input[type=button]').click(function () {
	            $('#searchform .search_results').hide();
	            $('#searchform .searchbox').find('input[type=text]').val('');
	            $('#searchform .search_results').html('');
	            $('#wrapper > section > aside > nav > ul.drilldownMenu').fadeIn();
	            $('.searchbox', $(this).parent()).animate({ marginRight: 0 }).next().fadeOut();
	        });
	    });
    </script>
	
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