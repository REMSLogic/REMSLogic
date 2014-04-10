<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="etasu-selections.ascx.cs" Inherits="Site.App.Views.prescriber.wizards.etasu_selections" %>
<%@ Import Namespace="Lib.Data" %>

<h1 class="page-title">ETASU Drug Selections</h1>
<div class="container_12 clearfix leading">  
    <div class="grid_12">
        <p>
            Below is a complete list of all ETASU drugs currently in the REMS Logic system.  You <strong>must</strong> select "Yes" or "No" for each drug
            in the list.  The drugs you select will be easily accessible from the dashboard once you log into the site.
        </p>
        <form class="form has-validation ajax-form" action="/api/Prescriber/Wizards/ETASUSelections">
            <%foreach(Drug drug in Drugs){ %>
            <div class="clearfix">
                <label for="form-drug-selections-<%=drug.ID%>" class="form-label" style="width: 75%;"><%=drug.GenericName%></label>
                <div class="form-input" style="width: 25%"><div class="radiogroup"><label><input type="radio" name="drug-selection[<%=drug.ID%>]" id="form-drug-selections-<%=drug.ID%>" required="required" value="Yes" /> Yes</label> <label><input type="radio" name="drug-selection[<%=drug.ID%>]" value="No" /> No</label></div></div>
            </div>
            <%}%>

            <div class="form-action clearfix" style="margin-left: 74%;">
                <button class="button" type="submit">OK</button>
                <button class="button" type="reset">Reset</button>
            </div>
        </form>
    </div>
</div>