using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Framework.Config.Framework
{
	public class LogSection : ConfigurationSection
	{
		[ConfigurationProperty("enabled")]
		public bool Enabled
		{
			get
			{
				return (bool)this["enabled"];
			}

		}

		[ConfigurationProperty("errors")]
		public Framework.Log.ErrorsElement Errors
		{
			get
			{
				return (Framework.Log.ErrorsElement)this["errors"];
			}
		}
	}
}
