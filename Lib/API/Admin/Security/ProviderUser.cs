using System;
using System.Collections.Generic;
using System.Web;
using Framework.API;

namespace Lib.API.Admin.Security
{
	public class ProviderUser : Base
	{
		[SecurityRole("view_admin")]
		[Method("Admin/Security/ProviderUser/Edit")]
		public static ReturnObject Edit(HttpContext context, long id, long parent_id, string user_type, string username, string password, string email, string first_name, string last_name, string street, string city, string state, string zip, string company = null, string street_2 = null, string phone = null)
		{
			Lib.Data.ProviderUser item = null;
			Lib.Data.Provider provider = new Data.Provider(parent_id);
			Lib.Data.UserProfile profile = null;
			Lib.Data.Contact contact = null;
			Lib.Data.Address address = null;
			Framework.Security.User user = null;

			if (id > 0)
			{
				item = new Lib.Data.ProviderUser(id);
				profile = item.Profile;
				user = profile.User;
				contact = profile.PrimaryContact;
				address = profile.PrimaryAddress;
			}
			else
			{
				item = new Lib.Data.ProviderUser();
				profile = new Data.UserProfile();
				profile.Created = DateTime.Now;
				contact = new Data.Contact();
				address = new Data.Address();

				string error = "";
				user = Framework.Security.Manager.CreateUser(username, password, email, out error);

				user.AddGroup(Framework.Security.Group.FindByName("users"));
				user.AddGroup(Framework.Security.Group.FindByName("providers"));

				if (!string.IsNullOrEmpty(error))
				{
					return new ReturnObject()
					{
						Error = true,
						StatusCode = 200,
						Message = error
					};
				}
			}

			if (user_type != "technical" && user_type != "administrative")
			{
				return new ReturnObject()
				{
					Error = true,
					StatusCode = 200,
					Message = "Invalid user type."
				};
			}

			address.Street1 = street;
			address.Street2 = street_2;
			address.City = city;
			address.State = state;
			address.Zip = zip;
			address.Country = "United States";
			address.Save();

			contact.Email = email;
			contact.FirstName = first_name;
			contact.LastName = last_name;
			contact.Phone = phone;
			contact.Save();

			var ut = Lib.Data.UserType.FindByName("provider");

			profile.UserTypeID = ut.ID.Value;
			profile.UserID = user.ID.Value;
			profile.PrimaryAddressID = address.ID.Value;
			profile.PrimaryContactID = contact.ID.Value;
			profile.Save();

			item.ProfileID = profile.ID.Value;
			item.ProviderID = provider.ID.Value;
			item.ProviderUserType = user_type;
			item.Save();

			return new ReturnObject()
			{
				Result = item,
				Redirect = new ReturnRedirectObject()
				{
					Hash = "admin/security/providers/list"
				},
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "You have successfully saved this provider user.",
						title = "Provider User Saved"
					}
				}
			};
		}

		[SecurityRole("view_admin")]
		[Method("Admin/Security/ProviderUser/Delete")]
		public static ReturnObject Delete(HttpContext context, long id)
		{
			if (id <= 0)
				return new ReturnObject() { Error = true, Message = "Invalid Provider." };

			var item = new Lib.Data.ProviderUser(id);
			item.Delete();

			return new ReturnObject()
			{
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "You have successfully deleted a provider user.",
						title = "Provider User deleted"
					}
				},
				Actions = new List<ReturnActionObject>()
				{
					new ReturnActionObject() {
						Ele = "#users-table tr[data-id=\""+id.ToString()+"\"]",
						Type = "remove"
					}
				}
			};
		}
	}
}
