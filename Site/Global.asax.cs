using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Site
{
	public class Global : System.Web.HttpApplication
	{
		protected void Application_Start(object sender, EventArgs e)
		{
			Lib.Manager.Init();
			Framework.Manager.Init();
		}

		protected void Application_Error(object sender, EventArgs e)
		{
			Framework.Manager.HandleError(HttpContext.Current);
		}

		protected void Application_BeginRequest(object sender, EventArgs e)
		{
			HttpContext.Current.Request.Headers["DBUNIQUEID"] = Guid.NewGuid().ToString();
		}

		protected void Application_EndRequest( object sender, EventArgs e )
		{
			Framework.Data.Database.CloseConnections( HttpContext.Current.Request.Headers["DBUNIQUEID"] );
		}
	}
}