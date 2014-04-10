using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace Framework.PayPal
{
	public class IPN : IHttpHandler
	{
		public void ProcessRequest(HttpContext context)
		{
			var config = Framework.Config.Manager.GetSection<Framework.PayPal.Config.PayPalSection>("paypal");
			Framework.PayPal.Config.EnvironmentElement environ = null;
			string url = Framework.Web.Manager.GetUrl("PayPal");
			if (!Framework.Web.Manager.IsDev())
				environ = config.Production;
			else
				environ = config.Sandbox;

			HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
			req.Method = "POST";
			req.ContentType = "application/x-www-form-urlencoded";

			byte[] param = context.Request.BinaryRead(context.Request.ContentLength);
			string origRequest = Encoding.ASCII.GetString(param);
			string strRequest = origRequest + "&cmd=_notify-validate";

			req.ContentLength = strRequest.Length;

			StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), System.Text.Encoding.ASCII);
			streamOut.Write(strRequest);
			streamOut.Close();

			StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
			string strResponse = streamIn.ReadToEnd();
			streamIn.Close();

			if (strResponse == "VERIFIED")
				ProcessVerifiedMessage(origRequest);
			else if (strResponse == "INVALID")
				ProcessInvalidMessage(origRequest);
			else
				ProcessUnknownMessage(origRequest);
		}

		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		public void ProcessVerifiedMessage(string req)
		{
			//check the payment_status is Completed
			//check that txn_id has not been previously processed
			//check that receiver_email is your Primary PayPal email
			//check that payment_amount/payment_currency are correct
			//process payment
		}

		public void ProcessInvalidMessage(string req)
		{
			//log for manual investigation
		}

		public void ProcessUnknownMessage(string req)
		{
			//log response/ipn data for manual investigation
		}
	}
}
