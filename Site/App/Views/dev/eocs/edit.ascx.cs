using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App.Views.dev.eocs
{
	public partial class edit : Lib.Web.AppControlPage
	{
		public Lib.Data.Eoc item;
		public IList<Lib.Data.UserType> UserTypes;
		public IList<Lib.Data.UserType> EocUserTypes;

		protected void Page_Init(object sender, EventArgs e)
		{
			string strID = Request.QueryString["id"];
			long id;
			if (string.IsNullOrEmpty(strID) || !long.TryParse(strID, out id))
				item = new Lib.Data.Eoc();
			else
				item = new Lib.Data.Eoc( id );

			UserTypes = Lib.Data.UserType.FindAll();
			EocUserTypes = item.GetUserTypes();
		}

		protected bool HasUserType( long ut_id )
		{
			foreach( var ut in EocUserTypes )
				if( ut.ID.Value == ut_id )
					return true;
			return false;
		}
	}
}