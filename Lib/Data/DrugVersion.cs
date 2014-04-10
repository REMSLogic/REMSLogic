using System;
using System.Collections.Generic;
using System.Data;
using Framework.Data;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "DrugVersions", PrimaryKeyColumn = "ID" )]
	public class DrugVersion : ActiveRecord
	{
		public static IList<DrugVersion> FindAll()
		{
			return FindAll<DrugVersion>( new[] {
				"+DrugID",
				"-Version"
			});
		}

		public static IList<DrugVersion> FindByDrug(Drug d)
		{
            if(d == null || d.ID == null)
                return new List<DrugVersion>();

			return FindAllBy<DrugVersion>( new Dictionary<string, object> {
				{ "DrugID", d.ID.Value }
			}, new [] { "-Version" } );
		}

		public static DrugVersion FindLatestByDrug(Drug d)
		{
			return FindLatestByDrug(d.ID);
		}
		public static DrugVersion FindLatestByDrug(long? drug_id)
		{
			return FindFirstBy<DrugVersion>( new Dictionary<string, object> {
				{ "DrugID", drug_id.Value }
			}, new[] { "-Version" } );
		}

		[Column]
		public long DrugID;
		[Column]
		public string Message;
		[Column]
		public int Version;
		[Column]
		public DateTime Updated;
		[Column]
		public long UpdatedBy;
		[Column]
		public string Status;
		[Column]
		public long? ReviewedBy;
		[Column]
		public DateTime? Reviewed;
		[Column]
		public string DenyReason;

		public DrugVersion(long? id = null) : base(id)
		{ }

		public DrugVersion(IDataRecord row) : base(row)
		{ }
	}
}
