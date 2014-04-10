using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Framework.PayPal.Config
{
	public class PayPalSection : ConfigurationSection
	{
		[ConfigurationProperty( "currEnviron" )]
		public string CurrentEnvironment
		{
			get
			{
				return (string)this["currEnviron"];
			}
		}

		[ConfigurationProperty( "sandbox" )]
		public EnvironmentElement Sandbox
		{
			get
			{
				return (EnvironmentElement)this["sandbox"];
			}
		}

		[ConfigurationProperty( "prod" )]
		public EnvironmentElement Production
		{
			get
			{
				return (EnvironmentElement)this["prod"];
			}
		}
	}
}
