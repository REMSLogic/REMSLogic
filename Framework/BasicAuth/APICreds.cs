using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Framework.Data;

namespace Framework.BasicAuth
{
    public class APICreds : ActiveRecord
    {
        public static IList<APICreds> FindAll()
        {
            var db = Database.Get("Site");
            string sql = "SELECT * FROM [tblAPICreds]";

            return db.ExecuteQuery<APICreds>(sql);
        }

		public static APICreds FindByUser( string user )
		{
			var db = Database.Get( "Site" );
			string sql = "SELECT * FROM [tblAPICreds] WHERE [username] LIKE @user";
			var ps = new List<Parameter>();
			ps.Add( new Parameter( "user", user ) );

			var rows = db.ExecuteQuery<APICreds>( sql, ps.ToArray() );

			if( rows.Count > 0 )
				return rows[0];

			return null;
		}

		[Column]
        public string username;
		[Column]
        public string password;

        public APICreds(long? id = null)
            : base("Site", "tblAPICreds", "clientid", id)
        { }

        public APICreds(IDataRecord row)
            : base("Site", "tblAPICreds", "clientid", row)
        { }
    }
}
