using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Web;
using Framework.API;

namespace Lib.API.Site
{
	public class ContactUs : Base
	{
		[Method( "Site/ContactUs/Submit" )]
		public static ReturnObject Submit(HttpContext context, string name, string email, string subject, string message)
		{
			var sb = new System.Text.StringBuilder();

			sb.AppendLine("<html><body>");
			sb.AppendLine("<b>Name</b>: " + name + "<br />");
			sb.AppendLine("<b>Email</b>: " + email + "<br />");
			sb.AppendLine("<b>Subject</b>: " + subject + "<br />");
			sb.AppendLine("<b>Message</b>:<br />" + message + "<br />");
			sb.AppendLine("</body></html>");

			//var msg = new System.Net.Mail.MailMessage("admin@remslogic.com", "info@remslogic.com");
            var msg = new System.Net.Mail.MailMessage("admin@remslogic.com", "mike.lindegarde@gmail.com");
            //var msg = new System.Net.Mail.MailMessage("no_reply@remslogic.com", "mike.lindegarde@gmail.com");
			msg.IsBodyHtml = true;
			msg.Subject = "Contact Message from REMSLogic Webite";
			msg.Body = sb.ToString();

			var client = new System.Net.Mail.SmtpClient("relay-hosting.secureserver.net");
            //var client = new System.Net.Mail.SmtpClient("smtpout.secureserver.net");
            //client.Credentials = new System.Net.NetworkCredential("no_reply@remslogic.com", "Safety1");
            //client.EnableSsl = false;
			client.Send(msg);

			sb = new System.Text.StringBuilder();

			sb.AppendLine("<html><body>");
			sb.AppendLine("Dear " + name + ",<br /><br />");
			sb.AppendLine("Thank you for your interest and for contacting us.<br /><br />");
			sb.AppendLine("<b>We will respond as soon as possible.</b><br />This website is in final development stages.<br /><br />");
			sb.AppendLine("</body></html>");

			msg = new System.Net.Mail.MailMessage("info@remslogic.com", email);
			msg.IsBodyHtml = true;
			msg.Subject = "Thank you for contacting REMSLogic";
			msg.Body = sb.ToString();

			client.Send(msg);

			var ret = new ReturnObject()
			{
				Error = false,
				StatusCode = 200,
				Message = "Message sent successfully"
			};

			return ret;
		}
	}
}
