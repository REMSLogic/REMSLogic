<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="non-etasu-selections.ascx.cs" Inherits="Site.App.Views.prescriber.wizards.non_etasu_selections" %>
<%@ Import Namespace="Lib.Data" %>

<h1 class="page-title">Non-ETASU Drug Selections</h1>
<div class="container_12 clearfix leading">  
    <div class="grid_12">
        <p>
            Below is a complete list of all non-ETASU drugs currently in the REMS Logic system.  Select only the drugs you currently prescribe.  The drugs you select will be easily accessible from the dashboard once you log into the site.
        </p>
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