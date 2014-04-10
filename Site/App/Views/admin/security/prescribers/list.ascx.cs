using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App.Views.admin.security.prescribers
{
	public partial class list : Lib.Web.AppControlPage
	{
		public IList<Lib.Data.Prescriber> Prescribers;
		protected void Page_Init(object sender, EventArgs e)
		{
			Prescribers = Lib.Data.Prescriber.FindAll();
		}
	}
}