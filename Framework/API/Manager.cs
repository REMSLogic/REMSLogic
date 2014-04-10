using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace Framework.API
{
	public class Manager : IHttpHandler, IRequiresSessionState, Framework.LoadTimeInitializer
	{
		protected static Dictionary<string, API.MethodInfo> apiMethods;

		static Manager()
		{
			Init();
		}

		public static void Init()
		{
			if( apiMethods != null )
				return;

			apiMethods = new Dictionary<string, MethodInfo>();

			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			var thisAss = Assembly.GetAssembly( typeof( Manager ) );

			foreach( var a in assemblies )
			{
				var refs = a.GetReferencedAssemblies();
				bool found = false;

				if( a.GetName().FullName == thisAss.GetName().FullName )
					found = true;
				else
				{
					foreach( var r in refs )
					{
						if( thisAss.GetName().FullName == r.FullName )
						{
							found = true;
							break;
						}
					}
				}
				if( !found )
					continue;

				var types = a.GetTypes();
				foreach( var t in types )
				{
					var methods = t.GetMethods( BindingFlags.Static | BindingFlags.Public );
					foreach( var m in methods )
					{
						var attrs = m.GetCustomAttributes( typeof( MethodAttribute ), false );
						if( attrs == null || attrs.Length <= 0 )
							continue;

						var ma = (MethodAttribute)attrs[0];
						apiMethods.Add( ma.Path.ToLower(), new API.MethodInfo(m) );
					}
				}
			}
		}

		public void ProcessRequest(HttpContext context)
		{
			var p = context.Request.QueryString["p"].ToLower();

			context.Response.ContentType = "application/json";

			if( !apiMethods.ContainsKey(p) )
			{
				context.Response.StatusCode = 404;
				context.Response.TrySkipIisCustomErrors = true;
				context.Response.Write( Newtonsoft.Json.JsonConvert.SerializeObject( new ReturnObject() { StatusCode = 404, Message = "API Url not found.", Result = null } ) );
				context.Response.End();
			}

			var method = apiMethods[p];
			var ret = method.Run( context );

			if( ret.StatusCode <= 0 )
				ret.StatusCode = 200;

			context.Response.StatusCode = 200;
			context.Response.TrySkipIisCustomErrors = true;

			context.Response.Write( Newtonsoft.Json.JsonConvert.SerializeObject( ret ) );
		}

		public bool IsReusable { get { return true; } }
	}
}
