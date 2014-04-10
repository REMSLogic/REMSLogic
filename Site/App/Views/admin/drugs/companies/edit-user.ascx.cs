using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App.Views.admin.drugs.companies
{
	public partial class edit_user : Lib.Web.AdminControlPage
	{
		public Lib.Data.DrugCompanyUser item;
		public Lib.Data.DrugCompany Company;
		public Lib.Data.UserProfile profile;
		public Lib.Data.Contact contact;
		public Lib.Data.Address address;
		public Framework.Security.User user;

		protected void Page_Init(object sender, EventArgs e)
		{
			string strID = Request.QueryString["id"];
			long id;

			string strParentID = Request.QueryString["parent-id"];
			long parent_id;

			if (!long.TryParse(strParentID, out parent_id))
				RedirectHash("admin/drugs/companies/list");

			Company = new Lib.Data.DrugCompany(parent_id);

			if (string.IsNullOrEmpty(strID) || !long.TryParse(strID, out id))
			{
				item = new Lib.Data.DrugCompanyUser();
				profile = new Lib.Data.UserProfile();
				contact = new Lib.Data.Contact();
				address = new Lib.Data.Address();
				user = new Framework.Security.User();
			}
			else
			{
				item = new Lib.Data.DrugCompanyUser(id);
				profile = item.Profile;
				user = profile.User;
				contact = profile.PrimaryContact;
				address = profile.PrimaryAddress;
			}
		}
	}
}