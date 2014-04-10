using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Framework.Config.Framework
{
	public class HashSection : ConfigurationSection
	{
		[ConfigurationProperty( "salt" )]
		public string Salt
		{
			get
			{
				return (string)this["salt"];
			}

		}

		[ConfigurationProperty("method")]
		public string Method
		{
			get
			{
				return (string)this["method"];
			}

		}

		[ConfigurationProperty( "default-encoding" )]
		public string DefaultEncoding
		{
			get
			{
				return (string)this["default-encoding"];
			}

		}

		[ConfigurationProperty( "output-type" )]
		public string OutputType
		{
			get
			{
				return (string)this["output-type"];
			}

		}
	}
}
