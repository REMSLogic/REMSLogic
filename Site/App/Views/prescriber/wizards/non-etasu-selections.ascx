<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="non-etasu-selections.ascx.cs" Inherits="Site.App.Views.prescriber.wizards.non_etasu_selections" %>
<%@ Import Namespace="Lib.Data" %>

<h1 class="page-title">Non-ETASU Drug Selections</h1>
<div class="container_12 clearfix leading">  
    <div class="grid_12">
        <p style="font-size: 18px; font-weight: bold;">
            Below is the list of REMS drugs that are categorized as Non-ETASU.  These medications do not require certification.  
        </p>
        <ul style="margin-bottom: 32px;">
            <li>Selection of drugs from this page is elective.</li>
            <li>Any drugs you select from this page will be added to your personalized drug list to allow quick access to any pertaining materials.</li>
            <li>Selection of Non-ETASU medications will not contribute to, or negatively impact compliance with the ETASU drugs.</li>
            <li>You will be able to modify your drug preferences at any time after profile creation using the ‘Manage Drugs’ page.</li>
        </ul>

        <form class="form has-validation ajax-form" action="/api/Prescriber/Wizards/NonETASUSelections">
        
            <div class="clearfix">
                <label for="form-drug-selections" class="form-label" style="width: 75%;">Drug Name</label>
                <div class="form-input" style="width: 25%;"><span class="form-info">I Prescribe</span></div>
            </div>

            <%foreach(Drug drug in Drugs){ %>
            <div class="clearfix">
                <label for="form-drug-selections" class="form-label" style="width: 75%;"><%=drug.GenericName %></label>
                <div class="form-input" style="width: 25%;"><div class="checkgroup"><input type="checkbox" name="drug-selections[]" id="form-drug-selections" value="<%=drug.ID%>" /> Yes</div></div>
            </div>
            <%}%>

            <div class="form-action clearfix" style="margin-left: 74%;">
                <button class="button" type="submit">OK</button>
                <button class="button" type="reset">Reset</button>
            </div>
        </form>
    </div>
</div>