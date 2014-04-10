using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Web;

namespace Lib.Web
{
	public class AdminPage : BasePage
	{
		protected override void OnInit(EventArgs e)
		{
			base.OnInit( e );

			if( !Framework.Security.Manager.HasRole( "view_admin" ) )
			{
				if (Request.ContentType == "application/json" || Request.ContentType.StartsWith("application/json") || Request.Headers["X-Requested-With"] == "XMLHttpRequest")
				{
					RedirectUrl( "/Login.aspx" );
					Response.End();
				}
				else
				{
					Response.Redirect( "~/Login.aspx", true );
				}
				return;
			}
		}
	}
}
