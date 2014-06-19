using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App.Views.hcos.facilities
{
	public partial class list : Lib.Web.AppControlPage
	{
		public Lib.Data.Provider Provider;
		public IList<Lib.Data.ProviderFacility> ProviderFacilities;

		protected void Page_Init(object sender, EventArgs e)
		{
			Provider = Lib.Systems.Security.GetCurrentProvider();
			ProviderFacilities = Lib.Data.ProviderFacility.FindAll();
		}
	}
}