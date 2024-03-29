﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wizard.ascx.cs" Inherits="Site.App.Views.examples.wizard" %>

<h1 class="page-title">Wizard</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
        <!-- wizard -->
        <form action="#" class="wizard" novalidate>
            <nav>
                <ul class="clearfix">
                    <li class="active"><strong>1.</strong> Create Account</li>
                    <li><strong>2.</strong> Contact Information</li>
                    <li><strong>3.</strong> Finalize</li>
                </ul>
            </nav>

            <div class="items">

                <!-- page1 -->
                <section>

                    <header>
                        <h2>
                            <strong>Step 1: </strong> Account Information
                            <em>Enter your login information:</em>
                        </h2>
                    </header>

                    <section>
                        <ul class="clearfix">
                            <!-- email -->
                            <li class="required">
                                <label>
                                    <strong>1.</strong> Enter Your Email Address <span>*</span><br />
                                    <input type="text" class="full" name="email" required />
                                    <em>Your password will be sent to this address. Your address will not made public.</em>
                                </label>
                            </li>

                            <!-- username -->
                            <li>
                                <label>
                                    <strong>2.</strong> Pick a username <br />
                                    <input type="text" class="full" name="username" />
                                    <em>Your preferred username to be used when logging in.</em>
                                </label>
                            </li>

                            <!-- password -->
                            <li class="double">

                                <label>
                                    <strong>3.</strong> Choose a Password <span>*</span><br />
                                    <input type="password" class="full" name="password" required />
                                    <em>Must be at least 8 characters long.</em>
                                </label>

                                <label>
                                    Verify Password <span>*</span><br />
                                    <input type="password" class="full" name="password1" required equalTo="password"  />
                                </label>
                            </li>
                        </ul>
                    </section>

                    <footer class="clearfix">
                        <button type="button" class="next fr">Proceed &raquo;</button>
                    </footer>

                </section>

                <!-- page2 -->
                <section>

                    <header>
                        <h2>
                            <strong>Step 2: </strong> Contact Information <b></b>
                            <em>Tell us where you live:</em>
                        </h2>
                    </header>

                    <section>
                        <ul class="clearfix">
                            <!-- address -->
                            <li>
                                <label>
                                    <strong>1.</strong> Enter Your Street Address <span>*</span><br />
                                    <input type="text" class="full" name="email" required />
                                    <em><strong>Example</strong>: Random Street 69 A 666</em>
                                </label>
                            </li>

                            <!-- zip / city -->
                            <li class="double">

                                <label>
                                    <strong>2.</strong> Enter Your Zip Code <span>*</span><br />
                                    <input type="text" class="full" name="zip" required />
                                    <em>This must be a numeric value</em>
                                </label>

                                <label>
                                    <strong>3.</strong> and The City <span>*</span>
                                    <select name="city" class="full" required>
                                        <option value="">-- please select --</option>
                                        <option>Helsinki</option>
                                        <option>Berlin</option>
                                        <option>New York</option>
                                    </select>
                                </label>
                            </li>
                        </ul>
                    </section>

                    <footer class="clearfix">
                        <button type="button" class="prev fl">&laquo; Back</button>
                        <button type="button" class="next fr">Proceed &raquo;</button>
                    </footer>

                </section>

                <!-- page3 -->
                <section>

                    <header>
                        <h2>
                            <strong>Step 3: </strong> Congratulations!
                            <em>You are now successfully registered.</em>
                        </h2>
                    </header>

                    <section>
                        <h3>Thank you for registering!</h3>
                    </section>

                    <footer class="clearfix">
                        <button type="button" class="prev">&laquo; Back</button>
                    </footer>

                </section>


            </div><!--items-->

        </form><!--wizard-->

    </div>
</div>

<!-- WIZARD SETUP -->
<script type="text/javascript">
    $(function () {
    	$('.wizard').wizard();
 });

 $(window).bind('content-loaded', function () {
     $('.wizard').wizard();
 });
</script>
<!-- WIZARD SETUP END -->