using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App.Views.provider.reports
{
	public partial class prescriber_summary : Lib.Web.AppControlPage
	{
		public IList<Lib.Data.Prescriber> Prescribers;

		protected void Page_Init(object sender, EventArgs e)
		{
			var p = Lib.Data.Provider.FindByUser(Lib.Data.ProviderUser.FindByProfile(Lib.Data.UserProfile.FindByUser(Framework.Security.Manager.GetUser())));
			Prescribers = p.GetPrescribers();

			if (Request.QueryString["export"] == "csv")
			{
				ExportCSV();
			}
		}

		protected void ExportCSV()
		{
			Response.Clear();
			Response.AppendHeader("Content-Disposition", "attachment; filename=prescriber-summary-" + DateTime.Now.Year + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + ".csv");
			Response.ContentType = "text/csv";

			Response.Write(EncodeRow(new string[] {"Name","Email","Phone"/*,"Enrolled Drugs","Percent Certified"*/}));

			foreach (var p in Prescribers)
			{
				var profile = p.Profile;
				var contact = profile.PrimaryContact;
				Response.Write(EncodeRow(new string[] {
					contact.Name,
					contact.Email,
					contact.Phone/*,
					p.GetNumSelectedDrugs().ToString(),
					((p.GetNumSelectedDrugs() <= 0) ? "0.00" : ((((float)p.GetNumCertifiedDrugs()) / ((float)p.GetNumSelectedDrugs())) * 100.0f).ToString("#.00"))+"%"*/
				}));
			}

			Response.Flush();
			Response.End();
		}

		protected string EncodeRow(string[] row)
		{
			string ret = "";
			bool first = true;

			foreach (var field in row)
			{
				if (!first)
					ret += ",";

				first = false;
				ret += EncodeField(field);
			}

			ret += "\n";

			return ret;
		}

		protected string EncodeField(string field)
		{
			return "\"" + field.Replace("\"", "\"\"") + "\"";
		}
	}
}