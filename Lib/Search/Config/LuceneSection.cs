using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Lib.Search.Config
{
	public class LuceneSection : ConfigurationSection
	{
		[ConfigurationProperty( "indexDir" )]
		public string IndexDirectory
		{
			get
			{
				return (string)this["indexDir"];
			}
		}
	}
}
