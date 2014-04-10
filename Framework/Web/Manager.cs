using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Web
{
	public static class Manager
	{
		public static bool IsDev(string host = null)
		{
			string url = host;

			if (string.IsNullOrWhiteSpace(url))
			{
				if (System.Web.HttpContext.Current == null)
					throw new InvalidOperationException("IsDev can only be called from a Thread that is serving a .Net HttpContext.");

				if (System.Web.HttpContext.Current.Application["Framework_Host"] != null && !string.IsNullOrEmpty((string)System.Web.HttpContext.Current.Application["Framework_Host"]))
					url = (string)System.Web.HttpContext.Current.Application["Framework_Host"];
				else if( System.Web.HttpContext.Current.Request != null )
					url = System.Web.HttpContext.Current.Request.Url.Host;
			}

			if( url.StartsWith( "http://" ) )
				url = url.Substring( 7 );
			if( url.StartsWith( "https://" ) )
				url = url.Substring( 8 );
			if( url.StartsWith( "www." ) )
				url = url.Substring( 4 );

			url = url.TrimEnd( '/' );

			return (url.ToLower() == "localhost" || url == Framework.Config.Manager.Framework.Net.Sites.CurrentSite.Dev);
		}

		public static string GetUrl(string SiteName)
		{
			var site = Framework.Config.Manager.Framework.Net.Sites[SiteName];

			if( IsDev() )
				return site.Dev;
			else
				return site.Prod;
		}
	}
}
