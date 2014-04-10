<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="edit.ascx.cs" Inherits="Site.App.Views.prescriber.profiles.edit" %>
<%@ Import Namespace="Lib.Data" %>

<h1 class="page-title">Prescriber Profile</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12 manage-drug-list">
        <a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        
        
        <h3>HCO Information</h3>
        <form style="margin-bottom: 32px;" class="form">
            <%if(Provider != null){%>
                <div class="clearfix">
                    <label for="provider-name" class="form-label">Name</label>
                    <div class="form-input"><input type="text" id="provider-name" value="<%=Provider.Name%>" /></div>
                </div>

                <div class="clearfix">
                    <label for="provider-facility-size" class="form-label">Facility Size</label>
                    <div class="form-input"><input type="password" id="provider-facility-size" value="<%=Provider.FacilitySize%>" /></div>
                </div>

                <%if(Provider.Address != null){%>
                <div class="clearfix">
                    <label for="provider-street-1" class="form-label">Street 1</label>
                    <div class="form-input"><input type="text" id="provider-street-1" value="<%=Provider.Address.Street1%>" /></div>
                </div>

                <div class="clearfix">
                    <label for="provider-street-2" class="form-label">Street 2</label>
                    <div class="form-input"><input type="text" id="provider-street-2" value="<%=Provider.Address.Street2%>" /></div>
                </div>

                <div class="clearfix">
                    <label for="provider-city" class="form-label">City</label>
                    <div class="form-input"><input type="text" id="provider-city" value="<%=Provider.Address.City%>" /></div>
                </div>

                <div class="clearfix">
                    <label for="provider-state" class="form-label">State</label>
                    <div class="form-input"><input type="text" id="provider-state" value="<%=Provider.Address.State%>" /></div>
                </div>

                <div class="clearfix">
                    <label for="provider-zip" class="form-label">Zip</label>
                    <div class="form-input"><input type="text" id="provider-zip" value="<%=Provider.Address.Zip%>" /></div>
                </div>
                <%}%>
            <%} else {%>
                <div class="clearfix">
                    <div class="form-label">No HCO information available.</div>
                </div>
            <%}%>
        </form>

        <h3>Facility Information</h3>
        <form style="margin-bottom: 32px;" class="form">
            <%if(Facility != null){ %>
                <div class="clearfix">
                    <label for="provider-name" class="form-label">Name</label>
                    <div class="form-input"><input type="text" id="Text1" value="<%=Facility.Name%>" /></div>
                </div>

                <%if(Facility.PrimaryAddress != null){%>
                <div class="clearfix">
                    <label for="provider-street-1" class="form-label">Street 1</label>
                    <div class="form-input"><input type="text" id="Text2" value="<%=Facility.PrimaryAddress.Street1%>" /></div>
                </div>

                <div class="clearfix">
                    <label for="provider-street-2" class="form-label">Street 2</label>
                    <div class="form-input"><input type="text" id="Text3" value="<%=Facility.PrimaryAddress.Street2%>" /></div>
                </div>

                <div class="clearfix">
                    <label for="provider-city" class="form-label">City</label>
                    <div class="form-input"><input type="text" id="Text4" value="<%=Facility.PrimaryAddress.City%>" /></div>
                </div>

                <div class="clearfix">
                    <label for="provider-state" class="form-label">State</label>
                    <div class="form-input"><input type="text" id="Text5" value="<%=Facility.PrimaryAddress.State%>" /></div>
                </div>

                <div class="clearfix">
                    <label for="provider-zip" class="form-label">Zip</label>
                    <div class="form-input"><input type="text" id="Text6" value="<%=Facility.PrimaryAddress.Zip%>" /></div>
                </div>
                <%}%>
            <%} else { %>
                <div class="clearfix">
                    <div class="form-label">No facility information available.</div>
                </div>
            <%} %>
        </form>

        <h3><%=(Provider != null)? "(for "+Provider.Name+")" : String.Empty%> Contact</h3>
        <form style="margin-bottom: 32px;" id="frmEditPrescriber" class="form has-validation ajax-form" action="/api/Common/Contact/EditPrimary?id=<%=Contact.ID.Value%>">
            <%if(Contact != null){%>
                <div class="clearfix">
                    <label for="first-name" class="form-label">First Name</label>
                    <div class="form-input"><input type="text" id="first-name" name="first-name" required="required" placeholder="Enter Your First Name" value="<%=Contact.FirstName%>" /></div>
                </div>

                <div class="clearfix">
                    <label for="last-name" class="form-label">Last Name</label>
                    <div class="form-input"><input type="text" id="last-name" name="last-name" required="required" placeholder="Enter Your Last Name" value="<%=Contact.LastName%>" /></div>
                </div>

                <div class="clearfix">
                    <label for="phone" class="form-label">Phone</label>
                    <div class="form-input"><input type="text" id="phone" name="phone" required="required" placeholder="Enter Your Phone" value="<%=Contact.Phone%>" /></div>
                </div>

                <div class="clearfix">
                    <label for="fax" class="form-label">Fax</label>
                    <div class="form-input"><input type="text" id="fax" name="fax" placeholder="Enter Your Fax" value="<%=Contact.Fax%>" /></div>
                </div>

                <div class="form-action clearfix">
                    <button class="button" type="submit">Save</button>
                    <button class="button" type="reset">Reset</button>
                </div>
            <%} else {%>
                <div class="clearfix">
                    <div class="form-label">No contact information available.</div>
                </div>
            <%}%>
        </form>

        
        <h3><%=(Provider != null)? "(for "+Provider.Name+")" : String.Empty%> Address</h3>
        <form style="margin-bottom: 32px;" id="Form1" class="form has-validation ajax-form" action="/api/Common/Address/EditPrimary?id=<%=Address.ID.Value%>">
            <%if(Address != null){%>
                <div class="clearfix">
                    <label for="street-1" class="form-label">Street 1</label>
                    <div class="form-input"><input type="text" id="street-1" name="street-1" required="required" placeholder="Enter Prescriber's Street 1" value="<%=Address.Street1%>" /></div>
                </div>

                <div class="clearfix">
                    <label for="street-2" class="form-label">Street 2</label>
                    <div class="form-input"><input type="text" id="street-2" name="street-2" placeholder="Enter Prescriber's Street 2" value="<%=Address.Street2%>" /></div>
                </div>

                <div class="clearfix">
                    <label for="city" class="form-label">City</label>
                    <div class="form-input"><input type="text" id="city" name="city" required="required" placeholder="Enter Prescriber's City" value="<%=Address.City%>" /></div>
                </div>

                <div class="clearfix">
                    <label for="state" class="form-label">State</label>
                    <div class="form-input"><input type="text" id="state" name="state" required="required" placeholder="Enter Prescriber's State" value="<%=Address.State%>" /></div>
                </div>

                <div class="clearfix">
                    <label for="zip" class="form-label">Zip</label>
                    <div class="form-input"><input type="text" id="zip" name="zip" required="required" placeholder="Enter Prescriber's Zip" value="<%=Address.Zip%>" /></div>
                </div>

                <div class="form-action clearfix">
                    <button class="button" type="submit">Save</button>
                    <button class="button" type="reset">Reset</button>
                </div>
            <%}else{ %>
                <div class="clearfix">
                    <div class="form-label">No address information available.</div>
                </div>
            <%} %>
        </form>
        
        <h3>Other Information</h3>
        <form style="margin-bottom: 32px;" id="Form1" class="form has-validation ajax-form" action="/api/Prescriber/Profile/Edit?id=<%=PrescriberProfile.ID.Value%>">
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

            <div class="form-action clearfix">
                <button class="button" type="submit">Save</button>
                <button class="button" type="reset">Reset</button>
            </div>
        </form>
    </div>
</div>