using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Framework.API;

namespace Lib.API.Admin.Security
{
	public class Group : Base
	{
		[SecurityRole("dev")]
		[Method("Admin/Security/Group/Edit")]
		public static ReturnObject Edit(HttpContext context, long id, string name, string display_name)
		{
			Framework.Security.Group item = null;
			if (id > 0)
				item = new Framework.Security.Group(id);
			else
				item = new Framework.Security.Group();

			item.Name = name;
			item.DisplayName = display_name;
			item.Save();

			return new ReturnObject() {
				Result = item,
				Redirect = new ReturnRedirectObject() {
					Hash = "admin/security/groups/list"
				},
				Growl = new ReturnGrowlObject() {
					Type = "default",
					Vars = new ReturnGrowlVarsObject() {
						text = "You have successfully saved this group.",
						title = "Group Saved"
					}
				}
			};
		}

		[SecurityRole("dev")]
		[Method("Admin/Security/Group/Delete")]
		public static ReturnObject Delete(HttpContext context, long id)
		{
			if (id <= 0)
				return new ReturnObject() { Error = true, Message = "Invalid Group." };

			var item = new Framework.Security.Group(id);
			item.Delete();

			return new ReturnObject()
			{
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "You have successfully deleted a group.",
						title = "Group Deleted"
					}
				},
				Actions = new List<ReturnActionObject>()
				{
					new ReturnActionObject() {
						Ele = "#groups-table tr[data-id=\""+id.ToString()+"\"]",
						Type = "remove"
					}
				}
			};
		}
	}
}
