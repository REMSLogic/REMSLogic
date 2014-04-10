using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Framework.Data;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "Tasks", PrimaryKeyColumn = "ID" )]
    public class Task : ActiveRecord
    {
        #region Member Variables
        [Column]
        public string Runner;
        [Column]
        public string State;
        [Column]
        public DateTime CreatedAt;
        [Column]
        public DateTime RunAt;
        #endregion

        #region Constructors
        public Task(long? id)
            : base(id)
        {
        }

        public Task(IDataRecord row)
            : base(row)
        {
        }
        #endregion

        #region Utility Methods
        public static IList<Task> FindAllPending()
        {
            var db = Database.Get("FDARems");
            StringBuilder query = new StringBuilder();

            query.Append("SELECT * ");
            query.Append(" FROM " + db.DelimTable("Tasks"));
            //query.Append(" WHERE (Archived = 0) AND (" + db.DelimColumn("UserID") + " = " + db.DelimParameter("id")+ ")");
            query.Append(" ORDER BY " + db.DelimColumn("CreatedAt") + " DESC");

            return db.ExecuteQuery<Task>(query.ToString());
        }
        #endregion
    }
}
