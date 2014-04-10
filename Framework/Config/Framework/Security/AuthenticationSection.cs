using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Framework.Config.Framework.Security
{
	public class AuthenticationSection : ConfigurationSection
	{
		[ConfigurationProperty("hash")]
		public string HashingMethod
		{
			get
			{
				return (string)this["hash"];
			}

		}

		[ConfigurationProperty("connection")]
		public string Connection
		{
			get
			{
				return (string)this["connection"];
			}
		}

		[ConfigurationProperty("accessdeniedurl")]
		public string AccessDeniedUrl
		{
			get
			{
				return (string)this["accessdeniedurl"];
			}
		}

		[ConfigurationProperty( "user" )]
		public Authentication.UserElement User
		{
			get
			{
				return (Authentication.UserElement)this["user"];
			}
		}

		[ConfigurationProperty( "group" )]
		public Authentication.GroupElement Group
		{
			get
			{
				return (Authentication.GroupElement)this["group"];
			}
		}
	}
}
