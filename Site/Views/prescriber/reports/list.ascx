<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="list.ascx.cs" Inherits="Site.App.Views.prescriber.reports.list" %>

<h1 class="page-title">Reports</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12 manage-drug-list">
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        <section class="portlet grid_6 leading">
			<header>
				<h2>Custom Report</h2> 
			</header>
			<section>
				Select any fields you want and enter whatever filters you want.<br />
				<br />
				<a class="button" href="#">Run</a>
			</section>
		</section>

		<section class="portlet grid_6 leading">
			<header>
				<h2>All Uncertified Drugs</h2> 
			</header>
			<section>
				Shows all missing certifications.<br />
				<br />
				<a class="button" href="#">Run</a>
			</section>
		</section>

		<section class="portlet grid_6 leading">
			<header>
				<h2>All Certified Drugs</h2> 
			</header>
			<section>
				Shows all certified drugs.<br />
				<br />
				<a class="button" href="#">Run</a>
			</section>
		</section>
    </div>
</div>