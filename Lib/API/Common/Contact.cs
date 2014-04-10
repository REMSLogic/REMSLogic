using System.Collections.Generic;
using System.Web;
using Framework.API;

namespace Lib.API.Common
{
	public class Contact : Base
	{
		[SecurityRole("view_admin")]
		[Method("Common/Contact/Edit")]
		public static ReturnObject Edit(HttpContext context, long id, string back_hash, string first_name, string last_name, string phone, string email, string title = null, string prefix = null, string postfix = null, string fax = null)
		{
			Lib.Data.Contact item = null;
			if (id > 0)
				item = new Lib.Data.Contact(id);
			else
				item = new Lib.Data.Contact();

			item.Prefix = prefix;
			item.FirstName = first_name;
			item.LastName = last_name;
			item.Postfix = postfix;
			item.Title = title;
			item.Email = email;
			item.Phone = phone;
			item.Fax = fax;
			item.Save();

			return new ReturnObject()
			{
				Result = item,
				Redirect = new ReturnRedirectObject()
				{
					Hash = back_hash
				},
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "You have successfully saved this contact.",
						title = "Contact Saved"
					}
				}
			};
		}

		[Method("Common/Contact/EditPrimary")]
		public static ReturnObject EditPrimary(HttpContext context, long id, string first_name, string last_name, string phone, string fax)
		{
			Data.Contact item = new Data.Contact(id);

			item.FirstName = first_name;
			item.LastName = last_name;
			item.Phone = phone;
			item.Fax = fax;
			item.Save();

			return new ReturnObject()
			{
				Result = item,
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "You have successfully updated your primary contact.",
						title = "Contact Updated"
					}
				}
			};
		}

		[SecurityRole("view_admin")]
		[Method("Common/Contact/Delete")]
		public static ReturnObject Delete(HttpContext context, long id)
		{
			if (id <= 0)
				return new ReturnObject() { Error = true, Message = "Invalid Contact." };

			var item = new Lib.Data.Contact(id);
			item.Delete();

			return new ReturnObject()
			{
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "You have successfully deleted a contact.",
						title = "Contact Deleted"
					}
				},
				Actions = new List<ReturnActionObject>()
				{
					new ReturnActionObject() {
						Ele = "#contacts-table tr[data-id=\""+id.ToString()+"\"]",
						Type = "remove"
					}
				}
			};
		}
	}
}
