using System;
using System.Collections.Generic;
using System.Data;
using Framework.Data;

namespace Lib.Data.DSQ
{
	[Table( DatabaseName = "FDARems", TableName = "DSQ_Sections", PrimaryKeyColumn = "ID" )]
	public class Section : ActiveRecord
	{
		public static IList<Section> FindAll()
		{
			return FindAll<Section>(new [] { "+Order" });
		}

		[Column]
		public string Name;
		[Column]
		public int Order;

		public Section(long? id = null) : base(id)
		{ }

		public Section(IDataRecord row) : base(row)
		{ }
	}
}
