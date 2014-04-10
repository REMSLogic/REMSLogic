using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App.Views.admin.dsq
{
	public partial class link : Lib.Web.AdminControlPage
	{
		public Lib.Data.DSQ.Link item;
		public long SectionID = -1;

		protected void Page_Init(object sender, EventArgs e)
		{
			string strID = Request.QueryString["id"];
			long id;
			if (string.IsNullOrEmpty(strID) || !long.TryParse(strID, out id))
				item = new Lib.Data.DSQ.Link();
			else
				item = new Lib.Data.DSQ.Link(id);

			strID = Request.QueryString["drug-id"];
			if (!string.IsNullOrEmpty(strID) && long.TryParse(strID, out id))
				item.DrugID = id;
			else
				base.RedirectHash("admin/drugs/drugs/list", true, "No drug selected to add a link to.");

			strID = Request.QueryString["question-id"];
			if (!string.IsNullOrEmpty(strID) && long.TryParse(strID, out id))
				item.QuestionID = id;
			else
				base.RedirectHash("admin/dsq/edit?id="+item.DrugID, true, "No question selected to add a link to.");

			if (item.QuestionID != 0)
			{
				var q = new Lib.Data.DSQ.Question(item.QuestionID);
				SectionID = q.SectionID;
			}
		}
	}
}