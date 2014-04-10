using System;
using System.Collections.Generic;
using System.Data;
using Framework.Data;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "UserTypes", PrimaryKeyColumn = "ID" )]
	public class UserType : ActiveRecord
	{
		public static IList<UserType> FindAll()
		{
			return FindAll<UserType>( new[] { "+Name" } );
		}

		public static UserType FindByName(string name)
		{
			return FindFirstBy<UserType>( new Dictionary<string, object> {
				{ "Name", name }
			} );
		}

		[Column]
		public string Name;
		[Column]
		public string DisplayName;
		[Column]
		public bool HasContact;
		[Column]
		public bool HasAddress;

		public UserType(long? id = null)
			: base(id)
		{ }

		public UserType(IDataRecord row)
			: base(row)
		{ }
	}
}
