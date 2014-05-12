using System.Web;
using Framework.API;

namespace Lib.API
{
	public class Search : Base
	{
		[Method("Search/DoSearch")]
		public static ReturnObject DoSearch(HttpContext context, string q)
		{
			if (string.IsNullOrEmpty(q))
				return new ReturnObject()
				{
					Result = null
				};

			var results = Lib.Search.Manager.Query(q);

			context.Response.ContentType = "text/html";

			if (results == null || results.Count <= 0)
			{
				context.Response.Write("No results found for: <strong>" + q + "</strong>");
			}
			else
			{
				context.Response.Write("<ul>");

				foreach (var r in results)
				{
					string url = "";
					string title = "";
					string note = "";

					switch (r.type)
					{
					case "prescriber":
						var prescriber = (Lib.Data.Prescriber)r.item;
						url = "#admin/security/prescribers/edit?id=" + prescriber.ID.Value;
						title = prescriber.Profile.PrimaryContact.Name;
						note = "Prescriber";
						break;
					case "provider":
						var provider = (Lib.Data.Provider)r.item;
						url = "#admin/security/providers/edit?id=" + provider.ID.Value;
						title = provider.Name;
						note = "Provider";
						break;
					case "drug-company":
						var drugcompany = (Lib.Data.DrugCompany)r.item;
						url = "#admin/drugs/companies/edit?id=" + drugcompany.ID.Value;
						title = drugcompany.Name;
						note = "Drug Company";
						break;
					case "drug-system":
						var drugsystem = (Lib.Data.DrugSystem)r.item;
						url = "#admin/drugs/systems/edit?id=" + drugsystem.ID.Value;
						title = drugsystem.Name;
						note = "Drug System";
						break;
					case "drug":
						var drug = (Lib.Data.Drug)r.item;
						url = "#common/drugs/detail?id=" + drug.ID.Value;
						title = drug.GenericName;
						note = "Drug";
						break;
					case "user-profile":
						var profile = (Lib.Data.UserProfile)r.item;
						url = "#admin/security/users/edit?id=" + profile.ID.Value;
						title = profile.PrimaryContact == null ? profile.User.Username : profile.PrimaryContact.Name;
						note = "User";
						break;
					}

					context.Response.Write("<li><a href=\""+url+"\">"+title+"</a><em>"+note+"</em></li>");
				}

				context.Response.Write("</ul>");
			}

			context.Response.End();

			return null;
		}

		[Method("Search/Suggest")]
		public static ReturnObject Suggest(HttpContext context, string q, string target)
		{
			context.Response.ContentType = "text/html";
			context.Response.Write("");
			context.Response.End();

			return null;
		}
	}
}
