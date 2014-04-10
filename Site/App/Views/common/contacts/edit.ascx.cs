using System;

namespace Site.App.Views.common.contacts
{
	public partial class edit : Lib.Web.AppControlPage
	{
		public Lib.Data.Contact item;
		public string BackHash;
		public string ParentType;
		public string ParentID;

		protected void Page_Init(object sender, EventArgs e)
		{
			BackHash = Request.QueryString["back-hash"];
			ParentType = Request.QueryString["parent-type"];
			ParentID = Request.QueryString["parent-id"];

			string strID = Request.QueryString["id"];
			long id;
			if (string.IsNullOrEmpty(strID) || !long.TryParse(strID, out id))
				item = new Lib.Data.Contact();
			else
				item = new Lib.Data.Contact(id);
		}
	}
}