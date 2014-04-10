using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Framework.API;

namespace Lib.API.Admin.Drugs
{
	public class Company : Base
	{
		[SecurityRole("view_admin")]
		[Method("Admin/Drugs/Company/Edit")]
		public static ReturnObject Edit(HttpContext context, string name, string website = "", string phone = "", long? id = null)
		{
			Lib.Data.DrugCompany item = null;
			if (id > 0)
				item = new Lib.Data.DrugCompany(id);
			else
				item = new Lib.Data.DrugCompany();

			item.Name = name;
			item.Website = website;
			item.Phone = phone;
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
						text = "You have successfully saved this drug company.",
						title = "Drug Company Saved"
					}
				}
			};
		}

		[SecurityRole("view_admin")]
		[Method("Admin/Drugs/Company/Delete")]
		public static ReturnObject Delete(HttpContext context, long id)
		{
			if (id <= 0)
				return new ReturnObject() { Error = true, Message = "Invalid Drug Company." };

			var item = new Lib.Data.DrugCompany(id);
			item.Delete();

			return new ReturnObject()
			{
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "You have successfully deleted a drug company.",
						title = "Drug Company Deleted"
					}
				},
				Actions = new List<ReturnActionObject>()
				{
					new ReturnActionObject() {
						Ele = "#companies-table tr[data-id=\""+id.ToString()+"\"]",
						Type = "remove"
					}
				}
			};
		}
	}
}
