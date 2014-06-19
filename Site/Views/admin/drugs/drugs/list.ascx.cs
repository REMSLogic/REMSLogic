using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App.Views.admin.drugs.drugs
{
	public partial class list : Framework.Web.BaseControl
	{
		public bool Pending;
		public string Title;

		public IList<Lib.Data.Drug> Drugs;
		protected void Page_Init(object sender, EventArgs e)
		{
			if( !string.IsNullOrEmpty(Request.QueryString["pending"]) && Request.QueryString["pending"] == "true" )
				Pending = true;

			bool my = false;
			if (!string.IsNullOrEmpty(Request.QueryString["my"]) && Request.QueryString["my"] == "true")
				my = true;

			if( my )
			{
				var up = Lib.Data.UserProfile.FindByUser(Framework.Security.Manager.GetUser());
				if( up.UserType == null || up.UserType.Name != "drug-company" )
				{
					RedirectHash("dashboard", true, "You don't have permission to see that page.");
					return;
				}

				var dcu = Lib.Data.DrugCompanyUser.FindByProfile(up);

				if( Pending )
				{
					Title = "My Pending Drug Changes";
					Drugs = Lib.Data.Drug.FindPending(dcu.DrugCompanyID);
				}
				else
				{
					Title = "My Drugs";
					Drugs = Lib.Data.Drug.FindAll(false, dcu.DrugCompanyID);
				}
			}
			else
			{
				if( Pending )
				{
					Title = "Pending Drug Changes";
					Drugs = Lib.Data.Drug.FindPending();
				}
				else
				{
					Title = "Drugs";
					Drugs = Lib.Data.Drug.FindAll(false);
				}
			}
		}
	}
}