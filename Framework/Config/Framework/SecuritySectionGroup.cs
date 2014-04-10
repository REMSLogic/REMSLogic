using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Framework.Config.Framework
{
	public class SecuritySectionGroup : ConfigurationSectionGroup
	{
		[ConfigurationProperty( "authentication" )]
		public Framework.Security.AuthenticationSection Authentication
		{
			get
			{
				return (Framework.Security.AuthenticationSection)this.Sections["authentication"];
			}
		}

		[ConfigurationProperty( "authorization" )]
		public Framework.Security.AuthorizationSection Authorization
		{
			get
			{
				return (Framework.Security.AuthorizationSection)this.Sections["authorization"];
			}
		}
	}
}
