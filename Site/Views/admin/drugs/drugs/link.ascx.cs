using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App.Views.admin.drugs.drugs
{
	public partial class link : Lib.Web.AdminControlPage
	{
		public Lib.Data.Drug Drug;
		public Lib.Data.DrugLink item;
        public List<string> LinkTypes;

		protected void Page_Init(object sender, EventArgs e)
		{
			string strID = Request.QueryString["id"];
			long id;
			if (string.IsNullOrEmpty(strID) || !long.TryParse(strID, out id))
				item = new Lib.Data.DrugLink();
			else
				item = new Lib.Data.DrugLink(id);

			strID = Request.QueryString["drug-id"];
			if (!string.IsNullOrEmpty(strID) && long.TryParse(strID, out id))
				Drug = new Lib.Data.Drug(id);
			else
				base.RedirectHash("admin/drugs/drugs/list", true, "No drug selected to add a link to.");

            LinkTypes = new List<string>();
            LinkTypes.Add("Medication Guide");
            LinkTypes.Add("Patient Informed Consent");
            LinkTypes.Add("Patient Counseling Document");
            LinkTypes.Add("Appropriate Use Checklist");
            LinkTypes.Add("Certification");
            LinkTypes.Add("Patient Enrollment");
            LinkTypes.Add("Pharmacy Information");
            LinkTypes.Add("Prescriber Enrollment");
            LinkTypes.Add("Facility Enrollment");
            LinkTypes.Add("Clinical Information");
		}
	}
}