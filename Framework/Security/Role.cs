using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Framework.Data;

namespace Framework.Security
{
	public class Role : Data.ActiveRecord
	{
		public static IList<Role> FindAll()
		{
			var db = Data.Database.Get(Config.Manager.Framework.Security.Authentication.Connection);
			string sql = "SELECT * " +
							"FROM " + db.Delim(Config.Manager.Framework.Security.Authorization.Role.Table, Data.DelimType.Table);

			return db.ExecuteQuery<Role>(sql);
		}

		public static Role FindByName(string name)
		{
			var db = Data.Database.Get(Config.Manager.Framework.Security.Authentication.Connection);
			string sql = "SELECT * " +
							"FROM " + db.Delim(Config.Manager.Framework.Security.Authorization.Role.Table, Data.DelimType.Table) + " " +
							"WHERE " + db.Delim("Name", Data.DelimType.Column) + " = @name";
            var ps = new List<Data.Parameter>();
            ps.Add(new Data.Parameter("name", name));

			var rows = db.ExecuteQuery<Role>(sql, ps.ToArray());

			if (rows.Count <= 0)
				return null;

			return rows[0];
		}

		[Column]
		public string Name;
		[Column]
		public string DisplayName;

		public Role(long? id = null)
			: base( Config.Manager.Framework.Security.Authentication.Connection,
				Config.Manager.Framework.Security.Authorization.Role.Table,
				Config.Manager.Framework.Security.Authorization.Role.IDCol, id )
		{ }

		public Role(IDataRecord row)
			: base( Config.Manager.Framework.Security.Authentication.Connection,
				Config.Manager.Framework.Security.Authorization.Role.Table,
				Config.Manager.Framework.Security.Authorization.Role.IDCol, row )
		{ }
	}
}
