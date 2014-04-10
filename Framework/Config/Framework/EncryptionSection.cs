using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Framework.Config.Framework
{
	public class EncryptionSection : ConfigurationSection
	{
		[ConfigurationProperty( "key" )]
		public string Key
		{
			get
			{
				return (string)this["key"];
			}

		}

		[ConfigurationProperty("iv")]
		public string IV
		{
			get
			{
				return (string)this["iv"];
			}

		}
	}
}
