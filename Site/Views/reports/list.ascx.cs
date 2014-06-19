using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App.Views.reports
{
	public partial class list : Lib.Web.AppControlPage
	{
		public IList<Lib.Data.Report> items;
		protected void Page_Init(object sender, EventArgs e)
		{
			items = Lib.Systems.Reports.GetMyReports();

			if( items.Count <= 0 )
				SendError( "You do not have any available reports." );
		}
	}
}