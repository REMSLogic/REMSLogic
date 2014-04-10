using System.Collections.Generic;
using System.Linq;

namespace Site.App.Controls.DSQ
{
	public partial class GeneralInfo : Lib.Web.AppControlPage
	{
		public Lib.Data.Drug item;
		public IList<Lib.Data.DrugSystem> Systems;
		public IList<Lib.Data.DrugFormulation> Formulations;
		public IList<Lib.Data.DrugVersion> Versions;

		public GeneralInfo() : base()
		{
			string strID = System.Web.HttpContext.Current.Request.QueryString["id"];
			long id;
			if (string.IsNullOrEmpty(strID) || !long.TryParse(strID, out id))
				item = new Lib.Data.Drug();
			else
				item = new Lib.Data.Drug(id);

			Systems = Lib.Data.DrugSystem.FindAll();
			Formulations = Lib.Data.DrugFormulation.FindByDrug(item);
			Versions = (from v in Lib.Data.DrugVersion.FindByDrug( item )
                        orderby v.Updated descending
                        select v).ToList();
		}
	}
}