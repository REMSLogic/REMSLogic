using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App.Views.admin.dsq
{
	public partial class edit : Lib.Web.AppControlPage
	{
		public Lib.Data.Drug item;
		public Lib.Data.DrugVersion version;
		public int SectionIndex;

		protected void Page_Init(object sender, EventArgs e)
		{
			string strID = Request.QueryString["id"];
			long id;
			if( string.IsNullOrEmpty( strID ) || !long.TryParse( strID, out id ) )
			{
				item = new Lib.Data.Drug();
				version = new Lib.Data.DrugVersion();
			}
			else
			{
				item = new Lib.Data.Drug( id );
				version = Lib.Data.DrugVersion.FindLatestByDrug( item );
			}

			SectionIndex = -1;

			strID = Request.QueryString["section-id"];
			if (!string.IsNullOrEmpty(strID) && long.TryParse(strID, out id))
			{
				var sections = Lib.Data.DSQ.Section.FindAll();

				for (int i = 0; i < sections.Count; i++)
				{
					if (sections[i].ID == id)
					{
						SectionIndex = i;
						break;
					}
				}
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			dsqView.DrugID = item.ID;
		}
	}
}