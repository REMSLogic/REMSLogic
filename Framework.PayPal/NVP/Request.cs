using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;

namespace Framework.PayPal.NVP
{
	public class Request
	{
		protected string _method;
		public string Method
		{ get { return _method; } }

		protected string _version;
		public string Version
		{ get { return _version; } }

		protected NameValueCollection _nvc;
		public string this[string name]
		{
			get { return _nvc[name]; }
			set { _nvc[name] = value; }
		}

		public Request(string method, string version)
		{
			_method = method;
			_version = version;
			_nvc = new NameValueCollection();
		}

		public Response Execute()
		{
			var config = Framework.Config.Manager.GetSection<Config.PayPalSection>( "paypal" );
			Config.EnvironmentElement environ = null;
			string url = Framework.Web.Manager.GetUrl( "PayPal" );
			if( !Framework.Web.Manager.IsDev() )
				environ = config.Production;
			else
				environ = config.Sandbox;

			var s = System.Web.HttpContext.Current.Server;

			string qs = "METHOD=" + s.UrlEncode( this._method ) +
						"&VERSION=" + s.UrlEncode( this._version ) +
						"&USER=" + s.UrlEncode( environ.User ) +
						"&PWD=" + s.UrlEncode( environ.Pwd ) +
						"&SIGNATURE=" + s.UrlEncode( environ.Signature );

			foreach( string k in this._nvc.Keys )
				qs += "&" + s.UrlEncode(k) + "=" + s.UrlEncode(this._nvc[k]);

			HttpWebRequest req = (HttpWebRequest)WebRequest.Create( url );
			req.Method = "POST";
			req.ContentType = "application/x-www-form-urlencoded";

			string origRequest = qs;
			string strRequest = origRequest;

			req.ContentLength = strRequest.Length;

			StreamWriter streamOut = new StreamWriter( req.GetRequestStream(), System.Text.Encoding.ASCII );
			streamOut.Write( strRequest );
			streamOut.Close();

			StreamReader streamIn = new StreamReader( req.GetResponse().GetResponseStream() );
			string strResponse = streamIn.ReadToEnd();
			streamIn.Close();

			return CreateResp( strResponse );
		}

		protected virtual Response CreateResp(string resp)
		{
			return new Response( resp );
		}
	}
}
