using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Framework.Config.Framework.Net
{
	public class SiteElement : ConfigurationElement
	{
		[ConfigurationProperty( "name" )]
		public string Name
		{
			get
			{
				return (string)this["name"];
			}
		}

		[ConfigurationProperty( "current" )]
		public bool Current
		{
			get
			{
				return (bool)this["current"];
			}
		}

		[ConfigurationProperty( "dev" )]
		public string Dev
		{
			get
			{
				return (string)this["dev"];
			}
		}

		[ConfigurationProperty( "prod" )]
		public string Prod
		{
			get
			{
				return (string)this["prod"];
			}
		}
	}
}
