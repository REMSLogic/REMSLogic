<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="detail.ascx.cs" Inherits="Site.App.Views.common.drugs.companies.detail" %>

<!-- DATATABLES CSS -->
<link rel="stylesheet" media="screen" href="/js/lib/datatables/css/vpad.css" />
<script type="text/javascript" src="/js/lib/datatables/js/jquery.dataTables.js"></script> 
<script type="text/javascript">
    $(window).bind('content-loaded', function () {
        $('#users-table').dataTable({
            "sPaginationType": "full_numbers",
            "bStateSave": true,
            "iCookieDuration": (60 * 60 * 24 * 30)
        });
    });
</script> 
<!-- DATATABLES CSS END -->
<h1 class="page-title">View Company</h1>
<div class="container_12 clearfix leading">
    <div class="grid_12">
        <a class="button back-button" href="#" style="margin-bottom: 10px;">Back</a>
        <h2>General Company Information</h2>
        <form style="margin-bottom: 32px;" class="form">
            <div class="clearfix">
                <label for="form-name" class="form-label">Name</label>
                <div class="form-input"><input type="text" id="form-name" name="name" required="required" placeholder="Enter the Name" value="<%=DrugCompany.Name%>" disabled="disabled" /></div>
            </div>

            <div class="clearfix">
                <label for="form-website" class="form-label">Website</label>
                <div class="form-input"><input type="url" id="form-website" name="website" placeholder="No website provided" value="<%=DrugCompany.Website%>" disabled="disabled" /></div>
            </div>

            <div class="clearfix">
                <label for="form-phone" class="form-label">Phone</label>
                <div class="form-input"><input type="text" id="form-phone" name="phone" placeholder="Enter the Phone Number" value="<%=DrugCompany.Phone%>" disabled="disabled" /></div>
            </div>
        </form>
        
        <%if(Formulation != null){ %>
        <h2>Formulation Information</h2>
        <form class="form">
            <div class="clearfix">
                <label for="form-name" class="form-label">Formulation</label>
                <div class="form-input"><input type="text" id="Text3" name="name" placeholder="Not Provided" value="<%=Formulation.Formulation.Name%>" disabled="disabled" /></div>
            </div>

            <div class="clearfix">
                <label for="form-name" class="form-label">Brand Name</label>
                <div class="form-input"><input type="text" id="Text4" name="name" placeholder="Not Provided" value="<%=Formulation.BrandName%>" disabled="disabled" /></div>
            </div>

            <div class="clearfix">
                <label for="form-name" class="form-label">Formulation Website</label>
                <div class="form-input"><input type="text" id="Text5" name="name" placeholder="Not Provided" value="<%=Formulation.DrugCompanyURL%>" disabled="disabled" /></div>
            </div>

            <div class="clearfix">
                <label for="form-name" class="form-label">Email for Formulation Information</label>
                <div class="form-input"><input type="text" id="Text6" name="name" placeholder="Not Provided" value="<%=Formulation.DrugCompanyEmail%>" disabled="disabled" /></div>
            </div>
            <div class="clearfix">
                <label for="form-name" class="form-label">Phone for Formulation Information</label>
                <div class="form-input"><input type="text" id="Text1" name="name" placeholder="Not Provided" value="<%=Formulation.DrugCompanyPhone%>" disabled="disabled" /></div>
            </div>

            <div class="clearfix">
                <label for="form-website" class="form-label">Fax for Formulation Information</label>
                <div class="form-input"><input type="url" id="Url1" name="website" placeholder="Not Provided" value="<%=Formulation.DrugCompanyFax%>" disabled="disabled" /></div>
            </div>
        </form>
        <%}%>
    </div>
</div>