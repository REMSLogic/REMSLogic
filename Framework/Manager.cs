using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web;

namespace Framework
{
	public class Manager
	{
		public static void Init()
		{
			foreach (var t in FindSubclassesOf(typeof(LoadTimeInitializer)))
			{
				var init = t.GetMethod("Init",(new Type[0]));
				if (init == null)
					continue;

				init.Invoke(null, new object[0]);
			}
		}

		public static Type[] FindSubclassesOf(Type theType)
		{
			var ret = new List<Type>();
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			var thisAss = Assembly.GetAssembly( typeof( Manager ) );

			foreach (var a in assemblies)
			{
				var refs = a.GetReferencedAssemblies();
				bool found = false;

				if (a.GetName().FullName == thisAss.GetName().FullName)
					found = true;
				else
				{
					foreach (var r in refs)
					{
						if (thisAss.GetName().FullName == r.FullName)
						{
							found = true;
							break;
						}
					}
				}
				if (!found)
					continue;

				var types = a.GetTypes();
				foreach (var t in types)
					if( IsSubclassOf( t, theType ) )
						ret.Add( t );
			}

			return ret.ToArray();
		}

		public static bool IsSubclassOf(Type t, Type theBase)
		{
			if( t == null )
				return false;
			else if( t == theBase )
				return false;
			else if( t.BaseType == theBase )
				return true;


			var ints = t.GetInterfaces();
			for( int i = 0; i < ints.Length; i++ )
				if( ints[i] == theBase )
					return true;

			return IsSubclassOf( t.BaseType, theBase );
		}

