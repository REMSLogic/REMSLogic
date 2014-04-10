using System;
using System.Data;
using Framework.Data;
using Framework.Security;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "ActivityLog", PrimaryKeyColumn = "ID" )]
    public class ActivityLogEntry : ActiveRecord
    {
        #region Member Variables
        [Column]
        public long? UserID;
        [Column]
        public string SessionID;
        [Column]
        public DateTime RecordedAt;
        [Column]
        public string Action;
        [Column]
        public string Data;
        #endregion

        #region Navigation Properties
        private User _cacheUser;
        public User User
        {
            get{return _cacheUser ?? (_cacheUser = new User(UserID));}
        }
        #endregion

        #region Constructors
        public ActivityLogEntry(long? id = null)
            : base(id)
        {
        }

        public ActivityLogEntry(IDataRecord row)
            : base(row)
        {
        }
        #endregion

        #region Utility Methods
        // at the moment we're simply storing the acitivity.  reporting
        // methods will be added shortly.
        #endregion
    }
}
