using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App.Views.admin.security.users
{
	public partial class edit : Lib.Web.AppControlPage
	{
		public Lib.Data.UserProfile item;
		public Lib.Data.Contact contact;
		public Framework.Security.User user;
		public IList<Framework.Security.Group> Groups;
		public IList<Framework.Security.Group> UserGroups;
		public IList<Lib.Data.UserType> UserTypes;

		protected void Page_Init(object sender, EventArgs e)
		{
			string strID = Request.QueryString["id"];
			long id;
			if( string.IsNullOrEmpty(strID) || !long.TryParse(strID,out id) )
				item = new Lib.Data.UserProfile();
			else
				item = new Lib.Data.UserProfile(id);

			user = item.User;

			if (user == null)
				user = new Framework.Security.User();

			contact = item.PrimaryContact;

			if (contact == null)
				contact = new Lib.Data.Contact();

			Groups = Framework.Security.Group.FindAll();
			UserGroups = user.GetGroups();
			UserTypes = Lib.Data.UserType.FindAll();
		}

		public bool IsInGroup(Framework.Security.Group g)
		{
			foreach( var group in UserGroups )
				if( group.ID == g.ID )
					return true;

			return false;
		}
	}
}