using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App.Views.hcos.facilities
{
	public partial class view : Lib.Web.AppControlPage
	{
		public Lib.Data.ProviderFacility item;
		public IList<Lib.Data.PrescriberProfile> profiles;

		protected void Page_Init(object sender, EventArgs e)
		{
			string strID = Request.QueryString["id"];
			long id;
			if (string.IsNullOrEmpty(strID) || !long.TryParse(strID, out id))
				item = new Lib.Data.ProviderFacility();
			else
				item = new Lib.Data.ProviderFacility(id);

			profiles = Lib.Data.PrescriberProfile.FindByFacility(item);
		}
	}
}