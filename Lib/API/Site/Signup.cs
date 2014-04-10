using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Web;
using Framework.API;

namespace Lib.API.Site
{
	public class Signup : Base
	{
		[Method("Site/Signup/Prescriber")]
		public static ReturnObject Prescriber(HttpContext context, string username, string password, string email, string fname, string lname, string street1, string city, string state, string zip, string npiid, string company = null, string street2 = null, string phone = null)
		{
			string err;

			var user = Framework.Security.Manager.CreateUser(username, password, email, out err);

			if (!string.IsNullOrEmpty(err))
			{
				return new ReturnObject()
				{
					Error = true,
					StatusCode = 200,
					Message = err
				};
			}

			user.AddGroup(Framework.Security.Group.FindByName("users"));
			user.AddGroup(Framework.Security.Group.FindByName("prescribers"));

			var c = new Lib.Data.Contact();
			c.Email = email;
			c.FirstName = fname;
			c.LastName = lname;
			c.Phone = phone;
			c.Save();

			var a = new Lib.Data.Address();
			a.Street1 = street1;
			a.Street2 = street2;
			a.City = city;
			a.State = state;
			a.Zip = zip;
			a.Country = "United States";
			a.Save();

			var ut = Lib.Data.UserType.FindByName("prescriber");

			var profile = new Lib.Data.UserProfile();
			profile.UserID = user.ID.Value;
			profile.UserTypeID = ut.ID.Value;
			profile.Created = DateTime.Now;
			profile.PrimaryAddressID = a.ID.Value;
			profile.PrimaryContactID = c.ID.Value;
			profile.Save();

			var p = new Lib.Data.Prescriber();
			p.ProfileID = profile.ID.Value;
			p.SpecialityID = 0;
			p.NpiId = npiid;
			p.Save();

			var pp = new Lib.Data.PrescriberProfile();
			pp.AddressID = a.ID.Value;
			pp.ContactID = c.ID.Value;
			pp.Deleted = false;
			pp.Expires = DateTime.Now.AddYears(1);
			pp.PrescriberID = p.ID.Value;
			pp.Save();

			// TODO: Redirect to Step2 to collect credit card info
			var ret = new ReturnObject()
			{
				Error = false,
				StatusCode = 200,
				Message = "",
				Redirect = new ReturnRedirectObject()
				{
					Url = "/Signup-Prescriber-Complete.aspx"
				}
			};

			return ret;
		}

		[Method("Site/Signup/Prescriber_Step2")]
		public static ReturnObject Prescriber_Step2(HttpContext context, string cc_num, string cc_type, string cc_name, int cc_exp_month, int cc_exp_year, string cc_cvv)
		{
			// TODO: PayPal Authorize API call
			
			var ret = new ReturnObject()
			{
				Error = false,
				StatusCode = 200,
				Message = "",
				Redirect = new ReturnRedirectObject()
				{
					Url = "/Signup-Prescriber-Confirm.aspx"
				}
			};

			return ret;
		}

		[Method("Site/Signup/Prescriber_Confirm")]
		public static ReturnObject Prescriber_Confirm(HttpContext context, bool confirm)
		{
			if( !confirm )
			{
				return new ReturnObject()
				{
					Error = false,
					StatusCode = 200,
					Message = "",
					Redirect = new ReturnRedirectObject()
					{
						Url = "/Signup-Prescriber-Step2.aspx"
					}
				};
			}

			// TODO: PayPal Capture API call

			// TODO: Set mail settings in web.config
			/*
			var sb = new System.Text.StringBuilder();

			sb.AppendLine("<html><body>");
			sb.AppendLine("<b>Name</b>: " + lname + ", " + fname + "<br />");
			sb.AppendLine("<b>Username</b>: " + username + "<br />");
			sb.AppendLine("<b>Email</b>: " + email + "<br />");
			sb.AppendLine("<b>Company</b>: " + company + "<br />");
			sb.AppendLine("<b>Address</b>: <br />" + street1 + "<br />" + ((!string.IsNullOrEmpty(street2)) ? street2 + "<br />" : "") + city + ", " + state + " " + zip + "<br />");
			sb.AppendLine("<b>Phone</b>: " + phone + "<br />");
			sb.AppendLine("</body></html>");

			var msg = new System.Net.Mail.MailMessage();
			msg.To.Add("info@remslogic.com");
			msg.IsBodyHtml = true;
			msg.Subject = "Prescriber Signup from REMSLogic Webite";
			msg.Body = sb.ToString();

			var client = new System.Net.Mail.SmtpClient();
			client.Send(msg);

			
			sb = new System.Text.StringBuilder();

			sb.AppendLine("<html><body>");
			sb.AppendLine("Dear " + fname + " " + lname + ",<br /><br />");
			sb.AppendLine("Thank you for your interest and for registering with us.<br /><br />");
			sb.AppendLine("<b>We will contact you as soon as possible.</b><br />This website is in final development stages.<br /><br />");
			sb.AppendLine("</body></html>");

			msg = new System.Net.Mail.MailMessage();
			msg.To.Add(email);
			msg.IsBodyHtml = true;
			msg.Subject = "Thank you for registering with REMSLogic";
			msg.Body = sb.ToString();

			client.Send(msg);
			*/

			var ret = new ReturnObject()
			{
				Error = false,
				StatusCode = 200,
				Message = "",
				Redirect = new ReturnRedirectObject()
				{
					Url = "/Signup-Prescriber-Complete.aspx"
				}
			};

			return ret;
		}

