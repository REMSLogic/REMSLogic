using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Systems
{
	public class EOC
	{
		public static bool IsCertified( Lib.Data.UserProfile up, string eoc_name, long drug_id, ref DateTime date_certified )
		{
			return IsCertified(up, Lib.Data.Eoc.FindByName(eoc_name), new Lib.Data.Drug(drug_id), ref date_certified);
		}

		public static bool IsCertified( Lib.Data.UserProfile up, Lib.Data.Eoc eoc, Lib.Data.Drug drug, ref DateTime date_certified )
		{
			if( up == null || eoc == null )
				return false;

			var ut = up.UserType;

			if( !eoc.HasUserType( ut ) )
				return false;

			if( ut.Name == "prescriber" )
			{
				var user_eocs = Lib.Data.UserEoc.FindByUserandDrug(up.ID.Value, drug.ID.Value);

				foreach( var ue in user_eocs )
				{
					if( ue.EocID == eoc.ID.Value )
					{
						date_certified = ue.DateCompleted;
						return true;
					}
				}
			}
			else if( ut.Name == "provider" )
			{
				var pu = Lib.Systems.Security.GetCurrentProviderUser();
				if( pu == null || pu.PrimaryFacilityID == null )
					return false;

				var facility_eocs = Lib.Data.FacilityEoc.FindByFacilityandDrug( pu.PrimaryFacilityID.Value, drug.ID.Value );

				foreach( var fe in facility_eocs )
				{
					if( fe.EocID == eoc.ID.Value )
					{
						date_certified = fe.DateCompleted;
						return true;
					}
				}
			}
			else
				return false;

			return false;
		}
	}
}
