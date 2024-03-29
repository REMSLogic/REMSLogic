﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="dashboard.ascx.cs" Inherits="Site.App.Views.dashboard" %>
<% if (Framework.Security.Manager.HasRole("view_provider", true) || Framework.Security.Manager.HasRole("view_admin", true)) { %>
<!-- jQplot SETUP -->
<!--[if lt IE 9]>
<script type="text/javascript" src="/js/lib/jqplot/excanvas.js"></script>
<![endif]-->
<script type="text/javascript" src="/js/lib/jqplot/jquery.jqplot.js"></script>
<script type="text/javascript" src="/js/lib/jqplot/plugins/jqplot.dateAxisRenderer.js"></script>
<script type="text/javascript" src="/js/lib/jqplot/plugins/jqplot.cursor.min.js"></script>
<script type="text/javascript" src="/js/lib/jqplot/plugins/jqplot.highlighter.min.js"></script>
<script type="text/javascript" src="/js/lib/jqplot/plugins/jqplot.barRenderer.min.js"></script>
<script type="text/javascript" src="/js/lib/jqplot/plugins/jqplot.categoryAxisRenderer.min.js"></script>
<script type="text/javascript" src="/js/lib/jqplot/plugins/jqplot.pointLabels.min.js"></script>
<script type="text/javascript" src="/js/views/prescriber/drugs/select.js"></script>
<!-- jQplot CSS -->
<link rel="stylesheet" media="screen" href="/js/lib/jqplot/jquery.jqplot.min.css" />
<!-- jQplot CSS END -->
<% } else if (Framework.Security.Manager.HasRole("view_prescriber", true)) { %>
<link rel="stylesheet" media="screen" href="/css/jquery.radialProgressBar.css" />
<script type="text/javascript" src="/js/lib/jquery.radialProgressBar.js"></script>
<script type="text/javascript" src="/js/views/common/drugs/select.js"></script>
<% } %>
<script type="text/javascript">
    $(window).bind('page-animation-completed', function () {
        $('.eoc-filter img').click(function () {
            $(this).toggleClass('off');
            UpdateMyDrugListFilter();
        }).each(function () {
            $(this).addClass('off');
        });

        $('#drugs-filter').keyup(function () {
            UpdateMyDrugListFilter();
        });
    });

    function UpdateMyDrugListFilter() {
        var v = $('#drugs-filter').val().toLowerCase();

        $('#drug-list-widget li').show().each(function () {
            var $this = $(this);

            var drug = $('span.name', $this);

            if (drug.html().toLowerCase().indexOf(v) == -1)
                $this.hide();
        });

        $('.eoc-filter img:not(.off)').each(function () {
            $('#drug-list-widget li:not([data-' + $(this).attr('data-eoc') + '])').hide();
        });
    }

    function ClearDrugListFilter() {
        $('.eoc-filter img').each(function () { $(this).addClass('off'); });
        $('#drugs-filter').val('');
        UpdateMyDrugListFilter();
    }
</script>
<!--- ========  Aaron need help on this script...====== ---->
<script type="text/javascript">
    $(document).ready(function () {
        //        $("div.red-alert").parent().css("background", "yellow");
        $("span.compliance-red").parent().css("border-left", "3px solid red");
        $("span.compliance-yellow").parent().css("border-left", "3px solid #FAA61A");
        $("span.compliance-green").parent().css("border-left", "3px solid green");
        // this is fixing the hover event on ipad
        $('.hover').bind('touchstart touchend', function (e) {
            e.preventDefault();
            $(this).toggleClass('hover_effect');
        });
    })
</script>

<h1 class="page-title">
    Dashboard</h1>
<!--
All of the "widgets" have been moved to individual user controls in the
App/Controls/Widgets folder.  The controls are loaded during the Page_Init
event of this view (see dashboard.ascx.cs).
-->
<!--
Simply floating the widgets results in odd white gaps.  To solve this I am
using two columns.  When the browser shrinks to a small enough window the two
panels should stack ontop of each other instead of being side by side.
-->
<div class="widget-container-wrapper">
    <asp:Panel ID="pnlColumn1" runat="server" CssClass="container_12 clearfix widget-container left" data-id="1" />
</div>
<div class="widget-container-wrapper">
    <asp:Panel ID="pnlColumn2" runat="server" CssClass="container_12 clearfix widget-container right" data-id="2" />
</div>
<script type="text/javascript">
    $(window).bind('content-loaded', function () {
        $(".widget-container").sortable({
            connectWith: ".widget-container",
            handle: ".portlet-header",
            placeholder: "portlet-placeholder",
            update: function (event, ui) {
                var container = $(event.target);
                var containerId = container.data().id;

                $.ajax({
                    url: '/api/Common/Widgets/UpdateLayout',
                    type: 'GET',
                    data: $(event.target).sortable('serialize')+'&containerId='+containerId
                });
            }
        });

        $(".eoc-menu-toggle").click(function () {
            $content = $(".eoc-filter");
            //open up the content needed - toggle the slide- if visible, slide up, if not slidedown.
            $content.slideToggle(500, function () {
            if ($(this).css('display') == 'block') {
                    $(this).css('display', 'inline-block');
            }
                if ($(this).css('display') == 'block') {
                    $(this).css('display', 'inline-block');
                }
            });
        });
    });
</script>