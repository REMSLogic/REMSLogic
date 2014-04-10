using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Framework.Data;

namespace Framework.Security
{
	public class Group : Data.ActiveRecord
	{
		public static IList<Group> FindAll()
		{
			var db = Database.Get( Config.Manager.Framework.Security.Authentication.Connection );
			string sql = "SELECT * " +
							"FROM " + db.Delim( Config.Manager.Framework.Security.Authentication.Group.Table, DelimType.Table );

			return db.ExecuteQuery<Group>( sql);
		}

		public static Group FindByName(string name)
		{
			var db = Database.Get(Config.Manager.Framework.Security.Authentication.Connection);
			string sql = "SELECT * " +
							"FROM " + db.Delim(Config.Manager.Framework.Security.Authentication.Group.Table, DelimType.Table) + " " +
							"WHERE " + db.Delim("Name", DelimType.Column) + " = "+db.Delim("name", DelimType.Parameter);
			var ps = new List<Parameter>();
            ps.Add(new Parameter("name", name));

			var rows = db.ExecuteQuery<Group>(sql,ps.ToArray());

			if( rows.Count <= 0 )
				return null;

			return rows[0];
		}

		[Column]
		public string Name;
		[Column]
		public string DisplayName;

		public Group(long? id = null)
			: base(Config.Manager.Framework.Security.Authentication.Connection,
				Config.Manager.Framework.Security.Authentication.Group.Table,
				Config.Manager.Framework.Security.Authentication.Group.IDCol, id)
		{ }

		public Group(IDataRecord row)
			: base(Config.Manager.Framework.Security.Authentication.Connection,
				Config.Manager.Framework.Security.Authentication.Group.Table,
				Config.Manager.Framework.Security.Authentication.Group.IDCol, row)
		{ }

		public bool HasRole(Role r)
		{
			var db = Database.Get( Config.Manager.Framework.Security.Authorization.Connection );
			string sql = "SELECT COUNT(1) FROM " + db.Delim( "GroupRoles", DelimType.Table ) + " WHERE " +
							db.Delim( "GroupID", DelimType.Column ) + " = @uid AND " + db.Delim( "RoleID", DelimType.Column ) + " = @rid";
            var ps = new List<Parameter>();
            ps.Add(new Parameter("uid", this.ID.Value));
            ps.Add(new Parameter("rid", r.ID.Value));
			int num = db.ExecuteScalar<int>( sql, ps.ToArray() );

			return (num >= 1);
		}
	}
}
