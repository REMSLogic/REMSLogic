using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App
{
	public partial class Page : Framework.Web.BasePage
	{
		Control view;
		protected void Page_Init(object sender, EventArgs e)
		{
			string rStr = "^([a-zA-Z0-9\\/\\-_]+)$";
			var r = new Regex( rStr );
			string v = Request.QueryString["v"];

			if( !r.IsMatch( v ) )
			{
				if (Request.ContentType == "application/json" || Request.ContentType.StartsWith("application/json") || Request.Headers["X-Requested-With"] == "XMLHttpRequest")
				{
					SendJson( new Framework.API.ReturnObject()
					{
						Error=true,
						Message="Could not find page "+v
					} );
				}
				else
				{
					Response.StatusCode = 404;
					Response.End();
				}
				return;
			}

			try
			{
				view = Page.LoadControl( "~/Views/" + v + ".ascx" );
			}
			catch( Exception ex )
			{
				SendJson( new Framework.API.ReturnObject {
					Error = true,
					Message = "There was an error loading the requested page.",
					Result = new {
						Message = ex.Message,
						Type = ex.GetType().FullName,
						StackTrace = ex.StackTrace
					}
				} );
				return;
			}

			this.Controls.Add( view );
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			StringBuilder renderedOutput = null;
			StringWriter strWriter = null;
			HtmlTextWriter tWriter = null;

			try
			{
				renderedOutput = new StringBuilder();
				strWriter = new StringWriter( renderedOutput );
				tWriter = new HtmlTextWriter( strWriter );

				view.RenderControl( tWriter );

				tWriter.Flush();
				strWriter.Flush();

				Response.Write( renderedOutput.ToString() );
				Response.Flush();
			}
			catch( Exception ex )
			{
				SendJson( new Framework.API.ReturnObject {
					Error = true,
					Message = "There was an error loading the requested page.",
					Result = new {
						Message = ex.Message,
						Type = ex.GetType().FullName,
						StackTrace = ex.StackTrace
					}
				} );
			}
			finally
			{
				if( tWriter != null )
				{
					tWriter.Close();
					tWriter.Dispose();
					tWriter = null;
				}

				if( strWriter != null )
				{
					strWriter.Close();
					strWriter.Dispose();
					strWriter = null;
				}
			}

			Response.End();
		}
	}
}