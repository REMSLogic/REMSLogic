<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="prefs.ascx.cs" Inherits="Site.App.Views.account.prefs" %>

<h1 class="page-title">Preferences</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
        <a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        <form class="form has-validation ajax-form" action="/api/Common/Account/Prefs?id=<%=UserPreferences.ID%>">
            
            <div class="clearfix">
                <label for="form-email-notifications" class="form-label">Email Notifications</label>
                <div class="form-input"><div class="checkgroup" ><input type="checkbox" id="form-email-notifications" name="email-notifications" value="True" <%=UserPreferences.EmailNotifications? "checked=\"checked\"" : String.Empty%> /></div></div>
            </div>
            
            <!--
            <div class="clearfix">
                <label for="form-email-frequency" class="form-label">Email Frequency</label>
                <div class="form-input">
                    <select id="form-email-frequency" name="email-frequency">
                        <option value="0">Instant</option>
                        <option value="1">Daily</option>
                        <option value="7">Weekly</option>
                        <option value="30">Monthly</option>
                    </select>
                </div>
            </div>
            -->

            <div class="form-action clearfix">
                <button class="button" type="submit">Save</button>
                <button class="button" type="reset">Reset</button>
            </div>
        </form>
    </div>
</div>