using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Framework.Data;
using Framework.Security;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "NotificationInstances", PrimaryKeyColumn = "ID" )]
    public class NotificationInstance : ActiveRecord
    {
        #region Member Variables
        [Column]
        public long UserID;
        [Column]
        public long NotificationID;
        [Column]
        public DateTime? Read;
        [Column]
        public bool Archived;
        [Column]
        public bool Deleted;
        #endregion

        #region Navigation Properties
        private Notification _cacheNotification;
        public Notification Notification
        {
            get{return _cacheNotification ?? (_cacheNotification = new Notification(NotificationID));}
        }
        #endregion

        #region Constructors
        public NotificationInstance(long? id = null)
            : base(id)
        {
        }

        public NotificationInstance(IDataRecord row)
            : base(row)
        {
        }
        #endregion

        #region Utility Methods
        public static NotificationInstance Create(long userId, long notificationId)
        {
            NotificationInstance ni = new NotificationInstance()
            {
                UserID = userId,
                NotificationID = notificationId,
                Read = null,
                Archived = false,
                Deleted = false
            };

            ni.Save();
            return ni;
        }

        public static NotificationInstance Find(long userId, long notificationId)
        {
            var db = Database.Get( "FDARems" );
            StringBuilder query = new StringBuilder();

            query.Append("SELECT * ");
            query.Append(" FROM " + db.DelimTable("NotificationInstances"));
            query.Append(" WHERE " + db.DelimColumn("UserID") + " = " + db.DelimParameter("userId"));
            query.Append(" AND "+db.DelimColumn("NotificationID")+" = "+db.DelimParameter("notificationId"));
            query.Append(" ORDER BY " + db.DelimColumn("ID") + " DESC");

            var ps = new List<Parameter>
            {
                new Parameter("userId", userId),
                new Parameter("notificationId", notificationId)
            };

            var rows = db.ExecuteQuery<NotificationInstance>(query.ToString(), ps.ToArray());

            if( rows == null || rows.Count <= 0 )
                return null;

            return rows[0];
        }

        public static IList<NotificationInstance> FindNewForUser(User user)
        {
            if (user == null || !user.ID.HasValue)
                return new List<NotificationInstance>();

            var db = Database.Get("FDARems");
            StringBuilder query = new StringBuilder();

            query.Append("SELECT * ");
            query.Append(" FROM " + db.DelimTable("Notifications"));
            query.Append(" INNER JOIN "+db.DelimTable("NotificationInstances")+" ON "+db.DelimTable("Notifications")+"."+db.DelimColumn("ID")+" = "+db.DelimTable("NotificationInstances")+"."+db.DelimColumn("NotificationID"));
            query.Append(" WHERE ("+db.DelimColumn("Deleted")+" = 0) AND ("+db.DelimColumn("Archived")+" = 0) AND (" + db.DelimColumn("UserID") + " = " + db.DelimParameter("id")+ ")");
            query.Append(" ORDER BY " + db.DelimColumn("Sent") + " DESC");

            return db.ExecuteQuery<NotificationInstance>(query.ToString(), new []{new Parameter("id", user.ID.Value)});
        }

        public static IList<NotificationInstance> FindAllForUser(User user)
        {
            if (user == null || !user.ID.HasValue)
                return new List<NotificationInstance>();

            var db = Database.Get("FDARems");
            StringBuilder query = new StringBuilder();

            query.Append("SELECT * ");
            query.Append(" FROM " + db.DelimTable("Notifications"));
            query.Append(" INNER JOIN "+db.DelimTable("NotificationInstances")+" ON "+db.DelimTable("Notifications")+"."+db.DelimColumn("ID")+" = "+db.DelimTable("NotificationInstances")+"."+db.DelimColumn("NotificationID"));
            query.Append(" WHERE ("+db.DelimColumn("Deleted")+" = 0) AND (" + db.DelimColumn("UserID") + " = " + db.DelimParameter("id")+ ")");
            query.Append(" ORDER BY " + db.DelimColumn("Sent") + " DESC");

            return db.ExecuteQuery<NotificationInstance>(query.ToString(), new []{new Parameter("id", user.ID.Value)});
        }
        #endregion
    }
}
