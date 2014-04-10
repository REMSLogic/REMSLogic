using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using Framework.API;

namespace Lib.API.Admin.Security
{
	public class DrugCompanyUser : Base
	{
		[SecurityRole("view_admin")]
		[Method("Admin/Security/DrugCompanyUser/Edit")]
		public static ReturnObject Edit(HttpContext context, long id, long parent_id, string username, string password, string email, string first_name, string last_name, string street, string city, string state, string zip, string company = null, string street_2 = null, string phone = null)
		{
			Lib.Data.DrugCompanyUser item = null;
			Lib.Data.DrugCompany parent = new Data.DrugCompany(parent_id);
			Lib.Data.UserProfile profile = null;
			Lib.Data.Contact contact = null;
			Lib.Data.Address address = null;
			Framework.Security.User user = null;

			if (id > 0)
			{
				item = new Lib.Data.DrugCompanyUser(id);
				profile = item.Profile;
				user = profile.User;
				contact = profile.PrimaryContact;
				address = profile.PrimaryAddress;
			}
			else
			{
				item = new Lib.Data.DrugCompanyUser();
				profile = new Data.UserProfile();
				profile.Created = DateTime.Now;
				contact = new Data.Contact();
				address = new Data.Address();

				string error = "";
				user = Framework.Security.Manager.CreateUser(username, password, email, out error);

				user.AddGroup(Framework.Security.Group.FindByName("users"));
				user.AddGroup(Framework.Security.Group.FindByName("drugcompany"));

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

			var ut = Lib.Data.UserType.FindByName("drug-company");

			profile.UserTypeID = ut.ID.Value;
			profile.UserID = user.ID.Value;
			profile.PrimaryAddressID = address.ID.Value;
			profile.PrimaryContactID = contact.ID.Value;
			profile.Save();

			item.ProfileID = profile.ID.Value;
			item.DrugCompanyID = parent.ID.Value;
			item.Save();

			return new ReturnObject()
			{
				Result = item,
				Redirect = new ReturnRedirectObject()
				{
					Hash = "admin/drugs/companies/list"
				},
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "You have successfully saved this drug company user.",
						title = "Drug Company User Saved"
					}
				}
			};
		}

		[SecurityRole("view_admin")]
		[Method("Admin/Security/DrugCompanyUser/Delete")]
		public static ReturnObject Delete(HttpContext context, long id)
		{
			if (id <= 0)
				return new ReturnObject() { Error = true, Message = "Invalid Drug Company." };

			var item = new Lib.Data.DrugCompanyUser(id);
			item.Delete();

			return new ReturnObject()
			{
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "You have successfully deleted a drug company user.",
						title = "Drug Company User deleted"
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
