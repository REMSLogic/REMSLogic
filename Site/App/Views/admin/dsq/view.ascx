<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="view.ascx.cs" Inherits="Site.App.Views.admin.dsq.view" %>

<!-- DATATABLES CSS -->
<link rel="stylesheet" media="screen" href="/App/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/App/js/lib/datatables/js/jquery.dataTables.js"></script> 
<script type="text/javascript" src="/App/js/dsq-view.js"></script> 
<h1 class="page-title"><%=item.GenericName %></h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
		<div class="dsq-form" >
			<div class="clearfix">
				<site:DSQView ID="dsqView" GeneralInfoControlPath="~/App/Controls/DSQ/GeneralInfoView.ascx" runat="server" />
			</div>
		</div>
	</div>
</div>