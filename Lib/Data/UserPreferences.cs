using System.Collections.Generic;
using System.Data;
using System.Text;
using Framework.Data;
using Framework.Security;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "UserPreferences", PrimaryKeyColumn = "ID" )]
    public class UserPreferences : ActiveRecord
    {
        #region Member Variables
        [Column]
        public long UserId;
        [Column]
        public bool EmailNotifications;
        #endregion

        #region Navigation Properties
        private User _cacheUser;
        public User User
        {
            get{return _cacheUser ?? (_cacheUser = new User(UserId));}
        }
        #endregion

        #region Constructors
        public UserPreferences(long? id = null)
            : base(id)
        {
        }

        public UserPreferences(IDataRecord row)
            : base(row)
        {
        }
        #endregion

        #region Utility Methods
        public static UserPreferences FindByUser(User user)
        {
            Database db = Database.Get("FDARems");
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT * ");
            sql.Append(" FROM "+db.DelimTable("UserPreferences")+" ");
            sql.Append(" WHERE "+db.DelimColumn("UserId")+" = "+db.DelimParameter("id")+";");

            var ps = new List<Parameter>();
            ps.Add(new Parameter("id", user.ID));

            var rows = db.ExecuteQuery<UserPreferences>(sql.ToString(), ps.ToArray());

            if(rows == null || rows.Count <= 0)
                return null;

            return rows[0];
        }
        #endregion
    }
}
