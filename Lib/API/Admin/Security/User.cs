using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Framework.API;

namespace Lib.API.AJAX.Admin.Security
{
	public class User
	{
		public struct UserEditReturn
		{
			public string username;
			public string email;
			public string password;
			public string confirm;
			public long id;
		}

		[SecurityRole("manage_users")]
		[Method("Admin/Security/User/Edit")]
		public static ReturnObject Edit(HttpContext context, string username, string email, long user_type, string contact_prefix, string contact_name, string contact_phone, string contact_suffix = "", string contact_title = "", string contact_fax = "", string password = "", string confirm = "", long id = 0)
		{
			if( id == 0 && string.IsNullOrEmpty( password ) )
				return new ReturnObject() { Error = true, Message = "A password is required to create a new user." };

			Framework.Security.User item = null;
			Data.UserProfile profile = null;
			Data.Contact contact = null;
			if (id > 0)
			{
				item = new Framework.Security.User(id);
				profile = Data.UserProfile.FindByUser(item);
				contact = profile.PrimaryContact;
				if( contact == null )
					contact = new Data.Contact();
			}
			else
			{
				if (Framework.Security.Manager.UserExists(email, username))
					return new ReturnObject() { Error = true, Message = "A user with that username / email is already in the system." };

				item = new Framework.Security.User();
				item.ResetPasswordGuid = Guid.Empty;
				item.LastLogin = DateTime.Now;

				profile = new Data.UserProfile();
				profile.Created = DateTime.Now;
				contact = new Data.Contact();
			}
			item.Username = username;
			item.Email = email;
			if( !string.IsNullOrEmpty( password ) )
			{
				if( password != confirm )
					return new ReturnObject() { Error = true, Message = "Your passwords do not match." };

				item.PasswordSalt = Framework.Security.Manager.GetRandomSalt();
				item.Password = Framework.Security.Hash.GetSHA512(password+item.PasswordSalt);
			}

			var name_parts = contact_name.Split(' ');

			if (name_parts.Length <= 1)
				return new ReturnObject() { Error = true, Message = "Please enter the contact's full name." };

			item.Save();

			string fname = name_parts[0];
			string lname = name_parts[name_parts.Length - 1];
			for (var i = 1; i < name_parts.Length - 1; i++)
				fname += " " + name_parts[i];

			contact.Prefix = contact_prefix;
			contact.FirstName = fname;
			contact.LastName = lname;
			contact.Postfix = contact_suffix;
			contact.Title = contact_title;
			contact.Email = email;
			contact.Phone = contact_phone;
			contact.Fax = contact_fax;
			contact.Save();

			profile.UserID = item.ID.Value;
			if (profile.UserTypeID != user_type)
			{
				profile.UserTypeID = user_type;

				item.ClearGroups();

				item.AddGroup(Framework.Security.Group.FindByName("users"));
				item.AddGroup(Framework.Security.Group.FindByName("admin"));

				if( user_type == 1 )
					item.AddGroup(Framework.Security.Group.FindByName("dev"));
			}
			profile.PrimaryContactID = contact.ID;
			profile.Save();

			return new ReturnObject() { Result = item, Redirect = new ReturnRedirectObject() { Hash = "admin/security/users/list" }, Growl = new ReturnGrowlObject() { Type = "default", Vars = new ReturnGrowlVarsObject() { text = "You have successfully saved this user.", title = "User Saved" } } };
		}

		[SecurityRole("manage_users")]
		[Method( "Admin/Security/User/Delete" )]
		public static ReturnObject Delete(HttpContext context, long id)
		{
			if( id <= 0 )
				return new ReturnObject() { Error = true, Message = "Invalid User." };

			var item = new Framework.Security.User( id );

			var profile = Data.UserProfile.FindByUser(item);
			if (profile != null)
			{
				var contact = profile.PrimaryContact;
				if (contact != null)
					contact.Delete();

				profile.Delete();
			}

			item.Delete();
			
			return new ReturnObject() {
				Growl = new ReturnGrowlObject() {
					Type = "default",
					Vars = new ReturnGrowlVarsObject() {
						text = "You have successfully deleted a user.",
						title = "User Deleted"
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
