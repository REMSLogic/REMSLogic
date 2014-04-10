using System;
using System.Collections.Generic;
using System.Data;
using Framework.Data;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "Orders", PrimaryKeyColumn = "ID" )]
	public class Order : Framework.Data.ActiveRecord
	{
		public Order(long? id = null) : base(id)
		{ }

		public Order(IDataRecord row) : base(row)
		{ }
	}
}
