using System;
using System.Collections.Generic;
using System.Data;
using Framework.Data;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "UserEocs", PrimaryKeyColumn = "ID" )]
	public class UserEoc : ActiveRecord
	{
		public static IList<UserEoc> FindAll()
		{
			return FindAll<UserEoc>( new[] {
				"-DateCompleted"
			} );
		}

		public static IList<UserEoc> FindByUserandDrug( long profileid, long drugid )
		{
			return FindAllBy<UserEoc>( new Dictionary<string, object> {
				{ "ProfileID", profileid },
				{ "DrugID", drugid }
			} );
		}

		[Column]
		public long ProfileID;
		[Column]
		public long DrugID;
		[Column]
		public long EocID;
		[Column]
		public DateTime DateCompleted;
        [Column]
        public long LinkId;

		public UserEoc(long? id = null) : base( id )
		{ }

		public UserEoc(IDataRecord row) : base( row )
		{ }
	}
}