		public static void HandleError(HttpContext ctx)
		{
			if (ctx.Request.IsLocal || ctx.Request.Url.IsLoopback || ctx.Request.Url.Host == "localhost")
                return;

			var req = ctx.Request;

			if (req == null)
			{
				ctx.ClearError();
				ctx.Response.Redirect("~/Errors/500.aspx?s=1", true);
				return;
			}

			string u = req.Url.PathAndQuery.ToLower();

			if (u.StartsWith("/Errors/"))
			{
				ctx.ClearError();
				ctx.Response.Write("An unrecoverable error has been encountered. We apologize for any inconvience.");
				ctx.Response.End();
				return;
			}

			if (u.StartsWith("/scriptresource.axd"))
			{
				ctx.ClearError();
				ctx.Response.Redirect("~/Errors/500.aspx?s=2", true);
				return;
			}

			Exception ex = ctx.Error;

			LogError(ex,ctx);

			ctx.ClearError();

			if( ctx.Request.ContentType.ToLower().StartsWith( "application/json" ) || (ctx.Request.Headers["X-Requested-With"] == "XMLHttpRequest") )
			{
				ctx.Response.ContentType = "application/json";
				ctx.Response.StatusCode = 200;
				ctx.Response.TrySkipIisCustomErrors = true;
				ctx.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new Framework.API.ReturnObject
				{
					StatusCode = 500,
					Message = ex.ToString(),
					Result = ex
				}));
				ctx.Response.End();
			}
			else
			{
				ctx.Response.ContentType = "application/json";
				ctx.Response.StatusCode = 200;
				ctx.Response.TrySkipIisCustomErrors = true;
				ctx.Response.Write( Newtonsoft.Json.JsonConvert.SerializeObject( new Framework.API.ReturnObject {
					StatusCode = 500,
					Message = ex.ToString(),
					Result = ex
				} ) );
				ctx.Response.Flush();
				ctx.Response.End();
				//ctx.Response.Redirect("~/Errors/500.aspx?s=3", true);
			}
		}

		public static void LogError(Exception ex, HttpContext ctx)
		{
			if (!Config.Manager.Framework.Log.Enabled)
				return;

			var req = ctx.Request;

			var sb = new StringBuilder();
			sb.AppendLine("<h2><span style=\"color:#FF0000;\">ERROR:</span></h2><br />");
			sb.AppendLine("<b>Request Originator:</b>  <a href=\"http://ip-lookup.net/index.php?ip=" + req.UserHostAddress + "\">" + req.UserHostName + "</a><br />");
			sb.AppendLine("<b>Offending URL:</b>  " + req.Url.ToString() + "<br />");
			sb.AppendLine("<b>Referring URL:</b>  " + ((req.UrlReferrer == null) ? "null" : req.UrlReferrer.ToString()) + "<br />");
			sb.AppendLine("<b>Browser:</b>  " + req.UserAgent + "<br />");
			sb.AppendLine("<b>Server:</b> " + ctx.Server.MachineName);

			while (ex != null)
			{
				if (ex.GetType() == typeof(System.Web.HttpUnhandledException))
				{
					ex = ex.InnerException;
					continue;
				}

				sb.AppendLine("<div style=\"border-style:solid; border-width:2px; margin: 20px 0; padding: 20px;\">");
				sb.AppendLine("<h3><span style=\"color:#FF0000;\">Exception:</span></h3>");
				sb.AppendLine("<b>Source:</b>  " + ex.Source);
				sb.AppendLine("<br /><b>Message:</b><br />" + ex.Message);
				sb.AppendLine("<br /><br /><b>Stack trace:</b><br /><pre style=\"font-family:Arial; font-size: 10px;\">" + ex.StackTrace + "</pre>");
				sb.AppendLine("</div>");

				if (ex.InnerException == null || ex.Message.Equals(ex.InnerException.Message))
					break;
				else
					ex = ex.InnerException;
			}

			var sess = ctx.Session;
			if (sess != null && sess.Count > 0)
			{
				sb.AppendLine("<br />");
				sb.AppendLine("<br />");
				sb.AppendLine("<div style=\"border-style:solid; border-width:1px; margin: 20px 0; padding: 20px;\">");
				sb.AppendLine("<h3><span style=\"color:#FF0000;\">Session:</span></h3>");
				sb.AppendLine("<b>ID:</b>  " + sess.SessionID);
				sb.AppendLine("<br /><b>Session Variables:</b><br /><span style=\"font-family:Arial; font-size: 10px;\">");

				for (int i = 0; i < sess.Count; i++)
				{
					sb.AppendLine("<br /><u>" + sess.Keys[i] + ":</u>  ");
					if (sess[i] != null)
						sb.AppendLine(sess[i].ToString() + "  ( " + sess[i].GetType().ToString() + " )");
					else
						sb.AppendLine("NULL");
				}
				sb.AppendLine("</span>");
				sb.AppendLine("</div>");
			}

			var form = req.Form;
			if (form != null && form.Count > 0)
			{
				sb.AppendLine("<br />");
				sb.AppendLine("<br />");
				sb.AppendLine("<div style=\"border-style:solid; border-width:1px; margin: 20px 0; padding: 20px;\">");
				sb.AppendLine("<h3><span style=\"color:#FF0000;\">Form:</span></h3>");
				sb.AppendLine("<br /><b>Values:</b><br /><span style=\"font-family:Arial; font-size: 10px;\">");

				for (int i = 0; i < form.Count; i++)
				{
					sb.AppendLine("<br /><u>" + form.Keys[i] + ":</u>  ");
					if (form[i] != null)
						sb.AppendLine(form[i].ToString() + "  ( " + form[i].GetType().ToString() + " )");
					else
						sb.AppendLine("NULL");
				}
				sb.AppendLine("</span>");
				sb.AppendLine("</div>");
			}

			if (Framework.Config.Manager.Framework.Log.Errors.Email)
			{
				try
				{
					Email.SendEmail( Framework.Config.Manager.Framework.Log.Errors.SendTo, "An Error has occured on " + ctx.Request.Url.Host, sb.ToString() );
				}
				catch( Exception ) { }
			}
		}
	}
}
