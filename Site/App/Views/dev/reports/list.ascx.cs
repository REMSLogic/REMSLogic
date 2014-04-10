using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App.Views.dev.reports
{
	public partial class list : System.Web.UI.UserControl
	{
		public IList<Lib.Data.Report> Items;
		protected void Page_Init(object sender, EventArgs e)
		{
			Items = Lib.Data.Report.FindAll();
		}
	}
}