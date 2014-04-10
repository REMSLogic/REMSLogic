<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="formulation.ascx.cs" Inherits="Site.App.Views.admin.dsq.formulation" %>

<script type="text/javascript" src="/App/js/jquery.autocomplete.js"></script>
<script type = "text/javascript">
	$(window).bind('content-loaded',function () {
		var data = <%=this.autocomplete_formulations %>;
		$("#form-formulation").autocomplete(data, {
			matchContains: true,
			scroll: true
		});
	});
</script>

<h1 class="page-title">Edit Drug Formulation</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
		<a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
		<form class="form has-validation ajax-form" action="/api/Admin/Drugs/DrugFormulation/Edit?drug-id=<%=drug.ID%>&id=<%=((item.ID == null) ? 0 : item.ID)%>">
			
			<div class="clearfix">
                <label for="form-formulation" class="form-label">Formulation <em>*</em></label>
                <div class="form-input"><input type="text" id="form-formulation" name="formulation" required="required" placeholder="The Drug's Formulation" value="<%=((item.ID != null) ? item.Formulation.Name : "") %>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-brand-name" class="form-label">Brand Name</label>
                <div class="form-input"><input type="text" id="form-brand-name" name="brand-name" placeholder="Enter the Brand Name" value="<%=item.BrandName%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-drug-company-id" class="form-label">Company <em>*</em></label>
                <div class="form-input">
					<select id="form-drug-company-id" name="drug-company-id" required="required">
						<option value="">Please select</option>
						<% foreach(var c in this.Companies) { %>
						<option value="<%=c.ID %>"<%=((item.DrugCompanyID == c.ID.Value) ? " selected=\"selected\"" : "") %>><%=c.Name %></option>
						<% } %>
					</select>
				</div>
            </div>

			<div class="clearfix">
                <label for="form-drug-company-url" class="form-label">REMS Program Website</label>
                <div class="form-input"><input type="url" id="form-drug-company-url" name="drug-company-url" placeholder="Enter the Company URL" value="<%=item.DrugCompanyURL%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-drug-company-email" class="form-label">Company Email</label>
                <div class="form-input"><input type="email" id="form-drug-company-email" name="drug-company-email" placeholder="Enter the Company Email" value="<%=item.DrugCompanyEmail%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-drug-company-phone" class="form-label">Company Phone</label>
                <div class="form-input"><input type="text" id="form-drug-company-phone" name="drug-company-phone" placeholder="Enter the Company Phone" value="<%=item.DrugCompanyPhone%>" /></div>
            </div>

			<div class="clearfix">
                <label for="form-drug-company-fax" class="form-label">Company Fax</label>
                <div class="form-input"><input type="text" id="form-drug-company-fax" name="drug-company-fax" placeholder="Enter the Company Fax" value="<%=item.DrugCompanyFax%>" /></div>
            </div>

			<div class="form-action clearfix">
                <button class="button" type="submit">Save</button>
                <button class="button" type="reset">Reset</button>
            </div>
		</form>
	</div>
</div>