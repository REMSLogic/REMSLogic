using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App.Views.admin.drugs.companies
{
	public partial class view : Lib.Web.AppControlPage
	{
		public Lib.Data.DrugCompany item;

		protected void Page_Init(object sender, EventArgs e)
		{
			string strID = Request.QueryString["id"];
			long id;
			if (string.IsNullOrEmpty(strID) || !long.TryParse(strID, out id))
			{
				RedirectHash("admin/drugs/companies/list", true, "Invalid Drug Company ID");
			}
			else
			{
				item = new Lib.Data.DrugCompany(id);
			}
		}
	}
}