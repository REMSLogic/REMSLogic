<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Site.App.Default" %>

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

	<script type="text/javascript" src="/js/lib/jquery.radialProgressBar.js"></script>
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
                            <div class="searchbox">
                                <input type="text" name="q" id="q" autocomplete="off" placeholder="Search...">
                            </div>
                            <input type="button" value="Cancel" />
                        </div>
                        <div class="search_results"></div>
                    </div>
                    <ul class="tlm">
						<li class="current"><a href="#dashboard" title="Dashboard"><i class="fa fa-tachometer fa-fw app-icon-nav"></i><span>Dashboard</span></a></li>
						<% if (Framework.Security.Manager.HasRole("dev"))
                        { %>
						<li class="hasul"><a href="#"><img src="/images/navicons/13.png" alt="" /><span>Dev Menu</span></a>
                            <ul>
								<li><a href="#admin/security/groups/list" title="Groups"><img src="/images/navicons/112.png" alt=""/><span>Groups</span></a></li>
								<li><a href="#dev/roles/list" title="Roles"><img src="/images/navicons/112.png" alt=""/><span>Roles</span></a></li>
								<li><a href="#dev/user-types/list" title="User Types"><img src="/images/navicons/111.png" alt=""/><span>User Types</span></a></li>
                                <li><a href="#dev/specialities/list" title="Specialities"><img src="/images/navicons/111.png" alt=""/><span>Specialities</span></a></li>
								<li><a href="#dev/langs/list" title="Languages"><img src="/images/navicons/59.png" alt=""/><span>Languages</span></a></li>
								<li><a href="#dev/eocs/list" title="EOCs"><img src="/images/navicons/136.png" alt=""/><span>EOCs</span></a></li>
								<li><a href="#dev/reports/list" title="Reports"><img src="/images/navicons/122.png" alt=""/><span>Reports</span></a></li>
								<li class="hasul"><a href="#"><img src="/images/navicons/140.png" alt=""/><span>DSQ</span></a>
									<ul>
										<li><a href="#dev/dsq/sections/list" title="Sections"><img src="/images/navicons/27.png" alt=""/><span>Sections</span></a></li>
										<li><a href="#dev/dsq/questions/list" title="Questions"><img src="/images/navicons/28.png" alt=""/><span>Questions</span></a></li>
									</ul>
								</li>
								<li class="hasul"><a href="#"><img src="/images/navicons/13.png" alt=""/><span>Test</span></a>
									<ul>
										<li><a href="#dev/test/monk" title="Monk's"><img src="/images/navicons/13.png" alt=""/><span>Monk's</span></a></li>
									</ul>
								</li>
							</ul>
						</li>
						<% } %>
						<% if (Framework.Security.Manager.HasRole("view_admin"))
                        { %>
						<li class="hasul"><a href="#"><img src="/images/navicons/25.png" alt="" /><span>User Management</span></a>
                            <ul>
								<li><a href="#admin/security/users/list" title="Users"><img src="/images/navicons/111.png" alt=""/><span>Users</span></a></li>
								<li><a href="#admin/security/providers/list" title="Providers"><img src="/images/navicons/77.png" alt=""/><span>Health Care Org.</span></a></li>
								<li><a href="#admin/drugs/companies/list" title="Drug Companies"><img src="/images/navicons/159.png" alt=""/><span>Drug Companies</span></a></li>
								<li><a href="#admin/security/us-fda/list" title="US FDA Officials"><img src="/images/navicons/167.png" alt=""/><span>US FDA Officials</span></a></li>
							</ul>
						</li>
						<li class="hasul"><a href="#"><img src="/images/navicons/15.png" alt=""/><span>Drug Management</span></a>
                            <ul>
								<li><a href="#admin/drugs/drugs/list" title="Drugs"><img src="/images/navicons/15.png" alt=""/><span>Drugs</span></a></li>
								<li><a href="#admin/drugs/drugs/list?pending=true" title="Pending Drugs"><img src="/images/navicons/15.png" alt=""/><span>Pending Drug Changes</span></a></li>
								<li><a href="#admin/drugs/companies/list" title="Drug Companies"><img src="/images/navicons/159.png" alt=""/><span>Drug Companies</span></a></li>
								<li><a href="#admin/drugs/systems/list" title="Drug Systems"><img src="/images/navicons/139.png" alt=""/><span>Drug Systems</span></a></li>
								<li><a href="#admin/drugs/formulations/list" title="Formulations"><img src="/images/navicons/140.png" alt=""/><span>Formulations</span></a></li>
							</ul>
						</li>
                        <li class=""><a href="#admin/tasks/list" title="Task Management"><img src="/images/navicons/81.png" alt=""/><span>Task Management</span></a></li>
						<li class="hasul"><a href="#"><img src="/images/navicons/139.png" alt=""/><span>Examples</span></a>
                            <ul>
								<li><a href="#examples/form1" title="Form Sample"><img src="/images/navicons/139.png" alt=""/><span>Form Sample</span></a></li>
								<li><a href="#examples/form2" title="Custom Form Elements"><img src="/images/navicons/139.png" alt=""/><span>Custom Form Elements</span></a></li>
								<li><a href="#examples/calendar" title="Calendar"><img src="/images/navicons/83.png" alt=""/><span>Calendar</span></a></li>
								<!--<li><a href="#examples/wysiwyg" title="WYSIWYG Editor"><img src="/images/navicons/26.png" alt=""/><span>WYSIWYG Editor</span></a></li>-->
								<li><a href="#examples/notifications" title="Notifications"><img src="/images/navicons/139.png" alt=""/><span>Notifications</span></a></li>
								<li><a href="#examples/tables" title="Tables"><img src="/images/navicons/29.png" alt=""/><span>Tables</span></a></li>
								<li><a href="#examples/wizard" title="Wizard"><img src="/images/navicons/165.png" alt=""/><span>Wizard</span></a></li>
							</ul>
						</li>
						<li><a href="/" title="Home Page"><img src="/images/navicons/12.png" alt="" /><span>Home Page</span></a></li>
						<% } %>
						<% if (Framework.Security.Manager.HasRole("dashboard_drugcompany_view", true))
                        { %>
						<li><a href="#admin/drugs/drugs/list?my=true" title="My Drugs"><%--<img src="/images/navicons/15.png" alt="" />--%><i class="fa fa-medkit fa-fw app-icon-nav"></i><span>Manage Drugs</span></a></li>
						<li><a href="#admin/drugs/drugs/list" title="All Drugs"><img src="/images/navicons/15.png" alt="" /><span>All Drugs</span></a></li>
						<% } %>
						<% if (Framework.Security.Manager.HasRole("view_provider", true))
                        { %>
						<li><a href="#provider/prescribers/list" title="Prescribers"><%--<img src="/images/navicons/63.png" alt="" />--%><i class="fa fa-users fa-fw app-icon-nav"></i><span>Prescribers</span></a></li>
						<li><a href="#hcos/facilities/list" title="Provider Facilities"><%--<img src="/images/navicons/159.png" alt="" />--%><i class="fa fa-building-o fa-fw app-icon-nav"></i><span>Facilities</span></a></li>
						<li><a href="#common/drugs/list" title="Drug List"><%--<img src="/images/navicons/15.png" alt="" />--%><i class="fa fa-medkit fa-fw app-icon-nav"></i><span>Manage Drugs</span></a></li>
						<% } %>
						<% if (Framework.Security.Manager.HasRole("view_prescriber", true))
                        { %>
						<li><a href="#prescriber/drugs/list" title="Drug List"><i class="fa fa-medkit fa-fw  app-icon-nav"></i><span>Manage Drugs</span></a></li>
						<% } %>
						<% if (Lib.Systems.Reports.GetMyReports().Count > 0)
                        { %>
						<li><a href="#reports/list" title="Reports"><%--<img src="/images/navicons/122.png" alt="" />--%><i class="fa fa-list-alt fa-fw app-icon-nav"></i><span>Reports</span></a></li>
						<% } %>
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
				    <ul class="toolbar clearfix fl" style="position: absolute; top: 8px; left: 8px;">
                        <li>
                            <a href="#" title="Notifications" class="icon-only" id="notifications-button">
                                <img src="images/navicons-small/08.png" alt=""/>
							    <% if( this.NumUnread > 0 ) { %>
                                <span class="message-count"><%=this.NumUnread %></span>
							    <% } %>
                            </a>
                        </li>
                        <% if( Framework.Security.Manager.HasRole("view_provider") || Framework.Security.Manager.HasRole("view_admin") ) { %>
                        <li>
                            <a href="#common/notifications/create" title="New notification" class="icon-only" id="new-notification-button">
                                <img src="images/navicons-small/10.png" alt=""/>
                            </a>
                        </li>
                        <% } %>
                    </ul>
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
            <div style="float: right; padding-right: 8px;"><a class="button" href="#common/notifications/archive">View Archive</a></div>
        </header>
        <section>
            <div class="content">
                <nav>
                    <ul>
						<% if (this.notifications.Count > 0)
         { %>
						<% foreach (var n in this.notifications)
         { %>
						<li class="<%=(n.read) ? "read" : "new" %> notification-instance" data-id="<%=n.notification.ID %>">
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
                        <li>
                            <div style="float: right;"><input type="button" value="Archive All" onclick="archiveAll();" /></div>
                        </li>
                    </ul>
                </nav>
            </div>
        </section>
        <footer class="clearfix">
            
        </footer>
    </div>
    <!-- POPOVERS SETUP END-->

	<script type="text/javascript">
	    function archiveAll() {
	        $.ajax({
	            url: '/api/Notifications/ArchiveAll',
	            data: 'id=' + encodeURIComponent(<%=UserId%>),
	            cache: false,
	            success: function (response) {
	                $('.notification-instance').remove();
	            }
	        });
	    }

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
	            $('#wrapper > section > aside > nav > ul').fadeIn();
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