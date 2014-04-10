using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Web;

namespace Lib.Web
{
	public class AdminControlPage : BaseControl
	{
		protected override void OnInit(EventArgs e)
		{
			RequireRole( "view_admin" );

			base.OnInit( e );
		}
	}
}
