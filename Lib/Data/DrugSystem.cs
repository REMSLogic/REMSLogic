using System;
using System.Collections.Generic;
using System.Data;
using Framework.Data;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "DrugSystems", PrimaryKeyColumn = "ID" )]
	public class DrugSystem : ActiveRecord
	{
		public static IList<DrugSystem> FindAll()
		{
			return FindAll<DrugSystem>(new [] { "+Name" });
		}

		[Column]
		public string Name;

		public DrugSystem(long? id = null) : base(id)
		{ }

		public DrugSystem(IDataRecord row) : base(row)
		{ }
	}
}
