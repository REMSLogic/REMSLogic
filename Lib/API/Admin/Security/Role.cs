using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Framework.API;

namespace Lib.API.Admin.Security
{
	public class Role : Base
	{
		[SecurityRole("dev")]
		[Method("Admin/Security/Role/Edit")]
		public static ReturnObject Edit(HttpContext context, long id, string name, string display_name)
		{
			Framework.Security.Role item = null;
			if (id > 0)
				item = new Framework.Security.Role(id);
			else
				item = new Framework.Security.Role();

			item.Name = name;
			item.DisplayName = display_name;
			item.Save();

			return new ReturnObject() {
				Result = item,
				Redirect = new ReturnRedirectObject() {
					Hash = "dev/roles/list"
				},
				Growl = new ReturnGrowlObject() {
					Type = "default",
					Vars = new ReturnGrowlVarsObject() {
						text = "You have successfully saved this role.",
						title = "Role Saved"
					}
				}
			};
		}

		[SecurityRole("dev")]
		[Method("Admin/Security/Role/Delete")]
		public static ReturnObject Delete(HttpContext context, long id)
		{
			if (id <= 0)
				return new ReturnObject() { Error = true, Message = "Invalid Role." };

			var item = new Framework.Security.Role(id);
			item.Delete();

			return new ReturnObject()
			{
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "You have successfully deleted a role.",
						title = "Role Deleted"
					}
				},
				Actions = new List<ReturnActionObject>()
				{
					new ReturnActionObject() {
						Ele = "#roles-table tr[data-id=\""+id.ToString()+"\"]",
						Type = "remove"
					}
				}
			};
		}
	}
}
