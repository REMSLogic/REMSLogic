using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using Framework.API;

namespace Lib.API.Admin.Security
{
	public class Prescriber : Base
	{
		[SecurityRole("view_admin")]
		[Method("Admin/Security/Prescriber/Edit")]
		public static ReturnObject Edit(HttpContext context, long id, string first_name, string last_name, string email, string phone, string npiid, string username, string street, string city, string state, string zip, string street_2 = "", string password = "", string confirm_password = "")
		{
			Lib.Data.UserProfile profile = null;
			Lib.Data.Prescriber item = null;
			Lib.Data.Address address = null;
			Lib.Data.Contact contact = null;
			Framework.Security.User user = null;

			if (id > 0)
			{
				item = new Lib.Data.Prescriber(id);
				profile = item.Profile;
				user = profile.User;
				address = profile.PrimaryAddress;
				contact = profile.PrimaryContact;
			}
			else
			{
				profile = new Lib.Data.UserProfile();
				profile.Created = DateTime.Now;
				item = new Lib.Data.Prescriber();
				contact = new Lib.Data.Contact();
				user = new Framework.Security.User();
				address = new Lib.Data.Address();
			}

			if (!user.ID.HasValue && string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirm_password))
			{
				return new ReturnObject()
				{
					Error = true,
					StatusCode = 200,
					Message = "If you are creating a new prescriber, you must enter a password."
				};
			}

			if (!string.IsNullOrEmpty(password) )
			{
				if (password != confirm_password)
				{
					return new ReturnObject()
					{
						Error = true,
						StatusCode = 200,
						Message = "The passwords you entered do no match."
					};
				}
				else
				{
					user.PasswordSalt = Framework.Security.Manager.GetRandomSalt();
					user.Password = Framework.Security.Hash.GetSHA512(password + user.PasswordSalt);
				}
			}

			user.Username = username;
			user.Email = email;
			user.Save();

			profile.UserID = user.ID.Value;
			//TODO: FIXME
			profile.UserTypeID = 0;
			
			contact.Email = email;
			contact.Phone = phone;
			contact.FirstName = first_name;
			contact.LastName = last_name;
			contact.Save();

			address.Street1 = street;
			address.Street2 = street_2;
			address.City = city;
			address.State = state;
			address.Zip = zip;
			address.Country = "United States";
			address.Save();

			profile.UserID = user.ID.Value;
			profile.PrimaryAddressID = address.ID.Value;
			profile.PrimaryContactID = contact.ID.Value;
			profile.Save();

			item.ProfileID = profile.ID.Value;
			//TODO: FIXME
			item.SpecialityID = 0;
			item.NpiId = npiid;
			item.Save();

			return new ReturnObject()
			{
				Result = item,
				Redirect = new ReturnRedirectObject()
				{
					Hash = "admin/prescribers/list"
				},
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "You have successfully saved this Prescriber.",
						title = "Prescriber Saved"
					}
				}
			};
		}

		[SecurityRole("view_admin")]
		[Method("Admin/Security/Prescriber/Delete")]
		public static ReturnObject Delete(HttpContext context, long id)
		{
			if (id <= 0)
				return new ReturnObject() { Error = true, Message = "Invalid Prescriber." };

			var item = new Lib.Data.Prescriber(id);
			item.Delete();

			return new ReturnObject()
			{
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "You have successfully deleted a Prescriber.",
						title = "Prescriber deleted"
					}
				},
				Actions = new List<ReturnActionObject>()
				{
					new ReturnActionObject() {
						Ele = "#prescribers-table tr[data-id=\""+id.ToString()+"\"]",
						Type = "remove"
					}
				}
			};
		}
	}
}
