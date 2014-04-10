using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App.Views.admin.drugs.companies
{
	public partial class edit : Lib.Web.AdminControlPage
	{
		public Lib.Data.DrugCompany item;
		public IList<Lib.Data.DrugCompanyUser> Users;

		protected void Page_Init(object sender, EventArgs e)
		{
			string strID = Request.QueryString["id"];
			long id;
			if (string.IsNullOrEmpty(strID) || !long.TryParse(strID, out id))
			{
				item = new Lib.Data.DrugCompany();
				Users = new List<Lib.Data.DrugCompanyUser>();
			}
			else
			{
				item = new Lib.Data.DrugCompany(id);
				Users = Lib.Data.DrugCompanyUser.FindByDrugCompany(item);
			}
		}
	}
}