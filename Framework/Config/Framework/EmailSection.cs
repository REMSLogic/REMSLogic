using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Framework.Config.Framework
{
	public class EmailSection : ConfigurationSection
	{
		[ConfigurationProperty( "template" )]
		public Email.TemplateElement Template
		{
			get
			{
				return (Email.TemplateElement)this["template"];
			}
		}
	}
}
