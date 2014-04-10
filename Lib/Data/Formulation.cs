using System;
using System.Collections.Generic;
using System.Data;
using Framework.Data;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "Formulations", PrimaryKeyColumn = "ID" )]
	public class Formulation : ActiveRecord
	{
		public static IList<Formulation> FindAll()
		{
			return FindAll<Formulation>( new[] { "+Name" } );
		}

		[Column]
		public string Name;

		public Formulation(long? id = null) : base(id)
		{ }

		public Formulation(IDataRecord row) : base(row)
		{ }
	}
}
