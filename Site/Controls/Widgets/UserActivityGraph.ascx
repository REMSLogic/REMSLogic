<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserActivityGraph.ascx.cs" Inherits="Site.App.Controls.Widgets.UserActivityGraph" %>
<%@ Import Namespace="Lib.Systems.Activity" %>

<script type="text/javascript">
    $(window).bind('content-loaded', function () {
        createGraph();
    });

    function createGraph() {
        $("#graph-container").empty().append($('<div class="jqPlot" id="jqplot-actions" style="width:100%;height:160px;"></div>'));

        $.ajax({
            url: "/api/Stats/SumActivityByName",
            type: "GET",
            dataType: "json",
            data: { name: "<%=ActivityService.StandardLogin%>", min_date: "<%=DateTime.Now.AddDays(-7).Date%>", max_date: "<%=DateTime.Now.Date%>" }
        }).done(function (response) {
            var data = {
                "data": [response],
                "lines": [{ "label": "Logins"}],
                "x_axis": {
                    "min": response[0][0],
                    "max": response[response.length - 1][0],
                    "tickOptions": {
                        "formatString": "%b&nbsp;%#d"
                    }
                },
                "y_axis": { "min": 0 }
            };

            var plot = $.jqplot("jqplot-actions", data.data, {
                grid: { shadow: false, borderWidth: 0 },
                show: true,
                legend: { show: true, location: "ne", xoffset: 0 },
                seriesDefaults: { shadow: false },
                series: data.lines,
                axes: {
                    xaxis: {
                        min: data.x_axis.min,
                        max: data.x_axis.max,
                        renderer: $.jqplot.DateAxisRenderer,
                        tickOptions: {
                            formatString: data.x_axis.tickOptions.formatString
                        }
                    },
                    yaxis: data.y_axis
                },
                highlighter: {
                    show: true,
                    sizeAdjust: 7.5
                },
                cursor: {
                    show: false
                }
            });

            $(window).off('resize.userActivityGraph');

            $(window).on('resize.userActivityGraph', function () {
                plot.replot({ resetAxis: true });
            });
        });
    }
</script>

<header class="portlet-header">
    <h2>User Activity</h2>
</header>
<section id="graph-container">
    <div class="jqPlot" id="Div1" style="width:100%;height:160px;"></div>
</section>