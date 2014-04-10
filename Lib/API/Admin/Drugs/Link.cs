using System;
using System.Collections.Generic;
using System.Web;
using Framework.API;
using Framework.Data;

namespace Lib.API.Admin.Drugs
{
	public class Link : Base
	{
		[SecurityRole("view_admin")]
		[Method("Admin/Drugs/Link/Edit")]
		public static ReturnObject Edit(HttpContext context, long id, long drug_id, string type, string text, DateTime date, string link=null)
		{
            if(text != null && text.Length > 500)
            {
                return new ReturnObject
                {
                    Error = true,
                    Message = "The help link must be 500 characters or less."
                };
            }

			Lib.Data.DrugLink item = null;
			if (id > 0)
				item = new Lib.Data.DrugLink(id);
			else
			{
				item = new Lib.Data.DrugLink();

				var links = Lib.Data.DrugLink.FindByDrug(new Data.Drug(drug_id));
				if (links.Count >= 1)
					item.Order = links[links.Count - 1].Order + 1;
				else
					item.Order = 1;
			}

			var u = Framework.Security.Manager.GetUser();

			item.DrugID = drug_id;
			item.Type = type;
			item.Text = text;
			item.Link = link;
			item.Date = date;
			item.Save();

			return new ReturnObject()
			{
				Result = item,
				Redirect = new ReturnRedirectObject()
				{
					Hash = "admin/drugs/drugs/edit?id=" + drug_id
				},
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "You have successfully saved this link.",
						title = "Link Saved"
					}
				}
			};
		}

		[SecurityRole("view_admin")]
		[Method("Admin/Drugs/Link/Delete")]
		public static ReturnObject Delete(HttpContext context, long id)
		{
			if (id <= 0)
				return new ReturnObject() { Error = true, Message = "Invalid Link." };

			var item = new Lib.Data.DrugLink(id);
			item.Delete();

			return new ReturnObject()
			{
				Growl = new ReturnGrowlObject()
				{
					Type = "default",
					Vars = new ReturnGrowlVarsObject()
					{
						text = "You have successfully deleted a link.",
						title = "Drug Deleted"
					}
				},
				Actions = new List<ReturnActionObject>()
				{
					new ReturnActionObject() {
						Ele = "#links-table tr[data-id=\""+id.ToString()+"\"]",
						Type = "remove"
					}
				}
			};
		}

		[SecurityRole("view_admin")]
		[Method("Admin/Drugs/Link/Reorder")]
		public static ReturnObject Reorder(HttpContext context, long id, int fromPosition, int toPosition)
		{
			if (id <= 0)
				return new ReturnObject() { Error = false, Message = "" };

			var item = new Lib.Data.DrugLink(id);
		
			var db = Framework.Data.Database.Get("FDARems");
			string sql = "";
			if( fromPosition > toPosition )
				sql = "UPDATE [DrugLinks] SET [Order] = [Order] + 1 WHERE [DrugID] = @id AND [Order] <= @from AND [Order] >= @to";
			else
				sql = "UPDATE [DrugLinks] SET [Order] = [Order] - 1 WHERE [DrugID] = @id AND [Order] >= @from AND [Order] <= @to";
		
			var ps = new List<Parameter>();
			ps.Add(new Parameter("id",item.DrugID));
			ps.Add(new Parameter("to",toPosition));
			ps.Add(new Parameter("from",fromPosition));

			db.ExecuteNonQuery(sql,ps.ToArray());
		
			item.Order = toPosition;
			item.Save();

			return new ReturnObject() { Error = false };
		}
	}
}