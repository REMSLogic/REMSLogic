using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App.Views.hcos.facilities
{
	public partial class edit : Lib.Web.AppControlPage
	{
		public Lib.Data.ProviderFacility item;
		public Lib.Data.Address address;
		public long ProviderID;

		protected void Page_Init(object sender, EventArgs e)
		{
			string strID = Request.QueryString["id"];
			string strPID = Request.QueryString["provider-id"];
			long id;

			// TODO: Once actual roles are in and setup, uncomment the role checks in this function - TJM 12/04/2013
			/* RequireAnyRole("hco_facility_view_all", "hco_facility_view_mine"); */

			if (string.IsNullOrEmpty(strID) || !long.TryParse(strID, out id))
			{
				if (string.IsNullOrEmpty(strPID) || !long.TryParse(strPID, out this.ProviderID))
				{
					this.RedirectBack(true, "Invalid provider ID");
					return;
				}
				/*else if (!HasRole("hco_facility_view_all") && !Lib.Systems.UserInfo.HasProvider(this.ProviderID))
				{
					this.RedirectBack(true, "Invalid provider ID");
					return;
				}*/

				item = new Lib.Data.ProviderFacility();
				address = new Lib.Data.Address();
			}
			else
			{
				item = new Lib.Data.ProviderFacility(id);
				address = new Lib.Data.Address(item.AddressID);

				/*if (!HasRole("hco_facility_view_all") && !Lib.Systems.UserInfo.HasProvider(item.ProviderID))
				{
					this.RedirectBack(true, "Invalid provider ID");
					return;
				}*/

				this.ProviderID = item.ProviderID;
			}
		}
	}
}