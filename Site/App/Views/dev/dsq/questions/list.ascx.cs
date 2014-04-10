using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App.Views.dev.dsq.questions
{
	public partial class list : Lib.Web.AppControlPage
	{
		public IList<Lib.Data.DSQ.Question> Items;
		protected void Page_Init(object sender, EventArgs e)
		{
			Items = Lib.Data.DSQ.Question.FindAll();
		}
	}
}