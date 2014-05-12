<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Site.App.Views.admin.security.prescribers.edit" %>
<%@ Import Namespace="Lib.Data" %>

<h1 class="page-title">Edit Prescriber</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
        <a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        <form id="frmEditPrescriber" class="form has-validation ajax-form" action="/api/Admin/Security/Prescriber/Edit?provider-id=<%=ProviderId%>&profile-id=<%=(PrescriberProfile.ID.HasValue? PrescriberProfile.ID.Value : 0)%>">
            <div class="clearfix">
                <label for="first-name" class="form-label">First Name</label>
                <div class="form-input">
                    <input type="text" id="first-name" name="first-name" required="required" placeholder="Enter Prescriber's First Name" 
                        value="<%=PrescriberProfile.Contact.FirstName%>" />
                </div>
            </div>

            <div class="clearfix">
                <label for="last-name" class="form-label">Last Name</label>
                <div class="form-input">
                    <input type="text" id="last-name" name="last-name" required="required" placeholder="Enter Prescriber's Last Name" 
                        value="<%=PrescriberProfile.Contact.LastName%>" />
                </div>
            </div>

            <div class="clearfix">
                <label for="phone" class="form-label">Phone</label>
                <div class="form-input">
                    <input type="text" id="phone" name="phone" required="required" placeholder="Enter Prescriber's Phone" 
                        value="<%=PrescriberProfile.Contact.Phone%>" />
                </div>
            </div>

            <div class="clearfix">
                <label for="fax" class="form-label">Fax</label>
                <div class="form-input">
                    <input type="text" id="fax" name="fax" placeholder="Enter Prescriber's Fax" 
                        value="<%=PrescriberProfile.Contact.Fax%>" />
                </div>
            </div>

            <div class="clearfix">
                <label for="email" class="form-label">Email</label>
                <div class="form-input">
                    <input type="text" id="email" name="email" required="required" placeholder="Enter Prescriber's Email" 
                        value="<%=PrescriberProfile.Contact.Email%>" />
                </div>
            </div>

            <div class="clearfix">
                <label for="street-1" class="form-label">Street 1</label>
                <div class="form-input">
                    <input type="text" id="street-1" name="street-1" required="required" placeholder="Enter Prescriber's Street 1" 
                        value="<%=PrescriberProfile.Address.Street1%>" />
                </div>
            </div>

            <div class="clearfix">
                <label for="street-2" class="form-label">Street 2</label>
                <div class="form-input">
                    <input type="text" id="street-2" name="street-2" placeholder="Enter Prescriber's Street 2" 
                        value="<%=PrescriberProfile.Address.Street2%>" />
                </div>
            </div>

            <div class="clearfix">
                <label for="city" class="form-label">City</label>
                <div class="form-input">
                    <input type="text" id="city" name="city" required="required" placeholder="Enter Prescriber's City" 
                        value="<%=PrescriberProfile.Address.City%>" />
                </div>
            </div>

            <div class="clearfix">
                <label for="state" class="form-label">State</label>
                <div class="form-input">
                    <input type="text" id="state" name="state" required="required" placeholder="Enter Prescriber's State" 
                        value="<%=PrescriberProfile.Address.State%>" />
                </div>
            </div>

            <div class="clearfix">
                <label for="zip" class="form-label">Zip</label>
                <div class="form-input">
                    <input type="text" id="zip" name="zip" required="required" placeholder="Enter Prescriber's Zip" 
                        value="<%=PrescriberProfile.Address.Zip%>" />
                </div>
            </div>

            <div class="clearfix">
                <label for="form-facility-id" class="form-label">Primary Facility <em>*</em></label>
                <div class="form-input">
                    <select id="form-facility-id" name="facility-id" required="required">
                        <option value="">Please Select</option>
                        <% foreach( var f in Facilities ) { %>
                        <option value="<%=f.Id%>"<%=((PrescriberProfile.PrimaryFacilityID == f.Id) ? " selected=\"selected\"" : "") %>><%=f.Name %></option>
                        <% } %>
                    </select>
                </div>
            </div>

            <div class="clearfix">
                <label for="form-npi" class="form-label">NPI Number</label>
                <div class="form-input">
                    <input type="text" id="form-npi" name="npi" placeholder="Enter your NPI Number" 
                        value="<%=Prescriber.NpiId%>"/>
                </div>
            </div>

            <div class="clearfix">
                <label for="form-state-id" class="form-label">State ID <em>*</em></label>
                <div class="form-input">
                    <input type="text" id="form-state-id" name="state-id" placeholder="Enter your State Id" 
                        value="<%=Prescriber.StateId%>" required="required" />
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
                <label for="form-prescriber-type" class="form-label">Prescriber Type <em>*</em></label>
                <div class="form-input">
                    <select id="form-prescriber-type" name="prescriber-type" required="required" >
                    <%foreach(PrescriberType pt in PrescriberTypes){%>
                        <option value="<%=pt.ID%>" <%=(TypeId == pt.ID)? "selected=\"selected\"" : String.Empty%>><%=pt.DisplayName%></option>
                    <%}%>
                    </select>
                </div>
            </div>

            <div class="clearfix">
                <label for="form-username" class="form-label">Username</label>
                <div class="form-input">
                    <input type="text" id="form-username" name="username" placeholder="Enter Username"
                        value="<%=User.Username%>"  />
                </div>
            </div>
            
            <div class="clearfix">
                <label for="form-password" class="form-label">Password</label>
                <div class="form-input">
                    <input type="text" id="form-password" name="password" placeholder="Enter Password" />
                </div>
            </div>
            
            <div class="clearfix">
                <label for="form-confirm-password" class="form-label">Confirm Password</label>
                <div class="form-input">
                    <input type="text" id="form-confirm-password" name="confirm-password" placeholder="Confirm your password" />
                </div>
            </div>

            <div class="form-action clearfix">
                <button class="button" type="submit">Save</button>
                <button class="button" type="reset">Reset</button>
            </div>
        </form>
    </div>
</div>