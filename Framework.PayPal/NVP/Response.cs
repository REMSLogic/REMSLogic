using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Framework.PayPal.NVP
{
	public struct Error
	{
		public string ErrorCode;
		public string ShortMessage;
		public string LongMessage;
		public string SeverityCode;
	}

	public class Response
	{
		protected NameValueCollection _nvc;
		public string this[string name]
		{
			get { return _nvc[name]; }
		}

		public string Ack
		{ get { return this["ACK"]; } }
		public string CorrelationID
		{ get { return this["CORRELATIONID"]; } }
		public string Timestamp
		{ get { return this["TIMESTAMP"]; } }
		public string Version
		{ get { return this["VERSION"]; } }
		public string Build
		{ get { return this["BUILD"]; } }

		protected List<Error> _errors;
		public Error[] Errors
		{ get { return _errors.ToArray(); } }

		public string _resp;

		public Response(string resp)
		{
			_resp = resp;
			_nvc = new NameValueCollection();
			string[] parts = resp.Split( '&' );
			for( int i = 0; i < parts.Length; i++ )
			{
				string[] ps = parts[i].Split( '=' );
				string name = System.Web.HttpContext.Current.Server.UrlDecode( ps[0] );
				string value = System.Web.HttpContext.Current.Server.UrlDecode( ps[1] );
				_nvc[name] = value;
			}

			_errors = new List<Error>();
			int index = 0;
			while(true)
			{
				if( _nvc["L_ERRORCODE" + index] == null )
					break;

				var e = new Error() { ErrorCode = _nvc["L_ERRORCODE" + index], ShortMessage = _nvc["L_SHORTMESSAGE" + index], LongMessage = _nvc["L_LONGMESSAGE" + index], SeverityCode = _nvc["L_SEVERITYCODE" + index] };
				_errors.Add( e );

				_nvc.Remove( "L_ERRORCODE" + index );
				_nvc.Remove( "L_SHORTMESSAGE" + index );
				_nvc.Remove( "L_LONGMESSAGE" + index );
				_nvc.Remove( "L_SEVERITYCODE" + index );

				index++;
			}
		}
	}
}
