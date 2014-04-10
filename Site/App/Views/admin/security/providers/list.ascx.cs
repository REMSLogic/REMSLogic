using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App.Views.admin.security.providers
{
	public partial class list : Lib.Web.AppControlPage
	{
		public IList<Lib.Data.Provider> Providers;
		protected void Page_Init(object sender, EventArgs e)
		{
			Providers = Lib.Data.Provider.FindAll();
		}
	}
}