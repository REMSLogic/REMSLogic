using System;
using System.Collections.Generic;
using System.Data;
using Framework.Data;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "UserLists", PrimaryKeyColumn = "ID" )]
	public class UserList : ActiveRecord
	{
        // MJL 2014-01-13 - Monk, I hate to make people remember string constants or
        // use indicies into an array (an implementation detail they shouldn't care about.
        // this static class provides a nice way to contain all of the string constants.
        public static class Types
        {
            public const string Drug = "drug";
            public const string Prescriber = "prescriber";
            public const string HCO = "hco";
            public const string DrugCompany = "drug-company";
            public const string DrugSystem = "drug-system";
            public const string DrugLink = "drug-link";
        }

        // MJL 2013-01-13 - I know you use this to ensure that string passed in is a valid
        // type.  I'm 100% sure we could combine this and the constants above into a much
        // better solution that serves both purposes.
        //
        // Also, you don't need to use the list constructor with an array of strings.  You
        // can just use the object initializer syntax.
        public static List<string> DataTypes = new List<string>
        {
            Types.Drug,
            Types.Prescriber,
            Types.HCO,
            Types.DrugCompany,
            Types.DrugSystem,
            Types.DrugLink
        };

		public static IList<UserList> FindByUserProfile(UserProfile user, string data_type = null, bool? system = null, string name = null)
		{
			return FindByUserProfile( (user != null && user.ID.HasValue) ? user.ID.Value : 0, data_type, system, name );
		}

		public static IList<UserList> FindByUserProfile( long user_profile_id, string data_type = null, bool? system = null, string name = null )
		{
			if( !DataTypes.Contains( data_type ) )
				throw new ArgumentOutOfRangeException();

			var db = Database.Get("FDARems");
			string sql = "SELECT * " +
							" FROM " + db.DelimTable("UserLists") +
							" WHERE ";

			var ps = new List<Parameter>();

			if( user_profile_id <= 0 )
			{
				sql += db.DelimColumn( "UserProfileID" ) + " IS NULL";
			}
			else
			{
				sql += db.DelimColumn( "UserProfileID" ) + " = " + db.DelimParameter( "id" );
				ps.Add( new Parameter( "id", user_profile_id ));
			}

			if( !string.IsNullOrWhiteSpace(data_type) )
			{
				sql += " AND " + db.DelimColumn( "DataType" ) + " = " + db.DelimParameter( "data_type" );
				ps.Add( new Parameter( "data_type", data_type ) );
			}

			if( system != null )
			{
				sql += " AND " + db.DelimColumn( "System" ) + " = " + db.DelimParameter( "system" );
				ps.Add( new Parameter( "system", system.Value ) );
			}

			if( !string.IsNullOrWhiteSpace( name ) )
			{
				sql += " AND " + db.DelimColumn( "Name" ) + " = " + db.DelimParameter( "name" );
				ps.Add( new Parameter( "name", name ) );
			}

			return db.ExecuteQuery<UserList>(sql, ps.ToArray());
		}

		public static IList<UserList> FindByItemAndType( long item_id, string data_type)
		{
			if( !DataTypes.Contains( data_type ) )
				throw new ArgumentOutOfRangeException();

			var db = Database.Get( "FDARems" );
			var sql = "SELECT " + db.DelimTable( "l" ) + ".* " +
					 " FROM " + db.DelimTable( "UserLists" ) + " AS " + db.DelimTable( "l" ) +
						" LEFT JOIN " + db.DelimTable( "UserListItems" ) + " AS " + db.DelimTable( "i" ) +
							" ON " + db.DelimColumn("l","ID") + " = " + db.DelimColumn("i","ListID") +
					 " WHERE " + db.DelimColumn("l", "DataType") + " = @data_type AND " +
						db.DelimColumn("i", "ItemID") + " = @item_id";

			var ps = new[] {
				new Parameter( "item_id", item_id ),
				new Parameter( "data_type", data_type )
			};

			return db.ExecuteQuery<UserList>( sql, ps );
		}

		[Column]
		public long? UserProfileID;
		[Column]
		public string Name;
		[Column]
		public DateTime DateCreated;
		[Column]
		public DateTime DateModified;
		[Column]
		public string DataType;
		[Column]
		public bool System;

		public UserList(long? id = null) : base( id )
		{ }

		public UserList(IDataRecord row) : base( row )
		{ }

		public void ClearItems()
		{
			var db = this.table.DB;
			var t = Table.Get(db, "UserListItems");
			t.Delete(db.DelimColumn("ListID") + " = " + db.DelimParameter("listid"), new Parameter[] { new Parameter("listid", this.ID.Value) });
		}

		public IList<UserListItem> GetItems()
		{
			return UserListItem.FindByList(this);
		}

		public IList<T> GetItems<T>() where T : ActiveRecord
		{
			return UserListItem.FindByList<T>(this);
		}

		public void RemoveItem(long item_id)
		{
			var db = this.table.DB;
			var t = Table.Get(db, "UserListItems");
			t.Delete(db.DelimColumn("ListID") + " = " + db.DelimParameter("listid") + " AND " + db.DelimColumn("ItemID") + " = " + db.DelimParameter("itemid"), new Parameter[] { new Parameter("listid", this.ID.Value), new Parameter("itemid", item_id) });
		}

		public void AddItem( long item_id )
		{
			RemoveItem( item_id );

			var lis = this.GetItems();
			int max_order = -1;

			foreach( var li in lis )
			{
				max_order = Math.Max( max_order, li.Order );
			}

			var item = new UserListItem();
			item.ListID = this.ID.Value;
			item.ItemID = item_id;
			item.Order = max_order+1;
			item.DateAdded = DateTime.Now;
			item.Save();
		}

		public void AddItem(long item_id, int order)
		{
			RemoveItem( item_id );

			var lis = this.GetItems();

			foreach( var li in lis )
			{
				if( li.Order >= order )
				{
					li.Order++;
					li.Save();
				}
			}

			var item = new UserListItem();
			item.ListID = this.ID.Value;
			item.ItemID = item_id;
			item.Order = order;
			item.DateAdded = DateTime.Now;
			item.Save();
		}

		public void ReorderItem(long item_id, int fromPosition, int toPosition)
		{
			var item = UserListItem.FindByListAndItem(this.ID.Value, item_id);

			var db = Framework.Data.Database.Get("FDARems");
			string sql = (fromPosition > toPosition)
				? "UPDATE [UserListItems] SET [Order] = [Order] + 1 WHERE [ListID] = @id AND [Order] <= @from AND [Order] >= @to"
				: "UPDATE [UserListItems] SET [Order] = [Order] - 1 WHERE [ListID] = @id AND [Order] >= @from AND [Order] <= @to";

			var ps = new List<Parameter>();
			ps.Add(new Parameter("id", this.ID.Value));
			ps.Add(new Parameter("to", toPosition));
			ps.Add(new Parameter("from", fromPosition));

			db.ExecuteNonQuery(sql, ps.ToArray());

			item.Order = toPosition;
			item.Save();
		}
	}
}
