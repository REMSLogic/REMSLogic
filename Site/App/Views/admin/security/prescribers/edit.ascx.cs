using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App.Views.admin.security.prescribers
{
	public partial class edit : Lib.Web.AdminControlPage
	{
		public Lib.Data.Prescriber item;
		public Lib.Data.UserProfile profile;
		public Lib.Data.Contact contact;
		public Framework.Security.User user;
		public Lib.Data.Address address;

		protected void Page_Init(object sender, EventArgs e)
		{
			string strID = Request.QueryString["id"];
			long id;
			if (string.IsNullOrEmpty(strID) || !long.TryParse(strID, out id))
			{
				item = new Lib.Data.Prescriber();
				profile = new Lib.Data.UserProfile();
				contact = new Lib.Data.Contact();
				user = new Framework.Security.User();
				address = new Lib.Data.Address();
			}
			else
			{
				item = new Lib.Data.Prescriber(id);
				profile = item.Profile;
				user = new Framework.Security.User(profile.UserID);
				address = new Lib.Data.Address(profile.PrimaryAddressID);
				contact = new Lib.Data.Contact(profile.PrimaryContactID);
			}
		}
	}
}