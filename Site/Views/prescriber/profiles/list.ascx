<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="list.ascx.cs" Inherits="Site.App.Views.prescriber.profiles.list" %>
<%@ Import Namespace="Lib.Data" %>

<!-- DATATABLES CSS -->
<link rel="stylesheet" media="screen" href="/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/js/lib/datatables/js/jquery.dataTables.js"></script> 
<script type="text/javascript">
    $(window).bind('content-loaded', function () {
        $('#profiles-table').dataTable({
            "sPaginationType": "full_numbers",
            "bStateSave": true,
            "iCookieDuration": (60 * 60 * 24 * 30)
        });
    });
</script> 

<h1 class="page-title">Manage Prescriber Profiles</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12 manage-drug-list">
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        <form style="margin-bottom: 32px;" id="frmEditPrescriber" class="form has-validation ajax-form" action="/api/Prescribers/Edit?id=<%=Prescriber.ID.Value%>">
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
        <div class="detail-sub-section clearfix">
            <h2>Prescriber Profiles</h2>
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
    </div>
</div>