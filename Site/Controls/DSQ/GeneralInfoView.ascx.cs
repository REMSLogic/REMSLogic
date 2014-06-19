using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.App.Controls.DSQ
{
	public partial class GeneralInfoView : Lib.Web.AppControlPage
	{
		public Lib.Data.Drug item;
		public Lib.Data.DrugSystem TheSystem;
		public IList<Lib.Data.DrugFormulation> Formulations;
		public IList<Lib.Data.DrugVersion> Versions;
		protected IList<Lib.Data.UserEoc> UserEocs;
		protected IList<Lib.Data.FacilityEoc> FacilityEocs;

		public string UserType;

		public GeneralInfoView() : base()
		{
			string strID = System.Web.HttpContext.Current.Request.QueryString["id"];
			long id;
			if (string.IsNullOrEmpty(strID) || !long.TryParse(strID, out id))
				item = new Lib.Data.Drug();
			else
				item = new Lib.Data.Drug(id);

			TheSystem = new Lib.Data.DrugSystem(item.SystemID);
			Formulations = Lib.Data.DrugFormulation.FindByDrug(item);
			Versions = (from v in Lib.Data.DrugVersion.FindByDrug( item )
						where v.Status == "Approved"
						orderby v.Updated descending
						select v).ToList();

			var profile = Lib.Systems.Security.GetCurrentProfile();
			UserType = profile.UserType.Name;
			if( UserType == "prescriber" )
				UserEocs = Lib.Data.UserEoc.FindByUserandDrug( profile.ID.Value, item.ID.Value );
			else if( UserType == "provider" )
			{
				var facilities = Lib.Data.ProviderFacility.FindAllByProviderUser( Lib.Systems.Security.GetCurrentProviderUser() );
				if( facilities != null && facilities.Count > 0 )
					FacilityEocs = Lib.Data.FacilityEoc.FindByFacilityandDrug( facilities[0].ID.Value, item.ID.Value );
			}
		}

		public bool EocIsCertified( string name )
		{
			var eoc = Lib.Data.Eoc.FindByName( name );

			if( eoc == null )
				return false;

			if( UserType == "prescriber" && UserEocs != null )
			{
				foreach( var e in UserEocs )
					if( e.EocID == eoc.ID.Value )
						return true;
			}
			else if( UserType == "provider" && FacilityEocs != null )
			{
				foreach( var e in FacilityEocs )
					if( e.EocID == eoc.ID.Value )
						return true;
			}

			return false;
		}
	}
}