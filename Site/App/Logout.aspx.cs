using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App
{
	public partial class Logout : System.Web.UI.Page
	{
		protected void Page_Init(object sender, EventArgs e)
		{
			Framework.Security.Manager.Logout();
			
			Response.Cookies["app_login"].Expires = DateTime.Now.AddYears(-30);
			Response.Redirect( "~/App/Login.aspx", true );
		}
	}
}