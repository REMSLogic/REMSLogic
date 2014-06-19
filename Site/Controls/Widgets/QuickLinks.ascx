<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuickLinks.ascx.cs" Inherits="Site.App.Controls.Widgets.QuickLinks" %>

<script type="text/javascript">
    $(window).bind('content-loaded', function () {
        $(".forms-icon").click(function () {
            $doc = $(this);
            var id = parseInt($doc.attr('id').toString().substring(3));
            $listItem = $(eval('link' + id.toString()));

            if (id) {
                $.ajax({
                    url: '/api/List/FormsAndDocuments/RemoveItem',
                    data: 'id=' + encodeURIComponent(id),
                    cache: false,
                    success: function (response) {
                        $listItem.remove();
                    }
                });
            }
        });
    });
</script>

<header class="portlet-header">
    <h2><i class="fa fa-plus pad-right"></i>Quick Links</h2>
</header>
<section>
    <ul class="drug-list-dc">
    <% foreach (var link in Lib.Systems.Lists.GetMyFormsAndDocumentsList().GetItems<Lib.Data.DSQ.Link>()){ %>
        <li class="forms-list" id="link<%=link.ID %>">
            <%--<a href="<%=link.Value %>" target="_blank"><%=link.Drug.GenericName + " - " + link.Label %></a>--%>
            <%--<img class="forms-remove" src="/images/navicons-small/134.png" alt="Remove" id="img<%=link.ID %>" />--%>
            <span class="forms-icon" id="img<%=link.ID %>">
                <i class="fa fa-times-circle"></i>
            </span>
            <span class="forms-text">
                <a href="<%=link.Value %>" target="_blank"><span class="forms-drugName"><%=link.Drug.GenericName %></span></a><br />
                <span class="forms-desc"><%=link.Label %></span>
            </span>
        </li>
    <% } %>
    </ul>
</section>