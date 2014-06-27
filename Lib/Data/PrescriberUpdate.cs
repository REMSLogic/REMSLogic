using System;
using System.Collections.Generic;
using System.Data;
using Framework.Data;
using System.Data.SqlClient;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "PrescriberUpdates", PrimaryKeyColumn = "ID" )]
	public class PrescriberUpdate : ActiveRecord
	{
		public static IList<PrescriberUpdate> FindAll()
		{
			return FindAll<PrescriberUpdate>( new[] { "-DateCreated" } );
		}

		public static IList<PrescriberUpdate> FindByProvider( Provider provider )
		{
			if( provider == null || provider.ID == null )
				return new List<PrescriberUpdate>();

			var db = Framework.Data.Database.Get( "FDARems" );
			string sql = "SELECT TOP 10 * " +
							" FROM " + db.Delim( "PrescriberUpdates", Framework.Data.DelimType.Table ) +
							" WHERE [ProviderID] = @providerID " +
							" ORDER BY [DateCreated] DESC";

			var ps = new List<Parameter>();
			ps.Add( new Parameter( "providerID", provider.ID.Value ) );

			return db.ExecuteQuery<PrescriberUpdate>( sql, ps.ToArray() );
		}

		public static IList<PrescriberUpdate> FindByFacility(long facilityId)
		{
			if(facilityId == 0)
				return new List<PrescriberUpdate>();

			var db = Framework.Data.Database.Get( "FDARems" );
			string sql = "SELECT TOP 10 * " +
							" FROM " + db.Delim( "PrescriberUpdates", Framework.Data.DelimType.Table ) +
							" WHERE [FacilityId] = @facilityId " +
							" ORDER BY [DateCreated] DESC";

			var ps = new List<Parameter>();
			ps.Add( new Parameter( "facilityId", facilityId) );

			return db.ExecuteQuery<PrescriberUpdate>( sql, ps.ToArray() );
		}

		[Column]
		public long PrescriberID;
		[Column]
        public long? ProviderID;
		[Column]
		public long DrugID;
		[Column]
        public string Message;
		[Column]
        public string Type;
		[Column]
        public DateTime DateCreated;
        [Column]
        public long FacilityId;
        [Column]
        public long OrganizationId;

		public PrescriberUpdate(long? id = null) : base(id)
		{ }

		public PrescriberUpdate(IDataRecord row) : base(row)
		{ }
	}
}
