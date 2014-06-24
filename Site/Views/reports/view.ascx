<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="view.ascx.cs" Inherits="Site.App.Views.reports.view" %>

<link rel="stylesheet" media="screen" href="/js/lib/jqplot/jquery.jqplot.min.css" />
<!--[if lt IE 9]>
<script type="text/javascript" src="/js/lib/jqplot/excanvas.js"></script>
<![endif]-->
<script type="text/javascript" src="/js/lib/jqplot/jquery.jqplot.js"></script>
<script type="text/javascript" src="/js/lib/jqplot/plugins/jqplot.dateAxisRenderer.js"></script>
<script type="text/javascript" src="/js/lib/jqplot/plugins/jqplot.cursor.min.js"></script>
<script type="text/javascript" src="/js/lib/jqplot/plugins/jqplot.highlighter.min.js"></script>
<script type="text/javascript" src="/js/lib/jqplot/plugins/jqplot.pieRenderer.min.js"></script>

<link rel="stylesheet" media="screen" href="/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/js/lib/datatables/js/jquery.dataTables.js"></script>

<script type="text/javascript">
    (function ($) {
        var transitioning = false;

        var build_funcs = {
            BuildPieChart: function ($o, data) {
                var id = $o.attr('data-id');
                var $contain = $('.output-container', $o);
                $contain.empty();
                $contain.append($('<div id="pie-chart-' + id + '" style="width: 370px; height: 300px;"></div>'));

                var d = [];

                for (i = 0; i < data.Data.length; i++) {
                    var vals = [];
                    for (key in data.Data[i]) {
                        vals.push(data.Data[i][key]);
                    }
                    d.push(vals);
                }

                var pc = jQuery.jqplot('pie-chart-' + id, [d], {
                    seriesDefaults: {
                        renderer: jQuery.jqplot.PieRenderer,
                        rendererOptions: { showDataLabels: true }
                    },
                    legend: {
                        show: true,
                        location: 'e'
                    }
                });

                $('.loading[data-id="' + id.toString() + '"]').remove();

                WaitForTransition(function () {
                    pc.redraw();
                });
            },
            BuildTable: function ($o, data) {
                var id = $o.attr('data-id');
                var $contain = $('.output-container', $o);
                $contain.empty();
                $contain.append($('<table id="data-table-' + id + '" class="display"></table>'));

                var d = [];
                var h = [];

                if (data.Data && data.Data.length) {
                    for (i = 0; i < data.Data.length; i++) {
                        var row = [];

                        for (key in data.Data[i]) {
                            if (i == 0)
                                h.push({ "sTitle": key });

                            row.push(data.Data[i][key]);
                        }

                        d.push(row);
                    }
                }
                else {
                    h.push('');
                    d.push(['There are no results that match your filters.']);
                }

                $('.loading[data-id="' + id.toString() + '"]').remove();

                $('#data-table-' + id).dataTable({
                    "sPaginationType": "full_numbers",
                    "sScrollX": "100%",
                    "aaData": d,
                    "aoColumns": h
                });
            }
        };

        function WaitForTransition(cb) {
            if (!transitioning)
                cb();
            else
                setTimeout(function () { WaitForTransition(cb) }, 5);
        }

        $(window).bind('content-loaded', function () {
            $('#report-output').hide();

            <%if(!item.GetFilters().Any()){%>
            $('#report-input').hide(); 
            <%}%>

            $('.reports-filter-form').submit(function () {
                $('.output .loading').show();

                transitioning = true;

                var data = $(this).serialize();
                var url = $(this).attr('action');

                $('.output').each(function () {
                    var $this = $(this);
                    var id = $this.attr('data-id');

                    $('#report-output').show();

                    $.ajax({
                        url: url + '&output-id=' + id.toString(),
                        type: 'POST',
                        data: data,
                        success: function (data) {
                            if (!data || data.Error == true || !data.Result)
                                return; // Error messaged handled in global ajax handler in global.js

                            data = data.Result;

                            var $o = $('.output[data-id="' + id.toString() + '"]');

                            var fname = 'Build' + data.OutputType;

                            if (build_funcs[fname])
                                build_funcs[fname]($o, data);
                        }
                    });
                });

                return false;
            });
        });

        $(window).bind('page-animation-completed', function () {
            <%if(!item.GetFilters().Any()){%>
            $('.reports-filter-form').submit();
            <%}%>
        });
    })(jQuery);
</script>
<h1 class="page-title"><%=item.Name%></h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
        <a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>

        <h2><%=item.Name%></h2>
        <section id="report-input">
            <form class="form has-validation reports-filter-form" action="/api/App/Reports/Get?id=<%=item.ID%>">
                <% foreach (var filter in item.GetFilters())
                    { %>

                <div class="clearfix">
                    <label for="form-filter-<%=filter.ID %>" class="form-label"><%=filter.DisplayName%></label>
                    <div class="form-input">
                        <%	switch (filter.Type)
                            {
                                case Lib.Data.ReportFilter.FilterTypes.String: %>
                        <input type="text" id="form-filter-<%=filter.ID %>" name="filter-<%=filter.ID %>" placeholder="<%=filter.PlaceholderText %>" value="" />
                        <%	break;
                        case Lib.Data.ReportFilter.FilterTypes.DateTime: %>
                        <input type="date" id="form-filter-<%=filter.ID %>" name="filter-<%=filter.ID %>" placeholder="<%=filter.PlaceholderText %>" value="" />
                        <%	break;
                        case Lib.Data.ReportFilter.FilterTypes.Integer: %>
                        <input type="number" id="form-filter-<%=filter.ID %>" name="filter-<%=filter.ID %>" placeholder="<%=filter.PlaceholderText %>" value="" />
                        <%	break;
                        } %>
                    </div>
                </div>
                <% } %>

                <div class="form-action clearfix">
                    <button class="button" type="submit">Run</button>
                    <button class="button" type="reset">Reset</button>
                </div>

            </form>
        </section>

        <section class="clearfix" id="report-output">
            <% int count = 0; 
                foreach (var o in outputs){
                    count++; 
                       
                    if (o.Type == Lib.Data.ReportOutput.OutputTypes.Table) count += 2; %>
            <h3><%=o.Name %><img class="loading fr" src="/images/ajax-loader-eeeeee.gif" alt="Loading..." data-id="<%=o.ID.Value%>"  /></h3> 
            <section class=" <%=((o.Type == Lib.Data.ReportOutput.OutputTypes.Table) ? " grid_12" : " grid_6") %> output" style="margin: 0;" data-id="<%=o.ID.Value %>">

                <section class="output-container">
                </section>
            </section>
            <% } %>
        </section>
    </div>
</div>
