using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Framework.Data;

namespace Framework.Security
{
	public class User : Data.ActiveRecord
	{
		public static IList<User> FindAll()
		{
			var db = Database.Get( Config.Manager.Framework.Security.Authentication.Connection );
			string sql = "SELECT * FROM " + db.Delim(Config.Manager.Framework.Security.Authentication.User.Table, DelimType.Table);
			return db.ExecuteQuery<User>( sql );
		}

		public static IList<User> FindByGroup(Group g)
		{
			if( g == null || g.ID == null )
				return new List<User>();

				return FindByGroup(g.ID);
		}

		public static IList<User> FindByGroup(long? group_id)
		{
			if( group_id == null )
				return new List<User>();

			var db = Database.Get("FDARems");
			string sql = "SELECT " + db.DelimTable("u") + ".* " +
							" FROM " + db.DelimTable("Users") + " AS " + db.DelimTable("u") +
								" LEFT JOIN " + db.DelimTable("UserGroups") + " AS " + db.DelimTable("ug") +
									" ON " + db.DelimTable("u") + "." + db.DelimColumn("ID") + " = " + db.DelimTable("ug") + "." + db.DelimColumn("UserID") +
							" WHERE " + db.DelimTable("ug") + "." + db.DelimColumn("GroupID") + " = " + db.DelimParameter("id") +
							" ORDER BY " + db.DelimColumn("Username") + " ASC";

			var ps = new [] {
				new Parameter("id",group_id)
			};

			return db.ExecuteQuery<User>(sql, ps);
		}

		[Column]
		public string Username;
		[Column]
		[Newtonsoft.Json.JsonIgnore]
		public string Password;
		[Column]
		[Newtonsoft.Json.JsonIgnore]
		public string PasswordSalt;
		[Column]
		public string Email;
		[Column]
		public DateTime? LastLogin;
		[Column]
		[Newtonsoft.Json.JsonIgnore]
		public Guid? ResetPasswordGuid;

		public User(long? id = null)
			: base( Config.Manager.Framework.Security.Authentication.Connection,
				Config.Manager.Framework.Security.Authentication.User.Table,
				Config.Manager.Framework.Security.Authentication.User.IDCol, id )
		{ }

		public User(IDataRecord row)
			: base( Config.Manager.Framework.Security.Authentication.Connection,
				Config.Manager.Framework.Security.Authentication.User.Table,
				Config.Manager.Framework.Security.Authentication.User.IDCol, row )
		{ }

		public IList<Group> GetGroups()
		{
			if( this.ID == null )
				return new List<Group>();

			string sql = "SELECT [g].* FROM [UserGroups] AS [ug] LEFT JOIN [Groups] AS [g] ON [ug].[GroupID] = [g].[ID] WHERE [ug].[UserID] = @uid";
            var ps = new List<Parameter>();
            ps.Add(new Parameter("uid", this.ID));
			return Database.Get( Config.Manager.Framework.Security.Authentication.Connection ).ExecuteQuery<Group>( sql, ps.ToArray() );
		}

		public void ClearGroups()
		{
			var db = Database.Get( Config.Manager.Framework.Security.Authorization.Connection );
			var tbl = Table.Get( db, "UserGroups" );

            tbl.Delete(db.Delim("UserID", DelimType.Column) + " = @id", new Parameter[] { new Parameter("id", this.ID.Value) });
		}

		public void AddGroup(Group g)
		{
			var tbl = Table.Get(Config.Manager.Framework.Security.Authentication.Connection, "UserGroups");
			var data = new Dictionary<string, object>();
			
			data.Add("UserID", this.ID.Value);
			data.Add("GroupID", g.ID.Value);

			tbl.Insert(data);
		}

		public bool IsInGroup(Group g)
		{
			var db = Database.Get(Config.Manager.Framework.Security.Authorization.Connection);
			string sql = "SELECT COUNT(1) FROM " + db.Delim("UserGroups", DelimType.Table) + " WHERE " +
							db.Delim("UserID", DelimType.Column) + " = @uid AND " + db.Delim("GroupID", DelimType.Column) + " = @gid";
            var ps = new List<Parameter>();
            ps.Add(new Parameter("uid", this.ID.Value));
            ps.Add(new Parameter("gid", g.ID.Value));
			int num = db.ExecuteScalar<int>(sql, ps.ToArray());

			return (num >= 1);
		}

		public bool HasRole(Role r)
		{
			var db = Database.Get( Config.Manager.Framework.Security.Authorization.Connection );
			string sql = "SELECT COUNT(1) FROM "+db.Delim("UserRoles", Data.DelimType.Table)+" WHERE "+
							db.Delim( "UserID", DelimType.Column ) + " = @uid AND " + db.Delim( "RoleID", DelimType.Column ) + " = @rid";
            var ps = new List<Parameter>();
            ps.Add(new Parameter("uid", this.ID.Value));
            ps.Add(new Parameter("rid", r.ID.Value));
			int num = db.ExecuteScalar<int>( sql, ps.ToArray() );

			return (num >= 1);
		}

        public override bool Equals(object obj)
        {
            if(obj == null)
                return false;

            User u = obj as User;
            
            if(u == null)
                return false;

            return u.ID == ID;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
	}
}
