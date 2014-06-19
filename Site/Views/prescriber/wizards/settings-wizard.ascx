<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="settings-wizard.ascx.cs" Inherits="Site.App.Views.prescriber.wizards.SettingsWizard" %>

<script type="text/javascript" src="/js/jquery.tools.min.js"></script>
<script type="text/javascript" src="/js/jquery.wizard.js"></script>

<h1 class="page-title">Prescriber Registration</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
        <form action="/api/Prescriber/Wizards/FinalStep" class="wizard form ajax-form" novalidate>
            <nav>
                <ul class="clearfix">
                    <li class="active"><strong>1.</strong>Notification Preferences</li>
                </ul>
            </nav>
            <div class="items">
                <!-- page1 -->
                <section>
                    <header>
                        <h2>
                            <strong>Step 1: </strong> Notification Preferences
                            <em>Let us know how often you would like to receive emails from REMS Logic.</em>
                        </h2>
                    </header>

                    <section>
                        <div class="grid_12">
                            <div class="form">
                                <div class="clearfix">
                                    <label for="form-email-notifications" class="form-label">Email Notifications</label>
                                    <div class="form-input"><div class="checkgroup" ><input type="checkbox" id="form-email-notifications" name="email-notifications" value="True" /></div></div>
                                </div>

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
                            </div>
                        </div>
                    </section>
                    
                    <footer class="clearfix">
                        <button type="submit" class="next fr">Submit &raquo;</button>
                    </footer>
                </section>
            </div>
        </form>
    </div>
</div>

<script type="text/javascript">
    $(window).bind('content-loaded', function () {
        try {
            $('.wizard').wizard();
        } catch (ex) {
        }
    });
</script>