using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Systems
{
	public static class Security
	{
		public static Framework.Security.User GetCurrentUser()
		{
			return Framework.Security.Manager.GetUser();
		}

		public static Lib.Data.UserProfile GetCurrentProfile()
		{
			var user = GetCurrentUser();

			if( user == null )
				return null;

			return Lib.Data.UserProfile.FindByUser( user );
		}

		public static Lib.Data.Prescriber GetCurrentPrescriber()
		{
			var profile = GetCurrentProfile();

			if( profile == null )
				return null;

			return Lib.Data.Prescriber.FindByProfile( profile );
		}

		public static Lib.Data.ProviderUser GetCurrentProviderUser()
		{
			var profile = GetCurrentProfile();

			if( profile == null )
				return null;

			return Lib.Data.ProviderUser.FindByProfile( profile );
		}

		public static Lib.Data.Provider GetCurrentProvider()
		{
			var pu = GetCurrentProviderUser();

			if( pu == null )
				return null;

			return pu.Provider;
		}

		public static Lib.Data.UserProfile GetProfileForUser(long user_id)
		{
			return Lib.Data.UserProfile.FindByUser( user_id );
		}
	}
}
