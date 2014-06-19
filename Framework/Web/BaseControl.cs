using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Web
{
	public class BaseControl : System.Web.UI.UserControl
	{
		protected Framework.Security.User User;

		public BaseControl()
		{
			User = Framework.Security.Manager.GetUser();
		}

		protected void SendJson(API.ReturnObject o)
		{
            Response.Clear();
			Response.ContentType = "application/json";
			Response.StatusCode = o.StatusCode;
			Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(o));
			Response.End();
		}

		protected void RedirectBack(bool error = false, string msg = "")
		{
			SendJson(new API.ReturnObject() { Error = error, Message = msg, Actions = new List<API.ReturnActionObject>() {
				new API.ReturnActionObject() { Type = "back" }
			} });
		}

		protected void RedirectHash(string hash, bool error = false, string msg = "")
		{
			SendJson(new API.ReturnObject() { Error = error, StatusCode = 200, Message = msg, Redirect = new API.ReturnRedirectObject() { Hash = hash } });
		}

		protected void RedirectUrl(string url, bool error = false, string msg = "")
		{
			SendJson(new API.ReturnObject() { Error = error, Message = msg, Redirect = new API.ReturnRedirectObject() { Url = url } });
		}

		protected void SendError(string msg)
		{
			SendJson(new API.ReturnObject() { Error = true, Message = msg });
		}

		protected void RequireRole(string role_name)
		{
			if (!HasRole(role_name))
				RedirectUrl("/Login.aspx", false, "You are not logged in or have insufficient permissions.");
		}

		protected void RequireAnyRole(params string[] roles)
		{
			foreach (var r in roles)
			{
				if (HasRole(r))
					return;
			}

			RedirectUrl("/Login.aspx", false, "You are not logged in or have insufficient permissions.");
		}

		protected void RequireAnyRole(IEnumerable<string> roles)
		{
			foreach( var r in roles )
			{
				if( HasRole(r) )
					return;
			}

			RedirectUrl("/Login.aspx", false, "You are not logged in or have insufficient permissions.");
		}

		protected bool HasRole(string role_name)
		{
			return Framework.Security.Manager.HasRole(role_name);
		}
	}
}
