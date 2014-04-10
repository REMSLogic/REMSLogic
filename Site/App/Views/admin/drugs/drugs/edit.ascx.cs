﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App.Views.admin.drugs.drugs
{
	public partial class edit : Lib.Web.AppControlPage
	{
		public Lib.Data.Drug item;
		public IList<Lib.Data.DrugCompany> Companies;
		public IList<Lib.Data.DrugSystem> Systems;
		public IList<Lib.Data.DrugLink> Links;

		protected void Page_Init(object sender, EventArgs e)
		{
			string strID = Request.QueryString["id"];
			long id;
			if (string.IsNullOrEmpty(strID) || !long.TryParse(strID, out id))
				item = new Lib.Data.Drug();
			else
				item = new Lib.Data.Drug(id);

			Companies = Lib.Data.DrugCompany.FindAll();
			Systems = Lib.Data.DrugSystem.FindAll();
			Links = Lib.Data.DrugLink.FindByDrug(item);
		}
	}
}