		[Method("Site/Signup/Provider")]
		public static ReturnObject Provider(HttpContext context, string facility_size, string street1, string city, string state, string zip, string fname, string lname, string email, string street2 = null, string title = null, string company = null, string phone = null)
		{
			var a = new Lib.Data.Address();
			a.Street1 = street1;
			a.Street2 = street2;
			a.City = city;
			a.State = state;
			a.Zip = zip;
			a.Country = "United States";
			a.Save();

			var c = new Lib.Data.Contact();
			c.FirstName = fname;
			c.LastName = lname;
			c.Title = title;
			c.Email = email;
			c.Phone = phone;
			c.Save();

			var p = new Lib.Data.Provider();
			p.PrimaryContactID = c.ID.Value;
			p.AddressID = a.ID.Value;
			p.Name = company;
			p.FacilitySize = facility_size;
			p.Created = DateTime.Now;
			p.Save();

			// TODO: Set mail settings in web.config
			/*
			var sb = new System.Text.StringBuilder();

			sb.AppendLine("<html><body>");
			sb.AppendLine("<b>Name</b>: " + lname + ", " + fname + "<br />");
			sb.AppendLine("<b>Title</b>: " + title + "<br />");
			sb.AppendLine("<b>Email</b>: " + email + "<br />");
			sb.AppendLine("<b>Company</b>: " + company + "<br />");
			sb.AppendLine("<b>Address</b>: <br />" + street1 + "<br />" + ((!string.IsNullOrEmpty(street2)) ? street2 + "<br />" : "") + city + ", " + state + " " + zip + "<br />");
			sb.AppendLine("<b>Phone</b>: " + phone + "<br />");
			sb.AppendLine("<b>Facility Size</b>: " + facility_size + "<br />");
			sb.AppendLine("</body></html>");

			var msg = new System.Net.Mail.MailMessage("admin@remslogic.com", "info@remslogic.com");
			msg.IsBodyHtml = true;
			msg.Subject = "Healthcare Provider Signup from REMSLogic Webite";
			msg.Body = sb.ToString();

			var client = new System.Net.Mail.SmtpClient("relay-hosting.secureserver.net");
			client.Send(msg);

			sb = new System.Text.StringBuilder();

			sb.AppendLine("<html><body>");
			sb.AppendLine("Dear " + fname + " " + lname + ",<br /><br />");
			sb.AppendLine("Thank you for your interest and for registering with us.<br /><br />");
			sb.AppendLine("<b>We will contact you soon with more information or to schedule a demonstration.</b><br />This website is in final development stages.<br /><br />");
			sb.AppendLine("</body></html>");

			msg = new System.Net.Mail.MailMessage("info@remslogic.com", email);
			msg.IsBodyHtml = true;
			msg.Subject = "Thank you for contacting REMSLogic";
			msg.Body = sb.ToString();

			client.Send(msg);
			*/

			var ret = new ReturnObject()
			{
				Error = false,
				StatusCode = 200,
				Message = "Message sent successfully",
				Redirect = new ReturnRedirectObject()
				{
					Url = "/Signup-HealthCare-Complete.aspx"
				}
			};

			return ret;
		}
	}
}
