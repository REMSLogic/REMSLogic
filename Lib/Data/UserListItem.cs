using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Framework.Data;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "UserListItems", PrimaryKeyColumn = "ID" )]
	public class UserListItem : ActiveRecord
	{
		public static UserListItem FindByListAndItem(long list_id, long item_id)
		{
			return FindFirstBy<UserListItem>( new Dictionary<string, object> {
				{ "ListID", list_id },
				{ "ItemID", item_id }
			}, new [] { "+Order" } );
		}

		public static IList<UserListItem> FindByList(UserList list)
		{
			if( list == null || list.ID == null )
				return new List<UserListItem>();

			return FindByList( list.ID.Value );
		}

		public static IList<UserListItem> FindByList(long list_id)
		{
			return FindAllBy<UserListItem>( new Dictionary<string, object> {
				{ "ListID", list_id }
			}, new[] { "+Order" } );
		}

		public static IList<T> FindByList<T>(UserList list) where T : ActiveRecord
		{
			if( list == null || list.ID == null )
				return new List<T>();

			return FindByList<T>( list.ID.Value );
		}

		public static IList<T> FindByList<T>(long list_id) where T : ActiveRecord
		{
			var tbl = Framework.Data.Manager.GetTableNameFromType( typeof( T ) );
			var db = Database.Get( "FDARems" );
			string sql = "SELECT "+db.DelimTable(tbl)+".* " +
							" FROM " + db.DelimTable( "UserListItems" ) +
								" LEFT JOIN " + db.DelimTable( tbl ) +
									" ON " + db.DelimColumn( "UserListItems", "ItemID" ) + " = " + db.DelimColumn(tbl, "ID") +
							" WHERE " + db.DelimColumn("UserListItems", "ListID") + " = " + db.DelimParameter("id") +
							" ORDER BY " + db.DelimColumn("UserListItems", "Order");

			var ps = new List<Parameter>();
			ps.Add( new Parameter( "id", list_id ) );

			return db.ExecuteQuery<T>( sql, ps.ToArray() );
		}

		[Column]
		public long ListID;
		[Column]
		public long ItemID;
		[Column]
		public int Order;
		[Column]
		public DateTime? DateAdded;

		public UserListItem(long? id = null) : base( id )
		{ }

		public UserListItem(IDataRecord row) : base( row )
		{}

		public T GetItem<T>() where T : ActiveRecord
		{
			var tbl = Framework.Data.Manager.GetTableNameFromType( typeof( T ) );
			var db = Database.Get( "FDARems" );
			var rows = db.ExecuteQuery<T>( "SELECT * FROM " + db.DelimTable( tbl ) + " WHERE [ID] = @id", new[] { new Parameter( "id", this.ItemID ) } );

			if( rows.Count > 0 )
				return rows[0];

			return null;
		}
	}
}
