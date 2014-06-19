using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App.Views.dev.roles
{
	public partial class list : Lib.Web.AppControlPage
	{
		public IList<Framework.Security.Role> Items;
		protected void Page_Init(object sender, EventArgs e)
		{
			Items = Framework.Security.Role.FindAll();
		}
	}
}