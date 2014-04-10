using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.Master
{
	public partial class Front : System.Web.UI.MasterPage
	{
		public string pageName;
		protected void Page_Init(object sender, EventArgs e)
		{
			pageName = this.Page.ToString().Substring( 4, this.Page.ToString().Substring( 4 ).Length - 5 ).ToLower();
		}
	}
}