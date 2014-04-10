using System;
using System.Data;
using Framework.Data;

namespace Lib.Data
{
	[Table( DatabaseName = "FDARems", TableName = "Notifications", PrimaryKeyColumn = "ID" )]
    public class Notification : ActiveRecord
    {
        [Column]
        public string Title;
        [Column]
        public string Message;
        [Column]
        public string DataType;
        [Column]
        public long? DataID;
        [Column]
        public DateTime Sent;
        [Column]
        public string Important;
        [Column]
        public string Link;
        [Column]
        public string LinkType;

        public Notification(long? id = null) : base(id )
        { }

        public Notification(IDataRecord row) : base( "FDARems", "Notifications", "ID", row )
        { }
    }
}
