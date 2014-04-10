using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Framework.PayPal.Config
{
	public class EnvironmentElement : ConfigurationElement
	{
		[ConfigurationProperty( "user", IsRequired = true )]
		public string User
		{
			get { return (string)base["user"]; }
		}

		[ConfigurationProperty( "pwd", IsRequired = true )]
		public string Pwd
		{
			get { return (string)base["pwd"]; }
		}

		[ConfigurationProperty( "sig", IsRequired = true )]
		public string Signature
		{
			get { return (string)base["sig"]; }
		}
	}
}
