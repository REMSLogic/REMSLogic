using System;
using System.Collections.Generic;
using System.Data;
using Framework.Data;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "Files", PrimaryKeyColumn = "ID" )]
	public class File : ActiveRecord
	{
		public static IList<File> FindAll()
		{
			return FindAll<File>();
		}

		public static IList<File> FindbyParent(string type, long id)
		{
			return FindAllBy<File>( new Dictionary<string, object> {
				{ "ParentType", type },
				{ "ParentID", id }
			} );
		}

		[Column]
		public string ParentType;
		[Column]
		public long ParentID;
		[Column]
		public string Path;
		[Column]
		public string Name;
		[Column]
		public string ContentType;

		public File(long? id = null) : base(id)
		{ }

		public File(IDataRecord row) : base(row)
		{ }
	}
}
