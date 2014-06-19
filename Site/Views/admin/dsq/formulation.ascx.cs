using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App.Views.admin.dsq
{
	public partial class formulation : Lib.Web.AdminControlPage
	{
		public Lib.Data.DrugFormulation item;
		public Lib.Data.Drug drug;
		public IList<Lib.Data.DrugCompany> Companies;
		public IList<Lib.Data.Formulation> Formulations;

		public string autocomplete_formulations;

		protected void Page_Init(object sender, EventArgs e)
		{
			string strID = Request.QueryString["drug-id"];
			long drug_id;
			if (string.IsNullOrEmpty(strID) || !long.TryParse(strID, out drug_id))
			{
				RedirectHash("admin/drugs/drugs/list");
				return;
			}

			drug = new Lib.Data.Drug(drug_id);

			strID = Request.QueryString["id"];
			long id;
			if (string.IsNullOrEmpty(strID) || !long.TryParse(strID, out id))
				item = new Lib.Data.DrugFormulation();
			else
				item = new Lib.Data.DrugFormulation(id);

			Companies = Lib.Data.DrugCompany.FindAll();
			Formulations = Lib.Data.Formulation.FindAll();

			autocomplete_formulations = "";

			foreach (var f in Formulations)
			{
				if (!string.IsNullOrEmpty(autocomplete_formulations))
					autocomplete_formulations += "\",\"";
				autocomplete_formulations += f.Name.Replace("\"", "\\\"");
			}

			autocomplete_formulations = "[\"" + autocomplete_formulations + "\"]";
		}
	}
}