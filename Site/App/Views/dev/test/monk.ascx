<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="monk.ascx.cs" Inherits="Site.App.Views.dev.test.monk" %>

<h1 class="page-title">Monk's Test Page</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
		<p>This is Monk's private testing/dev area. If you are not Monk, GO AWAY! (And do NOT touch anything here.)</p>
		<a href="/api/Dev/Test/Monk/ClearIndexes" class="button ajax-button" style="margin-top: 10px; margin-bottom: 10px;">Clear Indexes</a><br />
		<br />
		<br />
		<a href="/api/Dev/Test/Monk/IndexPrescribers" class="button ajax-button" style="margin-top: 10px; margin-bottom: 10px;">Index Prescribers</a><br />
		<a href="/api/Dev/Test/Monk/IndexProviders" class="button ajax-button" style="margin-top: 10px; margin-bottom: 10px;">Index Providers</a><br />
		<a href="/api/Dev/Test/Monk/IndexDrugCompanies" class="button ajax-button" style="margin-top: 10px; margin-bottom: 10px;">Index Drug Companies</a><br />
		<a href="/api/Dev/Test/Monk/IndexDrugSystems" class="button ajax-button" style="margin-top: 10px; margin-bottom: 10px;">Index Drug Systems</a><br />
		<a href="/api/Dev/Test/Monk/IndexDrugs" class="button ajax-button" style="margin-top: 10px; margin-bottom: 10px;">Index Drugs</a><br />
		<a href="/api/Dev/Test/Monk/IndexUserProfiles" class="button ajax-button" style="margin-top: 10px; margin-bottom: 10px;">Index User Profiles</a><br />
		<br />
		<br />
		<a href="/api/Dev/Test/Monk/RefreshDrugIcons" class="button ajax-button" style="margin-top: 10px; margin-bottom: 10px;">Refresh Drug Icons</a><br />
		<a href="/api/Dev/Test/Monk/UpdateDrugLists" class="button ajax-button" style="margin-top: 10px; margin-bottom: 10px;">Update Drug Lists</a><br />
	</div>
</div>