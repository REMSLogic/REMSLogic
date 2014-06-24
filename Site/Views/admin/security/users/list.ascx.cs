using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App.Views.admin.security.users
{
	public partial class list : Lib.Web.AppControlPage
	{
		public IList<Lib.Data.UserProfile> Items;
		protected void Page_Init(object sender, EventArgs e)
		{
			var uts = new List<Lib.Data.UserType>();
			uts.Add( Lib.Data.UserType.FindByName("admin") );
			uts.Add( Lib.Data.UserType.FindByName("dev") );

			Items = Lib.Data.UserProfile.FindByUserTypes(uts);
		}
	}
}