using System;
using System.Collections.Generic;
using System.Data;
using Framework.Data;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "FacilityEocs", PrimaryKeyColumn = "ID" )]
	public class FacilityEoc : ActiveRecord
	{
		public static IList<FacilityEoc> FindAll()
		{
			return FindAll<FacilityEoc>( new[] {
				"-DateCompleted"
			} );
		}

		public static IList<FacilityEoc> FindByFacilityandDrug( long facilityid, long drugid )
		{
			return FindAllBy<FacilityEoc>( new Dictionary<string, object> {
				{ "FacilityID", facilityid },
				{ "DrugID", drugid }
			} );
		}

		[Column]
		public long FacilityID;
		[Column]
		public long DrugID;
		[Column]
		public long EocID;
		[Column]
		public DateTime DateCompleted;

		public FacilityEoc( long? id = null )
			: base( id )
		{ }

		public FacilityEoc( IDataRecord row )
			: base( row )
		{ }
	}
}
