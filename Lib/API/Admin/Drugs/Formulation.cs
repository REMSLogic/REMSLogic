using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Framework.API;

namespace Lib.API.Admin.Drugs
{
	public class Formulation : Base
	{
		[SecurityRole("view_admin")]
		[Method("Admin/Drugs/Formulation/Edit")]
		public static ReturnObject Edit(HttpContext context, string name, long? id = null)
		{
			Lib.Data.Formulation item = null;
			if (id > 0)
				item = new Lib.Data.Formulation(id);
			else
				item = new Lib.Data.Formulation();

			item.Name = name;
			item.Save();

			return new ReturnObject()
			{
				Result = item,
				Redirect = new ReturnRedirectObject()
				{
					Hash = "admin/drugs/formulations/list"
				},
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "You have successfully saved this formulation.",
						title = "Formulation Saved"
					}
				}
			};
		}

		[SecurityRole("view_admin")]
		[Method("Admin/Drugs/Formulation/Delete")]
		public static ReturnObject Delete(HttpContext context, long id)
		{
			if (id <= 0)
				return new ReturnObject() { Error = true, Message = "Invalid Formulation." };

			var item = new Lib.Data.Formulation(id);
			item.Delete();

			return new ReturnObject()
			{
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "You have successfully deleted a formulation.",
						title = "Formulation Deleted"
					}
				},
				Actions = new List<ReturnActionObject>()
				{
					new ReturnActionObject() {
						Ele = "#formulations-table tr[data-id=\""+id.ToString()+"\"]",
						Type = "remove"
					}
				}
			};
		}
	}
}
