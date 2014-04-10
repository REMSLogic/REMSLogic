using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Framework.Config.Framework.Log
{
	public class ErrorsElement : ConfigurationElement
	{
		[ConfigurationProperty("email", IsRequired = true)]
		public bool Email
		{
			get
			{
				return (bool)this["email"];
			}

		}

		[ConfigurationProperty( "sendto" )]
		public string SendTo
		{
			get { return (string)this["sendto"]; }
		}
	}
}
