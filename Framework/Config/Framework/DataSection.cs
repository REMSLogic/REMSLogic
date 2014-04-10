using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Framework.Config.Framework
{
	public class DataSection : ConfigurationSection
	{
		[ConfigurationProperty("connections")]
		public Framework.Data.ConnectionElementCollection Connections
		{
			get
			{
				return (Framework.Data.ConnectionElementCollection)this["connections"];
			}
		}
	}
}
