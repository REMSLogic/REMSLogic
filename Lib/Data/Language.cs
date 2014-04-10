using System;
using System.Collections.Generic;
using System.Data;
using Framework.Data;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "Languages", PrimaryKeyColumn = "ID" )]
	public class Language : ActiveRecord
	{
		public static IList<Language> FindAll()
		{
			return FindAll<Language>();
		}

		[Column]
		public string Code;
		[Column]
		public string Name;
		[Column]
		public string EnabledIcon;
		[Column]
		public string DisabledIcon;

		public Language(long? id = null) : base(id)
		{ }

		public Language(IDataRecord row) : base(row)
		{ }
	}
}
