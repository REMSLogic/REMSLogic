using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site
{
	public partial class CauseError : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			int i=2;

			i-=2;
			int test = 5/i;

			lblOut.Text = test.ToString();
		}
	}
}