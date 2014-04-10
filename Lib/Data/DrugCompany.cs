using System;
using System.Collections.Generic;
using System.Data;
using Framework.Data;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "DrugCompanies", PrimaryKeyColumn = "ID" )]
	public class DrugCompany : ActiveRecord
	{
		public static IList<DrugCompany> FindAll()
		{
			return FindAll<DrugCompany>( new[] {
				"+Name"
			} );
		}

		[Column]
		public string Name;
		[Column]
		public string Website;
		[Column]
		public string Phone;

		public DrugCompany(long? id = null) : base(id)
		{ }

		public DrugCompany(IDataRecord row) : base(row)
		{ }
	}
}
