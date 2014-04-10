using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Framework.Config.Framework.Security
{
	public class AuthorizationSection : ConfigurationSection
	{
		[ConfigurationProperty( "connection" )]
		public string Connection
		{
			get
			{
				return (string)this["connection"];
			}
		}

		[ConfigurationProperty( "role" )]
		public Authorization.RoleElement Role
		{
			get
			{
				return (Authorization.RoleElement)this["role"];
			}
		}
	}
}
