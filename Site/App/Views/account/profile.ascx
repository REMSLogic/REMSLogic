<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="profile.ascx.cs" Inherits="Site.App.Views.account.profile" %>
<%@ Import Namespace="Lib.Data" %>

<!-- DATATABLES CSS -->
<link rel="stylesheet" media="screen" href="/App/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/App/js/lib/datatables/js/jquery.dataTables.js"></script> 

<% if( Framework.Security.Manager.HasRole("view_prescriber", true) ) { %>
<script type="text/javascript">
    $(window).bind('content-loaded', function () {
        $('#profiles-table').dataTable({
            "sPaginationType": "full_numbers",
            "bStateSave": true,
            "iCookieDuration": (60 * 60 * 24 * 30)
        });
    });
</script>
<%} %>

<h1 class="page-title">User Profile</h1>
<div class="container_12 clearfix">
    <div class="grid_12">
      <%--  <a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>--%>
        <h2>User</h2>
        <form style="margin-bottom: 32px;" class="form has-validation ajax-form" action="/api/Common/Account/Profile?id=<%=UserInfo.ID%>">
            <div class="clearfix">
                <label for="form-email" class="form-label">Email <em>*</em></label>
                <div class="form-input"><input type="email" id="form-email" name="email" required="required" placeholder="Enter your email" value="<%=UserInfo.Email%>" /></div>
            </div>

            <div class="clearfix">
                <label for="form-current-password" class="form-label">Current Password <em>*</em></label>
                <div class="form-input"><input type="password" id="form-current-password" name="current-password" placeholder="Your current password" value="" /></div>
            </div>

            <div class="clearfix">
                <label for="form-new-password" class="form-label">New Password <em>*</em></label>
                <div class="form-input"><input type="password" id="form-new-password" name="new-password" placeholder="Enter your new password" value="" /></div>
            </div>

            <div class="clearfix">
                <label for="form-confirm-password" class="form-label">Confirm Password <em>*</em></label>
                <div class="form-input"><input type="password" id="form-confirm-password" name="confirm-password" placeholder="Confirm your new password" value="" /></div>
            </div>
            
            <div class="form-action clearfix">
                <button class="button" type="submit">Save</button>
                <button class="button" type="reset">Reset</button>
            </div>
        </form>
        
        <h2>REMS Logic Contact</h2>
        <%if(PrimaryContact != null){%>
        <form style="margin-bottom: 32px;" id="frmEditPrescriber" class="form has-validation ajax-form" action="/api/Common/Contact/EditPrimary?id=<%=PrimaryContact.ID.Value%>">
            <div class="clearfix">
                <label for="first-name" class="form-label">First Name</label>
                <div class="form-input"><input type="text" id="first-name" name="first-name" required="required" placeholder="Enter Your First Name" value="<%=PrimaryContact.FirstName%>" /></div>
            </div>

            <div class="clearfix">
                <label for="last-name" class="form-label">Last Name</label>
                <div class="form-input"><input type="text" id="last-name" name="last-name" required="required" placeholder="Enter Your Last Name" value="<%=PrimaryContact.LastName%>" /></div>
            </div>

            <div class="clearfix">
                <label for="phone" class="form-label">Phone</label>
                <div class="form-input"><input type="text" id="phone" name="phone" required="required" placeholder="Enter Your Phone" value="<%=PrimaryContact.Phone%>" /></div>
            </div>

            <div class="clearfix">
                <label for="fax" class="form-label">Fax</label>
                <div class="form-input"><input type="text" id="fax" name="fax" placeholder="Enter Your Fax" value="<%=PrimaryContact.Fax%>" /></div>
            </div>

            <div class="form-action clearfix">
                <button class="button" type="submit">Save</button>
                <button class="button" type="reset">Reset</button>
            </div>
        </form>
        <%} else {%>
            <div style="margin-bottom: 32px;" >
                There is no contact associated with your user profile.
            </div>
        <%}%>
        
        <h2>REMS Logic Address</h2>
        <%if(PrimaryAddress != null && PrimaryAddress.ID != null){%>
        <form style="margin-bottom: 32px;" id="Form1" class="form has-validation ajax-form" action="/api/Common/Address/EditPrimary?id=<%=PrimaryAddress.ID.Value%>">
            <div class="clearfix">
                <label for="street-1" class="form-label">Street 1</label>
                <div class="form-input"><input type="text" id="street-1" name="street-1" required="required" placeholder="Enter Prescriber's Street 1" value="<%=PrimaryAddress.Street1%>" /></div>
            </div>

            <div class="clearfix">
                <label for="street-2" class="form-label">Street 2</label>
                <div class="form-input"><input type="text" id="street-2" name="street-2" placeholder="Enter Prescriber's Street 2" value="<%=PrimaryAddress.Street2%>" /></div>
            </div>

            <div class="clearfix">
                <label for="city" class="form-label">City</label>
                <div class="form-input"><input type="text" id="city" name="city" required="required" placeholder="Enter Prescriber's City" value="<%=PrimaryAddress.City%>" /></div>
            </div>

            <div class="clearfix">
                <label for="state" class="form-label">State</label>
                <div class="form-input"><input type="text" id="state" name="state" required="required" placeholder="Enter Prescriber's State" value="<%=PrimaryAddress.State%>" /></div>
            </div>

            <div class="clearfix">
                <label for="zip" class="form-label">Zip</label>
                <div class="form-input"><input type="text" id="zip" name="zip" required="required" placeholder="Enter Prescriber's Zip" value="<%=PrimaryAddress.Zip%>" /></div>
            </div>

            <div class="form-action clearfix">
                <button class="button" type="submit">Save</button>
                <button class="button" type="reset">Reset</button>
            </div>
        </form>
        <%}else{ %>
            <div style="margin-bottom: 32px;" >
                There is no address associated with your user profile.
            </div>
        <%} %>
        
        <% if( Framework.Security.Manager.HasRole("view_prescriber", true) ) { %>
        <h2>Prescriber</h2>
        <form style="margin-bottom: 32px;" id="Form2" class="form has-validation ajax-form" action="/api/Prescribers/Edit?id=<%=Prescriber.ID.Value%>">
            
            <div class="clearfix">
                <label for="form-npi" class="form-label">NPI Number</label>
                <div class="form-input"><input type="text" id="form-npi" name="npi" placeholder="Enter your NPI Number" value="<%=Prescriber.NpiId%>"/></div>
            </div>

            <div class="clearfix">
                <label for="form-state-id" class="form-label">State ID <em>*</em></label>
                <div class="form-input">
                    <input type="text" id="form-state-id" name="state-id" placeholder="Enter your State Id" value="<%=Prescriber.StateId%>" required="required" />
                </div>
            </div>
                                
            <div class="clearfix">
                <label for="form-state-id" class="form-label">Issuing State <em>*</em></label>
                <div class="form-input">
                    <select id="form-issuer" name="issuer" required="required" >
                    <%foreach(State s in States){%>
                        <option value="<%=s.ID%>" <%=(Prescriber.StateIdIssuer == s.ID)? "selected=\"selected\"" : String.Empty%>><%=s.USPS%></option>
                    <%}%>
                    </select>
                </div>
            </div>

            <div class="clearfix">
                <label for="form-speciality" class="form-label">Speciality <em>*</em></label>
                <div class="form-input">
                    <select id="form-speciality" name="speciality" required="required" >
                    <%foreach(Speciality s in Specialities){%>
                        <option value="<%=s.ID%>" <%=(SpecialityId == s.ID)? "selected=\"selected\"" : String.Empty%>><%=s.Name%></option>
                    <%}%>
                    </select>
                </div>
            </div>

            <div class="clearfix">
                <label for="form-primary-contact-id" class="form-label">Primary Contact <em>*</em></label>
                <div class="form-input">
                    <select id="form-primary-contact-id" name="primary-contact-id" required="required" >
                    <%foreach(Contact c in Contacts){%>
                        <option value="<%=c.ID%>" <%=(Prescriber.Profile.PrimaryContactID == c.ID)? "selected=\"selected\"" : String.Empty%>><%=c.Name%></option>
                    <%}%>
                    </select>
                </div>
            </div>

            <div class="clearfix">
                <label for="form-primary-address-id" class="form-label">Primary Address <em>*</em></label>
                <div class="form-input">
                    <select id="form-primary-address-id" name="primary-address-id" required="required" >
                    <%foreach(Address a in Addresses){%>
                        <option value="<%=a.ID%>" <%=(Prescriber.Profile.PrimaryAddressID == a.ID)? "selected=\"selected\"" : String.Empty%>><%=a.ToString()%></option>
                    <%}%>
                    </select>
                </div>
            </div>

            <div class="form-action clearfix">
                <button class="button" type="submit">Save</button>
                <button class="button" type="reset">Reset</button>
            </div>
        </form>

        <h2>Profiles</h2>
        <div class="detail-sub-section clearfix">
            <table class="display" id="profiles-table">
                <thead>
                    <tr>
                        <th></th>
                        <th>Provider</th>
                        <th>Facility</th>
                        <th>Street 1</th>
                        <th>Street 2</th>
                        <th>City</th>
                        <th>State</th>
                    </tr>
                </thead>
                <tbody>
                <% foreach(PrescriberProfile profile in PrescriberProfiles) { %>
                    <tr data-id="<%=profile.ID%>"> 
                        <td><a href="#prescriber/profiles/edit?id=<%=profile.ID%>" class="button">View</a></td>
                        <td><%=(profile.Provider != null)? profile.Provider.Name : String.Empty%></td>
                        <td><%=(profile.Facility != null)? profile.Facility.Name : String.Empty%></td>
                        <td><%=profile.Address.Street1%></td>
                        <td><%=profile.Address.Street2 %></td>
                        <td><%=profile.Address.City%></td>
                        <td><%=profile.Address.State %></td>
                    </tr>
                <% } %>
                </tbody>
            </table>
        </div>
        <%}%>
    </div>
</div>