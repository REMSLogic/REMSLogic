using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Framework.API;

namespace Lib.API.Admin.Drugs
{
	public class System : Base
	{
		[SecurityRole("view_admin")]
		[Method("Admin/Drugs/System/Edit")]
		public static ReturnObject Edit(HttpContext context, long id, string name)
		{
			Lib.Data.DrugSystem item = null;
			if (id > 0)
				item = new Lib.Data.DrugSystem(id);
			else
				item = new Lib.Data.DrugSystem();

			item.Name = name;
			item.Save();

			return new ReturnObject() {
				Result = item,
				Redirect = new ReturnRedirectObject() {
					Hash = "admin/drugs/systems/list"
				},
				Growl = new ReturnGrowlObject() {
					Type = "default",
					Vars = new ReturnGrowlVarsObject() {
						text = "You have successfully saved this drug system.",
						title = "Drug System Saved"
					}
				}
			};
		}

		[SecurityRole("view_admin")]
		[Method("Admin/Drugs/System/Delete")]
		public static ReturnObject Delete(HttpContext context, long id)
		{
			if (id <= 0)
				return new ReturnObject() { Error = true, Message = "Invalid Drug System." };

			var item = new Lib.Data.DrugSystem(id);
			item.Delete();

			return new ReturnObject()
			{
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "You have successfully deleted a drug system.",
						title = "Drug System Deleted"
					}
				},
				Actions = new List<ReturnActionObject>()
				{
					new ReturnActionObject() {
						Ele = "#systems-table tr[data-id=\""+id.ToString()+"\"]",
						Type = "remove"
					}
				}
			};
		}
	}
}
