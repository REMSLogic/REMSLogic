using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App.Views.admin.dsq
{
	public partial class view : Lib.Web.AppControlPage
	{
		public Lib.Data.Drug item;
		protected void Page_Init(object sender, EventArgs e)
		{
			int id;
			if( string.IsNullOrEmpty(Request.QueryString["id"]) || !int.TryParse(Request.QueryString["id"], out id ) )
				RedirectHash("admin/drugs/drugs/list");
			else
				item = new Lib.Data.Drug(id);
		}
	}
}