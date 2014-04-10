using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Framework.Config.Framework
{
	public class NetSection : ConfigurationSection
	{
		[ConfigurationProperty( "sites" )]
		public Framework.Net.SiteElementCollection Sites
		{
			get
			{
				return (Framework.Net.SiteElementCollection)this["sites"];
			}
		}
	}
}
