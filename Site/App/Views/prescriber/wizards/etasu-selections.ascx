<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="etasu-selections.ascx.cs" Inherits="Site.App.Views.prescriber.wizards.etasu_selections" %>
<%@ Import Namespace="Lib.Data" %>

<h1 class="page-title">ETASU Drug Selections</h1>
<div class="container_12 clearfix leading">  
    <div class="grid_12">
        <p style="font-size: 18px; font-weight: bold;">
            Below is the list of REMS drugs that include prescriber action to fulfill U.S. FDA elements to assure safe use (ETASU) compliance components.  
        </p>
        <p>
            Instructions (this is a one-time only activity):  
        </p>
        <ul style="margin-bottom: 32px;">
            <li>Review each drug to create your personalized REMS drug list.</li>
            <li>Select ‘Yes’ if you have prescribed the medication in the past 6 months or anticipate prescribing the medication within the next calendar year.</li>
            <li>Select ‘No’ if you do not prescribe the medication.</li>
            <li>Based upon your selection, REMS Logic will provide you with the necessary information needed to meet FDA compliance.</li>
            <li>You will be able to modify your drug preferences at any time after profile creation using the ‘Manage Drugs’ page.</li>
        </ul>
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