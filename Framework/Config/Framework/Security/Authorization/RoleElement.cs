using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Framework.Config.Framework.Security.Authorization
{
	public class RoleElement : ConfigurationElement
	{
		[ConfigurationProperty( "table", IsRequired = true )]
		public string Table
		{
			get { return (string)base["table"]; }
		}

		[ConfigurationProperty( "idcol", IsRequired = true )]
		public string IDCol
		{
			get { return (string)base["idcol"]; }
		}
	}
}
