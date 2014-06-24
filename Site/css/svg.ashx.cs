using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.App.css
{
	/// <summary>
	/// Summary description for svg
	/// </summary>
	public class svg : IHttpHandler
	{

		public void ProcessRequest(HttpContext context)
		{
			context.Response.ContentType = "image/svg+xml";

			context.Response.Write( "<svg xmlns=\"http://www.w3.org/2000/svg\" version=\"1.1\" width=\"100%\" height=\"100%\">\n" );
			context.Response.Write( "\n" );
			context.Response.Write( "\t<defs>\n" );
			context.Response.Write( "\t\t<linearGradient id=\"linear-gradient\" x1=\"0%\" y1=\"0%\" x2=\"0%\" y2=\"100%\">\n" );

			string[] stops = context.Request.QueryString["stops"].Split( ',' );
			foreach( var stop in stops )
			{
				string[] parts = stop.Split( ' ' );
				string color = parts[0];
				string offset = parts[1];

				context.Response.Write( "\t\t\t<stop offset=\""+offset+"\" stop-color=\""+color+"\" stop-opacity=\"1\"/>\n" );
			}

			context.Response.Write( "\t\t</linearGradient>\n" );
			context.Response.Write( "\t</defs>\n" );
			context.Response.Write( "\t<rect width=\"100%\" height=\"100%\" fill=\"url(#linear-gradient)\"/>\n" );
			context.Response.Write( "</svg>\n" );
		}

		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
	}
}