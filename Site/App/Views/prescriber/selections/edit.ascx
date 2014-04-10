<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Site.App.Views.prescriber.edit" %>
<%@ Import Namespace="Lib.Data" %>

<h1 class="page-title">Drug Selections</h1>
<div class="container_12 clearfix leading">  
    <div class="grid_12 manage-drug-list">
        <p>
            The following drugs have been added to the system or updated since your last login.  Please updated your selections below.  All selected drugs will automatically be added to your drug list.
        </p>
        <form class="form has-validation ajax-form" action="/api/Prescriber/Selections/Edit">

            <%foreach(Drug drug in Drugs){ %>
            <div class="clearfix">
                <label for="form-drug-selections-<%=drug.ID%>" class="form-label" style="width: 67%;"><%=drug.GenericName%></label>
                <div class="form-input" style="width: 33%">
                    <div class="radiogroup" style="text-align: right;">
                        <label style="cursor: pointer;">
                            <input type="radio" name="drug-selection[<%=drug.ID%>]" id="form-drug-selections-<%=drug.ID%>" required="required" value="Yes" /> Yes
                        </label> 
                        <label style="cursor: pointer;">
                            <input type="radio" name="drug-selection[<%=drug.ID%>]" value="No" /> No
                        </label>
                    </div>
                </div>
            </div>
            <%}%>

            <div class="form-action clearfix" style="margin-left: 74%;">
                <button class="button" type="submit">OK</button>
                <button class="button" type="reset">Reset</button>
            </div>
        </form>
    </div>
</div>

<script type="text/javascript">
    $(window).bind('content-loaded', function () {
        setTimeout(function () {
            $('.button-group').buttonset();
        }, 0);
    });
</script>