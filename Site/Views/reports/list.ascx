<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="list.ascx.cs" Inherits="Site.App.Views.reports.list" %>

<h1 class="page-title">Reports</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
		<div class="actions grid_12">
			<a class="button back-button" href="#">Back</a>
		</div>
		<% foreach( var item in items ) { %>

		<section class="portlet grid_6 leading">
			<header>
				<h2><%=item.Name %></h2> 
			</header>
			<section>
				<%=item.Description %><br />
				<br />
				<a class="button" href="#reports/view?id=<%=item.ID.Value %>">Run</a>
			</section>
		</section>
		<% } %>

    </div>
</div>