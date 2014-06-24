<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="list.ascx.cs" Inherits="Site.App.Views.provider.reports.list" %>

<h1 class="page-title">Reports</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12 facility-add-wrap">
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        <section class="portlet grid_6 leading">
			<header>
				<h2>Prescriber Summary Report</h2> 
			</header>
			<section>
				Give you a summary of all prescibers and their status.<br />
				<br />
				<br />
				<a class="button" href="#provider/reports/prescriber-summary">Run</a>
			</section>
		</section>

		<section class="portlet grid_6 leading">
			<header>
				<h2>Drug System Report</h2> 
			</header>
			<section>
				Give you a summary of all your prescibers enrolled in a given system and if they have been certified.<br />
				<br />
				<a class="button" href="#provider/reports/system-summary">Run</a>
			</section>
		</section>

		<section class="portlet grid_6 leading">
			<header>
				<h2>Drug Report</h2> 
			</header>
			<section>
				Give you a list of all prescibers enrolled in a drug and if they have been certified.<br />
				<br />
				<a class="button" href="#provider/reports/drug-summary">Run</a>
			</section>
		</section>
    </div>
</div>