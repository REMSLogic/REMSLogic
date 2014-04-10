using System;
using System.Collections.Generic;
using System.Security;
using System.Web;
using Framework.API;
using Framework.Data;

namespace Lib.API.Dev
{
	public class Reports : Base
	{
		[SecurityRole("view_dev")]
		[Method("Dev/Reports/Edit")]
		public static ReturnObject Edit(HttpContext context, string name, string roles, long? id = null)
		{
			Data.Report item;
			if( id == null || id <= 0 )
				item = new Data.Report();
			else
				item = new Data.Report(id);

			item.Name = name;
			item.ForRoles = roles;
			item.Save();

			return new ReturnObject
			{
				Result = item,
				Growl = new ReturnGrowlObject
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject
					{
						text = "Report '"+item.Name+"' has been saved.",
						title = "Report saved"
					}
				},
				Redirect = new ReturnRedirectObject {
					Hash = "dev/reports/edit?id="+item.ID.Value.ToString()
				}
			};
		}

		[SecurityRole("view_dev")]
		[Method("Dev/Reports/Delete")]
		public static ReturnObject Delete(HttpContext context, long id)
		{
			if (id <= 0)
				return new ReturnObject() { Error = true, Message = "Invalid Report." };

			var item = new Lib.Data.Report(id);
			item.Delete();

			return new ReturnObject()
			{
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "You have successfully deleted this report.",
						title = "Report deleted"
					}
				},
				Actions = new List<ReturnActionObject>()
				{
					new ReturnActionObject() {
						Ele = "#reports-table tr[data-id=\""+id.ToString()+"\"]",
						Type = "remove"
					}
				}
			};
		}
	}
}
