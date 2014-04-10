using System;
using System.Collections.Generic;
using System.Data;
using Framework.Data;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "DrugLinks", PrimaryKeyColumn = "ID" )]
	public class DrugLink : ActiveRecord
	{
		public static IList<DrugLink> FindAll()
		{
			return FindAll<DrugLink>( new[] { "-DrugID", "+Order" } );
		}

		public static IList<DrugLink> FindByDrug(Drug d)
		{
			if (d == null || d.ID == null)
				return new List<DrugLink>();

			return FindAllBy<DrugLink>( new Dictionary<string, object> {
				{ "DrugID", d.ID.Value }
			}, new[] { "+Order" } );
		}

		[Column]
		public long DrugID;
		[Column]
		public string Type;
		[Column]
		public string Text;
		[Column]
		public string Link;
		[Column]
		public int Order;
		[Column]
		public DateTime? Date;

		public Drug Drug
		{ get { return new Drug(DrugID); } }

		public DrugLink(long? id = null) : base(id)
		{ }

		public DrugLink(IDataRecord row) : base(row)
		{ }
	}
}
