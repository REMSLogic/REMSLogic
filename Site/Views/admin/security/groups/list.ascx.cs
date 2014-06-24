using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App.Views.admin.security.groups
{
	public partial class list : Lib.Web.AppControlPage
	{
		public IList<Framework.Security.Group> Groups;
		protected void Page_Init(object sender, EventArgs e)
		{
			Groups = Framework.Security.Group.FindAll();
		}
	}
}