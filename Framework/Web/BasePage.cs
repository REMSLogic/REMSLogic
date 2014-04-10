using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Web
{
	public class BasePage : System.Web.UI.Page
	{
		protected Security.User WebUser;

		protected override void OnInit(EventArgs e)
		{
			base.OnInit( e );

			WebUser = Security.Manager.GetUser();
		}

		protected void SendJson(API.ReturnObject o)
		{
			Response.ContentType = "application/json";
			Response.StatusCode = 200;
			Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(o));
			Response.End();
		}

		protected void RedirectHash(string hash, bool error = false, string msg = "")
		{
			//if( this.Request.
			SendJson(new API.ReturnObject() { Error = error, Message = msg, Redirect = new API.ReturnRedirectObject() { Hash = hash } });
		}

		protected void RedirectUrl(string url, bool error = false, string msg = "")
		{
			SendJson(new API.ReturnObject() { Error = error, Message = msg, Redirect = new API.ReturnRedirectObject() { Url = url } });
		}
	}
}
