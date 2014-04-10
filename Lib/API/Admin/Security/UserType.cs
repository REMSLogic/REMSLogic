using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Framework.API;

namespace Lib.API.Admin.Security
{
	public class UserType : Base
	{
		[SecurityRole("dev")]
		[Method("Admin/Security/UserType/Edit")]
		public static ReturnObject Edit(HttpContext context, long id, string name, string display_name, bool has_contact = false, bool has_address = false)
		{
			Lib.Data.UserType item = null;
			if (id > 0)
				item = new Lib.Data.UserType(id);
			else
				item = new Lib.Data.UserType();

			item.Name = name;
			item.DisplayName = display_name;
			item.HasContact = has_contact;
			item.HasAddress = has_address;
			item.Save();

			return new ReturnObject() {
				Result = item,
				Redirect = new ReturnRedirectObject() {
					Hash = "dev/user-types/list"
				},
				Growl = new ReturnGrowlObject() {
					Type = "default",
					Vars = new ReturnGrowlVarsObject() {
						text = "You have successfully saved this user type.",
						title = "User Type Saved"
					}
				}
			};
		}

		[SecurityRole("dev")]
		[Method("Admin/Security/UserType/Delete")]
		public static ReturnObject Delete(HttpContext context, long id)
		{
			if (id <= 0)
				return new ReturnObject() { Error = true, Message = "Invalid User Type." };

			var item = new Lib.Data.UserType(id);
			item.Delete();

			return new ReturnObject()
			{
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "You have successfully deleted a user type.",
						title = "User Type Deleted"
					}
				},
				Actions = new List<ReturnActionObject>()
				{
					new ReturnActionObject() {
						Ele = "#user-types-table tr[data-id=\""+id.ToString()+"\"]",
						Type = "remove"
					}
				}
			};
		}
	}
}
