using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App.Views.admin.drugs.systems
{
	public partial class list : Lib.Web.AppControlPage
	{
		public IList<Lib.Data.DrugSystem> Systems;
		protected void Page_Init(object sender, EventArgs e)
		{
			Systems = Lib.Data.DrugSystem.FindAll();
		}
	}
}