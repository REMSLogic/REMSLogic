using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Systems
{
	// TJM 2014-03-27: This is pretty much a duplicate of Lib.Systems.Security
	public class UserInfo
	{
		public static Data.UserProfile GetProfile()
		{
			var user = Framework.Security.Manager.GetUser();

			if( user == null )
				return null;

			return Data.UserProfile.FindByUser(user);
		}

		public static Data.Prescriber GetPrescriber()
		{
			var profile = GetProfile();

			if( profile == null )
				return null;

			return Data.Prescriber.FindByProfile(profile);
		}

		public static Data.ProviderUser GetProviderUser()
		{
			var profile = GetProfile();

			if (profile == null)
				return null;

			return Data.ProviderUser.FindByProfile(profile);
		}

		public static IList<Data.Provider> GetProviders()
		{
			var prescriber = GetPrescriber();

			if( prescriber != null )
			{
				return (from p in Data.PrescriberProfile.FindByPrescriber(prescriber)
						where p != null && p.ProviderID > 0
						select p.Provider).ToList();
			}

			var pu = GetProviderUser();

			if( pu != null )
			{
				return new List<Data.Provider>(new Data.Provider[] { pu.Provider });
			}

			return new List<Data.Provider>();
		}

		public static bool HasProvider(Data.Provider p)
		{
			if( p == null || p.ID == null )
				return false;
			
			return HasProvider(p.ID.Value);
		}

		public static bool HasProvider(long pid)
		{
			return (from p in GetProviders()
							 where p.ID == pid
							 select p).Count() > 0;
		}
	}
}
