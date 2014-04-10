using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App.Views.dev.reports
{
	public partial class edit : Lib.Web.AppControlPage
	{
		public Lib.Data.Report item;

		protected void Page_Init(object sender, EventArgs e)
		{
			string strID = Request.QueryString["id"];
			long id;
			if (string.IsNullOrEmpty(strID) || !long.TryParse(strID, out id))
				item = new Lib.Data.Report();
			else
				item = new Lib.Data.Report(id);
		}
	}
}