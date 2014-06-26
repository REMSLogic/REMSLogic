<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ComplianceGraph.ascx.cs" Inherits="Site.App.Controls.Widgets.ComplianceGraph" %>
<style type="text/css">
    #chart .jqplot-series-0 {
      color: #FFFFFF;
    }
  </style>

<script type="text/javascript">
    $(window).bind('content-loaded', function () {
        $.ajax({
            url: '/api/App/Stats/Compliance',
            type: 'GET',
            dataType: 'json',
            data: {
                facilityId: <%=FacilityId%>
            }
        }).done(function (response) {
            displayComplianceGraph(response.compliant, response.nonCompliant);
        });
    });

    function displayComplianceGraph(compliant, nonCompliant) {
        var plot = $.jqplot('chart', [compliant, nonCompliant], {
            title: {
                text: '<div style="font-size: 14pt; padding: 8px;">Jan 1, 2012 - Dec 31, 2012</div>',
                show: true,
                escapeHtml: false
            },
            // Tell the plot to stack the bars.
            grid: { shadow: false, borderWidth: 0 },
            animate: !$.jqplot.use_excanvas,
            stackSeries: true,
            captureRightClick: true,
            seriesDefaults: {
                renderer: $.jqplot.BarRenderer,
                rendererOptions: {
                    // Put a 30 pixel margin between bars.
                    barMargin: 64,
                    // Highlight bars when mouse button pressed.
                    // Disables default highlighting on mouse over.
                    highlightMouseDown: true
                },
                pointLabels: { show: true, stackedValue: true },
                shadow: false
            },
            series: [
                { label: 'Compliance', color: '#70B8FF' },
                { label: 'Non-compliance', color: '#E06666' }
            ],
            axesDefaults: {
                tickOptions: {
                    showMark: false
                }
            },
            axes: {
                xaxis: {
                    renderer: $.jqplot.CategoryAxisRenderer,
                    ticks: [
                        '<div style="text-align: center">Prescriber<br />Enrollment</div>', 
                        '<div style="text-align: center">Patient<br />Enrollment</div>', 
                        '<div style="text-align: center">Education<br />Training</div>'
                    ],
                    tickOptions: {
                        showGridline: false,
                        fontSize: '12pt',
                        labelPosition: 'middle'
                    }
                },
                yaxis: {
                    // Don't pad out the bottom of the data range.  By default,
                    // axes scaled as if data extended 10% above and below the
                    // actual range to prevent data points right on grid boundaries.
                    // Don't want to do that here.
                    padMin: 0,
                    min: 0,
                    max: 100,
                    tickInterval: 10,
                    tickOptions: {
                        formatString: '%d%%',
                        fontSize: '10pt'
                    }
                }
            },
            legend: {
                show: true,
                location: 'ne',
                placement: 'inside',
                fontSize: '10pt'
            }
        });
        // Bind a listener to the "jqplotDataClick" event.  Here, simply change
        // the text of the info3 element to show what series and ponit were
        // clicked along with the data for that point.
        $('#chart').bind('jqplotDataClick',
            function (ev, seriesIndex, pointIndex, data) {
                $('#info3').html('series: ' + seriesIndex + ', point: ' + pointIndex + ', data: ' + data);
            }
        );

        $(window).off('resize.complianceGraph');

        $(window).on('resize.complianceGraph', function(){
            plot.replot({resetAxis: true});
        });
    }

    function downloadImage(content, filename) {
        var $link = $('<a/>');
        var e;

        $link.attr('download', filename);
        $link.attr('href', content);

        if (document.createEvent) {
            e = document.createEvent("MouseEvents");
            e.initMouseEvent("click", true, true, window,
                0, 0, 0, 0, 0, false, false, false,
                false, 0, null);

            $link[0].dispatchEvent(e);
        }

        else if ($link[0].fireEvent) {
            $link[0].fireEvent("onclick");
        }
    }
</script>

<header class="portlet-header">
    <h2>HCO Compliance (All)</h2>
</header>
<section>
  <div id="chart" style="display: inline-block; width: 100%; height: 300px;"></div>
</section>

<!--
<div onclick="downloadImage($('#chart').jqplotToImageStr(), 'test.png');">
  Save image as file
</div>
-->