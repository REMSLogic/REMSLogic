using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Framework.Web;

namespace Lib.Web
{
	public class SecurePage : BasePage
	{
		protected override void OnInit(EventArgs e)
		{
			if( !Framework.Security.Manager.IsLoggedIn() )
			{
				Session["ReturnUrl"] = Request.ServerVariables.AllKeys.Contains("HTTP_X_ORIGINAL_URL") ? Request.ServerVariables["HTTP_X_ORIGINAL_URL"] : Request.Url.PathAndQuery;
				Response.Redirect( Framework.Config.Manager.Framework.Security.Authentication.AccessDeniedUrl );
				return;
			}

			base.OnInit( e );
		}

		protected void RequireRole(string role_name)
		{
			if( !Framework.Security.Manager.HasRole( role_name ) )
			{
				Response.Redirect("~/Login.aspx", true);
			}
		}
	}
}
