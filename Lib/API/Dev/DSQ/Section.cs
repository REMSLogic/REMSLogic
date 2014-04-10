using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using Framework.API;

namespace Lib.API.Dev.DSQ
{
	public class Section : Base
	{
		[SecurityRole("view_dev")]
		[Method("Dev/DSQ/Section/Edit")]
		public static ReturnObject Edit(HttpContext context, string name, int order, long? id = null)
		{
			var item = new Lib.Data.DSQ.Section(id);
			item.Name = name;
			item.Order = order;
			item.Save();

			return new ReturnObject() { Result = item, Redirect = new ReturnRedirectObject() { Hash = "dev/dsq/sections/list" }, Growl = new ReturnGrowlObject() { Type = "default", Vars = new ReturnGrowlVarsObject() { text = "You have successfully saved this section.", title = "Section Saved" } } };
		}

		[SecurityRole("view_dev")]
		[Method("Dev/DSQ/Section/Delete")]
		public static ReturnObject Delete(HttpContext context, long id)
		{
			if (id <= 0 || id == 1) // id == 1 is to protect the General info section which is special
				return new ReturnObject() { Error = true, Message = "Invalid Section." };

			var item = new Lib.Data.DSQ.Section(id);
			item.Delete();

			return new ReturnObject()
			{
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "You have successfully deleted a section.",
						title = "Section Deleted"
					}
				},
				Actions = new List<ReturnActionObject>()
				{
					new ReturnActionObject() {
						Ele = "#sections-table tr[data-id=\""+id.ToString()+"\"]",
						Type = "remove"
					}
				}
			};
		}
	}
}
