using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Framework.Config.Framework.Email
{
	public class TemplateElement : ConfigurationElement
	{
		[ConfigurationProperty( "path", IsRequired = true )]
		public string Path
		{
			get
			{
				return (string)this["path"];
			}

		}
	}
}
