using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App.Views.reports
{
	public partial class view : Lib.Web.AppControlPage
	{
		public Lib.Data.Report item;
		public IList<Lib.Data.ReportOutput> outputs;

		protected void Page_Init( object sender, EventArgs e )
		{
			string strID = Request.QueryString["id"];
			long id;
			if( string.IsNullOrEmpty( strID ) || !long.TryParse( strID, out id ) )
			{
				SendError( "Invalid Report Requested." );
				return;
			}
			
			item = new Lib.Data.Report( id );
			outputs = item.GetOutputs();
		}
	}
}