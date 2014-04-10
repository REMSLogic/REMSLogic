using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using Framework.API;

namespace Lib.API.Admin
{
	public class Language : Base
	{
		[SecurityRole("view_dev")]
		[Method("Admin/Language/Edit")]
		public static ReturnObject Edit(HttpContext context, string name, string code, string enabled_icon, string disabled_icon, long? id = null)
		{
			var item = new Lib.Data.Language(id);
			item.Name = name;
			item.Code = code;
			item.EnabledIcon = enabled_icon;
			item.DisabledIcon = disabled_icon;
			item.Save();

			return new ReturnObject() { Result = item, Redirect = new ReturnRedirectObject() { Hash = "dev/langs/list" }, Growl = new ReturnGrowlObject() { Type = "default", Vars = new ReturnGrowlVarsObject() { text = "You have successfully saved this language.", title = "Language Saved" } } };
		}

		[SecurityRole("view_dev")]
		[Method("Admin/Language/Delete")]
		public static ReturnObject Delete(HttpContext context, long id)
		{
			if (id <= 0)
				return new ReturnObject() { Error = true, Message = "Invalid Language." };

			var item = new Lib.Data.Language(id);
			item.Delete();

			return new ReturnObject()
			{
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "You have successfully deleted a language.",
						title = "Language Deleted"
					}
				},
				Actions = new List<ReturnActionObject>()
				{
					new ReturnActionObject() {
						Ele = "#langs-table tr[data-id=\""+id.ToString()+"\"]",
						Type = "remove"
					}
				}
			};
		}
	}
}
